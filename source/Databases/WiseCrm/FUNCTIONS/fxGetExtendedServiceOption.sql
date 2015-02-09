USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetExtendedServiceOption')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetExtendedServiceOption'
		DROP FUNCTION  dbo.fxGetExtendedServiceOption
	END
GO

PRINT 'Creating FUNCTION fxGetExtendedServiceOption'
GO
/******************************************************************************
**		File: fxGetExtendedServiceOption.sql
**		Name: fxGetExtendedServiceOption
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
**		Date: 09/17/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	09/17/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetExtendedServiceOption
(
	@AccountID BIGINT
)
RETURNS BIT
AS
BEGIN
	-- DECLARATIONS
	DECLARE @Result BIT = 0;

	-- Execute Query
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
		(ITM.ItemTypeId = 'MS_SERVICE_OPTIONS');

	-- Return Result
	RETURN ISNULL(@Result, 0);
END
GO

--SELECT [dbo].fxGetExtendedServiceOption(150925);
SELECT [dbo].fxGetExtendedServiceOption(130532) AS ServiceOption;
--SELECT * FROM dbo.AE_Invoices WHERE AccountId = 130532;