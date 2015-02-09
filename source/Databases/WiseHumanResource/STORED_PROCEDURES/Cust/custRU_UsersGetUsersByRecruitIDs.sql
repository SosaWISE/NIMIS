USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetUsersByRecruitIDs')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetUsersByRecruitIDs'
		DROP  Procedure  dbo.custRU_UsersGetUsersByRecruitIDs
	END
GO

PRINT 'Creating Procedure custRU_UsersGetUsersByRecruitIDs'
GO
/******************************************************************************
**		File: custRU_UsersGetUsersByRecruitIDs.sql
**		Name: custRU_UsersGetUsersByRecruitIDs
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
CREATE Procedure dbo.custRU_UsersGetUsersByRecruitIDs
(
	@RecruitIDList NVARCHAR(MAX)
)
AS
BEGIN

	SELECT
		RU.*
	FROM RU_Users AS RU WITH (NOLOCK)
	INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
	ON
		(RU.UserID = RR.UserID)
	INNER JOIN dbo.SplitIntList(@RecruitIDList) AS Ids
	ON
		RR.RecruitID = Ids.ID
	ORDER BY
		RU.FullName ASC
		
END
GO

GRANT EXEC ON dbo.custRU_UsersGetUsersByRecruitIDs TO PUBLIC
GO