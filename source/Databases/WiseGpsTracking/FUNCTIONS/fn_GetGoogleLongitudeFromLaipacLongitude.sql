USE [WISE_GPSTRACKING]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fn_GetGoogleLongitudeFromLaipacLongitude')
	BEGIN
		PRINT 'Dropping FUNCTION fn_GetGoogleLongitudeFromLaipacLongitude'
		DROP FUNCTION  dbo.fn_GetGoogleLongitudeFromLaipacLongitude
	END
GO

PRINT 'Creating FUNCTION fn_GetGoogleLongitudeFromLaipacLongitude'
GO
/******************************************************************************
**		File: fn_GetGoogleLongitudeFromLaipacLongitude.sql
**		Name: fn_GetGoogleLongitudeFromLaipacLongitude
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
CREATE FUNCTION dbo.fn_GetGoogleLongitudeFromLaipacLongitude
(
	@EWIndicator CHAR(1)
	, @Longitude FLOAT
)
RETURNS VARCHAR(50)
AS
BEGIN
	/** Declarations */
	DECLARE @LongitudeStr VARCHAR(50) = LTRIM(STR(@Longitude,25,4));
	DECLARE @DegressStr VARCHAR(5) = SUBSTRING(@LongitudeStr,1,3);
	DECLARE @Degrees FLOAT = CAST(@DegressStr AS FLOAT);
	DECLARE @Minutes VARCHAR(20) = SUBSTRING(@LongitudeStr, 4, LEN(@LongitudeStr));
	DECLARE @MinFlot FLOAT = CAST(@Minutes AS FLOAT);
	
	/** Convert Minutes to Degrees */
	SET @MinFlot = @MinFlot / 60;
	SET @Degrees = @Degrees + @MinFlot;
	SET @LongitudeStr = CAST(CAST(@Degrees AS DECIMAL(10,6)) AS VARCHAR(50));
	
	/** Figure out positive or negative. */
	IF (@EWIndicator = 'W')
	BEGIN
		SET @LongitudeStr = '-' + @LongitudeStr;
	END

	RETURN @LongitudeStr;
END
GO
/** TESTS 
SELECT dbo.fn_GetGoogleLongitudeFromLaipacLongitude('E', 11141.2213)
SELECT dbo.fn_GetGoogleLongitudeFromLaipacLongitude('W', 11141.2213)
*/