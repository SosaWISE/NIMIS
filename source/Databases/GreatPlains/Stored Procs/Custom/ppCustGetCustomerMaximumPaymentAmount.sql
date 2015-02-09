USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetCustomerMaximumPaymentAmount')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetCustomerMaximumPaymentAmount'
		DROP  Procedure  dbo.ppCustGetCustomerMaximumPaymentAmount
	END
GO

PRINT 'Creating Procedure ppCustGetCustomerMaximumPaymentAmount'
GO
/******************************************************************************
**		File: ppCustGetCustomerMaximumPaymentAmount.sql
**		Name: ppCustGetCustomerMaximumPaymentAmount
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
CREATE Procedure dbo.ppCustGetCustomerMaximumPaymentAmount
(
	@CustomerNumber NVARCHAR(30)
)
AS
BEGIN

	SELECT
		SUM(InvoiceBalance) AS TotalOwed
	FROM
		vwCustomerOutstandingInvoices
	WHERE
		CustomerNumber = @CustomerNumber

END
GO

GRANT EXEC ON dbo.ppCustGetCustomerMaximumPaymentAmount TO PUBLIC
GO