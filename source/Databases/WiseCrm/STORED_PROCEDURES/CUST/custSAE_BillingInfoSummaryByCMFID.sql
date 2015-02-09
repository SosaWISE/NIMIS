USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSAE_BillingInfoSummaryByCMFID')
	BEGIN
		PRINT 'Dropping Procedure custSAE_BillingInfoSummaryByCMFID'
		DROP  Procedure  dbo.custSAE_BillingInfoSummaryByCMFID
	END
GO

PRINT 'Creating Procedure custSAE_BillingInfoSummaryByCMFID'
GO
/******************************************************************************
**		File: custSAE_BillingInfoSummaryByCMFID.sql
**		Name: custSAE_BillingInfoSummaryByCMFID
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
**		Date: 01/03/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/03/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custSAE_BillingInfoSummaryByCMFID
(
	@CMFID BIGINT = NULL
	, @AccountID BIGINT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Execute SQL Statment*/
	SELECT
		*
	FROM
		[dbo].[vwSAE_BillingInfoSummary] AS BIS WITH (NOLOCK)
	WHERE
		((@CMFID IS NULL) OR (BIS.CustomerMasterFileId = @CMFID))
		AND ((@AccountID IS NULL) OR (BIS.AccountID = @AccountID));
	
END
GO

GRANT EXEC ON dbo.custSAE_BillingInfoSummaryByCMFID TO PUBLIC
GO

/** 
EXEC dbo.custSAE_BillingInfoSummaryByCMFID 3051185

SELECT * FROM [dbo].[vwSAE_BillingInfoSummary] AS BIS WITH (NOLOCK)
*/