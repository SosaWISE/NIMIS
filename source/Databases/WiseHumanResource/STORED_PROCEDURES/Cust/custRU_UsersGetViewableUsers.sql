USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetViewableUsers')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetViewableUsers'
		DROP  Procedure  dbo.custRU_UsersGetViewableUsers
	END
GO

PRINT 'Creating Procedure custRU_UsersGetViewableUsers'
GO
/******************************************************************************
**		File: custRU_UsersGetViewableUsers.sql
**		Name: custRU_UsersGetViewableUsers
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
CREATE Procedure dbo.custRU_UsersGetViewableUsers
(
	@ViewingUserID INT
	, @CompanyID NVARCHAR(25) = NULL
	, @UserID INT = NULL
)
AS
BEGIN

--	DECLARE @ViewingUserID INT
--	SET @ViewingUserID = 4146
--
--	DECLARE @CompanyID NVARCHAR(25)
--	SET @CompanyID = 'CLAR001'
--
--	DECLARE @UserID INT
--	--SET @UserID = 7733
--	SET @UserID = NULL
--
--	--DECLARE @RecruitID INT
--	--SET @RecruitID = NULL

	SELECT
		DISTINCT RU.*
	FROM dbo.GetReportingTree('UserID', @ViewingUserID, NULL, NULL, NULL) AS Tree
	INNER JOIN RU_Users AS RU WITH(NOLOCK)
	ON
		(Tree.UserID = RU.UserID)
	WHERE
		((@CompanyID IS NULL) OR (RU.GPEmployeeID = @CompanyID))
		AND ((@UserID IS NULL) OR (RU.UserID = @UserID))
		--AND ((@RecruitID IS NULL) OR (Tree.RecruitID = @RecruitID))

END
GO

GRANT EXEC ON dbo.custRU_UsersGetViewableUsers TO PUBLIC
GO