USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxMsAccountSetupFeeGet')
	BEGIN
		PRINT 'Dropping FUNCTION fxMsAccountSetupFeeGet'
		DROP FUNCTION  dbo.fxMsAccountSetupFeeGet
	END
GO

PRINT 'Creating FUNCTION fxMsAccountSetupFeeGet'
GO
/******************************************************************************
**		File: fxMsAccountSetupFeeGet.sql
**		Name: fxMsAccountSetupFeeGet
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
CREATE FUNCTION dbo.fxMsAccountSetupFeeGet
(
	@AccountID BIGINT
	, @IncludeOver3Months BIT = 0
)
RETURNS MONEY
AS
BEGIN
	-- DECLARATIONS
	DECLARE @Result MONEY = NULL;

	-- Execute Query
	/** FIGURE out the Setup Fee **/
	IF (@IncludeOver3Months = 0)
	BEGIN
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
			(ITM.ItemTypeId LIKE 'SETUP_FEE%')
			AND (ITM.ItemTypeId <> 'SETUP_FEE_OVR3')
		GROUP BY
			INV.[InvoiceID]
			, INV.[AccountId]
			, INV.[InvoiceTypeId];
	END
	ELSE
	BEGIN
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
			(ITM.ItemTypeId LIKE 'SETUP_FEE%')
		GROUP BY
			INV.[InvoiceID]
			, INV.[AccountId]
			, INV.[InvoiceTypeId];
	END

	-- Return Result
	RETURN @Result;
END
GO

--SELECT [dbo].fxMsAccountSetupFeeGet(150925, 0) AS [Setup Fee 150925];
SELECT [dbo].fxMsAccountSetupFeeGet(100288, 0) AS [Setup Fee No O3M 100288];
SELECT [dbo].fxMsAccountSetupFeeGet(100288, 1) AS [Setup Fee With O3M 100288];