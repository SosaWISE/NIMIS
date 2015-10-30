USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwTS_Tech')
	BEGIN
		PRINT 'Dropping VIEW vwTS_Tech'
		DROP VIEW dbo.vwTS_Tech
	END
GO

PRINT 'Creating VIEW vwTS_Tech'
GO

/****** Object:  View [dbo].[vwTS_Tech]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwTS_Tech.sql
**		Name: vwTS_Tech
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
**		Auth: Aaron Shumway
**		Date: 1/06/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
*******************************************************************************/
CREATE VIEW [dbo].[vwTS_Tech]
AS
	-- Enter Query here

	SELECT
		RR.RecruitId -- get from recruit instead of tech
		, RR.TeamId

		, T.ID
		, T.CreatedOn
		, T.CreatedBy
		, T.ModifiedOn
		, T.ModifiedBy
		, T.IsDeleted
		, T.[Version]
		--, T.RecruitId
		, T.StartLocation
		, T.StartLocLatitude
		, T.StartLocLongitude
		, T.MaxRadius

		, UT.UserTypeTeamTypeID

		, RU.FullName
		, RU.GPEmployeeID
		, RS.SeasonID
		, RS.SeasonName
	FROM [dbo].[RU_Recruits] AS RR WITH (NOLOCK)
	LEFT OUTER JOIN TS_Techs AS T WITH(NOLOCK)
	ON
		RR.RecruitID = T.RecruitId
	INNER JOIN [dbo].[RU_UserType] AS UT WITH (NOLOCK)
	ON
		RR.UserTypeId = UT.UserTypeID
	INNER JOIN [dbo].[RU_Users] AS RU WITH (NOLOCK)
	ON
		(RR.UserID = RU.UserID)
	INNER JOIN [dbo].[RU_Season] AS RS WITH (NOLOCK)
	ON
		(RS.SeasonID = RR.SeasonID)
	WHERE
		UT.UserTypeTeamTypeID IN (4, 7) -- 4-Tech Team Member, 7-Tech Team Manager

GO
/* TEST */
-- SELECT * FROM vwTS_Tech
