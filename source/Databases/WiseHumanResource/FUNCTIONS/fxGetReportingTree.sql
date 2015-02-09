USE [WISE_HumanResource]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetReportingTree')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetReportingTree'
		DROP FUNCTION  dbo.fxGetReportingTree
	END
GO

PRINT 'Creating FUNCTION fxGetReportingTree'
GO
/******************************************************************************
**		File: fxGetReportingTree.sql
**		Name: fxGetReportingTree
**		Desc: Creates a Reporting Tree by using the ReportsToID. This structure can be used to find a recruits Team and the managers on said Team
**              
**		Parameters:
**		Input
**     ----------
**		@TopReportingLevel INT = NULL -- 3 is the Top Reporting Level...for now
**		@IsTeamMember BIT = NULL --(Optional, pass in null) - Valid Values(NULL, 0, 1) - 0:IsManager, 1:IsMember, NULL:Both
**		@TeamID INT = NULL --(Optional, pass in null)
**		@SeasonID INT = NULL --(Optional, pass in null)
**		
**		Auth: Aaron Shumway
**		Date: 10/13/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	10/13/2014	Aaron Shumway	Created By
**	
*******************************************************************************/
CREATE FUNCTION [dbo].[fxGetReportingTree]
(
	--First 2 Params used for determining the Starting Level
	@Type NVARCHAR(50) = NULL -- Valid Values('All'/NULL, 'ReportingLevel', and 'UserID')
	, @TypeID INT = NULL --(Optional, pass in null)

	, @HasOwnTeam BIT = NULL --(Optional, pass in null) - Valid Values(NULL, 0, 1) - 0:IsMember, 1:IsManager, NULL:Both
	, @TeamID INT = NULL --(Optional, pass in null)
	, @SeasonID INT = NULL --(Optional, pass in null)
)
RETURNS TABLE
AS

RETURN
(
	WITH ReportingTree_TopDown
	(
		--Recursion Info
		RecruitLevel
		, RecruitingSequence

		--Recruit Info
		, RecruitID
		, UserID
		, UserTypeId
		, ReportsToID
		, SeasonID
		--, PayScaleID
		--, IsRecruiter
		, MyTeamID
		, TeamID
		, UserTypeTeamTypeID

		, HasOwnTeam
	)
	AS
	(
		-- Recursion starter
		-- Start with 
		SELECT
			--Recursion Info
			0 AS RecruitLevel
			, CAST(RR.RecruitID AS NVARCHAR(900))
--			, CAST(ISNULL(CAST(RR.TeamID AS NVARCHAR(10)), '(No Team)') AS NVARCHAR(900) )

			--Recruit Info
			, RR.RecruitID
			, RR.UserID
			, RR.UserTypeId
			, RR.ReportsToID
			, RR.SeasonID
			--, RR.PayScaleID
			--, RR.IsRecruiter
			, RR.TeamID
			, RR.TeamID
			, RUT.UserTypeTeamTypeID
			, CASE
				WHEN RR.TeamID IS NOT NULL THEN 1
				ELSE NULL
			END
		FROM RU_Recruits AS RR WITH (NOLOCK)
		INNER JOIN RU_UserType AS RUT WITH (NOLOCK)
		ON
			RR.UserTypeID = RUT.UserTypeID
--		INNER JOIN GetUserTypeTeamTypes(5) AS Managers
--		ON
--			RUT.UserTypeTeamTypeID = Managers.UserTypeTeamTypeID
		WHERE
			(
				(@Type = 'All' OR @Type IS NULL) -- Default
				AND (RR.ReportsToID IS NULL)
			)
			OR
			(
				(@Type = 'ReportingLevel')
				AND
				(
					(RUT.ReportingLevel = @TypeID)
					OR
					(
						(@TypeID IS NULL) -- (Optional) -Use Default if @TypeID Is Null
						AND
						(RUT.ReportingLevel = 4--3
							--(
							--	SELECT TOP 1 ReportingLevel FROM RU_UserType
							--	ORDER BY ReportingLevel DESC
							--)
						)
					)
				)
			)
			OR
			(
				(@Type = 'UserID')
				AND (RR.UserID = @TypeID)
			)

		UNION ALL

		-- Recursion Helper
		SELECT
			--Recursion Info
			Parent.RecruitLevel + 1
			, CAST
			(
				Parent.RecruitingSequence
				+ '->' +
				CAST(RR.RecruitID AS NVARCHAR(10))
			AS NVARCHAR(900))

			--Recruit Info
			, RR.RecruitID
			, RR.UserID
			, RR.UserTypeId
			, RR.ReportsToID
			, RR.SeasonID
			--, RR.PayScaleID
			--, RR.IsRecruiter
			, RR.TeamID
			, ISNULL(RR.TeamID, Parent.TeamID)
			, RUT.UserTypeTeamTypeID
			, CASE
				WHEN RR.TeamID IS NOT NULL THEN 1
				WHEN Parent.TeamID IS NOT NULL THEN 0
				ELSE NULL
			END
		FROM RU_Recruits AS RR WITH (NOLOCK)
		INNER JOIN RU_UserType AS RUT WITH (NOLOCK)
		ON
			RR.UserTypeID = RUT.UserTypeID
		--Join to recursive parent
		INNER JOIN ReportingTree_TopDown AS Parent
		ON
			RR.ReportsToID = Parent.RecruitID
	)

	SELECT * FROM ReportingTree_TopDown
	WHERE
		((@HasOwnTeam IS NULL) OR (HasOwnTeam = @HasOwnTeam))-- (Optional)
		AND ((@TeamID IS NULL) OR (TeamID = @TeamID))-- (Optional)
		AND ((@SeasonID IS NULL) OR (SeasonID = @SeasonID))-- (Optional)
)

GO
