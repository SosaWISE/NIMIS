USE [WISE_CRM]
GO
/** Declarations */
--191189 ACCOUNT WE'RE TRYING TO PUT ONLINE
--191103 ACCOUTNTHAT WENT ONLINE
/** Submit Types
AccountSubmitTypeID	AccountSubmitType
0	Undefined
1	Onboard System
2	Turn Service On
3	Turn Service On Pending
4	Turn Service On Cancel
5	Onboard and Turn Service On
6	Shell Account
7	Initiate Two Way Test
8	Pull Panel
*/
DECLARE @AccountID BIGINT = 191333
	, @AccountSubmitTypeId INT = 1  -- Onboard System
	, @WasSuccessfull BIT = NULL;
SELECT TOP 1 * FROM dbo.MS_AccountSubmits WHERE (AccountID = @AccountID) AND (AccountSubmitTypeId = @AccountSubmitTypeId) AND (@WasSuccessfull IS NULL OR WasSuccessfull = @WasSuccessfull) ORDER BY AccountSubmitID DESC;
SELECT * FROM MS_Accounts WHERE AccountID = @AccountID;
SELECT TOP 100 * FROM dbo.MS_AccountSubmitMs 
WHERE (AccountSubmitID IN (SELECT TOP 1 AccountSubmitID FROM dbo.MS_AccountSubmits WHERE (AccountID = @AccountID) AND (AccountSubmitTypeId = @AccountSubmitTypeId) AND (@WasSuccessfull IS NULL OR WasSuccessfull = @WasSuccessfull) ORDER BY AccountSubmitID DESC))
ORDER BY AccountSubmitMsID DESC;
SELECT * FROM dbo.MS_AccountSubmitMsXmls WHERE (AccountSubmitID IN (SELECT TOP 1 AccountSubmitID FROM dbo.MS_AccountSubmits WHERE (AccountID = @AccountID) AND (AccountSubmitTypeId = @AccountSubmitTypeId) AND (@WasSuccessfull IS NULL OR WasSuccessfull = @WasSuccessfull) ORDER BY AccountSubmitID DESC));


--SELECT * FROM dbo.MS_AccountSubmitMsXmls WHERE (AccountSubmitID = 41178);
--SELECT * FROM dbo.MS_AccountSubmitMs WHERE (AccountSubmitId = 41178);