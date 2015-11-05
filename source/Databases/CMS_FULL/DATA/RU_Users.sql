USE [WISE_CRM]
GO

/**** CREATE First User */
--SELECT * FROM dbo.MC_PoliticalCountrys;
--SELECT * FROM dbo.MC_PoliticalStates;
--SELECT * FROM dbo.MC_PoliticalTimeZones;

BEGIN TRANSACTION

DELETE [dbo].[AC_Users];
DELETE [dbo].[AE_Dealers];
DELETE [dbo].[RU_Users];
DELETE [dbo].[RU_RecruitAddresses];

DECLARE @UserEmployeeTypeId VARCHAR(20) = (SELECT UserEmployeeTypeID FROM [dbo].[RU_UserEmployeeTypes] WHERE UserEmployeeTypeName = 'Corporate')
	, @PermanentAddressId INT
	, @UserID INT
	, @DealerID INT
	, @AcUserID INT;
PRINT 'Corporate ID: ' + @UserEmployeeTypeId;

DBCC CHECKIDENT ('[dbo].[RU_RecruitAddresses]', RESEED, 0);
INSERT INTO [dbo].[RU_RecruitAddresses] (
	[StateId]
	,[CountryId]
	,[TimeZoneId]
	,[StreetAddress]
	,[StreetAddress2]
	,[City]
	,[PostalCode]
	,[PlusFour]
) VALUES (
	'UT'
	, 'USA' --<CountryId, nvarchar(10),>
	, 8 --<TimeZoneId, int
	,'1184 N 840 E' --<StreetAddress, nvarchar(50),>
	, NULL --<StreetAddress2, nvarchar(50),>
	, 'OREM' --<City, nvarchar(50),>
	, '84097' --<PostalCode, nvarchar(10),>
	, NULL --<PlusFour, nvarchar(4),>
);
SET @PermanentAddressId = SCOPE_IDENTITY();
SELECT * FROM [dbo].[RU_RecruitAddresses] WHERE (AddressId = @PermanentAddressId);

DBCC CHECKIDENT ('[dbo].[RU_Users]', RESEED, 0);
INSERT INTO [dbo].[RU_Users] (
	[RecruitedById]
	,[GPEmployeeId]
	,[UserEmployeeTypeId]
	,[PermanentAddressId]
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
	,[ShirtSize]
	,[HatSize]
	,[DLNumber]
	,[DLState]
	,[DLCountry]
	,[DLExpiresOn]
	,[DLExpiration]
	,[Height]
	,[Weight]
	,[EyeColor]
	,[HairColor]
	,[PhoneHome]
	,[PhoneCell]
	,[PhoneCellCarrierID]
	,[PhoneFax]
	,[Email]
	,[CorporateEmail]
	,[TreeLevel]
	,[HasVerifiedAddress]
	,[RightToWorkExpirationDate]
	,[RightToWorkNotes]
	,[RightToWorkStatusID]
	,[IsLocked]
	,[RecruitedDate]
) VALUES (
	NULL --<RecruitedById, int,>
	, 'SOSAA001' --<GPEmployeeId, nvarchar(25),>
	, @UserEmployeeTypeId -- <UserEmployeeTypeId, varchar(20),>
	, @PermanentAddressId -- <PermanentAddressId, int,>
	, NULL --<SSN, nvarchar(50),>
	, 'Andres' --<FirstName, nvarchar(50),>
	, NULL --<MiddleName, nvarchar(50),>
	, 'Sosa' --<LastName, nvarchar(50),>
	, 'WISE ADMIN' --<PreferredName, nvarchar(50),>
	, 'WISE Architects Inc' --<CompanyName, nvarchar(50),>
	, 1 --<MaritalStatus, bit,>
	, 'Sandra Sosa' --<SpouseName, nvarchar(50),>
	, 'asosa' --<UserName, nvarchar(50),>
	, 'Jugete!98' --<Password, varchar(60),>
	, '12/14/1968' --<BirthDate, datetime,>
	, 'Orem UT USA' --<HomeTown, nvarchar(50),>
	, 'Buenos Aires ARG' --<BirthCity, nvarchar(50),>
	, 'Buenos Aires' --<BirthState, nvarchar(50),>
	, 'ARGENTINA' --<BirthCountry, nvarchar(50),>
	, 1 --<Sex, tinyint,>
	, 4 --<ShirtSize, tinyint,>
	, 4 --<HatSize, tinyint,>
	, '333333333333' --<DLNumber, nvarchar(50),>
	, 'UT' --<DLState, nvarchar(50),>
	, 'USA' --<DLCountry, nvarchar(50),>
	, '12/14/2020' --<DLExpiresOn, datetime,>
	, NULL --<DLExpiration, nvarchar(50),>
	, NULL --<Height, nvarchar(10),>
	, NULL --<Weight, nvarchar(10),>
	, NULL --<EyeColor, nvarchar(20),>
	, NULL --<HairColor, nvarchar(20),>
	, NULL --<PhoneHome, nvarchar(25),>
	, '8018229323' --<PhoneCell, nvarchar(50),>
	, 3 --<PhoneCellCarrierID, smallint,>
	, NULL --<PhoneFax, nvarchar(25),>
	, 'sosawise@gmail.com' --<Email, nvarchar(100),>
	, 'asosa@nexsense.com' --<CorporateEmail, nvarchar(100),>
	, 1 --<TreeLevel, int,>
	, 0 --<HasVerifiedAddress, bit,>
	, 1 --<RightToWorkExpirationDate, datetime,>
	, NULL --<RightToWorkNotes, nvarchar(250),>
	, NULL --<RightToWorkStatusID, int,>
	, 0 --<IsLocked, bit,>
	, GETUTCDATE() --<RecruitedDate, datetime,>
);
SET @UserID = SCOPE_IDENTITY();
SELECT * FROM RU_Users WHERE (UserID = @UserID);

