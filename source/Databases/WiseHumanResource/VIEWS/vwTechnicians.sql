USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwTechnicians')
	BEGIN
		PRINT 'Dropping VIEW vwTechnicians'
		DROP VIEW dbo.vwTechnicians
	END
GO

PRINT 'Creating VIEW vwTechnicians'
GO

/****** Object:  View [dbo].[vwTechnicians]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwTechnicians.sql
**		Name: vwTechnicians
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
**	08/15/2014	Reagan 	Created By
**	
*******************************************************************************/
CREATE VIEW [dbo].[vwTechnicians]
AS
 
 SELECT
	    RU.GPEmployeeId AS TechId
       , RU.FirstName AS TechFirstName
       , RU.LastName AS TechLastName
       , RU.BirthDate AS [TechBDay]
       , RS.SeasonID AS TechSeasonId
       , RS.SeasonName AS TechSeasonName
FROM

       [WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
       INNER JOIN [WISE_HumanResource].[dbo].[RU_Recruits] AS RR WITH (NOLOCK)
       ON
              (RR.UserID = RU.UserID)
       INNER JOIN [WISE_HumanResource].[dbo].[RU_Season] AS RS WITH (NOLOCK)
       ON
              (RS.SeasonID = RR.SeasonID)
/*

-- this query is from andres
-- it returns duplicate names for technician

SELECT
       MSA.AccountID
       , MSA.TechId
       , RU.FirstName AS TechFirstName
       , RU.LastName AS TechLastName
       , RU.BirthDate AS [TechB-Day]
       , RS.SeasonID AS TechSeasonId
       , RS.SeasonName AS TechSeasonName
FROM
       [dbo].MS_Accounts AS MSA WITH (NOLOCK)
       INNER JOIN [WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
       ON
              (RU.GPEmployeeID = MSA.TechId)
       INNER JOIN [WISE_HumanResource].[dbo].[RU_Recruits] AS RR WITH (NOLOCK)
       ON
              (RR.UserID = RU.UserID)
       INNER JOIN [WISE_HumanResource].[dbo].[RU_Season] AS RS WITH (NOLOCK)
       ON
              (RS.SeasonID = RR.SeasonID)
 */





GO
/* TEST */
-- SELECT * FROM vwTechnicians
