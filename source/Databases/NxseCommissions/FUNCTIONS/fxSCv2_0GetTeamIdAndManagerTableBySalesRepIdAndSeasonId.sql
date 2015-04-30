USE [NXSE_Commissions]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxSCv2_0GetTeamIdAndManagerTableBySalesRepIdAndSeasonId')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv2_0GetTeamIdAndManagerTableBySalesRepIdAndSeasonId'
		DROP FUNCTION  dbo.fxSCv2_0GetTeamIdAndManagerTableBySalesRepIdAndSeasonId
	END
GO

PRINT 'Creating FUNCTION fxSCv2_0GetTeamIdAndManagerTableBySalesRepIdAndSeasonId'
GO
/******************************************************************************
**		File: fxSCv2_0GetTeamIdAndManagerTableBySalesRepIdAndSeasonId.sql
**		Name: fxSCv2_0GetTeamIdAndManagerTableBySalesRepIdAndSeasonId
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
CREATE FUNCTION dbo.fxSCv2_0GetTeamIdAndManagerTableBySalesRepIdAndSeasonId
(
	@SalesRepId VARCHAR(25)
	, @SeasonId INT
)
RETURNS 
@ResultsTable TABLE
(
	TeamID INT, TeamName VARCHAR(50), ManSalesRepId VARCHAR(25), ManSalesRepFullName VARCHAR(100), SalesRepId VARCHAR(25), SalesRepFullName VARCHAR(150)
)
AS
BEGIN

	/** Return Table */
	DECLARE @TeamsTable TABLE (TeamID INT, TeamName VARCHAR(50), ManSalesRepId VARCHAR(25), ManSalesRepFullName VARCHAR(100));

	/** LOCALS */
	--PRINT 'INSERT INTO @TeamsTable';
	INSERT INTO @TeamsTable (TeamID, TeamName, ManSalesRepId, ManSalesRepFullName)
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


	/** Check to see if the rep in question is a manager. */
	IF(EXISTS(SELECT * FROM @TeamsTable WHERE (ManSalesRepId = @SalesRepId)))
	BEGIN
		INSERT INTO @ResultsTable (
			TeamID ,
			TeamName ,
			ManSalesRepId ,
			ManSalesRepFullName ,
			SalesRepId ,
			SalesRepFullName
		)
			SELECT 
				TeamID
				, TeamName
				, ManSalesRepId
				, ManSalesRepFullName
				, @SalesRepId -- varchar(25)
				, ManSalesRepFullName -- varchar(150)
			FROM
				@TeamsTable 
			WHERE
				(ManSalesRepId = @SalesRepId);

		RETURN;
	END

	--PRINT 'UPDATE INTO @TeamsTable';
	--UPDATE RLT SET
	INSERT INTO @ResultsTable
			( TeamID ,
			  TeamName ,
			  ManSalesRepId ,
			  ManSalesRepFullName ,
			  SalesRepId ,
			  SalesRepFullName
			)
	SELECT TOP 1
		RUR.TeamId
		, RUT.Description AS TeamName
		, RLT.ManSalesRepId
		, RLT.ManSalesRepFullName
		, RU.GPEmployeeId AS SalesRepID
		, RU.FullName AS SalesRepFullName
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
			AND (RU.GPEmployeeId = @SalesRepId)
			AND (RUR.SeasonId = @SeasonId)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_UserType] AS RUUT WITH (NOLOCK)
		ON
			(RUUT.UserTypeID = RUR.UserTypeId)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_UserTypeTeamTypes] AS RUUTTT WITH (NOLOCK)
		ON
			(RUUTTT.UserTypeTeamTypeID = RUUT.UserTypeTeamTypeID)
			AND (RUUTTT.[Description] = 'Sales Team Member')  -- 3 : Sales Team Member in [dbo].[RU_UserTypeTeamTypes]
	RETURN;
END
GO
--SELECT * FROM dbo.fxSCv2_0GetTeamIdAndManagerTableBySalesRepIdAndSeasonId('EDOUE001', 4);