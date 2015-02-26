/**************************************
***  Find employees not on CRM yet  ***
***************************************/
/*
SELECT 
	RTRIM(LTRIM(emp.FRSTNAME)) AS FirstName, 
	RTRIM(LTRIM(emp.MIDLNAME)) AS MiddleName, 
	RTRIM(LTRIM(emp.LASTNAME)) AS LastName, 
	--emp.* ,
	addr.*
FROM 
	[DYSNEYDAD].NEX.dbo.UPR00100 as emp WITH(NOLOCK)
	LEFT JOIN [DYSNEYDAD].NEX.dbo.UPR00102 addr WITH(NOLOCK)
		ON (addr.ADRSCODE = emp.ADRSCODE )
			AND (addr.EMPLOYID = emp.EMPLOYID)
	LEFT JOIN WISE_HumanResource.dbo.RU_Users
	ON emp.EMPLOYID = RU_Users.GPEmployeeID
WHERE RU_Users.GPEmployeeID IS NULL
*/
/*******************************
***  CREATE EMPLOYEE IN CRM  ***
********************************/
DECLARE @EMPLOYID char(15)
DECLARE @DealerID int 
DECLARE @ApplicationID varchar(50)
DECLARE @Username varchar(50)
DECLARE @Password varchar(20)
DECLARE @users_employeetypeid varchar(20)
DECLARE @recruit_usertypeid int
DECLARE @SeasonID int

DECLARE @addressID int
DECLARE @ACUserID int

/*
SET THE FOLLOWING:
@EMPLOYID is the username set in GP
@Username is the username that will be created in CRM
@Password is the password to get in to CRM
*/
SET @DealerID = 5000
SET @ApplicationID = 'SSE_CMS_CORS'

-- SET THE FOLLOWING:
SET @EMPLOYID =	'VITEL001'
SET @Username = 'lvitela'
SET @password = 'lvitela'
/*
BYERD001
JOHNM001
SPENJ001
WELLR001
MEJIE001
VAZQA001
*/
SET @users_employeetypeid = 'CORP'
/*
SELECT * FROM WISE_HumanResource.dbo.RU_UserEmployeeTypes
Table: RU_UserEmployeeTypes
UserEmployeeTypeID	UserEmployeeTypeName
	CONT			Contractor
	CORP			Corporate
	DEFAULT			Default
	SALESREP		Sales Rep
	SUBCONT			Sub Contractor
	TECHNCN			Technician
	VENDOR			Vendor
NOTE: SALESREP CAN RUN CREDIT
*/

SET @recruit_usertypeid = 12
/*
SELECT UserTypeID, Description from WISE_HumanResource.dbo.RU_UserType
Table: RU_UserType
UserTypeID	Description
	1		Administrator
	2		Sales Manager
	3		Sales Co-Manager
	4		Sales Assistant Manager
	5		Sales Rep
	6		Technician Lead
	7		Technician
	8		Regional Manager - Technician
	10		Technician Assistant Lead
	11		Regional Manager - Sales
	12		Corporate
	13		Office Assistant
	14		Inventory Manager
	15		Corporate Service
	18		Senior Regional - Sales
	19		National Regional - Sales
	20		National Regional - Technician
	22		Service Technician
	23		Vendor
*/
SET @SeasonID = 2
/*
SELECT SeasonID, SeasonName FROM Wise_HumanResource.dbo.RU_Season
Table: RU_Season
SeasonID	SeasonName
	1		Interim Swing Account Season
	2		Year Round Tech
	3		Inside Sales Group 1
	4		Extended Season Test
	5		Pre Season Test
*/

/*
Users are set up by doing the following:
	1) Create the user in Great Plains - the EMPLOYID is assigned along with the user information (name, social, birthdate, spouse, department, title, etc) is entered into the UPR00100.
	2) In the WISE_HumanResources database enter the user in the RU_USERS table.  Ensure that the GPEmployeeID contains the EMPLOYID from Great Plains. The UserID column is the primary key and is assigned by the database.
	3) In the WISE_AuthenticationControl database enter the user in the AC_Users table.  The UserID from the RU_Users table is inserted into the HRUserID column on the AC_Users table.  The CRM username and password are also on this table.  The default DealerID for Nexsense employees is 5000.
	4) In the Wise_AuthenticationControl database the user is tied to the applications they can access via the AC_UserACLs table.  The Application ID from the AC_Applications table is tied to the UserID from the AC_Users table.  The ApplicationID for CRM is 'SSE_CMS_CORS'

RU_Users
UserID = <assigned>
GPEmployeeID = (Great Plains) UPR00100.EMPLOYID

AC_USERS
UserID = <assigned>
HRUserID = RU_Users.UserID
Username
Password

AC_UserACLs
ACLID = <assigned>
UserID = AC_Users.UserID
ApplicationID = AC_Applications.ApplicationID (CRM = 'SSE_CMS_CORS')
*/



