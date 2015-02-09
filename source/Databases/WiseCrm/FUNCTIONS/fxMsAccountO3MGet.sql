USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxMsAccountO3MGet')
	BEGIN
		PRINT 'Dropping FUNCTION fxMsAccountO3MGet'
		DROP FUNCTION  dbo.fxMsAccountO3MGet
	END
GO

PRINT 'Creating FUNCTION fxMsAccountO3MGet'
GO
/******************************************************************************
**		File: fxMsAccountO3MGet.sql
**		Name: fxMsAccountO3MGet
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
CREATE FUNCTION dbo.fxMsAccountO3MGet
(
	@AccountID BIGINT
)
RETURNS BIT
AS
BEGIN
	-- DECLARATIONS
	DECLARE @RetailPrice MONEY = NULL
		, @Result BIT = NULL;

	-- Execute Query
	/** FIGURE out the O3M **/
		SELECT TOP 1 
			@RetailPrice = SUM(INVT.RetailPrice)
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
			(ITM.ItemTypeId = 'SETUP_FEE_OVR3')
		GROUP BY
			INV.[InvoiceID]
			, INV.[AccountId]
			, INV.[InvoiceTypeId];

	-- Calculate O3M
	IF (@RetailPrice IS NULL OR @RetailPrice = 0) 
	BEGIN
		SET @Result = 0;
	END
	ELSE
	BEGIN
		SET @Result = 1;
	END
	-- Return Result
	RETURN @Result;
END
GO
/*
SELECT [dbo].fxMsAccountO3MGet(150925) AS [O3M 150925];
SELECT [dbo].fxMsAccountO3MGet(100288) AS [O3M 100288];
SELECT [dbo].fxMsAccountO3MGet(100274) AS [O3M 100274];
*/