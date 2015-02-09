USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetOrphanedSalesUsers')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetOrphanedSalesUsers'
		DROP  Procedure  dbo.custRU_UsersGetOrphanedSalesUsers
	END
GO

PRINT 'Creating Procedure custRU_UsersGetOrphanedSalesUsers'
GO
/******************************************************************************
**		File: custRU_UsersGetOrphanedSalesUsers.sql
**		Name: custRU_UsersGetOrphanedSalesUsers
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
CREATE Procedure dbo.custRU_UsersGetOrphanedSalesUsers
--(
--	No parameters
--)
AS
BEGIN
	SELECT
		RU.FullName
		, RU.UserID
		, R.UserTypeID
	FROM
		RU_Users AS RU WITH (NOLOCK)
		INNER JOIN RU_Recruits AS R WITH (NOLOCK)
		ON
			RU.UserID = R.UserID
			AND R.RecruitID IN
				(SELECT -- Get most recent recruiting season
					TOP 1
					RR.RecruitID
				FROM
					RU_Recruits AS RR WITH (NOLOCK)
				WHERE
					RR.UserID = R.UserID
					AND RR.UserTypeID IN (2,3,4,5,11) -- Only Sales
				ORDER BY
					RR.SeasonID DESC)
	WHERE
		RU.IsActive = 1
		AND RU.IsDeleted = 0
		AND RU.UserID != 5799 -- Can't be [Platinum Protection] User
		AND RecruitedByID IS NULL -- Return Users who have been orphaned
	ORDER BY
		RU.FullName
END
GO

GRANT EXEC ON dbo.custRU_UsersGetOrphanedSalesUsers TO PUBLIC
GO