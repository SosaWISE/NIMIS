USE [WISE_AuthenticationControl]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxRU_SeasonGetByName')
	BEGIN
		PRINT 'Dropping FUNCTION fxRU_SeasonGetByName'
		DROP FUNCTION  dbo.fxRU_SeasonGetByName
	END
GO

PRINT 'Creating FUNCTION fxRU_SeasonGetByName'
GO
/******************************************************************************
**		File: fxRU_SeasonGetByName.sql
**		Name: fxRU_SeasonGetByName
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
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/05/2013	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxRU_SeasonGetByName
(
	@SeasonName NVARCHAR(50)
)
RETURNS INT
AS
BEGIN
	-- Locals
	DECLARE @SeasonID INT
	SET @SeasonName = LTRIM(RTRIM(@SeasonName))
	
	/* Init values */
	SELECT @SeasonID = SeasonID FROM dbo.RU_Season WHERE (SeasonName = @SeasonName)
	
	/** Check that the season ID was found. */
	IF (@SeasonID IS NULL) SET @SeasonID = 0

	-- Return result
	RETURN @SeasonID
END
GO