USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_AccountGeoFenceRectangleSave')
	BEGIN
		PRINT 'Dropping Procedure custGS_AccountGeoFenceRectangleSave'
		DROP  Procedure  dbo.custGS_AccountGeoFenceRectangleSave
	END
GO

PRINT 'Creating Procedure custGS_AccountGeoFenceRectangleSave'
GO
/******************************************************************************
**		File: custGS_AccountGeoFenceRectangleSave.sql
**		Name: custGS_AccountGeoFenceRectangleSave
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
**		Date: 11/08/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	11/08/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custGS_AccountGeoFenceRectangleSave
(
	@GeoFenceID BIGINT
	, @AccountId BIGINT
	, @ReportModeId VARCHAR(3)
	, @GeoFenceName NVARCHAR(50)
	, @GeoFenceDescription NVARCHAR(MAX)
	, @MaxLattitude FLOAT
	, @MinLongitude FLOAT
	, @MinLattitude FLOAT
	, @MaxLongitude FLOAT
	, @GoogleMapZoomLevel SMALLINT = NULL
	, @ModifiedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Get OGC Type */
	DECLARE @OGCType VARCHAR(50);
	SET @OGCType = 'POLYGON'
	DECLARE @GeogCol1 VARCHAR(500);
	--SET @GeogCol1 = CAST(@MinLongitude AS VARCHAR) + ' ' + CAST(@MinLattitude AS VARCHAR)
	--	+ ',' + CAST(@MaxLongitude AS VARCHAR) + ' ' + CAST(@MinLattitude AS VARCHAR)
	--	+ ',' + CAST(@MaxLongitude AS VARCHAR) + ' ' + CAST(@MaxLattitude AS VARCHAR)
	--	+ ',' + CAST(@MinLongitude AS VARCHAR) + ' ' + CAST(@MaxLattitude AS VARCHAR)
	--	+ ',' + CAST(@MinLongitude AS VARCHAR) + ' ' + CAST(@MinLattitude AS VARCHAR);
	SET @GeogCol1 = STR(@MinLongitude, 15, 6) + ' ' + STR(@MinLattitude, 15, 6)
		+ ',' + STR(@MaxLongitude, 15, 6) + ' ' + STR(@MinLattitude, 15, 6)
		+ ',' + STR(@MaxLongitude, 15, 6) + ' ' + STR(@MaxLattitude, 15, 6)
		+ ',' + STR(@MinLongitude, 15, 6) + ' ' + STR(@MaxLattitude, 15, 6)
		+ ',' + STR(@MinLongitude, 15, 6) + ' ' + STR(@MinLattitude, 15, 6);
	
	DECLARE @GeoText VARCHAR(4000);
	SET @GeoText = @OGCType + '((' + @GeogCol1 + '))';
	
	PRINT '@GeoText: ' + @GeoText;	
	
	/** Calculate center. */
	DECLARE @LatCenter FLOAT;
	SET @LatCenter = ((@MaxLattitude - @MinLattitude) / 2) + @MinLattitude;
	DECLARE @LogCenter FLOAT;
	SET @LogCenter = ((@MaxLongitude - @MinLongitude) / 2) + @MinLongitude;
	
	/** Initialize. */
	DECLARE @GeoFenceTypeId VARCHAR(50);
	SET @GeoFenceTypeId = 'RECT';
	PRINT 'I''M HERE...!!!!!';

	BEGIN TRY
		BEGIN TRANSACTION
		
		/** Check to see if this is an insert or an update. */
		IF(EXISTS(SELECT * FROM dbo.GS_AccountGeoFences WHERE (GeoFenceID = @GeoFenceID)))
		BEGIN
			/** UPDATE */
			PRINT 'THIS IS AN UPDATE.  GeoFenceID: ' + CAST(@GeoFenceID AS VARCHAR);
			UPDATE dbo.GS_AccountGeoFences SET
				GeoFenceTypeId = @GeoFenceTypeId
				, AccountId = @AccountId
				, ReportModeId = @ReportModeId
				, GeoFenceName = ISNULL(@GeoFenceName, GAGF.GeoFenceName)
				, GeoFenceDescription = ISNULL(@GeoFenceDescription, GAGF.GeoFenceDescription)
				, GeogCol1 = GEOGRAPHY::STGeomFromText(@GeoText, 4326)
				, MeanLattitude = @LatCenter
				, MeanLongitude = @LogCenter
				, GoogleMapZoomLevel = @GoogleMapZoomLevel
				, ModifiedOn = GETDATE()
				, ModifiedBy = @ModifiedBy
			FROM
				dbo.GS_AccountGeoFences AS GAGF WITH (NOLOCK)
			WHERE
				(GAGF.GeoFenceID = @GeoFenceID);
		END
		ELSE
		BEGIN
			/** INSERT */
			PRINT 'THIS IS A CREATE.  GeoFenceID: ' + CAST(@GeoFenceID AS VARCHAR);
			INSERT INTO dbo.GS_AccountGeoFences( GeoFenceTypeId, AccountId, ReportModeId, GeoFenceName, GeoFenceDescription, GeogCol1, MeanLattitude, MeanLongitude, GoogleMapZoomLevel, ModifiedBy, CreatedBy) 
			VALUES (
				@GeoFenceTypeId, -- GeoFenceTypeId - varchar(50)
				@AccountId, -- AccountId - bigint
				@ReportModeId,
				@GeoFenceName,
				@GeoFenceDescription,
				GEOGRAPHY::STGeomFromText(@GeoText, 4326), -- GeogCol1 - geography
				@LatCenter,
				@LogCenter,
				@GoogleMapZoomLevel,
			    @ModifiedBy, -- ModifiedBy - nvarchar(50)
			    @ModifiedBy -- CreatedBy - nvarchar(50)
				);
			SET @GeoFenceID = SCOPE_IDENTITY();
		END
		
		/** Reset the points of the polygon. */
		DELETE dbo.GS_AccountGeoFencePolygons WHERE GeoFenceId = @GeoFenceID;
		DELETE dbo.GS_AccountGeoFenceCircles WHERE GeoFenceId = @GeoFenceID;
		DELETE dbo.GS_AccountGeoFencePoints WHERE GeoFenceId = @GeoFenceID;
		
		/** Check to see if this is an insert or an update. */
		IF(EXISTS(SELECT * FROM dbo.GS_AccountGeoFenceRectangles WHERE (GeoFenceID = @GeoFenceID)))
		BEGIN
			UPDATE dbo.GS_AccountGeoFenceRectangles SET
				MaxLattitude = @MaxLattitude
				, MinLongitude = @MinLongitude
				, MinLattitude = @MinLattitude
				, MaxLongitude = @MaxLongitude
			WHERE 
				(GeoFenceID = @GeoFenceID);
		END
		ELSE
		BEGIN
			INSERT INTO dbo.GS_AccountGeoFenceRectangles (
				GeoFenceID,
				MaxLattitude,
				MinLongitude,
				MinLattitude,
				MaxLongitude,
				CreatedBy
			) VALUES (
				@GeoFenceID, -- GeoFenceID - bigint
				@MaxLattitude, -- UpperLattitude - float
				@MinLongitude, -- UpperLongitude - float
				@MinLattitude, -- BottomLattitude - float
				@MaxLongitude, -- BottomLongitude - float
				@ModifiedBy -- CreatedBy - nvarchar(50)
			);
		END
					
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

