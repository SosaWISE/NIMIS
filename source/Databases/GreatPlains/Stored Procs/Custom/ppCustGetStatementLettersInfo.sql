USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetStatementLettersInfo')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetStatementLettersInfo'
		DROP  Procedure  dbo.ppCustGetStatementLettersInfo
	END
GO

PRINT 'Creating Procedure ppCustGetStatementLettersInfo'
GO
/******************************************************************************
**		File: ppCustGetStatementLettersInfo.sql
**		Name: ppCustGetStatementLettersInfo
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
**		Auth: Brett Kotter
**		Date: 12/09/2009
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	12/09/2009	Brett Kotter	Created	
**	12/22/2009	Brett Kotter	Change to pull all items for a balance
**	12/29/2009	Brett Kotter	Added way to pull in unposted amounts and their totals
**	01/25/2010	Brett Kotter	Convert DueDate to a string instead of it being a datetime
**	03/15/2010	Gregory Short	Added a line to show items that have a dollar amount associated to the item
**	04/05/2010	Gregory Short	Changed the set dates to ensure that we are capturing all the invoices 
*******************************************************************************/
CREATE Procedure dbo.ppCustGetStatementLettersInfo

AS
BEGIN

DECLARE @BeginSearchDate DATETIME
DECLARE @EndSearchDate DATETIME

/* 4/5/2010 - Pulls the day before the last invoices were pulled and appends 11:59 PM */
SET @BeginSearchDate = (SELECT DATEADD(d, -1, CONVERT(varchar(10), MAX(ProcessDate), 101)) + ' 23:59:59.000'
							FROM  [Platinum_Protection_InterimCRM].[dbo].[MC_StatementLettersSent])
/* 4/5/2010 - Pulls the yesterdays date */							
SET @EndSearchDate = (DATEADD(d, -1, CONVERT(varchar(10), getdate(), 101)) + ' 23:59:59.000')


SELECT 
	(CASE 
		WHEN ((BLNC.AGPERAMT_2 > 0) OR (BLNC.AGPERAMT_3 > 0) OR (BLNC.AGPERAMT_4 > 0) OR (BLNC.AGPERAMT_5 > 0) OR (BLNC.AGPERAMT_6 > 0))
		THEN 2  --Late Notice Statement
		ELSE 1  --Regular Notice Statement
		END) AS [SttType]
	, HDR.CUSTNMBR AS [AcctNum]
	, HDR.DOCNUMBR AS [Invoice]
	, CONVERT(nvarchar(10), HDR.DOCDATE, 101) AS [InvDate]
	, HDR.CURTRXAM AS [InvAmt]
	, CUST.CUSTNAME
	, HDR.ITEM
	, (CASE WHEN BAddr.Address1 IS NULL THEN RTRIM(PAddr.Address1) ELSE RTRIM(BAddr.Address1) END) AS 'mail_adr'
	, (CASE WHEN BAddr.Address2 IS NULL THEN RTRIM(PAddr.Address2) ELSE RTRIM(BAddr.Address2) END) AS 'mail_adr2'
	, (CASE WHEN BAddr.CITY IS NULL THEN RTRIM(PAddr.CITY) ELSE RTRIM(BAddr.CITY) END) AS 'mail_City'
	, (CASE WHEN BAddr.STATE IS NULL THEN RTRIM(PAddr.STATE) ELSE RTRIM(BAddr.STATE) END) AS 'mail_st'
	, (CASE WHEN BAddr.ZIP IS NULL THEN RTRIM(PAddr.ZIP) ELSE RTRIM(BAddr.ZIP) END) AS 'mail_zip'
	, ISNULL(upstamt.amt, 0) + BLNC.CUSTBLNC AS [Balance]
	, ISNULL(upstamt.amt, 0) + BLNC.AGPERAMT_1 AS [Current]
	, BLNC.AGPERAMT_2 AS [030]
	, BLNC.AGPERAMT_3 AS [3160]
	, BLNC.AGPERAMT_4 AS [6190]
	, BLNC.AGPERAMT_5 AS [91120]
	, BLNC.AGPERAMT_6 AS [Over120]
	, (CASE WHEN month(getdate()) = 1 THEN 'January'
			WHEN month(getdate()) = 2 THEN 'February'
			WHEN month(getdate()) = 3 THEN 'March'
			WHEN month(getdate()) = 4 THEN 'April'
			WHEN month(getdate()) = 5 THEN 'May'
			WHEN month(getdate()) = 6 THEN 'June'
			WHEN month(getdate()) = 7 THEN 'July'
			WHEN month(getdate()) = 8 THEN 'August'
			WHEN month(getdate()) = 9 THEN 'September'
			WHEN month(getdate()) = 10 THEN 'October'
			WHEN month(getdate()) = 11 THEN 'November'
		ELSE 'December' END) + ' ' + CONVERT(CHAR(2),day(getdate())) + ', ' + CONVERT(CHAR(4), YEAR(getdate())) AS [StmtDate]
	, CONVERT(nvarchar(10), DueDt.DueDate, 101) AS DueDate
