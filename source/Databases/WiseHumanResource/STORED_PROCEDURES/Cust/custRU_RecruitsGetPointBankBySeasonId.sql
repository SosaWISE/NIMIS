USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetPointBankBySeasonId')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetPointBankBySeasonId'
		DROP  Procedure  dbo.custRU_RecruitsGetPointBankBySeasonId
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetPointBankBySeasonId'
GO
/******************************************************************************
**		File: custRU_RecruitsGetPointBankBySeasonId.sql
**		Name: custRU_RecruitsGetPointBankBySeasonId
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
CREATE Procedure dbo.custRU_RecruitsGetPointBankBySeasonId
(
	@GPEmployeeID NVarChar(25)
	, @SeasonID INT
)
WITH RECOMPILE
AS
BEGIN

	IF @SeasonID < 12
	BEGIN

		SELECT
			SUM(MSA.SalesRepPointBank) AS [Point Bank]
		FROM
			[Platinum_Protection_InterimCRM].[dbo].MS_Account MSA WITH (NOLOCK)
			INNER JOIN RU_TeamLocations AS TeamLocs
			ON
				(MSA.TeamLocationID = TeamLocs.TeamLocationID)
				AND (TeamLocs.SeasonID = @SeasonID) -- Filter TeamLocation By Season
		WHERE
			(MSA.GPSalesRepID = @GPEmployeeID)
		GROUP BY
			MSA.GPSalesRepID

	END
	ELSE
	BEGIN

		SELECT
			CAST
			(
				CASE
					WHEN COALESCE(T1.AccountPointBank, 0) > 0 THEN COALESCE(T1.AccountPointBank, 0)
					WHEN ABS(COALESCE(T1.AccountPointBank, 0)) < RPS.StartingPointBank THEN 0
					ELSE RPS.StartingPointBank + COALESCE(T1.AccountPointBank, 0)
				END 
			AS DECIMAL(8, 3)) AS [Point Bank]
		FROM		
			VW_RecruitPayrollStatus AS RPS WITH (NOLOCK)
			LEFT JOIN
			(
				SELECT
					GPSalesRepID
					, SeasonID
					, SUM(COALESCE(SalesRepPointBank, 0)) AS AccountPointBank
				FROM
					SAE_AccountsInstalled AS AI WITH (NOLOCK)
					INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_Account AS ACCT WITH (NOLOCK)
					ON
						(ACCT.AccountID = AI.AccountID)					
				WHERE
					AI.Status = 'OK'
				GROUP BY
					GPSalesRepID
					, SeasonID
			) AS T1
			ON
				(T1.GPSalesRepID = RPS.GPEmployeeID)
				AND (T1.SeasonID = RPS.SeasonID)
		WHERE
			(RPS.GPEmployeeID = @GPEmployeeID)
			AND (RPS.SeasonID = @SeasonID)

	END
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetPointBankBySeasonId TO PUBLIC
GO