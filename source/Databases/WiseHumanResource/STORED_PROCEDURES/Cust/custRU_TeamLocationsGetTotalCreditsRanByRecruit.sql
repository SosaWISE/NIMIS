USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetTotalCreditsRanByRecruit')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetTotalCreditsRanByRecruit'
		DROP  Procedure  dbo.custRU_TeamLocationsGetTotalCreditsRanByRecruit
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetTotalCreditsRanByRecruit'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetTotalCreditsRanByRecruit.sql
**		Name: custRU_TeamLocationsGetTotalCreditsRanByRecruit
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
CREATE Procedure dbo.custRU_TeamLocationsGetTotalCreditsRanByRecruit
(
	@StartDate DATETIME
	, @EndDate DATETIME
	, @TeamLocationID INT
)
AS
BEGIN
	
--	DECLARE @StartDate DATETIME
--	SET @StartDate = '4/17/2008 4:00:00'
--
--	DECLARE @EndDate DATETIME
--	SET @EndDate = '9/2/2008 3:59:59.997'
--
--	DECLARE @TeamLocationID INT
--	SET @TeamLocationID = 117

	SELECT DISTINCT UserID
		, FullName
		, PublicFullName
		, SUM(
			CASE
				WHEN GroupNum = 8 THEN 1
				ELSE 0
			END) AS EightToOne
		, SUM(
			CASE
				WHEN GroupNum = 1 THEN 1
				ELSE 0
			END) AS OneToThree
		, SUM(
			CASE
				WHEN GroupNum = 3 THEN 1
				ELSE 0
			END) AS ThreeToFive
		, SUM(
			CASE
				WHEN GroupNum = 5 THEN 1
				ELSE 0
			END) AS FiveToSeven
		, SUM(
			CASE
				WHEN GroupNum = 7 THEN 1
				ELSE 0
			END) AS SevenToNine
		, SUM(
			CASE
				WHEN GroupNum = 9 THEN 1
				ELSE 0
			END) AS AfterNine
		, COUNT(CreditScore) AS Total
	FROM 
	(
		SELECT RU.UserID
			, RU.FullName
			, RU.PublicFullName
			, CR.CreditScore
			, CASE
					WHEN DATEPART(HOUR, (DATEADD(HOUR, -RUTL.TimeZoneOffset, CR.QualificationDate))) IN (8,9,10,11,12) THEN 8
					WHEN DATEPART(HOUR, (DATEADD(HOUR, -RUTL.TimeZoneOffset, CR.QualificationDate))) IN (13,14) THEN 1
					WHEN DATEPART(HOUR, (DATEADD(HOUR, -RUTL.TimeZoneOffset, CR.QualificationDate))) IN (15,16) THEN 3
					WHEN DATEPART(HOUR, (DATEADD(HOUR, -RUTL.TimeZoneOffset, CR.QualificationDate))) IN (17,18) THEN 5
					WHEN DATEPART(HOUR, (DATEADD(HOUR, -RUTL.TimeZoneOffset, CR.QualificationDate))) IN (19,20) THEN 7
					WHEN DATEPART(HOUR, (DATEADD(HOUR, -RUTL.TimeZoneOffset, CR.QualificationDate))) IN (21,22,23,24,0,1,2,3,4,5,6,7) THEN 9
					ELSE 0
				END AS GroupNum
		FROM SAE_CreditsRun AS CR WITH (NOLOCK)
		INNER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
		ON
			CR.TeamLocationID = RUTL.TeamLocationID
		LEFT OUTER JOIN RU_Users AS RU WITH (NOLOCK)
		ON
			(RU.GPEmployeeID = CR.GPSalesRepID)
		WHERE
			(CR.QualificationDate BETWEEN @StartDate AND @EndDate)
			AND (CR.TeamLocationID = @TeamLocationID)
	) AS Qualifications		
	GROUP BY
		UserID
		, FullName
		, PublicFullName
	ORDER BY
		FullName

END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetTotalCreditsRanByRecruit TO PUBLIC
GO