USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCust_GetEICFilingStatus')
	BEGIN
		PRINT 'Dropping Procedure ppCust_GetEICFilingStatus'
		DROP  Procedure  dbo.ppCust_GetEICFilingStatus
	END
GO

PRINT 'Creating Procedure ppCust_GetEICFilingStatus'
GO
/******************************************************************************
**		File: ppCust_GetEICFilingStatus.sql
**		Name: ppCust_GetEICFilingStatus
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
CREATE Procedure dbo.ppCust_GetEICFilingStatus
--(
	
--)
AS
BEGIN

SELECT	TXFLGSTS AS ID
 		, STSDESCR AS [Description]

FROM DYNAMICS.dbo.UPR41301
WHERE Taxcode = 'EIC'
		
END
GO

GRANT EXEC ON dbo.ppCust_GetEICFilingStatus TO PUBLIC
GO
