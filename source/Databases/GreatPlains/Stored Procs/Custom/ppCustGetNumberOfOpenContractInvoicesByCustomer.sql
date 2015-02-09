USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetNOpenContractInvoicesByCustomer')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetNOpenContractInvoicesByCustomer'
		DROP  Procedure  dbo.ppCustGetNOpenContractInvoicesByCustomer
	END
GO

PRINT 'Creating Procedure ppCustGetNOpenContractInvoicesByCustomer'
GO
/******************************************************************************
**		File: ppCustGetNOpenContractInvoicesByCustomer.sql
**		Name: ppCustGetNOpenContractInvoicesByCustomer
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
**		Date: 09/04/2009
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	09/04/2009	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustGetNOpenContractInvoicesByCustomer
(
	@CustomerNumber CHAR(15)
)
AS
BEGIN

	SELECT
		COUNT(SOPNUMBE) AS NOpenContractInvoices
	FROM
		SOP10100
	WHERE
		CUSTNMBR = @CustomerNumber

END
GO

GRANT EXEC ON dbo.ppCustGetNOpenContractInvoicesByCustomer TO PUBLIC
GO