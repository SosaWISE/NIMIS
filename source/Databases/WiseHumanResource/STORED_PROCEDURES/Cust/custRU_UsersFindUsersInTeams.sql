USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersFindUsersInTeams')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersFindUsersInTeams'
		DROP  Procedure  dbo.custRU_UsersFindUsersInTeams
	END
GO

PRINT 'Creating Procedure custRU_UsersFindUsersInTeams'
GO
/******************************************************************************
**		File: custRU_UsersFindUsersInTeams.sql
**		Name: custRU_UsersFindUsersInTeams
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
CREATE Procedure dbo.custRU_UsersFindUsersInTeams
(
	@FirstName AS NVARCHAR(50) = '%'
	, @LastName AS NVARCHAR(50) = '%'
	, @DeletionStatus NVARCHAR(10) = NULL -- ('ALL', 'Deleted', NULL/'NotDeleted')
	, @TeamIDList NVARCHAR(MAX) = NULL
	, @SeasonID INT = NULL
)
AS
BEGIN

--	DECLARE @TeamIDList NVARCHAR(MAX)
--	SET @TeamIDList = '149'
--
--	DECLARE @FirstName NVARCHAR(50)
--	SET @FirstName = '%'
--
--	DECLARE @LastName NVARCHAR(50)
--	SET @LastName = '%'
--
--	DECLARE @DeletionStatus NVARCHAR(10)
--	SET @DeletionStatus = 'Deleted'
--	SET @DeletionStatus = 'NotDeleted' --NULL
--	SET @DeletionStatus = 'All'

	--IF(@FirstName = '') BEGIN
	--	SET @FirstName = '%'
	--END
	--
	--IF(@LastName = '') BEGIN
	--	SET @LastName = '%'
	--END

	SELECT DISTINCT
		RU.*
	FROM dbo.GetReportingTree(NULL, NULL, NULL, NULL, NULL) AS Tree
	INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
	ON
		Tree.RecruitID = RR.RecruitID
	INNER JOIN RU_Users AS RU WITH(NOLOCK)
	ON
		Tree.UserID = RU.UserID
	WHERE
		(RU.FirstName LIKE @FirstName)
		AND (RU.LastName LIKE @LastName) 
		AND ((@TeamIDList IS NULL) OR (Tree.TeamID IN (SELECT * FROM SplitIntList(@TeamIDList))))
		AND ((@SeasonID IS NULL) OR (Tree.SeasonID = @SeasonID))
		AND
		(
			(RU.IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus)))
			AND
			(RR.IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus)))
		)
	ORDER BY RU.FullName

END
GO

GRANT EXEC ON dbo.custRU_UsersFindUsersInTeams TO PUBLIC
GO