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
	@SalesRepId VARCHAR(25)
	, @SessionId INT
	, @NumThisWeek INT
)
RETURNS 
@ResultTable TABLE (
	CommissionRateScaleId VARCHAR(20)
	, CommissionAmount MONEY
)
AS
BEGIN
	/** Declarations */
	DECLARE @Rate MONEY = 550.00
	, @CommissionRateScaleTypeId VARCHAR(20);

	/** Find the type of rep this is */
	SELECT
		@CommissionRateScaleTypeId = CASE
			WHEN RUR.UserTypeId =  2 /**Sales Manager*/				THEN 'SALES_MAN'
			WHEN RUR.UserTypeId =  3 /**Sales Co-Manager*/			THEN 'SALES_MAN'
			WHEN RUR.UserTypeId =  5 /**Sales Rep*/					THEN 'SALES_REP'
			WHEN RUR.UserTypeId =  4 /**Sales Assistant Manager*/	THEN 'SALES_REP'
			WHEN RUR.UserTypeId = 11 /**Regional Manager - Sales*/	THEN 'SALES_MAN'
			WHEN RUR.UserTypeId = 18 /**Senior Regional - Sales*/	THEN 'SALES_MAN'
			WHEN RUR.UserTypeId = 19 /**National Regional - Sales*/ THEN 'SALES_MAN'
			ELSE 'SALES_REP'
		  END
	FROM
		[WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_Recruits] AS RUR WITH (NOLOCK)
		ON
			(RUR.UserId = RU.UserID)
	WHERE
		(RU.GPEmployeeId = @SalesRepId);
	
	INSERT INTO @ResultTable (
		CommissionRateScaleId ,
		CommissionAmount
	) SELECT 
			SCCRS.CommissionRateScaleId -- varchar(20)
			, SCCRS.CommissionAmt -- money
		FROM 
			[dbo].[SC_CommissionRateScales] AS SCCRS WITH (NOLOCK)
		WHERE
			(SCCRS.CommissionRateScaleTypeId = @CommissionRateScaleTypeId)
			AND (SCCRS.CommissionEngineId = 'SCv2.0')
			AND (@NumThisWeek BETWEEN SCCRS.Start AND SCCRS.[End]);

	RETURN;
END
GO

/* SELECT dbo.fxSCv2_0GetRateBasedOnScaleByAccountId(23423, 13)*/