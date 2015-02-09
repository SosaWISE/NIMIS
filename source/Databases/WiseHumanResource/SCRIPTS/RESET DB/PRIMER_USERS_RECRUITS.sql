USE [WISE_HumanResource]
GO

BEGIN TRANSACTION

-- DECLARATIONS
DECLARE @UserID INT;

INSERT INTO [dbo].[RU_Users] (
	[RecruitedByID]
	,[GPEmployeeID]
	,[UserEmployeeTypeId]
	--,[PermanentAddressID]
	,[SSN]
	,[FirstName]
	,[MiddleName]
	,[LastName]
	,[PreferredName]
	,[CompanyName]
	,[MaritalStatus]
	,[SpouseName]
	,[UserName]
	,[Password]
	,[BirthDate]
	,[HomeTown]
	,[BirthCity]
	,[BirthState]
	,[BirthCountry]
	,[Sex]
	--,[ShirtSize]
	--,[HatSize]
	--,[DLNumber]
	--,[DLState]
	--,[DLCountry]
	--,[DLExpiration]
	--,[Height]
	--,[Weight]
	--,[EyeColor]
	--,[HairColor]
	--,[PhoneHome]
	--,[PhoneCell]
	--,[PhoneCellCarrierID]
	--,[PhoneFax]
	--,[Email]
	--,[CorporateEmail]
	--,[TreeLevel]
	,[HasVerifiedAddress]
	--,[RightToWorkExpirationDate]
	--,[RightToWorkNotes]
	--,[RightToWorkStatusID]
	--,[IsLocked]
	--,[IsActive]
	--,[IsDeleted]
	--,[RecruitedDate]
	--,[CreatedByID]
	--,[CreatedDate]
	--,[ModifiedByID]
	--,[ModifiedDate]
) VALUES (
	1 --<RecruitedByID, int,>
	, 'SOSA001' --<GPEmployeeID, nvarchar(25),>
	, 'SALESREP' --<UserEmployeeTypeId, varchar(20),>
	--,<PermanentAddressID, int,>
	, 'UG6HPI0/25IGOeRskfS5qw==' -- <SSN, nvarchar(50),>
	, 'Andres' -- <FirstName, nvarchar(50),>
	, 'Efraim' -- <MiddleName, nvarchar(50),>
	, 'Sosa' -- <LastName, nvarchar(50),>
	, 'Super Master' -- <PreferredName, nvarchar(50),>
	, 'WISE Architects Inc' -- <CompanyName, nvarchar(50),>
	, 1 -- <MaritalStatus, bit,>
	, 'Sandee Sosa' -- <SpouseName, nvarchar(50),>
	,'SosaWISE' -- <UserName, nvarchar(50),>
	,'Nexsense' -- <Password, nvarchar(50),>
	, '12/14/1978' -- <BirthDate, datetime,>
	, 'OREM UT' -- <HomeTown, nvarchar(50),>
	, 'Buenos Aires' -- <BirthCity, nvarchar(50),>
	, 'Buenos Aires' -- <BirthState, nvarchar(50),>
	, 'ARGENTINA' -- <BirthCountry, nvarchar(50),>
	, 1 -- <Sex, tinyint,>
	--,<ShirtSize, tinyint,>
	--,<HatSize, tinyint,>
	--,<DLNumber, nvarchar(50),>
	--,<DLState, nvarchar(50),>
	--,<DLCountry, nvarchar(50),>
	--,<DLExpiration, nvarchar(50),>
	--,<Height, nvarchar(10),>
	--,<Weight, nvarchar(10),>
	--,<EyeColor, nvarchar(20),>
	--,<HairColor, nvarchar(20),>
	--,<PhoneHome, nvarchar(25),>
	--,<PhoneCell, nvarchar(50),>
	--,<PhoneCellCarrierID, smallint,>
	--,<PhoneFax, nvarchar(25),>
	--,<Email, nvarchar(100),>
	--,<CorporateEmail, nvarchar(100),>
	--,<TreeLevel, int,>
	, 0 -- <HasVerifiedAddress, bit,>
	--,<RightToWorkExpirationDate, datetime,>
	--,<RightToWorkNotes, nvarchar(250),>
	--,<RightToWorkStatusID, int,>
	--,<IsLocked, bit,>
	--,<IsActive, bit,>
	--,<IsDeleted, bit,>
	--,<RecruitedDate, datetime,>
	--,<CreatedByID, nvarchar(50),>
	--,<CreatedDate, datetime,>
	--,<ModifiedByID, nvarchar(50),>
	--,<ModifiedDate, datetime,>
);

