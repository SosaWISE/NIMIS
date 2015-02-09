USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetUsersByUserTypeID')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetUsersByUserTypeID'
		DROP  Procedure  dbo.custRU_UsersGetUsersByUserTypeID
	END
GO

PRINT 'Creating Procedure custRU_UsersGetUsersByUserTypeID'
GO
/******************************************************************************
**		File: custRU_UsersGetUsersByUserTypeID.sql
**		Name: custRU_UsersGetUsersByUserTypeID
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
CREATE Procedure dbo.custRU_UsersGetUsersByUserTypeID
(
	@UserTypeID INT = NULL
	, @IDList NVARCHAR(MAX) = NULL
	, @BreakDownType NVARCHAR(10) -- 'Season', 'Team'
	, @DeletionStatus NVARCHAR(10) = NULL
)
AS
BEGIN
	
--	DECLARE @TeamIDList NVARCHAR(500)
--	SET @TeamIDList = '248'
--	DECLARE @UserTypeID INT
--	SET @UserTypeID = 5
--	DECLARE @DeletionStatus NVARCHAR(10)
--	SET @DeletionStatus = NULL

	SELECT DISTINCT
		RU.*
	FROM GetReportingTree(NULL, NULL, NULL, NULL, NULL) AS Tree
	INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
	ON
		Tree.RecruitID = RR.RecruitID
	INNER JOIN RU_Users AS RU WITH (NOLOCK)
	ON
		Tree.UserID = RU.UserID

	INNER JOIN RU_UserType AS RUT WITH (NOLOCK)
	ON
		Tree.UserTypeID = RUT.UserTypeID

	--not every recruit has a team
	LEFT OUTER JOIN RU_Teams AS RT WITH (NOLOCK)
	ON
		Tree.TeamID = RT.TeamID
	WHERE
		((@UserTypeID IS NULL) OR (Tree.UserTypeID = @UserTypeID)) --Optional UserTypeID
		AND
		(
			(
				((@BreakDownType IS NULL) OR (@BreakDownType = 'Office'))
				AND ((@IDList IS NULL) OR (RT.TeamLocationID IN (SELECT * FROM SplitIntList(@IDList))))
			)
			OR
			(
				(@BreakDownType = 'Team')
				AND ((@IDList IS NULL) OR (Tree.TeamID IN (SELECT * FROM SplitIntList(@IDList))))
			)
			OR
			(
				(@BreakDownType = 'Season')
				AND ((@IDList IS NULL) OR (Tree.SeasonID IN (SELECT * FROM SplitIntList(@IDList))))
			)
		)
		AND
		(
			RU.IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus))
			AND
			RR.IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus))
		)
	ORDER BY
		RU.FullName
	
END
GO

GRANT EXEC ON dbo.custRU_UsersGetUsersByUserTypeID TO PUBLIC
GO