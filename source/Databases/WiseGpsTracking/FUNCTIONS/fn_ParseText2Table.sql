USE [WISE_GPSTRACKING]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fn_ParseText2Table')
	BEGIN
		PRINT 'Dropping FUNCTION fn_ParseText2Table'
		DROP FUNCTION  dbo.fn_ParseText2Table
	END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

PRINT 'Creating FUNCTION fn_ParseText2Table'
GO
/**********************************************************************************************************************
**	File: fn_ParseText2Table.sql
**	Name: fn_ParseText2Table
**	PURPOSE: Parse values from a delimited string & return the result as an indexed table Copyright 1996, 1997, 2000,
**	2003 Clayton Groom (<A href="mailto:Clayton_Groom@hotmail.com">Clayton_Groom@hotmail.com</A>)
**	Posted to the public domain Aug, 2004
**	06-17-03 Rewritten as SQL 2000 function.
**	Reworked to allow for delimiters > 1 character in length 
**	and to convert Text values to numbers
**
**	URL: http://www.codeproject.com/Articles/7938/SQL-User-Defined-Function-to-Parse-a-Delimited-Str
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
CREATE FUNCTION dbo.fn_ParseText2Table
(
	@p_SourceText  VARCHAR(8000)
	,@p_Delimeter VARCHAR(100) = ',' --default to comma delimited.
)
RETURNS @retTable TABLE 
(
	Position  INT IDENTITY(1,1)
	,Int_Value INT  
	,Num_value NUMERIC(18,3)
	,txt_value VARCHAR(2000)
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

	IF LEN(@p_SourceText) = 0
	BEGIN
		SET  @w_Continue = 0 -- force early exit
	END 
	ELSE
	BEGIN
		-- parse the original @p_SourceText array into a temp table
		SET  @w_Continue = 1
		SET @w_StartPos = 1
		SET @p_SourceText = RTRIM( LTRIM( @p_SourceText))
		SET @w_Length   = DATALENGTH( RTRIM( LTRIM( @p_SourceText)))
		SET @w_Delimeter_Len = len(@p_Delimeter)
	END
	
	WHILE @w_Continue = 1
	BEGIN
		SET @w_Delimeter_pos = CHARINDEX( @p_Delimeter
		  ,(SUBSTRING( @p_SourceText, @w_StartPos
		  ,((@w_Length - @w_StartPos) + @w_Delimeter_Len)))
		  )

		IF @w_Delimeter_pos > 0  -- delimeter(s) found, get the value
		BEGIN
			SET @w_tmp_txt = LTRIM(RTRIM( SUBSTRING( @p_SourceText, @w_StartPos 
				,(@w_Delimeter_pos - 1)) ))

			IF isnumeric(@w_tmp_txt) = 1
			BEGIN
				SET @w_tmp_int = cast( cast(@w_tmp_txt as NUMERIC) as INT)
				SET @w_tmp_num = cast( @w_tmp_txt as NUMERIC(18,3))
			END
			ELSE
			BEGIN
				SET @w_tmp_int =  null
				SET @w_tmp_num =  null
			END
			SET @w_StartPos = @w_Delimeter_pos + @w_StartPos + (@w_Delimeter_Len- 1)
		END
		ELSE -- No more delimeters, get last value
		BEGIN
		   SET @w_tmp_txt = LTRIM(RTRIM( SUBSTRING( @p_SourceText, @w_StartPos 
			  ,((@w_Length - @w_StartPos) + @w_Delimeter_Len)) ))
		   IF isnumeric(@w_tmp_txt) = 1
		   BEGIN
			SET @w_tmp_int = cast( cast(@w_tmp_txt as NUMERIC) as INT)
			SET @w_tmp_num = cast( @w_tmp_txt as NUMERIC(18,3))
		   END
		   ELSE
		   BEGIN
			SET @w_tmp_int =  null
			SET @w_tmp_num =  null
		   END
		   SELECT @w_Continue = 0
		END

		INSERT INTO @retTable VALUES( @w_tmp_int, @w_tmp_num, @w_tmp_txt )
	END

	RETURN
END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO