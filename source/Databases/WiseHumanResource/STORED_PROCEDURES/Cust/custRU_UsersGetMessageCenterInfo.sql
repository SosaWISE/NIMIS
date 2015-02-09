USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetMessageCenterInfo')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetMessageCenterInfo'
		DROP  Procedure  dbo.custRU_UsersGetMessageCenterInfo
	END
GO

PRINT 'Creating Procedure custRU_UsersGetMessageCenterInfo'
GO
/******************************************************************************
**		File: custRU_UsersGetMessageCenterInfo.sql
**		Name: custRU_UsersGetMessageCenterInfo
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
CREATE Procedure dbo.custRU_UsersGetMessageCenterInfo
(
	@RecruitIDList NVARCHAR(MAX) -- This is a comma delimited list of ints e.g. '1,2,3'
	, @SeasonID INT = NULL
)
AS
BEGIN

--	DECLARE @RecruitIDList NVARCHAR(MAX) -- This is a comma delimited list of ints e.g. '1,2,3'
--	SET @RecruitIDList = '2521,3104' -- [Platinum Protection] both recruits
--	DECLARE @SeasonID INT
--	SET @SeasonID = 6

--	SELECT
--		Tree.UserID
--		, RU.FullName
--		, RUT.Description + ' -- ' + RT.Description  AS Description
--	FROM GetReportingTree('ReportingLevel', NULL, NULL, NULL, @SeasonID) AS Tree
--	INNER JOIN RU_Users AS RU WITH (NOLOCK)
--	ON
--		Tree.UserID = RU.UserID
--	INNER JOIN RU_Teams AS RT WITH (NOLOCK)
--	ON
--		Tree.TeamID = RT.TeamID
--	INNER JOIN RU_UserType AS RUT WITH (NOLOCK)
--	ON
--		Tree.UserTypeID = RUT.UserTypeID
--	INNER JOIN SplitIntList(@UserIDList) AS Ids
--	ON
--		Tree.UserID = Ids.ID
	
	-- this one doesn't get the team, but it adds all the users from @UserIDList 
	SELECT
		RR.RecruitID
		, RUT.Description
		, RU.*
	FROM  RU_Users AS RU WITH (NOLOCK)
	INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
	ON
		(RU.UserID = RR.UserID)
		--AND (RR.SeasonID = @SeasonID)
	INNER JOIN SplitIntList(@RecruitIDList) AS Ids
	ON
		(RR.RecruitID = Ids.ID)
	INNER JOIN RU_UserType AS RUT WITH (NOLOCK)
	ON
		(RR.UserTypeID = RUT.UserTypeID)
	ORDER BY
		RU.FullName ASC
		
END
GO

GRANT EXEC ON dbo.custRU_UsersGetMessageCenterInfo TO PUBLIC
GO