DBCC CHECKIDENT ('[dbo].[AE_Dealers]', RESEED, 4999);
INSERT INTO [dbo].[AE_Dealers] (
	[DealerName]
	,[ContactFirstName]
	,[ContactLastName]
	,[ContactEmail]
	,[PhoneWork]
	,[PhoneMobile]
	,[PhoneFax]
	,[Address]
	,[Address2]
	,[City]
	,[StateAB]
	,[PostalCode]
	,[PlusFour]
	,[Username]
	,[Password]
) VALUES (
	'Master Dealer' --<DealerName, nvarchar(150),>
	, 'Andres' --<ContactFirstName, nvarchar(50),>
	, 'Sosa' --<ContactLastName, nvarchar(50),>
	, 'andres@nuvol9.com' --<ContactEmail, nvarchar(500),>
	, '8018229323' --<PhoneWork, char(20),>
	, '8018229323' --<PhoneMobile, char(20),>
	, NULL --<PhoneFax, char(20),>
	, '1184 N 840 E' --<Address, nvarchar(50),>
	, NULL --<Address2, nvarchar(50),>
	, 'OREM' --<City, nvarchar(50),>
	, 'UT' --<StateAB, char(2),>
	, '84097' --<PostalCode, char(5),>
	, NULL --<PlusFour, char(4),>
	, 'SosaWISE' --<Username, nvarchar(500),>
	, 'Jugete!98' --<Password, nvarchar(20),>
);
SET @DealerID = SCOPE_IDENTITY();
SELECT * FROM [dbo].[AE_Dealers] WHERE (DealerID = @DealerID);

DBCC CHECKIDENT ('[dbo].[AC_Users]', RESEED, 10099);
INSERT INTO [dbo].[AC_Users] (
	[DealerId]
	,[HRUserId]
	,[GPEmployeeID]
	,[SSID]
	,[Username]
	,[Password]
) VALUES (
	@DealerID --<DealerId, int,>
	, @UserID --<HRUserId, int,>
	, 'SOSAA0001' --<GPEmployeeID, nvarchar(25),>
	, NEWID() --<SSID, uniqueidentifier,>
	, 'asosa' --<Username, nvarchar(50),>
	, 'Jugete!98' --<Password, varchar(60),>
);
SET @AcUserID = SCOPE_IDENTITY();
SELECT * FROM [dbo].[AC_Users] WHERE (UserID = @AcUserID);

COMMIT TRANSACTION