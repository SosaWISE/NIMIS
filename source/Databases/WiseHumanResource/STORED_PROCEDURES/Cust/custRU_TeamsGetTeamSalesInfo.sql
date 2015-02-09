USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetTeamSalesInfo')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetTeamSalesInfo'
		DROP  Procedure  dbo.custRU_TeamsGetTeamSalesInfo
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetTeamSalesInfo'
GO
/******************************************************************************
**		File: custRU_TeamsGetTeamSalesInfo.sql
**		Name: custRU_TeamsGetTeamSalesInfo
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: 
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:				Description:
**	-----------	-------------		-------------------------------------------
**	12/05/2013	Carly Christiansen	Created by
*******************************************************************************/
CREATE Procedure dbo.custRU_TeamsGetTeamSalesInfo
(
	@TeamID INT
	, @RoleLocationID INT = 1 --This determines sales and installs. Not UserTypes
	, @HasOwnTeam BIT = NULL -- (NULL, 0, 1)
	, @DeletionStatus VARCHAR(10) = NULL -- ('ALL', 'Deleted', NULL/'NotDeleted')
)
AS
BEGIN
	--DECLARE @TeamID INT
	--SET @TeamID = 36

	--------------------------------------------------------------------------------
	-- Teams
	DECLARE @Teams TABLE
	(
		TeamID INT
		, Description VARCHAR(50)
		, TeamLocationID INT
	)
	INSERT INTO @Teams
	SELECT
		RT.TeamID
		, RT.Description
		, RT.TeamLocationID

	FROM RU_Teams AS RT
	WHERE
		RT.TeamID = @TeamID

	DECLARE @SeasonID INT
	SELECT
		@SeasonID = SeasonID
	FROM RU_Teams AS RT
	INNER JOIN RU_TeamLocations AS RTL
	ON
		RT.TeamLocationID = RTL.TeamLocationID
	WHERE
		RT.TeamID = @TeamID

	--Don't filter Accounts by TeamID, only by SeasonID. Filtering on TeamID will exclude some accounts
	--------------------------------------------------------------------------------
	-- Accounts
	DECLARE @Accounts TABLE
	(
		AccountID INT
		, GPEmployeeID NVARCHAR(25)
		, InstallDate DATETIME
	)
	INSERT INTO @Accounts
	SELECT
		MSA.AccountID
		, CASE
			  WHEN @RoleLocationID = 1 THEN MSA.GPSalesRepID --Sales
			  WHEN @RoleLocationID = 2 THEN MSA.GPTechnicianID --Installs
			  ELSE MSA.GPSalesRepID
			END
		, ST.InstallDate
	FROM Platinum_Protection_InterimCRM.dbo.MS_Account AS MSA WITH (NOLOCK)
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_AccountStatus AS ST WITH (NOLOCK)
	ON
		MSA.AccountID = ST.AccountID
	INNER JOIN RU_TeamLocations AS TeamLocs
	ON
		MSA.TeamLocationID = TeamLocs.TeamLocationID
		AND TeamLocs.SeasonID = @SeasonID -- Filter TeamLocation By Season
	LEFT OUTER JOIN Platinum_Protection_InterimCRM.dbo.MS_AccountStatus AS MSAS WITH (NOLOCK) -- Only has an account status if cancelling, etc.
	ON
		MSA.AccountID = MSAS.AccountID
		AND MSAS.NOCDate IS NULL
		AND MSAS.CancelDate IS NULL
	WHERE
		ST.InstallDate IS NOT NULL

	--------------------------------------------------------------------------------
	-- @Members
	DECLARE @Members TABLE
	(
		TeamID INT
		, TeamLocationID INT
		
		, RecruitID INT
		, UserID INT
		, UserTypeId INT
		, ReportsToID INT
		, SeasonID INT
		, PayScaleID INT
		, IsDeleted BIT
		, IsRecruiter INT
		, GPEmployeeID NVARCHAR(25)

		, HasOwnTeam BIT
	)
	INSERT INTO @Members
	SELECT
		Tree.TeamID
		, RT.TeamLocationID
	
		, Tree.RecruitID
		, Tree.UserID
		, Tree.UserTypeId
		, Tree.ReportsToID
		, Tree.SeasonID
		, RR.PayScaleID
		, RU.IsDeleted
		, RR.IsRecruiter
		, RU.GPEmployeeID		

		, Tree.HasOwnTeam
	--Depending on @HasOwnTeam value, this query return Managers, Recruits, or Both
	FROM GetReportingTree('ReportingLevel', NULL, @HasOwnTeam, @TeamID, NULL) AS Tree  -- ('ReportingLevel', NULL-Top Recruiting Level, HasOwnTeam, TeamID, SeasonID)
	INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
	ON
		Tree.RecruitID = RR.RecruitID
	INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
	ON
		(RR.UserTypeID = RUT.UserTypeID)
	INNER JOIN RU_Users AS RU WITH (NOLOCK)
	ON
		RR.UserID = RU.UserID
	INNER JOIN RU_Teams AS RT WITH (NOLOCK)
	ON
		Tree.TeamID = RT.TeamID

	--------------------------------------------------------------------------------
	-- Member Numbers
	DECLARE @MemberNumbers TABLE
	(
		TeamID INT
		, RecruitID INT
		, InstallDate DATETIME
	)
	INSERT INTO @MemberNumbers
	SELECT
		Members.TeamID
		, Members.RecruitID
		, MSA.InstallDate
	FROM @Members AS Members
	INNER JOIN @Accounts AS MSA
	ON
		Members.GPEmployeeID = MSA.GPEmployeeID
	ORDER BY
		TeamID
		, RecruitID

	--SELECT * FROM @Members

	--------------------------------------------------------------------------------
	--	Select out the info we want from the previous tables
	SELECT
		Teams.TeamID
		, Teams.Description AS TeamDescription

		, RUTL.TeamLocationID
		, RUTL.SeasonID
		, RUTL.CRMTerritoryGuid
		, RUTL.MarketID
		, RUTL.Description AS TeamLocationDescription
		, RUTL.City

		, STATES.StateAbbreviation

		, ISNULL(TotalTeamMembersTable.Num, 0) AS TotalRecruits
		, ISNULL(MemberNumsTable.Num, 0) AS TotalOfficeNums
		, ISNULL(NumRepsWithNumsTable.Num, 0) AS NumRepsWithNums

	FROM @Teams AS Teams

	--------------------------------------------------------------------------------
	--  Get Team Info
	INNER JOIN RU_TeamLocations AS RUTL WITH (NOLOCK)
	ON
		(Teams.TeamLocationID = RUTL.TeamLocationID)
	INNER JOIN MC_PoliticalStates AS STATES
	ON
		RUTL.StateID = STATES.StateID

	--------------------------------------------------------------------------------
	-- Members - Total
	LEFT OUTER JOIN
		(SELECT
			TeamID
			, ISNULL(COUNT(*), 0) AS Num
		FROM @Members
		WHERE
			(IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus)))
		GROUP BY
			TeamID
		) AS TotalTeamMembersTable
	ON
		Teams.TeamID = TotalTeamMembersTable.TeamID

	--------------------------------------------------------------------------------
	-- Nums - Personal
	LEFT OUTER JOIN
		(SELECT
			TeamID
			, ISNULL(COUNT(*), 0) AS Num
		FROM @MemberNumbers
		WHERE
			(InstallDate IS NOT NULL)
		GROUP BY
			TeamID
		) AS MemberNumsTable
	ON
		Teams.TeamID = MemberNumsTable.TeamID

	--------------------------------------------------------------------------------
	-- Number of Reps with Nums
	LEFT OUTER JOIN
		(SELECT
			TeamID
			, ISNULL(COUNT(*), 0) AS Num
		FROM @Members
		WHERE
			(RecruitID IN
				(
					SELECT RecruitID FROM @MemberNumbers
					WHERE
						HasOwnTeam = 0
				)
			)
			AND IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus))
		GROUP BY
			TeamID
		) AS NumRepsWithNumsTable
	ON
		Teams.TeamID = NumRepsWithNumsTable.TeamID


--
--	SELECT
--		RUT.TeamID
--		, RUT.Description AS TeamDescription
--
--		, RUTL.TeamLocationID
--		, RUTL.SeasonID
--		, RUTL.CRMTerritoryGuid
--		, RUTL.MarketID
--		, RUTL.Description AS TeamLocationDescription
--		, RUTL.City
--
--		, STATES.StateAbbreviation
--	FROM RU_Teams AS RUT WITH (NOLOCK)
--		INNER JOIN RU_TeamLocations AS RUTL WITH (NOLOCK)
--		ON
--			RUT.TeamLocationID = RUTL.TeamLocationID
--		INNER JOIN MC_PoliticalStates AS STATES
--		ON
--			RUTL.StateID = STATES.StateID
--	WHERE
--		(RUT.IsActive = 1)
--		AND (RUT.IsDeleted = 0)
--		AND (RUT.TeamID = @TeamID)
--	ORDER BY
--		RUT.Description ASC
END
GO

GRANT EXEC ON dbo.custRU_TeamsGetTeamSalesInfo TO PUBLIC
GO