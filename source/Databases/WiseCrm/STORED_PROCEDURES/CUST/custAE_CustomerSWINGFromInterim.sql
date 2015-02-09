USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGFromInterim')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGFromInterim'
		DROP  Procedure  dbo.custAE_CustomerSWINGFromInterim
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGFromInterim'
GO
/******************************************************************************************
**		File: custAE_CustomerSWINGFromInterim.sql
**		Name: custAE_CustomerSWINGFromInterim
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
**		Date: 03/24/2014
*******************************************************************************************
**	Change History
*******************************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------------------
**	03/24/2014	Andres Sosa		Created By
**	04/01/2014  Reagan/Junryll	Comply processes based on swing documentation (2. creating stored procedure and save customer information)
**  04/16/2014  Reagan          Added checking if the AccountID is for a full customer or a Lead
**  05/26/2014  Junryl          Added creating and updating of columns of MS_AccountSwungInfo to track the swing process
**  05/26/2014  Junryl          Change the AccountID_Old to InterimAccountID and AccountID_New to MsAccountID
**  05/29/2014  Andres Sosa     Change the AccountID_Old to InterimAccountID and AccountID_New to MsAccountID
**	06/02/2014	Bob McFadden	Call function to convert Emergency Contact Relationship Types
								Call function to convert Emergency Contact Phone Types
**	07/07/2014	Andres Sosa		Fixed a bug that was generated during a refactor.
******************************************************************************************/
CREATE Procedure [dbo].[custAE_CustomerSWINGFromInterim]
(
	@InterimAccountID BIGINT,
	@SwingEquipment BIT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @CustomerMasterFileID BIGINT
		 ,@AddressID BIGINT  -- AddressId generated from creating Premise Address
		 ,@AddressID2 BIGINT -- AddressId generated from MC_Addresses
		,@AccountID BIGINT 

		 ,@Lead1ID BIGINT
		 ,@Lead2ID BIGINT


		 ,@Customer1IDOld BIGINT
		 ,@Customer2IDOld BIGINT

		 ,@Customer1IDNew BIGINT
		 ,@Customer2IDNew BIGINT

		 ,@CreditReportID BIGINT
		 ,@CreditReportVendorMicrobiltID BIGINT
		, @CreditReportVendorAbaraID BIGINT
	
	DECLARE	@SwingEquipmentStatusTable TABLE(SwingEquipmentStatus VARCHAR(2))
	
	DECLARE @AccountStatus VARCHAR(30) --- onboard full or onboard lead
	SET @AccountStatus = dbo.fxGetCustomerSWINGAccountStatus(@InterimAccountID)
	
	--DECLARE @SwingStatus VARCHAR(2)
	DECLARE @SwingStatus VARCHAR(max)
			,@SwingEquipmentStatus VARCHAR(2)
	
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Transfer data */

		/** Create an initial record on MS_AccountSwungInfo that tracks the swing process. Each status column will be updated once the process is successful **/
		INSERT INTO [dbo].[MS_AccountSwungInfo]
		(
			[InterimAccountID]
			,[MsAccountID]
			,[CreatedOn]
			,[CreatedBy]
			,[CustomerMasterFileID]
			,[CustomerMasterFile]
			,[PremiseAddress]
			,[McAccount]
			,[MsAccount]
			,[QlLead]
			,[QlCreditReport]
			,[AeCustomer]
			,[AeCustomerAccount]
			,[MsEmergencyContact]
			,[EquipmentSwung]
			,[SwingStatus]
		)
		VALUES
		(
			@InterimAccountID		
			,null
			,GETDATE()
			,'DevUser' --still need to change this to pull from parameter
			,null
			,null
			,null
			,null
			,null
			,null
			,null
			,null
			,null
			,null
			,NULL
            ,'0'
		)


/*step 1.  Create a Master File Record*/
		INSERT INTO [dbo].[AE_CustomerMasterFiles]
		(
			DealerId
		)
		VALUES
		(
			5000
		)

		SET @CustomerMasterFileId = SCOPE_IDENTITY()
		PRINT  CAST (@CustomerMasterFileId AS VARCHAR(10)) + ' - CustomerMasterFileId'

		/** Update info of MS_AccountSwungInfo **/
		UPDATE [dbo].MS_AccountSwungInfo SET [CustomerMasterFileID] = @CustomerMasterFileId WHERE InterimAccountID = @InterimAccountID;
		UPDATE [dbo].MS_AccountSwungInfo SET [CustomerMasterFile] = GETUTCDATE() WHERE InterimAccountID = @InterimAccountID;

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
				ELSE 'NOVENDOR'
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
			1 [SeasonId], -- default SeasonId value based on swing documentation
			0 [TeamLocationId], -- default TeamLocationId value based on swing documentation
			'MSTR001' [SalesRepId], -- default SalesRepId value based on swing documentation
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
			--MC_Address.[PostalCode],			
			CASE WHEN MC_Address.[PostalCode]='[No Value]' THEN '' ELSE MC_Address.[PostalCode] END AS [PostalCode], -- this prevents truncation error if [No Value]
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
			(MS_Account.[AccountID] =  @InterimAccountID)

		SET @AddressID = SCOPE_IDENTITY()
		
		PRINT CAST (@AddressID AS VARCHAR(10))   + ' - AddressID'


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
          --,QL_Address.[PostalCode]
		  ,CASE WHEN QL_Address.[PostalCode]='[No Value]' THEN '' ELSE QL_Address.[PostalCode] END AS [PostalCode] -- this prevents truncation error if [No Value]
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

		SET @AddressID2 = SCOPE_IDENTITY()
		
		PRINT CAST (@AddressID2 AS VARCHAR(10))   + ' - AddressID - MC_Addresses'

		/** Update info of MS_AccountSwungInfo **/
		UPDATE [dbo].MS_AccountSwungInfo SET [PremiseAddress] = GETUTCDATE() WHERE InterimAccountID = @InterimAccountID;		
	

/*step 3. Create the WCM.MC_Accounts
		*/

		INSERT INTO [WISE_CRM].[dbo].[MC_Accounts]
		(
		   [AccountTypeId]
		  ,[ShipContactId]
		  ,[ShipAddressId]
		  ,[DealerAccountId]
		  ,[ShipContactSameAsCustomer]
		  ,[ShipAddressSameAsCustomer]
		  ,[AccountName]
		  ,[AccountDesc]
		)
		SELECT
			'ALRM',
			NULL,
			NULL,
			NULL,
			1,
			1,
			([MC_Lead].FirstName + ' ' + [MC_Lead].LastName),
			'This is a swung account'
		FROM 
		[Platinum_Protection_InterimCRM].[dbo].[MS_Account]
		INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MC_Lead]
		ON
			[MS_Account].Customer1ID = [MC_Lead].LeadID
		WHERE 
			[MS_Account].AccountID = @InterimAccountID

		SET @AccountID = SCOPE_IDENTITY()
		
		PRINT CAST (@AccountID AS VARCHAR(10)) + '- AccountID'

		/** Update info of MS_AccountSwungInfo **/
		UPDATE [dbo].MS_AccountSwungInfo SET [McAccount] = GETUTCDATE() WHERE InterimAccountID = @InterimAccountID;
		UPDATE [dbo].MS_AccountSwungInfo SET [MsAccountID] = @AccountID WHERE InterimAccountID = @InterimAccountID;

		/** Update SAE_BillingInfoSummary */
		INSERT INTO [dbo].SAE_BillingInfoSummary (
			CustomerMasterFileId
			, AccountId
			, AmountDue
		) VALUES (
			@CustomerMasterFileId -- CustomerMasterFileId - bigint
			, @AccountID -- AccountId - bigint
			, 0 -- AmountDue - money
        );

