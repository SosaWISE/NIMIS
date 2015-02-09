USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSAE_BillingHistoryByCMFID')
	BEGIN
		PRINT 'Dropping Procedure custSAE_BillingHistoryByCMFID'
		DROP  Procedure  dbo.custSAE_BillingHistoryByCMFID
	END
GO

PRINT 'Creating Procedure custSAE_BillingHistoryByCMFID'
GO
/******************************************************************************
**		File: custSAE_BillingHistoryByCMFID.sql
**		Name: custSAE_BillingHistoryByCMFID
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
**		Date: 06/30/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	08/06/2014	Bob McFadden	Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custSAE_BillingHistoryByCMFID (@CMFID BIGINT)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		SELECT 
			CustomerMasterFileId,
			BillingType,
			BillingDate,
			BillingNumber,
			BillingAmount
		FROM vwSAE_BillingHistory 
		WHERE CustomerMasterFileID = @CMFID
		ORDER BY 
			BillingDate DESC, 
			BillingType ASC
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custSAE_BillingHistoryByCMFID TO PUBLIC
GO

/** EXEC dbo.custSAE_BillingHistoryByCMFID 3051158*/