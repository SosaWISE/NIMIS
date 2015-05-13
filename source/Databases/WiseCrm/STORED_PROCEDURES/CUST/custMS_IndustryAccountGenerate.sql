USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_IndustryAccountGenerate')
	BEGIN
		PRINT 'Dropping Procedure custMS_IndustryAccountGenerate'
		DROP  Procedure  dbo.custMS_IndustryAccountGenerate
	END
GO

PRINT 'Creating Procedure custMS_IndustryAccountGenerate'
GO
/******************************************************************************
**		File: custMS_IndustryAccountGenerate.sql
**		Name: custMS_IndustryAccountGenerate
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
**		Date: 01/13/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/13/2014	Andres Sosa		Created By
**	05/07/2015	Andres Sosa		Added mechanism for Moni Takeovers.
**
*******************************************************************************/
CREATE Procedure dbo.custMS_IndustryAccountGenerate
(
	@AccountId BIGINT
	, @IsPrimary BIT
	, @AccountType VARCHAR(20)
	, @MonitoringStationOSId VARCHAR(50)
	, @GpEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @ReceiverLineId VARCHAR(30)
	, @ReceiverLineBlockId VARCHAR(50)
	, @LastSubscriber INT
	, @StartNum INT, @EndNum INT
	, @Designator VARCHAR(6)
	, @Subscriber VARCHAR(6)
	, @CSID VARCHAR(12)
	, @IndustryAccountID BIGINT
	, @SubscriberLength SMALLINT
	, @DealerId INT
	, @AlarmCompanyID INT;

	/** Initialize */
	-- SET @ReceiverLineId = 'AG_ALARMSYS:I3';
	SET @ReceiverLineId = 'MI_MASTER:76826:DC2:AN';  -- Monitronics DIGCELLTWOWAY

	/** Get the AlarmCompanyID */
	SELECT TOP 1 
		@AlarmCompanyID = MLT.AlarmCompanyId
	FROM 
		dbo.fxGetLeadTakeOverByAccountId(@AccountID) AS MLT;

	/** FOR DEBUGGING PURPUSES ONLY. */
	-- SELECT * FROM [dbo].[vwMS_IndustryAccountNumbers] WHERE (IndustryAccountID = 30223);
	
	/** Assign the correct Receiver Line */
	IF (@AlarmCompanyID IS NOT NULL AND @AlarmCompanyID = 1) -- 1: Monitronics Take Over Account
	BEGIN
		SET @ReceiverLineId = 'AG_ALARMSYS:I3';
	END

	BEGIN TRY
		BEGIN TRANSACTION;
	
			/** Get Max and Min of Receiver Line and subscriber length. */
			SELECT 
				@StartNum = RL.StartNumber
				, @EndNum = RL.EndNumber
				, @Designator = RL.Designator
				, @SubscriberLength = RL.SubscriberLength
			FROM
				[dbo].MS_ReceiverLines AS RL WITH (NOLOCK)
			WHERE
				(RL.ReceiverLineID = @ReceiverLineId);
			
			/** Find Last Subscriber. */
			SELECT 
				@LastSubscriber = ISNULL(CAST(MAX(RLB.SubscriberNumber) AS INT) + 1, @StartNum)
			FROM 
				[dbo].[MS_ReceiverLineBlocks] AS RLB WITH (NOLOCK)
			WHERE 
				(RLB.ReceiverLineId = @ReceiverLineId);

			/** Check that we are in range. */
			IF NOT EXISTS(SELECT * FROM [dbo].MS_ReceiverLines AS RL WITH (NOLOCK) WHERE (@LastSubscriber BETWEEN RL.StartNumber AND RL.EndNumber))
			BEGIN
				RAISERROR (N'Sorry but this receiver line ''%s'' is full with Maximum number of ''%d''.  ', 18, 1, @ReceiverLineId, @EndNum);
			END

			/** Increment subscriber */
			SELECT @Subscriber = [dbo].fxSubscriberNumberWithPadding(CAST(@LastSubscriber AS VARCHAR(10)), @SubscriberLength, '0');
			SET @CSID = @Designator + @Subscriber;
			SET @ReceiverLineBlockId = @ReceiverLineId + ':' + @Designator + ':' + @Subscriber;

			/** Create Receiver Block row. */
			INSERT INTO [dbo].[MS_ReceiverLineBlocks] (
				[ReceiverLineBlockID]
				, [ReceiverLineId]
				, [CSID]
				, [SubscriberNumber]
				, [AccountId]
				, [IsAssigned]
				, [AssignedDate]
				, [IsActive]
				, [IsDeleted]
				, [ModifiedBy]
				, [CreatedBy]
			) VALUES (
				@ReceiverLineBlockId
				, @ReceiverLineId
				, @CSID
				, @Subscriber
				, @AccountId
				, 1
				, GETUTCDATE()
				, 1
				, 0
				, @GpEmployeeId
				, @GpEmployeeId
			);

			INSERT INTO [dbo].[MS_IndustryAccounts] (
				[AccountId]
				, [ReceiverLineId]
				, [ReceiverLineBlockId]
				, [SubscriberId]
				, [Csid]
				, [InTestMode]
				, [IsMove]
				, [IsTakeover]
				, [IsActive]
				, [IsDeleted]
				, [ModifiedBy]
				, [CreatedBy]
			) VALUES (
				@AccountId
				, @ReceiverLineId
				, @ReceiverLineBlockId
				, @Subscriber
				, @CSID
				, 0
				, 0
				, 0
				, 1
				, 0
				, @GpEmployeeId
				, @GpEmployeeId
			);

			SET @IndustryAccountID = SCOPE_IDENTITY();

			/** Pick if primary or secondary. */
			IF (@IsPrimary = 1)
			BEGIN
				UPDATE [dbo].MS_Accounts SET IndustryAccountId = @IndustryAccountID WHERE AccountID = @AccountId;
			END
			ELSE
			BEGIN
				UPDATE [dbo].MS_Accounts SET IndustryAccount2Id = @IndustryAccountID WHERE AccountID = @AccountId;
			END
		COMMIT TRANSACTION;

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
	
	/** Return result */
	SELECT * FROM [dbo].[vwMS_IndustryAccountNumbers] WHERE (IndustryAccountID = @IndustryAccountID);
END
GO

GRANT EXEC ON dbo.custMS_IndustryAccountGenerate TO PUBLIC
GO

/** EXEC dbo.custMS_IndustryAccountGenerate 211217, 0, 'CELLDIGTWOTWAY', 'AG_ALARMSYS', 'PRIVIT'; */