SET @UserID = SCOPE_IDENTITY();

INSERT INTO [dbo].[RU_Recruits] (
		[UserID]
		,[UserTypeId]
		--,[ReportsToID]
		--,[CurrentAddressID]
		,[SeasonID]
		--,[OwnerApprovalId]
		,[TeamID]
		--,[PayScaleID]
		--,[SchoolId]
		--,[ShackingUpId]
		--,[RecruitCohabbitTypeId]
		--,[AlternatePayScheduleID]
		,[DealerId]
		--,[Location]
		--,[OwnerApprovalDate]
		--,[ManagerApprovalDate]
		--,[EmergencyName]
		--,[EmergencyPhone]
		--,[EmergencyRelationship]
		,[IsRecruiter]
		--,[PreviousSummer]
		--,[SignatureDate]
		--,[HireDate]
		--,[GPExemptions]
		--,[GPW4Allowances]
		--,[GPW9Name]
		--,[GPW9BusinessName]
		--,[GPW9TIN]
		,[SocialSecCardStatusID]
		,[DriversLicenseStatusID]
		,[W4StatusID]
		,[I9StatusID]
		,[W9StatusID]
		--,[SocialSecCardNotes]
		--,[DriversLicenseNotes]
		--,[W4Notes]
		--,[I9Notes]
		--,[W9Notes]
		--,[EIN]
		--,[SUTA]
		--,[WorkersComp]
		--,[FedFilingStatus]
		--,[EICFilingStatus]
		--,[TaxWitholdingState]
		--,[StateFilingStatus]
		--,[GPDependents]
		--,[CriminalOffense]
		--,[Offense]
		--,[OffenseExplanation]
		--,[Rent]
		--,[Pet]
		--,[Utilities]
		--,[Fuel]
		--,[Furniture]
		--,[CellPhoneCredit]
		--,[GasCredit]
		,[RentExempt]
		,[IsServiceTech]
		--,[StateId]
		--,[CountryId]
		--,[StreetAddress]
		--,[StreetAddress2]
		--,[City]
		--,[PostalCode]
		--,[CBxSocialSecCard]
		--,[CBxDriversLicense]
		--,[CBxW4]
		--,[CBxI9]
		--,[CBxW9]
		--,[IsActive]
		--,[IsDeleted]
		--,[CreatedByID]
		--,[CreatedDate]
		--,[ModifiedByID]
		--,[ModifiedDate]
		--,[CreatedBy]
		--,[CreatedOn]
		--,[ModifiedBy]
		--,[ModifiedOn]
	) VALUES (
			@UserID --<UserID, int,>
           , 19 --<UserTypeId, smallint,>
           --,<ReportsToID, int,>
           --,<CurrentAddressID, int,>
           , 3 --<SeasonID, int,>SELECT * FROM [WISE_HumanResource].[dbo].RU_Season;
           --,<OwnerApprovalId, int,>
           , 1 -- <TeamID, int,>
           --,<PayScaleID, int,>
           --,<SchoolId, smallint,>
           --,<ShackingUpId, int,>
           --,<RecruitCohabbitTypeId, int,>
           --,<AlternatePayScheduleID, int,>
           , 5000 --<DealerId, int,>
           --,<Location, nvarchar(50),>
           --,<OwnerApprovalDate, datetime,>
           --,<ManagerApprovalDate, datetime,>
           --,<EmergencyName, nvarchar(50),>
           --,<EmergencyPhone, varchar(20),>
           --,<EmergencyRelationship, nvarchar(50),>
           , 0-- <IsRecruiter, bit,>
           --,<PreviousSummer, nvarchar(200),>
           --,<SignatureDate, datetime,>
           --,<HireDate, datetime,>
           --,<GPExemptions, int,>
           --,<GPW4Allowances, tinyint,>
           --,<GPW9Name, nvarchar(50),>
           --,<GPW9BusinessName, nvarchar(100),>
           --,<GPW9TIN, varchar(50),>
           , 0 --<SocialSecCardStatusID, int,>
           , 0 --<DriversLicenseStatusID, int,>
           , 0 --<W4StatusID, int,>
           , 0 --<I9StatusID, int,>
           , 0 --<W9StatusID, int,>
           --,<SocialSecCardNotes, nvarchar(250),>
           --,<DriversLicenseNotes, nvarchar(250),>
           --,<W4Notes, nvarchar(250),>
           --,<I9Notes, nvarchar(250),>
           --,<W9Notes, nvarchar(250),>
           --,<EIN, nvarchar(50),>
           --,<SUTA, nvarchar(50),>
           --,<WorkersComp, nvarchar(max),>
           --,<FedFilingStatus, nvarchar(50),>
           --,<EICFilingStatus, nvarchar(50),>
           --,<TaxWitholdingState, nvarchar(5),>
           --,<StateFilingStatus, nvarchar(50),>
           --,<GPDependents, int,>
           --,<CriminalOffense, bit,>
           --,<Offense, nvarchar(max),>
           --,<OffenseExplanation, nvarchar(max),>
           --,<Rent, money,>
           --,<Pet, money,>
           --,<Utilities, money,>
           --,<Fuel, money,>
           --,<Furniture, money,>
           --,<CellPhoneCredit, money,>
           --,<GasCredit, money,>
           , 1 --<RentExempt, bit,>
           , 0 --<IsServiceTech, bit,>
           --,<StateId, varchar(4),>
           --,<CountryId, nvarchar(50),>
           --,<StreetAddress, nvarchar(50),>
           --,<StreetAddress2, nvarchar(50),>
           --,<City, nvarchar(50),>
           --,<PostalCode, nvarchar(10),>
           --,<CBxSocialSecCard, bit,>
           --,<CBxDriversLicense, bit,>
           --,<CBxW4, bit,>
           --,<CBxI9, bit,>
           --,<CBxW9, bit,>
           --,<IsActive, bit,>
           --,<IsDeleted, bit,>
           --,<CreatedByID, int,>
           --,<CreatedDate, datetime,>
           --,<ModifiedByID, int,>
           --,<ModifiedDate, datetime,>
           --,<CreatedBy, nvarchar(50),>
           --,<CreatedOn, datetime,>
           --,<ModifiedBy, nvarchar(50),>
           --,<ModifiedOn, datetime,>
		);

