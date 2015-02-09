USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerCreateFromLead')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerCreateFromLead'
		DROP  Procedure  dbo.custAE_CustomerCreateFromLead
	END
GO

--PRINT 'Creating Procedure custAE_CustomerCreateFromLead'
--GO
--/******************************************************************************
--**		File: custAE_CustomerCreateFromLead.sql
--**		Name: custAE_CustomerCreateFromLead
--**		Desc: 
--**
--**		Return values:
--** 
--**		Called by:   
--**              
--**		Parameters:
--**		Input							Output
--**     ----------						-----------
--**		LeadID
--**		CustomerTypeID
--**		HideResult
--**		CustomerID
--**
--**		Auth: Andres Sosa
--**		Date: 
--*******************************************************************************
--**	Change History
--*******************************************************************************
--**	Date:		Author:			Description:
--**	--------	--------		-------------------------------------------
--**	07/18/14	Bob McFadden	Add Parameter: CustomerAddressTypeID
--**								Add insert into AE_CustomerAddress table
--*******************************************************************************/
--CREATE Procedure dbo.custAE_CustomerCreateFromLead
--(
--	@LeadID BIGINT
--	, @CustomerTypeID VARCHAR(20)
--	, @CustomerAddressTypeID VARCHAR(20)
--	, @HideResult BIT = 1
--	, @CustomerID BIGINT OUTPUT
--)
--AS
--BEGIN
--	PRINT '|*** START SP: dbo.custAE_CustomerCreateFromLead'
--	/** SET NO COUNTING */
--	SET NOCOUNT ON

--	/** Initialize */
--	DECLARE @AddressID BIGINT;
	
--	BEGIN TRY

--		BEGIN TRANSACTION;
	
--		/** Save Data */
--		INSERT INTO MC_Addresses
--			(QlAddressId
--			, DealerId
--			, ValidationVendorId
--			, AddressValidationStateId
--			, StateId
--			, CountryId
--			, TimeZoneId
--			, AddressTypeId
--			, StreetAddress
--			, StreetAddress2
--			, StreetNumber
--			, StreetName
--			, StreetType
--			, PreDirectional
--			, PostDirectional
--			, Extension
--			, ExtensionNumber
--			, County
--			, CountyCode
--			, Urbanization
--			, UrbanizationCode
--			, City
--			, PostalCode
--			, PlusFour
--			, Phone
--			, DeliveryPoint
--			, Latitude
--			, Longitude
--			, CongressionalDistric
--			, DPV
--			, DPVResponse
--			, DPVFootnote
--			, CarrierRoute)
--		SELECT
--			LD.AddressID
--			, LD.DealerId
--			, ValidationVendorId
--			, AddressValidationStateId
--			, StateId
--			, CountryId
--			, TimeZoneId
--			, AddressTypeId
--			, StreetAddress
--			, StreetAddress2
--			, StreetNumber
--			, StreetName
--			, StreetType
--			, PreDirectional
--			, PostDirectional
--			, Extension
--			, ExtensionNumber
--			, County
--			, CountyCode
--			, Urbanization
--			, UrbanizationCode
--			, City
--			, PostalCode
--			, PlusFour
--			, Phone
--			, DeliveryPoint
--			, Latitude
--			, Longitude
--			, CongressionalDistric
--			, DPV
--			, DPVResponse
--			, DPVFootnote
--			, CarrierRoute
--		FROM
--			QL_Address AS QAD WITH (NOLOCK)
--			INNER JOIN dbo.QL_Leads AS LD WITH (NOLOCK)
--			ON
--				(QAD.AddressID = LD.AddressId)
--		WHERE
--			(LD.LeadID = @LeadID);
--		-- Save AddressID
--		SET @AddressID = SCOPE_IDENTITY();
			
--		/** Create Customer*/
--		INSERT INTO AE_Customers (
--			LeadId
--			, AddressId
--			, CustomerTypeId
--			, CustomerMasterFileId
--			, DealerId
--			, LocalizationId
--			, Prefix
--			, FirstName
--			, MiddleName
--			, LastName
--			, Postfix
--			, Gender
--			, SSN
--			, DOB
--			, Email
--			, PhoneHome
--			, PhoneWork
--			, PhoneMobile)
--		SELECT 
--			LeadID
--			, @AddressId
--			, @CustomerTypeId
--			, CustomerMasterFileId
--			, DealerId
--			, LocalizationId
--			, Salutation
--			, FirstName
--			, MiddleName
--			, LastName
--			, Suffix
--			, Gender
--			, SSN
--			, DOB
--			, Email
--			, PhoneHome
--			, PhoneWork
--			, PhoneMobile
--		FROM 
--			QL_Leads AS QL WITH (NOLOCK)
--		WHERE
--			(QL.LeadID = @LeadID);
--		-- Save Customer Id
--		SET @CustomerID = SCOPE_IDENTITY();
--		PRINT 'CustomerID = ' + CAST(@CustomerID AS VARCHAR);

--		/* Create AE_CustomerAddress */
--		INSERT INTO AE_CustomerAddress (
--			CustomerId,
--			AddressId,
--			CustomerAddressTypeId
--		)
--		VALUES (@CustomerID,@AddressID,@CustomerAddressTypeID)

--		COMMIT TRANSACTION;
		
--	END TRY
--	BEGIN CATCH
--		ROLLBACK TRANSACTION;
--		EXEC dbo.wiseSP_ExceptionsThrown;
--		RETURN;
--	END CATCH
	
--	IF (@HideResult <> 1)
--	BEGIN
--		/** Return the Customer just created. */
--		SELECT * FROM dbo.AE_Customers WHERE CustomerID = @CustomerID
--	END	
--	PRINT '|*** END   SP: dbo.custAE_CustomerCreateFromLead'

--END
--GO

--GRANT EXEC ON dbo.custAE_CustomerCreateFromLead TO PUBLIC
--GO

--/** Unit Tests 
--EXEC dbo.custAE_CustomerCreateFromLead 1000132, 'PRI', NULL;
--*/