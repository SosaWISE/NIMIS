USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwQL_CreditReportTransactionAndToken')
	BEGIN
		PRINT 'Dropping VIEW vwQL_CreditReportTransactionAndToken'
		DROP VIEW dbo.vwQL_CreditReportTransactionAndToken
	END
GO

PRINT 'Creating VIEW vwQL_CreditReportTransactionAndToken'
GO

/****** Object:  View [dbo].[vwQL_CreditReportTransactionAndToken]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwQL_CreditReportTransactionAndToken.sql
**		Name: vwQL_CreditReportTransactionAndToken
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
**		Date: 01/10/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/10/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwQL_CreditReportTransactionAndToken]
AS
	-- Enter Query here
	SELECT
		QCR.CreditReportID
		, QCRV.VendorName
		, QCR.BureauId
		, CAST(QCRA.ReportID AS VARCHAR(50)) AS TransactionID
		, CAST(QCRA.ReportGuid AS VARCHAR(50)) AS Token
		, QCRA.CreatedOn
	FROM
		[dbo].[QL_CreditReports] AS QCR WITH (NOLOCK)
		INNER JOIN [dbo].[QL_CreditReportVendors] AS QCRV WITH (NOLOCK)
		ON
			(QCRV.CreditReportVendorID = QCR.CreditReportVendorId)
		INNER JOIN [dbo].[QL_CreditReportVendorAbara] AS QCRA WITH (NOLOCK)
		ON
			(QCRA.CreditReportVendorAbaraID = QCR.CreditReportVendorAbaraId)
	UNION
	SELECT
		QCR.CreditReportID
		, QCRV.VendorName
		, QCR.BureauId
		, CAST(QCRM.CreditReportVendorMicrobiltID AS VARCHAR(50)) AS TransactionID
		, CAST(QCRM.MicroBiltGUID AS VARCHAR(50)) AS Token
		, QCRM.CreatedOn
	FROM
		[dbo].[QL_CreditReports] AS QCR WITH (NOLOCK)
		INNER JOIN [dbo].[QL_CreditReportVendors] AS QCRV WITH (NOLOCK)
		ON
			(QCRV.CreditReportVendorID = QCR.CreditReportVendorId)
		INNER JOIN [dbo].[QL_CreditReportVendorMicrobilt] AS QCRM WITH (NOLOCK)
		ON
			(QCRM.CreditReportVendorMicrobiltID = QCR.CreditReportVendorMicrobiltId)

GO
/* TEST */
-- SELECT * FROM vwQL_CreditReportTransactionAndToken
