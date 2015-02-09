USE [WISE_GPSTRACKING]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND (name = 'fxGetGeoFenceTypeUi'))
	BEGIN
		PRINT 'Dropping FUNCTION fxGetGeoFenceTypeUi'
		DROP FUNCTION  dbo.fxGetGeoFenceTypeUi
	END
GO

PRINT 'Creating FUNCTION fxGetGeoFenceTypeUi'
GO
/******************************************************************************
**		File: fxGetGeoFenceTypeUi.sql
**		Name: fxGetGeoFenceTypeUi
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés E. Sosa
**		Date: 07/02/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	07/02/2013	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetGeoFenceTypeUi
(
	@GeoFenceTypeID VARCHAR(50)
)
RETURNS NVARCHAR(100)
AS
BEGIN
	/** Declarations. */
	DECLARE @Result NVARCHAR(100) = '[Name not Set]';

	/** Get the Geo Fence Type. */
	SET @Result = CASE
			WHEN @GeoFenceTypeID = 'CIR' THEN 'fence' -- Circle
			WHEN @GeoFenceTypeID = 'PNT' THEN 'fence' -- Point
			WHEN @GeoFenceTypeID = 'POLY' THEN 'fence' -- Polygon
			WHEN @GeoFenceTypeID = 'RECT' THEN 'fence' -- Rectangle
			ELSE 'fence'
		END;

	/** Return result. */
	RETURN @Result;
END
GO