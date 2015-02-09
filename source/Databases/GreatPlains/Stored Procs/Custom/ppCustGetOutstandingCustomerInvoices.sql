USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetOutstandingCustomerInvoices')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetOutstandingCustomerInvoices'
		DROP  Procedure  dbo.ppCustGetOutstandingCustomerInvoices
	END
GO

PRINT 'Creating Procedure ppCustGetOutstandingCustomerInvoices'
GO
/******************************************************************************
**		File: ppCustGetOutstandingCustomerInvoices.sql
**		Name: ppCustGetOutstandingCustomerInvoices
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
**		Date: 08/20/2009
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	08/20/2009	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustGetOutstandingCustomerInvoices
(
	@DocumentNumber NVARCHAR(30)
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

GRANT EXEC ON dbo.ppCustGetOutstandingCustomerInvoices TO PUBLIC
GO