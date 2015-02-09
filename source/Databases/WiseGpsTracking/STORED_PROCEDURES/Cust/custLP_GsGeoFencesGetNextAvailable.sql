USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLP_GsGeoFencesGetNextAvailable')
	BEGIN
		PRINT 'Dropping Procedure custLP_GsGeoFencesGetNextAvailable'
		DROP  Procedure  dbo.custLP_GsGeoFencesGetNextAvailable
	END
GO

PRINT 'Creating Procedure custLP_GsGeoFencesGetNextAvailable'
GO
/******************************************************************************
**		File: custLP_GsGeoFencesGetNextAvailable.sql
**		Name: custLP_GsGeoFencesGetNextAvailable
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
**		Date: 12/19/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	12/19/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custLP_GsGeoFencesGetNextAvailable
(
	@AccountID BIGINT
	, @UnitID BIGINT = NULL
	, @NumberOfFences INT = 10
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Check to see if there is @UnitID */
	IF (@UnitID IS NULL)
	BEGIN
		SELECT TOP 1 @UnitID = UnitID FROM dbo.LP_Devices WHERE (AccountID = @AccountID);
	END

	/** Initialize */
	DECLARE @GeoFenceBucketTable TABLE (GeoFenceI INT, UnitID BIGINT);
	DECLARE @Counter INT = 0;
	
	/** Build Temporary tables */
	WHILE (@Counter < @NumberOfFences)
	BEGIN
		INSERT INTO @GeoFenceBucketTable (GeoFenceI, UnitID) VALUES (@Counter, @UnitID);
		/** Increment counter */
		SET @Counter = @Counter + 1;
	END

	SELECT TOP 1
		ISNULL(GGF1.LPGeoFenceID, 0) AS LPGeoFenceID
		, BUK.UnitID
		, ISNULL(GGF1.GsGeoFenceId, 0) AS GsGeoFenceId
		, BUK.GeoFenceI
		, ISNULL(GGF1.ReportModeI, '3') AS ReportModeI
		, ISNULL(GGF1.LattitudeI1, 0) AS LattitudeI1
		, ISNULL(GGF1.LongitudeI1, 0) AS LongitudeI1
		, ISNULL(GGF1.LattitudeI2, 0) AS LattitudeI2
		, ISNULL(GGF1.LongitudeI2, 0) AS LongitudeI2
		, ISNULL(GGF1.IsActive, 1) AS IsActive
		, ISNULL(GGF1.IsDeleted, 0) AS IsDeleted
		, ISNULL(GGF1.ModifiedBy, 'SYSTEM') AS ModifiedBy
		, ISNULL(GGF1.ModifiedOn, GETDATE()) AS ModifiedOn
		, ISNULL(GGF1.CreatedBy, 'SYSTEM') AS CreatedBy
		, ISNULL(GGF1.CreatedOn, GETDATE()) AS CreatedOn
		, ISNULL(GGF1.DEX_ROW_TS, GETUTCDATE()) AS DEX_ROW_TS
		--, GGF2.*
	FROM
		@GeoFenceBucketTable AS BUK
		LEFT OUTER JOIN (
			SELECT
				GGF.LPGeoFenceID
				, ROW_NUMBER() OVER (PARTITION BY GGF.UnitID ORDER BY GGF.GeoFenceI) - 1 AS RowNumber
				, GGF.GeoFenceI - (ROW_NUMBER() OVER (PARTITION BY GGF.UnitID ORDER BY GGF.GeoFenceI) - 1) AS Decision
				, GGF.GeoFenceI
			FROM 
				LP_GsGeoFences AS GGF WITH (NOLOCK)
			WHERE
				(GGF.UnitID = @UnitID)
				AND (GGF.IsDeleted = 0)
				) AS GGF2
		ON
			(BUK.GeoFenceI = GGF2.GeoFenceI)
		LEFT OUTER JOIN [dbo].LP_GsGeoFences AS GGF1
		ON	
			(GGF1.GeoFenceI = GGF2.GeoFenceI)
	WHERE
		(BUK.UnitID = @UnitID)
		--AND (GGF2.Decision <> 0)
	ORDER BY
		GGF1.GeoFenceI;
		
END
GO

GRANT EXEC ON dbo.custLP_GsGeoFencesGetNextAvailable TO PUBLIC
GO

/** Test Stored Procedure. */
EXEC dbo.custLP_GsGeoFencesGetNextAvailable 100169