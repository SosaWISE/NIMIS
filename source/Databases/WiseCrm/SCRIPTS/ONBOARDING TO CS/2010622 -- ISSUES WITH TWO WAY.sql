/** 
* THIS IS FOR TROUBLE SHOOTING TWO WAY TEST
*/
USE WISE_CRM;

DECLARE @AccountID BIGINT = 191276;

SELECT * FROM [dbo].[MS_AccountSubmits] WHERE (AccountId = @AccountID);
SELECT * FROM [dbo].[MS_AccountSubmitMs] WHERE (AccountSubmitId IN (SELECT [AccountSubmitID] FROM [dbo].[MS_AccountSubmits] WHERE (AccountId = @AccountID)));
SELECT * FROM [dbo].[MS_AccountSubmitMsXmls] WHERE (AccountSubmitId IN (SELECT [AccountSubmitID] FROM [dbo].[MS_AccountSubmits] WHERE (AccountId = @AccountID)));

