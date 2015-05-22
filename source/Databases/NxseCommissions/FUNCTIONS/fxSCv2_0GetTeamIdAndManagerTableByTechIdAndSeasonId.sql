USE [NXSE_Commissions]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxSCv2_0GetTeamIdAndManagerTableByTechIdAndSeasonId')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv2_0GetTeamIdAndManagerTableByTechIdAndSeasonId'
		DROP FUNCTION  dbo.fxSCv2_0GetTeamIdAndManagerTableByTechIdAndSeasonId
	END
GO

PRINT 'Creating FUNCTION fxSCv2_0GetTeamIdAndManagerTableByTechIdAndSeasonId'
GO
/******************************************************************************
**		File: fxSCv2_0GetTeamIdAndManagerTableByTechIdAndSeasonId.sql
**		Name: fxSCv2_0GetTeamIdAndManagerTableByTechIdAndSeasonId
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
**		Date: 05/20/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	05/20/2015	Peter Fry	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCv2_0GetTeamIdAndManagerTableByTechIdAndSeasonId
(
	@TechId VARCHAR(25)
	, @SeasonId INT
)
RETURNS 
@ResultsTable TABLE
(
	TeamID INT, TeamName VARCHAR(50), ManTechId VARCHAR(25), ManTechFullName VARCHAR(100), TechID VARCHAR(25), TechFullName VARCHAR(150)
)
AS
BEGIN

	/** Return Table */
	DECLARE @TeamsTable TABLE (TeamID INT, TeamName VARCHAR(50), ManTechId VARCHAR(25), ManTechFullName VARCHAR(100));

	/** LOCALS */
	--PRINT 'INSERT INTO @TeamsTable';
	INSERT INTO @TeamsTable (TeamID, TeamName, ManTechId, ManTechFullName)
	SELECT DISTINCT
		RUR.TeamId
		, RUT.Description AS TeamName
		, RU.GPEmployeeId AS ManTechId
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
			AND (RUUTTT.[Description] = 'Tech Team Manager')  -- 7 : Tech Team Manager in [dbo].[RU_UserTypeTeamTypes]


	/** Check to see if the rep in question is a manager. */
	IF(EXISTS(SELECT * FROM @TeamsTable WHERE (ManTechId = @TechId)))
	BEGIN
		INSERT INTO @ResultsTable (
			TeamID ,
			TeamName ,
			ManTechId ,
			ManTechFullName ,
			TechID ,
			TechFullName
		)
			SELECT 
				TeamID
				, TeamName
				, ManTechId
				, ManTechFullName
				, @TechId -- varchar(25)
				, ManTechFullName -- varchar(150)
			FROM
				@TeamsTable 
			WHERE
				(ManTechId = @TechId);

		RETURN;
	END

	--PRINT 'UPDATE INTO @TeamsTable';
	--UPDATE RLT SET
	INSERT INTO @ResultsTable
			( TeamID ,
			  TeamName ,
			  ManTechId ,
			  ManTechFullName ,
			  TechID ,
			  TechFullName
			)
	SELECT TOP 1
		RUR.TeamId
		, RUT.Description AS TeamName
		, RLT.ManTechId
		, RLT.ManTechFullName
		, RU.GPEmployeeId AS TechID
		, RU.FullName AS TechFullName
	FROM
		@TeamsTable AS RLT
		INNER JOIN [WISE_HumanResource].[dbo].[RU_Teams] AS RUT WITH (NOLOCK)
		ON
			(RUT.TeamID = RLT.TeamID)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_Recruits] AS RUR WITH (NOLOCK)
		ON
			(RUR.TeamId = RUT.TeamID)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
		ON
			(RUR.UserId = RU.UserID)
			AND (RU.GPEmployeeId = @TechId)
			AND (RUR.SeasonId = @SeasonId)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_UserType] AS RUUT WITH (NOLOCK)
		ON
			(RUUT.UserTypeID = RUR.UserTypeId)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_UserTypeTeamTypes] AS RUUTTT WITH (NOLOCK)
		ON
			(RUUTTT.UserTypeTeamTypeID = RUUT.UserTypeTeamTypeID)
			AND (RUUTTT.[Description] = 'Tech Team Member')  -- 4 : Tech Team Member in [dbo].[RU_UserTypeTeamTypes]
	RETURN;
END
GO
--SELECT * FROM dbo.fxSCv2_0GetTeamIdAndManagerTableByTechIdAndSeasonId('MILLB001', 2);