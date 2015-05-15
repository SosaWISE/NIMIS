USE [NXSE_Commissions]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxSCv3_0GetManagersBySeasonId')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv3_0GetManagersBySeasonId'
		DROP FUNCTION  dbo.fxSCv3_0GetManagersBySeasonId
	END
GO

PRINT 'Creating FUNCTION fxSCv3_0GetManagersBySeasonId'
GO
/******************************************************************************
**		File: fxSCv3_0GetManagersBySeasonId.sql
**		Name: fxSCv3_0GetManagersBySeasonId
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
**		Date: 05/15/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	05/15/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCv3_0GetManagersBySeasonId
(
	@SeasonId INT
)
RETURNS 
@ResultsTable TABLE
(
	TeamID INT, TeamName VARCHAR(50), ManSalesRepId VARCHAR(25), ManSalesRepFullName VARCHAR(100)
)
AS
BEGIN
	/** LOCALS */
	--PRINT 'INSERT INTO @TeamsTable';
	INSERT INTO @ResultsTable (TeamID, TeamName, ManSalesRepId, ManSalesRepFullName)
	SELECT DISTINCT
		RUR.TeamId
		, RUT.Description AS TeamName
		, RU.GPEmployeeId AS ManSalesRepID
		, RU.FullName
	FROM
		[WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_Recruits] AS RUR WITH (NOLOCK)
		ON
			(RUR.UserId = RU.UserID)
			AND (RUR.SeasonId = @SeasonId)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_Teams] AS RUT WITH (NOLOCK)
		ON
			(RUT.TeamID = RUR.TeamId)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_UserType] AS RUUT WITH (NOLOCK)
		ON
			(RUUT.UserTypeID = RUR.UserTypeId)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_UserTypeTeamTypes] AS RUUTTT WITH (NOLOCK)
		ON
			(RUUTTT.UserTypeTeamTypeID = RUUT.UserTypeTeamTypeID)
			AND (RUUTTT.[Description] = 'Sales Team Manager')  -- 6 : Sales Team Manager in [dbo].[RU_UserTypeTeamTypes]
	RETURN;
END
GO
--SELECT * FROM dbo.fxSCv3_0GetManagersBySeasonId(4);