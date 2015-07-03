USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetMS_AccountHoldsCountByAccountId')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetMS_AccountHoldsCountByAccountId'
		DROP FUNCTION  dbo.fxGetMS_AccountHoldsCountByAccountId
	END
GO

PRINT 'Creating FUNCTION fxGetMS_AccountHoldsCountByAccountId'
GO
/******************************************************************************
**		File: fxGetMS_AccountHoldsCountByAccountId.sql
**		Name: fxGetMS_AccountHoldsCountByAccountId
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
**		Date: 07/02/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	07/02/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetMS_AccountHoldsCountByAccountId
(
	@AccountID BIGINT
)
RETURNS INT
AS
BEGIN
	/** Declarations */
	DECLARE @Score INT;

	/** Execute actions. */
	SELECT @Score = COUNT(*) FROM dbo.fxGetMS_AccountHoldsTableByAccountId(@AccountID) AS HLDS WHERE (HLDS.FixedOn IS NULL);


	RETURN @Score;
END
GO