USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRecruitsByTeamLocationAndUserTypeID')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRecruitsByTeamLocationAndUserTypeID'
		DROP  Procedure  dbo.custRU_RecruitsGetRecruitsByTeamLocationAndUserTypeID
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRecruitsByTeamLocationAndUserTypeID'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRecruitsByTeamLocationAndUserTypeID.sql
**		Name: custRU_RecruitsGetRecruitsByTeamLocationAndUserTypeID
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
CREATE Procedure dbo.custRU_RecruitsGetRecruitsByTeamLocationAndUserTypeID
(
	@TeamLocationIDList NVARCHAR(500)
	, @UserTypeID INT = 0
)
AS
BEGIN
	
	--DECLARE @TeamLocationIDList NVARCHAR(500)
	--SET @TeamLocationIDList = '86,89,91,92,93,94,95,96,97,98,99,100,103,105,106,107,108,110,111,112'
	--DECLARE @UserTypeID INT
	--SET @UserTypeID = 0

	SELECT
		RR.TeamLocationID
		, RecruitID
		, UserID
		, UserTypeID
		, RR.SeasonID
	FROM VW_RecruitTreeBy_ReportsToID AS RR
	INNER JOIN SplitIntList(@TeamLocationIDList) AS Ids
	ON
		RR.TeamLocationID = Ids.ID
	WHERE
		((@UserTypeID = 0) OR (UserTypeID = @UserTypeID)) --Optional UserTypeID
	GROUP BY
		RR.TeamLocationID
		, RecruitID
		, UserID
		, UserTypeID
		, RR.SeasonID
	ORDER BY UserID
	
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetRecruitsByTeamLocationAndUserTypeID TO PUBLIC
GO