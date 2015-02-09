USE [WISE_GPSTRACKING]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fn_GetGoogleLattitudeFromLaipacLattitude')
	BEGIN
		PRINT 'Dropping FUNCTION fn_GetGoogleLattitudeFromLaipacLattitude'
		DROP FUNCTION  dbo.fn_GetGoogleLattitudeFromLaipacLattitude
	END
GO

PRINT 'Creating FUNCTION fn_GetGoogleLattitudeFromLaipacLattitude'
GO
/******************************************************************************
**		File: fn_GetGoogleLattitudeFromLaipacLattitude.sql
**		Name: fn_GetGoogleLattitudeFromLaipacLattitude
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: VARCHAR(50) Latitude
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés E. Sosa
**		Date: 11/21/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	11/21/2012	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fn_GetGoogleLattitudeFromLaipacLattitude
(
	@NSIndicator CHAR(1)
	, @Latitude FLOAT
)
RETURNS VARCHAR(50)
AS
BEGIN
	/** Declarations */
	DECLARE @LatitudeStr VARCHAR(50) = LTRIM(STR(@Latitude,25,4));
	DECLARE @DegressStr VARCHAR(5) = SUBSTRING(@LatitudeStr,1,2);
	DECLARE @Degrees FLOAT = CAST(@DegressStr AS FLOAT);
	DECLARE @Minutes VARCHAR(20) = SUBSTRING(@LatitudeStr, 3, LEN(@LatitudeStr));
	DECLARE @MinFlot FLOAT = CAST(@Minutes AS FLOAT);
	
	/** Convert Minutes to Degrees */
	SET @MinFlot = @MinFlot / 60;
	SET @Degrees = @Degrees + @MinFlot;
	SET @LatitudeStr = CAST(CAST(@Degrees AS DECIMAL(10,6)) AS VARCHAR(50));
	
	/** Figure out positive or negative. */
	IF (@NSIndicator = 'S')
	BEGIN
		SET @LatitudeStr = '-' + @LatitudeStr;
	END

	RETURN @LatitudeStr;
END
GO
/** TESTS 
SELECT dbo.fn_GetGoogleLattitudeFromLaipacLattitude('N', 4019.159)
SELECT dbo.fn_GetGoogleLattitudeFromLaipacLattitude('S', 4019.159)
*/