/*
The following is now replaced by the HIRING MANAGER MODULE in CRM:
/****************************
***  RU_RecruitAddresses  ***
*****************************/
-- Insert the address first because the AddressID is used on the RU_Users table
INSERT WISE_HumanResource.dbo.RU_RecruitAddresses (
	StateId, 
	CountryId, 
--	TimeZoneId, 
	StreetAddress, 
	StreetAddress2, 
	City, 
	PostalCode
--	PlusFour, 
	)
SELECT 
	CASE
		WHEN stateabbr.StateAB IS NOT NULL THEN stateabbr.StateID
		WHEN statedesc.StateAB IS NOT NULL THEN statedesc.StateID
		ELSE '00'
	END as StateID,
	CASE
		WHEN countries.CountryAB IS NOT NULL THEN countries.CountryID
		ELSE 'USA'
	END as CountryID,
	--TimeZoneID
	RTRIM(LTRIM(addr.ADDRESS1)) as StreetAddress,
	RTRIM(LTRIM(addr.ADDRESS2)) as StreetAddress2,
	RTRIM(LTRIM(addr.CITY)) as City,
	RTRIM(LTRIM(addr.ZIPCODE)) as PostalCode
	--PlusFour,
FROM 
	[DYSNEYDAD].NEX.dbo.UPR00100 emp WITH(NOLOCK)
	JOIN [DYSNEYDAD].NEX.dbo.UPR00102 addr WITH(NOLOCK)
		ON (addr.ADRSCODE = emp.ADRSCODE )
			AND (addr.EMPLOYID = emp.EMPLOYID)
	LEFT JOIN WISE_HumanResource.dbo.MC_PoliticalStates as stateabbr
		on addr.STATE = stateabbr.StateAB
	LEFT JOIN WISE_HumanResource.dbo.MC_PoliticalStates as statedesc
		on addr.STATE = statedesc.StateName
	LEFT JOIN WISE_HumanResource.dbo.MC_PoliticalCountrys as countries
		on addr.COUNTRY = countries.CountryAB
WHERE 
	emp.EMPLOYID = @EMPLOYID
	AND emp.EMPLOYID NOT IN (SELECT GPEmployeeID FROM WISE_HumanResource.dbo.RU_Users WHERE GPEmployeeID = @EMPLOYID)

SET @addressID = SCOPE_IDENTITY();

/*****************
***  RU_Users  ***
******************/
-- RU_USERS
INSERT WISE_HumanResource.DBO.RU_Users
	(
	RecruitedByID, 
	GPEmployeeID, 
	UserEmployeeTypeId, 
	PermanentAddressID, 
	SSN, 
	FirstName, 
	MiddleName, 
	LastName, 
	PreferredName, 
	CompanyName, 
	MaritalStatus, 
	SpouseName, 
	--UserName, 
	--Password, 
	BirthDate, 
	HomeTown, 
	BirthCity, 
	BirthState, 
	BirthCountry, 
	Sex
	--, 
	--ShirtSize, 
	--HatSize, 
	--DLNumber, 
	--DLState, 
	--DLCountry, 
	--DLExpiration, 
	--Height, 
	--Weight, 
	--EyeColor, 
	--HairColor, 
	--PhoneHome, 
	--PhoneCell, 
	--PhoneCellCarrierID, 
	--PhoneFax, 
	--Email, 
	--CorporateEmail, 
	--TreeLevel, 
	--HasVerifiedAddress, 
	--RightToWorkExpirationDate, 
	--RightToWorkNotes, 
	--RightToWorkStatusID, 
	--RecruitedDate
	--FullName,  --CALCULATE UPON INSERT
	--PublicFullName, 
	)
