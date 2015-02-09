USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetSalesTotals')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetSalesTotals'
		DROP  Procedure  dbo.custRU_RecruitsGetSalesTotals
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetSalesTotals'
GO
/******************************************************************************
**		File: custRU_RecruitsGetSalesTotals.sql
**		Name: custRU_RecruitsGetSalesTotals
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
CREATE Procedure dbo.custRU_RecruitsGetSalesTotals
(
	@SeasonID INT
)
AS
BEGIN
	
	--	DECLARE @SeasonID INT
	--	SET @SeasonID = 6

	-- Sales
	DECLARE @Sales TABLE
	(
		AccountID INT
		, GPSalesRepID NVARCHAR(25)
		, InstallDate DATETIME
	)
	INSERT INTO @Sales
	SELECT
		MSA.AccountID
		, MSA.GPSalesRepID
		, ST.InstallDate
	FROM Platinum_Protection_InterimCRM.dbo.MS_Account AS MSA WITH (NOLOCK)
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_AccountStatus AS ST WITH (NOLOCK)
	ON
		MSA.AccountID = ST.AccountID
	INNER JOIN RU_TeamLocations AS TeamLocs
	ON
		MSA.TeamLocationID = TeamLocs.TeamLocationID
		AND TeamLocs.SeasonID = @SeasonID -- Filter TeamLocation By Season
	LEFT OUTER JOIN Platinum_Protection_InterimCRM.dbo.MS_AccountStatus AS MSAS WITH (NOLOCK)
	ON
		MSA.AccountID = MSAS.AccountID
		AND MSAS.NOCDate IS NULL
		AND MSAS.CancelDate IS NULL
	WHERE
		ST.InstallDate IS NOT NULL
		AND GPSalesRepID IS NOT NULL


	SELECT
		RR.*
		, RU.*
		, SalesTotals.TotalSales
	FROM RU_Recruits AS RR WITH (NOLOCK)
	INNER JOIN RU_Users AS RU WITH (NOLOCK)
	ON
		RR.UserID = RU.UserID
	INNER JOIN
	(
		SELECT
			GPSalesRepID
			, COUNT(GPSalesRepID) AS TotalSales
		FROM @Sales
		GROUP BY
			GPSalesRepID
	) AS SalesTotals
	ON
		RU.GPEmployeeID = SalesTotals.GPSalesRepID
	WHERE
		RR.SeasonID = @SeasonID
	ORDER BY
		SalesTotals.TotalSales DESC

END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetSalesTotals TO PUBLIC
GO