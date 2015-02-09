USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetManagersRoster')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetManagersRoster'
		DROP  Procedure  dbo.custRU_RecruitsGetManagersRoster
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetManagersRoster'
GO
/******************************************************************************
**		File: custRU_RecruitsGetManagersRoster.sql
**		Name: custRU_RecruitsGetManagersRoster
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
CREATE Procedure dbo.custRU_RecruitsGetManagersRoster
(
	@seasonID int, --The ID of the season for the roster
	@oldSeasonID int --The ID of the season for the Old Area
)
AS
BEGIN
	DECLARE @ManagersUserTypes TABLE (UserTypeID INT)
	--INSERT INTO @ManagersUserTypes (UserTypeID) VALUES (1) -- Administrator
	INSERT INTO @ManagersUserTypes (UserTypeID) VALUES (2) -- Sales Manager
	INSERT INTO @ManagersUserTypes (UserTypeID) VALUES (3) -- Sales Co-Manager
	INSERT INTO @ManagersUserTypes (UserTypeID) VALUES (4) -- Sales Assistant Manager
	--INSERT INTO @ManagersUserTypes (UserTypeID) VALUES (5) -- Sales Rep
	INSERT INTO @ManagersUserTypes (UserTypeID) VALUES (6) -- Technician Lead
	--INSERT INTO @ManagersUserTypes (UserTypeID) VALUES (7) -- Technician
	INSERT INTO @ManagersUserTypes (UserTypeID) VALUES (8) -- Regional Manager - Technician
	INSERT INTO @ManagersUserTypes (UserTypeID) VALUES (10) -- Technician Assistant Lead
	INSERT INTO @ManagersUserTypes (UserTypeID) VALUES (11) -- Regional Manager - Sales
	--INSERT INTO @ManagersUserTypes (UserTypeID) VALUES (12) -- Corporate
	--INSERT INTO @ManagersUserTypes (UserTypeID) VALUES (13) -- Office Assistant

	DECLARE @RegionalUserTypes TABLE (UserTypeID INT)
	INSERT INTO @RegionalUserTypes (UserTypeID) VALUES (8) -- Regional Manager - Technician
	INSERT INTO @RegionalUserTypes (UserTypeID) VALUES (11) -- Regional Manager - Sales

	SELECT DISTINCT
		RU.UserID
		, RR.RecruitID
		, RR.ReportsToID AS RegionalManagerID
		, RU.FullName AS ManagerName
		, UT.Description AS ManagerType
		, RUR.FullName AS RegionalManager
		, RR.Location AS Location
		, RU.PhoneCell AS CellPhone
		, RU.PhoneHome AS HomePhone
		, RU.Email AS PersonalEmail
		, RU.CorporateEmail AS CompanyEmail
		, RU.SpouseName
		, RT.Description AS TeamName
		, OldOffice.Description AS OldArea
	FROM
		RU_Users AS RU WITH (NOLOCK)
		INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
		ON
			RU.UserID = RR.UserID
		INNER JOIN RU_UserType AS UT WITH (NOLOCK)
		ON
			(RR.UserTypeID = UT.UserTypeID)
			AND (UT.UserTypeID IN (SELECT UserTypeID FROM @ManagersUserTypes))
		INNER JOIN RU_Season AS RS WITH (NOLOCK)
		ON
			RR.SeasonID = RS.SeasonID
		LEFT OUTER JOIN RU_Recruits AS RRR WITH (NOLOCK)
		ON
			RR.ReportsToID = RRR.RecruitID
		LEFT OUTER JOIN RU_Users AS RUR WITH (NOLOCK)
		ON
			RRR.UserID = RUR.UserID
		LEFT OUTER JOIN RU_Teams AS RT WITH (NOLOCK)
		ON
			RT.TeamID = RR.TeamID
		LEFT OUTER JOIN RU_Recruits AS ROLD WITH (NOLOCK)
		ON
			(RU.UserID = ROLD.UserID)
			AND (ROLD.SeasonID = @oldSeasonID)
		LEFT OUTER JOIN (
			SELECT
				RR.RecruitID
				, TL.Description
			FROM
				RU_Recruits AS RR WITH (NOLOCK)
				INNER JOIN RU_Teams AS RT WITH (NOLOCK)
				ON
					RR.TeamID = RT.TeamID
				INNER JOIN RU_TeamLocations AS TL WITH (NOLOCK)
				ON
					RT.TeamLocationID = TL.TeamLocationID
					AND (TL.SeasonID = @oldSeasonID)
		) AS OldOffice
		ON
			ROLD.RecruitID = OldOffice.RecruitID
	WHERE
		RR.SeasonID = @seasonID
	ORDER BY
		RU.FullName

END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetManagersRoster TO PUBLIC
GO