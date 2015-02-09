USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGQL_Address')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGQL_Address'
		DROP  Procedure  dbo.SPROC_NAME
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGQL_Address'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGQL_Address.sql
**		Name: custAE_CustomerSWINGQL_Address
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
CREATE Procedure dbo.custAE_CustomerSWINGQL_Address
(
	@InterimAccountID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Transfer data */
		/*step 2. Create the Premise Address
			Extract InterimAccount Address from [Platinum_Protection_InterimCRM].dbo.MC_Address 
			and Insert into [WISE_CRM].dbo.QL_Address
		*/


		INSERT INTO [dbo].[QL_Address]
		(
			[DealerId],
			[ValidationVendorId],
			[AddressValidationStateId],
			[StateId],
			[CountryId],
			[TimeZoneId],
			[AddressTypeId],
			[SeasonId],
			[TeamLocationId],
			[SalesRepId],
			[StreetAddress],
			[StreetAddress2],
			[StreetNumber],
			[StreetName],
			[StreetType],
			[PreDirectional],
			[PostDirectional],
			[Extension],
			[ExtensionNumber],
			[County],
			[CountyCode],
			[Urbanization],
			[UrbanizationCode],
			[City],
			[PostalCode],
			[PlusFour],
			[PostalCodeFull],
			--[Phone],  excluded
			[DeliveryPoint],
			[Latitude],
			[Longitude],
			[CongressionalDistric],
			[DPV],
			[DPVResponse],
			[DPVFootnote],
			[CarrierRoute]
		)
		SELECT
			5000, --default DealerId based on swing documentation
			--MC_Address.[ValidationVendorID], -- this need to be resolve(mapping) - apply case
			CASE
				WHEN [MC_Address].[ValidationVendorID]= 1 THEN 'INTELSEARCH'
				WHEN [MC_Address].[ValidationVendorID]= 2 THEN 'STRIKEIRN'
			END [ValidationVendorId],

			--MC_AddressValidationStates.[AddressValidationStateId],  -- this need to be resolve (mapping) - since not all data matched- apply case
			CASE
				WHEN [MC_AddressStatus].AddressStatus = 'Unverified' THEN 'UNV'
				WHEN [MC_AddressStatus].AddressStatus = 'Bypassed' THEN 'MABP'
				WHEN [MC_AddressStatus].AddressStatus = 'Validated' THEN 'VER'
				WHEN [MC_AddressStatus].AddressStatus = 'Manager Bypassed' THEN 'MAN'
				WHEN [MC_AddressStatus].AddressStatus = 'Failed Verification' THEN 'FVER'
			END  [AddressValidationStateId],

		--	MC_PoliticalState.[StateAB],  -- this need to be resolve (mapping) - since not all data matched apply subquery - because if use join there are tendency that the query will return nothing when StateAB not matched
			ISNULL( (SELECT MCPS.StateAB FROM [WISE_CRM].[dbo].[MC_PoliticalStates] 
			MCPS WHERE MCPS.StateAB = MC_PoliticalState.[StateAB] ) ,'NON') StateId,
			MC_PoliticalCountry.[CountryAB] CountryId, 
			
			--MC_PoliticalTimeZonesNew.[TimeZoneID],  -- this need to be resolve (mapping)  -- since not all data matched - apply case based on the suggested mapping
			CASE
				WHEN [MC_Address].[TimeZoneID] = 8 THEN 8
				WHEN [MC_Address].[TimeZoneID] = 2 THEN 2
				WHEN [MC_Address].[TimeZoneID] = 5 THEN 6
				WHEN [MC_Address].[TimeZoneID] = 3 THEN 4
				WHEN [MC_Address].[TimeZoneID] = 1 THEN 0
				WHEN [MC_Address].[TimeZoneID] = 9 THEN 14
				WHEN [MC_Address].[TimeZoneID] = 6 THEN 8
				WHEN [MC_Address].[TimeZoneID] = 7 THEN 10
			END [TimeZoneId],

			MC_Address.[AddressType],
			0 [SeasonId], -- default SeasonId value based on swing documentation
			0 [TeamLocationId], -- default TeamLocationId value based on swing documentation
			'SYSSWING' [SalesRepId], -- default SalesRepId value based on swing documentation
			MC_Address.[StreetAddress],
			MC_Address.[StreetAddress2],
			MC_Address.[StreetNumber],
			MC_Address.[StreetName],
			MC_Address.[StreetType],
			MC_Address.[PreDirectional],
			MC_Address.[PostDirectional],
			MC_Address.[Extension],
			MC_Address.[ExtensionNumber],
			MC_Address.[County],
			MC_Address.[CountyCode],
			MC_Address.[Urbanization],
			MC_Address.[UrbanizationCode],
			MC_Address.[City],
			MC_Address.[PostalCode],
			MC_Address.[PlusFour],
			--(MC_Address.[PostalCode]+MC_Address.[PlusFour]) [PostalCodeFull], --PostalCodeFull -  Combination of both Postal Code and PlusFour.
			(MC_Address.[PostalCode]+ISNULL(MC_Address.[PlusFour],'')) [PostalCodeFull], --PostalCodeFull -  Combination of both Postal Code and PlusFour.
		
			MC_Address.[DeliveryPoint],
			MC_Address.[Latitude],
			MC_Address.[Longitude],
			MC_Address.[CongressionalDistric],
			MC_Address.[DPV],
			MC_Address.[DPVResponse],
			MC_Address.[DPVFootnote],
			MC_Address.[CarrierRoute]
			--MC_PoliticalState.* ---temporary
			--, MC_AddressStatus.* --temporary
			--, MC_PoliticalCountry.* --temporary

		FROM 
		[Platinum_Protection_InterimCRM].dbo.[MS_Account] 
		INNER JOIN 
		[Platinum_Protection_InterimCRM].dbo.[MC_Address] 
		ON
		MS_Account.PremiseAddressID = MC_Address.AddressID
		
		INNER JOIN
		[Platinum_Protection_InterimCRM].dbo.[MC_PoliticalState]    --- state
		ON
		MC_Address.StateID = MC_PoliticalState.StateID  
		
		INNER JOIN
		[Platinum_Protection_InterimCRM].[dbo].[MC_AddressStatus] -- AddressStatus
		ON
		MC_Address.AddressStatusID = MC_AddressStatus.AddressStatusID

	--	INNER JOIN
	--	[WISE_CRM].[dbo].[MC_AddressValidationStates]
	--	ON
	--	[MC_AddressStatus].AddressStatus = MC_AddressValidationStates.AddressValidationStateName

		INNER JOIN
		[Platinum_Protection_InterimCRM].[dbo].[MC_PoliticalCountry]  -- Country
		ON
		[MC_Address].[CountryID] = MC_PoliticalCountry.[CountryID] 

	
	--	INNER JOIN
	--	[WISE_CRM].[dbo].[MC_PoliticalCountrys] 
	

		--INNER JOIN
		--[Platinum_Protection_InterimCRM].[dbo].[MC_PoliticalTimeZones]  -- TimeZone
		--ON
		--[MC_Address].[TimeZoneID]= [MC_PoliticalTimeZones].TimeZoneID

		--INNER JOIN
		--[WISE_CRM].[dbo].[MC_PoliticalTimeZones] MC_PoliticalTimeZonesNew
		--ON
		--[MC_PoliticalTimeZones].Code  = MC_PoliticalTimeZonesNew.TimeZoneAB
	
	--	INNER JOIN
    --    [Platinum_Protection_InterimCRM].[dbo].[MC_AddressValidationVendor]
	--	ON
	--	[MC_AddressValidationVendor].[ValidationVendorID] = [MC_Address].[ValidationVendorID]

		WHERE
		MS_Account.[AccountID] =  @InterimAccountID

		--SET @AddressID = SCOPE_IDENTITY()
		SELECT SCOPE_IDENTITY()

		--PRINT CAST (@AddressID AS VARCHAR(10))   + ' - AddressID'



	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerSWINGQL_Address TO PUBLIC
GO

/** EXEC dbo.SPROC_NAME */