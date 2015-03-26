USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custFE_PacketsReadOpen')
	BEGIN
		PRINT 'Dropping Procedure custFE_PacketsReadOpen'
		DROP  Procedure  dbo.custFE_PacketsReadOpen
	END
GO

PRINT 'Creating Procedure custFE_PacketsReadOpen'
GO
/******************************************************************************
**		File: custFE_PacketsReadOpen.sql
**		Name: custFE_PacketsReadOpen
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
**		Date: 02/25/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/25/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custFE_PacketsReadOpen
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT
			*
		FROM
			[dbo].[vwFE_Packets] AS FEP WITH (NOLOCK)

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custFE_PacketsReadOpen TO PUBLIC
GO

/** EXEC dbo.custFE_PacketsReadOpen */