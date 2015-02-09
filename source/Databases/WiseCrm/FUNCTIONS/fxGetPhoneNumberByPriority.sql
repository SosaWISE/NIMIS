USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetPhoneNumberByPriority')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetPhoneNumberByPriority'
		DROP FUNCTION  dbo.fxGetPhoneNumberByPriority
	END
GO

PRINT 'Creating FUNCTION fxGetPhoneNumberByPriority'
GO
/******************************************************************************
**		File: fxGetPhoneNumberByPriority.sql
**		Name: fxGetPhoneNumberByPriority
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
**		Date: 03/14/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	03/14/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetPhoneNumberByPriority
(
	@PhoneHome VARCHAR(20)
	, @PhoneWork NVARCHAR(30)
	, @PhoneMble VARCHAR(20)
)
RETURNS VARCHAR(50)
AS
BEGIN
	/** Declarations */
	DECLARE @Result VARCHAR(50);

	/** Initialize */
	SET @PhoneHome = LTRIM(RTRIM(@PhoneHome));
	SET @PhoneWork = LTRIM(RTRIM(@PhoneWork));
	SET @PhoneMble = LTRIM(RTRIM(@PhoneMble));

	/** Build fullname */
	SELECT @Result = 
		CASE
			WHEN @PhoneHome IS NOT NULL AND @PhoneHome <> '' THEN 'H: ' + @PhoneHome
			WHEN @PhoneWork IS NOT NULL AND @PhoneWork <> '' THEN 'W: ' + @PhoneWork
			WHEN @PhoneMble IS NOT NULL AND @PhoneMble <> '' THEN 'M: ' + @PhoneMble
			ELSE '[No Phone]'
		END
	
	RETURN @Result;
END
GO

SELECT [dbo].fxGetPhoneNumberByPriority('CCCJJJOOOO', 'AAABBBLLLL', '9879874563') AS [Phone];