/*step 4. Create the WCM.MS_Accounts
		*/

		INSERT INTO [WISE_CRM].[dbo].[MS_Accounts]
		(
		       [AccountID]
			  ,[PremiseAddressId]
			  ,[IndustryAccountId]
			  ,[SystemTypeId]
			  ,[CellularTypeId]
			  ,[PanelTypeId]
			  ,[DslSeizureId]
			  ,[PanelItemId]
			  ,[CellPackageItemId]
			  ,[ContractId]
			  ,[TechId]
			  ,[AccountPassword]
			  ,[SimProductBarcodeId]
			  ,[DispatchMessage]
		)

		SELECT
			@AccountID,
			@AddressID2,
			NULL,
			CASE 
				WHEN MonitoringTypeId = 1 THEN 'DIGI'
				WHEN MonitoringTypeId = 2 THEN '2WAY'
				WHEN MonitoringTypeId = 3 THEN '2WYCELL'
			END [SystemTypeId],
			CASE 
				WHEN [AccountCellularTypeId] = 1 THEN 'NOCELL'
				WHEN [AccountCellularTypeId] = 2 THEN 'CELLSEC'
				WHEN [AccountCellularTypeId] = 3 THEN 'CELLPRI'
			END [CellularTypeId],			
			[dbo].fxGetPanelTypeIDFromInterimPanelTypeId(PanelTypeID),  
			CASE
				WHEN [DSLSeizure] = 'Seizure' THEN 3 
				WHEN [DSLSeizure] = 'DSL' THEN 2
				WHEN [DSLSeizure] IS NULL THEN 1

			END [DSLSeizure],
			NULL,  -- PanelItemId
			NULL,  -- CellPackageItemId
			NULL,  -- ContractId
			'SYST001', -- TechId  (This is the Super TECH T.)
			[AbortCode], -- AccountPassword
			NULL, -- SimProductBarcodeId
			NULL -- DispatchMessage
			--[MS_Account].*
		FROM
			[Platinum_Protection_InterimCRM].[dbo].[MS_Account]
		WHERE
			[MS_Account].AccountID =  @InterimAccountID
	
		/** Update info of MS_AccountSwungInfo **/
		UPDATE [dbo].MS_AccountSwungInfo SET [MsAccount] = GETUTCDATE() WHERE InterimAccountID = @InterimAccountID;

