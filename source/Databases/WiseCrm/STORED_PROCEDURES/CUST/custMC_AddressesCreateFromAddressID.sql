USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_AddressesCreateFromAddressID')
	BEGIN
		PRINT 'Dropping Procedure custMC_AddressesCreateFromAddressID'
		DROP  Procedure  dbo.custMC_AddressesCreateFromAddressID
	END
GO

PRINT 'Creating Procedure custMC_AddressesCreateFromAddressID'
GO
/******************************************************************************
**		File: custMC_AddressesCreateFromAddressID.sql
**		Name: custMC_AddressesCreateFromAddressID
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
**		Date: 05/24/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/24/2012	Andres Sosa		Created By
**			
*******************************************************************************/
CREATE Procedure dbo.custMC_AddressesCreateFromAddressID
(
	@AddressId BIGINT
)
AS
BEGIN
	/** Initialize */
	DECLARE @NewAddressID BIGINT
	
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY
		
		BEGIN TRANSACTION
		/** Transfer data */
		
		INSERT INTO MC_Addresses(
			QlAddressId
			, DealerId
			, ValidationVendorId
			, AddressValidationStateId
			, StateId
			, CountryId
			, TimeZoneId
			, AddressTypeId
			, StreetAddress
			, StreetAddress2
			, StreetNumber
			, StreetName
			, StreetType
			, PreDirectional
			, PostDirectional
			, Extension
			, ExtensionNumber
			, County
			, CountyCode
			, Urbanization
			, UrbanizationCode
			, City
			, PostalCode
			, PlusFour
			, Phone
			, DeliveryPoint
			, Latitude
			, Longitude
			, CongressionalDistric
			, DPV
			, DPVResponse
			, DPVFootNote
			, CarrierRoute)
		SELECT
			NULL AS QlAddressId
			, DealerId
			, ValidationVendorId
			, AddressValidationStateId
			, StateId
			, CountryId
			, TimeZoneId
			, AddressTypeId
			, StreetAddress
			, StreetAddress2
			, StreetNumber
			, StreetName
			, StreetType
			, PreDirectional
			, PostDirectional
			, Extension
			, ExtensionNumber
			, County
			, CountyCode
			, Urbanization
			, UrbanizationCode
			, City
			, PostalCode
			, PlusFour
			, Phone
			, DeliveryPoint
			, Latitude
			, Longitude
			, CongressionalDistric
			, DPV
			, DPVResponse
			, DPVFootNote
			, CarrierRoute
		FROM
			MC_Addresses AS ADR WITH (NOLOCK)
		WHERE
			(ADR.AddressID = @AddressId)
		
		SET @NewAddressID = SCOPE_IDENTITY()
		PRINT 'NewAddressID = ' + CAST(@NewAddressID AS VARCHAR)

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
		RETURN
	END CATCH
	
	/** Return result */
	SELECT * FROM dbo.MC_Addresses WHERE AddressID = @NewAddressID;
END
GO

GRANT EXEC ON dbo.custMC_AddressesCreateFromAddressID TO PUBLIC
GO