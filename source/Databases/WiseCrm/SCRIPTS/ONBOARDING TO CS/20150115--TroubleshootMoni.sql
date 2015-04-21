USE [WISE_CRM]
GO
/** Declarations */
--191189 ACCOUNT WE'RE TRYING TO PUT ONLINE
--191103 ACCOUTNTHAT WENT ONLINE
DECLARE @AccountID BIGINT = 191217
	, @AccountSubmitTypeId INT = 1
	, @WasSuccessfull BIT = NULL;
SELECT TOP 100 * FROM dbo.MS_AccountSubmits WHERE (AccountID = @AccountID) AND (AccountSubmitTypeId = @AccountSubmitTypeId) AND (@WasSuccessfull IS NULL OR WasSuccessfull = @WasSuccessfull) ORDER BY AccountSubmitID DESC;
SELECT * FROM MS_Accounts WHERE AccountID = @AccountID;
SELECT TOP 1 * FROM dbo.MS_AccountSubmitMs 
WHERE (AccountSubmitID IN (SELECT TOP 1 AccountSubmitID FROM dbo.MS_AccountSubmits WHERE (AccountID = @AccountID) AND (AccountSubmitTypeId = @AccountSubmitTypeId) AND (@WasSuccessfull IS NULL OR WasSuccessfull = @WasSuccessfull) ORDER BY AccountSubmitID DESC))
ORDER BY AccountSubmitMsID DESC;
SELECT * FROM dbo.MS_AccountSubmitMsXmls WHERE (AccountSubmitID IN (SELECT TOP 1 AccountSubmitID FROM dbo.MS_AccountSubmits WHERE (AccountID = @AccountID) AND (AccountSubmitTypeId = @AccountSubmitTypeId) AND (@WasSuccessfull IS NULL OR WasSuccessfull = @WasSuccessfull) ORDER BY AccountSubmitID DESC));