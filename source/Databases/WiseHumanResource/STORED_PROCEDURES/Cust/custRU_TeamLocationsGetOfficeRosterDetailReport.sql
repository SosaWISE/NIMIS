USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetOfficeRosterDetailReport')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetOfficeRosterDetailReport'
		DROP  Procedure  dbo.custRU_TeamLocationsGetOfficeRosterDetailReport
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetOfficeRosterDetailReport'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetOfficeRosterDetailReport.sql
**		Name: custRU_TeamLocationsGetOfficeRosterDetailReport
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
CREATE Procedure dbo.custRU_TeamLocationsGetOfficeRosterDetailReport
(
	@TeamLocationID INT
	, @SnapShotDate DATETIME
)
AS
BEGIN

	-- Execute SQL Scrpt
	SELECT
		*
	FROM
	(
		SELECT
			RecUser.UserID
			, RecUser.RecruitID
			, RecUser.FullName
			, RecUser.PublicFullName
			, CASE
					WHEN RecUser.RoleLocationID = 1 THEN 'Sales Rep'
					WHEN RecUser.RoleLocationID = 2 THEN 'Technician'
					ELSE 'Other'
				END AS RoleLocation
			, RecUser.RoleLocationID
			, ROS.ArrivalDate
			, ROS.QuitDate
			
			, CASE
					WHEN ROS.ArrivalDate IS NOT NULL AND ROS.QuitDate IS NULL THEN 0
					WHEN ROS.ArrivalDate IS NOT NULL AND ROS.QuitDate IS NOT NULL THEN 1
					WHEN ROS.ArrivalDate IS NULL AND ROS.QuitDate IS NOT NULL THEN 2
					ELSE 3
				END AS DateNullOrder
				
			, CAST(CASE
					WHEN DT.DATE IS NOT NULL THEN 1
					ELSE 0
				END AS BIT) AS IsCurrentlyOnRoster
				
			, CASE
					--WHEN DT.DATE IS NOT NULL THEN 'On Roster'
					WHEN ROS.ArrivalDate IS NOT NULL THEN 'On Roster'
					WHEN ROS.QuitDate IS NOT NULL THEN 'Quit'
					WHEN ROS.ArrivalDate IS NULL THEN 'Pending Arrival'
					ELSE ''
				END AS RosterStatus

		FROM vwRU_TeamLocatonRosterCurrentByRecruit AS ROS WITH(NOLOCK)
		LEFT OUTER JOIN SAE_Dates AS DT
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
			ROS.TeamLocationID = @TeamLocationID
			AND (ROS.IsDeleted = 0)
	) AS S
	ORDER BY
		--RecruitID,
		DateNullOrder
		, RoleLocationID
		, ArrivalDate
		, QuitDate
		, FullName


END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetOfficeRosterDetailReport TO PUBLIC
GO