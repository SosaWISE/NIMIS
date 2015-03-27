USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custFE_BundleItemsViewByBundleID')
	BEGIN
		PRINT 'Dropping Procedure custFE_BundleItemsViewByBundleID'
		DROP  Procedure  dbo.custFE_BundleItemsViewByBundleID
	END
GO

PRINT 'Creating Procedure custFE_BundleItemsViewByBundleID'
GO
/******************************************************************************
**		File: custFE_BundleItemsViewByBundleID.sql
**		Name: custFE_BundleItemsViewByBundleID
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
**		Date: 03/26/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/26/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custFE_BundleItemsViewByBundleID
(
	@BundleID INT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT
			*
		FROM
			[dbo].[vwFE_BundleItems] AS FEPI WITH (NOLOCK)
		WHERE
			(FEPI.BundleId = @BundleID);

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custFE_BundleItemsViewByBundleID TO PUBLIC
GO

/** EXEC dbo.custFE_BundleItemsViewByBundleID 1*/