/*step 5. Create a Lead
			*/
		--1st Lead  Lead1ID
		SET @Customer1IDOld = (SELECT Customer1ID FROM [Platinum_Protection_InterimCRM].[dbo].[MS_Account] WHERE [AccountID]= @InterimAccountID)	
		IF @Customer1IDOld IS NOT NULL  --- check if @Customer1IDOld is available, if available generate ql_lead for customer1
		BEGIN

			INSERT INTO [WISE_CRM].[dbo].[QL_Leads]
			(
				[AddressId],
				[CustomerTypeId],
				[CustomerMasterFileId],
				[DealerId],
				[LocalizationId],
				[TeamLocationId],
				[SeasonId],
				[SalesRepId],
				[LeadSourceId],
				[LeadDispositionId],
				[LeadDispositionDateChange],
				[Salutation],
		  		[FirstName],
		  		[MiddleName],
				[LastName],
				[Suffix],
				[Gender],
				[SSN],
				[DOB],
				[DL],
				[DLStateID],
				[Email],
				[PhoneHome],
				[PhoneWork], 
				[PhoneMobile]
			)
			SELECT
				@AddressID,
				'LEAD',
				@CustomerMasterFileID,
				5000,
				'en-US',
				0,
				1, -- Found a season that would work.
				'MSTR001',
				15,
			--	14,
				10,
			--	9,	
				GETDATE(),
				[Salutation],
				[FirstName],
		  		[MiddleName],
				[LastName],
				[Suffix],
				'N',
				[SSN],
				[DOB],
				[DL],
				[DLStateID],
				[Email],
				[PhoneHome],
				[PhoneWork]+[PhoneWorkExt], 
				[PhoneCell]

		  FROM [Platinum_Protection_InterimCRM].[dbo].[MC_Lead]
		  WHERE [MC_Lead].LeadID = @Customer1IDOld

		  SET @Lead1ID = SCOPE_IDENTITY()

		  PRINT CAST (@Lead1ID AS VARCHAR(10)) + '- Lead1ID'

	 END

	 
	  --2nd Lead  Lead2ID

	 SET @Customer2IDOld = (SELECT Customer2ID FROM [Platinum_Protection_InterimCRM].[dbo].[MS_Account] WHERE [AccountID]= @InterimAccountID)	
	 IF @Customer2IDOld IS NOT NULL  --- check if Customer2 is available, if available generate ql_lead for customer2
	 BEGIN
			INSERT INTO [WISE_CRM].[dbo].[QL_Leads]
			(
				[AddressId],
				[CustomerTypeId],
				[CustomerMasterFileId],
				[DealerId],
				[LocalizationId],
				[TeamLocationId],
				[SeasonId],
				[SalesRepId],
				[LeadSourceId],
				[LeadDispositionId],
				[LeadDispositionDateChange],
				[Salutation],
		  		[FirstName],
		  		[MiddleName],
				[LastName],
				[Suffix],
				[Gender],
				[SSN],
				[DOB],
				[DL],
				[DLStateID],
				[Email],
				[PhoneHome],
				[PhoneWork], 
				[PhoneMobile]
			)
			SELECT
				@AddressID,
				'LEAD', 
 				@CustomerMasterFileID,
				5000,
				'en-US',
				0,
				1,
				'MSTR001',
				15,
				--14, -- temporary
				10,
				--9, -- temporary	
				GETDATE(),
				[Salutation],
				[FirstName],
		  		[MiddleName],
				[LastName],
				[Suffix],
				'N',
				[SSN],
				[DOB],
				[DL],
				[DLStateID],
				[Email],
				[PhoneHome],
				[PhoneWork]+[PhoneWorkExt], 
				[PhoneCell]

		  FROM [Platinum_Protection_InterimCRM].[dbo].[MC_Lead]
		  WHERE [MC_Lead].LeadID = @Customer2IDOld

		  SET @Lead2ID = SCOPE_IDENTITY()
		  PRINT CAST (@Lead2ID AS VARCHAR(10)) + '- Lead2ID'

	END  -- END IF
	ELSE
	BEGIN
		SET @Lead2ID = NULL
	END

	/** Update info of MS_AccountSwungInfo **/
	UPDATE [dbo].MS_AccountSwungInfo SET [QlLead] = GETUTCDATE() WHERE InterimAccountID = @InterimAccountID;


