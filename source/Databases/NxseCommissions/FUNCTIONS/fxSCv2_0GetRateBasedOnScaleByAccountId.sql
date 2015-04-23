USE [NXSE_Commissions]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxSCv2_0GetRateBasedOnScaleByAccountId')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv2_0GetRateBasedOnScaleByAccountId'
		DROP FUNCTION  dbo.fxSCv2_0GetRateBasedOnScaleByAccountId
	END
GO

PRINT 'Creating FUNCTION fxSCv2_0GetRateBasedOnScaleByAccountId'
GO
/******************************************************************************
**		File: fxSCv2_0GetRateBasedOnScaleByAccountId.sql
**		Name: fxSCv2_0GetRateBasedOnScaleByAccountId
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
**		Date: 04/20/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/20/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCv2_0GetRateBasedOnScaleByAccountId
(
	@AccountID BIGINT
	, @NumThisWeek INT
)
RETURNS MONEY
AS
BEGIN
	/** Declarations */
	DECLARE @Rate MONEY = 550.00
		, @Deduction MONEY = 100.00;

	/** Execute actions. */
	SELECT @Rate = CASE
		WHEN @NumThisWeek <= 3  THEN 550.00
		WHEN @NumThisWeek <= 5  THEN 650.00
		WHEN @NumThisWeek <= 8  THEN 700.00
		WHEN @NumThisWeek <= 11 THEN 750.00
		WHEN @NumThisWeek >= 12 THEN 800.00
	  END;

	IF (@NumThisWeek < 3) SET @Rate = @Rate - @Deduction;

	RETURN @Rate;
END
GO

/* SELECT dbo.fxSCv2_0GetRateBasedOnScaleByAccountId(23423, 13)*/