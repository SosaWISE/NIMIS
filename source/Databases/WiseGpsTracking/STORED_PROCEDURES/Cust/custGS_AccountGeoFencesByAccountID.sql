USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_AccountGeoFencesByAccountID')
	BEGIN
		PRINT 'Dropping Procedure custGS_AccountGeoFencesByAccountID'
		DROP  Procedure  dbo.custGS_AccountGeoFencesByAccountID
	END
GO

PRINT 'Creating Procedure custGS_AccountGeoFencesByAccountID'
GO
/******************************************************************************
**		File: custGS_AccountGeoFencesByAccountID.sql
**		Name: custGS_AccountGeoFencesByAccountID
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
**		Date: 00/00/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	00/00/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custGS_AccountGeoFencesByAccountID
(
	@AccountID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	SELECT
		*
	FROM
		[dbo].[vwGS_AccountGeoFences] AS AGF
	WHERE
		(AGF.AccountID = @AccountID)
		AND (AGF.IsDeleted = 0)
	
END
GO

GRANT EXEC ON dbo.custGS_AccountGeoFencesByAccountID TO PUBLIC
GO