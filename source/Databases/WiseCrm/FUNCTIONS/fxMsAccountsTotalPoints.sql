USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxMsAccountsTotalPoints')
	BEGIN
		PRINT 'Dropping FUNCTION fxMsAccountsTotalPoints'
		DROP FUNCTION  dbo.fxMsAccountsTotalPoints
	END
GO

PRINT 'Creating FUNCTION fxMsAccountsTotalPoints'
GO
/******************************************************************************
**		File: fxMsAccountsTotalPoints.sql
**		Name: fxMsAccountsTotalPoints
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
**		Date: 04/08/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/08/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxMsAccountsTotalPoints
(
	@AccountID BIGINT
)
RETURNS INT
AS
BEGIN
	/** Declarations */
	DECLARE @TotalPoints INT;

	/** Execute actions. */
	SELECT 
		@TotalPoints = SUM(MSAE.Points) 
	FROM 
		[dbo].[MS_AccountEquipment] AS MSAE WITH (NOLOCK)
		INNER JOIN [dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
		ON
			(AEII.AccountEquipmentId = MSAE.AccountEquipmentID)
			AND (MSAE.IsActive = 1 AND MSAE.IsDeleted = 0)
			AND (MSAE.IsExisting = 0)
			AND (MSAE.AccountId = @AccountID)
		INNER JOIN [dbo].[AE_Invoices] AS AEI WITH (NOLOCK)
		ON
			(AEII.InvoiceId = AEI.InvoiceID)
			AND (AEI.InvoiceTypeId = 'INSTALL')
			AND (AEI.IsActive = 1 AND AEI.IsDeleted = 0)
			AND (AEII.IsActive = 1 AND AEII.IsDeleted = 0)
		INNER JOIN [dbo].[MS_Equipments] AS MSE WITH (NOLOCK)
		ON
			(AEII.ItemId = MSE.EquipmentID)
		INNER JOIN [dbo].[MS_EquipmentTypes] AS MSET WITH (NOLOCK)
		ON
			(MSET.EquipmentTypeID = MSE.EquipmentTypeId)
			AND (MSET.EquipmentType <> 'Cell' AND MSET.EquipmentType <> 'Panel');

	RETURN @TotalPoints;
END
GO

/** */
DECLARE @AccountID BIGINT = 191168;
--SELECT AccountID, dbo.fxMsAccountsTotalPoints(AccountID) FROM [dbo].[MS_Accounts] WHERE AccountID = @AccountID;
	SELECT 
		 MSAE.AccountEquipmentID ,
		        MSAE.AccountId ,
		        MSAE.EquipmentId ,
				MSE.GPItemNmbr ,
				MSE.GenDescription ,
		        MSAE.EquipmentLocationId ,
		        MSAE.InvoiceItemId ,
		        MSAE.GPEmployeeId ,
		        MSAE.OfficeReconciliationItemId ,
		        MSAE.AccountEquipmentUpgradeTypeId ,
		        MSAE.CustomerLocation ,
		        MSAE.Points ,
		        MSAE.ActualPoints ,
		        MSAE.Price ,
		        MSAE.IsExisting ,
		        MSAE.BarcodeId ,
		        MSAE.IsServiceUpgrade ,
		        MSAE.IsExistingWiring ,
		        MSAE.IsMainPanel
	FROM 
		[dbo].[MS_AccountEquipment] AS MSAE WITH (NOLOCK)
		INNER JOIN [dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
		ON
			(AEII.AccountEquipmentId = MSAE.AccountEquipmentID)
			AND (MSAE.IsActive = 1 AND MSAE.IsDeleted = 0)
			--AND (MSAE.IsExisting = 0)
			AND (MSAE.AccountId = @AccountID)
		INNER JOIN [dbo].[AE_Invoices] AS AEI WITH (NOLOCK)
		ON
			(AEII.InvoiceId = AEI.InvoiceID)
			AND (AEI.InvoiceTypeId = 'INSTALL')
			AND (AEI.IsActive = 1 AND AEI.IsDeleted = 0)
			AND (AEII.IsActive = 1 AND AEII.IsDeleted = 0)
		INNER JOIN [dbo].[MS_Equipments] AS MSE WITH (NOLOCK)
		ON
			(AEII.ItemId = MSE.EquipmentID)
		INNER JOIN [dbo].[MS_EquipmentTypes] AS MSET WITH (NOLOCK)
		ON
			(MSET.EquipmentTypeID = MSE.EquipmentTypeId)
			AND (MSET.EquipmentType <> 'Cell' AND MSET.EquipmentType <> 'Panel')
			

--SELECT 
--	*
--	, CustomerMasterFileId
--FROM
--	dbo.AE_CustomerAccounts
--	INNER JOIN dbo.AE_Customers 
--	ON
--		(dbo.AE_Customers.CustomerID = dbo.AE_CustomerAccounts.CustomerId)
--WHERE
--	AccountId = 191168
--SELECT * FROM dbo.AE_Invoices WHERE AccountID = 191168

SELECT * FROM dbo.MS_AccountEquipment WHERE AccountID = @AccountID
SELECT * FROM dbo.MS_Equipments WHERE EquipmentID = 'EQPM_INVT22'