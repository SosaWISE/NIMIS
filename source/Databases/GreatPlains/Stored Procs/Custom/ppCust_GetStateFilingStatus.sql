USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCust_GetStateFilingStatus')
	BEGIN
		PRINT 'Dropping Procedure ppCust_GetStateFilingStatus'
		DROP  Procedure  dbo.ppCust_GetStateFilingStatus
	END
GO

PRINT 'Creating Procedure ppCust_GetStateFilingStatus'
GO
/******************************************************************************
**		File: ppCust_GetStateFilingStatus.sql
**		Name: ppCust_GetStateFilingStatus
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
CREATE Procedure dbo.ppCust_GetStateFilingStatus
(
	@StateAB NVARCHAR(5) = NULL
)
AS
BEGIN

SELECT	TXFLGSTS AS ID
 		, STSDESCR AS [Description]

FROM DYNAMICS.dbo.UPR41301
WHERE Taxcode = @StateAB
		
END
GO

GRANT EXEC ON dbo.ppCust_GetStateFilingStatus TO PUBLIC
GO