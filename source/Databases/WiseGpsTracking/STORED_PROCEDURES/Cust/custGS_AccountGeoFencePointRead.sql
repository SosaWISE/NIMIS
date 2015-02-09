USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_AccountGeoFencePointRead')
	BEGIN
		PRINT 'Dropping Procedure custGS_AccountGeoFencePointRead'
		DROP  Procedure  dbo.custGS_AccountGeoFencePointRead
	END
GO

PRINT 'Creating Procedure custGS_AccountGeoFencePointRead'
GO
/******************************************************************************
**		File: custGS_AccountGeoFencePointRead.sql
**		Name: custGS_AccountGeoFencePointRead
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
CREATE Procedure dbo.custGS_AccountGeoFencePointRead
(
	@GeoFenceID BIGINT
	, @AccountId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	BEGIN TRY
		/** Check that we have a GeoFenceID. */
		SELECT * FROM [dbo].vwGS_AccountGeoFencePoints WHERE (GeoFenceID = @GeoFenceID) AND (AccountId = @AccountId) AND (IsDeleted = 0);
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown
	END CATCH
END
GO

GRANT EXEC ON dbo.custGS_AccountGeoFencePointRead TO PUBLIC
GO