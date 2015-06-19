USE [WISE_CRM]

/** This is for fixing Addresses before the account is put online with Monitronics. 
* This only works if the PRI is the same as the MONI.
*/
DECLARE @AccountID BIGINT = 191286
	, @CMFID BIGINT = 3091665
	, @LeadID BIGINT
	, @CustomerID BIGINT
	, @CustomerAccountID BIGINT;

BEGIN TRANSACTION
SELECT 
	@CustomerAccountID = AECA.CustomerAccountID 
	, @LeadID = AECA.LeadId
	, @CustomerID = AEC.CustomerID
FROM
	dbo.AE_CustomerAccounts AS AECA WITH (NOLOCK)
	INNER JOIN dbo.AE_Customers AS AEC WITH (NOLOCK)
	ON
		(AEC.CustomerID = AECA.CustomerId)
WHERE
	(CustomerMasterFileId = @CMFID) AND (AECA.CustomerTypeId = 'MONI');

--INSERT INTO [dbo].[QL_Address]([DealerId],[ValidationVendorId],[AddressValidationStateId],[StateId],[CountryId],[TimeZoneId],[AddressTypeId],[SeasonId],[TeamLocationId],[SalesRepId],[StreetAddress],[StreetAddress2],[StreetNumber],[StreetName],[StreetType],[PreDirectional],[PostDirectional],[Extension],[ExtensionNumber]
--           ,[County],[CountyCode],[Urbanization],[UrbanizationCode],[City],[PostalCode],[PlusFour],[PostalCodeFull],[Phone],[DeliveryPoint],[CrossStreet],[Latitude],[Longitude],[CongressionalDistric]
--           ,[DPV],[DPVResponse],[DPVFootnote],[CarrierRoute])
--SELECT ADR.DealerId,ADR.ValidationVendorId,ADR.AddressValidationStateId,ADR.StateId,ADR.CountryId,ADR.TimeZoneId,ADR.AddressTypeId,ADR.SeasonId,ADR.TeamLocationId,ADR.SalesRepId,ADR.StreetAddress,ADR.StreetAddress2,ADR.StreetNumber,ADR.StreetName,ADR.StreetType,ADR.PreDirectional,ADR.PostDirectional,ADR.Extension,ADR.ExtensionNumber,ADR.County,ADR.CountyCode,ADR.Urbanization,ADR.UrbanizationCode,ADR.City,ADR.PostalCode,ADR.PlusFour,ADR.PostalCodeFull,ADR.Phone,ADR.DeliveryPoint,ADR.CrossStreet,ADR.Latitude,ADR.Longitude,ADR.CongressionalDistric,ADR.DPV,ADR.DPVResponse,ADR.DPVFootnote,ADR.CarrierRoute
--FROM
--	dbo.QL_Leads AS QLL WITH (NOLOCK)
--	INNER JOIN dbo.QL_Address AS ADR WITH (NOLOCK)
--	ON
--		(ADR.AddressID = QLL.AddressId)
--WHERE
--	(QLL.LeadID = @LeadID);
DECLARE @QLAddressID BIGINT;
SET @QLAddressID = 122301 --SCOPE_IDENTITY();

INSERT INTO [dbo].[QL_Leads]([AddressId],[CustomerTypeId],[CustomerMasterFileId],[DealerId],[LocalizationId],[TeamLocationId],[SeasonId],[SalesRepId],[LeadSourceId],[LeadDispositionId],[LeadDispositionDateChange],[Salutation],[FirstName],[MiddleName],[LastName],[Suffix],[Gender],[SSN],[DOB],[DL],[DLStateID],[Email],[PhoneHome],[PhoneWork],[PhoneMobile],[InsideSalesId])
SELECT @QLAddressID,CustomerTypeId,CustomerMasterFileId,DealerId,LocalizationId,TeamLocationId,SeasonId,SalesRepId,LeadSourceId,LeadDispositionId,LeadDispositionDateChange,Salutation,FirstName,MiddleName,LastName,Suffix,Gender,SSN,DOB,DL,DLStateID,Email,PhoneHome,PhoneWork,PhoneMobile,InsideSalesId
FROM
	dbo.QL_Leads 
WHERE
	(LeadID = @LeadID);
