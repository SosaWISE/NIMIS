USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetOfficeRosterReport')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetOfficeRosterReport'
		DROP  Procedure  dbo.custRU_TeamLocationsGetOfficeRosterReport
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetOfficeRosterReport'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetOfficeRosterReport.sql
**		Name: custRU_TeamLocationsGetOfficeRosterReport
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
CREATE Procedure dbo.custRU_TeamLocationsGetOfficeRosterReport
(
	@SeasonID INT
	, @SnapShotDate DATETIME
)
AS
BEGIN


	SELECT

		RUTL.TeamLocationID
		, RUTL.Description AS OfficeName
		
		, COALESCE(S.NReps, 0) AS NReps
		, COALESCE(S.NTechs, 0) AS NTechs
		
	FROM RU_TeamLocations AS RUTL WITH(NOLOCK)
	LEFT OUTER JOIN
	(
		SELECT
			ROS.TeamLocationID
			, SUM(CASE
					WHEN RecUser.RoleLocationID = 1 THEN 1
					ELSE 0
				END) AS NReps
			, SUM(CASE
					WHEN RecUser.RoleLocationID = 2 THEN 1
					ELSE 0
				END) AS NTechs
		--FROM SAE_TeamRecruitSnapShots AS SS WITH(NOLOCK)
		FROM vwRU_TeamLocatonRosterCurrentByRecruit AS ROS WITH(NOLOCK)
		INNER JOIN SAE_Dates AS DT
		ON
			(
				(ROS.ArrivalDate < (DT.NEXT_DAY_DATE))
				--AND ((ROS.QuitDate IS NULL) OR (DT.NEXT_DAY_DATE <= ROS.QuitDate))--Exclude QuitDate
				AND ((ROS.QuitDate IS NULL) OR ((DT.DATE) <= ROS.QuitDate))--Include QuitDate
			)
			AND (DT.DATE = @SnapShotDate)
		INNER JOIN VW_RecruitUser AS RecUser WITH(NOLOCK)
		ON
			ROS.RecruitID = RecUser.RecruitID
		WHERE
			RecUser.SeasonID = @SeasonID
			AND (ROS.IsDeleted = 0)
		GROUP BY
			ROS.TeamLocationID
	) AS S
	ON
		RUTL.TeamLocationID = S.TeamLocationID
	WHERE
		RUTL.IsDeleted = 0
		AND RUTL.SeasonID = @SeasonID
	ORDER BY
		RUTL.Description


END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetOfficeRosterReport TO PUBLIC
GO