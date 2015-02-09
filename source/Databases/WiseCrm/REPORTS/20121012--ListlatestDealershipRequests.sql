/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000
	[DealerLeadID]
	,[DealerLeadTypeId]
	,[DealerName]
	,[ContactFirstName]
	,[ContactLastName]
	,[ContactEmail]
	,[PhoneWork]
	,[PhoneMobile]
	--,[PhoneFax]
	--,[Address]
	--,[Address2]
	,[City]
	,[StateAB]
	,[PostalCode]
	--,[PlusFour]
	--,[Memo]
	,[Username]
	--,[Password]
	--,[IsActive]
	--,[IsDeleted]
	--,[ModifiedOn]
	--,[ModifiedBy]
	,[CreatedOn]
	--,[CreatedBy]
	--,[DEX_ROW_TS]
FROM
	[WISE_CRM].[dbo].[QL_DealerLeads] AS QDL WITH (NOLOCK)
WHERE
	(QDL.[CreatedOn] > '9/10/2012')
	AND (QDL.DealerLeadID NOT IN (65,64))
ORDER BY
	QDL.DealerLeadID DESC