USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sys.objects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxSplitStringList')
	BEGIN
		PRINT 'Dropping FUNCTION fxSplitStringList'
		DROP FUNCTION  dbo.fxSplitStringList
	END
GO

PRINT 'Creating FUNCTION fxSplitStringList'
GO
/******************************************************************************
**		File: fxSplitStringList.sql
**		Name: fxSplitStringList
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
**		Date: 05/26/2011
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/26/2011	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSplitStringList
(
	@IDList NVARCHAR(MAX)
)
RETURNS 
@ParsedList TABLE
(
	ID NVARCHAR(100)
)
AS
BEGIN
	DECLARE @ID NVARCHAR(100), @Pos int

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
				VALUES (@ID) --Use Appropriate conversion
			END
			SET @IDList = RIGHT(@IDList, LEN(@IDList) - @Pos)
			SET @Pos = CHARINDEX(',', @IDList, 1)

		END
	END	
	RETURN
END
GO