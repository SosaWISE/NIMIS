USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetCustomerOutstandingInvoices')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetCustomerOutstandingInvoices'
		DROP  Procedure  dbo.ppCustGetCustomerOutstandingInvoices
	END
GO

PRINT 'Creating Procedure ppCustGetCustomerOutstandingInvoices'
GO
/******************************************************************************
**		File: ppCustGetCustomerOutstandingInvoices.sql
**		Name: ppCustGetCustomerOutstandingInvoices
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
CREATE Procedure dbo.ppCustGetCustomerOutstandingInvoices
(
	@CustomerNumber NVARCHAR(30)
)
AS
BEGIN

	SELECT
		*
	FROM
		vwCustomerOutstandingInvoices
	WHERE
		(CustomerNumber = @CustomerNumber)
	ORDER BY
		DocumentDate

END
GO

GRANT EXEC ON dbo.ppCustGetCustomerOutstandingInvoices TO PUBLIC
GO