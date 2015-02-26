USE NXSE_DoNotCallList
GO
/*
When new DNC lists are downloaded the following is done to import the new list:
	1) Validate the PhoneNumberID - the PhoneNumberID is a 10 character number which is the area code and phone number concatentated
	2) Add new numbers - add new phone numbers to the DC_PhoneNumbers table.
	3) Reactivate numbers - previously inactivated numbers (IsDeleted = 'true') that are on the new list are reactivated by setting IsDeleted to 'False'.
	4) Deactivate numbers - if the number is not on the current list but is active on DC_PhoneNumbers it is deactivated by setting IsDeleted to 'true'.
	5) Log the transaction - create a new transaction and log 
		- the total number of DNC records
		- date processed
		- the number reactivated
		- number removed (deactivated)

Error checking:
Verify:
	total number of distinct phone numbers in the new DNC list (TDNC) = total added (TA) + total reactivated (TR) + total unchanged (TU)
*/
DECLARE @TotalCurrentDNC BIGINT
DECLARE @NumberToAdd BIGINT
DECLARE @NumberToDeactivate BIGINT
DECLARE @NumberToReactivate BIGINT
DECLARE @NumberUnchanged BIGINT

DECLARE @ActualAdd BIGINT
DECLARE @ActualInactivate BIGINT
DECLARE @ActualReactivate BIGINT
DECLARE @TransactionID BIGINT

DECLARE @ListSourceID INT

SET @ListSourceID = 5
/*
DNCListSourceID	DNCListSourceDesc
1	DoNotCall.Gov
2	Colorado State
3	Indiana State
4	Indiana Federal
5	Missouri State
*/

BEGIN TRANSACTION

/***************************
***  1) Data Validation  ***
****************************/
PRINT 'Data Validation'
-- If PhoneNumberID is NULL, set it by concatenating the area code and phone number
UPDATE SAE_DoNotCall
SET PhoneNumberID = AreaCode + PhoneNumber
WHERE 
	(PhoneNumberID IS NULL)
	AND ((AreaCode IS NOT NULL) AND (PhoneNumber IS NOT NULL))

-- If AreaCode or PhoneNumber is NULL, set them from the PhoneNumberID
UPDATE SAE_DoNotCall
SET 
	AreaCode = LEFT(PhoneNumberID,3),
	PhoneNumber = RIGHT(PhoneNumberID,7)
WHERE 
	(PhoneNumberID IS NOT NULL)
	AND ((AreaCode IS NULL) OR (PhoneNumber IS NULL))

/****************************
***  2) GET LIST OF ADDS  ***
*****************************/
PRINT 'Get ADDS'
-- If the Phone number from the new DNC list is not already in the DNC table, we add it
IF object_id('tempdb..#addDNC') IS NOT NULL BEGIN DROP TABLE #addDNC END

SELECT DISTINCT
	new_DNC.PhoneNumberID AS PhoneNumberID
INTO #addDNC
FROM 
	dbo.SAE_DoNotCall AS new_DNC
	LEFT JOIN dbo.DC_PhoneNumbers AS DNC
		ON new_DNC.PhoneNumberID = DNC.PhoneNumberID
WHERE DNC.PhoneNumberID IS NULL

/***********************************
***  3) GET LIST OF REACTIVATED  ***
************************************/
PRINT 'Get REACTIVATIONS'
-- If the Phone number from the new DNC list is on the DNC table and marked deleted, we reactivate it
-- NOTE: On the DNC table, if IsDelete = 'false' the phone number cannot be called.  If true, it has been removed from DNC. 
IF object_id('tempdb..#reactivateDNC') IS NOT NULL BEGIN DROP TABLE #reactivateDNC END

SELECT DISTINCT
	new_DNC.PhoneNumberID AS PhoneNumberID
INTO #reactivateDNC
FROM 
	dbo.SAE_DoNotCall AS new_DNC WITH(NOLOCK)
	JOIN dbo.DC_PhoneNumbers AS DNC WITH(NOLOCK)
		ON new_DNC.PhoneNumberID = DNC.PhoneNumberID
		AND DNC.IsDeleted = 'TRUE'

