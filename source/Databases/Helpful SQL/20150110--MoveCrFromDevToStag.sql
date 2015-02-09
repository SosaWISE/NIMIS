USE [WISE_CRM]
GO
/** PROD  */
SELECT
	*
FROM
	[dbo].[MC_Accounts] AS MCA WITH (NOLOCK)
	INNER JOIN [dbo].[AE_Customers] AS CUST WITH (NOLOCK)
	ON
		(CUST.CustomerID = MCA.ShipContactId)
WHERE
	(AccountID = 181056);

SELECT * FROM [dbo].[AE_Customers] WHERE CustomerMasterFileId = 3081355

SELECT 
	CR.*
FROM
	dbo.QL_CreditReports AS CR WITH (NOLOCK)
	--INNER JOIN dbo.QL_CreditReportVendorAbara AS CRA WITH (NOLOCK)
	--ON
	--	(CRA.CreditReportId = CR.CreditReportID)
WHERE
	CR.LeadId = 1081103
ORDER BY
	CreditReportID DESC;



BEGIN TRANSACTION
DECLARE @AbID BIGINT;

--

INSERT INTO [dbo].[QL_CreditReportVendorAbara]
        ( [CreditReportId] ,
          [BureauId] ,
          [ReportID] ,
          [ReportGuid] ,
          [Result] ,
          [Score] ,
          [IsScored] ,
          [IsHit] ,
          [ReportHtml] ,
          [ReportXML] ,
          [ErrorMessage] ,
          [HitStatus] ,
          [DecisionCode] ,
          [DecisionText] ,
          [CreatedBy] ,
          [CreatedOn]
        )
	SELECT
	        85651 AS CreditReportId ,
	        BureauId ,
	        ReportID ,
	        ReportGuid ,
	        Result ,
	        Score ,
	        IsScored ,
	        IsHit ,
	        ReportHtml ,
	        ReportXML ,
	        ErrorMessage ,
	        HitStatus ,
	        DecisionCode ,
	        DecisionText ,
	        CreatedBy ,
	        CreatedOn 
	FROM [DB1.DEV.NEXSENSE.COM].[WISE_CRM].[dbo].[QL_CreditReportVendorAbara] WHERE CreditReportVendorAbaraID = 74191;

SET @AbID = SCOPE_IDENTITY();

PRINT 'IDENTITY: ' + CAST(@ABId AS VARCHAR(50))

ROLLBACK TRANSACTION