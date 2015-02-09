USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetInvoiceDetail')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetInvoiceDetail'
		DROP  Procedure  dbo.ppCustGetInvoiceDetail
	END
GO

PRINT 'Creating Procedure ppCustGetInvoiceDetail'
GO
/******************************************************************************
**		File: ppCustGetInvoiceDetail.sql
**		Name: ppCustGetInvoiceDetail
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
**		Auth: Todd Carlson
**		Date: 03/03/2009
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	03/03/2009	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustGetInvoiceDetail
(
	@DocumentNumber NVARCHAR(50)
)
AS
BEGIN

	-- First result set has the invoice header
	SELECT
		RTRIM(CUSTNMBR) AS CUSTNMBR
		, RTRIM(DOCNUMBR) AS DOCNUMBR
		, DOCDATE
		, ORTRXAMT
		, TAXAMNT
		, RTRIM(TAXSCHID) AS TAXSCHID
		, RTRIM(PYMTRMID) AS PYMTRMID
		, VOIDSTTS
	FROM
		RM20101
	WHERE
		DOCNUMBR = @DocumentNumber
	UNION
	SELECT
		RTRIM(CUSTNMBR) AS CUSTNMBR
		, RTRIM(DOCNUMBR) AS DOCNUMBR
		, DOCDATE
		, ORTRXAMT
		, TAXAMNT
		, RTRIM(TAXSCHID) AS TAXSCHID
		, RTRIM(PYMTRMID) AS PYMTRMID
		, VOIDSTTS
	FROM
		RM30101
	WHERE
		DOCNUMBR = @DocumentNumber
	
	-- Second result set has the invoice line items
	SELECT
		RTRIM(SOPNUMBE) AS SOPNUMBE
		, RTRIM(ITEMNMBR) AS ITEMNMBR
		, RTRIM(ITEMDESC) AS ITEMDESC
		, RTRIM(TAXSCHID) AS TAXSCHID
		, XTNDPRCE
		, QUANTITY
	FROM
		SOP30300 LNS
	WHERE
		SOPNUMBE = @DocumentNumber
	
	-- Third result set has payments for the invoice
	SELECT
		RTRIM(APFRDCNM) AS APFRDCNM
		, APFRDCDT
		, APPTOAMT
	FROM
		RM20201
	WHERE
		APTODCNM = @DocumentNumber
	UNION
	SELECT
		RTRIM(APFRDCNM) AS APFRDCNM
		, APFRDCDT
		, APPTOAMT
	FROM
		RM30201
	WHERE
		APTODCNM = @DocumentNumber
		
	-- Fourth result set has CC history for the invoice
	SELECT
		*
	FROM
		vwCCTransactions
	WHERE
		MSO_Doc_Number = @DocumentNumber

END
GO

GRANT EXEC ON dbo.ppCustGetInvoiceDetail TO PUBLIC
GO