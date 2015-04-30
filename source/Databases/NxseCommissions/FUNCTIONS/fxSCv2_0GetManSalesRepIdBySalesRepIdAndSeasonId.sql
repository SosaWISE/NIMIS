USE [NXSE_Commissions]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxSCv2_0GetManSalesRepIdBySalesRepIdAndSeasonId')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv2_0GetManSalesRepIdBySalesRepIdAndSeasonId'
		DROP FUNCTION  dbo.fxSCv2_0GetManSalesRepIdBySalesRepIdAndSeasonId
	END
GO

PRINT 'Creating FUNCTION fxSCv2_0GetManSalesRepIdBySalesRepIdAndSeasonId'
GO
/******************************************************************************
**		File: fxSCv2_0GetManSalesRepIdBySalesRepIdAndSeasonId.sql
**		Name: fxSCv2_0GetManSalesRepIdBySalesRepIdAndSeasonId
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
**		Date: 04/29/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/29/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCv2_0GetManSalesRepIdBySalesRepIdAndSeasonId
(
	@SalesRepId VARCHAR(25)
	, @SeasonId INT
)
RETURNS VARCHAR(25)
AS
BEGIN
	/** Declarations */
	DECLARE @ManSalesRepId VARCHAR(25);

	/** Execute actions. */
	SELECT @ManSalesRepId = ManSalesRepId FROM dbo.fxSCv2_0GetTeamIdAndManagerTableBySalesRepIdAndSeasonId(@SalesRepId, @SeasonId);

	RETURN @ManSalesRepId;
END
GO

SELECT dbo.fxSCv2_0GetManSalesRepIdBySalesRepIdAndSeasonId('SHERJ001', 4) AS ManSalesRepId;