USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCust_GetFedFilingStatus')
	BEGIN
		PRINT 'Dropping Procedure ppCust_GetFedFilingStatus'
		DROP  Procedure  dbo.ppCust_GetFedFilingStatus
	END
GO

PRINT 'Creating Procedure ppCust_GetFedFilingStatus'
GO
/******************************************************************************
**		File: ppCust_GetFedFilingStatus.sql
**		Name: ppCust_GetFedFilingStatus
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
**		Date: 1/21/2009
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	1/21/2009	Denzil Frost	Created
*******************************************************************************/
CREATE Procedure dbo.ppCust_GetFedFilingStatus
--(
	
--)
AS
BEGIN

SELECT	TXFLGSTS AS ID
 		, STSDESCR AS [Description]

FROM DYNAMICS.dbo.UPR41301
WHERE Taxcode = 'FED'
		
END
GO

GRANT EXEC ON dbo.ppCust_GetFedFilingStatus TO PUBLIC
GO