USE [WISE_GPSTRACKING]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND (name = 'fxGetGeoFenceNameUi'))
	BEGIN
		PRINT 'Dropping FUNCTION fxGetGeoFenceNameUi'
		DROP FUNCTION  dbo.fxGetGeoFenceNameUi
	END
GO

PRINT 'Creating FUNCTION fxGetGeoFenceNameUi'
GO
/******************************************************************************
**		File: fxGetGeoFenceNameUi.sql
**		Name: fxGetGeoFenceNameUi
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
CREATE FUNCTION dbo.fxGetGeoFenceNameUi
(
	@GeoFenceTypeID VARCHAR(50)
	, @ReportModeID VARCHAR(3)
	, @GeoFenceName NVARCHAR(50)
)
RETURNS NVARCHAR(100)
AS
BEGIN
	/** Declarations. */
	DECLARE @FenceType NVARCHAR(100) = '[Name not Set]';
	DECLARE @ReportMod NVARCHAR(100) = '[Name not Set]';
	DECLARE @Result NVARCHAR(100) = '[Name not Set]';

	/** Check to see if there is a GeoFenceName set. */
	IF (@GeoFenceName IS NOT NULL) RETURN @GeoFenceName;

	/** Get the Geo Fence Type. */
	SET @FenceType = CASE
			WHEN @GeoFenceTypeID = 'CIR' THEN 'Circular Geo Fence' -- Circle
			WHEN @GeoFenceTypeID = 'PNT' THEN 'Point of Interest' -- Point
			WHEN @GeoFenceTypeID = 'POLY' THEN 'Polygon Geo Fence' -- Polygon
			WHEN @GeoFenceTypeID = 'RECT' THEN 'Rectangular Geo Fence' -- Rectangle
			ELSE 'Undefined'
		END;

	/** Get the Report Mode type. */
	SET @ReportMod = CASE
			WHEN @ReportModeID = '0' THEN 'Uncertain Zone' -- Uncertain
			WHEN @ReportModeID = '1' THEN 'Inclusion Zone' -- Exit Alert
			WHEN @ReportModeID = '2' THEN 'Exclusion Zone' -- Enter Alert
			WHEN @ReportModeID = '3' THEN 'Both Inclusion and Exclusion Zone' -- Exit Enter Alert
			WHEN @ReportModeID = '4' THEN 'Deleted Fence' -- Delete
			ELSE ''
		END;

	/** Build Name */
	SET @Result = @ReportMod + ' of a '+ @FenceType + '.';

	/** Return result. */
	RETURN @Result;
END
GO