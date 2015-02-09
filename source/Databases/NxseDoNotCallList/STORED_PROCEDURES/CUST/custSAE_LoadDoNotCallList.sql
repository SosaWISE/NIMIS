USE [NXSE_DoNotCallList]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSAE_LoadDoNotCallList')
	BEGIN
		PRINT 'Dropping Procedure custSAE_LoadDoNotCallList'
		DROP  Procedure  dbo.custSAE_LoadDoNotCallList
	END
GO

PRINT 'Creating Procedure custSAE_LoadDoNotCallList'
GO
/******************************************************************************
**		File: custSAE_LoadDoNotCallList.sql
**		Name: custSAE_LoadDoNotCallList
**		Desc: The Do Not Call list is maintained by the government and contains
**		the phone numbers of people and businesses that do not wish to be 
**		contacted via phone.
**		Every month we need to download a text file from the donotcall.gov site
**		and load it into the WISE_CRM.dbo.DC_PhoneNumbers table
**
**		The steps to be followed are as follows:
**		1) login to the donotcall.gov site. Download the text file and save it.
**		
**		2) import the data file into the SAE_DoNotCall table in the 
**		NXSE_DoNotCallList database.
**
**		3) Run this stored procedure or the job agent that calls it.
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Bob McFadden
**		Date: 06/09/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/09/2014	Bob McFadden	Created
**	08/26/2014	Bob McFadden	commented out the alter table to add PhoneNumberID
**	
*******************************************************************************/
CREATE Procedure dbo.custSAE_LoadDoNotCallList
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
		DECLARE @NumberAdded BIGINT
		DECLARE @NumberRemoved BIGINT
		DECLARE @TotalCurrentDNC BIGINT
		DECLARE @TransactionID BIGINT

	BEGIN TRY
		BEGIN TRANSACTION;

		-- Add the PhoneNumberID column to the SAE_DoNotCall table
		--ALTER TABLE SAE_DoNotCall ADD PhoneNumberID CHAR(10)

		-- Set the PhoneNumberID by concatenating the area code and phone number
		UPDATE SAE_DoNotCall SET PhoneNumberID = AreaCode + PhoneNumber

		SELECT @TotalCurrentDNC = COUNT(*) FROM dbo.SAE_DoNotCall

	/***********************
	***  ADD AREA CODES  ***
	************************/
	-- IF THE DNC LIST COMES IN WITH ADDITIONAL AREA CODES, ADD THE AREA CODES TO THE AREA CODE TABLE
	INSERT dbo.DC_AreaCodes
		(
		AreaCodeID
		)
	SELECT DISTINCT AreaCode
	FROM 
		SAE_DoNotCall
		LEFT JOIN dbo.DC_AreaCodes
			ON dbo.SAE_DoNotCall.AreaCode = dbo.DC_AreaCodes.AreaCodeID
	WHERE dbo.DC_AreaCodes.AreaCodeID IS NULL

	/**********************
	INSERT TRANSACTION  ***
	***********************/
	INSERT DC_Transactions
		( 
		TransactionDate ,
		NumberAdded ,
		NumberRemoved ,
		TotalDncRecords
		)
	VALUES  
		( 
		GETDATE() , -- TransactionDate - datetime
		0 , -- Set NumberAdded to 0 and update later in the add DNC step
		0 , -- Set NumberRemoved to 0 and update later in the remove DNC step
		@TotalCurrentDNC  -- Set TotalDncRecords to the number of rows in the current DNC list
		)

	SET @TransactionID = SCOPE_IDENTITY();

	/*************************
	***  GET LIST OF ADDS  ***
	**************************/
	-- If the Phone number is in the new DNC list but not in our DNC table we are adding them
	IF object_id('tempdb..#addDNC') IS NOT NULL
	BEGIN DROP TABLE #addDNC END

	SELECT
		new_DNC.PhoneNumberID AS PhoneNumberID,
		new_DNC.AreaCode AS AreaCode,
		new_DNC.PhoneNumber AS PhoneNumber
	INTO #addDNC
	FROM 
		SAE_DoNotCall AS new_DNC
		LEFT JOIN dbo.DC_PhoneNumbers AS our_DNC
			ON new_DNC.PhoneNumberID = our_DNC.PhoneNumberID
			and our_DNC.IsDeleted = 0
	WHERE our_DNC.PhoneNumberID IS NULL 

	SET @NumberAdded = @@ROWCOUNT

	/*****************************
	***  GET LIST OF REMOVALS  ***
	*****************************/
	-- If the Phone number is in our DNC table (and not marked deleted) but is not on the new DNC list we are removing
	IF object_id('tempdb..#removeDNC') IS NOT NULL
	BEGIN DROP TABLE #removeDNC END

	SELECT 
		our_DNC.PhoneNumberID AS PhoneNumberID
		--,
		--our_DNC.AreaCodeId AS AreaCode,
		--our_DNC.PhoneNumber AS PhoneNumber
	INTO #removeDNC
	FROM 
		DC_PhoneNumbers AS our_DNC
		LEFT JOIN SAE_DoNotCall AS new_DNC
			ON our_DNC.PhoneNumberID = new_DNC.PhoneNumberID
	WHERE new_DNC.PhoneNumberID IS NULL AND our_DNC.IsDeleted = 0

	SET @NumberRemoved = @@ROWCOUNT

	-- UPDATE TRANSACTION WITH NUMBER ADDED AND REMOVED
	UPDATE dbo.DC_Transactions
	SET 
		NumberAdded = @NumberAdded,
		NumberRemoved = @NumberRemoved
	WHERE TransactionID = @TransactionID

	/***********
	*** ADDS ***
	************/
	-- IF THE PHONE NUMBER IN THE DNC TABLE (and is currently marked deleted), SET IsDeleted to 0
	UPDATE DC_PhoneNumbers
		SET IsDeleted = 0
	FROM 
		DC_PhoneNumbers
		JOIN #addDNC
			ON DC_PhoneNumbers.PhoneNumberID = #addDNC.PhoneNumberID
			AND dbo.DC_PhoneNumbers.IsDeleted = 1

	-- IF THE PHONE NUMBER IS NOT IN THE DNC TABLE, INSERT IT
	INSERT DC_PhoneNumbers
		(
		PhoneNumberID,
		AreaCodeId,
		PhoneNumber,
		IsDeleted
		)
	SELECT 
		new_DNC.AreaCode + new_DNC.PhoneNumber,
		new_DNC.AreaCode,
		new_DNC.PhoneNumber,
		0 -- not deleted
	FROM 
		#addDNC AS new_DNC
		LEFT JOIN dbo.DC_PhoneNumbers AS our_DNC
			ON new_DNC.PhoneNumberID = our_DNC.PhoneNumberID
	WHERE our_DNC.PhoneNumberID IS NULL

	-- LOG ADDED PHONE NUMBERS IN THE TRANSACTION PHONE NUMBERS TABLE
	INSERT DC_TransactionPhoneNumbers
		( 
		TransactionId,
		PhoneNumberId,
		Added
		)
	SELECT
		@TransactionID,
		PhoneNumberID,
		1 -- Added = 1, removed = 0
	FROM 
		#addDNC

	/****************
	***  REMOVAL  ***
	*****************/
	-- IF THE PHONE NUMBER IN THE DNC TABLE, SET IsDeleted to 0
	UPDATE DC_PhoneNumbers
		SET IsDeleted = 1
	FROM 
		DC_PhoneNumbers
		JOIN #removeDNC
			ON dbo.DC_PhoneNumbers.PhoneNumberID = #removeDNC.PhoneNumberID

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
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC
		 wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custSAE_LoadDoNotCallList TO PUBLIC
GO

/** EXEC dbo.custSAE_LoadDoNotCallList */