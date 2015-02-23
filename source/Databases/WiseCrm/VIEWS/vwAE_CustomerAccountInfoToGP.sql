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
		, MSAC.CustomerId AS [CustomerID]
		, MCA.AccountID AS [AccountID]
		, MSIA.Csid AS [Central Station ID]
		, MSASI.CurrentMonitoringStation
		, MSASI.ContractSignedDate AS [AMA Sign Date]
		, MSASI.SalesRepId AS [Sales Rep ID]
		, MSASI.InstallDate AS [Install Date]
		, MSASI.TechId AS [Tech ID]
		, MSASI.[MMR] AS RMR
		, MSASI.BillingDay AS [Billing Day]
		, MSASI.ContractLength AS [Contract Length]
		, MSA.PanelTypeId AS [Panel Type]
		, MSAST.SystemTypeName AS [System Type]  -- 2-way; cellular; cell/interactvie
		, CAST(
			CASE
				WHEN MSASI.[SetupFee] IS NULL THEN 0
				ELSE 1
			END
		 AS BIT) AS [Activation Collected]
		, MSASI.[SetupFee] AS [Activation Fee]
		, CAST(
			CASE
				WHEN MSASI.[Over3Months] = 1 THEN '3 Months'
				WHEN MSASI.[Over3Months] = 0 THEN 'Paid Full'
				ELSE 'NOT SET'
			END
		 AS VARCHAR) AS [Paid Full / 3 Months]
		, MSASI.CancelDate AS [Cancelled Date]
		, MCACR.AccountCancelReason AS [Cancelled Reason]
		, MSASI.IsTakeOver AS [Take Over]
		, CAST(NULL AS BIT) AS [Has Existing Equipment]
		, dbo.fxQlCreditReportGetScoreByMsAccountID(MCA.AccountID) AS [Credit Score]
		, CAST(NULL AS SMALLINT) AS [Points]
	FROM
		[dbo].[MC_Accounts] AS MCA WITH (NOLOCK)
		INNER JOIN [dbo].[MS_AccountCustomers] AS MSAC WITH (NOLOCK)
		ON
			(MSAC.AccountId = MCA.AccountID)
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
			(QL.LeadID = MSAC.LeadId)
GO
/* TEST */
SELECT * FROM [dbo].[vwAE_CustomerAccountInfoToGP]
