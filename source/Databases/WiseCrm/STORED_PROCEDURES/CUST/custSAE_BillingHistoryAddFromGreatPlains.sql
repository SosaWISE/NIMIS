USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSAE_BillingHistoryAddFromGreatPlains')
	BEGIN
		PRINT 'Dropping Procedure custSAE_BillingHistoryAddFromGreatPlains'
		DROP  Procedure  dbo.custSAE_BillingHistoryAddFromGreatPlains
	END
GO

PRINT 'Creating Procedure custSAE_BillingHistoryAddFromGreatPlains'
GO
/******************************************************************************
**		File: custSAE_BillingHistoryAddFromGreatPlains.sql
**		Name: custSAE_BillingHistoryAddFromGreatPlains
**		Desc: 
**			Stored procedure to run periodically to refresh the SAE_BillingHistory
**			table in the WISE_CRM database.
**			SAE_BillingHistory is first truncated then populated from Great Plains.
**
**
**		This template can be customized:
**              
**		Return values:
**			None
** 
**		Called by:   
**			Job Agent to pull Great Plains data on a regular basis (should be daily)
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**		None							SAE_BillingHistory table contains payment history
**
**		Auth: Bob McFadden
**		Date: 08/06/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	08/06/2014	Bob McFadden	Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custSAE_BillingHistoryAddFromGreatPlains
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;

		TRUNCATE TABLE dbo.SAE_BillingHistory

		INSERT .dbo.SAE_BillingHistory
		(
		--	BillingHistoryID, 
			CustomerMasterFileId, 
			--AccountId, 
			BillingDate, 
			InvoiceNumber, 
			Amount, 
			PaymentDate, 
			PaymentNumber, 
			PaymentAmount, 
			AmountRemain
		)
		/** TODO:  Integrate into GP */
		SELECT
		--	BillingHistoryID, 
			CustomerMasterFileId, 
			--AccountId, 
			BillingDate, 
			InvoiceNumber, 
			Amount, 
			PaymentDate, 
			PaymentNumber, 
			PaymentAmount, 
			AmountRemain
		FROM
			[dbo].[SAE_BillingHistory]
		WHERE
			(BillingHistoryID = -1);
			
		--SELECT 
		--	CONVERT(BIGINT,RM20101.CUSTNMBR) AS CustomerMasterFileId, 
		--	-- AccountId, 
		--	CASE WHEN RMDTYPAL = 1 THEN RM20101.DOCDATE ELSE NULL END AS BillingDate, 
		--	CASE WHEN RMDTYPAL = 1 THEN LTRIM(RTRIM(RM20101.DOCNUMBR)) ELSE NULL END AS InvoiceNumber, 
		--	CASE WHEN RMDTYPAL = 1 THEN CONVERT(MONEY,RM20101.ORTRXAMT) ELSE 0 END AS Amount, 
		--	CASE WHEN RMDTYPAL = 9 THEN RM20101.DOCDATE ELSE NULL END AS PaymentDate, 
		--	CASE WHEN RMDTYPAL = 9 THEN LTRIM(RTRIM(RM20101.DOCNUMBR)) ELSE NULL END AS PaymentNumber, 
		--	CASE WHEN RMDTYPAL = 9 THEN CONVERT(MONEY,RM20101.ORTRXAMT) ELSE 0 END AS PaymentAmount,
		--	CASE WHEN RMDTYPAL = 1 THEN CONVERT(MONEY,RM20101.ORTRXAMT) ELSE 0 END AS AmountRemain
		--FROM 
		--	[WISE_GP].[NEX].dbo.RM20101 WITH (NOLOCK)
		--	JOIN dbo.AE_CustomerMasterFiles WITH (NOLOCK)
		--		ON CONVERT(BIGINT,RM20101.CUSTNMBR) = AE_CustomerMasterFiles.CustomerMasterFileID
		--WHERE 
		--	ISNUMERIC(RM20101.CUSTNMBR) = 1
		--	AND RM20101.RMDTYPAL IN (1,9)
		--	/* RMDTYPAL values:
		--	1 = INVOICE
		--	9 = PAYMENT
		--	*/
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custSAE_BillingHistoryAddFromGreatPlains TO PUBLIC
GO

/** EXEC dbo.custSAE_BillingHistoryAddFromGreatPlains */