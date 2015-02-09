USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRecruitsByUserTypeIDAndTeamIDList')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRecruitsByUserTypeIDAndTeamIDList'
		DROP  Procedure  dbo.custRU_RecruitsGetRecruitsByUserTypeIDAndTeamIDList
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRecruitsByUserTypeIDAndTeamIDList'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRecruitsByUserTypeIDAndTeamIDList.sql
**		Name: custRU_RecruitsGetRecruitsByUserTypeIDAndTeamIDList
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
CREATE Procedure dbo.custRU_RecruitsGetRecruitsByUserTypeIDAndTeamIDList
(
	@TeamIDList NVARCHAR(500)
	, @UserTypeID INT = 0
)
AS
BEGIN
	
	--DECLARE @TeamIDList NVARCHAR(500)
	--SET @TeamIDList = ''
	--DECLARE @UserTypeID INT
	--SET @UserTypeID = 0

	SELECT
		RR.TeamID
		, RecruitID
		, RR.UserID
		, UserTypeID
		, RR.SeasonID
	FROM GetReportingTree('ReportingLevel', NULL, NULL, NULL, NULL) AS RR
	INNER JOIN RU_Users AS RU
	ON
		(RR.UserID = RU.UserID)
		AND (RU.IsDeleted = 0)
	INNER JOIN SplitIntList(@TeamIDList) AS Ids
	ON
		RR.TeamID = Ids.ID
	WHERE
		((@UserTypeID = 0) OR (UserTypeID = @UserTypeID)) --Optional UserTypeID
	GROUP BY
		RR.TeamID
		, RecruitID
		, RR.UserID
		, UserTypeID
		, RR.SeasonID
	ORDER BY UserID
	
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetRecruitsByUserTypeIDAndTeamIDList TO PUBLIC
GO