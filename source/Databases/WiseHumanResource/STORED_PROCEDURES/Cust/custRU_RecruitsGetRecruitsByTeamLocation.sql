USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRecruitsByTeamLocation')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRecruitsByTeamLocation'
		DROP  Procedure  dbo.custRU_RecruitsGetRecruitsByTeamLocation
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRecruitsByTeamLocation'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRecruitsByTeamLocation.sql
**		Name: custRU_RecruitsGetRecruitsByTeamLocation
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
CREATE Procedure dbo.custRU_RecruitsGetRecruitsByTeamLocation
(
	@TeamLocationID INT
	, @RoleLocationID INT = NULL
	, @DeletionStatus NVARCHAR(10) = NULL -- ('ALL', 'Deleted', NULL/'NotDeleted')
)
AS
BEGIN
	
--	DECLARE @TeamLocationID INT
--	SET @TeamLocationID = 107
--
--	DECLARE @RoleLocationID INT
--	SET @RoleLocationID = 2
--
--	DECLARE @DeletionStatus NVARCHAR(10)
--	SET @DeletionStatus = 'NotDeleted'
	

	SELECT
		RR.*
	FROM dbo.SAE_RecruitTeamMappings AS TMAP
	INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
	ON
		(TMAP.RecruitID = RR.RecruitID)
	INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
	ON
		(RR.UserTypeID = RUT.UserTypeID)
	INNER JOIN RU_Teams AS RT WITH(NOLOCK)
	ON
		(TMAP.TeamID = RT.TeamID)
	INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
	ON
		(RT.TeamLocationID = RUTL.TeamLocationID)
	WHERE
		(RUTL.TeamLocationID = @TeamLocationID)
		AND ((@RoleLocationID IS NULL) OR (RUT.RoleLocationID = @RoleLocationID))
		AND (RR.IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus)))
	
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetRecruitsByTeamLocation TO PUBLIC
GO