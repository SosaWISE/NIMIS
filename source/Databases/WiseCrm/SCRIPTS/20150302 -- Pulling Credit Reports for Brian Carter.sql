USE [WISE_CRM]
GO

SELECT 
	CR.CreditReportID
	, CR.LeadId
	, CR.FirstName
	, CR.LastName
	, CRVA.Score
FROM
	dbo.QL_CreditReports AS CR WITH (NOLOCK)
	INNER JOIN dbo.QL_Leads AS QL WITH (NOLOCK)
	ON
		(QL.LeadID = CR.LeadId)
	INNER JOIN dbo.QL_CreditReportVendorAbara AS CRVA WITH (NOLOCK)
	ON
		(CRVA.CreditReportId = CR.CreditReportID)
WHERE
	(CR.CreatedOn > '08/25/2014')
	AND (CR.IsScored = 1)