SELECT
	--UserID, 
	--FullName, 
	--PublicFullName, 
	1 AS RecruitedByID, 
	@EMPLOYID AS GPEmployeeID, 
	@users_employeetypeid AS UserEmployeeTypeId, 
	@addressID AS PermanentAddressID, 
	RTRIM(LTRIM(emp.SOCSCNUM)) AS SSN, 
	RTRIM(LTRIM(emp.FRSTNAME)) AS FirstName, 
	RTRIM(LTRIM(emp.MIDLNAME)) AS MiddleName, 
	RTRIM(LTRIM(emp.LASTNAME)) AS LastName, 
	RTRIM(LTRIM(emp.FRSTNAME)) AS PreferredName, 
	'Nexsense LLC' AS CompanyName, 
	CASE 
		WHEN emp.MARITALSTATUS = 2 THEN 0 --SINGLE = 2 IN GP, 0 IN CRM 
		ELSE emp.MARITALSTATUS
	END AS MaritalStatus, 
	LTRIM(RTRIM(emp.SPOUSE)) AS SpouseName, 
	--@Username AS UserName, 
	--@Password AS Password, 
	emp.BRTHDATE as BirthDate, 
	'' AS HomeTown, 
	'' AS BirthCity, 
	'' AS BirthState, 
	'' AS BirthCountry, 
	CASE
		WHEN emp.GENDER = 2 THEN 0 --FEMALE = 2 IN GP, 0 IN CRM
		ELSE emp.GENDER
	END AS Sex
	--, 
	--0 AS ShirtSize, 
	--0 AS HatSize, 
	--'' AS DLNumber, 
	--'' AS DLState, 
	--'' AS DLCountry, 
	--'' AS DLExpiration, 
	--'' AS Height, 
	--'' AS Weight, 
	--'' AS EyeColor, 
	--'' AS HairColor, 
	--'' AS PhoneHome, 
	--'' AS PhoneCell, 
	--'' AS PhoneCellCarrierID, 
	--'' AS PhoneFax, 
	--'' AS Email, 
	--'' AS CorporateEmail, 
	--0 AS TreeLevel, 
	--0 AS HasVerifiedAddress, 
	--GETDATE() AS RightToWorkExpirationDate, 
	--'' AS RightToWorkNotes, 
	--0 AS RightToWorkStatusID
	-- THE FOLLOWING COLUMNS ARE INSERTED WITH THE DEFAULT:
	--IsLocked, 
	--IsActive, 
	--IsDeleted, 
	--RecruitedDate, 
	--CreatedBy, 
	--CreatedOn, 
	--ModifiedBy, 
	--ModifiedOn
FROM 
	[DYSNEYDAD].NEX.dbo.UPR00100 emp WITH(NOLOCK)
WHERE 
	--(emp.EMPLOYID NOT IN (SELECT GPEmployeeID FROM WISE_HumanResource.DBO.RU_Users))
--	AND 
	(emp.EMPLOYID = @EMPLOYID)

--SET @UserID = SCOPE_IDENTITY()
*/

-- Hiring Manager is currently set to insert the employee with the IsActive flag set to 'false'.  If so,
-- set the flag to true.
UPDATE RU_Users
SET IsActive = 'True'
FROM WISE_HumanResource.dbo.RU_Users AS RU_Users
WHERE GPEmployeeId = @EMPLOYID
AND IsActive = 'False'

