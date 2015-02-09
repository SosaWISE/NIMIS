USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGMS_Account')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGMS_Account'
		DROP  Procedure  dbo.custAE_CustomerSWINGMS_Account
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGMS_Account'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGMS_Account.sql
**		Name: custAE_CustomerSWINGMS_Account
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
CREATE Procedure dbo.custAE_CustomerSWINGMS_Account
(
	@InterimAccountID BIGINT
	, @AccountID BIGINT -- new account id
	, @PremiseAddressId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/*step 4. Create the WCM.MS_Accounts
		*/

		INSERT INTO [WISE_CRM].[dbo].[MS_Accounts]
		(
		       [AccountID]
			  ,[IndustryAccountId]
			  ,[SystemTypeId]
			  ,[CellularTypeId]
			  ,[PanelTypeId]
			  ,[DslSeizureId]
			  ,[PanelItemId]
			  ,[CellPackageItemId]
			  ,[ContractId]
			  ,[AccountPassword]
			  ,[SimProductBarcodeId]
			  ,[DispatchMessage]
		)

		SELECT
			@AccountID,
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
			NULL,  -- PanelTypeId  ??? Not sure this is a problem. A resolution coming soon. - statement from swing documentation
			CASE
				WHEN [DSLSeizure] = 'Seizure' THEN 3 
				WHEN [DSLSeizure] = 'DSL' THEN 2
				WHEN [DSLSeizure] IS NULL THEN 1

			END [DSLSeizure],
			NULL,  -- PanelItemId
			NULL,  -- CellPackageItemId
			NULL,  -- ContractId
			[AbortCode], -- AccountPassword
			NULL, -- SimProductBarcodeId
			NULL -- DispatchMessage
			--[MS_Account].*
		FROM
			[Platinum_Protection_InterimCRM].[dbo].[MS_Account]
		WHERE
			[MS_Account].AccountID =  @InterimAccountID


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

GRANT EXEC ON dbo.custAE_CustomerSWINGMS_Account TO PUBLIC
GO

/** EXEC dbo.SPROC_NAME */