/*step 6. Move Credit Reports
			*/
	
			-- 6.1 	WCM.QL_CreditReports
			  INSERT INTO [Wise_CRM].[dbo].[QL_CreditReports]
					(
						   [LeadId]
						  ,[AddressId]
						  ,[BureauId]
						  ,[SeasonId]
						  ,[CreditReportVendorId]
						  ,[CreditReportVendorAbaraId]
						  ,[CreditReportVendorMicrobiltId]
						  ,[CreditReportVendorEasyAccessId]
						  ,[CreditReportVendorManualId]
						  ,[Prefix]
						  ,[FirstName]
						  ,[MiddleName]
						  ,[LastName]
						  ,[Suffix]
						  ,[SSN]
						  ,[DOB]
						  ,[Score]
						  ,[IsScored]
						  ,[IsHit]
					)

					SELECT 
						  @Lead1ID, -- not sure on this part 
						  @AddressID,  -- not sure on this part 
						  --QL_CreditReportBureaus.BureauID, not all data matched apply case
						  CASE
							WHEN MC_CreditReport.[BureauID] = 1 THEN 'MN'
							WHEN MC_CreditReport.[BureauID] = 2 THEN 'TU'
							WHEN MC_CreditReport.[BureauID] = 3 THEN 'EQ'
							WHEN MC_CreditReport.[BureauID] = 4 THEN 'EX'
						  END [BureauId], 
						  0, --use 0 for SeasonId
						  QL_CreditReportVendors.CreditReportVendorID,
						  NULL, --	  QL_CreditReportAbara.CreditReportVendorAbaraId, --not sure with this -- we plan to update this credit after CreditReportVendorAbara was generated
						  NULL, --	  QL_CreditReportMicrobilt.CreditReportVendorMicrobiltId, -not sure with this -- we plan to update this credit after CreditReportVendorMicrobilt was generated
						  NULL, --never used
						  NULL, --never used
						  MC_CreditReport.Prefix,
						  MC_CreditReport.FirstName,
						  MC_CreditReport.MiddleName,
						  MC_CreditReport.LastName,
						  MC_CreditReport.Suffix,
						  MC_CreditReport.SSN,
						  MC_CreditReport.DOB,
						  MC_CreditReport.Score,
						  CASE										-- IsScored -- not sure with this
							WHEN   MC_CreditReport.Score = 0 THEN 0
							ELSE 1
						  END  IsScored,
						  0  IsHit -- IsHit -- not sure with this - not clearly defined in the documentation
					FROM
					[Platinum_Protection_InterimCRM].[dbo].[MC_CreditReport]
					INNER JOIN
					[Platinum_Protection_InterimCRM].[dbo].[MS_Account]
					ON
					[MC_CreditReport].[LeadID]= [MS_Account].[Customer1ID]

					--INNER JOIN
					--[WISE_CRM].[dbo].[QL_CreditReportBureaus]
					--ON
					--[MC_CreditReport].BureauName = [QL_CreditReportBureaus].BureauName   -- commented since not all BureauName are equal

					INNER JOIN
					[WISE_CRM].[dbo].[QL_CreditReportVendors]
					ON
					[QL_CreditReportVendors].[CreditReportVendorID] = [MC_CreditReport].[CreditReportVendorID]
		
					WHERE 
					[MS_Account].[AccountID] =  @InterimAccountID

					SET @CreditReportID = SCOPE_IDENTITY()
					
					PRINT CAST (@CreditReportID AS VARCHAR(10)) + '- CreditReportID'
					

			-- 6.2 	WCM.QL_CreditReportVendorMicrobilt   -- not sure if this the correct query here
					
				INSERT INTO [WISE_CRM].[dbo].[QL_CreditReportVendorMicrobilt]
				(
					[BureauId],
					[Score],
					[IsScored],
					[IsHit],
					[CreditReport],
					[MicroBiltGUID]
				)
				SELECT 
					--  QL_CreditReportBureaus.BureauID,
					CASE
							WHEN MC_CreditReport.[BureauID] = 1 THEN 'MN'
							WHEN MC_CreditReport.[BureauID] = 2 THEN 'TU'
							WHEN MC_CreditReport.[BureauID] = 3 THEN 'EQ'
							WHEN MC_CreditReport.[BureauID] = 4 THEN 'EX'
						  END [BureauId], 

						MC_CreditReport.Score,
						CASE										-- IsScored -- not sure with this
							WHEN   MC_CreditReport.Score = 0 THEN 0
							ELSE 1
						END  IsScored,
						0  IsHit, -- IsHit -- not sure with this - not clearly defined in the documentation
						'This is the actual credit report.',
						MC_CreditReport.MicroBiltGUID

				FROM
				[Platinum_Protection_InterimCRM].[dbo].[MC_CreditReport]
				INNER JOIN
				[Platinum_Protection_InterimCRM].[dbo].[MS_Account]
				ON
				[MC_CreditReport].[LeadID]= [MS_Account].[Customer1ID]

				--INNER JOIN
				--[WISE_CRM].[dbo].[QL_CreditReportBureaus]
				--ON
				--[MC_CreditReport].BureauName = [QL_CreditReportBureaus].BureauName   -- commented since not all BureauName are equal
				WHERE 
				[MS_Account].[AccountID] =  @InterimAccountID -- not sure if this is only the filter

				--SET @CreditReportVendorMicrobiltID = SCOPE_IDENTITY()  --- not sure if it is needed
		

			-- 6.3 	[QL_CreditReportVendorAbara]
				INSERT INTO [WISE_CRM].[dbo].[QL_CreditReportVendorAbara]
				(
					 [CreditReportId]
					,[BureauId]
					,[ReportID]
					,[ReportGuid]
					,[Result]
					,[Score]
					,[IsScored]
					,[IsHit]
					,[ReportHtml]
					,[ReportXML]
					,[ErrorMessage]
					,[HitStatus]
					,[DecisionCode]
					,[DecisionText]
	
				)
				SELECT 
					@CreditReportID,
				--  QL_CreditReportBureaus.BureauID,
					CASE
							WHEN [MC_CreditReportAbara].[BureauID] = 1 THEN 'MN'
							WHEN [MC_CreditReportAbara].[BureauID] = 2 THEN 'TU'
							WHEN [MC_CreditReportAbara].[BureauID] = 3 THEN 'EQ'
							WHEN [MC_CreditReportAbara].[BureauID] = 4 THEN 'EX'
						  END [BureauId], 
					[MC_CreditReportAbara].[ReportID],
					[MC_CreditReportAbara].[ReportGuid],
					[MC_CreditReportAbara].[Result],
					[MC_CreditReportAbara].[Score],
					[MC_CreditReportAbara].IsScored,
					[MC_CreditReportAbara].IsHit, 
					[MC_CreditReportAbara].[ReportHtml],
					[MC_CreditReportAbara].[ReportXML],
					[MC_CreditReportAbara].[ErrorMessage],
					[MC_CreditReportAbara].[HitStatus],
					[MC_CreditReportAbara].[DecisionCode],
					[MC_CreditReportAbara].[DecisionText]
					--[MS_Account].*, --Temp
					--[MC_CreditReport].*, --Temp
					--[MC_CreditReportAbara].* -- Temp
				FROM
				[Platinum_Protection_InterimCRM].[dbo].[MC_CreditReport]
				INNER JOIN
				[Platinum_Protection_InterimCRM].[dbo].[MS_Account]
				ON
				[MC_CreditReport].[LeadID]= [MS_Account].[Customer1ID]
				INNER JOIN
				[Platinum_Protection_InterimCRM].[dbo].[MC_CreditReportAbara]
				ON
				[MC_CreditReportAbara].[CreditReportId] = [MC_CreditReport].CreditReportID

				--INNER JOIN
				--[WISE_CRM].[dbo].[QL_CreditReportBureaus]
				--ON
				--[MC_CreditReport].BureauName = [QL_CreditReportBureaus].BureauName   -- commented since not all BureauName are equal
				WHERE 
				[MS_Account].[AccountID] = @InterimAccountID  --- not sure if it is needed


				--SET @CreditReportVendorAbaraID = SCOPE_IDENTITY() -- not sure if it is needed

				/** Update info of MS_AccountSwungInfo **/
				UPDATE [dbo].MS_AccountSwungInfo SET [QlCreditReport] = GETUTCDATE() WHERE InterimAccountID = @InterimAccountID;


