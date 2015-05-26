USE [NXSE_Commissions]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxSCv2_0GetSalesRepBonusUpgradesByAccountId')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv2_0GetSalesRepBonusUpgradesByAccountId'
		DROP FUNCTION  dbo.fxSCv2_0GetSalesRepBonusUpgradesByAccountId
	END
GO

PRINT 'Creating FUNCTION fxSCv2_0GetSalesRepBonusUpgradesByAccountId'
GO
/******************************************************************************
**		File: fxSCv2_0GetSalesRepBonusUpgradesByAccountId.sql
**		Name: fxSCv2_0GetSalesRepBonusUpgradesByAccountId
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
**		Date: 04/17/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/17/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCv2_0GetSalesRepBonusUpgradesByAccountId
(
	@AccountID BIGINT
	, @CommissionEngineId VARCHAR(10) = 'SCv2.0'
)
RETURNS 
@ParsedList table
(
	ItemID VARCHAR(50)
	, ItemDesc NVARCHAR(136)
	, InvoiceItemId BIGINT
	, ItemTypeId VARCHAR(50)
	, CustomerPays MONEY
	, BonusUpgrade MONEY
)
AS
BEGIN
	INSERT INTO @ParsedList (ItemID, ItemDesc, InvoiceItemId, ItemTypeId, CustomerPays, BonusUpgrade )
	SELECT
		AEIT.ItemID
		, AEIT.ItemDesc
		, AEII.InvoiceItemID
		, AEIT.ItemTypeId
		, AEII.RetailPrice AS CustomerPays
		, ISNULL(SCEQ.UpgradeBonus, ISNULL(MSEQ.RepBonusUpgrade, 0)) AS [Bonus Upgrade]
	FROM
		[WISE_CRM].[dbo].[AE_Invoices] AS AEI WITH (NOLOCK)
		INNER JOIN [WISE_CRM].[dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
		ON
			(AEII.InvoiceId = AEI.InvoiceID)
			AND (AEI.InvoiceTypeId = 'INSTALL')
			AND (AEI.IsActive = 1 AND AEI.IsDeleted = 0)
		INNER JOIN [WISE_CRM].[dbo].[AE_Items] AS AEIT WITH (NOLOCK)
		ON
			(AEIT.ItemID = AEII.ItemId)
			AND (AEIT.ItemTypeId = 'EQPM_INVT')
		INNER JOIN [WISE_CRM].[dbo].[MS_Equipments] AS MSEQ WITH (NOLOCK)
		ON
			(MSEQ.EquipmentID = AEIT.ItemID)
		LEFT OUTER JOIN [dbo].[SC_Equipments] AS SCEQ WITH (NOLOCK)
		ON
			(SCEQ.ModelNumberID = AEIT.ModelNumber)
			AND (SCEQ.CommissionEngineId = @CommissionEngineId)
	WHERE
		(AEI.AccountId = @AccountID)
		AND (AEII.IsCustomerPaying = 'TRUE');

	RETURN;
END
GO

SELECT * FROM dbo.fxSCv2_0GetSalesRepBonusUpgradesByAccountId(191237, 'SCv2.0');