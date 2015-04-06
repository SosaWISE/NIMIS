USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custFE_BundlesReadOpen')
	BEGIN
		PRINT 'Dropping Procedure custFE_BundlesReadOpen'
		DROP  Procedure  dbo.custFE_BundlesReadOpen
	END
GO

PRINT 'Creating Procedure custFE_BundlesReadOpen'
GO
/******************************************************************************
**		File: custFE_BundlesReadOpen.sql
**		Name: custFE_BundlesReadOpen
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
**		Date: 02/26/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/26/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custFE_BundlesReadOpen
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT
			*
		FROM
			[dbo].[vwFE_Bundles] AS FEB WITH (NOLOCK)

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custFE_BundlesReadOpen TO PUBLIC
GO

/** EXEC dbo.custFE_BundlesReadOpen */