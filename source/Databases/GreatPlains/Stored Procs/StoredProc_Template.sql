USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SPROC_NAME')
	BEGIN
		PRINT 'Dropping Procedure SPROC_NAME'
		DROP  Procedure  dbo.SPROC_NAME
	END
GO

PRINT 'Creating Procedure SPROC_NAME'
GO
/******************************************************************************
**		File: SPROC_NAME.sql
**		Name: SPROC_NAME
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
**		Auth: 
**		Date: 
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**			
*******************************************************************************/
CREATE Procedure dbo.SPROC_NAME
(
	@PARAM INT
	, @PARAM2 NVARCHAR(25) OUTPUT
)
AS
BEGIN

	-- Enter CODE HERE>
	
END
GO

GRANT EXEC ON dbo.SPROC_NAME TO PUBLIC
GO