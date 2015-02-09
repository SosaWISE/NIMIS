USE [WISE_HumanResource]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetPhoneNumberScrubbed')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetPhoneNumberScrubbed'
		DROP FUNCTION  dbo.fxGetPhoneNumberScrubbed
	END
GO

PRINT 'Creating FUNCTION fxGetPhoneNumberScrubbed'
GO
/******************************************************************************
**		File: fxGetPhoneNumberScrubbed.sql
**		Name: fxGetPhoneNumberScrubbed
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
CREATE FUNCTION dbo.fxGetPhoneNumberScrubbed
(
	@PhoneNumber VARCHAR(50)
)
RETURNS VARCHAR(20)
AS
BEGIN
	-- Locals
	DECLARE @Result VARCHAR(20)
	SET @Result = LTRIM(RTRIM(@PhoneNumber))
	
	/* Init values */
	SET @Result = REPLACE(@Result, '(', '')
	SET @Result = REPLACE(@Result, ')', '')
	SET @Result = REPLACE(@Result, ' ', '')
	SET @Result = REPLACE(@Result, '_', '')
	SET @Result = REPLACE(@Result, 'x', '')
	SET @Result = REPLACE(@Result, '-', '')

	-- Return result
	RETURN @Result
END
GO