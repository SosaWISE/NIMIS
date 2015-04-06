USE [NXSE_Licensing]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetBindMessageTokensTo')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetBindMessageTokensTo'
		DROP FUNCTION  dbo.fxGetBindMessageTokensTo
	END
GO

PRINT 'Creating FUNCTION fxGetBindMessageTokensTo'
GO
/******************************************************************************
**		File: fxGetBindMessageTokensTo.sql
**		Name: fxGetBindMessageTokensTo
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
**		Date: 04/02/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/02/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetBindMessageTokensTo
(
	@CallCenterMessage NVARCHAR(MAX)
)
RETURNS VARCHAR(MAX)
AS
BEGIN
	/** Declarations */
	DECLARE @Result NVARCHAR(MAX)
		, @MessageTokenID VARCHAR(50)
		, @Replacement VARCHAR(50);
	DECLARE MessageToken_cursor CURSOR 
		FOR SELECT MessageTokenID, Replacement FROM [dbo].[LM_MessageTokens];

	/** Initialize */
	SET @Result = @CallCenterMessage;

	OPEN MessageToken_cursor;

	FETCH NEXT FROM MessageToken_cursor
	INTO @MessageTokenID, @Replacement;

	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		/** Execute actions. */
		SELECT @Result = REPLACE (@Result, @MessageTokenID, @Replacement);

		/** Get next items */
		FETCH NEXT FROM MessageToken_cursor
		INTO @MessageTokenID, @Replacement;
	END

	CLOSE MessageToken_cursor;
	DEALLOCATE MessageToken_cursor;

	/** Return result*/
	RETURN @Result;
END
GO