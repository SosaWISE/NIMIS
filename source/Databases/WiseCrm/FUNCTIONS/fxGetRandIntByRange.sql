USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetRandIntByRange')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetRandIntByRange'
		DROP FUNCTION  dbo.fxGetRandIntByRange
	END
GO

PRINT 'Creating FUNCTION fxGetRandIntByRange'
GO
/******************************************************************************
**		File: fxGetRandIntByRange.sql
**		Name: fxGetRandIntByRange
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
**		Date: 01/27/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	01/27/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetRandIntByRange
(
	@Upper INT,
	@Lower INT
)
RETURNS INT
AS
BEGIN
	---- Create the variables for the random number generation
	DECLARE @Random INT,
		@Result INT;

	-- Get Random
	SELECT @Random = rndResult FROM [dbo].[vwRandNumber];

	---- This will create a random number between 1 and 999
	SELECT @Random = ROUND(((@Upper - @Lower -1) * @Random + @Lower), 0);

	/** Return result. */
	RETURN @Random;
END
GO

/** Testing */

--SELECT dbo.fxGetRandIntByRange(10, 20);