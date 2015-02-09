USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGGetSystemDetail')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGGetSystemDetail'
		DROP  Procedure  dbo.custAE_CustomerSWINGGetSystemDetail
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGGetSystemDetail'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGGetSystemDetail.sql
**		Name: custAE_CustomerSWINGGetSystemDetail
**		Desc: retrived account system detail to be displayed in the	SWING Screen
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
**	05/09/2014	Reagan/Junryll	Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerSWINGGetSystemDetail
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
		/*SELECT 
			SGC.ServiceType
			,MACT.[AccountCellularType]	 'CellularType'
			,'NOT AVAILABLE'	'PassPhrase'-- to be change later
			,MA.[PanelTypeID] 'PanelType'
			,MA.[DSLSeizure] 'DslSeizure'
		FROM 
			[Platinum_Protection_InterimCRM].[dbo].[MS_Account] MA
			INNER JOIN
			[Platinum_Protection_InterimCRM].[dbo].[MS_AccountCellularType] MACT
			ON MA.[AccountCellularTypeId] = MACT.[AccountCellularTypeId]
			INNER JOIN
			[Platinum_Protection_InterimCRM].[dbo].[SAE_GPCustContracts] SGC  -- not sure with this relationship
			ON SGC.AccountID = MA.AccountID
		WHERE
			MA.AccountID = @InterimAccountID*/

				SELECT 
					--   MAS.AccountID
					 --  , MAS.MonitoringTypeID
					   MAM.MonitoringType AS [ServiceType]
					--   , MAS.AccountCellularTypeId
					   , MAC.AccountCellularType AS [CellularType]
					   , MAS.AbortCode AS [PassPhrase]
					   , MAS.PanelTypeID AS [PanelType]
					   , MAS.DSLSeizure AS [DslSeizure]
				FROM
					   [Platinum_Protection_InterimCRM].[dbo].MS_Account AS MAS WITH (NOLOCK)
					--   INNER JOIN [Platinum_Protection_InterimCRM].[dbo].MS_AccountStatus AS MSS WITH (NOLOCK)
					  -- ON
					--		  (MSS.AccountID = MAS.AccountID)
					   INNER JOIN [Platinum_Protection_InterimCRM].dbo.MS_AccountMonitoringType AS MAM WITH (NOLOCK)
					   ON
							  (MAM.MonitoringTypeID = MAS.MonitoringTypeID)
					   INNER JOIN [Platinum_Protection_InterimCRM].[dbo].MS_AccountCellularType AS MAC WITH (NOLOCK)
					   ON
							  (MAC.AccountCellularTypeId = MAS.AccountCellularTypeId)
				WHERE
					   --(MSS.InstallDate IS NOT NULL)
					   --AND (MAS.DSLSeizure IS NOT NULL)
						
						--AND 
						MAS.AccountID = @InterimAccountID


		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerSWINGGetSystemDetail TO PUBLIC
GO

/** EXEC dbo.custAE_CustomerSWINGGetSystemDetail  100392*/