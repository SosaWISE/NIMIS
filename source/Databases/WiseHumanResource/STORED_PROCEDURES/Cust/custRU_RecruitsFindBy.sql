USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsFindBy')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsFindBy'
		DROP  Procedure  dbo.custRU_RecruitsFindBy
	END
GO

PRINT 'Creating Procedure custRU_RecruitsFindBy'
GO
/******************************************************************************
**		File: custRU_RecruitsFindBy.sql
**		Name: custRU_RecruitsFindBy
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
CREATE Procedure dbo.custRU_RecruitsFindBy
(
	@SeasonIDList NVARCHAR(500) -- This is a comma delimited list of ints e.g. '1,2,3' 
	, @FirstName NVARCHAR(50) = '%'
	, @LastName NVARCHAR(50) = '%'
)
AS
BEGIN
	SELECT 
		RU.*
		, RR.*
	FROM [RU_Users] AS RU WITH (NOLOCK)
	INNER JOIN [RU_Recruits] AS RR WITH (NOLOCK)
	ON
		((RU.IsActive = 1) AND (RU.IsDeleted = 0))
		AND RU.UserID = RR.UserID
	INNER JOIN [RU_UserType] AS RUT WITH (NOLOCK)
	ON
		((RR.IsActive = 1) AND (RR.IsDeleted = 0))
		AND RR.UserTypeID = RUT.UserTypeID
	INNER JOIN [RU_RoleLocations] AS RRL WITH (NOLOCK)
	ON
		RUT.RoleLocationID = RRL.RoleLocationID
	INNER JOIN SplitIntList(@SeasonIDList) AS Ids -- Filter by SeasonIDs
	ON
		Ids.ID = RR.SeasonID
	WHERE
		(RU.FirstName LIKE @FirstName)
		AND (RU.LastName LIKE @LastName)
	ORDER BY
		RU.FullName
END
GO

GRANT EXEC ON dbo.custRU_RecruitsFindBy TO PUBLIC
GO