USE [WISE_GPSTRACKING]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fn_ParseGpsCoordinates')
	BEGIN
		PRINT 'Dropping FUNCTION fn_ParseGpsCoordinates'
		DROP FUNCTION  dbo.fn_ParseGpsCoordinates
	END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

PRINT 'Creating FUNCTION fn_ParseGpsCoordinates'
GO
/**********************************************************************************************************************
**	File: fn_ParseGpsCoordinates.sql
**	Name: fn_ParseGpsCoordinates
**	PURPOSE: Returns a table with the following columns
**		Position INT
**		Longitude NUMERIC(10, 5)
**		Lattitude NUMERIC(10, 5) 
**              
**	Return values: Table of IDs/Ints
** 
**	Called by:   
**              
**	Parameters:
**	Input						Output
**	----------					-----------
**
**	Auth: Andrés E. Sosa
**	Date: 10/02/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	----------	--------------	-------------------------------------------
**	10/02/2012	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fn_ParseGpsCoordinates
(
	@p_SourceText  VARCHAR(8000)
	,@p_Delimeter VARCHAR(100) = ',' --default to comma delimited.
)
RETURNS @retTable TABLE 
(
	Position  INT IDENTITY(1,1)
	, Longitude NUMERIC(18,3)
	, Lattitude NUMERIC(18,3)
)
AS
BEGIN
	DECLARE @w_Continue  INT
		,@w_StartPos  INT
		,@w_Length  INT
		,@w_Delimeter_pos INT
		,@w_tmp_int  INT
		,@w_tmp_num  NUMERIC(18,3)
		,@w_tmp_txt   VARCHAR(2000)
		,@w_Delimeter_Len TINYINT
	DECLARE @coordTable TABLE (Position INT, Coordinates VARCHAR(50)); 

	IF LEN(@p_SourceText) = 0
	BEGIN
		SET  @w_Continue = 0 -- force early exit
	END 
	ELSE
	BEGIN
		-- parse the original @p_SourceText array into a temp table
		SET @w_Continue = 1
		SET @w_StartPos = 1
		SET @p_SourceText = RTRIM( LTRIM( @p_SourceText))
		SET @w_Length   = DATALENGTH( RTRIM( LTRIM( @p_SourceText)))
		SET @w_Delimeter_Len = len(@p_Delimeter)
		
		INSERT INTO @coordTable (Position, Coordinates)
			SELECT Position, txt_value AS Coordinates FROM dbo.fn_ParseText2Table(@p_SourceText, ',');
		/** Get the length of the result. */
		SELECT @w_Length = COUNT(*) FROM @coordTable;
	END
	
	WHILE (@w_Continue = 1)
	BEGIN
		/** Initialize */
		DECLARE @w_coordinatesText VARCHAR(100);
		/** Get current row info. */
		SELECT @w_coordinatesText = Coordinates FROM @coordTable WHERE Position = @w_StartPos;
		
		/** Parse text */
		DECLARE @w_lonText VARCHAR(50)
			, @w_latText VARCHAR(50)
		SELECT @w_lonText = SUBSTRING(@w_coordinatesText, 0, CHARINDEX(' ', @w_coordinatesText));
		SELECT @w_latText = SUBSTRING(@w_coordinatesText, CHARINDEX(' ', @w_coordinatesText), @w_Length);

		/** Add values to the return table. */
		INSERT INTO @retTable VALUES( CAST(@w_lonText AS DECIMAL(10,5)), CAST(@w_latText AS DECIMAL(10,5)));
		
		/** Increase row position */
		SET @w_StartPos = @w_StartPos + 1;
		
		/** Check that we are done with the list of coords. */
		IF (@w_StartPos >= @w_Length) SET @w_Continue = 0;
	END

	RETURN
END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO