USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGMC_Addresses')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGMC_Addresses'
		DROP  Procedure  dbo.custAE_CustomerSWINGMC_Addresses
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGMC_Addresses'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGMC_Addresses.sql
**		Name: custAE_CustomerSWINGMC_Addresses
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
**		Date: 03/28/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/28/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerSWINGMC_Addresses
(
	@AddressID BIGINT  -- QL_AddressId
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
	
/*step 2.1 Create the MC_Addresses
			Extract InterimAccount Address from [Platinum_Protection_InterimCRM].dbo.MC_Address 
			and Insert into [WISE_CRM].dbo.MC_Addresses
		*/

		INSERT INTO [dbo].[MC_Addresses]
		(
          [QlAddressId]
          ,[DealerId]
          ,[ValidationVendorId]
          ,[AddressValidationStateId]
          ,[StateId]
          ,[CountryId]
          ,[TimeZoneId]
          ,[AddressTypeId]
          ,[StreetAddress]
          ,[StreetAddress2]
          ,[StreetNumber]
          ,[StreetName]
          ,[StreetType]
          ,[PreDirectional]
          ,[PostDirectional]
          ,[Extension]
          ,[ExtensionNumber]
          ,[County]
          ,[CountyCode]
          ,[Urbanization]
          ,[UrbanizationCode]
          ,[City]
          ,[PostalCode]
          ,[PlusFour]
          ,[Phone]
          ,[DeliveryPoint]
          ,[Latitude]
          ,[Longitude]
          ,[CongressionalDistric]
          ,[DPV]
          ,[DPVResponse]
          ,[DPVFootNote]
          ,[CarrierRoute]
		)
		SELECT
		    @AddressID -- AddressId from Premise Address
          ,QL_Address.[DealerId]
          ,QL_Address.[ValidationVendorId]
          ,QL_Address.[AddressValidationStateId]
          ,QL_Address.[StateId]
          ,QL_Address.[CountryId]
          ,QL_Address.[TimeZoneId]
          ,QL_Address.[AddressTypeId]
          ,QL_Address.[StreetAddress]
          ,QL_Address.[StreetAddress2]
          ,QL_Address.[StreetNumber]
          ,QL_Address.[StreetName]
          ,QL_Address.[StreetType]
          ,QL_Address.[PreDirectional]
          ,QL_Address.[PostDirectional]
          ,QL_Address.[Extension]
          ,QL_Address.[ExtensionNumber]
          ,QL_Address.[County]
          ,QL_Address.[CountyCode]
          ,QL_Address.[Urbanization]
          ,QL_Address.[UrbanizationCode]
          ,QL_Address.[City]
          ,QL_Address.[PostalCode]
          ,QL_Address.[PlusFour]
          ,NULL
          ,QL_Address.[DeliveryPoint]
          ,QL_Address.[Latitude]
          ,QL_Address.[Longitude]
          ,QL_Address.[CongressionalDistric]
          ,QL_Address.[DPV]
          ,QL_Address.[DPVResponse]
          ,QL_Address.[DPVFootNote]
          ,QL_Address.[CarrierRoute]			

        FROM
		[Wise_CRM].[dbo].[QL_Address]
		WHERE
		QL_Address.[AddressID] =  @AddressID

		SELECT SCOPE_IDENTITY()

		



	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerSWINGMC_Addresses TO PUBLIC
GO

/** EXEC dbo.SPROC_NAME */