USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custQL_LeadBasicInfoViewCreate')
	BEGIN
		PRINT 'Dropping Procedure custQL_LeadBasicInfoViewCreate'
		DROP  Procedure  dbo.custQL_LeadBasicInfoViewCreate
	END
GO

PRINT 'Creating Procedure custQL_LeadBasicInfoViewCreate'
GO
/******************************************************************************
**		File: custQL_LeadBasicInfoViewCreate.sql
**		Name: custQL_LeadBasicInfoViewCreate
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
**		Date: 01/13/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/13/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custQL_LeadBasicInfoViewCreate
(
	@AddressId BIGINT
	, @DealerId INT
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
	, @ProductSkwId VARCHAR(50) = NULL -- This is the product offering
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
	IF (@ProductSkwId IS NULL) SET @ProductSkwId = 'HSSS001';
	
	/** DECLARARTIONS */
	DECLARE @CMFID BIGINT
		, @LeadID BIGINT;
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Create a Master File first */
		INSERT INTO dbo.AE_CustomerMasterFiles ( DealerId ) VALUES ( @DealerId );
		SET @CMFID = SCOPE_IDENTITY();

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
		SET @LeadID = @@IDENTITY
	
		/** Create LeadAddress */
		INSERT INTO dbo.QL_LeadAddress(LeadId, AddressId)
			VALUES (@LeadID, @AddressID);

		/** Save Product Offering if available. */
		IF (@ProductSkwId IS NOT NULL)
		BEGIN
			INSERT INTO [dbo].[QL_LeadProductOffers] (
				[LeadId]
				, [ProductSkwId]
				, [SalesRepId]
				, [OfferDate]
	        ) VALUES (
				@LeadID -- LeadId - bigint
				, @ProductSkwId -- ProductSkwId - varchar(50)
				, @SalesRepId -- SalesRepId - varchar(10)
				, GETUTCDATE()  -- OfferDate - datetime
			);
		END
	
		/** Put all things together. */
		INSERT INTO dbo.QL_CustomerMasterLeads (
			CustomerMasterFileId
			, LeadId
		) VALUES (
			@CMFID -- CustomerMasterFileId - bigint
			, @LeadID -- LeadId - bigint
		)

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result. */
	SELECT * FROM vwQL_LeadBasicInfo WHERE LeadID = @LeadID;
END
GO

GRANT EXEC ON dbo.custQL_LeadBasicInfoViewCreate TO PUBLIC
GO

/** EXEC dbo.custQL_LeadBasicInfoViewCreate */