/*step 7. Create Customer Records
			*/
			
			-- PRI Customer
			IF @Customer1IDOld IS NOT NULL  --- check if @Customer1IDOld is available, if available generate WCM.AE_Customers for customer1
			BEGIN

				INSERT INTO [WISE_CRM].[dbo].[AE_Customers]
				(
					[CustomerTypeId],
					[CustomerMasterFileId],
					[DealerId],
					[AddressId],
					[LeadId],
					[LocalizationId],
					[Prefix],
					[FirstName],
					[MiddleName],
					[LastName],
					[Postfix],
					[Gender],
					[PhoneHome],
					[PhoneWork], 
					[PhoneMobile],
					[DOB],
					[SSN],
					[Email],
					[Username],
					[Password]
				)
				SELECT
					'PRI',
					@CustomerMasterFileID,
					5000,
					@AddressID2,
					@Lead1ID,
					(SELECT [LocalizationId] FROM [WISE_CRM].[dbo].[QL_Leads] WHERE LeadID = @Lead1ID) [LocalizationId],
					[Salutation],
					[FirstName],
		  			[MiddleName],
					[LastName],
					[Suffix],
					'N',
					[PhoneHome],
					[PhoneWork]+[PhoneWorkExt], 
					[PhoneCell],
					[DOB],
					[SSN],
					[Email],
					NULL,
					NULL

			  FROM [Platinum_Protection_InterimCRM].[dbo].[MC_Lead]
			  WHERE [MC_Lead].LeadID =  @Customer1IDOld

			  SET @Customer1IDNew = SCOPE_IDENTITY()
		 END
		 

		 
		  --SEC Customer
			IF @Customer2IDOld IS NOT NULL  --- check if @Customer2IDOld is available, if available generate WCM.AE_Customers for customer2
			BEGIN

				INSERT INTO [WISE_CRM].[dbo].[AE_Customers]
				(
					[CustomerTypeId],
					[CustomerMasterFileId],
					[DealerId],
					[AddressId],
					[LeadId],
					[LocalizationId],
					[Prefix],
					[FirstName],
					[MiddleName],
					[LastName],
					[Postfix],
					[Gender],
					[PhoneHome],
					[PhoneWork], 
					[PhoneMobile],
					[DOB],
					[SSN],
					[Email],
					[Username],
					[Password]
				)
				SELECT
					'SEC',
					@CustomerMasterFileID,
					5000,
					@AddressID2,
					@Lead1ID,
					(SELECT [LocalizationId] FROM [WISE_CRM].[dbo].[QL_Leads] WHERE LeadID = @Lead1ID) [LocalizationId],
					[Salutation],
					[FirstName],
		  			[MiddleName],
					[LastName],
					[Suffix],
					'N',
					[PhoneHome],
					[PhoneWork]+[PhoneWorkExt], 
					[PhoneCell],
					[DOB],
					[SSN],
					[Email],
					NULL,
					NULL

			  FROM [Platinum_Protection_InterimCRM].[dbo].[MC_Lead]
			  WHERE [MC_Lead].LeadID =  @Customer2IDOld
			  SET @Customer2IDNew = SCOPE_IDENTITY()
		 END
	
		/** Update info of MS_AccountSwungInfo **/
		UPDATE [dbo].MS_AccountSwungInfo SET [AeCustomer] = GETUTCDATE() WHERE InterimAccountID = @InterimAccountID;		
			

