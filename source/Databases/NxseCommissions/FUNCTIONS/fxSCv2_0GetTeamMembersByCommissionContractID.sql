USE [NXSE_Commissions]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxSCv2_0GetTeamMembersByCommissionContractID')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv2_0GetTeamMembersByCommissionContractID'
		DROP FUNCTION  dbo.fxSCv2_0GetTeamMembersByCommissionContractID
	END
GO

PRINT 'Creating FUNCTION fxSCv2_0GetTeamMembersByCommissionContractID'
GO
/******************************************************************************
**		File: fxSCv2_0GetTeamMembersByCommissionContractID.sql
**		Name: fxSCv2_0GetTeamMembersByCommissionContractID
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
**		Date: 04/26/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/26/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCv2_0GetTeamMembersByCommissionContractID
(
	@CommissionContractID INT = 1
	, @IncludeManagers BIT = 1
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
		INNER JOIN [dbo].[SC_CommissionContracts] AS SCCC WITH (NOLOCK)
		ON
			(SCCC.SeasonId = RUR.SeasonId)
			AND (SCCC.CommissionContractID = @CommissionContractID)

	/** Add Managers to the list if the flag is active. */
	IF (@IncludeManagers = 'TRUE')
	BEGIN
		INSERT INTO @ResultsTable (
			TeamID
			, TeamName 
			, ManSalesRepId 
			, ManSalesRepFullName 
			, SalesRepId 
			, SalesRepFullName
		)
		SELECT 
			TeamID
			, TeamName 
			, ManSalesRepId 
			, ManSalesRepFullName 
			, ManSalesRepId AS SalesRepId 
			, ManSalesRepFullName AS SalesRepFullName
		FROM @TeamsTable
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
	SELECT
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
		INNER JOIN [dbo].[SC_CommissionContracts] AS SCCC WITH (NOLOCK)
		ON
			(SCCC.SeasonId = RUR.SeasonId)
			AND (SCCC.CommissionContractID = @CommissionContractID)
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
/**
SELECT * FROM [dbo].fxSCv2_0GetTeamMembersByCommissionContractID(1, 'TRUE') AS TMLI
*/