USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_InvoiceMsInstallInfo')
	BEGIN
		PRINT 'Dropping VIEW vwAE_InvoiceMsInstallInfo'
		DROP VIEW dbo.vwAE_InvoiceMsInstallInfo
	END
GO

PRINT 'Creating VIEW vwAE_InvoiceMsInstallInfo'
GO

/****** Object:  View [dbo].[vwAE_InvoiceMsInstallInfo]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_InvoiceMsInstallInfo.sql
**		Name: vwAE_InvoiceMsInstallInfo
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
**		Date: 01/27/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/27/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_InvoiceMsInstallInfo]
AS
	-- Enter Query here
	SELECT 
		INV.InvoiceID
		, INV.AccountId
		, CAST('SETUP_FEE_199' AS VARCHAR(50)) AS ActivationFeeItemId
		, CAST(199 AS MONEY) AS ActivationFee
		, CAST(199 AS MONEY) AS ActivationFeeActual
		, CAST('MON_CONT_5000' AS VARCHAR(50)) AS MonthlyMonitoringRateItemId
		, CAST(39.95 AS MONEY) AS MonthlyMonitoringRate
		, CAST(39.95 AS MONEY) AS MonthlyMonitoringRateActual
		, MASI.PaymentTypeId
		, MASI.BillingDay
		, MASI.Email
		, CAST('WRLFWN' AS VARCHAR(20)) AS AlarmComPackageId
		, CAST('CELL_SRV_AC_WSF' AS VARCHAR(50)) AS AlarmComPackageItemId
		, [dbo].fxMsAccountCellUnitTypeGet(MAC.CellPackageItemId) AS CellVendorId
		, CAST(0 AS BIT) AS Over3Months
		, MAC.CellularTypeId
		, MAC.PanelTypeId
		, MAC.ContractId
		, MASI.IsMoni
		, MASI.IsTakeOver
		, MASI.IsOwner
	FROM
		[dbo].AE_Invoices AS INV WITH (NOLOCK)
		LEFT OUTER JOIN [dbo].[MS_Accounts] AS MAC WITH (NOLOCK)
		ON
			(MAC.AccountID = INV.AccountId)
		LEFT OUTER JOIN [dbo].MS_AccountSalesInformations AS MASI WITH (NOLOCK)
		ON
			(MASI.AccountID = MAC.AccountID)
	WHERE
		(INV.InvoiceTypeId = 'INSTALL');
GO
/* TEST */
-- SELECT * FROM vwAE_InvoiceMsInstallInfo WHERE InvoiceID = 1;