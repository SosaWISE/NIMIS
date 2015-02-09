USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetEftTransactions')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetEftTransactions'
		DROP  Procedure  dbo.ppCustGetEftTransactions
	END
GO

PRINT 'Creating Procedure ppCustGetEftTransactions'
GO
/******************************************************************************
**		File: ppCustGetEftTransactions.sql
**		Name: ppCustGetEftTransactions
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
CREATE Procedure dbo.ppCustGetEftTransactions
(
	@PostingDate DATETIME
	, @BatchNumber NVARCHAR(15)
	, @CustomerIDList NVARCHAR(MAX)
	, @NTransactionsExpected INT
)
AS
BEGIN

	SELECT
		RTRIM(CUSTNMBR) AS CustomerNumber
		, RTRIM(DOCNUMBR) AS PaymentNumber
		, ORTRXAMT AS PaymentAmount
	FROM
		RM20101
	WHERE
		DOCNUMBR LIKE 'PYMT-%'
		AND (CUSTNMBR IN (SELECT * FROM dbo.SplitStringList(@CustomerIDList)))
		AND TRXSORCE =
		(
			SELECT
				TRXSORCE
			FROM
				(
					SELECT
						DOC.TRXSORCE
						, COUNT(DOC.DOCNUMBR) AS NTransactions
					FROM
						RM20101 AS DOC WITH (NOLOCK)
					WHERE
						(DOC.DOCNUMBR LIKE 'PYMT-%')
						AND (DOC.CHEKNMBR IN ('EFT','EFT.'))
						AND (DOC.DOCDATE = @PostingDate OR DOC.POSTDATE = @PostingDate OR DOC.GLPOSTDT = @PostingDate)
						AND (DOC.CUSTNMBR IN (SELECT * FROM dbo.SplitStringList(@CustomerIDList)))
						AND (DOC.BACHNUMB = @BatchNumber)
					GROUP BY
						DOC.TRXSORCE
				) V1
			WHERE
				NTransactions = @NTransactionsExpected
		)
	ORDER BY
		CUSTNMBR
	
END
GO

GRANT EXEC ON dbo.ppCustGetEftTransactions TO PUBLIC
GO