/********************
***  RU_Recruits  ***
*********************/
/* SHUMWAY ADDED THIS TO HR MANAGER
INSERT WISE_HumanResource.dbo.RU_Recruits (
	--RecruitID, 
	UserID, 
	UserTypeId, 
	--ReportsToID, 
	--CurrentAddressID, 
	SeasonID, 
	--OwnerApprovalId, 
	TeamID, 
	--PayScaleID, 
	--SchoolId, 
	--ShackingUpId, 
	--RecruitCohabbitTypeId, 
	--AlternatePayScheduleID, 
	DealerId, 
	--Location, 
	--OwnerApprovalDate, 
	--ManagerApprovalDate, 
	--EmergencyName, 
	--EmergencyPhone, 
	--EmergencyRelationship, 
	IsRecruiter, 
	--PreviousSummer, 
	--SignatureDate, 
	--HireDate, 
	--GPExemptions, 
	--GPW4Allowances, 
	--GPW9Name, 
	--GPW9BusinessName, 
	--GPW9TIN, 
	SocialSecCardStatusID, 
	DriversLicenseStatusID, 
	W4StatusID, 
	I9StatusID, 
	W9StatusID, 
	--SocialSecCardNotes, 
	--DriversLicenseNotes, 
	--W4Notes, 
	--I9Notes, 
	--W9Notes, 
	--EIN, 
	--SUTA, 
	--WorkersComp, 
	--FedFilingStatus, 
	--EICFilingStatus, 
	--TaxWitholdingState, 
	--StateFilingStatus, 
	--GPDependents, 
	--CriminalOffense, 
	--Offense, 
	--OffenseExplanation, 
	--Rent, 
	--Pet, 
	--Utilities, 
	--Fuel, 
	--Furniture, 
	--CellPhoneCredit, 
	--GasCredit, 
	RentExempt, 
	IsServiceTech, 
	--StateId, 
	--CountryId, 
	--StreetAddress, 
	--StreetAddress2, 
	--City, 
	--PostalCode, 
	--CBxSocialSecCard, 
	--CBxDriversLicense, 
	--CBxW4, 
	--CBxI9, 
	--CBxW9, 
	IsActive, 
	IsDeleted,
	CreatedByID 
	--CreatedDate, 
	--ModifiedByID, 
	--ModifiedDate, 
	--CreatedBy, 
	--CreatedOn, 
	--ModifiedBy, 
	--ModifiedOn
	)
	SELECT
	--RecruitID, 
	RU_Users.UserID AS UserID, 
	@recruit_usertypeid AS UserTypeId, 
	--ReportsToID, 
	--CurrentAddressID, 
	@SeasonID AS SeasonID, 
	--OwnerApprovalId, 
	1 AS TeamID, 
	--PayScaleID, 
	--SchoolId, 
	--ShackingUpId, 
	--RecruitCohabbitTypeId, 
	--AlternatePayScheduleID, 
	@DealerID AS DealerId, 
	--Location, 
	--OwnerApprovalDate, 
	--ManagerApprovalDate, 
	--EmergencyName, 
	--EmergencyPhone, 
	--EmergencyRelationship, 
	0 AS IsRecruiter, 
	--PreviousSummer, 
	--SignatureDate, 
	--HireDate, 
	--GPExemptions, 
	--GPW4Allowances, 
	--GPW9Name, 
	--GPW9BusinessName, 
	--GPW9TIN, 
	0 AS SocialSecCardStatusID, 
	0 AS DriversLicenseStatusID, 
	0 AS W4StatusID, 
	0 AS I9StatusID, 
	0 AS W9StatusID, 
	--SocialSecCardNotes, 
	--DriversLicenseNotes, 
	--W4Notes, 
	--I9Notes, 
	--W9Notes, 
	--EIN, 
	--SUTA, 
	--WorkersComp, 
	--FedFilingStatus, 
	--EICFilingStatus, 
	--TaxWitholdingState, 
	--StateFilingStatus, 
	--GPDependents, 
	--CriminalOffense, 
	--Offense, 
	--OffenseExplanation, 
	--Rent, 
	--Pet, 
	--Utilities, 
	--Fuel, 
	--Furniture, 
	--CellPhoneCredit, 
	--GasCredit, 
	0 AS RentExempt, 
	0 AS IsServiceTech, 
	--StateId, 
	--CountryId, 
	--StreetAddress, 
	--StreetAddress2, 
	--City, 
	--PostalCode, 
	--CBxSocialSecCard, 
	--CBxDriversLicense, 
	--CBxW4, 
	--CBxI9, 
	--CBxW9, 
	1 AS IsActive, 
	0 AS IsDeleted, 
	1 AS CreatedByID 
	--AS CreatedDate, 
	--AS ModifiedByID, 
	--AS ModifiedDate, 
	--AS CreatedBy, 
	--AS CreatedOn, 
	--AS ModifiedBy, 
	--AS ModifiedOn
FROM
	WISE_HumanResource.dbo.RU_Users
WHERE
	RU_Users.GPEmployeeID = @EMPLOYID
*/
/*****************
***  AC_Users  ***
******************/
INSERT WISE_AuthenticationControl.dbo.AC_Users
(
	DealerId, 
	HRUserId, 
	GPEmployeeID, 
	--SSID,
	Username, 
	Password
)
SELECT
	@DealerID,
	RU_Users.UserID,
	RU_Users.GPEmployeeID,
	@Username,
	@Password