/*step 8.Create Customer Account and Account Customer Tables
			*/
			
		--Customer1ID
		IF @Customer1IDNew IS NOT NULL
		BEGIN
			INSERT INTO [WISE_CRM].[dbo].AE_CustomerAccounts
			(
			   [LeadId]
			  ,[AccountId]
			  ,[CustomerId]
			  ,[CustomerTypeId]
			)
			SELECT 
				@Lead1ID,
				@AccountID,
				@Customer1IDNew,
				'PRI'
		END


		--Customer2ID
		IF @Customer2IDNew IS NOT NULL
		BEGIN
			INSERT INTO [WISE_CRM].[dbo].AE_CustomerAccounts
			(
			   [LeadId]
			  ,[AccountId]
			  ,[CustomerId]
			  ,[CustomerTypeId]
			)
			SELECT 
				@Lead2ID,
				@AccountID,
				@Customer2IDNew,
				'SEC'
		END

		/** Create the MS_AccountCustomers records. */
		INSERT INTO [dbo].[MS_AccountCustomers] (
			[AccountCustomerTypeId]
			, [LeadId]
			, [CustomerId]
			, [AccountId]
		) VALUES (
			'MONI' -- AccountCustomerTypeId - varchar(5)
			, @Lead1ID -- LeadId - bigint
			, @Customer1IDNew -- CustomerId - bigint
			, @AccountID -- AccountId - bigint
		);

		/** Update info of MS_AccountSwungInfo **/
		UPDATE [dbo].MS_AccountSwungInfo SET [AeCustomerAccount] = GETUTCDATE() WHERE InterimAccountID = @InterimAccountID;
		
		-- account status is Onboard Full  - run step 9 and step 10
		IF @AccountStatus = 'Onboard Full'
		BEGIN
				--PRINT 'step 9 : lacking'
				/*step 9.Swinging of Emergency Contacts happens here - but no documentation for this
							*/	
							
				-- swing emergency contacts	
				INSERT INTO [dbo].[MS_EmergencyContacts]
					   ([CustomerId]
					   ,[AccountId]
					   ,[RelationshipId]
					   ,[OrderNumber]
					   ,[Allergies]
					   ,[MedicalConditions]
					   ,[HasKey]
					   ,[DOB]
					   ,[Prefix]
					   ,[FirstName]
					   ,[MiddleName]
					   ,[LastName]
					   ,[Postfix]
					   ,[Email]
					   ,[Password]
					   ,[Phone1]
					   ,[Phone1TypeId]
					   ,[Phone2]
					   ,[Phone2TypeId]
					   ,[Phone3]
					   ,[Phone3TypeId]
					   ,[Comment1]
					   ,[IsActive]
					   ,[IsDeleted]
					   ,[ModifiedOn]
					   ,[ModifiedBy]
					   ,[CreatedOn]
					   ,[CreatedBy]
					)
				SELECT 
					   @Customer1IDNew  -- not sure if we will be using only Customer1ID
					  ,@AccountID
--					  ,ISNULL((SELECT RelationshipID FROM [WISE_CRM].[dbo].[MS_EmergencyContactRelationships] WHERE MsRelationshipId = MSECR.RelationshipName),115) AS [RelationshipId]  -- 115 - other
					  ,dbo.fxGetEmergencyContactTypeIDByPlatinumEMCTypeID(MSEC.EmergencyContactRelationshipId)
					  ,[ContactOrder]
					  , NULL AS [Allergies]
					  , NULL AS [MedicalConditions]
					  ,[HasKeys]
					  , NULL AS [DOB]
					  ,[Prefix]
					  ,[FirstName]
					  ,[MiddleInit]
					  ,[LastName]
					  ,[Suffix]
					  ,[Email]
					  ,[Passcode]
					  ,[PhoneNumber1]
--					  ,(SELECT MSECPT.PhoneTypeID FROM [WISE_CRM].[dbo].[MS_EmergencyContactPhoneTypes]  MSECPT  INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContactPhoneType] MSECPT2 ON MSECPT2.AvantGuardCode = MSECPT.MsPhoneTypeId WHERE MSECPT2.[EmergencyContactPhoneTypeId] = MSEC.PhoneTypeId1)
					  ,dbo.fxGetEmergencyContactPhoneTypeIDByPlatinumEmergencyContactPhoneTypeID(MSEC.PhoneTypeId1)
					  ,[PhoneNumber2]
