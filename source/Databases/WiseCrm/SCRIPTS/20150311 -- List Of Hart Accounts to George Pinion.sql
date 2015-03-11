USE [WISE_CRM]
GO

SELECT
	CR.CreditReportID
	, LD.LeadID
	, CRHV.TransactionID
	, CRHV.Token
	, LD.FirstName
	, LD.MiddleName
	, LD.LastName
	, ADR.StreetAddress
	, ADR.StreetAddress2
	, ADR.City
	, ADR.StateID
	, ADR.PostalCode
	, CR.IsScored
	, CR.Score
	, CR.IsHit
	, CRHV.ReportHtml
	, CRHV.XmlResponse
FROM
	dbo.QL_CreditReports AS CR WITH (NOLOCK)
	INNER JOIN dbo.QL_Leads AS LD WITH (NOLOCK)
	ON
		(CR.LeadId = LD.LeadID)
	INNER JOIN dbo.QL_Address AS ADR WITH (NOLOCK)
	ON
		(LD.AddressId = ADR.AddressID)
	INNER JOIN dbo.QL_CreditReportVendorHartSoftware AS CRHV WITH (NOLOCK)
	ON
		(CRHV.[CreditReportVendorHartSoftwareId] = CR.[CreditReportVendorHartSoftwareId])
WHERE
	(CR.CreditReportVendorID = 4)