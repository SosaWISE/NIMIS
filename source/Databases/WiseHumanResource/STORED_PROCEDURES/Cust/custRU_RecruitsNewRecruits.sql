USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsNewRecruits')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsNewRecruits'
		DROP  Procedure  dbo.custRU_RecruitsNewRecruits
	END
GO

PRINT 'Creating Procedure custRU_RecruitsNewRecruits'
GO
/******************************************************************************
**		File: custRU_RecruitsNewRecruits.sql
**		Name: custRU_RecruitsNewRecruits
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
CREATE Procedure dbo.custRU_RecruitsNewRecruits
(
	@StartDate DATETIME
	, @EndDate DATETIME
	, @UserTypeIDList NVARCHAR(MAX) = '5'
	, @SeasonIDList NVARCHAR(MAX)
)
AS
BEGIN

--	DECLARE @StartDate DATETIME
--	SET @StartDate = '4/5/2008 00:00:00.000'
--
--	DECLARE @EndDate DATETIME
--	SET @EndDate = '5/7/2008 23:59:59.997'
--
--	DECLARE @UserTypeIDList NVARCHAR(MAX)
--	SET @UserTypeIDList = '5'
--
--	DECLARE @SeasonIDList NVARCHAR(MAX)
--	SET @SeasonIDList = '7'

	--------------------------------------------------------------------------------
	-- Recruits
	DECLARE @TeamManagers TABLE
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

		, HasOwnTeam BIT
	)
	INSERT INTO @TeamManagers
	SELECT
		Tree.TeamID
		, RT.TeamLocationID

		, Tree.RecruitID
		, Tree.UserID
		, Tree.UserTypeId
		, Tree.ReportsToID
		, Tree.SeasonID
		, RR.PayScaleID
		, RR.IsRecruiter
		, RU.GPEmployeeID		

		, Tree.HasOwnTeam
	FROM GetReportingTree('ReportingLevel', NULL, 1, NULL, NULL) Tree  -- ('ReportingLevel', NULL-Top Recruiting Level, HasOwnTeam, TeamID, SeasonID)
	INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
	ON
		Tree.RecruitID = RR.RecruitID
	INNER JOIN SplitIntList(@SeasonIDList) AS SeasonIds
	ON
		RR.SeasonID = SeasonIds.ID
	INNER JOIN RU_Users AS RU WITH (NOLOCK)
	ON
		RR.UserID = RU.UserID
	INNER JOIN RU_Teams AS RT WITH (NOLOCK)
	ON
		Tree.TeamID = RT.TeamID


	DECLARE @Recruits TABLE
	(
		UserID INT
		, GPEmployeeID NVARCHAR(25)
		, FullName NVARCHAR(101)

		, CreatedDate DATETIME
		, RecruitID INT
		, ReportsToID INT
	)
	INSERT INTO @Recruits
	SELECT
		RU.UserID
		, RU.GPEmployeeID
		, RU.FullName

		, RR.CreatedDate
		, RR.RecruitID
		, RR.ReportsToID
	FROM RU_Users AS RU
	INNER JOIN RU_Recruits AS RR
	ON
		RU.UserID = RR.UserID
	INNER JOIN SplitIntList(@UserTypeIDList) AS Ids
	ON
		RR.UserTypeID = Ids.ID
	INNER JOIN SplitIntList(@SeasonIDList) AS SeasonIds
	ON
		RR.SeasonID = SeasonIds.ID
	WHERE
		RU.RecruitedDate BETWEEN @StartDate AND @EndDate
	ORDER BY
		FullName


	SELECT
		RU.FullName
		, COALESCE(RecruitsTable.NumRecruits, 0) AS NumRecruits
		, RUT.Description
		, RU.UserID
		, RR.RecruitID
	FROM @TeamManagers AS Managers
	INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
	ON
		Managers.RecruitID = RR.RecruitID
	INNER JOIN RU_Users AS RU WITH (NOLOCK)
	ON
		RR.UserID = RU.UserID
	INNER JOIN RU_UserType AS RUT
	ON
		RR.UserTypeID = RUT.UserTypeID

	--------------------------------------------------------------------------------
	-- 
	INNER JOIN
		(SELECT
			Recruits.ReportsToID
			, ISNULL(COUNT(*), 0) AS NumRecruits
		FROM @Recruits AS Recruits
		GROUP BY
			Recruits.ReportsToID
		) AS RecruitsTable
	ON
		Managers.RecruitID = RecruitsTable.ReportsToID

	ORDER BY
		NumRecruits DESC
		, FullName

END
GO

GRANT EXEC ON dbo.custRU_RecruitsNewRecruits TO PUBLIC
GO