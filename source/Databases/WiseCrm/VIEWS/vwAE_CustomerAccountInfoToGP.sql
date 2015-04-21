USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerAccountInfoToGP')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerAccountInfoToGP'
		DROP VIEW dbo.vwAE_CustomerAccountInfoToGP
	END
GO

PRINT 'Creating VIEW vwAE_CustomerAccountInfoToGP'
GO

/****** Object:  View [dbo].[vwAE_CustomerAccountInfoToGP]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerAccountInfoToGP.sql
**		Name: vwAE_CustomerAccountInfoToGP
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
**		Date: 02/17/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/17/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_CustomerAccountInfoToGP]
AS
	-- Enter Query here
	SELECT
		MCA.CustomerMasterFileId AS [CustomerMasterFileID]
		, MCA.AccountID AS [AccountID]
		, AEC.CustomerId AS [PrimaryCustomerID]
		, AEC.FirstName + ' ' + AEC.LastName AS [PrimaryCustomerName]
		, AEC2.CustomerID AS [SecondaryCustomerID]
		, CASE
			WHEN AEC2.CustomerID IS NULL THEN ''
			ELSE AEC2.FirstName + ' ' + AEC2.LastName 
		  END AS [SecondaryCustomerName]
		, MSIA.Csid AS [CentralStationID]
		, MSASI.CurrentMonitoringStation
		, MSASI.ContractSignedDate AS [AMASignDate]
		, MSASI.NOCDateCalculated
		, MSASI.SalesRepId AS [SalesRepID]
		, MSASI.InstallDate AS [InstallDate]
		, MSASI.ApprovedDate
		, MSASI.ApproverID
		, MSASI.TechId AS [TechID]
		, MSASI.[MMR] AS RMR
		, MSASI.BillingDay AS [BillingDay]
		, MSASI.PaymentTypeId AS [PaymentType]
		, MSASI.ContractLength AS [ContractLength]
		, MSA.PanelTypeId AS [PanelType]
		, MSAST.SystemTypeName AS [SystemType]  -- 2-way; cellular; cell/interactvie
		, CAST(
			CASE
				WHEN MSASI.[SetupFee] IS NULL THEN 0
				WHEN MSASI.[SetupFee] = 0 THEN 0
				ELSE 1
			END
		 AS BIT) AS [ActivationCollected]
		, MSASI.[SetupFee] AS [ActivationFee]
		, CAST(
			CASE
				WHEN MSASI.[Over3Months] = 1 THEN '3 Months'
				WHEN MSASI.[Over3Months] = 0 THEN 'Paid Full'
				ELSE 'NOT SET'
			END
		 AS VARCHAR) AS [PaidFull3Months]
		, MSASI.CancelDate AS [CancelledDate]
		, MCACR.AccountCancelReason AS [CancelledReason]
		, MSASI.IsTakeOver AS [TakeOver]
		, MSASI.IsOwner
		, MSASI.FriendsAndFamilyTypeId
		, MSASI.AccountPackageId
		, MSASI.AccountPackageName
		, MSASI.[TotalPoints]
		, MSASI.[TotalPointsAllowed]
		, MSASI.RepPoints
		, dbo.fxGetMS_AccountEquipmentHasExistingEquipment(MCA.AccountID) AS [HasExistingEquipment]
		, dbo.fxQlCreditReportGetScoreByMsAccountID(MCA.AccountID) AS [CreditScore]
		, dbo.fxQlCreditReportGetTransactionIdByMsAccountID(MCA.AccountID) AS [TransactionID]
		, dbo.fxQlCreditReportGetReportGuidByMsAccountID(MCA.AccountID) AS [ReportGuid]
		, dbo.fxQlCreditReportGetCreditBureauByMsAccountID(MCA.AccountID) AS [Bureau]
		, dbo.fxQlCreditReportGetGatewayByMsAccountID(MCA.AccountID) AS [Gateway]
		, dbo.fxGetMS_AccountEquipmentPoints(MCA.AccountID) AS [Points]
	FROM
		[dbo].[MC_Accounts] AS MCA WITH (NOLOCK)
--		INNER JOIN [dbo].[AE_CustomerAccounts] AS MSAC WITH (NOLOCK)
--		ON
--			(MSAC.AccountId = MCA.AccountID)
		INNER JOIN [dbo].[AE_Customers] AS AEC WITH(NOLOCK)
		ON
			(MCA.CustomerMasterFileId = AEC.CustomerMasterFileId)
			AND (AEC.CustomerTypeId = 'PRI')
		LEFT JOIN [dbo].[AE_Customers] AS AEC2 WITH(NOLOCK)
		ON
			(MCA.CustomerMasterFileId = AEC2.CustomerMasterFileId)
			AND (AEC2.CustomerTypeId = 'SEC')
		INNER JOIN [dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
		ON
			(MSA.AccountID = MCA.AccountID)
			AND (MSA.IsDeleted = 0)
			AND (MSA.ContractId IS NOT NULL)
		LEFT OUTER JOIN [dbo].[MS_IndustryAccounts] AS MSIA WITH (NOLOCK)
		ON
			(MSIA.IndustryAccountID = MSA.IndustryAccountId)
		LEFT OUTER JOIN [dbo].[MS_AccountSystemTypes] AS MSAST WITH (NOLOCK)
		ON
			(MSAST.SystemTypeID = MSA.SystemTypeId)
		--INNER JOIN [dbo].[MS_AccountSalesInformations] AS MSASI WITH (NOLOCK)
		INNER JOIN [dbo].[vwMS_AccountSalesInformations] AS MSASI WITH (NOLOCK)
		ON
			(MSASI.AccountID = MSA.AccountID)
		LEFT OUTER JOIN [dbo].[MC_AccountCancelReasons] AS MCACR WITH (NOLOCK)
		ON
			(MCACR.AccountCancelReasonID = MSASI.AccountCancelReasonId)
		INNER JOIN [dbo].[QL_Leads] AS QL WITH (NOLOCK)
		ON
			(QL.LeadID = AEC.LeadId)
GO
/* TEST 
DECLARE @AccountID BIGINT = 191186;
SELECT CustomerMasterFileId, AccountID, TotalPoints FROM [dbo].[vwAE_CustomerAccountInfoToGP] WHERE AccountID = @AccountID;
--WHERE CUSTOMERMASTERFILEID = 3091526
--WHERE INSTALLDATE BETWEEN '1/1/15' AND '3/31/15'
--WHERE [Transaction ID] IS NOT NULL ORDER BY CustomerMasterFileID DESC;
*/