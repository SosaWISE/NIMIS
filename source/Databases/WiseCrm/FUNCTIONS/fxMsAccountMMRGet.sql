USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxMsAccountMMRGet')
	BEGIN
		PRINT 'Dropping FUNCTION fxMsAccountMMRGet'
		DROP FUNCTION  dbo.fxMsAccountMMRGet
	END
GO

PRINT 'Creating FUNCTION fxMsAccountMMRGet'
GO
/******************************************************************************
**		File: fxMsAccountMMRGet.sql
**		Name: fxMsAccountMMRGet
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
**		Auth: Andrés E. Sosa
**		Date: 06/07/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	06/07/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxMsAccountMMRGet
(
	@AccountID BIGINT
)
RETURNS MONEY
AS
BEGIN
	-- DECLARATIONS
	DECLARE @Result MONEY = NULL;

	-- Execute Query
	/** FIGURE out the Setup Fee **/
		SELECT TOP 1 
			--INV.[InvoiceID]
			--, INV.[AccountId]
			--, INV.[InvoiceTypeId]
			@Result = SUM(INVT.RetailPrice)
			--, SUM(ITM.SystemPoints) AS SystemPoints
		FROM
			[dbo].[AE_Invoices] AS INV WITH (NOLOCK)
			INNER JOIN [dbo].[AE_InvoiceItems] AS INVT WITH (NOLOCK)
			ON
				(INVT.InvoiceId = INV.InvoiceID)
				AND (INV.AccountId = @AccountID)
				AND (INV.IsActive = 1) AND (INV.IsDeleted = 0)
				AND (INVT.IsActive = 1) AND (INVT.IsDeleted = 0)
				AND (INV.InvoiceTypeId = 'INSTALL')
			INNER JOIN [dbo].[AE_Items] AS ITM WITH (NOLOCK)
			ON
				(ITM.ItemID = INVT.ItemId)
		WHERE
			(ITM.ItemTypeId = 'MON_CONT')
			OR (ITM.ItemTypeId LIKE 'MMR_SREP%')
		GROUP BY
			INV.[InvoiceID]
			, INV.[AccountId]
			, INV.[InvoiceTypeId];

	-- Return Result
	RETURN @Result;
END
GO
/*
SELECT [dbo].fxMsAccountMMRGet(191217) AS [MMR 150925];
SELECT [dbo].fxMsAccountMMRGet(100288) AS [MMR 100288];
SELECT [dbo].fxMsAccountMMRGet(100274) AS [MMR 100274];*/
DECLARE @AccountID BIGINT = 191217;

		SELECT
			INV.[InvoiceID]
			, INV.[AccountId]
			, INV.[InvoiceTypeId]
			, INVT.ItemId
			, INVT.RetailPrice
			--SUM(INVT.RetailPrice)
			--, SUM(ITM.SystemPoints) AS SystemPoints
		FROM
			[dbo].[AE_Invoices] AS INV WITH (NOLOCK)
			INNER JOIN [dbo].[AE_InvoiceItems] AS INVT WITH (NOLOCK)
			ON
				(INVT.InvoiceId = INV.InvoiceID)
				AND (INV.AccountId = @AccountID)
				AND (INV.IsActive = 1) AND (INV.IsDeleted = 0)
				AND (INVT.IsActive = 1) AND (INVT.IsDeleted = 0)
				AND (INV.InvoiceTypeId = 'INSTALL')
			INNER JOIN [dbo].[AE_Items] AS ITM WITH (NOLOCK)
			ON
				(ITM.ItemID = INVT.ItemId)
		WHERE
			(ITM.ItemTypeId = 'MON_CONT')
			OR (ITM.ItemTypeId LIKE 'MMR_SREP%')
		--GROUP BY
		--	INV.[InvoiceID]
		--	, INV.[AccountId]
		--	, INV.[InvoiceTypeId];


