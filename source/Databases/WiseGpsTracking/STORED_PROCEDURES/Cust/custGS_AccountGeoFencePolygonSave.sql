USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_AccountGeoFencePolygonSave')
	BEGIN
		PRINT 'Dropping Procedure custGS_AccountGeoFencePolygonSave'
		DROP  Procedure  dbo.custGS_AccountGeoFencePolygonSave
	END
GO

PRINT 'Creating Procedure custGS_AccountGeoFencePolygonSave'
GO
/******************************************************************************
**		File: custGS_AccountGeoFencePolygonSave.sql
**		Name: custGS_AccountGeoFencePolygonSave
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
CREATE Procedure dbo.custGS_AccountGeoFencePolygonSave
(
	@GeoFenceID BIGINT
	, @GeoFenceTypeId VARCHAR(50)
	, @AccountId BIGINT
	, @GeogCol1 VARCHAR(MAX)
	, @ModifiedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Get OGC Type */
	DECLARE @OGCType VARCHAR(50);
	SELECT
		@OGCType = CASE @GeoFenceTypeId
			WHEN 'POLY' THEN 'POLYGON'
			WHEN 'PNT' THEN 'POINT'
			WHEN 'CIR' THEN 'CircularString'
			ELSE 'POINT'
		END;
	PRINT '@OGCType: ' + @OGCType;
	DECLARE @GeoText VARCHAR(4000);
	SELECT
		@GeoText = CASE @OGCType
			WHEN 'POLYGON' THEN @OGCType + '((' + @GeogCol1 + '))'
			WHEN 'POINT' THEN @OGCType + '(' + @GeogCol1 + ')'
			WHEN 'CircularString' THEN @OGCType + '(' + @GeogCol1 + ')'
			ELSE @OGCType + '(' + @GeogCol1 + ')'
		END;
	PRINT '@GeoText: ' + @GeoText;
	
	BEGIN TRY
		BEGIN TRANSACTION
		
		/** Check to see if this is an insert or an update. */
		IF(EXISTS(SELECT * FROM dbo.GS_AccountGeoFences WHERE (GeoFenceID = @GeoFenceID)))
		BEGIN
			/** UPDATE */
			UPDATE dbo.GS_AccountGeoFences SET
				AccountId = @AccountId
				, GeogCol1 = GEOGRAPHY::STGeomFromText(@GeoText, 4326)
				, ModifiedOn = GETDATE()
				, ModifiedBy = @ModifiedBy
			WHERE
				(GeoFenceID = @GeoFenceID);
		END
		ELSE
		BEGIN
			/** INSERT */
			INSERT INTO dbo.GS_AccountGeoFences( GeoFenceTypeId, AccountId, GeogCol1, ModifiedBy, CreatedBy) 
			VALUES (
				@GeoFenceTypeId, -- GeoFenceTypeId - varchar(50)
				@AccountId, -- AccountId - bigint
				GEOGRAPHY::STGeomFromText(@GeoText, 4326), -- GeogCol1 - geography
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
		
		/** Insert new */
		INSERT INTO dbo.GS_AccountGeoFencePolygons (GeoFenceId, Sequence, Lattitude, Longitude, CreatedBy)
		SELECT 
			@GeoFenceID AS GeoFenceID
			, Position AS Sequence
			, Lattitude
			, Longitude
			, @ModifiedBy
		FROM
			dbo.fn_ParseGpsCoordinates(@GeogCol1, ',');
			
		/** Set the Center point to be the average of the Polygons */
		DECLARE @MeanLattitude FLOAT, @MeanLongitude FLOAT
		SELECT
			@MeanLattitude = AVG(CAST(AGFP.Lattitude AS FLOAT(25)))
			, @MeanLongitude = AVG(CAST(AGFP.Longitude AS FLOAT(25)))
		FROM
			[dbo].GS_AccountGeoFences AS AGF WITH (NOLOCK)
			INNER JOIN [dbo].[GS_AccountGeoFencePolygons] AS AGFP WITH (NOLOCK)
			ON
				(AGF.GeoFenceID = AGFP.GeoFenceId)
		WHERE 
			(AGF.GeoFenceID = @GeoFenceID);
		
		/** Upate GeoFence Table. */
		UPDATE [dbo].GS_AccountGeoFences SET 
			MeanLattitude = @MeanLattitude
			, MeanLongitude = @MeanLongitude
		FROM 
			[dbo].GS_AccountGeoFences AS AGF WITH (NOLOCK)
		WHERE
			(AGF.GeoFenceID = @GeoFenceID);
			
			
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
	END CATCH
	
	/** Check that we have a GeoFenceID. */
	SELECT * FROM [dbo].vwGS_AccountGeoFences WHERE GeoFenceID = @GeoFenceID;
	
END
GO

GRANT EXEC ON dbo.custGS_AccountGeoFencePolygonSave TO PUBLIC
GO

/*
EXEC dbo.custGS_AccountGeoFencePolygonSave NULL, 'POLY', 100166, '-111.896009 40.768454,-111.896031 40.768324,-111.89485 40.766228,-111.893134 40.766017,-111.890902 40.766553,-111.889958 40.767772,-111.889829 40.769868,-111.893134 40.770323,-111.894657 40.770258,-111.896009 40.768454', 'donbright'
EXEC dbo.custGS_AccountGeoFencePolygonSave 6, 'POLY', 100166, '-111.896009 40.768454,-111.896031 40.768324,-111.89485 40.766228,-111.893134 40.766017,-111.890902 40.766553,-111.889958 40.767772,-111.889829 40.769868,-111.893134 40.770323,-111.894657 40.770258,-111.896009 40.768454', 'donbright'
EXEC dbo.custGS_AccountGeoFencePolygonSave NULL, 'POLY', 100166, '34.80485954492187 50.91607609098315,34.80485954492187 50.91753710953153,34.815159227539056 50.91759122044873,34.815159227539056 50.9159678655622,34.81258430688476 50.91044803534999,34.81584587304687 50.91044803534999,34.81533088891601 50.90931151845126,34.811897661376946 50.90931151845126,34.8094944020996 50.90395327929007,34.80700531213378 50.9040074060014,34.809666063476556 50.90914915662899,34.8065761586914 50.90920327729935,34.80700531213378 50.91033979684091,34.81035270898437 50.910285677492006,34.81301346032714 50.91607609098315,34.80485954492187 50.91607609098315', 'donbright'

SELECT * FROM dbo.fn_ParseGpsCoordinates('2000.00 100.00,2001.00 101.00,2002.00 102.00,2003.00 103.00,2004.00 104.00', ',');
SELECT * FROM dbo.fn_ParseGpsCoordinates('-111.896009 40.768454,-111.896031 40.768324,-111.89485 40.766228,-111.893134 40.766017,-111.890902 40.766553,-111.889958 40.767772,-111.889829 40.769868,-111.893134 40.770323,-111.894657 40.770258,-111.896009 40.768454', ',');

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

DECLARE @WKT VARCHAR(MAX) = 'POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))'
SELECT GEOGRAPHY::STGeomFromText(@WKT, 4326)