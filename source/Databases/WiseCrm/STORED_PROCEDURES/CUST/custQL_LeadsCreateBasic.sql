USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custQL_LeadsCreateBasic')
	BEGIN
		PRINT 'Dropping Procedure custQL_LeadsCreateBasic'
		DROP  Procedure  dbo.custQL_LeadsCreateBasic
	END
GO

PRINT 'Creating Procedure custQL_LeadsCreateBasic'
GO
/******************************************************************************
**		File: custQL_LeadsCreateBasic.sql
**		Name: custQL_LeadsCreateBasic
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 02/29/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	02/29/2012	Andres Sosa		Created By
*******************************************************************************/
CREATE Procedure dbo.custQL_LeadsCreateBasic
(
	@DealerId INT
	, @LocalizationId VARCHAR(20) = 'en-US'
	, @TeamLocationId INT = 0
	, @SeasonId INT = 0
	, @SalesRepId VARCHAR(10) = 'WEBS001'
	, @LeadSourceId INT = 1
	, @LeadDispositionId INT = 1
	, @Salutation NVARCHAR(50)
	, @FirstName NVARCHAR(50)
	, @MiddleName NVARCHAR(50)
	, @LastName NVARCHAR(50)
	, @Suffix NVARCHAR(50)
	, @SSN NVARCHAR(50) = NULL
	, @DOB DATETIME = NULL
	, @DL NVARCHAR(30) = NULL
	, @DLStateID VARCHAR(4) = NULL
	, @Email NVARCHAR(256)
	, @PhoneHome NVARCHAR(20) = NULL
	, @PhoneWork NVARCHAR(20) = NULL
	, @PhoneMobile NVARCHAR(20) = NULL
	, @StreetAddress NVARCHAR(50)
	, @City NVARCHAR(50)
	, @StateId	VARCHAR(4)
	, @PostalCode VARCHAR(10)
	, @PlusFour VARCHAR(4) = NULL
	, @CountryId VARCHAR(4) = 'USA'
	, @Phone VARCHAR(20)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** INIT arguments */
	IF (@LocalizationId IS NULL) SET @LocalizationId = 'en-US';
	IF (@TeamLocationId IS NULL) SET @TeamLocationId = 0;
	IF (@SeasonId IS NULL) SET @SeasonId = 0;
	IF (@SalesRepId IS NULL) SET @SalesRepId = 'WEBS001';
	IF (@CountryId IS NULL) SET @CountryId = 'USA';
	
	BEGIN TRANSACTION
	/** Create Address */
	INSERT INTO dbo.QL_Address (
		DealerId
		, StateId
		, StreetAddress
		, City
		, PostalCode
		, Phone
	) VALUES (
		@DealerId -- DealerId - int
		, @StateId -- StateId - varchar(4)
		, @StreetAddress -- StreetAddress - nvarchar(50)
		, @City -- City - nvarchar(50)
		, @PostalCode -- PostalCode - nvarchar(10)
		, @Phone -- Phone - varchar(20)
	)
	IF (@@ERROR<>0 AND @@TRANCOUNT>0)
	BEGIN
		PRINT 'ERROR on INSERT INTO dbo.QL_Address'
		ROLLBACK TRANSACTION;
		RETURN;
	END
	DECLARE @AddressID BIGINT
	SET @AddressID = @@IDENTITY
	
	/** Create a Master File first */
	INSERT INTO dbo.AE_CustomerMasterFiles ( DealerId ) VALUES ( @DealerId );
	IF (@@ERROR<>0 AND @@TRANCOUNT>0)
	BEGIN
		PRINT 'ERROR on INSERT INTO dbo.AE_CustomerMasterFiles'
		ROLLBACK TRANSACTION;
		RETURN;
	END
	DECLARE @CMFID BIGINT
	SET @CMFID = @@IDENTITY
	
	/** Create Lead. */
	INSERT INTO dbo.QL_Leads (
		AddressId
		, CustomerMasterFileId
		, DealerId
		, LocalizationId
		, TeamLocationId
		, SeasonId
		, SalesRepId
		, LeadSourceId
		, LeadDispositionId
		, Salutation
		, FirstName
		, MiddleName
		, LastName
		, Suffix
		, SSN
		, DOB
		, DL
		, DLStateID
		, Email
		, PhoneHome
		, PhoneWork
		, PhoneMobile
	) VALUES (
		@AddressID -- AddressId - bigint
		, @CMFID -- CustomerMasterFileId - bigint
		, @DealerId -- DealerId - int
		, @LocalizationId
		, @TeamLocationId
		, @SeasonId
		, @SalesRepId
		, @LeadSourceId
		, @LeadDispositionId
		, @Salutation -- Salutation - nvarchar(50)
		, @FirstName -- FirstName - nvarchar(50)
		, @MiddleName -- MiddleName - nvarchar(50)
		, @LastName -- LastName - nvarchar(50)
		, @Suffix -- Suffix - nvarchar(50)
		, @SSN -- SSN - nvarchar(50)
		, @DOB -- DOB - datetime
		, @DL -- DL - nvarchar(30)
		, @DLStateID -- DLStateID - varchar(4)
		, @Email -- Email - nvarchar(256)
		, @PhoneHome -- PhoneHome - nvarchar(20)
		, @PhoneWork -- PhoneWork - nvarchar(20)
		, @PhoneMobile -- PhoneMobile - nvarchar(20)
	)
	IF (@@ERROR<>0 AND @@TRANCOUNT>0)
	BEGIN
		PRINT 'ERROR on INSERT INTO dbo.QL_Leads'
		ROLLBACK TRANSACTION;
		RETURN;
	END
	DECLARE @LeadID BIGINT
	SET @LeadID = @@IDENTITY
	
	/** Create LeadAddress */
	INSERT INTO dbo.QL_LeadAddress(LeadId, AddressId)
		VALUES (@LeadID, @AddressID);
	IF (@@ERROR<>0 AND @@TRANCOUNT>0)
	BEGIN
		PRINT 'ERROR on INSERT INTO dbo.QL_LeadAddress'
		ROLLBACK TRANSACTION;
		RETURN;
	END
	
	/** Put all things together. */
	INSERT INTO dbo.QL_CustomerMasterLeads (
		CustomerMasterFileId
		, LeadId
	) VALUES (
		@CMFID -- CustomerMasterFileId - bigint
		, @LeadID -- LeadId - bigint
	)
	IF (@@ERROR<>0 AND @@TRANCOUNT>0)
	BEGIN
		PRINT 'ERROR on INSERT INTO dbo.QL_CustomerMasterLeads'
		ROLLBACK TRANSACTION;
		RETURN;
	END
	
	/** Commit Final */
	COMMIT TRANSACTION
	
	/** Return result */
	SELECT * FROM vwQL_LeadBasicInfo WHERE (LeadID = @LeadID)
END
GO

GRANT EXEC ON dbo.custQL_LeadsCreateBasic TO PUBLIC
GO

/** Unit Test 
EXEC dbo.custQL_LeadsCreateBasic
	@DealerID = 5000
	-- , NULL -- @LocalizationId
	-- , NULL -- @TeamLocationId
	-- , NULL -- @SeasonId
	-- , NULL -- @SalesRepID
	, @Salutation = 'Mr'
	, @FirstName = 'Andres'
	, @MiddleName = 'Efraim'
	, @LastName = 'Sosa'
	, @Suffix = 'III'
	-- , NULL -- @SSN
	-- , NULL -- @DOB
	-- , NULL -- @DL
	-- , NULL -- @DLStateID VARCHAR(4)
	, @Email = 'sosawise@me.com' -- @Email NVARCHAR(256)
	-- , NULL -- @PhoneHome NVARCHAR(20)
	-- , NULL -- @PhoneWork NVARCHAR(20)
	-- , NULL -- @PhoneMobile NVARCHAR(20)
	, @StreetAddress = '1184 N 840 E' -- @StreetAddress NVARCHAR(50)
	, @City = 'OREM' -- @City NVARCHAR(50)
	, @StateId = 'UT' -- @StateId	VARCHAR(4)
	, @PostalCode = '84097' -- @PostalCode VARCHAR(10)
	, @PlusFour = '2222' -- @PlusFour VARCHAR(4)
	, @CountryId = NULL -- @CountryId VARCHAR(4)
	, @Phone = '801 26-7067' -- @Phone VARCHAR(20)
*/