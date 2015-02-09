USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustPayrollGetTotalBackendPayments')
	BEGIN
		PRINT 'Dropping Procedure ppCustPayrollGetTotalBackendPayments'
		DROP  Procedure  dbo.ppCustPayrollGetTotalBackendPayments
	END
GO

PRINT 'Creating Procedure ppCustPayrollGetTotalBackendPayments'
GO
/******************************************************************************
**		File: ppCustPayrollGetTotalBackendPayments.sql
**		Name: ppCustPayrollGetTotalBackendPayments
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
**		Date: 
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**			
*******************************************************************************/
CREATE Procedure dbo.ppCustPayrollGetTotalBackendPayments
(
	@GPEmployeeID NVARCHAR(20)
	, @StartDate DATETIME
	, @EndDate DATETIME
)
AS
BEGIN

	SELECT
		COALESCE(SUM(Amount), 0) AS Amount
	FROM
	(
		SELECT 
			CASE
				WHEN pm302.DOCTYPE = 5 THEN -1 * pm302.DOCAMNT
				ELSE pm302.DOCAMNT
			END AS Amount
		FROM 
			PM30200 pm302
			INNER JOIN PM00200 pm200
			ON
				(pm200.VENDORID = pm302.VENDORID)
		WHERE
			(pm302.DocDate BETWEEN @StartDate AND @EndDate) 
			AND (pm302.Voided = 0)					-- Exclude voided transactions
			AND (pm302.DOCNUMBR LIKE '%BKEND%'
				OR pm302.DOCNUMBR LIKE '%BK%'
				OR pm302.DOCNUMBR LIKE '%BACK%'
				OR pm302.DOCNUMBR LIKE '%BK-END%'
				OR pm302.DOCNUMBR LIKE '%BACK END%'
				OR pm302.DOCNUMBR LIKE '%BACK-END%'
				OR pm302.DOCNUMBR LIKE '%BACKEND%')
			AND pm302.VENDORID = @GPEmployeeID

		UNION

		SELECT
			upr303.UPRTRXAM * paycd.Multiplier AS Amount
		FROM
			UPR00100 upr100
			INNER JOIN UPR30100 upr301
			ON
				(upr301.EMPLOYID = upr100.EMPLOYID)
			INNER JOIN UPR30300 upr303
			ON
				(upr303.CHEKNMBR = upr301.CHEKNMBR)
			INNER JOIN 
			(
				SELECT PAYRCORD, DSCRIPTN, 1 AS Multiplier, 1 AS PYRLRTYP FROM UPR40600
				UNION
				SELECT DEDUCTON, DSCRIPTN, -1 AS Multiplier, 2 AS PYRLRTYP FROM UPR40900 
			) paycd
			ON
				(paycd.PAYRCORD = upr303.PAYROLCD AND paycd.PYRLRTYP = UPR303.PYRLRTYP)
		WHERE
			(upr100.EMPLOYID = @GPEmployeeID)
			AND (upr301.CHEKDATE BETWEEN @StartDate AND @EndDate)
			AND (upr301.VOIDED = 0)					-- Exclude voided transactions
			AND (upr303.UPRTRXAM <> 0)				-- Exclude zero amount transactions 
			AND (paycd.PAYRCORD = 'BK-END')			-- Only get Backend Transactions
	) AS Data
	
END
GO

GRANT EXEC ON dbo.ppCustPayrollGetTotalBackendPayments TO PUBLIC
GO