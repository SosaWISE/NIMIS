USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_ItemActivationFeesGet')
	BEGIN
		PRINT 'Dropping Procedure custAE_ItemActivationFeesGet'
		DROP  Procedure  dbo.custAE_ItemActivationFeesGet
	END
GO

PRINT 'Creating Procedure custAE_ItemActivationFeesGet'
GO
/******************************************************************************
**		File: custAE_ItemActivationFeesGet.sql
**		Name: custAE_ItemActivationFeesGet
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
**		Date: 12/17/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	12/17/2013	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_ItemActivationFeesGet
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Execute Statement */
	SELECT
		ITM.*
	FROM
		[dbo].AE_Items AS ITM WITH (NOLOCK)
	WHERE
		(ITM.ItemTypeId = 'SETUP_FEE')
		AND (ITM.IsActive = 1 AND ITM.IsDeleted = 0);

END
GO

GRANT EXEC ON dbo.custAE_ItemActivationFeesGet TO PUBLIC
GO