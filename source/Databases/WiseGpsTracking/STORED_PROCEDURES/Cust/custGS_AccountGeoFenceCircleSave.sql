USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_AccountGeoFenceCircleSave')
	BEGIN
		PRINT 'Dropping Procedure custGS_AccountGeoFenceCircleSave'
		DROP  Procedure  dbo.custGS_AccountGeoFenceCircleSave
	END
GO

PRINT 'Creating Procedure custGS_AccountGeoFenceCircleSave'
GO
/******************************************************************************
**		File: custGS_AccountGeoFenceCircleSave.sql
**		Name: custGS_AccountGeoFenceCircleSave
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
CREATE Procedure dbo.custGS_AccountGeoFenceCircleSave
(
	@GeoFenceID BIGINT
	, @AccountId BIGINT
	, @CenterLattitude FLOAT
	, @CenterLongitude FLOAT
	, @Radius FLOAT
	, @ModifiedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Initialize */
	DECLARE @GeoFenceTypeId VARCHAR(50);
	SET @GeoFenceTypeId = 'CIR';

	PRINT '@GeoFenceTypeId: ' + @GeoFenceTypeId;

	/** Get OGC Type */
	DECLARE @OGCType VARCHAR(50);
	SET @OGCType = 'POINT' -- ONCE we upgrade to MS SQL 2012 we can change it to a Circle.
	DECLARE @GeogCol1 VARCHAR(500);
	SET @GeogCol1 = CAST(@CenterLongitude AS VARCHAR) + ' ' + CAST(@CenterLattitude AS VARCHAR);
	DECLARE @GeoText VARCHAR(4000);
	SET @GeoText = @OGCType + '(' + @GeogCol1 + ')';
	
	BEGIN TRY
		BEGIN TRANSACTION
		
		/** Check to see if this is an insert or an update. */
		IF(EXISTS(SELECT * FROM dbo.GS_AccountGeoFences WHERE (GeoFenceID = @GeoFenceID)))
		BEGIN
			/** UPDATE */
			UPDATE dbo.GS_AccountGeoFences SET
				GeoFenceTypeId = @GeoFenceTypeId
				, AccountId = @AccountId
				, GeogCol1 = GEOGRAPHY::STGeomFromText(@GeoText, 4326)
				, MeanLattitude = @CenterLattitude
				, MeanLongitude = @CenterLongitude
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
				@CenterLattitude,
				@CenterLongitude,
			    @ModifiedBy, -- ModifiedBy - nvarchar(50)
			    @ModifiedBy -- CreatedBy - nvarchar(50)
				);
			SET @GeoFenceID = SCOPE_IDENTITY();
		END
		
		/** Reset the points of the polygon. */
		DELETE dbo.GS_AccountGeoFencePolygons WHERE GeoFenceId = @GeoFenceID;
		DELETE dbo.GS_AccountGeoFenceCircles WHERE GeoFenceId = @GeoFenceID;
		DELETE dbo.GS_AccountGeoFencePoints WHERE GeoFenceId = @GeoFenceID;
		
		/** Insert new */
		INSERT INTO dbo.GS_AccountGeoFenceCircles(
			GeoFenceId,
			CenterLattitude,
			CenterLongitude,
			Radius,
			CreatedBy
		) VALUES (
			@GeoFenceID, -- GeoFenceId - bigint
			@CenterLattitude, -- CenterLattitude - float
			@CenterLongitude, -- CenterLongitude - float
			@Radius, -- Radius - varchar(50)
			@ModifiedBy -- CreatedBy - nvarchar(50)
		);
			
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

GRANT EXEC ON dbo.custGS_AccountGeoFenceCircleSave TO PUBLIC
GO

/*
EXEC dbo.custGS_AccountGeoFenceCircleSave NULL, 100166, 40.768454, -111.896009, 2000, 'donbright'
EXEC dbo.custGS_AccountGeoFenceCircleSave   13, 100166, 40.768454, -111.896009, 2000, 'donbright'

SELECT * FROM dbo.fn_ParseGpsCoordinates('2000.00 100.00,2001.00 101.00,2002.00 102.00,2003.00 103.00,2004.00 104.00', ',');
SELECT * FROM dbo.fn_ParseGpsCoordinates('-111.896009 40.768454,-111.896031 40.768324,-111.89485 40.766228,-111.893134 40.766017,-111.890902 40.766553,-111.889958 40.767772,-111.889829 40.769868,-111.893134 40.770323,-111.894657 40.770258,-111.896009 40.768454', ',');

*/