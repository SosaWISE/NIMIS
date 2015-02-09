USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetMigrationRecruits')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetMigrationRecruits'
		DROP  Procedure  dbo.custRU_RecruitsGetMigrationRecruits
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetMigrationRecruits'
GO
/******************************************************************************
**		File: custRU_RecruitsGetMigrationRecruits.sql
**		Name: custRU_RecruitsGetMigrationRecruits
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
CREATE Procedure dbo.custRU_RecruitsGetMigrationRecruits
(
	@MigrateFromSeasonID INT
	, @MigrateToSeasonID INT
	, @UserTypeID INT
	, @ExcludeAlreadyInSeason BIT
)
AS
BEGIN
	--DECLARE @MigrateFromSeasonID INT
	--SET @MigrateFromSeasonID = 16

	--DECLARE @MigrateToSeasonID INT
	--SET @MigrateToSeasonID = 17

	--DECLARE @UserTypeID INT
	--SET @UserTypeID = 1

	--DECLARE @ExcludeAlreadyInSeason BIT
	--SET @ExcludeAlreadyInSeason = 0


	SELECT
	
		RR.*
		
		--Man.SeasonID
		--, Man.RecruitID
		--, Man.UserID
		--, Man.FullName
		
		--, RecUser.SeasonID
		--, RecUser.RecruitID
		--, RecUser.UserID
		--, RecUser.FullName
		
		----, *
		 
	FROM RU_Recruits AS RR WITH(NOLOCK)
	LEFT OUTER JOIN VW_RecruitUser AS Man WITH(NOLOCK)
	ON
		RR.ReportsToID = MAN.RecruitID
	INNER JOIN VW_RecruitUser AS RecUser WITH(NOLOCK)
	ON
		RR.RecruitID = RecUser.RecruitID
	WHERE
		RecUser.IsDeleted = 0--User and Recruit Not Deleted
		AND RecUser.SeasonID = @MigrateFromSeasonID
		AND RecUser.UserTypeID = @UserTypeID
		AND
		(
			@ExcludeAlreadyInSeason = 0
			OR RecUser.UserID NOT IN
			(
				--Users in Migrate to season
				SELECT
					RR.UserID
				FROM RU_Recruits AS RR WITH(NOLOCK)
				WHERE
					RR.IsDeleted = 0
					AND RR.SeasonID = @MigrateToSeasonID
			)
		)
	ORDER BY
		Man.FullName--Group by the manager
		, RecUser.FullName--and then order by the recruits name

END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetMigrationRecruits TO PUBLIC
GO