FROM 
	WISE_HumanResource.dbo.RU_Users
WHERE 
	RU_Users.UserID = (SELECT UserID FROM WISE_HumanResource.dbo.RU_Users WHERE GPEmployeeID = @EMPLOYID)
	AND RU_Users.UserID not in (SELECT HRUserID from WISE_AuthenticationControl.dbo.AC_Users)

SELECT @ACUserID = AC_Users.UserID
FROM 
	WISE_AuthenticationControl.dbo.AC_Users
	JOIN WISE_HumanResource.dbo.RU_Users
		ON AC_Users.HRUserId = RU_Users.UserID 
WHERE RU_Users.GPEmployeeID = @EMPLOYID

/*******************
***  AC_UserACLs ***
********************/
INSERT WISE_AuthenticationControl.dbo.AC_UserACLs 
	(
	UserId, 
	ApplicationId
	)
SELECT
	AC_Users.UserID,
	@ApplicationID
FROM
	WISE_AuthenticationControl.dbo.AC_Users
WHERE 
	AC_Users.UserID NOT IN (
		SELECT AC_Users.UserID 
		FROM WISE_AuthenticationControl.dbo.AC_UserACLs
		WHERE UserId = @ACUserID AND ApplicationId = @ApplicationID
		)
	AND AC_Users.UserID = @ACUserID

/*
DECLARE @EMPLOYID char(15)
SET @EMPLOYID = 'MCFAB001'

SELECT 
	UPR00100.* 
FROM [DYSNEYDAD].NEX.dbo.UPR00100
WHERE EMPLOYID = @EMPLOYID

SELECT 
	'RU_Users' as TableName,
	RU_Users.FullName,
	RU_Users.*
FROM WISE_HumanResource.dbo.RU_Users
--WHERE RU_Users.GPEmployeeID = @EMPLOYID
ORDER BY RU_Users.FullName

SELECT 
	'RU_RecruitAddresses' as TableName,
	RU_USERS.FullName,
	RU_RecruitAddresses.* 
FROM 
	WISE_HumanResource.dbo.RU_Users
	JOIN WISE_HumanResource.dbo.RU_RecruitAddresses
		ON RU_Users.PermanentAddressID = RU_RecruitAddresses.AddressId
--WHERE RU_Users.GPEmployeeID = @EMPLOYID
ORDER BY RU_Users.FullName

SELECT 
	'RU_Recruits' as TableName,
	RU_Users.FullName,
	RU_Recruits.* 
FROM
	WISE_HumanResource.dbo.RU_Users 
	JOIN WISE_HumanResource.dbo.RU_Recruits
		on RU_Users.UserID = RU_Recruits.UserID
--WHERE RU_Users.GPEmployeeID = @EMPLOYID
ORDER BY RU_Users.FullName

SELECT 
	'AC_Users' as TableName,
	RU_Users.FullName,
	AC_Users.*
FROM 
	WISE_HumanResource.dbo.RU_Users
	JOIN WISE_AuthenticationControl.dbo.AC_Users
		on RU_Users.UserID= AC_Users.HRUserId 
--WHERE RU_Users.GPEmployeeID = @EMPLOYID
ORDER BY RU_Users.FullName

SELECT 
	'AC_UserACLs' AS TableName,
	RU_Users.FullName, 
	AC_Users.UserID,
	AC_UserACLs.* 
FROM 
	WISE_HumanResource.dbo.RU_Users
	JOIN WISE_HumanResource.dbo.RU_Recruits
		on RU_Users.UserID = RU_Recruits.UserID 
	JOIN WISE_AuthenticationControl.dbo.AC_Users
		on RU_Recruits.UserID = AC_Users.HRUserId 
	JOIN WISE_AuthenticationControl.dbo.AC_UserACLs
		ON AC_Users.UserID = AC_UserACLs.UserId
--WHERE RU_Users.GPEmployeeID = @EMPLOYID
ORDER BY RU_Users.FullName
*/

/******************************
*** DEACTIVATE AN EMPLOYEE  ***
UPDATE WISE_HumanResource.dbo.RU_Users
SET IsActive = 'FALSE'
WHERE UserID = 1204
*******************************/

/******************************
*** ACTIVATE AN EMPLOYEE  ***
UPDATE dbo.RU_Users
SET IsActive = 'TRUE'
WHERE UserID = 1201
*******************************/