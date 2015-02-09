USE [WISE_CRM]
GO
/**
TODO:  Populate the following fields in MS_AccountSalesInformations
	InstallDate
	SubmittedToCSDate
	CsConfirmationNumber
	
*/

-- SELECT * FROM [dbo].[MS_AccountSubmitTypes]

BEGIN TRANSACTION
DECLARE @MonitoringStationOSId VARCHAR(50) = 'MI_MASTER';

SELECT
	*
FROM
	(SELECT
		--*
		ROW_NUMBER() OVER (PARTITION BY MAS.AccountID ORDER BY DateSubmitted) AS ROWNumber
		, MAS.AccountID
		, MAS.AccountSubmitID
		, MAS.MonitoringStationOSId
		, MAS.DateSubmitted
	FROM
		[dbo].[MS_AccountSubmits] AS MAS WITH (NOLOCK)
	WHERE
		(AccountSubmitTypeId = 1)
		AND (WasSuccessfull = 1)) AS RSLT
WHERE
	(RSLT.ROWNumber = 1)

ROLLBACK TRANSACTION