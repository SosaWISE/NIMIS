USE [WISE_CRM]
GO
/** Declarations */
DECLARE @AccountID BIGINT = 191147;
SELECT TOP 1 * FROM dbo.MS_AccountSubmits WHERE AccountID = @AccountID ORDER BY AccountSubmitID DESC;
SELECT * FROM MS_Accounts WHERE AccountID = @AccountID;
SELECT TOP 100 * FROM dbo.MS_AccountSubmitMs 
WHERE (AccountSubmitID IN (SELECT TOP 1 AccountSubmitID FROM dbo.MS_AccountSubmits WHERE AccountId = @AccountID ORDER BY AccountSubmitID DESC))
ORDER BY AccountSubmitMsID DESC;
SELECT * FROM dbo.MS_AccountSubmitMsXmls WHERE (AccountSubmitID IN (SELECT TOP 1 AccountSubmitID FROM dbo.MS_AccountSubmits WHERE AccountId = @AccountID ORDER BY AccountSubmitID DESC));