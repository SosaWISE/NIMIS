USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custReport_SalesRepRankings')
	BEGIN
		PRINT 'Dropping Procedure custReport_SalesRepRankings'
		DROP  Procedure  dbo.custReport_SalesRepRankings
	END
GO

PRINT 'Creating Procedure custReport_SalesRepRankings'
GO
/******************************************************************************
**		File: custReport_SalesRepRankings.sql
**		Name: custReport_SalesRepRankings
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
**		Auth: Andres Sosa
**		Date: 07/01/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/01/2015	Andres Sosa		Creating the report
**	
*******************************************************************************/
CREATE Procedure dbo.custReport_SalesRepRankings
(
	@officeId INT = NULL
	, @salesRepId VARCHAR(50) = NULL
	, @DealerId INT = 5000
	, @startDate DATETIME
	, @endDate DATETIME
)
AS
BEGIN
	SELECT
		 DAT1.SalesRepId
		, DAT1.SalesRepName
		, DAT1.NumOfSales
		, ROW_NUMBER() OVER (PARTITION BY DAT1.[RowCount] ORDER BY DAT1.NumOfSales DESC) AS Ranking
	FROM
	(
		SELECT 
			DTA.SalesRepID
			, RU.FullName AS SalesRepName
			, 1 AS [RowCount]
			, COUNT(*) AS NumOfSales
		FROM 
			[WISE_CRM].[dbo].[SAE_ReportsPerformanceAllData] AS DTA WITH (NOLOCK)
			INNER JOIN [WISE_HumanResource].[dbo].RU_Users AS RU WITH (NOLOCK)
			ON
				(DTA.SalesRepId = RU.GPEmployeeId)
		WHERE
			(DTA.InstallDate IS NOT NULL AND (DTA.InstallDate BETWEEN @startDate AND @endDate))
		GROUP BY
			DTA.SalesRepId
			, RU.FullName
	) AS DAT1
	ORDER BY
		DAT1.NumOfSales DESC;
END
GO

GRANT EXEC ON dbo.custReport_SalesRepRankings TO PUBLIC
GO

/*
*/
DECLARE @officeId INT = NULL
	, @salesRepId VARCHAR(50) = NULL
	, @DealerId INT = 5000
	, @startDate DATETIME = '2013-02-01 06:00:00'
	, @endDate DATETIME = GETUTCDATE();

EXEC dbo.custReport_SalesRepRankings @officeId, NULL, @DealerId, @startDate, @endDate