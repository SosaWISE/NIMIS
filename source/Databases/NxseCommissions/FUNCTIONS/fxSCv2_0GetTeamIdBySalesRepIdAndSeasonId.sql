USE [NXSE_Commissions]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxSCv2_0GetTeamIdBySalesRepIdAndSeasonId')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv2_0GetTeamIdBySalesRepIdAndSeasonId'
		DROP FUNCTION  dbo.fxSCv2_0GetTeamIdBySalesRepIdAndSeasonId
	END
GO

PRINT 'Creating FUNCTION fxSCv2_0GetTeamIdBySalesRepIdAndSeasonId'
GO
/******************************************************************************
**		File: fxSCv2_0GetTeamIdBySalesRepIdAndSeasonId.sql
**		Name: fxSCv2_0GetTeamIdBySalesRepIdAndSeasonId
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
CREATE FUNCTION dbo.fxSCv2_0GetTeamIdBySalesRepIdAndSeasonId
(
	@SalesRepId VARCHAR(25)
	, @SeasonId INT
)
RETURNS INT
AS
BEGIN
	/** Declarations */
	DECLARE @TeamID INT;

	/** Execute actions. */
	SELECT @TeamID = TeamID FROM dbo.fxSCv2_0GetTeamIdAndManagerTableBySalesRepIdAndSeasonId(@SalesRepId, @SeasonId);

	RETURN @TeamID;
END
GO

SELECT dbo.fxSCv2_0GetTeamIdBySalesRepIdAndSeasonId('EDOUE001', 4) AS TeamID;