FROM (SELECT HDR.CUSTNMBR
				, HDR.DOCNUMBR
				, CONVERT(nvarchar(10), HDR.DOCDATE, 101) AS [DOCDATE]
				, HDR.CURTRXAM
				, (CASE WHEN LNE.ITEMNMBR = LNE.ITEMDESC THEN RTRIM(LNE.ITEMNMBR)
					ELSE RTRIM(LNE.ITEMNMBR) + ' - ' + RTRIM(LNE.ITEMDESC) END) AS ITEM
			FROM RM20101 AS HDR --Closed Invoices
				INNER JOIN SOP30300 AS LNE --Line Items for Closed Invoices
				ON
					(HDR.DOCNUMBR = LNE.SOPNUMBE)
			WHERE HDR.RMDTYPAL = 1
			AND LNE.UNITPRCE > 0 -- Added 3/15/2010 'GShort' to exclude items that don't have a dollar amount associated to the item.
			AND HDR.CURTRXAM > 0
			AND HDR.VOIDSTTS = 0
			AND LEFT(HDR.CUSTNMBR, 4) <> 'MONI'
			UNION
			SELECT HDR.CUSTNMBR
				, HDR.SOPNUMBE
				, CONVERT(nvarchar(10), HDR.DOCDATE, 101)
				, HDR.DOCAMNT
				, (CASE WHEN LNE.ITEMNMBR = LNE.ITEMDESC THEN RTRIM(LNE.ITEMNMBR)
					ELSE RTRIM(LNE.ITEMNMBR) + ' - ' + RTRIM(LNE.ITEMDESC) END) AS ITEM
			FROM SOP10100 AS HDR --Open Invoices
				INNER JOIN SOP10200 AS LNE --Line Information for Open Invoices
				ON
					(HDR.SOPNUMBE = LNE.SOPNUMBE)
			WHERE HDR.DOCAMNT > 0
			AND LNE.UNITPRCE > 0 -- Added 3/15/2010 'GShort' to exclude items that don't have a dollar amount associated to the item.			
			AND HDR.VOIDSTTS = 0
			AND LEFT(HDR.CUSTNMBR, 4) <> 'MONI'
			UNION
			SELECT HDR.CUSTNMBR
				, HDR.DOCNUMBR
				, CONVERT(nvarchar(10), HDR.DOCDATE, 101) AS [DOCDATE]
				, HDR.CURTRXAM
				, 'MONITORING'
			FROM RM20101 AS HDR
			WHERE HDR.RMDTYPAL = 1
			AND HDR.CURTRXAM > 0
			AND HDR.VOIDSTTS = 0
			AND LEFT(HDR.CUSTNMBR, 4) <> 'MONI'
			AND LEFT(HDR.DOCNUMBR, 5) = 'SALES'
						UNION --Adds in info for invoices not yet posted
			SELECT HDR.CUSTNMBR
				, HDR.SOPNUMBE
				, CONVERT(nvarchar(10), HDR.DOCDATE, 101)
				, HDR.DOCAMNT
				, 'MONITORING'
			FROM SOP10100 AS HDR
			WHERE HDR.DOCAMNT > 0
			AND HDR.VOIDSTTS = 0
			AND LEFT(HDR.CUSTNMBR, 4) <> 'MONI'
			AND LEFT(HDR.SOPNUMBE, 5) = 'SALES'
		) AS HDR
	LEFT JOIN PPROT.dbo.RM00102 AS BAddr
	ON
		(HDR.CUSTNMBR = BAddr.CUSTNMBR AND (BAddr.ADRSCODE = 'Billing'))
	INNER JOIN PPROT.dbo.RM00102 AS PAddr
	ON
		(HDR.CUSTNMBR = PAddr.CUSTNMBR AND (PAddr.ADRSCODE = 'MONITORING' OR PAddr.ADRSCODE = 'PRIMARY'))
	INNER JOIN (SELECT CUSTNMBR, SUM(CURTRXAM) AS Total FROM RM20101 GROUP BY CUSTNMBR) AS TOT
	ON
		(HDR.CUSTNMBR = TOT.CUSTNMBR)
	INNER JOIN RM00103 AS BLNC
	ON
		(HDR.CUSTNMBR = BLNC.CUSTNMBR)
	INNER JOIN RM00101 AS CUST
	ON
		(HDR.CUSTNMBR = CUST.CUSTNMBR)
	INNER JOIN (SELECT HDR.CUSTNMBR, MAX(HDR.DUEDATE) AS DueDate
		FROM (SELECT CUSTNMBR, DUEDATE FROM RM20101 WHERE RMDTYPAL = 1
					AND CURTRXAM > 0
					AND VOIDSTTS = 0
					AND LEFT(CUSTNMBR, 4) <> 'MONI'
				UNION SELECT CUSTNMBR, DUEDATE FROM SOP10100 WHERE DOCAMNT > 0
					AND VOIDSTTS = 0
					AND LEFT(CUSTNMBR, 4) <> 'MONI') AS HDR
		GROUP BY HDR.CUSTNMBR)
	AS DueDt
	ON
		(HDR.CUSTNMBR = DueDt.CUSTNMBR)
	LEFT JOIN --Adds in the totals for invoices that are not yet posted
		(			SELECT HDR.CUSTNMBR
				, SUM(HDR.DOCAMNT) AS AMT
				FROM SOP10100 AS HDR
			WHERE HDR.DOCAMNT > 0
			AND HDR.VOIDSTTS = 0
			AND LEFT(HDR.CUSTNMBR, 4) <> 'MONI'
			GROUP BY HDR.CUSTNMBR) upstamt
	ON
		HDR.CUSTNMBR = upstamt.CUSTNMBR
	WHERE BLNC.CUSTBLNC > 5
	AND
	HDR.CUSTNMBR IN (
								SELECT CUSTNMBR
									FROM SOP30200 AS SOP
										WHERE (BACHNUMB = 'MONITORING' OR BACHNUMB = 'MANUAL')  --Gets Manual Bill Customer Invoices
										AND (DOCDATE BETWEEN @BeginSearchDate AND @EndSearchDate)
								UNION
								SELECT CAST(AccountID AS NVARCHAR(10))
									FROM [Platinum_Protection_InterimCRM].[dbo].[MC_StatementLetterRequest]
										WHERE IsHandled = 0
							)

END
GO

GRANT EXEC ON dbo.ppCustGetStatementLettersInfo TO PUBLIC
GO