DECLARE @NewLeadID BIGINT;
SET @NewLeadID = SCOPE_IDENTITY();
-- ** Create the Customer now.
INSERT INTO [dbo].[MC_Addresses]([QlAddressId],[DealerId],[ValidationVendorId],[AddressValidationStateId],[StateId],[CountryId],[TimeZoneId],[AddressTypeId],[StreetAddress],[StreetAddress2],[StreetNumber],[StreetName],[StreetType],[PreDirectional],[PostDirectional],[Extension],[ExtensionNumber],[County],[CountyCode],[Urbanization],[UrbanizationCode],[City],[PostalCode],[PlusFour],[Phone],[DeliveryPoint],[CrossStreet],[Latitude],[Longitude],[CongressionalDistric],[DPV],[DPVResponse],[DPVFootNote],[CarrierRoute])
SELECT [AddressID],[DealerId],[ValidationVendorId],[AddressValidationStateId],[StateId],[CountryId],[TimeZoneId],[AddressTypeId],[StreetAddress],[StreetAddress2],[StreetNumber],[StreetName],[StreetType],[PreDirectional],[PostDirectional],[Extension],[ExtensionNumber],[County],[CountyCode],[Urbanization],[UrbanizationCode],[City],[PostalCode],[PlusFour],[Phone],[DeliveryPoint],[CrossStreet],[Latitude],[Longitude],[CongressionalDistric],[DPV],[DPVResponse],[DPVFootnote],[CarrierRoute]
FROM dbo.QL_Address WHERE (AddressID = @QLAddressID)
DECLARE @McAddressID BIGINT
SET @McAddressID = SCOPE_IDENTITY();

INSERT INTO [dbo].[AE_Customers]([CustomerTypeId],[CustomerMasterFileId],[DealerId],[AddressId],[LeadId],[LocalizationId],[Prefix],[FirstName],[MiddleName],[LastName],[Postfix],[BusinessName],[Gender],[PhoneHome],[PhoneWork],[PhoneMobile],[Email],[DOB],[SSN],[Username],[Password])
SELECT 'MONI',[CustomerMasterFileId],[DealerId],@McAddressID,@NewLeadID,[LocalizationId],[Prefix],[FirstName],[MiddleName],[LastName],[Postfix],[BusinessName],[Gender],[PhoneHome],[PhoneWork],[PhoneMobile],[Email],[DOB],[SSN],[Username],[Password]
FROM [dbo].AE_Customers
WHERE
	(CustomerID = @CustomerID);
DECLARE @NewCustomerID BIGINT;
SET @NewCustomerID = SCOPE_IDENTITY();

UPDATE dbo.AE_CustomerAccounts SET
	CustomerID = @NewCustomerID
	, LeadId = @NewLeadID
WHERE
	(CustomerAccountID = @CustomerAccountId);

--// Update the premise address on MsAccount
UPDATE dbo.MS_Accounts SET PremiseAddressId = @McAddressID WHERE (AccountID = @AccountID);


--// Get the QL_CustomerMasterLeads
DECLARE @CustomerMasterLeadID UNIQUEIDENTIFIER;
SELECT @CustomerMasterLeadID = CustomerMasterLeadID FROM dbo.QL_CustomerMasterLeads WHERE (CustomerMasterFileId = @CMFID) AND (CustomerTypeId = 'MONI');
UPDATE dbo.QL_CustomerMasterLeads SET LeadId = @NewLeadID WHERE CustomerMasterLeadID = @CustomerMasterLeadID;

DECLARE @CreditReportVendorManualID BIGINT;
INSERT INTO dbo.QL_CreditReportVendorManual (BureauId, Score, IsScored, IsHit, Report)
SELECT
	QCRVM.BureauId
	,QCRVM.Score
	,QCRVM.IsScored
	,QCRVM.IsHit
	,QCRVM.Report
FROM
	dbo.QL_CreditReports AS QLCR WITH (NOLOCK)
	INNER JOIN dbo.QL_CreditReportVendorManual AS QCRVM WITH (NOLOCK)
	ON
		(QCRVM.CreditReportVendorManualID = QLCR.CreditReportVendorManualId)
WHERE
	(LeadId = @LeadID);
SET @CreditReportVendorManualID = SCOPE_IDENTITY();

INSERT INTO [dbo].[QL_CreditReports] ([LeadId],[AddressId],[BureauId],[SeasonId],[CreditReportVendorId],[CreditReportVendorManualId],[Prefix],[FirstName],[MiddleName],[LastName],[Suffix],[SSN],[DOB],[Score],[IsSelected],[IsScored],[IsHit])
SELECT
	@NewLeadID ,
	AddressId ,
	BureauId ,
	SeasonId ,
	CreditReportVendorId ,
	@CreditReportVendorManualID ,
	Prefix ,
	FirstName ,
	MiddleName ,
	LastName ,
	Suffix ,
	SSN ,
	DOB ,
	Score ,
	IsSelected ,
	IsScored ,
	IsHit
FROM
	dbo.QL_CreditReports AS QLCR WITH (NOLOCK)
WHERE
	(QLCR.LeadID = @LeadID)

SELECT * FROM dbo.AE_Customers WHERE (CustomerMasterFileId = @CMFID);

SELECT
	AECA.* 
FROM
	dbo.AE_CustomerAccounts AS AECA WITH (NOLOCK)
	
WHERE
	(AccountId = @AccountID);
EXEC dbo.custMC_AddressGetPremiseByAccountId @AccountID;

ROLLBACK TRANSACTION