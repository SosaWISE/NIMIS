USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetTeamsByRecruitID')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetTeamsByRecruitID'
		DROP  Procedure  dbo.custRU_TeamsGetTeamsByRecruitID
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetTeamsByRecruitID'
GO
/******************************************************************************
**		File: custRU_TeamsGetTeamsByRecruitID.sql
**		Name: custRU_TeamsGetTeamsByRecruitID
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
CREATE Procedure dbo.custRU_TeamsGetTeamsByRecruitID
(@RecruitID INT
)
AS
BEGIN
	
	--DECLARE @RecruitID INT
	--SET @RecruitID = 2209

	SELECT
		*
	FROM RU_Teams AS RUTL WITH (NOLOCK)
	WHERE
		TeamID IN
		(
			SELECT DISTINCT
				RR.TeamID
			FROM GetReportingTree('ReportingLevel', NULL, NULL, NULL, NULL) AS RR
			WHERE
				(RR.RecruitID = @RecruitID)
		)
	
END
GO

GRANT EXEC ON dbo.custRU_TeamsGetTeamsByRecruitID TO PUBLIC
GO