USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwRU_Technicians')
	BEGIN
		PRINT 'Dropping VIEW vwRU_Technicians'
		DROP VIEW dbo.vwRU_Technicians
	END
GO

PRINT 'Creating VIEW vwRU_Technicians'
GO

/****** Object:  View [dbo].[vwRU_Technicians]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwRU_Technicians.sql
**		Name: vwRU_Technicians
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
**	10/01/2014	Bob McFadden	Modified to return if RU_UserType.Description
**								is like '%TECH%'
**	
*******************************************************************************/
CREATE VIEW [dbo].[vwRU_Technicians]
AS
 
 SELECT
	    RU.GPEmployeeId AS TechnicianId
	   , RU.FullName
       , RU.FirstName AS TechFirstName
       , RU.LastName AS TechLastName
       , RU.BirthDate AS [TechBirthDate]
       , RS.SeasonID AS TechSeasonId
       , RS.SeasonName AS TechSeasonName
FROM

       [WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
       INNER JOIN [WISE_HumanResource].[dbo].[RU_Recruits] AS RR WITH (NOLOCK)
       ON
              (RR.UserID = RU.UserID)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_UserType] AS UT WITH (NOLOCK)
			ON RR.UserTypeId = UT.UserTypeID
			AND UT.Description LIKE '%TECH%'
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
-- SELECT * FROM vwRU_Technicians
