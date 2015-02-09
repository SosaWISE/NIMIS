USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetCertificationInfo')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetCertificationInfo'
		DROP  Procedure  dbo.custRU_TeamsGetCertificationInfo
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetCertificationInfo'
GO
/******************************************************************************
**		File: custRU_TeamsGetCertificationInfo.sql
**		Name: custRU_TeamsGetCertificationInfo
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
CREATE Procedure dbo.custRU_TeamsGetCertificationInfo
(
	@TeamID INT
	, @HasOwnTeam BIT = NULL -- (NULL, 0, 1)
	, @DeletionStatus VARCHAR(10) = NULL -- ('ALL', 'Deleted', NULL/'NotDeleted')
)
AS
BEGIN

--	DECLARE @TeamID INT
--	SET @TeamID = 149

--	DECLARE @HasOwnTeam BIT
--	SET @HasOwnTeam = NULL

	DECLARE @TodayEnd DATETIME
	SET @TodayEnd = dbo.GetDateEnd(GETDATE())

	--DECLARE @SeasonID INT
	--SELECT
	--	@SeasonID = SeasonID
	--FROM RU_Teams AS RT
	--INNER JOIN RU_TeamLocations AS RTL
	--ON
	--	RT.TeamLocationID = RTL.TeamLocationID
	--WHERE
	--	RT.TeamID = @TeamID

	--------------------------------------------------------------------------------
	-- Recruits
	DECLARE @TeamMembers TABLE
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
	INSERT INTO @TeamMembers
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
	WHERE
		(RU.IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus)))


	--------------------------------------------------------------------------------
	--	Select out the info we want from the previous tables
	SELECT
		RU.UserID
		, RU.FullName
		, RUT.Description
		, RR.RecruitID
		, RP.[Name] AS PayScale

		, CASE
			WHEN RR.CBxSocialSecCard = 1 THEN 'Yes' 
			ELSE 'No'
		END CBxSocialSecCard

		, CASE
			WHEN RR.CBxDriversLicense = 1 THEN 'Yes' 
			ELSE 'No'
		END CBxDriversLicense

		, CASE
			WHEN RR.CBxI9 = 1 THEN 'Yes' 
			ELSE 'No'
		END CBxI9

		, CASE
			WHEN RR.UserTypeID IN (1,2,3,4,5,11) THEN 'N/A' 
			WHEN RR.CBxW4 = 1 THEN 'Yes' 
			ELSE 'No'
		END CBxW9

		, CASE
			WHEN RR.UserTypeID IN (6,7,8,9,10) THEN 'N/A' 
			WHEN RR.CBxW9 = 1 THEN 'Yes' 
			ELSE 'No'
		END CBxW4

	FROM @TeamMembers AS REC

	--------------------------------------------------------------------------------
	--  Get User Info
	INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
	ON
		REC.RecruitID = RR.RecruitID
	INNER JOIN RU_Users AS RU WITH (NOLOCK)
	ON
		RR.UserID = RU.UserID
	INNER JOIN RU_UserType AS RUT WITH (NOLOCK)
	ON
		RR.UserTypeID = RUT.UserTypeID
	INNER JOIN RU_Payscales AS RP WITH (NOLOCK)
	ON
		RR.PayScaleID = RP.PayScaleID


----Order By
	ORDER BY
		RU.FullName
----Order By

		
END
GO

GRANT EXEC ON dbo.custRU_TeamsGetCertificationInfo TO PUBLIC
GO