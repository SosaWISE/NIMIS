USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitGetAllTechsBySeasonID')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitGetAllTechsBySeasonID'
		DROP  Procedure  dbo.custRU_RecruitGetAllTechsBySeasonID
	END
GO

PRINT 'Creating Procedure custRU_RecruitGetAllTechsBySeasonID'
GO
/******************************************************************************
**		File: custRU_RecruitGetAllTechsBySeasonID.sql
**		Name: custRU_RecruitGetAllTechsBySeasonID
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
**	Date:		Author:			Description:
**	-----------	-------------	-------------------------------------------
**	12/05/2013
*******************************************************************************/
CREATE Procedure dbo.custRU_RecruitGetAllTechsBySeasonID
(
	@SeasonID INT
)
AS
BEGIN
	SELECT
		RR.*
	FROM
		RU_Recruits AS RR WITH (NOLOCK)
		INNER JOIN RU_Season AS RS WITH (NOLOCK)
		ON
			(RR.SeasonID = RS.SeasonID)
		INNER JOIN RU_Users AS RU WITH (NOLOCK)
		ON
			(RR.UserID = RU.UserID)
	WHERE
		(RU.GPEmployeeID IS NOT NULL) 
		AND (RR.UserTypeID IN 
			(SELECT UserTypeID
				FROM RU_UserType
				WHERE (Description = 'Lead Technician') OR
						(Description = 'Technician') OR
						(Description = 'Regional Technical Manager')))
		AND (RR.IsActive = 1)
		AND (RR.IsDeleted = 0)
		AND (RR.SeasonID = 1)
	ORDER BY
		RU.FullName ASC
END
GO

GRANT EXEC ON dbo.custRU_RecruitGetAllTechsBySeasonID TO PUBLIC
GO