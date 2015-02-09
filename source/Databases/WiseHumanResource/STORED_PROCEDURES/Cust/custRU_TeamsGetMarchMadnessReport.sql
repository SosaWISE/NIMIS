USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetMarchMadnessReport')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetMarchMadnessReport'
		DROP  Procedure  dbo.custRU_TeamsGetMarchMadnessReport
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetMarchMadnessReport'
GO
/******************************************************************************
**		File: custRU_TeamsGetMarchMadnessReport.sql
**		Name: custRU_TeamsGetMarchMadnessReport
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
CREATE Procedure dbo.custRU_TeamsGetMarchMadnessReport
(
	@SeasonID INT
	, @RoleLocationID INT = 1
	, @StartDate DATETIME
	, @EndDate DATETIME
	, @PPNum INT --Points Per Num
	, @PPNewRecruit INT --Points Per Recruit
)
AS
BEGIN


--Rules
--1 point for each install 
--3 points for each signed recruit
--Total points will be added at the end of each round and then divided by the total number of signed reps in your office
--
--Tie Breaker
--The team with the fewest number of waived activations in each round will get the tie breaker. 
--If waived activations are the same then the team that had the most reps sell (including the managers) gets the tie breaker.
--If the number of reps is the same we will flip a coin.


--	DECLARE @SeasonID INT
--	SET @SeasonID = 6
--
--	DECLARE @RoleLocationID INT
--	SET @RoleLocationID = 1
--
--	DECLARE @StartDate DATETIME
--	SET @StartDate = '03-04-2008'
--
--	DECLARE @EndDate DATETIME
--	SET @EndDate = '03-05-2008'
--
--	DECLARE @PPNum INT
--	SET @PPNum = 1
--
--	DECLARE @PPNewRecruit INT
--	SET @PPNewRecruit = 3

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
		RUT.TeamID
		, RUT.Description
		, RUT.TeamLocationID
	FROM RU_Teams AS RUT
	INNER JOIN RU_TeamLocations AS RUTL WITH (NOLOCK)
	ON
		RUT.TeamLocationID = RUTL.TeamLocationID
		AND (RUTL.SeasonID = @SeasonID) -- Filter Team By Season
	WHERE
		((RUT.IsDeleted = 0))-- AND (RUT.IsActive = 1))
		AND ((RUTL.IsDeleted = 0))-- AND (RUTL.IsActive = 1))
		--HACK for: Sales Team/Tech Team
		AND
		(
			(@RoleLocationID = 1 AND RUT.Description NOT LIKE 'TECH%')
			OR
			(@RoleLocationID = 2 AND RUT.Description LIKE 'TECH%')
		)
	ORDER BY
		RUT.TeamID

	--------------------------------------------------------------------------------
	-- Accounts
	DECLARE @Accounts TABLE
	(
		AccountID INT
		, GPEmployeeID NVARCHAR(25)
		, InstallDate DATETIME
		, ActivationFee MONEY
	)
	INSERT INTO @Accounts
	SELECT
		MSA.AccountID
		, CASE
			  WHEN @RoleLocationID = 1 THEN MSA.GPSalesRepID
			  WHEN @RoleLocationID = 2 THEN MSA.GPTechnicianID
			  ELSE MSA.GPSalesRepID
			END
		, ST.InstallDate
		, MSA.ActivationFee
	FROM Platinum_Protection_InterimCRM.dbo.MS_Account AS MSA WITH (NOLOCK)
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_AccountStatus AS ST WITH (NOLOCK)
	ON
		MSA.AccountID = ST.AccountID
	INNER JOIN RU_TeamLocations AS TeamLocs
	ON
		MSA.TeamLocationID = TeamLocs.TeamLocationID
		AND TeamLocs.SeasonID = @SeasonID -- Filter TeamLocation By Season
	LEFT OUTER JOIN Platinum_Protection_InterimCRM.dbo.MS_AccountStatus AS MSAS WITH (NOLOCK)
	ON
		MSA.AccountID = MSAS.AccountID
		AND MSAS.NOCDate IS NULL
		AND MSAS.CancelDate IS NULL
	WHERE
		ST.InstallDate IS NOT NULL
		--AND ((@StartDate <= ST.InstallDate) AND (ST.InstallDate <= @EndDate))
		AND (ST.InstallDate BETWEEN @StartDate AND @EndDate)

	--------------------------------------------------------------------------------
	-- Team Managers
	DECLARE @Managers TABLE
	(
		TeamID INT
		, TeamLocationID INT
		
		, RecruitID INT
		, UserID INT
		, UserTypeId INT
		, ReportsToID INT
		, SeasonID INT
		, PayScaleID INT
		, IsRecruiter INT
		, GPEmployeeID NVARCHAR(25)

		, IsTeamMember BIT
	)
	INSERT INTO @Managers
	SELECT MAN.* FROM GetAllManagers(NULL, @SeasonID, 'NotDeleted') AS MAN
	INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
	ON
		(MAN.RecruitID = RR.RecruitID)
	INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
	ON
		(RR.UserTypeID = RUT.UserTypeID)
	WHERE
		(MAN.TeamID IN (SELECT TeamID FROM @Teams))
		AND (RUT.RoleLocationID = @RoleLocationID)

--	SELECT * FROM @Managers

	--------------------------------------------------------------------------------
	-- Team Members
	DECLARE @Members TABLE
	(
		TeamID INT
		, RecruitedByID INT
		, RecruitID INT
		, UserTypeID INT
		, ReportsToID INT
		, SeasonID INT
		, PayScaleID INT
		, GPEmployeeID NVARCHAR(25)
		, RecruitedDate DATETIME
	)
	INSERT INTO @Members
	SELECT
		MAN.TeamID
		, RU.RecruitedByID
		, RR.RecruitID
		, RR.UserTypeID
		, RR.ReportsToID
		, RR.SeasonID
		, RR.PayScaleID
		, RU.GPEmployeeID
		, RU.RecruitedDate
	FROM RU_Recruits AS RR WITH (NOLOCK)
	INNER JOIN @Managers AS MAN
	ON
		RR.ReportsToID = MAN.RecruitID
	INNER JOIN RU_Users AS RU WITH (NOLOCK)
	ON
		RR.UserID = RU.UserID	
	INNER JOIN RU_UserType AS RUT WITH (NOLOCK)
	ON
		RR.UserTypeID = RUT.UserTypeID
		AND (RUT.RoleLocationID = @RoleLocationID)
	INNER JOIN GetUserTypeTeamTypes(2) AS Recruits -- 2 - Team Members
	ON
		RUT.UserTypeTeamTypeID = Recruits.UserTypeTeamTypeID
	WHERE
		(RR.IsDeleted = 0)
		AND (RR.SeasonID = @SeasonID)
	ORDER BY
		RR.RecruitID

	--------------------------------------------------------------------------------
	-- Manager Numbers
	DECLARE @ManagerNumbers TABLE
	(
		TeamID INT
		, RecruitID INT
		, InstallDate DATETIME
		, ActivationFee MONEY
	)
	INSERT INTO @ManagerNumbers
	SELECT
		Manager.TeamID
		, Manager.RecruitID
		, MSA.InstallDate
		, MSA.ActivationFee
	FROM @Managers AS Manager
	INNER JOIN @Accounts AS MSA
	ON
		Manager.GPEmployeeID = MSA.GPEmployeeID
	ORDER BY
		TeamID
		, RecruitID

	--------------------------------------------------------------------------------
	-- Rep Numbers
	DECLARE @RepNumbers TABLE
	(
		TeamID INT
		, ReportsToID INT
		, RecruitID INT
		, InstallDate DATETIME
		, ActivationFee MONEY
	)
	INSERT INTO @RepNumbers
	SELECT
		Reps.TeamID
		, Reps.ReportsToID
		, Reps.RecruitID
		, InstallDate
		, MSA.ActivationFee
	FROM @Members AS Reps
	INNER JOIN @Accounts AS MSA
	ON
		Reps.GPEmployeeID = MSA.GPEmployeeID
	ORDER BY
		TeamID
		, ReportsToID
		, Reps.RecruitID


	--------------------------------------------------------------------------------
	-- WholeTeam
	DECLARE @WholeTeam TABLE
	(
		RecruitID INT
		, TeamID INT
		, PayScaleID INT
		, RecruitedDate DATETIME
		, UserTypeID INT
	)
	INSERT INTO @WholeTeam
	SELECT
		RecruitID
		, TeamID
		, PayScaleID
		, RecruitedDate
		, UserTypeID
	FROM
	(
		SELECT
			RecruitID
			, TeamID
			, PayScaleID
			, RecruitedDate
			, UserTypeID
		FROM @Members AS Recruits

		UNION ALL

		SELECT
			RecruitID
			, TeamID
			, PayScaleID
			, RecruitedDate
			, UserTypeID
		FROM @Managers AS Managers
		INNER JOIN RU_Users AS RU WITH (NOLOCK)
		ON
			Managers.UserID = RU.UserID
	) AS WholeTeam


	--------------------------------------------------------------------------------
	--	Select out the info we want from the previous tables
	SELECT
		Teams.TeamID
		, Teams.Description AS TeamName

		--Total points will be added at the end of each round and then divided by the total number of signed reps in your office
		, ISNULL(TotalRepsTable.Num, 0) AS TotalReps

		--1 point for each install 

		, ISNULL(NewRecruitsTable.Num, 0) AS NewRecruits
		, ISNULL(NewRecruitsTable.Num, 0) * @PPNewRecruit AS NewRecruitPoints

		----???????????????
		--, ISNULL(PersonalNumsTable.Num, 0) AS PersonalNums
		--, ISNULL(TeamNumsTable.Num, 0) AS TeamNums
		----???????????????

		--3 points for each signed recruit
		, ISNULL(PersonalNumsTable.Num, 0) + ISNULL(TeamNumsTable.Num, 0) AS TotalOfficeNums
		, (ISNULL(PersonalNumsTable.Num, 0) + ISNULL(TeamNumsTable.Num, 0)) * @PPNum AS NumsPoints

		, ((ISNULL(NewRecruitsTable.Num, 0) * @PPNewRecruit)
		+ (ISNULL(PersonalNumsTable.Num, 0) + ISNULL(TeamNumsTable.Num, 0)) * @PPNum)
			 AS TotalPoints

		, CASE
			WHEN TotalRepsTable.Num IS NULL OR TotalRepsTable.Num = 0 THEN 0
			ELSE CONVERT(DECIMAL(9,4),
				((ISNULL(NewRecruitsTable.Num, 0) * @PPNewRecruit)
				+ (ISNULL(PersonalNumsTable.Num, 0) + ISNULL(TeamNumsTable.Num, 0)) * @PPNum)
				/CONVERT(DECIMAL(9,4),
						TotalRepsTable.Num))
		END AS TotalPointsPerRep

		, (ISNULL(PersonalAcctWaives.Num, 0) + ISNULL(TeamAcctWaives.Num, 0)) AS WaivedActivations
		, ISNULL(NumRepsWithNumsTable.Num, 0) AS NumRepsWithNums


	FROM @Teams AS Teams

	--------------------------------------------------------------------------------
	-- Members - Total
	LEFT OUTER JOIN
		(SELECT
			TeamID
			, ISNULL(COUNT(*), 0) AS Num
		FROM @WholeTeam
		GROUP BY
			TeamID
		) AS TotalRepsTable
	ON
		Teams.TeamID = TotalRepsTable.TeamID

	--------------------------------------------------------------------------------
	-- Recruits - Last 7 Days Recruits
	LEFT OUTER JOIN
		(SELECT
			Recruits.TeamID
			, ISNULL(COUNT(*), 0) AS Num
		FROM @Members AS Recruits
		WHERE
			(Recruits.RecruitedDate BETWEEN @StartDate AND @EndDate)
		GROUP BY
			Recruits.TeamID
		) AS NewRecruitsTable
	ON
		Teams.TeamID = NewRecruitsTable.TeamID

	--------------------------------------------------------------------------------
	-- Nums - Last 7 Days
	LEFT OUTER JOIN
		(SELECT
			TeamID
			, ISNULL(COUNT(*), 0) AS Num
		FROM @ManagerNumbers
		WHERE
			InstallDate > GETDATE()-7
		GROUP BY
			TeamID
		) AS Last7DaysNumsTable
	ON
		Teams.TeamID = Last7DaysNumsTable.TeamID

	--------------------------------------------------------------------------------
	-- Nums - Last 30 Days
	LEFT OUTER JOIN
		(SELECT
			TeamID
			, ISNULL(COUNT(*), 0) AS Num
		FROM @ManagerNumbers
		WHERE
			InstallDate > GETDATE()-30
		GROUP BY
			TeamID
		) AS Last30DaysNumsTable
	ON
		Teams.TeamID = Last30DaysNumsTable.TeamID

	--------------------------------------------------------------------------------
	-- Nums - Personal
	LEFT OUTER JOIN
		(SELECT
			TeamID
			, ISNULL(COUNT(*), 0) AS Num
		FROM @ManagerNumbers
		WHERE
			InstallDate IS NOT NULL
		GROUP BY
			TeamID
		) AS PersonalNumsTable
	ON
		Teams.TeamID = PersonalNumsTable.TeamID

	--------------------------------------------------------------------------------
	-- Team Nums
	LEFT OUTER JOIN
		(SELECT
			TeamID
			, ISNULL(COUNT(*), 0) AS Num
		FROM @RepNumbers
		WHERE
			InstallDate IS NOT NULL
		GROUP BY
			TeamID
		) AS TeamNumsTable
	ON
		Teams.TeamID = TeamNumsTable.TeamID

	--------------------------------------------------------------------------------
	-- Personal Account Waives
	LEFT OUTER JOIN
		(SELECT
			TeamID
			, ISNULL(COUNT(*), 0) AS Num
		FROM @ManagerNumbers
		WHERE
			(InstallDate IS NOT NULL)
			AND (ActivationFee = 0)
		GROUP BY
			TeamID
		) AS PersonalAcctWaives
	ON
		Teams.TeamID = PersonalAcctWaives.TeamID

	--------------------------------------------------------------------------------
	-- Team Account Waives
	LEFT OUTER JOIN
		(SELECT
			TeamID
			, ISNULL(COUNT(*), 0) AS Num
		FROM @RepNumbers
		WHERE
			(InstallDate IS NOT NULL)
			AND (ActivationFee = 0)
		GROUP BY
			TeamID
		) AS TeamAcctWaives
	ON
		Teams.TeamID = TeamAcctWaives.TeamID

	--------------------------------------------------------------------------------
	-- Number of Reps with Nums
	LEFT OUTER JOIN
		(SELECT
			TeamID
			, ISNULL(COUNT(*), 0) AS Num
		FROM @Members
		WHERE
			RecruitID IN (SELECT RecruitID FROM @RepNumbers)
		GROUP BY
			TeamID
		) AS NumRepsWithNumsTable
	ON
		Teams.TeamID = NumRepsWithNumsTable.TeamID


----Order By
	ORDER BY
		TotalPointsPerRep DESC -- Total points will be added at the end of each round and then divided by the total number of signed reps in your office
		, WaivedActivations ASC -- The team with the fewest number of waived activations in each round will get the tie breaker.
		, NumRepsWithNums DESC -- If waived activations are the same then the team that had the most reps sell (including the managers) gets the tie breaker
----Order By


END
GO

GRANT EXEC ON dbo.custRU_TeamsGetMarchMadnessReport TO PUBLIC
GO