--					  ,(SELECT MSECPT.PhoneTypeID FROM [WISE_CRM].[dbo].[MS_EmergencyContactPhoneTypes]  MSECPT  INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContactPhoneType] MSECPT2 ON MSECPT2.AvantGuardCode = MSECPT.MsPhoneTypeId WHERE MSECPT2.[EmergencyContactPhoneTypeId] = MSEC.PhoneTypeId2)
					  ,dbo.fxGetEmergencyContactPhoneTypeIDByPlatinumEmergencyContactPhoneTypeID(MSEC.PhoneTypeId2)
					  ,[PhoneNumber3]
--					  ,(SELECT MSECPT.PhoneTypeID FROM [WISE_CRM].[dbo].[MS_EmergencyContactPhoneTypes]  MSECPT  INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContactPhoneType] MSECPT2 ON MSECPT2.AvantGuardCode = MSECPT.MsPhoneTypeId WHERE MSECPT2.[EmergencyContactPhoneTypeId] = MSEC.PhoneTypeId3)
					  ,dbo.fxGetEmergencyContactPhoneTypeIDByPlatinumEmergencyContactPhoneTypeID(MSEC.PhoneTypeId3)
					  , NULL AS [Comment1]
					  , 1 AS [IsActive]
					  , 0 AS [IsDeleted]
					  ,[ModifiedByDate]
					  ,ISNULL([ModifiedByID], 'SWUNG')
					  ,[CreatedByDate]
					  ,ISNULL([CreatedByID],'SWUNG')
				  FROM
						[Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContact] AS MSEC WITH (NOLOCK)
						INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContactRelationships] AS MSECR WITH (NOLOCK)
						ON
							(MSEC.[EmergencyContactRelationshipId] = MSECR.[EmergencyContactRelationshipId])
				  WHERE	
					(MSEC.[AccountID] = @InterimAccountID)
				  
				/** Update info of MS_AccountSwungInfo **/
				UPDATE [dbo].MS_AccountSwungInfo SET [MsEmergencyContact] = GETUTCDATE() WHERE InterimAccountID = @InterimAccountID;

						
				/*
					mapping helper for emergency contacts

					SELECT *  FROM [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContactRelationships]
					SELECT * FROM [dbo].[MS_EmergencyContactRelationships]

					--select  Top(100)  * from [WISE_CRM].[dbo].[MS_EmergencyContacts]
					--select Top(100) * from [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContact]

					SELECT * FROM [dbo].[MS_EmergencyContactPhoneTypes]
					SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContactPhoneType]

				*/			
								
		--		PRINT 'step 10 : lacking'
				/*step 10.Swinging of Equipment with all Zones happens here - but no documentation for this*/
				
				-- check if allow swing equipment
				IF @SwingEquipment = 1
				BEGIN
					-- @AccountID here refers to the newly insert MC_ACCOUNT in WISE_CRM
				  --PRINT 'Before Swing Equipment Status'
				  --store procedure result in a temporary table variable to avoid display select result set
				  INSERT INTO @SwingEquipmentStatusTable
				  EXEC [custAE_CustomerSWINGEquipment] @InterimAccountID, @CustomerMasterFileID, @AccountID
				  --SELECT * FROM @SwingEquipmentStatusTable

				END
		END

		/** If everyting is successful, insert swung info on MS_AccountSwungInfo **/
		SET @SwingStatus = '1';
		UPDATE [dbo].[MS_AccountSwungInfo] SET SwingStatus = @SwingStatus WHERE (InterimAccountID = @InterimAccountID);
		
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
							
		SET @SwingStatus = ERROR_MESSAGE();		
		
		-- SELECT @SwingStatus AS SwingStatus, @AccountID AS MsAccountID, @InterimAccountID AS InterimAccountID;
		UPDATE [dbo].[MS_AccountSwungInfo] SET SwingStatus = @SwingStatus WHERE (InterimAccountID = @InterimAccountID);
		SELECT * FROM [dbo].[vwAE_CustomerSWINGInterim] WHERE (InterimAccountID = @InterimAccountID);

		EXEC dbo.wiseSP_ExceptionsThrown;		
		RETURN;
	END CATCH
	
	--SELECT @SwingStatus AS SwingStatus, @AccountID AS MsAccountID, @InterimAccountID AS InterimAccountID;
	SELECT * FROM [dbo].[vwAE_CustomerSWINGInterim] WHERE (InterimAccountID = @InterimAccountID);

	--PRINT 'Swing Status:' + @SwingStatus;

END

GRANT EXEC ON dbo.custAE_CustomerSWINGFromInterim TO PUBLIC
GO

