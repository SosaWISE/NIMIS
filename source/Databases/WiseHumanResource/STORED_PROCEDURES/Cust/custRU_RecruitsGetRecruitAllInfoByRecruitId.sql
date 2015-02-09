USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRecruitAllInfoByRecruitId')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRecruitAllInfoByRecruitId'
		DROP  Procedure  dbo.custRU_RecruitsGetRecruitAllInfoByRecruitId
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRecruitAllInfoByRecruitId'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRecruitAllInfoByRecruitId.sql
**		Name: custRU_RecruitsGetRecruitAllInfoByRecruitId
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
CREATE Procedure dbo.custRU_RecruitsGetRecruitAllInfoByRecruitId
(
	@RecruitId INT
)
AS
BEGIN
	-- Locals
SELECT 
	RU.*
	, RR.*
	, RS.SeasonName
FROM 
	[RU_Users] AS RU WITH (NOLOCK)
	INNER JOIN [RU_Recruits] AS RR WITH (NOLOCK)
	ON
		RU.UserID = RR.UserID
		AND (RU.IsActive = 1)
		AND (RU.IsDeleted = 0)
	INNER JOIN [RU_UserType] AS RUT WITH (NOLOCK)
	ON
		RR.UserTypeID = RUT.UserTypeID
		AND (RR.IsActive = 1)
		AND (RR.IsDeleted = 0)
	INNER JOIN [RU_RoleLocations] AS RRL WITH (NOLOCK)
	ON
		RUT.RoleLocationID = RRL.RoleLocationID
	INNER JOIN [RU_Season] AS RS WITH (NOLOCK)
	ON
		RR.SeasonID = RS.SeasonID
WHERE
	RR.RecruitId = @RecruitId
ORDER BY
	RU.FullName
	, RS.SeasonName

END

GO

GRANT EXEC ON dbo.custRU_RecruitsGetRecruitAllInfoByRecruitId TO PUBLIC
GO