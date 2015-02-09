USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetEftCashReceipts')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetEftCashReceipts'
		DROP  Procedure  dbo.ppCustGetEftCashReceipts
	END
GO

PRINT 'Creating Procedure ppCustGetEftCashReceipts'
GO
/******************************************************************************
**		File: ppCustGetEftCashReceipts.sql
**		Name: ppCustGetEftCashReceipts
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
**	Execute dbo.ppCustGetEftCashReceipts '5/14/2010 12:00:00 AM', 'EFTNSF 05/14/10', '100259,303138,307702,313315,317064,321918,334712,338790,339905,346568,349033,356534,373732,373740,387297,388141,395096,395201,399614,405157,407099,418870,425137,426987,428070,434814,481530,491325,495502,501923,502707,505336,510395,522883,529044,530433,553959,555463,557078,557078,557211,559109,559533,559953,560352,560352,560501,560501,560642,560642,560778,560860,561217,561235,561235,561661,',56
*******************************************************************************/
CREATE Procedure dbo.ppCustGetEftCashReceipts
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
		(
			SELECT
				*
				, COUNT(*) OVER (PARTITION BY DOC.BACHNUMB) AS NTransactionsInBatch
			FROM
				PPROT.dbo.RM10201 AS DOC WITH (NOLOCK)
--				PPROT.dbo.RM20101 AS DOC WITH (NOLOCK)
			WHERE
				(DOC.DOCNUMBR LIKE 'PYMT-%')
				AND (DOC.CHEKNMBR IN ('EFT','EFT.'))
				AND (DOC.BACHNUMB = @BatchNumber)
				AND (ABS(DATEDIFF(DD, DOC.DOCDATE, @PostingDate)) < 3)
		) V1
	WHERE
		(BACHNUMB = @BatchNumber)
		AND (NTransactionsInBatch = @NTransactionsExpected)
	
END
GO

GRANT EXEC ON dbo.ppCustGetEftCashReceipts TO PUBLIC
GO