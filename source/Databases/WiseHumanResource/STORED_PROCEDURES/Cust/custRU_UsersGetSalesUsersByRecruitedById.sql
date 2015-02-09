USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetSalesUsersByRecruitedById')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetSalesUsersByRecruitedById'
		DROP  Procedure  dbo.custRU_UsersGetSalesUsersByRecruitedById
	END
GO

PRINT 'Creating Procedure custRU_UsersGetSalesUsersByRecruitedById'
GO
/******************************************************************************
**		File: custRU_UsersGetSalesUsersByRecruitedById.sql
**		Name: custRU_UsersGetSalesUsersByRecruitedById
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
CREATE Procedure dbo.custRU_UsersGetSalesUsersByRecruitedById
(
	@RecruitedById INT
)
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
					AND RR.UserTypeID IN (1,2,3,4,5,11) -- Only Sales
				ORDER BY
					RR.SeasonID DESC)
	WHERE
		--((RU.IsActive = 1) AND (RU.IsDeleted = 0)) AND 
		RU.UserID != 5799 -- Can't be [Platinum Protection] User
		AND RecruitedByID = @RecruitedById
	ORDER BY
		RU.FullName
END
GO

GRANT EXEC ON dbo.custRU_UsersGetSalesUsersByRecruitedById TO PUBLIC
GO