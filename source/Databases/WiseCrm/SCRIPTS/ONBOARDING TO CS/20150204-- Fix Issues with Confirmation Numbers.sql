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
DECLARE @MonitoringStationOSId VARCHAR(50) = 'AG_ALARMSYS';

UPDATE [dbo].[MS_AccountSalesInformations] SET
	CsConfirmationNumber = CAST(MSS.AccountSubmitID AS VARCHAR)
	, SubmittedToCSDate = MSS.DateSubmitted
	, InstallDate = MSS.DateSubmitted
--SELECT
--	MSS.*
--	, MASI.InstallDate
FROM
	[dbo].[MS_AccountSalesInformations] AS MASI WITH (NOLOCK)
	INNER JOIN 
	(SELECT
		ROW_NUMBER() OVER (PARTITION BY MAS.AccountID ORDER BY DateSubmitted) AS ROWNumber
		, MAS.*
	FROM
		[dbo].[MS_AccountSubmits] AS MAS WITH (NOLOCK)
	WHERE
		(AccountSubmitTypeId = 1)
		AND (WasSuccessfull = 1)) AS MSS
	ON
		(MSS.AccountId = MASI.AccountID)
WHERE
	(MSS.ROWNumber = 1)
	AND (MSS.MonitoringStationOSId = @MonitoringStationOSId)


--UPDATE [dbo].[MS_AccountSalesInformations] SET
--	CsConfirmationNumber = SUBSTRING(MASM.[ErrText], 24, 8)
--	, SubmittedToCSDate = RSLT.DateSubmitted
--	, InstallDate = RSLT.DateSubmitted
----SELECT
----	RSLT.*
----	, SUBSTRING(MASM.[ErrText], 24, 8) AS [Moni Confirmation#]
----	, MASM.*
--FROM
--	[dbo].[MS_AccountSalesInformations] AS MASI WITH (NOLOCK)
--	INNER JOIN 
--	(SELECT
--		--*
--		ROW_NUMBER() OVER (PARTITION BY MAS.AccountID ORDER BY DateSubmitted) AS ROWNumber
--		, MAS.AccountID
--		, MAS.AccountSubmitID
--		, MAS.MonitoringStationOSId
--		, MAS.DateSubmitted
--	FROM
--		[dbo].[MS_AccountSubmits] AS MAS WITH (NOLOCK)
--	WHERE
--		(AccountSubmitTypeId = 1)
--		AND (WasSuccessfull = 1)) AS RSLT
--	ON
--		(RSLT.AccountId = MASI.AccountID)
--	INNER JOIN [dbo].[MS_AccountSubmitMs] AS MASM WITH (NOLOCK)
--	ON
--		(MASM.AccountSubmitId = RSLT.AccountSubmitID)
--		AND (MASM.ErrNo = 199)
--WHERE
--	(RSLT.ROWNumber = 1)
	

ROLLBACK TRANSACTION