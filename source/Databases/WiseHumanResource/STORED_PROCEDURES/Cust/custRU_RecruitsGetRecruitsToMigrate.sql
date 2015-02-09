USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRecruitsToMigrate')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRecruitsToMigrate'
		DROP  Procedure  dbo.custRU_RecruitsGetRecruitsToMigrate
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRecruitsToMigrate'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRecruitsToMigrate.sql
**		Name: custRU_RecruitsGetRecruitsToMigrate
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
CREATE Procedure dbo.custRU_RecruitsGetRecruitsToMigrate
(
	@SeasonID INT
)
AS
BEGIN
	
	
		SELECT
			RR.*
		FROM RU_Recruits AS RR WITH(NOLOCK)
		INNER JOIN RU_Users AS RU WITH(NOLOCK)
		ON
			(RR.UserID = RU.UserID)
		WHERE
			(
				RR.RecruitID IN
				(
					SELECT
						RecruitID
					FROM vwRU_TerminationsWithStatus
					WHERE
						(TerminationCategory = 'End of Season')
				)
				OR
				(
					--Always include these usertypes even if the termination status is not End of Season
					RR.UserTypeID IN (2,3,6,8,11,19,20)--Managers, Regionals and National Regionals
					AND RR.IsDeleted = 0
					AND RU.IsDeleted = 0
				)
			)
			AND RR.SeasonID = @SeasonID


END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetRecruitsToMigrate TO PUBLIC
GO