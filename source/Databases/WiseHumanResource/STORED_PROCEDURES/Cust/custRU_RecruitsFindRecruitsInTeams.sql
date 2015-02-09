USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsFindRecruitsInTeams')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsFindRecruitsInTeams'
		DROP  Procedure  dbo.custRU_RecruitsFindRecruitsInTeams
	END
GO

PRINT 'Creating Procedure custRU_RecruitsFindRecruitsInTeams'
GO
/******************************************************************************
**		File: custRU_RecruitsFindRecruitsInTeams.sql
**		Name: custRU_RecruitsFindRecruitsInTeams
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
CREATE Procedure dbo.custRU_RecruitsFindRecruitsInTeams
(
	@FirstName AS NVARCHAR(50)
	, @LastName AS NVARCHAR(50)
	, @TeamIDList NVARCHAR(MAX)
	, @DeletionStatus NVARCHAR(10) = NULL -- ('ALL', 'Deleted', NULL/'NotDeleted')
)
AS
BEGIN
	--	DECLARE @FirstName AS NVARCHAR(50)
--	SET @FirstName = '%l%'
--
--	DECLARE @LastName AS NVARCHAR(50)
--	SET @LastName = '%'
--
--	DECLARE @TeamIDList NVARCHAR(MAX)
--	SET @TeamIDList = '115,107,149'
--
--	DECLARE @DeletionStatus NVARCHAR(10)
--	SET @DeletionStatus = 'ALL' -- ('ALL', 'Deleted', NULL/'NotDeleted')

	SELECT
		RU.*
		, RR.*
	FROM dbo.SAE_RecruitTeamMappings AS Mapping
	INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
	ON
		Mapping.RecruitID = RR.RecruitID
	INNER JOIN RU_Users AS RU WITH(NOLOCK)
	ON
		RR.UserID = RU.UserID
	INNER JOIN SplitIntList(@TeamIDList) AS Ids
	ON
		Mapping.TeamID = Ids.ID
	WHERE
		(RU.FirstName LIKE @FirstName)
		AND (RU.FirstName LIKE @FirstName)
		AND (RR.IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus)))

END
GO

GRANT EXEC ON dbo.custRU_RecruitsFindRecruitsInTeams TO PUBLIC
GO