USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxSplitLongList')
	BEGIN
		PRINT 'Dropping FUNCTION fxSplitLongList'
		DROP FUNCTION  dbo.fxSplitLongList
	END
GO

PRINT 'Creating FUNCTION fxSplitLongList'
GO
/******************************************************************************
**		File: fxSplitLongList.sql
**		Name: fxSplitLongList
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
CREATE FUNCTION dbo.fxSplitLongList
(
	@IDList NVARCHAR(MAX)
)
RETURNS 
@ParsedList table
(
	ID BIGINT
)
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