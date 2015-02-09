USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'VW_Teams')
	BEGIN
		PRINT 'Dropping VIEW VW_Teams'
		DROP VIEW dbo.VW_Teams
	END
GO

PRINT 'Creating VIEW VW_Teams'
GO

/****** Object:  View [dbo].[VW_Teams]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: VW_Teams.sql
**		Name: VW_Teams
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	-----------	-------------------------------------------
**	12/05/2013	Andres Sosa	Created
*******************************************************************************/
CREATE VIEW [dbo].[VW_Teams]
AS
	-- Enter Query here
SELECT
	RT.TeamID
	, RT.Description AS TeamName
	, RT.CreatedFromTeamID
	, RT.RoleLocationID
	, RT.RegionalManagerRecruitID
	, RT.IsActive
	, RT.IsDeleted
	, RT.CreatedBy
	, RT.CreatedOn
	, RT.ModifiedBy
	, RT.ModifiedOn
	
	, RUTL.TeamLocationID
	, RUTL.CreatedFromTeamLocationID
	, RUTL.Description AS OfficeName
	, RUTL.City
	
	, RS.SeasonID
	, RS.SeasonName
	
	, RST.StateID
	, RST.StateName
	, RST.StateAB AS StateAbbreviation
	
	, RUTL.CreatedBy AS OfficeCreatedBy
	, RUTL.CreatedOn AS OfficeCreatedOn
	, RUTL.ModifiedBy AS OfficeModifiedBy
	, RUTL.ModifiedOn AS OfficeModifiedOn
	
	, RRL.[Role] AS [TeamType]
	
FROM RU_Teams AS RT WITH(NOLOCK)
INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
ON
	(RT.TeamLocationID = RUTL.TeamLocationID)
INNER JOIN RU_Season AS RS WITH(NOLOCK)
ON
	(RUTL.SeasonID = RS.SeasonID)
INNER JOIN [WISE_CRM].[dbo].MC_PoliticalStates AS RST WITH(NOLOCK)
ON
	(RUTL.StateID = RST.StateID)
LEFT OUTER JOIN RU_RoleLocations AS RRL WITH (NOLOCK)
ON
	(RT.RoleLocationID = RRL.RoleLocationID)
GO
/* TEST */
-- SELECT * FROM VW_Teams
