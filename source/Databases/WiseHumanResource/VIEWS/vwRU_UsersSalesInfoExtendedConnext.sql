USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwRU_UsersSalesInfoExtendedConnext')
	BEGIN
		PRINT 'Dropping VIEW vwRU_UsersSalesInfoExtendedConnext'
		DROP VIEW dbo.vwRU_UsersSalesInfoExtendedConnext
	END
GO

PRINT 'Creating VIEW vwRU_UsersSalesInfoExtendedConnext'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwRU_UsersSalesInfoExtendedConnext.sql
**		Name: vwRU_UsersSalesInfoExtendedConnext
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
CREATE VIEW [dbo].[vwRU_UsersSalesInfoExtendedConnext]
AS
	-- Enter Query here
	SELECT
		SalesInfo.UserID
		, SalesInfo.FirstName
		, SalesInfo.MiddleName
		, SalesInfo.LastName
		, SalesInfo.PhotoURL
		, SalesInfo.MLMDepth
		, SalesInfo.ManagerHasOwnTeam
		, SalesInfo.RegionName
		, SalesInfo.OfficeName
		, SalesInfo.TeamName
		, SalesInfo.CurrentNationalRank
		, SalesInfo.PreviousNationalRank
		, SalesInfo.CurrentRegionalRank
		, SalesInfo.PreviousRegionalRank
		, SalesInfo.CurrentOfficeRank
		, SalesInfo.PreviousOfficeRank
		, SalesInfo.CurrentTeamRank
		, SalesInfo.PreviousTeamRank
		, SalesInfo.StartDate
		, CASE 
			WHEN RU.PhoneCell IS NOT NULL THEN PhoneCell
			ELSE RU.PhoneHome
		  END AS Phone
		, SalesInfo.Email
		, ADDR.StreetAddress AS StreetAddress
		, CASE WHEN ADDR.StreetAddress2 IS NULL THEN '' ELSE ADDR.StreetAddress2 END AS StreetAddress2
		, ADDR.City AS City
		, States.StateAB AS State
		, ADDR.PostalCode AS Zip
		, CAST(7 AS INT) AS WeeklySalesGoal
		, CAST(30 AS INT) AS MonthlySalesGoal
		, CAST(257 AS INT) AS YearlySalesGoal
		, CAST(10 AS FLOAT) AS WeeklyQualityGoal
		, CAST(9.75 AS FLOAT) AS MonthlyQualityGoal
		, CAST(9.5 AS FLOAT) AS YearlyQualityGoal
		, '2014SalesAgreement' as License1
		, 'www.nexsense.com' AS License1URL
		, 'GothamCitySolicitationLicense' as License2
		, 'www.batman.com' AS License2URL
		, 'MetropolisSolicitationLicense' as License3
		, 'www.superman.com' AS License3URL
	FROM
		dbo.vwRU_UsersSalesInfoConnext AS SalesInfo WITH (NOLOCK)
		JOIN dbo.RU_Users as RU WITH(NOLOCK)
			ON SalesInfo.UserID = RU.UserID
		LEFT JOIN dbo.RU_RecruitAddresses AS ADDR WITH(NOLOCK)
			ON RU.PermanentAddressId = ADDR.AddressId
		LEFT JOIN dbo.MC_PoliticalStates AS States WITH(NOLOCK)
			ON ADDR.StateId = States.StateID
		-- GOALS
		-- LICENSING
GO
/* TEST */
-- SELECT * FROM vwRU_UsersSalesInfoExtendedConnext