/********************************
***  GET LIST OF DEACTIVATED  ***
*********************************/
PRINT 'Get DEACTIVATIONS'
-- If the Phone number is in the DNC table and not marked deleted, 
-- and is from the DNC Source List we are processing, we are marking it as deleted
IF object_id('tempdb..#removeDNC') IS NOT NULL BEGIN DROP TABLE #removeDNC END
/*
SELECT DISTINCT
	DNC.PhoneNumberID AS PhoneNumberID
INTO #removeDNC
FROM 
	DC_PhoneNumbers AS DNC
	JOIN SAE_DoNotCall AS new_DNC
		ON DNC.PhoneNumberID = new_DNC.PhoneNumberID
WHERE
	(DNC.IsDeleted = 'FALSE')
	AND (DNC.AreaCodeId IN (SELECT DISTINCT AreaCode FROM SAE_DoNotCall))
*/
SELECT DISTINCT activeDNC_qry.PhoneNumberID
INTO #removeDNC
FROM
	-- phone numbers that are active on DNC
	(
	SELECT 
		PhoneNumberID
	FROM 
		DC_PhoneNumbers
	WHERE 
		IsDeleted = 'FALSE'
	) AS activeDNC_qry
	JOIN
	-- phone numbers for the DNC source list we are processing
	(
	SELECT 
		DCTP.PhoneNumberId
	FROM 
		DC_TransactionPhoneNumbers AS DCTP
		JOIN DC_Transactions AS DCT
			ON DCTP.TransactionId = DCT.TransactionID
			AND DCT.DNCListSourceId = @ListSourceID
	) AS TransPhones_qry
		ON activeDNC_qry.PhoneNumberID = TransPhones_qry.PhoneNumberId
	LEFT JOIN SAE_DoNotCall
		ON activeDNC_qry.PhoneNumberID = SAE_DoNotCall.PhoneNumberID
WHERE SAE_DoNotCall.PhoneNumberID IS NULL

/*******************************
***  GET TRANSACTION COUNTS  ***
********************************/
-- Total on the new DNC list
SELECT @TotalCurrentDNC = COUNT(DISTINCT PhoneNumberID) FROM dbo.SAE_DoNotCall WHERE PhoneNumberID IS NOT NULL

-- Number to ADD
SELECT @NumberToAdd = COUNT(*) FROM #addDNC

-- Number to REACTIVATE
SELECT @NumberToReactivate = COUNT(*) FROM #reactivateDNC

-- Number UNCHANGED
SELECT 
	@NumberUnchanged = COUNT(*)
FROM
	dbo.SAE_DoNotCall AS new_DNC WITH(NOLOCK)
	JOIN dbo.DC_PhoneNumbers AS DNC WITH(NOLOCK)
		ON new_DNC.PhoneNumberID = DNC.PhoneNumberID
		AND DNC.IsDeleted = 'FALSE'

-- Number to DEACTIVATE
SELECT @NumberToDeactivate = COUNT(*) FROM #removeDNC
/*
SELECT 
@TotalCurrentDNC AS Total,
@NumberToAdd AS Adds,
@NumberToReactivate as Reactivate,
@NumberToDeactivate AS Deactivate,
@NumberUnchanged AS Unchanged
*/

-- VALIDATE COUNTS
IF (@NumberToAdd + @NumberToDeactivate + @NumberToReactivate) = 0
BEGIN
	PRINT 'No Updates are necessary'
	RETURN
END

IF @TotalCurrentDNC <> (@NumberToAdd + @NumberToReactivate + @NumberToDeactivate + @NumberUnchanged)
BEGIN
	PRINT 'Totals do not match'
	RETURN
END

/**********************
INSERT TRANSACTION  ***
***********************/
INSERT DC_Transactions
	( 
	DNCListSourceId,
	TransactionDate,
	NumberAdded,
	NumberRemoved,
	TotalDncRecords
	)
