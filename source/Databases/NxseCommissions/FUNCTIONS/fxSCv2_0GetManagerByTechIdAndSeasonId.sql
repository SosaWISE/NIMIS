USE [NXSE_Commissions]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxSCv2_0GetManagerByTechIdAndSeasonId')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv2_0GetManagerByTechIdAndSeasonId'
		DROP FUNCTION  dbo.fxSCv2_0GetManagerByTechIdAndSeasonId
	END
GO

PRINT 'Creating FUNCTION fxSCv2_0GetManagerByTechIdAndSeasonId'
GO
/******************************************************************************
**		File: fxSCv2_0GetManagerByTechIdAndSeasonId.sql
**		Name: fxSCv2_0GetManagerByTechIdAndSeasonId
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
**		Auth: Peter Fry
**		Date: 04/30/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/30/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCv2_0GetManagerByTechIdAndSeasonId
(
	@TechId VARCHAR(25)
	, @SeasonId INT
)
RETURNS VARCHAR(25)
AS
BEGIN
	/** Declarations */
	DECLARE @ManTechId VARCHAR(25);

	/** Execute actions. */
	SELECT @ManTechId = ManSalesRepId FROM dbo.fxSCv2_0GetTeamIdAndManagerTableBySalesRepIdAndSeasonId(@TechId, @SeasonId);

	RETURN @ManTechId;
END
GO

--SELECT dbo.fxSCv2_0GetManagerByTechIdAndSeasonId('MILLB001', 2) AS ManTechId;