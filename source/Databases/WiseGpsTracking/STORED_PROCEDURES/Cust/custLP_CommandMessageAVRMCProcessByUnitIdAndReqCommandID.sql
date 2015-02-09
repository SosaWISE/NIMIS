USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLP_CommandMessageAVRMCProcessByUnitIdAndReqCommandID')
	BEGIN
		PRINT 'Dropping Procedure custLP_CommandMessageAVRMCProcessByUnitIdAndReqCommandID'
		DROP  Procedure  dbo.custLP_CommandMessageAVRMCProcessByUnitIdAndReqCommandID
	END
GO

PRINT 'Creating Procedure custLP_CommandMessageAVRMCProcessByUnitIdAndReqCommandID'
GO
/******************************************************************************
**		File: custLP_CommandMessageAVRMCProcessByUnitIdAndReqCommandID.sql
**		Name: custLP_CommandMessageAVRMCProcessByUnitIdAndReqCommandID
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
CREATE Procedure dbo.custLP_CommandMessageAVRMCProcessByUnitIdAndReqCommandID
(
	@UnitID BIGINT
	, @ReqCommandMessageId BIGINT
	, @EventCodeId VARCHAR(3)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Initialize. */
	DECLARE @ProcessDate DATETIME = GETUTCDATE();
	DECLARE @DateRange DATETIME = DATEADD(minute,-2,@ProcessDate);

	/** Find Responses from Unit */	
	IF (EXISTS(SELECT 
		CMD.*
	FROM
		[dbo].[vwLP_CommandMessageAVRMCs] AS CMD
	WHERE
		(CMD.UnitID = @UnitID)
		AND (CMD.EventCodeId IN (@EventCodeId, '0'))
		AND (CMD.ProcessedDate IS NULL)
		AND (CMD.DEX_ROW_TS > @DateRange)))  -- Only look at responses that are 2 minutes old
	BEGIN

		BEGIN TRY
			BEGIN TRANSACTION
			/** Flag tuples as processed */
			UPDATE [dbo].LP_CommandMessageAVRMCs SET 
				ProcessedDate = @ProcessDate
				, ReqCommandMessageId = @ReqCommandMessageId
			FROM
				[dbo].LP_CommandMessageAVRMCs AS AVRM WITH (NOLOCK)
				INNER JOIN [dbo].LP_CommandMessages AS CMD WITH (NOLOCK)
				ON
					(AVRM.CommandMessageID = CMD.CommandMessageID)
			WHERE
				(CMD.UnitID = @UnitID)
				/** Added the '0' because the documentation says that on a location request it returns an 'A'
				 * however the devices are not doing that. */
				AND (AVRM.EventCodeId IN (@EventCodeId, '0'))
				AND (AVRM.ProcessedDate IS NULL)
				AND (AVRM.DEX_ROW_TS > @DateRange)  -- Only look at responses that are 2 minutes old
			COMMIT TRANSACTION
		END TRY
		
		BEGIN CATCH
			ROLLBACK TRANSACTION
			EXEC dbo.wiseSP_ExceptionsThrown
		END CATCH
		
		/** Return result */
		SELECT * FROM [dbo].[vwLP_CommandMessageAVRMCs] WHERE (UnitID = @UnitID) AND (ProcessedDate = @ProcessDate) AND (ReqCommandMessageId = @ReqCommandMessageId);
	END 
END
GO

GRANT EXEC ON dbo.custLP_CommandMessageAVRMCProcessByUnitIdAndReqCommandID TO PUBLIC
GO

/**
EXEC dbo.custLP_CommandMessageAVRMCProcessByUnitIdAndReqCommandID 80003902, 4941, 'A'
DECLARE @ProcessDate DATETIME = GETUTCDATE();
DECLARE @DateRange DATETIME = DATEADD(minute,-2,@ProcessDate);
SELECT 
		CMD.*
	FROM
		[dbo].[vwLP_CommandMessageAVRMCs] AS CMD
	WHERE
		(CMD.UnitID = 80003902)
		AND (CMD.EventCodeId IN ('A','0'))
		AND (CMD.ProcessedDate IS NULL)
		AND (CMD.DEX_ROW_TS > @DateRange)  -- Only look at responses that are 2 minutes old
*/