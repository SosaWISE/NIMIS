USE [WISE_CRM]
GO

DECLARE @CMFID BIGINT = 3091386;

SELECT
	CRVA.*
FROM
	[dbo].[QL_Leads] AS QL WITH (NOLOCK)
	INNER JOIN [dbo].[QL_CreditReports] AS QCR WITH (NOLOCK)
	ON
		(QCR.LeadId = QL.LeadID)
	/** ABARA */
	INNER JOIN [dbo].[QL_CreditReportVendorAbara] AS CRVA WITH (NOLOCK)
	ON
		(CRVA.CreditReportId = QCR.CreditReportID)
WHERE
	(QL.CustomerMasterFileId = @CMFID);

/** This is for customers 
SELECT 
	*
FROM
	[dbo].[MS_AccountCustomers] AS MAC WITH (NOLOCK)
	INNER JOIN [dbo].[AE_Customers] AS AC WITH (NOLOCK)
	ON
		(AC.CustomerID = MAC.CustomerId)
WHERE
	(AC.CustomerMasterFileId = @CMFID);
*/