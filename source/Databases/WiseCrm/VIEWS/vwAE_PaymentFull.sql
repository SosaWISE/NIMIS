USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_PaymentFull')
	BEGIN
		PRINT 'Dropping VIEW vwAE_PaymentFull'
		DROP VIEW dbo.vwAE_PaymentFull
	END
GO

PRINT 'Creating VIEW vwAE_PaymentFull'
GO

/****** Object:  View [dbo].[vwAE_PaymentFull]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_PaymentFull.sql
**		Name: vwAE_PaymentFull
**		Desc: 
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
**		Date: 05/27/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/27/2012	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_PaymentFull]
AS
	/** Enter Query Here */
	SELECT
		PAY.PaymentID
		, PAY.PaymentTypeId
		, INV.InvoiceID
		, INVT.InvoiceTypeID
		, INVT.InvoiceType
		, INV.OriginalTransactionAmount
		, INV.CurrentTransactionAmount
		, INV.SalesAmount
		, INV.TaxAmount
		, IPJ.InvoicePaymentJoinID
		, PAY.AccountId
		, PAY.TransactionSuccess
		, PAY.DocDate
		, PAY.PostedDate
		, PAY.ActualTransactionAmount
	FROM
		dbo.AE_Payments AS PAY WITH (NOLOCK)
		INNER JOIN dbo.AE_InvoicePaymentJoins AS IPJ WITH (NOLOCK)
		ON
			(PAY.PaymentID = IPJ.PaymentId)
		INNER JOIN dbo.AE_Invoices AS INV WITH (NOLOCK)
		ON
			(IPJ.InvoiceId = INV.InvoiceID)
		INNER JOIN dbo.AE_InvoiceTypes AS INVT WITH (NOLOCK)
		ON
			(INV.InvoiceTypeId = INVT.InvoiceTypeID)
		INNER JOIN dbo.MG_Transactions AS TRN WITH (NOLOCK)
		ON
			(PAY.TransactionId = TRN.TransactionID)

GO
/* TEST */
SELECT * FROM vwAE_PaymentFull
