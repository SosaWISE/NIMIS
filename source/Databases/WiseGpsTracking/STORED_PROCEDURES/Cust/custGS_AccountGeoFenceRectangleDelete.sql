USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_AccountGeoFenceRectangleDelete')
	BEGIN
		PRINT 'Dropping Procedure custGS_AccountGeoFenceRectangleDelete'
		DROP  Procedure  dbo.custGS_AccountGeoFenceRectangleDelete
	END
GO

PRINT 'Creating Procedure custGS_AccountGeoFenceRectangleDelete'
GO
/******************************************************************************
**		File: custGS_AccountGeoFenceRectangleDelete.sql
**		Name: custGS_AccountGeoFenceRectangleDelete
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
**		Auth: Carly Christiansen
**		Date: 08/22/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	08/22/2013	Carly Chris		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custGS_AccountGeoFenceRectangleDelete
(
	@GeoFenceID BIGINT
	, @ModifiedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Execute Transaction. */	
	BEGIN TRY
		BEGIN TRANSACTION
		
		/** Check to see if this is an insert or an update. */
		IF(EXISTS(SELECT * FROM dbo.GS_AccountGeoFences WHERE (GeoFenceID = @GeoFenceID)))
		BEGIN
			/** Mark GeoFence as DELETE */
			UPDATE dbo.GS_AccountGeoFences SET
				IsDeleted = 1
				, ModifiedOn = GETDATE()
				, ModifiedBy = @ModifiedBy
			WHERE
				(GeoFenceID = @GeoFenceID);

			/** Reset the points of the polygon. */
			DELETE dbo.GS_AccountGeoFencePolygons WHERE GeoFenceId = @GeoFenceID;
			DELETE dbo.GS_AccountGeoFenceCircles WHERE GeoFenceId = @GeoFenceID;
			DELETE dbo.GS_AccountGeoFencePoints WHERE GeoFenceId = @GeoFenceID;
			DELETE dbo.GS_AccountGeoFenceRectangles WHERE GeoFenceId = @GeoFenceID;
		END
			
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
	END CATCH

	/** Check that we have a GeoFenceID. */
	SELECT * FROM [dbo].GS_AccountGeoFenceRectangles WHERE GeoFenceID = @GeoFenceID;

END
GO

GRANT EXEC ON dbo.custGS_AccountGeoFenceRectangleDelete TO PUBLIC
GO