USE [WISE_HumanResource]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type='FN') AND name = 'fxRU_SeasonCreditGroup')
	BEGIN
		PRINT 'Dropping FUNCTION fxRU_SeasonCreditGroup'
		DROP FUNCTION  dbo.fxRU_SeasonCreditGroup
	END
GO

PRINT 'Creating FUNCTION fxRU_SeasonCreditGroup'
GO
/******************************************************************************
**		File: fxRU_SeasonCreditGroup.sql
**		Name: fxRU_SeasonCreditGroup
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
**		Date: 05/06/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	05/06/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxRU_SeasonCreditGroup
(
	@SeasonId INT
	, @Score INT
)
RETURNS VARCHAR(20)
AS
BEGIN
	/** DECLARATIONS. */
	DECLARE @CreditGroup varchar(20) = '[NOT FOUND]';

	SELECT 
		@CreditGroup = CASE
			WHEN @Score >= ExcellentCreditScoreThreshold THEN 'EXCELLENT'
			WHEN @Score BETWEEN ExcellentCreditScoreThreshold AND PassCreditScoreThreshold THEN 'GOOD'
			WHEN @Score BETWEEN PassCreditScoreThreshold AND SubCreditScoreThreshold THEN 'PASS'
			WHEN @Score <= SubCreditScoreThreshold THEN 'BAD'
			ELSE 'BAD'
		END
	FROM
		[dbo].RU_Season WHERE (SeasonID = @SeasonId);

	/** Return */
	RETURN @CreditGroup;
END
GO

/** Test */

SELECT [dbo].fxRU_SeasonCreditGroup(1, 750) AS [Group];
