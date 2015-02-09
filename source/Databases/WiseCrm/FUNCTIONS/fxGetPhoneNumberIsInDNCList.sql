USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetPhoneNumberIsInDNCList')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetPhoneNumberIsInDNCList'
		DROP FUNCTION  dbo.fxGetPhoneNumberIsInDNCList
	END
GO

PRINT 'Creating FUNCTION fxGetPhoneNumberIsInDNCList'
GO
/******************************************************************************
**		File: fxGetPhoneNumberIsInDNCList.sql
**		Name: fxGetPhoneNumberIsInDNCList
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
**		Date: 03/20/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	03/20/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetPhoneNumberIsInDNCList
(
	@PhoneNumber VARCHAR(20)
)
RETURNS BIT
AS
BEGIN
	/** Initialize */
	DECLARE @Result BIT = 0;

	/** Check for phone number */
	SELECT @Result = 1 FROM [dbo].[DC_PhoneNumbers] WHERE (PhoneNumberID = @PhoneNumber);

	/** Return result. */
	RETURN @Result;
END
GO

/**
	SELECT dbo.fxGetPhoneNumberIsInDNCList('9009879876') AS [In List];
	SELECT dbo.fxGetPhoneNumberIsInDNCList('8012267067') AS [In List];
	SELECT dbo.fxGetPhoneNumberIsInDNCList(NULL) AS [In List];
*/