GRANT EXEC ON dbo.custGS_AccountGeoFenceRectangleSave TO PUBLIC
GO

/** Create a Rectangle */
DECLARE @MaxLattitude FLOAT = 40.3276785;			-- 39.842796
DECLARE @MinLongitude FLOAT = -111.69248879999998;	-- -105.309337
DECLARE @MinLattitude FLOAT = 40.318542808709708;	-- 39.746777
DECLARE @MaxLongitude FLOAT = -111.68210183710936;	-- -105.016826
DECLARE @ReportModeId VARCHAR(3) = '3';
DECLARE @GeoFenceName NVARCHAR(50) = 'Yellow Hello Friends. ';
DECLARE @GeoFenceDescription NVARCHAR(MAX) = 'Thsi is a test...';


EXEC dbo.custGS_AccountGeoFenceRectangleSave 10011, 100169, @ReportModeId, @GeoFenceName, @GeoFenceDescription, @MaxLattitude, @MinLongitude, @MinLattitude, @MaxLongitude, NULL, 'andres';
SELECT * FROM GS_AccountGeoFences; -- WHERE GeoFenceID = 15


/** This gives the area of a Polygon in square meters because the SRID of the instance is 4326 
-- DECLARE @WKT VARCHAR(MAX) = 'POLYGON((-105.309337 39.842796,-105.309337 39.746777,-105.016826 39.746777,-105.016826 39.842796,-105.309337 39.842796))';
DECLARE @WKT VARCHAR(MAX) = 'POLYGON((-111.68210183710936 40.3276785,-111.68210183710936 40.318542808709708,-111.69248879999998 40.318542808709708,-111.69248879999998 40.3276785,-111.68210183710936 40.3276785))';
DECLARE @g geography = geography::STGeomFromText(@WKT, 4326);

SELECT @g.STArea() AS [Area];
SELECT @g AS [GEO];
*/