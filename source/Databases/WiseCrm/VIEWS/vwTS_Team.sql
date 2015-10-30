USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwTS_Team')
	BEGIN
		PRINT 'Dropping VIEW vwTS_Team'
		DROP VIEW dbo.vwTS_Team
	END
GO

PRINT 'Creating VIEW vwTS_Team'
GO

/****** Object:  View [dbo].[vwTS_Team]    Script Date: 10/29/2015 8:00:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/******************************************************************************
**		File: vwTS_Team.sql
**		Name: vwTS_Team
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
**		Date: 03/24/2015
*******************************************************************************/
CREATE VIEW [dbo].[vwTS_Team]
AS
	-- Enter Query here
	SELECT
		TT.TeamId -- get from ru_teams instead of ts_teams
		, TT.TeamId AS ID
		, TT.Version
		, TT.IsDeleted
		, TT.CreatedOn
		, TT.CreatedBy
		, TT.ModifiedOn
		, TT.ModifiedBy

		, TT.AddressId
		, AD.Latitude
		, AD.Longitude

		--, TT.TeamId
		, RT.Description

	FROM [dbo].[RU_Teams] AS RT WITH (NOLOCK)
	LEFT OUTER JOIN TS_Teams AS TT
	ON
		TT.TeamId = RT.TeamID
		AND TT.IsDeleted = 0
	LEFT OUTER JOIN QL_Address AS AD
	ON
		TT.AddressId = AD.AddressID

GO


