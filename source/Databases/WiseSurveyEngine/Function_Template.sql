USE [WISE_SurveyEngine]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'FUNCTION_TEMPLATE')
	BEGIN
		PRINT 'Dropping FUNCTION FUNCTION_TEMPLATE'
		DROP FUNCTION  dbo.FUNCTION_TEMPLATE
	END
GO

PRINT 'Creating FUNCTION FUNCTION_TEMPLATE'
GO
/******************************************************************************
**		File: FUNCTION_TEMPLATE.sql
**		Name: FUNCTION_TEMPLATE
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
**		Date: 05/26/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/26/2012	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.FUNCTION_TEMPLATE
(
	@IDList NVARCHAR(MAX)
)
RETURNS BIGINT
AS
BEGIN
	DECLARE @ID varchar(20), @Pos int

	SET @IDList = LTRIM(RTRIM(@IDList))+ ','
	SET @Pos = CHARINDEX(',', @IDList, 1)

	IF REPLACE(@IDList, ',', '') <> ''
	BEGIN
		WHILE @Pos > 0
		BEGIN
			SET @ID = LTRIM(RTRIM(LEFT(@IDList, @Pos - 1)))
			IF @ID <> ''
			BEGIN
				INSERT INTO @ParsedList (ID) 
				VALUES (CAST(@ID AS BIGINT)) --Use Appropriate conversion
			END
			SET @IDList = RIGHT(@IDList, LEN(@IDList) - @Pos)
			SET @Pos = CHARINDEX(',', @IDList, 1)

		END
	END	
	RETURN
END
GO