/** Create a Technician. */
INSERT INTO [dbo].[RU_Users] (
	[RecruitedByID]
	,[GPEmployeeID]
	,[UserEmployeeTypeId]
	--,[PermanentAddressID]
	,[SSN]
	,[FirstName]
	,[MiddleName]
	,[LastName]
	,[PreferredName]
	,[CompanyName]
	,[MaritalStatus]
	,[SpouseName]
	,[UserName]
	,[Password]
	,[BirthDate]
	,[HomeTown]
	,[BirthCity]
	,[BirthState]
	,[BirthCountry]
	,[Sex]
	--,[ShirtSize]
	--,[HatSize]
	--,[DLNumber]
	--,[DLState]
	--,[DLCountry]
	--,[DLExpiration]
	--,[Height]
	--,[Weight]
	--,[EyeColor]
	--,[HairColor]
	--,[PhoneHome]
	--,[PhoneCell]
	--,[PhoneCellCarrierID]
	--,[PhoneFax]
	--,[Email]
	--,[CorporateEmail]
	--,[TreeLevel]
	,[HasVerifiedAddress]
	--,[RightToWorkExpirationDate]
	--,[RightToWorkNotes]
	--,[RightToWorkStatusID]
	--,[IsLocked]
	--,[IsActive]
	--,[IsDeleted]
	--,[RecruitedDate]
	--,[CreatedByID]
	--,[CreatedDate]
	--,[ModifiedByID]
	--,[ModifiedDate]
) VALUES (
	1 --<RecruitedByID, int,>
	, 'SYST001' --<GPEmployeeID, nvarchar(25),>
	, 'TECHNCN' --<UserEmployeeTypeId, varchar(20),>
	--,<PermanentAddressID, int,>
	, 'UG6HPI0/25IGOeRskfS5qw==' -- <SSN, nvarchar(50),>
	, 'SYSTEM' -- <FirstName, nvarchar(50),>
	, NULL -- <MiddleName, nvarchar(50),>
	, 'TECHNICIAN' -- <LastName, nvarchar(50),>
	, 'Super TECH' -- <PreferredName, nvarchar(50),>
	, 'Nexsense LLC' -- <CompanyName, nvarchar(50),>
	, 1 -- <MaritalStatus, bit,>
	, 'Sandee Sosa' -- <SpouseName, nvarchar(50),>
	,'SysTech' -- <UserName, nvarchar(50),>
	,'Nexsense' -- <Password, nvarchar(50),>
	, '12/14/1978' -- <BirthDate, datetime,>
	, 'OREM UT' -- <HomeTown, nvarchar(50),>
	, 'OREM' -- <BirthCity, nvarchar(50),>
	, 'UT' -- <BirthState, nvarchar(50),>
	, 'USA' -- <BirthCountry, nvarchar(50),>
	, 1 -- <Sex, tinyint,>
	--,<ShirtSize, tinyint,>
	--,<HatSize, tinyint,>
	--,<DLNumber, nvarchar(50),>
	--,<DLState, nvarchar(50),>
	--,<DLCountry, nvarchar(50),>
	--,<DLExpiration, nvarchar(50),>
	--,<Height, nvarchar(10),>
	--,<Weight, nvarchar(10),>
	--,<EyeColor, nvarchar(20),>
	--,<HairColor, nvarchar(20),>
	--,<PhoneHome, nvarchar(25),>
	--,<PhoneCell, nvarchar(50),>
	--,<PhoneCellCarrierID, smallint,>
	--,<PhoneFax, nvarchar(25),>
	--,<Email, nvarchar(100),>
	--,<CorporateEmail, nvarchar(100),>
	--,<TreeLevel, int,>
	, 0 -- <HasVerifiedAddress, bit,>
	--,<RightToWorkExpirationDate, datetime,>
	--,<RightToWorkNotes, nvarchar(250),>
	--,<RightToWorkStatusID, int,>
	--,<IsLocked, bit,>
	--,<IsActive, bit,>
	--,<IsDeleted, bit,>
	--,<RecruitedDate, datetime,>
	--,<CreatedByID, nvarchar(50),>
	--,<CreatedDate, datetime,>
	--,<ModifiedByID, nvarchar(50),>
	--,<ModifiedDate, datetime,>
);

