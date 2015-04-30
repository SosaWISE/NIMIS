USE [NXSE_Commissions]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxSCv2_0GetManagerBySalesRepIdAndSeasonId')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv2_0GetManagerBySalesRepIdAndSeasonId'
		DROP FUNCTION  dbo.fxSCv2_0GetManagerBySalesRepIdAndSeasonId
	END
GO

PRINT 'Creating FUNCTION fxSCv2_0GetManagerBySalesRepIdAndSeasonId'
GO
/******************************************************************************
**		File: fxSCv2_0GetManagerBySalesRepIdAndSeasonId.sql
**		Name: fxSCv2_0GetManagerBySalesRepIdAndSeasonId
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
**		Auth: Andr�s E. Sosa
**		Date: 04/30/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/30/2015	Andr�s E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCv2_0GetManagerBySalesRepIdAndSeasonId
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

SELECT dbo.fxSCv2_0GetManagerBySalesRepIdAndSeasonId('EDOUE001', 4) AS ManSalesRepId;