VALUES  
	( 
	@ListSourceID,
	GETDATE(), -- TransactionDate = datetime
	@NumberToAdd + @NumberToReactivate,
	@NumberToDeactivate,
	@TotalCurrentDNC
	)

SET @TransactionID = SCOPE_IDENTITY()

/***********
*** ADDS ***
************/
IF (@NumberToAdd > 0)
BEGIN
	PRINT 'ADD'
	INSERT DC_PhoneNumbers
		(
		PhoneNumberID,
		AreaCodeId,
		PhoneNumber,
		IsDeleted
		)
	SELECT DISTINCT
		addDNC.PhoneNumberID,
		LEFT(addDNC.PhoneNumberID,3),
		RIGHT(addDNC.PhoneNumberID,3),
		0 -- not deleted
	FROM 
		#addDNC AS addDNC

	SET @ActualAdd = @@ROWCOUNT

	-- LOG ADDED PHONE NUMBERS IN THE TRANSACTION PHONE NUMBERS TABLE
	INSERT DC_TransactionPhoneNumbers
		( 
		TransactionId,
		PhoneNumberId,
		Added
		)
	SELECT DISTINCT
		@TransactionID,
		addDNC.PhoneNumberID,
		1 -- Added = 1, removed = 0
	FROM 
		#addDNC AS addDNC

	IF @NumberToAdd <> @ActualAdd
		PRINT 'Add: numbers do not match'
	ELSE
		PRINT 'Add: numbers match'
END -- ADD

/*******************
***  REACTIVATE  ***
********************/
IF (@NumberToReactivate > 0)
BEGIN
	PRINT 'REACTIVATE'
	-- If the phone number is in the DNC table, set IsDeleted to TRUE
	UPDATE DNC
		SET IsDeleted = 'FALSE'
	FROM 
		DC_PhoneNumbers as DNC
		JOIN #reactivateDNC as activateDNC WITH(NOLOCK)
			ON DNC.PhoneNumberID = activateDNC.PhoneNumberID
	
	SET @ActualReactivate = @@ROWCOUNT

	-- LOG REACTIVATED PHONE NUMBERS IN THE TRANSACTION PHONE NUMBERS TABLE
	INSERT DC_TransactionPhoneNumbers
		( 
		TransactionId,
		PhoneNumberId,
		Added
		)
	SELECT
		@TransactionID,
		reactivateDNC.PhoneNumberID,
		1 -- Added = 1, removed = 0
	FROM 
		#reactivateDNC AS reactivateDNC

	IF (@NumberToReactivate <> @ActualReactivate)
		PRINT 'Reactivate: numbers do not match'
	ELSE
		PRINT 'Reactivate: numbers match'
END --REACTIVATE

/*******************
***  DEACTIVATE  ***
********************/
IF (@NumberToDeactivate > 0)
BEGIN
	PRINT 'DEACTIVATE'
	-- If the phone number is on the DNC table, set IsDeleted to true
	UPDATE DC_PhoneNumbers
		SET IsDeleted = 'TRUE'
	FROM 
		DC_PhoneNumbers as DNC
		JOIN #removeDNC AS removeDNC
			ON DNC.PhoneNumberID = removeDNC.PhoneNumberID

		SET @ActualInactivate = @@ROWCOUNT

	-- LOG ADDED PHONE NUMBERS IN THE TRANSACTION PHONE NUMBERS TABLE
	INSERT dbo.DC_TransactionPhoneNumbers
		( 
		TransactionId,
		PhoneNumberId,
		Added
		)
	SELECT
		@TransactionID,
		PhoneNumberID,
		0 -- Added = 1, removed = 0
	FROM 
		#removeDNC

	IF (@NumberToDeactivate <> @ActualInactivate)
		PRINT 'Deactivate: numbers do not match'
	ELSE
		PRINT 'Deactivate: numbers '

END -- INACTIVATE
COMMIT
