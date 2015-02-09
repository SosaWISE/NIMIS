USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetOfficesUnderRecruit')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetOfficesUnderRecruit'
		DROP  Procedure  dbo.custRU_TeamLocationsGetOfficesUnderRecruit
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetOfficesUnderRecruit'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetOfficesUnderRecruit.sql
**		Name: custRU_TeamLocationsGetOfficesUnderRecruit
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
CREATE Procedure dbo.custRU_TeamLocationsGetOfficesUnderRecruit
(
	@RegionID INT = NULL
	, @NationalRegionID INT = NULL
)
AS
BEGIN


	SELECT
	*
	FROM RU_TeamLocations
	WHERE
		TeamLocationID IN
		(
			SELECT DISTINCT
				RT.TeamLocationID
			FROM VW_RecruitingStructure AS RS
			INNER JOIN RU_Teams AS RT
			ON
				RS.TeamID = RT.TeamID
			WHERE
				(@RegionID IS NOT NULL AND RS.RegionID = @RegionID)
				OR (@NationalRegionID IS NOT NULL AND RS.NationalRegionID = @NationalRegionID)
		)
	ORDER BY
		[Description]


END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetOfficesUnderRecruit TO PUBLIC
GO