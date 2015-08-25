USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwReports_Performance')
	BEGIN
		PRINT 'Dropping VIEW vwReports_Performance'
		DROP VIEW dbo.vwReports_Performance
	END
GO

PRINT 'Creating VIEW vwReports_Performance'
GO

/****** Object:  View [dbo].[vwReports_Performance]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwReports_Performance.sql
**		Name: vwReports_Performance
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
**		Date: 07/31/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/31/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwReports_Performance]
AS
	-- Enter Query here
	SELECT
		MSASI.TeamLocationId AS OfficeId
		, MSASI.AccountID
		, MSASI.ContractLength AS Term
		, 0 AS [CloseRate]
		, MSASI.SetupFee AS [SetupFee]
		, MSASI.SetupFee1stMonth AS [FirstMonth]
		, MSASI.Over3Months AS [Over3Months]
		, MSASI.AccountPackageId AS [PackageSoldId]
		, MSASCL.SubmitAccountOnline
		, MSASI.InstallDate
		, MSASI.DealerId
		, MSASI.SalesRepId
		, MSASI.SeasonId
	FROM
		[WISE_CRM].[dbo].vwMS_AccountSalesInformations AS MSASI WITH (NOLOCK)
		--ON
		--	(OFFICE.OfficeId = MSASI.TeamLocationId)
		INNER JOIN [WISE_CRM].[dbo].[MS_AccountSetupCheckLists] AS MSASCL WITH (NOLOCK)
		ON
			(MSASCL.AccountID = MSASI.AccountID)
		INNER JOIN [WISE_CRM].[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
		ON
			(AECA.AccountId = MSASI.AccountID)
			AND (AECA.CustomerTypeId = 'PRI')
		INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS AEC WITH (NOLOCK)
		ON
			(AEC.CustomerID = AECA.CustomerId)
GO
/* TEST */
SELECT TOP 100 * FROM vwReports_Performance ORDER BY AccountID DESC;
