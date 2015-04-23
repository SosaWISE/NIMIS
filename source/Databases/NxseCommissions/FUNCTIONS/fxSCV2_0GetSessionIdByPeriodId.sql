USE [NXSE_Commissions]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxSCV2_0GetSessionIdByPeriodId')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCV2_0GetSessionIdByPeriodId'
		DROP FUNCTION  dbo.fxSCV2_0GetSessionIdByPeriodId
	END
GO

PRINT 'Creating FUNCTION fxSCV2_0GetSessionIdByPeriodId'
GO
/******************************************************************************
**		File: fxSCV2_0GetSessionIdByPeriodId.sql
**		Name: fxSCV2_0GetSessionIdByPeriodId
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
**		Date: 04/21/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/21/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCV2_0GetSessionIdByPeriodId
(
	@CommissionPeriodID INT
)
RETURNS 
@ParsedList table
(
	SeasonId INT
	, DealerId INT
)
AS
BEGIN
	INSERT INTO @ParsedList ( SeasonId, DealerId )
	SELECT
		SCCC.SeasonId
		, RUS.DealerId
	FROM
		[dbo].[SC_CommissionPeriods] AS SCCP WITH (NOLOCK)
		INNER JOIN [dbo].[SC_CommissionEngines] AS SCCE WITH (NOLOCK)
		ON
			(SCCP.CommissionEngineId = SCCE.CommissionEngineID)
		INNER JOIN [dbo].[SC_CommissionContracts] AS SCCC WITH (NOLOCK)
		ON
			(SCCE.CommissionEngineID = SCCC.CommissionEngineId)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_Season] AS RUS WITH (NOLOCK)
		ON
			(SCCC.SeasonId = RUS.SeasonID)
	WHERE
		(SCCP.CommissionPeriodID = @CommissionPeriodID);
	
	RETURN
END
GO

/** TEST */
SELECT * FROM dbo.fxSCV2_0GetSessionIdByPeriodId(1)