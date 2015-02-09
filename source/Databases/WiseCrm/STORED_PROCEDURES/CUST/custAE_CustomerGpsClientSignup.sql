USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerGpsClientSignup')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerGpsClientSignup'
		DROP  Procedure  dbo.custAE_CustomerGpsClientSignup
	END
GO

PRINT 'Creating Procedure custAE_CustomerGpsClientSignup'
GO
/******************************************************************************
**		File: custAE_CustomerGpsClientSignup.sql
**		Name: custAE_CustomerGpsClientSignup
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
**		Date: 07/16/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	07/16/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerGpsClientSignup
(
	 @DealerId INT = 5000
	, @SalesRepId VARCHAR(10) = 'WEBS001'
	, @LocalizationId VARCHAR(20) = 'en-US'
	, @FirstName NVARCHAR(50)
	, @LastName NVARCHAR(50)
	, @Gender NVARCHAR(10) = 'NOT SET'
	, @PhoneHome VARCHAR(20) = NULL
	, @PhoneWork VARCHAR(30) = NULL
	, @PhoneMobile VARCHAR(20) = NULL
	, @Email VARCHAR(256)
	, @Username NVARCHAR(50)
	, @Password NVARCHAR(50)
	, @LeadSourceId INT = 14 -- Portal
	, @LeadDispositionId INT = 8 -- Signup On Portal
)
AS
BEGIN
	/** Initialize Locals. */
	DECLARE @CustomerMasterFileID BIGINT
		, @CustomerID BIGINT
		, @AddressId BIGINT
		, @CustomerAddressTypeId VARCHAR(20) = 'PREM'
		, @LeadId BIGINT
		, @EmailInUse BIT = 0;

	BEGIN TRY

		BEGIN TRANSACTION;

		/** Check parameters. */
		SELECT @EmailInUse = dbo.fxCustomerGpsClientsCheckDuplicateEmail(@Email);
		PRINT 'Email In Use is ' + CASE WHEN @EmailInUse = 1 THEN 'True' ELSE 'False' END;

		IF (@EmailInUse = 1)
		BEGIN
			DECLARE @msg NVARCHAR(256);
			SET @msg = 'The email ' + @Email + ' is in use already.';
			THROW 51000, @msg, 1;
		END

		/** Create a Master File. */
		INSERT INTO [dbo].AE_CustomerMasterFiles (DealerId) VALUES (@DealerId);
		SET @CustomerMasterFileID = SCOPE_IDENTITY();

		/**
		* Create the Lead and its components
		*/  
		INSERT INTO [dbo].QL_Address (DealerId, ValidationVendorId, AddressValidationstateId, StateId, CountryId, TimeZoneId, AddressTypeId, StreetAddress, City, PostalCode, Latitude, Longitude, DPV) VALUES
		(
			@DealerId
			, 'NOVENDOR' -- No vendor
			, 'UNV' -- Unverified Address
			, '00' -- No State
			, 'NON' -- No Country
			, 0 -- No Time Zone
			, 'S'  -- Standard Address
			, '' -- Address Street
			, '' -- City
			, '' -- Postal Code
			, 0 -- Latitude
			, 0 -- Longitude
			, 0 -- DPV
		);
		SET @AddressId = SCOPE_IDENTITY();
		INSERT INTO [dbo].[QL_Leads] (AddressId, CustomerTypeId, CustomerMasterFileId, DealerId, LocalizationId, TeamLocationId, SeasonId, SalesRepId, LeadSourceId, LeadDispositionId, FirstName, LastName, Gender, Email, PhoneHome, PhoneWork, PhoneMobile) VALUES
		(
			@AddressId
			, 'GPSCLNT'
			, @CustomerMasterFileID
			, @DealerId
			, @LocalizationId
			, 0 -- TeamLocationId
			, 0 -- SeasonId
			, @SalesRepId -- Is not Present then use the default.
			, @LeadSourceId
			, @LeadDispositionId
			, @FirstName
			, @LastName
			, @Gender
			, @Email
			, @PhoneHome
			, @PhoneWork
			, @PhoneMobile
		);
		SET @LeadId = SCOPE_IDENTITY();
		INSERT INTO [dbo].[QL_LeadAddress] (LeadId, AddressId) VALUES (@LeadId, @AddressId);
		SET @AddressId = NULL;

		/** Create an Address. */
		INSERT INTO [dbo].MC_Addresses (DealerID, ValidationVendorId, AddressValidationStateId, StateId, CountryId, TimeZoneId, AddressTypeId, StreetAddress, City, PostalCode, Latitude, Longitude, DPV) VALUES
		(
			@DealerId
			, 'NOVENDOR' -- No vendor
			, 'UNV' -- Unverified Address
			, '00' -- No State
			, 'NON' -- No Country
			, 0 -- No Time Zone
			, 'S'  -- Standard Address
			, '' -- Address Street
			, '' -- City
			, '' -- Postal Code
			, 0 -- Latitude
			, 0 -- Longitude
			, 0 -- DPV
		);
		SET @AddressId = SCOPE_IDENTITY();

		/** Create Customer */
		INSERT INTO [dbo].AE_Customers (CustomerTypeId, CustomerMasterFileId, DealerId, AddressId, LeadId, LocalizationId, FirstName, LastName, Gender, PhoneHome, PhoneWork, PhoneMobile, Email, Username, [Password]) VALUES
		(
			'GPSCLNT' -- Gps Client Customer
			, @CustomerMasterFileID
			, @DealerId
			, @AddressId
			, @LeadId
			, @LocalizationId
			, @FirstName
			, @LastName
			, @Gender
			, @PhoneHome
			, @PhoneWork
			, @PhoneMobile
			, @Email
			, @Username
			, @Password
		);
		SET @CustomerID = SCOPE_IDENTITY();

		/** Create CustomerAddress. */
		INSERT [dbo].AE_CustomerAddress (CustomerId, AddressId, CustomerAddressTypeId) VALUES (@CustomerID, @AddressId, @CustomerAddressTypeId);

		/** Create a CustomerGPS Client. */
		INSERT [dbo].[AE_CustomerGpsClients] (CustomerID, Username, [Password]) VALUES (@CustomerID, @Username, @Password);

		COMMIT TRANSACTION;

		/** Return result. */
		SELECT * FROM dbo.vwAE_CustomerGpsClients WHERE CustomerID = @CustomerID;

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerGpsClientSignup TO PUBLIC
GO

--EXEC dbo.custAE_CustomerGpsClientSignup @FirstName = 'Glenn', @LastName = 'Beck', @PhoneMobile = '8019876541', @Email = 'beck@something1.com', @Username = 'BeckWISE', @Password = 'passTheButter';