USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxSubscriberNumberWithPadding')
	BEGIN
		PRINT 'Dropping FUNCTION fxSubscriberNumberWithPadding'
		DROP FUNCTION  dbo.fxSubscriberNumberWithPadding
	END
GO

PRINT 'Creating FUNCTION fxSubscriberNumberWithPadding'
GO
/******************************************************************************
**		File: fxSubscriberNumberWithPadding.sql
**		Name: fxSubscriberNumberWithPadding
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
**		Date: 02/27/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/27/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSubscriberNumberWithPadding
(
	@SubscriberInt INT
	, @PadLength INT = 6
	, @LeftPadChar CHAR(1)
)
RETURNS VARCHAR(6)
AS
BEGIN
	/** Declarations */
	DECLARE @Result VARCHAR(6)
	, @SubscriberChar VARCHAR(6);

	/** Initialize. */
	SET @SubscriberChar = CAST(@SubscriberInt AS VARCHAR(6));

	SELECT @Result = REPLICATE(@LeftPadChar, @PadLength - LEN(@SubscriberChar))
		+ @SubscriberChar;
    
	/** Return result. */
	RETURN @Result;
END
GO

/** Tests
SELECT dbo.fxSubscriberNumberWithPadding(1, 5, 'e');
SELECT dbo.fxSubscriberNumberWithPadding(12, 5, '0');
SELECT dbo.fxSubscriberNumberWithPadding(12, 6, '0');
SELECT dbo.fxSubscriberNumberWithPadding(54321, 5, 'F');
SELECT dbo.fxSubscriberNumberWithPadding(54321, 6, 'F');
SELECT dbo.fxSubscriberNumberWithPadding(3214, 5, '0');

*/