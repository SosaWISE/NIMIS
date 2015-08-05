USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwRU_SalesReps')
	BEGIN
		PRINT 'Dropping VIEW vwRU_SalesReps'
		DROP VIEW dbo.vwRU_SalesReps
	END
GO

PRINT 'Creating VIEW vwRU_SalesReps'
GO

/****** Object:  View [dbo].[vwRU_SalesReps]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwRU_SalesReps.sql
**		Name: vwRU_SalesReps
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
**		Date: 08/05/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	08/05/2015	Andres Sosa 	Created By
*******************************************************************************/
CREATE VIEW [dbo].[vwRU_SalesReps]
AS
 
 SELECT
	AR.SalesRepId
	, AR.FullName
	, AR.RepFirstName
	, AR.RepLastName
	, AR.RepBirthDate
	, AR.RepSeasonId
	, AR.RepSeasonName
FROM
	(SELECT
			RU.GPEmployeeId AS SalesRepId
		   , RU.FullName
		   , RU.FirstName AS RepFirstName
		   , RU.LastName AS RepLastName
		   , RU.BirthDate AS [RepBirthDate]
		   , RS.SeasonID AS RepSeasonId
		   , RS.SeasonName AS RepSeasonName
		   , ROW_NUMBER() OVER (PARTITION BY RU.GPEmployeeId ORDER BY RS.SeasonID) AS RWN
	FROM

		   [WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
		   INNER JOIN [WISE_HumanResource].[dbo].[RU_Recruits] AS RR WITH (NOLOCK)
		   ON
				  (RR.UserID = RU.UserID)
			INNER JOIN [WISE_HumanResource].[dbo].[RU_UserType] AS UT WITH (NOLOCK)
				ON RR.UserTypeId = UT.UserTypeID
				AND UT.[Description] LIKE '%Sales%'
		   INNER JOIN [WISE_HumanResource].[dbo].[RU_Season] AS RS WITH (NOLOCK)
		   ON
				  (RS.SeasonID = RR.SeasonID)
	WHERE
		(RS.SeasonID NOT IN (2)) -- Exclude seasons that are not sales like tech seasons.
	) AS AR
WHERE
	(AR.RWN = 1)
GO
/* TEST */
SELECT * FROM vwRU_SalesReps
