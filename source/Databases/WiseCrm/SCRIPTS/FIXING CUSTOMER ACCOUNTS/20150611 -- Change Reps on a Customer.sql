USE [WISE_CRM]
GO

/**
Ralph Lynch -  3091655
Connisa Garcia - 3091647
Julie Padron - 3091648
William Leelum – 3091654
Cira Miranda - 3091653
Alberto Espinoza – 3091640
Ali Abdi – 3091649
Franklin West – 3091633
Bill Stockman – 3091642

*/

DECLARE @SalesRepID VARCHAR(50) = 'CARTB001'
	, @CMFID BIGINT = 3091655
	, @FullName VARCHAR(100) = 'Ralph Lynch';

SET @FullName = 'Charles Quarells';
SET @CMFID = 3091651;
EXEC dbo.wiseMC_ChangeSalesRepIdOnAccounts @SalesRepID, @CMFID, @FullName;
SET @FullName = 'Little Bethel COGIC';
SET @CMFID = 3091652;
EXEC dbo.wiseMC_ChangeSalesRepIdOnAccounts @SalesRepID, @CMFID, @FullName;
SET @FullName = 'Tonya Wallace';
SET @CMFID = 3091644;
EXEC dbo.wiseMC_ChangeSalesRepIdOnAccounts @SalesRepID, @CMFID, @FullName;


BEGIN TRANSACTION
SELECT * FROM [WISE_HumanResource].[dbo].RU_Users WHERE UserID = 2272;
UPDATE [WISE_HumanResource].[dbo].RU_Users SET GPEmployeeId = 'CORP000' WHERE UserID = 2272;

ROLLBACK TRANSACTION