USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountSalesInformations')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountSalesInformations'
		DROP VIEW dbo.vwMS_AccountSalesInformations
	END
GO

PRINT 'Creating VIEW vwMS_AccountSalesInformations'
GO

/****** Object:  View [dbo].[vwMS_AccountSalesInformations]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountSalesInformations.sql
**		Name: vwMS_AccountSalesInformations
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
**		Date: 06/07/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/07/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountSalesInformations]
AS
	-- Enter Query here
	SELECT
		MSA.AccountID
		, MSI.PaymentTypeId
		, MSI.BillingDay
		, MSIA.MonitoringStationName AS CurrentMonitoringStation
		, MSA.PanelTypeId
		, MSA.PanelItemId
		, MSI.IsTakeOver
		, MSI.IsOwner
		, MSA.CellPackageItemId
		, CPKG.ItemDesc AS [CellServicePackage]
		, MSA.CellularTypeId
		, CTY.CellularTypeName
		, [dbo].fxMsAccountCellUnitTypeGet(MSA.CellPackageItemId) AS [CellularVendor]
		, [dbo].fxMsAccountSetupFeeGet(MSA.AccountID, 0) AS [SetupFee]
		, [dbo].fxMsAccountSetupFeeGet(MSA.AccountID, 1) AS [SetupFee1stMonth]
		, [dbo].fxMsAccountMMRGet(MSA.AccountID) AS [MMR]
		, [dbo].fxMsAccountO3MGet(MSA.AccountID) AS [Over3Months]
		, [dbo].fxMsAccountsTotalPoints(MSA.AccountID) AS [TotalPoints]
		, [dbo].fxMsAccountTotalPointsAllowed(MSA.AccountID) AS [TotalPointsAllowed]
		, [dbo].fxMsAccountTotalPointsRep(MSA.AccountID) AS RepPoints
		, CAST(0 AS DECIMAL(5,2)) AS TechPoints
		, CNTC.ContractLength
		, MSA.ContractId
		, ACT.ContractTemplateId
		, MSI.Email
		, MSI.IsMoni
		, MSI.ContractSignedDate
		, MSI.SalesRepId
		, MSI.InstallDate
		, MSI.TechId
		, MSI.CancelDate
		, MSI.AccountCancelReasonId
		, MSI.FriendsAndFamilyTypeId
		, MSI.AccountSubmitId
		, MSI.SubmittedToCSDate
		, MSI.CsConfirmationNumber
		, MSI.CsTwoWayConfNumber
		, MSI.SubmittedToGPDate
		, MSI.AMA
		, MSI.NOC
		, MSI.SOP
		, MSI.ApprovedDate
		, MSI.ApproverID
		, MSI.NOCDate
		, MSI.OptOutCorporate
		, MSI.OptOutAffiliate
	FROM
		[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
		LEFT OUTER JOIN [dbo].AE_Contracts AS ACT WITH (NOLOCK)
		ON
			(ACT.ContractID = MSA.ContractId)
			AND (ACT.IsActive = 1) AND (ACT.IsDeleted = 0)
		LEFT OUTER JOIN [dbo].[MS_AccountCellularTypes] AS CTY WITH (NOLOCK)
		ON
			(CTY.CellularTypeID = MSA.CellularTypeId)
		LEFT OUTER JOIN [dbo].[MS_AccountSalesInformations] AS MSI WITH (NOLOCK)
		ON
			(MSI.AccountID = MSA.AccountID)
		LEFT OUTER JOIN [dbo].[AE_Contracts] AS CNTC WITH (NOLOCK)
		ON
			(CNTC.ContractId = MSA.ContractId)
		LEFT OUTER JOIN [dbo].[AE_Items] AS CPKG WITH (NOLOCK)
		ON
			(CPKG.ItemID = MSA.CellPackageItemId)
		LEFT OUTER JOIN [dbo].[vwMS_IndustryAccountNumbersWithReceiverLineInfo] AS MSIA WITH (NOLOCK)
		ON
			(MSA.IndustryAccountId = MSIA.IndustryAccountID)

GO
/* TEST 
SELECT * FROM vwMS_AccountSalesInformations WHERE AccountID = 191168; --191101;
SELECT * FROM vwMS_AccountSalesInformations WHERE AccountID = 191168;
SELECT * FROM [dbo].[AE_Contracts] WHERE ContractID = 1000022;
*/

SELECT 
	 AEII.InvoiceItemID ,
	        AEII.InvoiceId ,
	        AEII.ItemId ,
			MSE.ItemSKU,
			MSE.ItemDesc,
	        AEII.ProductBarcodeId ,
	        AEII.AccountEquipmentId ,
	        AEII.TaxOptionId ,
	        AEII.Qty ,
	        AEII.Cost ,
	        AEII.RetailPrice ,
	        AEII.PriceWithTax ,
	        AEII.SystemPoints ,
	        AEII.SalesmanId ,
	        AEII.TechnicianId
FROM
	[dbo].[AE_Invoices] AS AEI WITH (NOLOCK)
	INNER JOIN [dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
	ON
		(AEII.InvoiceId = AEI.InvoiceID)
		AND (AEI.InvoiceTypeId = 'INSTALL')
		AND (AEI.IsActive = 1 AND AEI.IsDeleted = 0)
		AND (AEII.IsActive = 1 AND AEII.IsDeleted = 0)
		AND (AEI.AccountId = 191168)
	INNER JOIN [dbo].[AE_Items] AS MSE WITH (NOLOCK)
	ON
		(AEII.ItemId = MSE.ItemID)