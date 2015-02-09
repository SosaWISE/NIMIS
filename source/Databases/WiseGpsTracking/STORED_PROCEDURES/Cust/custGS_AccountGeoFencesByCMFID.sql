USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_AccountGeoFencesByCMFID')
	BEGIN
		PRINT 'Dropping Procedure custGS_AccountGeoFencesByCMFID'
		DROP  Procedure  dbo.custGS_AccountGeoFencesByCMFID
	END
GO

PRINT 'Creating Procedure custGS_AccountGeoFencesByCMFID'
GO
/******************************************************************************
**		File: custGS_AccountGeoFencesByCMFID.sql
**		Name: custGS_AccountGeoFencesByCMFID
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
**		Date: 06/17/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	06/17/2013	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custGS_AccountGeoFencesByCMFID
(
	@CMFID BIGINT,
	@CustomerID BIGINT = NULL
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
		(AGF.CustomerMasterFileId = @CMFID)
		AND ((@CustomerID IS NULL) OR (AGF.CustomerId = @CustomerID))
		AND (AGF.IsDeleted = 0)
	
END
GO

GRANT EXEC ON dbo.custGS_AccountGeoFencesByCMFID TO PUBLIC
GO
/** 
EXEC dbo.custGS_AccountGeoFencesByCMFID 3000035;
*/