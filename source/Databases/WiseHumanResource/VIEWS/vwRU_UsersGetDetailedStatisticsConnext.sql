USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwRU_UsersGetDetailedStatisticsConnext')
	BEGIN
		PRINT 'Dropping VIEW vwRU_UsersGetDetailedStatisticsConnext'
		DROP VIEW dbo.vwRU_UsersGetDetailedStatisticsConnext
	END
GO

PRINT 'Creating VIEW vwRU_UsersGetDetailedStatisticsConnext'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwRU_UsersGetDetailedStatisticsConnext.sql
**		Name: vwRU_UsersGetDetailedStatisticsConnext
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
CREATE VIEW [dbo].[vwRU_UsersGetDetailedStatisticsConnext]
AS

SELECT 
	RU_Users.UserID AS UserID,
	RU_Users.FirstName as FirstName,
	RU_Users.LastName as LastName,
	CONVERT(INT,2014) AS SalesYear,
	CONVERT(INT,11) AS SalesMonth,
	CONVERT(INT,1) AS RegionID,
	RU_Salesperson.RegionName AS RegionName,
	CONVERT(INT,2) AS TeamID,
	RU_Salesperson.TeamName AS TeamName,
	CASE 
		WHEN RU_Salesperson.OfficeName = 'SEATTLE' THEN CONVERT(INT,1)
		WHEN RU_Salesperson.OfficeName = 'DENVER' THEN CONVERT(INT,2)
		WHEN RU_Salesperson.OfficeName = 'NEW YORK' THEN CONVERT(INT,3)
		ELSE CONVERT(INT,0)
	END AS OfficeID,
	RU_Salesperson.OfficeName AS OfficeName,
	CONVERT(BIT,1) AS HasRecruits,

	--CREDIT COLUMNS
	CONVERT(INT,246) AS NumberCreditReportsPulled,
	CONVERT(INT,185) AS NumberCreditsPassed,
	CONVERT(INT,131) AS NumberExcellentCreditScores,
	CONVERT(INT,16) AS NumberGoodCreditScores,
	CONVERT(INT,0) AS NumberBadCreditScores,
	CONVERT(INT,724) AS AverageCreditScore,
	CONVERT(DECIMAL(5,2),75) AS CreditPassPercentage,
	CONVERT(DECIMAL(5,2),79) AS PassAndInstallPercentage,

	CONVERT(INT,27) AS NumberCancels,
	CONVERT(INT,120) AS NumberNetSales,

	-- SURVEY COLUMNNS
	CONVERT(INT,168) AS NumberPresurveys,
	CONVERT(INT,160) AS NumberPostsurveys,

	CONVERT(INT,147) AS NumberInstallations,
	CONVERT(INT,138) AS NumberSameDayInstallations,
	CONVERT(DECIMAL(5,2),94) AS SameDayInstallationPercentage,

	CONVERT(INT,2) AS NumberActivationsWaived,
	CONVERT(DECIMAL(5,2),1) AS ActivationsWaivedPercentage,

	-- BILLING COLUMNS
	CONVERT(INT,50) AS NumberCCPayments,
	CONVERT(INT,60) AS NumberACHPayments,
	CONVERT(INT,10) AS NumberInvoicePayments,
	CONVERT(INT,20) AS NumberSystemsOver8Points,
	CONVERT(INT,30) AS NumberFreePointsGivenBySalesRep,
	CONVERT(INT,20) AS NumberFreePointsGivenByTech

FROM 
	dbo.RU_Users WITH(NOLOCK)
	JOIN dbo.RU_Salesperson WITH(NOLOCK)
		ON RU_Users.UserID = RU_Salesperson.UserID

GO
/* TEST */
-- SELECT * FROM vwRU_UsersGetDetailedStatisticsConnext