-- EXEC [dbo].[custAE_CustomerSWINGFromInterim] 100195, 1
/*

SUPPLEMENTAL QUERIES AND ISSUES

1. QUERIES FOR CHECKING RECORDS

SELECT MAX([CustomerMasterFileID]) FROM  [dbo].[AE_CustomerMasterFiles]
SELECT * FROM 	[Platinum_Protection_InterimCRM].dbo.[MS_Account] MS_Account


2. MAPPING FOR ADDRESS
	
	2.1 State
		ISSUE - there are states who were available in PPI but not in WISE_CRM
	-- Try to retrieve list of States from PPI - to verify if it has the same listings with WISE_CRM state listings
		SELECT TOP 100 MC_Address.*,  MCPS.*
		FROM 
		[Platinum_Protection_InterimCRM].[dbo].[MC_Address] MC_Address
		INNER JOIN 
		[Platinum_Protection_InterimCRM].[dbo].[MC_PoliticalState] MCPS
		ON
		MC_Address.StateID = MCPS.StateID


		SELECT *
		FROM 
		[Platinum_Protection_InterimCRM].[dbo].[MC_PoliticalState] MCPS ORDER BY MCPS.StateAB

		SELECT *
		FROM 
		[WISE_CRM].[dbo].[MC_PoliticalStates] MCPS ORDER BY MCPS.StateAB

		SELECT TOP 100 *
		FROM 
		[WISE_CRM].[dbo].[QL_Address]

	2.2 AddressStatusID
	    ISSUE - some of the values under AddressStatus column does not match to AddressValidationStateName

		SELECT *
		FROM 
		[Platinum_Protection_InterimCRM].[dbo].[MC_AddressStatus]

		SELECT *
		FROM 
		[WISE_CRM].[dbo].[MC_AddressValidationStates] WHERE [AddressValidationStateName]='Validated'

	2.3 CountyID
		ISSUE - Not all values are matched
		SELECT *
		FROM 
		[Platinum_Protection_InterimCRM].[dbo].[MC_PoliticalCountry]

		SELECT *
		FROM 
		[WISE_CRM].[dbo].[MC_PoliticalCountrys] 
		
	2.4 TimeZoneId
		ISSUE - Not all values are matched
		SELECT
		* 
		FROM
		[Platinum_Protection_InterimCRM].[dbo].[MC_PoliticalTimeZones]

		SELECT
		* 
		FROM
		[WISE_CRM].[dbo].[MC_PoliticalTimeZones]

	2.5 AddressType

		SELECT
		*
		FROM
		[WISE_CRM].[dbo].[MC_AddressTypes]

		SELECT TOP 10
		[AddressType]
		FROM
		[Platinum_Protection_InterimCRM].[dbo].[MC_Address]


	2.5 NOT NULLABLE FIELDS - not defined in the swing documentation
		[IsActive]
		[CreatedBy]


	2.6 ValidationVendorId 
		SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].[MC_AddressValidationVendor]
		SELECT * FROM [WISE_CRM].[dbo].[MC_AddressValidationVendors]
	



3. TO BE REVIEWED

	3.1 [dbo].[MC_AddressCoords]




	
		SELECT 
		ValidationVendorID
		FROM
		[Platinum_Protection_InterimCRM].[dbo].[MC_Address]


4. CREDIT REPORT MAPPING

	4.1 [BureauID]
		ISSUE - not all values are matched
			SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].[MC_CreditReportBureau]
			SELECT * FROM [WISE_CRM].[dbo].[QL_CreditReportBureaus]

	4.2 [CreditReportVendorId]
			SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].[MC_CreditReportVendors]
			SELECT * FROM [WISE_CRM].[dbo].[QL_CreditReportVendors]

	4.3 [CreditReportVendorAbaraId]
		
		
		Steps:

			1. create the credit report
				- CreditReportVendorAbaraId -- null
				- CreditReportVendorMicrobiltId  -- null

			2. WCM.QL_CreditReportVendorMicrobilt
			3. WCM.QL_CreditReportVendorAbara


		SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].[MC_CreditReport]
		
		
		SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].[MC_CreditReportAbara]
		

		SELECT * FROM [WISE_CRM].[dbo].[QL_CreditReportVendorAbara]

		SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].[MC_CreditReportAbara]
				WHERE [LeadId] = 232104

		SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].[MC_CreditReport]
		WHERE [CreditReportID] = 767449

		SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].[MC_CreditReport]
		WHERE [LeadId] = 232104
				
		SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].[MC_Lead]
		WHERE [LeadID] = 232104

		SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].[MS_Account]
		WHERE [Customer1ID] = 232104




5. TESTING QUERIES

	SELECT * FROM [WISE_CRM].dbo.[AE_CustomerMasterFiles] WHERE 	[CustomerMasterFileID] = 3050497


	SELECT * FROM [WISE_CRM].[dbo].MS_AccountSystemTypes
	SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].MS_AccountMonitoringType

	
	SELECT * FROM [WISE_CRM].[dbo].[MS_AccountCellularTypes]
	SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].MS_AccountCellularType

6. LeadDispositionID
	ISSUE - the swing documentation suggest to use "10" as LeadDispositionID but the 
	SELECT * FROM [WISE_CRM].[dbo].QL_LeadDispositions does not contain "10"


*/