SET @UserID = SCOPE_IDENTITY();

INSERT INTO [dbo].[RU_Recruits] (
		[UserID]
		,[UserTypeId]
		--,[ReportsToID]
		--,[CurrentAddressID]
		,[SeasonID]
		--,[OwnerApprovalId]
		,[TeamID]
		--,[PayScaleID]
		--,[SchoolId]
		--,[ShackingUpId]
		--,[RecruitCohabbitTypeId]
		--,[AlternatePayScheduleID]
		,[DealerId]
		--,[Location]
		--,[OwnerApprovalDate]
		--,[ManagerApprovalDate]
		--,[EmergencyName]
		--,[EmergencyPhone]
		--,[EmergencyRelationship]
		,[IsRecruiter]
		--,[PreviousSummer]
		--,[SignatureDate]
		--,[HireDate]
		--,[GPExemptions]
		--,[GPW4Allowances]
		--,[GPW9Name]
		--,[GPW9BusinessName]
		--,[GPW9TIN]
		,[SocialSecCardStatusID]
		,[DriversLicenseStatusID]
		,[W4StatusID]
		,[I9StatusID]
		,[W9StatusID]
		--,[SocialSecCardNotes]
		--,[DriversLicenseNotes]
		--,[W4Notes]
		--,[I9Notes]
		--,[W9Notes]
		--,[EIN]
		--,[SUTA]
		--,[WorkersComp]
		--,[FedFilingStatus]
		--,[EICFilingStatus]
		--,[TaxWitholdingState]
		--,[StateFilingStatus]
		--,[GPDependents]
		--,[CriminalOffense]
		--,[Offense]
		--,[OffenseExplanation]
		--,[Rent]
		--,[Pet]
		--,[Utilities]
		--,[Fuel]
		--,[Furniture]
		--,[CellPhoneCredit]
		--,[GasCredit]
		,[RentExempt]
		,[IsServiceTech]
		--,[StateId]
		--,[CountryId]
		--,[StreetAddress]
		--,[StreetAddress2]
		--,[City]
		--,[PostalCode]
		--,[CBxSocialSecCard]
		--,[CBxDriversLicense]
		--,[CBxW4]
		--,[CBxI9]
		--,[CBxW9]
		--,[IsActive]
		--,[IsDeleted]
		--,[CreatedByID]
		--,[CreatedDate]
		--,[ModifiedByID]
		--,[ModifiedDate]
		--,[CreatedBy]
		--,[CreatedOn]
		--,[ModifiedBy]
		--,[ModifiedOn]
	) VALUES (
			@UserID --<UserID, int,>
           , 20 --<UserTypeId, smallint,>
           --,<ReportsToID, int,>
           --,<CurrentAddressID, int,>
           , 3 --<SeasonID, int,>SELECT * FROM [WISE_HumanResource].[dbo].RU_Season;
           --,<OwnerApprovalId, int,>
           , 1 -- <TeamID, int,>
           --,<PayScaleID, int,>
           --,<SchoolId, smallint,>
           --,<ShackingUpId, int,>
           --,<RecruitCohabbitTypeId, int,>
           --,<AlternatePayScheduleID, int,>
           , 5000 --<DealerId, int,>
           --,<Location, nvarchar(50),>
           --,<OwnerApprovalDate, datetime,>
           --,<ManagerApprovalDate, datetime,>
           --,<EmergencyName, nvarchar(50),>
           --,<EmergencyPhone, varchar(20),>
           --,<EmergencyRelationship, nvarchar(50),>
           , 0-- <IsRecruiter, bit,>
           --,<PreviousSummer, nvarchar(200),>
           --,<SignatureDate, datetime,>
           --,<HireDate, datetime,>
           --,<GPExemptions, int,>
           --,<GPW4Allowances, tinyint,>
           --,<GPW9Name, nvarchar(50),>
           --,<GPW9BusinessName, nvarchar(100),>
           --,<GPW9TIN, varchar(50),>
           , 0 --<SocialSecCardStatusID, int,>
           , 0 --<DriversLicenseStatusID, int,>
           , 0 --<W4StatusID, int,>
           , 0 --<I9StatusID, int,>
           , 0 --<W9StatusID, int,>
           --,<SocialSecCardNotes, nvarchar(250),>
           --,<DriversLicenseNotes, nvarchar(250),>
           --,<W4Notes, nvarchar(250),>
           --,<I9Notes, nvarchar(250),>
           --,<W9Notes, nvarchar(250),>
           --,<EIN, nvarchar(50),>
           --,<SUTA, nvarchar(50),>
           --,<WorkersComp, nvarchar(max),>
           --,<FedFilingStatus, nvarchar(50),>
           --,<EICFilingStatus, nvarchar(50),>
           --,<TaxWitholdingState, nvarchar(5),>
           --,<StateFilingStatus, nvarchar(50),>
           --,<GPDependents, int,>
           --,<CriminalOffense, bit,>
           --,<Offense, nvarchar(max),>
           --,<OffenseExplanation, nvarchar(max),>
           --,<Rent, money,>
           --,<Pet, money,>
           --,<Utilities, money,>
           --,<Fuel, money,>
           --,<Furniture, money,>
           --,<CellPhoneCredit, money,>
           --,<GasCredit, money,>
           , 1 --<RentExempt, bit,>
           , 0 --<IsServiceTech, bit,>
           --,<StateId, varchar(4),>
           --,<CountryId, nvarchar(50),>
           --,<StreetAddress, nvarchar(50),>
           --,<StreetAddress2, nvarchar(50),>
           --,<City, nvarchar(50),>
           --,<PostalCode, nvarchar(10),>
           --,<CBxSocialSecCard, bit,>
           --,<CBxDriversLicense, bit,>
           --,<CBxW4, bit,>
           --,<CBxI9, bit,>
           --,<CBxW9, bit,>
           --,<IsActive, bit,>
           --,<IsDeleted, bit,>
           --,<CreatedByID, int,>
           --,<CreatedDate, datetime,>
           --,<ModifiedByID, int,>
           --,<ModifiedDate, datetime,>
           --,<CreatedBy, nvarchar(50),>
           --,<CreatedOn, datetime,>
           --,<ModifiedBy, nvarchar(50),>
           --,<ModifiedOn, datetime,>
		);

ROLLBACK TRANSACTION