USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwSAE_BillingHistory')
	BEGIN
		PRINT 'Dropping VIEW vwSAE_BillingHistory'
		DROP VIEW dbo.vwSAE_BillingHistory
	END
GO

PRINT 'Creating VIEW vwSAE_BillingHistory'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwSAE_BillingHistory.sql
**		Name: vwSAE_BillingHistory
**		Desc: 
**		View on the SAE_BillingHistory table.  On this table there are either
**		INVOICES or PAYMENTS.  For Invoices, the BillingDate, InvoiceNumber,
**		and Amount colums are filled in and the PaymentDate, PaymentNumber,
**		and PaymentAmount columns are empty.  The opposite is true for PAYMENTS.
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 06/07/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/07/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwSAE_BillingHistory]
AS
	SELECT
		CustomerMasterFileId AS CustomerMasterFileId, 
		'Invoice' as BillingType,
		BillingDate as BillingDate, 
		InvoiceNumber as BillingNumber, 
		Amount as BillingAmount 
	FROM
		dbo.SAE_BillingHistory
	WHERE InvoiceNumber IS NOT NULL

	UNION

	SELECT
		CustomerMasterFileId AS CustomerMasterFileId, 
		'Payment' as BillingType,
		PaymentDate as BillingDate, 
		PaymentNumber as BillingNumber, 
		PaymentAmount as BillingAmount 
	FROM
		dbo.SAE_BillingHistory
	WHERE PaymentNumber IS NOT NULL

GO
/* TEST */
-- SELECT * FROM vwSAE_BillingHistory
