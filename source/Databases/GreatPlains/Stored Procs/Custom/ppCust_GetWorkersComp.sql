USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCust_GetWorkersComp')
	BEGIN
		PRINT 'Dropping Procedure ppCust_GetWorkersComp'
		DROP  Procedure  dbo.ppCust_GetWorkersComp
	END
GO

PRINT 'Creating Procedure ppCust_GetWorkersComp'
GO
/******************************************************************************
**		File: ppCust_GetWorkersComp.sql
**		Name: ppCust_GetWorkersComp
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
**		Auth: Denzil Frost
**		Date: 1/28/2009
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	1/28/2009	Denzil Frost	Created
*******************************************************************************/
CREATE Procedure dbo.ppCust_GetWorkersComp
--(
	
--)
AS
BEGIN

SELECT WRKRCOMP, DSCRIPTN 
      FROM UPR40700
      ORDER BY DSCRIPTN
		
END
GO

GRANT EXEC ON dbo.ppCust_GetWorkersComp TO PUBLIC
GO
