-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
DROP PROCEDURE custGetCreditScoreBreakdown
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Del Stirling
-- Create date: 12/10/2008
-- Description:	Returns the credit score groupings of all active accounts that are still with Platinum.
-- =============================================
CREATE PROCEDURE custGetCreditScoreBreakdown
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #AllCust 
		(
			AccountID int
			, AcctStatus varchar(4)
			, Purchaser varchar(20)
			, Performing bit
		)
	DELETE FROM #AllCust
	INSERT INTO #AllCust
	SELECT convert(int, cust.CUSTNMBR)
		, cust.UPSZONE 
		, cust.SHIPMTHD
		, CASE 
			WHEN cust.INACTIVE = 0 
				AND age.AGPERAMT_5 = 0
				AND age.AGPERAMT_6 = 0
				AND age.AGPERAMT_7 = 0
			THEN 1
			ELSE 0
		  END AS Performing
	FROM RM00101 cust
		INNER JOIN RM00103 age
			ON age.CUSTNMBR = cust.CUSTNMBR
	WHERE isnumeric(cust.CUSTNMBR) > 0 AND len(cust.CUSTNMBR) = 6
		AND cust.UPSZONE in ('', 'INH','CHB','NS','MR','*FW','COL') 
		AND cust.INACTIVE = 0
		AND cust.CUSTNMBR IN 
			(
			SELECT convert(varchar, AccountID)
			FROM Platinum_Protection_InterimCRM.dbo.MS_AccountStatus 
			WHERE InactiveDate IS NULL
				AND InstallDate IS NOT NULL
				AND CancelDate IS NULL
				AND NOCDate IS NULL
				AND SentToCollectionsDate IS NULL
			)

	SELECT 'Performing In-House Accounts' AS AccountType
		, sum(CASE WHEN isNull(crdt.CreditScore, 0) < 550 THEN 1 ELSE 0 END) AS [549 Below]
		, sum(CASE WHEN crdt.CreditScore >= 550 AND crdt.CreditScore < 575 THEN 1 ELSE 0 END) AS [550-574]
		, sum(CASE WHEN crdt.CreditScore >= 575 AND crdt.CreditScore < 600 THEN 1 ELSE 0 END) AS [575-599]
		, sum(CASE WHEN crdt.CreditScore >= 600 AND crdt.CreditScore < 625 THEN 1 ELSE 0 END) AS [600-624]
		, sum(CASE WHEN crdt.CreditScore >= 625 AND crdt.CreditScore < 650 THEN 1 ELSE 0 END) AS [625-649]
		, sum(CASE WHEN crdt.CreditScore >= 650 AND crdt.CreditScore < 675 THEN 1 ELSE 0 END) AS [650-674]
		, sum(CASE WHEN crdt.CreditScore >= 675 AND crdt.CreditScore < 700 THEN 1 ELSE 0 END) AS [675-699]
		, sum(CASE WHEN crdt.CreditScore >= 700 THEN 1 ELSE 0 END) AS [700 +]
	FROM #AllCust acust
		LEFT JOIN Platinum_Protection_Recruiting.dbo.SAE_MaxCredit crdt
			ON crdt.AccountID = acust.AccountID
	WHERE acust.Performing = 1
	UNION
	SELECT 'Performing Far West Collateralized Accounts' AS AccountType
		, sum(CASE WHEN isNull(crdt.CreditScore, 0) < 550 THEN 1 ELSE 0 END) AS [549 Below]
		, sum(CASE WHEN crdt.CreditScore >= 550 AND crdt.CreditScore < 575 THEN 1 ELSE 0 END) AS [550-574]
		, sum(CASE WHEN crdt.CreditScore >= 575 AND crdt.CreditScore < 600 THEN 1 ELSE 0 END) AS [575-599]
		, sum(CASE WHEN crdt.CreditScore >= 600 AND crdt.CreditScore < 625 THEN 1 ELSE 0 END) AS [600-624]
		, sum(CASE WHEN crdt.CreditScore >= 625 AND crdt.CreditScore < 650 THEN 1 ELSE 0 END) AS [625-649]
		, sum(CASE WHEN crdt.CreditScore >= 650 AND crdt.CreditScore < 675 THEN 1 ELSE 0 END) AS [650-674]
		, sum(CASE WHEN crdt.CreditScore >= 675 AND crdt.CreditScore < 700 THEN 1 ELSE 0 END) AS [675-699]
		, sum(CASE WHEN crdt.CreditScore >= 700 THEN 1 ELSE 0 END) AS [700 +]
	FROM #AllCust acust
		LEFT JOIN Platinum_Protection_Recruiting.dbo.SAE_MaxCredit crdt
			ON crdt.AccountID = acust.AccountID
	WHERE acust.AcctStatus IN ('COL','*FW')
		AND acust.Purchaser <> 'MONITRONICS'
		AND acust.Performing = 1
	UNION
	SELECT 'All Far West Collateralized Accounts' AS AccountType
		, sum(CASE WHEN isNull(crdt.CreditScore, 0) < 550 THEN 1 ELSE 0 END) AS [549 Below]
		, sum(CASE WHEN crdt.CreditScore >= 550 AND crdt.CreditScore < 575 THEN 1 ELSE 0 END) AS [550-574]
		, sum(CASE WHEN crdt.CreditScore >= 575 AND crdt.CreditScore < 600 THEN 1 ELSE 0 END) AS [575-599]
		, sum(CASE WHEN crdt.CreditScore >= 600 AND crdt.CreditScore < 625 THEN 1 ELSE 0 END) AS [600-624]
		, sum(CASE WHEN crdt.CreditScore >= 625 AND crdt.CreditScore < 650 THEN 1 ELSE 0 END) AS [625-649]
		, sum(CASE WHEN crdt.CreditScore >= 650 AND crdt.CreditScore < 675 THEN 1 ELSE 0 END) AS [650-674]
		, sum(CASE WHEN crdt.CreditScore >= 675 AND crdt.CreditScore < 700 THEN 1 ELSE 0 END) AS [675-699]
		, sum(CASE WHEN crdt.CreditScore >= 700 THEN 1 ELSE 0 END) AS [700 +]
	FROM #AllCust acust
		LEFT JOIN Platinum_Protection_Recruiting.dbo.SAE_MaxCredit crdt
			ON crdt.AccountID = acust.AccountID
	WHERE acust.AcctStatus IN ('COL','*FW')
		AND acust.Purchaser <> 'MONITRONICS'
	UNION
	SELECT 'Performing Monitronics Collateralized Accounts' AS AccountType
		, sum(CASE WHEN isNull(crdt.CreditScore, 0) < 550 THEN 1 ELSE 0 END) AS [549 Below]
		, sum(CASE WHEN crdt.CreditScore >= 550 AND crdt.CreditScore < 575 THEN 1 ELSE 0 END) AS [550-574]
		, sum(CASE WHEN crdt.CreditScore >= 575 AND crdt.CreditScore < 600 THEN 1 ELSE 0 END) AS [575-599]
		, sum(CASE WHEN crdt.CreditScore >= 600 AND crdt.CreditScore < 625 THEN 1 ELSE 0 END) AS [600-624]
		, sum(CASE WHEN crdt.CreditScore >= 625 AND crdt.CreditScore < 650 THEN 1 ELSE 0 END) AS [625-649]
		, sum(CASE WHEN crdt.CreditScore >= 650 AND crdt.CreditScore < 675 THEN 1 ELSE 0 END) AS [650-674]
		, sum(CASE WHEN crdt.CreditScore >= 675 AND crdt.CreditScore < 700 THEN 1 ELSE 0 END) AS [675-699]
		, sum(CASE WHEN crdt.CreditScore >= 700 THEN 1 ELSE 0 END) AS [700 +]
	FROM #AllCust acust
		LEFT JOIN Platinum_Protection_Recruiting.dbo.SAE_MaxCredit crdt
			ON crdt.AccountID = acust.AccountID
	WHERE acust.AcctStatus IN ('COL','*FW')
		AND acust.Purchaser = 'MONITRONICS'
		AND acust.Performing = 1
	UNION
	SELECT 'All Monitronics Collateralized Accounts' AS AccountType
		, sum(CASE WHEN isNull(crdt.CreditScore, 0) < 550 THEN 1 ELSE 0 END) AS [549 Below]
		, sum(CASE WHEN crdt.CreditScore >= 550 AND crdt.CreditScore < 575 THEN 1 ELSE 0 END) AS [550-574]
		, sum(CASE WHEN crdt.CreditScore >= 575 AND crdt.CreditScore < 600 THEN 1 ELSE 0 END) AS [575-599]
		, sum(CASE WHEN crdt.CreditScore >= 600 AND crdt.CreditScore < 625 THEN 1 ELSE 0 END) AS [600-624]
		, sum(CASE WHEN crdt.CreditScore >= 625 AND crdt.CreditScore < 650 THEN 1 ELSE 0 END) AS [625-649]
		, sum(CASE WHEN crdt.CreditScore >= 650 AND crdt.CreditScore < 675 THEN 1 ELSE 0 END) AS [650-674]
		, sum(CASE WHEN crdt.CreditScore >= 675 AND crdt.CreditScore < 700 THEN 1 ELSE 0 END) AS [675-699]
		, sum(CASE WHEN crdt.CreditScore >= 700 THEN 1 ELSE 0 END) AS [700 +]
	FROM #AllCust acust
		LEFT JOIN Platinum_Protection_Recruiting.dbo.SAE_MaxCredit crdt
			ON crdt.AccountID = acust.AccountID
	WHERE acust.AcctStatus IN ('COL','*FW')
		AND acust.Purchaser = 'MONITRONICS'
	UNION
	SELECT 'All In-House Accounts' AS AccountType
		, sum(CASE WHEN isNull(crdt.CreditScore, 0) < 550 THEN 1 ELSE 0 END) AS [549 Below]
		, sum(CASE WHEN crdt.CreditScore >= 550 AND crdt.CreditScore < 575 THEN 1 ELSE 0 END) AS [550-574]
		, sum(CASE WHEN crdt.CreditScore >= 575 AND crdt.CreditScore < 600 THEN 1 ELSE 0 END) AS [575-599]
		, sum(CASE WHEN crdt.CreditScore >= 600 AND crdt.CreditScore < 625 THEN 1 ELSE 0 END) AS [600-624]
		, sum(CASE WHEN crdt.CreditScore >= 625 AND crdt.CreditScore < 650 THEN 1 ELSE 0 END) AS [625-649]
		, sum(CASE WHEN crdt.CreditScore >= 650 AND crdt.CreditScore < 675 THEN 1 ELSE 0 END) AS [650-674]
		, sum(CASE WHEN crdt.CreditScore >= 675 AND crdt.CreditScore < 700 THEN 1 ELSE 0 END) AS [675-699]
		, sum(CASE WHEN crdt.CreditScore >= 700 THEN 1 ELSE 0 END) AS [700 +]	 	
	FROM #AllCust acust
		LEFT JOIN Platinum_Protection_Recruiting.dbo.SAE_MaxCredit crdt
			ON crdt.AccountID = acust.AccountID
END
GO


