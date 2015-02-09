USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_AccountGeoFencesBindLaipacFence')
	BEGIN
		PRINT 'Dropping Procedure custGS_AccountGeoFencesBindLaipacFence'
		DROP  Procedure  dbo.custGS_AccountGeoFencesBindLaipacFence
	END
GO

PRINT 'Creating Procedure custGS_AccountGeoFencesBindLaipacFence'
GO
/******************************************************************************
**		File: custGS_AccountGeoFencesBindLaipacFence.sql
**		Name: custGS_AccountGeoFencesBindLaipacFence
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
**		Date: 12/17/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	12/17/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custGS_AccountGeoFencesBindLaipacFence
(
	@CommandMessageID BIGINT
	, @AccountId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Initialize. */
	DECLARE @UnitID BIGINT;
	DECLARE @GeoFenceI TINYINT;
	DECLARE @ReportModeI CHAR(1);
	DECLARE @GeoFenceID BIGINT = 0;
	DECLARE @Latitude1 FLOAT;
	DECLARE @Longitud1 FLOAT;
	DECLARE @Latitude2 FLOAT;
	DECLARE @Longitud2 FLOAT;
	SELECT @UnitID = UnitID, @GeoFenceI = GeoFenceI, @ReportModeI = ReportModeI, @Latitude1 = LattitudeI1, @Longitud1 = LongitudeI1, @Latitude2 = LattitudeI2, @Longitud2 = LongitudeI2 FROM [WISE_GPSTRACKING].[dbo].LP_CommandMessageEAVRSP4s WHERE (CommandMessageID = @CommandMessageID);

	/** Check to see if there is LP_GsGeoFence */
	IF (EXISTS(SELECT * FROM [WISE_GPSTRACKING].[dbo].LP_GsGeoFences WHERE (UnitID = @UnitID) AND (GeoFenceI = @GeoFenceI) AND (IsDeleted = 0)))
	BEGIN
		SELECT @GeoFenceID = GsGeoFenceId FROM [WISE_GPSTRACKING].[dbo].LP_GsGeoFences WHERE (UnitID = @UnitID) AND (GeoFenceI = @GeoFenceI)
	END

	/** Build the GeoFence in GS_AccountGeoFences and get the @GeoFenceID. */	
	EXEC [dbo].custGS_AccountGeoFenceRectangleSave @GeoFenceID OUTPUT, @AccountId, @ReportModeI, NULL, NULL,
	    @MaxLattitude = @Latitude1, -- float
	    @MinLongitude = @Longitud1, -- float
	    @MinLattitude = @Latitude2, -- float
	    @MaxLongitude = @Longitud2, -- float
	    @ModifiedBy = N'SYSTEM'; -- nvarchar(50)

	/** Save GeoFence information */
	
	BEGIN TRANSACTION
	BEGIN TRY
		/** Initialize */
		DECLARE @LPGeoFenceID BIGINT = NULL;
		SELECT @LPGeoFenceID = LPGeoFenceID FROM dbo.LP_GsGeoFences WHERE (UnitID = @UnitID) AND (GeoFenceI = @GeoFenceI) AND (IsDeleted = 0);
		
		/** Check to see that there is an entry in dbo.LP_GsGeoFences. */
		IF (@LPGeoFenceID IS NULL)
		BEGIN
			INSERT INTO [dbo].LP_GsGeoFences (
				UnitID ,
				GsGeoFenceId ,
				GeoFenceI ,
				ReportModeI ,
				LattitudeI1 ,
				LongitudeI1 ,
				LattitudeI2 ,
				LongitudeI2
			) VALUES (
				@UnitID , -- UnitID - bigint
				@GeoFenceID , -- GsGeoFenceId - bigint
				@GeoFenceI , -- GeoFenceI - tinyint
				@ReportModeI , -- ReportModeI - tinyint
				@Latitude1 , -- LattitudeI1 - float
				@Longitud1 , -- LongitudeI1 - float
				@Latitude2 , -- LattitudeI2 - float
				@Longitud2  -- LongitudeI2 - float
			);
			SET @LPGeoFenceID = SCOPE_IDENTITY();
		END
		ELSE
		BEGIN 
			UPDATE [dbo].LP_GsGeoFences SET 
				UnitID = @UnitID,
				GsGeoFenceId = @GeoFenceID,
				GeoFenceI = @GeoFenceI,
				ReportModeI = @ReportModeI,
				LattitudeI1 = @Latitude1,
				LongitudeI1 = @Longitud1,
				LattitudeI2 = @Latitude2,
				LongitudeI2 = @Longitud2
			WHERE
				(LPGeoFenceID = @LPGeoFenceID);
		END
	END TRY
	
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
	END CATCH
	COMMIT TRANSACTION
	
	/** Return result */
	SELECT * FROM [dbo].[vwGS_AccountGeoFences] WHERE (GeoFenceID = @GeoFenceID);
END
GO

GRANT EXEC ON dbo.custGS_AccountGeoFencesBindLaipacFence TO PUBLIC
GO

/**
EXEC dbo.custGS_AccountGeoFencesBindLaipacFence @CommandMessageID = 5546, @AccountId = 100169;

@GeoText: POLYGON((
-111.887 40.4256,
-111.887 40.4256,
-111.887 40.4263,
-111.887 40.4263,
-111.887 40.4256))
                   04025.5760,-11153.2497,04025.5335,-11153.1928
SELECT UnitID, GeoFenceI, ReportModeI, LattitudeI1, LongitudeI1, STR(LongitudeI1, 15, 6), LattitudeI2, LongitudeI2 FROM [WISE_GPSTRACKING].[dbo].LP_CommandMessageEAVRSP4s WHERE (CommandMessageID = 5546);
*/