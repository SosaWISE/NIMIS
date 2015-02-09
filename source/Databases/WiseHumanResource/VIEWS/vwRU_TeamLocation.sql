USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwRU_TeamLocation')
	BEGIN
		PRINT 'Dropping VIEW vwRU_TeamLocation'
		DROP VIEW dbo.vwRU_TeamLocation
	END
GO

PRINT 'Creating VIEW vwRU_TeamLocation'
GO

/****** Object:  View [dbo].[vwRU_TeamLocation]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwRU_TeamLocation.sql
**		Name: vwRU_TeamLocation
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
**		Date: 12/13/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	12/13/2013	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE VIEW [dbo].[vwRU_TeamLocation]
AS

SELECT
	TL.TeamLocationID
	, TL.[Description]
	, RS.SeasonID
	, RS.SeasonName
	, MM.MarketID
	, MM.MarketName
	, TL.City
	, PS.StateAB
	, PTZ.TimeZoneID
	, PTZ.TimeZoneName
	, PTZ.HourDifference + 7 AS HourDifference -- The #7 is used because this is the offset of MST from Greenwhich
	, DATEADD(hh, PTZ.HourDifference, getUTCDate()) AS [CurrTime]
FROM
	RU_TeamLocations AS TL WITH (NOLOCK)
	INNER JOIN RU_Season AS RS WITH (NOLOCK)
	ON
		(TL.SeasonID = RS.SeasonID)
	INNER JOIN WISE_CRM.dbo.MC_Markets AS MM WITH (NOLOCK)
	ON
		(TL.MarketId = MM.MarketID)
	INNER JOIN WISE_CRM.dbo.MC_PoliticalStates AS PS WITH (NOLOCK)
	ON
		(TL.StateId = PS.StateID)
	LEFT OUTER JOIN WISE_CRM.dbo.MC_PoliticalTimeZones AS PTZ WITH (NOLOCK)
	ON
		(TL.TimeZoneId = PTZ.TimeZoneID)
WHERE
	(TL.IsActive = 1)
	AND (TL.IsDeleted = 0)
	
GO
/* TEST */
-- SELECT * FROM vwRU_TeamLocation
