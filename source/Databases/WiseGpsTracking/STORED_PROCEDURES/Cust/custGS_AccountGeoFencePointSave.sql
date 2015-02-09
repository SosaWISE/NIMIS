USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_AccountGeoFencePointSave')
	BEGIN
		PRINT 'Dropping Procedure custGS_AccountGeoFencePointSave'
		DROP  Procedure  dbo.custGS_AccountGeoFencePointSave
	END
GO

PRINT 'Creating Procedure custGS_AccountGeoFencePointSave'
GO
/******************************************************************************
**		File: custGS_AccountGeoFencePointSave.sql
**		Name: custGS_AccountGeoFencePointSave
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
CREATE Procedure dbo.custGS_AccountGeoFencePointSave
(
	@GeoFenceID BIGINT
	, @AccountId BIGINT
	, @PlaceName NVARCHAR(500)
	, @PlaceDescription NVARCHAR(MAX)
	, @Lattitude FLOAT
	, @Longitude FLOAT
	, @ModifiedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Get OGC Type */
	DECLARE @OGCType VARCHAR(50);
	SET @OGCType = 'POINT'
	DECLARE @GeogCol1 VARCHAR(500);
	SET @GeogCol1 = CAST(@Longitude AS VARCHAR) + ' ' + CAST(@Lattitude AS VARCHAR);
	DECLARE @GeoText VARCHAR(4000);
	SET @GeoText = @OGCType + '(' + @GeogCol1 + ')';
	
	/** Initialize. */
	DECLARE @GeoFenceTypeId VARCHAR(50);
	SET @GeoFenceTypeId = 'PNT';
	
	BEGIN TRY
		BEGIN TRANSACTION
		
		/** Check to see if this is an insert or an update. */
		IF(EXISTS(SELECT * FROM dbo.GS_AccountGeoFences WHERE (GeoFenceID = @GeoFenceID)))
		BEGIN
			/** UPDATE */
			UPDATE dbo.GS_AccountGeoFences SET
				GeoFenceTypeId = @GeoFenceTypeId
				, AccountId = @AccountId
				, GeoFenceName = @PlaceName
				, GeoFenceDescription = @PlaceDescription
				, GeogCol1 = GEOGRAPHY::STGeomFromText(@GeoText, 4326)
				, MeanLattitude = @Lattitude
				, MeanLongitude = @Longitude
				, ModifiedOn = GETDATE()
				, ModifiedBy = @ModifiedBy
			WHERE
				(GeoFenceID = @GeoFenceID);
		END
		ELSE
		BEGIN
			/** INSERT */
			INSERT INTO dbo.GS_AccountGeoFences( GeoFenceTypeId, AccountId, GeogCol1, MeanLattitude, MeanLongitude, ModifiedBy, CreatedBy) 
			VALUES (
				@GeoFenceTypeId, -- GeoFenceTypeId - varchar(50)
				@AccountId, -- AccountId - bigint
				GEOGRAPHY::STGeomFromText(@GeoText, 4326), -- GeogCol1 - geography
				@Lattitude,
				@Longitude,
			    @ModifiedBy, -- ModifiedBy - nvarchar(50)
			    @ModifiedBy -- CreatedBy - nvarchar(50)
				);
			SET @GeoFenceID = SCOPE_IDENTITY();
		END
		
		/** Reset the points of the polygon. */
		DELETE dbo.GS_AccountGeoFencePolygons WHERE GeoFenceId = @GeoFenceID;
		DELETE dbo.GS_AccountGeoFenceCircles WHERE GeoFenceId = @GeoFenceID;
		DELETE dbo.GS_AccountGeoFencePoints WHERE GeoFenceId = @GeoFenceID;
		DELETE dbo.GS_AccountGeoFenceRectangles WHERE GeoFenceId = @GeoFenceID;

		INSERT INTO dbo.GS_AccountGeoFencePoints (
			GeoFenceId ,
			PlaceName ,
			PlaceDescription ,
			Lattitude ,
			Longitude ,
			CreatedBy
		) VALUES ( 
			@GeoFenceID, -- GeoFenceId - bigint
			@PlaceName, -- PlaceName - nvarchar(500)
			@PlaceDescription, -- PlaceDescription - nvarchar(max)
			@Lattitude, -- Lattitude - varchar(50)
			@Longitude, -- Longitude - varchar(50)
			@ModifiedBy -- CreatedBy - nvarchar(50)
		);
			
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
	END CATCH
	
	/** Check that we have a GeoFenceID. */
	SELECT * FROM [dbo].vwGS_AccountGeoFencePoints WHERE GeoFenceID = @GeoFenceID;
	
END
GO

GRANT EXEC ON dbo.custGS_AccountGeoFencePointSave TO PUBLIC
GO

/*
EXEC dbo.custGS_AccountGeoFencePolygonSave NULL, 'POLY', 1000001, '2000.00 100.00,2001.00 101.00,2002.00 102.00,2003.00 103.00,2004.00 104.00', 'donbright'
EXEC dbo.custGS_AccountGeoFencePolygonSave NULL, 'POLY', 100166, '-111.896009 40.768454,-111.896031 40.768324,-111.89485 40.766228,-111.893134 40.766017,-111.890902 40.766553,-111.889958 40.767772,-111.889829 40.769868,-111.893134 40.770323,-111.894657 40.770258,-111.896009 40.768454', 'donbright'
EXEC dbo.custGS_AccountGeoFencePolygonSave 7, 'POLY', 100166, '-111.896009 40.768454,-111.896031 40.768324,-111.89485 40.766228,-111.893134 40.766017,-111.890902 40.766553,-111.889958 40.767772,-111.889829 40.769868,-111.893134 40.770323,-111.894657 40.770258,-111.896009 40.768454', 'donbright'
EXEC dbo.custGS_AccountGeoFencePointSave NULL, 'PNT', 100166, 'This is the place', 'The description field is here.  So enjoy.', 40.768454, -111.896009, 'donbright'
EXEC dbo.custGS_AccountGeoFencePointSave 9, 'PNT', 100166, 'This is the place', 'The description field is here.  So enjoy.', 40.768454, -111.896009, 'donbright'

SELECT * FROM dbo.fn_ParseGpsCoordinates('2000.00 100.00,2001.00 101.00,2002.00 102.00,2003.00 103.00,2004.00 104.00', ',');
SELECT * FROM dbo.fn_ParseGpsCoordinates('40.768454 -111.896009,40.768324 -111.896031,40.766228 -111.89485,40.766017 -111.893134,40.766553 -111.890902,40.767772 -111.889958,40.769868 -111.889829,40.770323 -111.893134,40.770258 -111.894657,40.768454 -111.896009', ',');
*/

DECLARE @Geo1 geography;
SET @Geo1 = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656 )', 4326);

/** This gives the area of a Polygon in square meters because the SRID of the instance is 4326 */
DECLARE @g geography;
SET @g = geography::STGeomFromText('POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326);
SELECT @g.STArea();

/** This gives the length in meters because of the SRID of the instance is 4326. */
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);
SELECT @g.STLength();