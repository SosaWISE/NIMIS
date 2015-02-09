USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRecruitSeasonsMaps')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRecruitSeasonsMaps'
		DROP  Procedure  dbo.custRU_RecruitsGetRecruitSeasonsMaps
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRecruitSeasonsMaps'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRecruitSeasonsMaps.sql
**		Name: custRU_RecruitsGetRecruitSeasonsMaps
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
CREATE Procedure dbo.custRU_RecruitsGetRecruitSeasonsMaps
(
	@FromSeasonID INT
	, @ToSeasonID INT
)
AS
BEGIN
	
	
	--DECLARE @FromSeasonID INT
	--SET @FromSeasonID = 16

	--DECLARE @ToSeasonID INT
	--SET @ToSeasonID = 17
	
	SELECT
		FromRR.RecruitID AS FromID
		, ToRR.RecruitID AS ToID
		--, *
	FROM RU_Recruits AS FromRR WITH(NOLOCK)
	INNER JOIN RU_Recruits AS ToRR WITH(NOLOCK)
	ON
		FromRR.UserID = ToRR.UserID
		AND (FromRR.UserID <> 5799 OR FromRR.UserTypeID = ToRR.UserTypeID)
	WHERE
		FromRR.IsDeleted = 0
		AND ToRR.IsDeleted = 0
		
		AND FromRR.SeasonID = @FromSeasonID
		AND ToRR.SeasonID = @ToSeasonID


END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetRecruitSeasonsMaps TO PUBLIC
GO