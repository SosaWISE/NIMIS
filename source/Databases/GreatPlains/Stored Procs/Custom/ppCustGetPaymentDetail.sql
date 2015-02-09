USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetPaymentDetail')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetPaymentDetail'
		DROP  Procedure  dbo.ppCustGetPaymentDetail
	END
GO

PRINT 'Creating Procedure ppCustGetPaymentDetail'
GO
/******************************************************************************
**		File: ppCustGetPaymentDetail.sql
**		Name: ppCustGetPaymentDetail
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
CREATE Procedure dbo.ppCustGetPaymentDetail
(
	@DocumentNumber NVARCHAR(50)
)
AS
BEGIN

	-- First result set has the payment header
	SELECT
		RTRIM(CUSTNMBR) AS CUSTNMBR
		, RTRIM(DOCNUMBR) AS DOCNUMBR
		, DOCDATE
		, ORTRXAMT
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
		, VOIDSTTS
	FROM
		RM30101
	WHERE
		DOCNUMBR = @DocumentNumber

	-- Second result set has invoices to which payment was applied
	SELECT
		RTRIM(APTODCNM) AS APTODCNM
		, APTODCDT
		, APPTOAMT
	FROM
		RM20201
	WHERE
		APFRDCNM = @DocumentNumber
	UNION
	SELECT
		RTRIM(APTODCNM) AS APTODCNM
		, APTODCDT
		, APPTOAMT
	FROM
		RM30201
	WHERE
		APFRDCNM = @DocumentNumber

END
GO

GRANT EXEC ON dbo.ppCustGetPaymentDetail TO PUBLIC
GO