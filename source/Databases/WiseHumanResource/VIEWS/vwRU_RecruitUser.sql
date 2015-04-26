USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwRU_RecruitUser')
	BEGIN
		PRINT 'Dropping VIEW vwRU_RecruitUser'
		DROP VIEW dbo.vwRU_RecruitUser
	END
GO

PRINT 'Creating VIEW vwRU_RecruitUser'
GO

/****** Object:  View [dbo].[vwRU_RecruitUser]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwRU_RecruitUser.sql
**		Name: vwRU_RecruitUser
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
**		Date: 02/05/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/05/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE VIEW [dbo].[vwRU_RecruitUser]
AS
SELECT
	RU.UserID
	, RU.RecruitedByID
	, RU.GPEmployeeID
	, RU.FirstName
	, RU.MiddleName
	, RU.LastName
	, RU.PreferredName
	, RU.FullName
	, RU.PublicFullName
	, RU.UserName
	, RU.Email
	, RU.PhoneCell
	, RU.PhoneCellCarrierID
	, RU.IsActive AS IsActiveUser
	, RU.IsDeleted AS IsDeletedUser
		
	, RR.RecruitID
	, RR.SeasonID
	, RR.ReportsToID
	, RR.TeamID
	
	, RR.SocialSecCardStatusID
	, RR.DriversLicenseStatusID
	, RR.W9StatusID
	, RR.I9StatusID
	, RR.W4StatusID
	, RR.SocialSecCardNotes
	, RR.DriversLicenseNotes
	, RR.W9Notes
	, RR.I9Notes
	, RR.W4Notes
	
	, RR.IsActive AS IsActiveRecruit
	, RR.IsDeleted AS IsDeletedRecruit
	
	, RUT.UserTypeID
	, RUT.Description AS UserType
	, RUT.RoleLocationID
	, RUT.SecurityLevel
	, RUT.ReportingLevel
	
	, RUTTT.UserTypeTeamTypeID
	, RUTTT.Description AS UserTypeTeamType
	
	--, PS.PayscaleID
	--, PS.Name AS PayScaleName
	, RR.AlternatePayScheduleID

	, CAST(CASE
		WHEN (RU.IsActive = 1 OR RR.IsActive = 1) THEN 1
		ELSE 0
	END AS BIT) AS IsActive
	, CAST(CASE
		WHEN (RU.IsDeleted = 1 OR RR.IsDeleted = 1) THEN 1
		ELSE 0
	END AS BIT) AS IsDeleted
	
	, CAST(COALESCE(RR.IsServiceTech, 0) AS BIT) AS IsServiceTech

	, RS.IsVisibleToRecruits AS SeasonIsVisibleToRecruits
	, RS.StartDate AS SeasonStartDate
	, RS.EndDate AS SeasonEndDate
	, RS.IsCurrent AS SeasonIsCurrent
	, RS.IsActive AS SeasonIsActive
	, RS.IsDeleted AS SeasonIsDeleted
	, RS.SeasonName AS SeasonName
	
	--, MAP.TeamID AS ActualTeamID
	--, MAP.TeamLocationID AS ActualTeamLocationID

FROM 
	RU_Users AS RU WITH(NOLOCK)
	INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
	ON
		RU.UserID = RR.UserID
	INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
	ON
		RR.UserTypeID = RUT.UserTypeID	
	INNER JOIN RU_UserTypeTeamTypes AS RUTTT WITH(NOLOCK)
	ON
		RUT.UserTypeTeamTypeID = RUTTT.UserTypeTeamTypeID
	--LEFT JOIN RU_Payscales AS PS WITH(NOLOCK)
	--ON
	--	(COALESCE(RR.PayscaleID, 1) = PS.PayscaleID)
	INNER JOIN RU_Season AS RS WITH (NOLOCK)
	ON
		RR.SeasonID = RS.SeasonID
	--LEFT OUTER JOIN SAE_RecruitTeamMappings AS MAP
	--ON
	--	RR.RecruitID = MAP.RecruitID
GO

SELECT 
	*
FROM
	vwRU_RecruitUser AS VRUR
WHERE
	(VRUR.GPEmployeeID = 'LEDUM001')
	AND (VRUR.SeasonID = 4)
