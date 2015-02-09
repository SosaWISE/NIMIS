USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetOrphanRecruitsByManagersAndSeason')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetOrphanRecruitsByManagersAndSeason'
		DROP  Procedure  dbo.custRU_RecruitsGetOrphanRecruitsByManagersAndSeason
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetOrphanRecruitsByManagersAndSeason'
GO
/******************************************************************************
**		File: custRU_RecruitsGetOrphanRecruitsByManagersAndSeason.sql
**		Name: custRU_RecruitsGetOrphanRecruitsByManagersAndSeason
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
CREATE Procedure dbo.custRU_RecruitsGetOrphanRecruitsByManagersAndSeason
(
	@SeasonID INT
)
AS
BEGIN
	-- Enter CODE HERE>
	SELECT
		RR.*
	FROM
		RU_Recruits AS RR WITH (NOLOCK)
		INNER JOIN RU_Users AS RU WITH (NOLOCK)
		ON
			RR.UserID = RU.UserID
		INNER JOIN RU_UserType AS RT WITH (NOLOCK)
		ON
			RR.UserTypeID = RT.UserTypeID
	WHERE
		(RR.IsActive = 1)
		AND (RR.IsDeleted = 0)
		AND (RR.SeasonID = @SeasonID)
		AND (RR.ReportsToID IS NULL)
		AND (RT.UserTypeID NOT IN (1,8,11,12,13,14))
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetOrphanRecruitsByManagersAndSeason TO PUBLIC
GO