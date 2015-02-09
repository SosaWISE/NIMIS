USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_ReceiverLineBlockAccountGetNewByAccountID')
	BEGIN
		PRINT 'Dropping Procedure custMS_ReceiverLineBlockAccountGetNewByAccountID'
		DROP  Procedure  dbo.custMS_ReceiverLineBlockAccountGetNewByAccountID
	END
GO

PRINT 'Creating Procedure custMS_ReceiverLineBlockAccountGetNewByAccountID'
GO
/******************************************************************************
**		File: custMS_ReceiverLineBlockAccountGetNewByAccountID.sql
**		Name: custMS_ReceiverLineBlockAccountGetNewByAccountID
**		Desc: 
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
**		Auth: Andres Sosa
**		Date: 00/00/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	00/00/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_ReceiverLineBlockAccountGetNewByAccountID
(
	@AccountID BIGINT,
	@CreatedBy NVARCHAR(50),
	@ReceiverLineID VARCHAR(20)
)
AS
BEGIN
	-- Locals
	--DECLARE @ReceiverLineID VARCHAR(20) = 'AG_GPSTRACK:AB'
	DECLARE @tblResults TABLE (IndustryAccountID VARCHAR(15), BlockID VARCHAR(50), CSID VARCHAR(15), HasError BIT, ErrorMsg VARCHAR(500))
	DECLARE @BlockID VARCHAR(50)
	DECLARE @SubscriberNumber NVARCHAR(6)
	DECLARE @Designator VARCHAR(6)
	
	-- Set initial values
	INSERT INTO @tblResults SELECT NULL, NULL, NULL, 0, NULL
	
	-- Find Receiver Line first get Ranges
	DECLARE @Start INT
	DECLARE @End INT
	
	-- -- Find Receiver Line
	SELECT @Start = RL.StartNumber, @End = RL.EndNumber, @Designator = RL.Designator FROM MS_ReceiverLines AS RL WITH (NOLOCK) WHERE (RL.ReceiverLineID = @ReceiverLineID)
	
	-- Check that we found a receiver line
	IF (@Start IS NULL)
	BEGIN
		UPDATE @tblResults SET HasError = 1, ErrorMsg = ErrorMsg + ':Receiver Line was not found'
		SELECT * FROM @tblResults
		RETURN  /***** EXIT *******/
	END
	
	BEGIN TRY
	
	/******* TRANSACTION BEGINS *********/
		BEGIN TRAN
	/******* TRANSACTION BEGINS *********/
		
		-- Execute Search for Max subscriber
		SELECT TOP 1
			@BlockID = RLB.ReceiverLineBlockID
			, @SubscriberNumber = RLB.SubscriberNumber
		FROM
			MS_ReceiverLineBlocks AS RLB WITH (NOLOCK)
		WHERE
			(CAST (RLB.SubscriberNumber AS INT) BETWEEN @Start AND @END)
			AND (RLB.ReceiverLineId = @ReceiverLineID)
			--AND (RLB.IsAssigned = 0)
			--AND (RLB.AssignedDate IS NULL)
			--AND (RLB.AccountID IS NULL)
			--AND (RLB.IsActive = 1)
			--AND (RLB.IsDeleted = 0)
		ORDER BY
			RLB.SubscriberNumber DESC
		
		-- Check that the max subscriber number was found
		PRINT '@BlockID: ' + @BlockID
		IF (@BlockID IS NULL) 
		BEGIN
			PRINT 'BlockID IS NULL 1112'
			SET @SubscriberNumber = CASE 
					WHEN @Start < 10 THEN '00000' + LTRIM(RTRIM(CAST(@Start AS VARCHAR)))
					WHEN @Start < 100 THEN '0000' + LTRIM(RTRIM(CAST(@Start AS VARCHAR)))
					WHEN @Start < 1000 THEN '000' + LTRIM(RTRIM(CAST(@Start AS VARCHAR)))
					WHEN @Start < 10000 THEN '00' + LTRIM(RTRIM(CAST(@Start AS VARCHAR)))
					WHEN @Start < 100000 THEN '0' + LTRIM(RTRIM(CAST(@Start AS VARCHAR)))
					ELSE LTRIM(RTRIM(CAST(@Start AS VARCHAR)))
				END 
		END
		ELSE
		BEGIN
			PRINT 'BlockID IS NOT NULL 1113'
			DECLARE @SubNumber INT
			SET @SubNumber = CAST(@SubscriberNumber AS INT) + 1
			
			PRINT 'SubNumber: ' + CAST(@SubNumber AS VARCHAR)
			-- -- Check that this new number is in range
			IF (NOT @SubNumber BETWEEN @Start AND @End)
			BEGIN
				UPDATE @tblResults SET HasError = 1, ErrorMsg = ErrorMsg + ':Receiver Line max range has been reached.'
				SELECT * FROM @tblResults
	/******* TRANSACTION ENDS *********/
				PRINT 'TRAN ROLLBACK 111'
				ROLLBACK TRAN
				RETURN
	/******* TRANSACTION ENDS *********/
			END

			-- -- Create the subscriber number
			SET @SubscriberNumber = CASE 
					WHEN @SubNumber < 10 THEN '00000' + LTRIM(RTRIM(CAST(@SubNumber AS VARCHAR)))
					WHEN @SubNumber < 100 THEN '0000' + LTRIM(RTRIM(CAST(@SubNumber AS VARCHAR)))
					WHEN @SubNumber < 1000 THEN '000' + LTRIM(RTRIM(CAST(@SubNumber AS VARCHAR)))
					WHEN @SubNumber < 10000 THEN '00' + LTRIM(RTRIM(CAST(@SubNumber AS VARCHAR)))
					WHEN @SubNumber < 100000 THEN '0' + LTRIM(RTRIM(CAST(@SubNumber AS VARCHAR)))
					ELSE LTRIM(RTRIM(CAST(@SubNumber AS VARCHAR)))
				END -- End Case
		END

		-- Save information and create a shell
		-- -- Create Primary Key
		SET @BlockID = @ReceiverLineID + ':' + @SubscriberNumber
		PRINT '@BlockID: ' + @BlockID
		INSERT INTO dbo.MS_ReceiverLineBlocks (ReceiverLineBlockID, ReceiverLineId, SubscriberNumber, AccountId, IsAssigned, AssignedDate) VALUES
			(@BlockID, @ReceiverLineID, @SubscriberNumber, @AccountID, 1, GetDate())
		DECLARE @IndustryAccountID BIGINT
		INSERT INTO dbo.MS_IndustryAccounts (AccountId, ReceiverLineId, ReceiverLineBlockId, SubscriberId, CSID) VALUES 
			(@AccountID, @ReceiverLineID, @BlockID, @SubscriberNumber, LTRIM(RTRIM(@Designator)) + LTRIM(RTRIM(@SubscriberNumber)))
		SET @IndustryAccountID = SCOPE_IDENTITY();
		
		UPDATE @tblResults SET 
			IndustryAccountID = @IndustryAccountID,
			BlockID = @BlockID,
			CSID = LTRIM(RTRIM(@Designator)) + LTRIM(RTRIM(@SubscriberNumber))

	/******* TRANSACTION ENDS *********/
		COMMIT TRAN
	/******* TRANSACTION ENDS *********/

		-- Show Result 
		SELECT * FROM @tblResults

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
		RETURN	
	END CATCH	
END
GO

GRANT EXEC ON dbo.custMS_ReceiverLineBlockAccountGetNewByAccountID TO PUBLIC
GO

/** Unit test 
EXEC dbo.custMS_ReceiverLineBlockAccountGetNewByAccountID 100194, 'SosaWISE', 'AG_GPSTRACK:AB';

SELECT * FROM MS_Accounts WHERE AccountID = 100194;
*/