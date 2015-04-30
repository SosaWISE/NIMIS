USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetRecruits')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetRecruits'
		DROP  Procedure  dbo.custRU_UsersGetRecruits
	END
GO

PRINT 'Creating Procedure custRU_UsersGetRecruits'
GO
/******************************************************************************
**		File: custRU_UsersGetRecruits.sql
**		Name: custRU_UsersGetRecruits
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
CREATE Procedure dbo.custRU_UsersGetRecruits
(
	@UserID INT
	, @RoleLocationID INT
)
AS
BEGIN


	DECLARE @LastRecruitTable TABLE (UserID INT, RecruitID INT, SeasonID INT, RoleLocationID INT)
	INSERT INTO @LastRecruitTable
	SELECT
		UserID
		, RecruitID
		, SeasonID
		, RoleLocationID
	FROM
	(
		SELECT
			RR.UserID
			, RR.RecruitID
			, RR.SeasonID
			, RUT.RoleLocationID
			-- Order for lastest using the start date of the season
			, Rank() OVER (PARTITION BY RR.UserID ORDER BY RS.StartDate DESC) AS RecruitOrder
		FROM RU_Recruits AS RR WITH (NOLOCK)
		INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
		ON
			(RR.UserTypeID = RUT.UserTypeID)
		INNER JOIN RU_Season AS RS WITH(NOLOCK)--Join on season to get start and end dates
		ON
			(RR.SeasonID = RS.SeasonID)
	) RR
	-- last = 1
	WHERE
		RR.RecruitOrder = 1
		AND RR.RoleLocationID = @RoleLocationID


	SELECT
		RU.UserID
		, FullName
		, PublicFullName
		, CAST(
			(CASE
				WHEN Recruits.RecruitedByID IS NOT NULL THEN 1
				ELSE 0
			END) AS BIT) AS HasRecruits
	FROM RU_Users AS RU WITH(NOLOCK)
	INNER JOIN @LastRecruitTable AS LR
	ON
		RU.UserID = LR.UserID
	LEFT OUTER JOIN
	(
		SELECT
			RecruitedByID
		FROM RU_Users AS RU WITH(NOLOCK)
		INNER JOIN @LastRecruitTable AS LR
		ON
			RU.UserID = LR.UserID
		GROUP BY
			RecruitedByID
	) AS Recruits
	ON
		RU.UserID = Recruits.RecruitedByID
	WHERE
		(RU.IsDeleted = 'FALSE')
		AND (RU.RecruitedByID = @UserID)
	ORDER BY
		RU.FullName

END
GO

GRANT EXEC ON dbo.custRU_UsersGetRecruits TO PUBLIC
GO