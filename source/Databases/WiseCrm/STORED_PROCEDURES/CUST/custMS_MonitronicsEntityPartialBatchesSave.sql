USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityPartialBatchesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityPartialBatchesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityPartialBatchesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityPartialBatchesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityPartialBatchesSave.sql
**		Name: custMS_MonitronicsEntityPartialBatchesSave
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
**		Auth: Jake Jenne
**		Date: 12/1/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/1/2014	Jake Jenne		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityPartialBatchesSave
(
	@WsiBatchNoID INT
	, @CustServNo VARCHAR(50)
	, @SiteName VARCHAR(50)
	, @ServcoNo INT
	, @MmChangeDate DATETIME
	, @ModifiedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			-- Check if exists
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityPartialBatches] WHERE (WsiBatchNoID = @WsiBatchNoID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityPartialBatches] SET
					CustServNo = @CustServNo
					, SiteName = @SiteName
					, ServcoNo = @ServcoNo
					, MmChangeDate = @MmChangeDate
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(WsiBatchNoID = @WsiBatchNoID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityPartialBatches] (
					WsiBatchNoID
					, CustServNo
					, SiteName
					, ServcoNo
					, MmChangeDate
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@WsiBatchNoID
					, @CustServNo
					, @SiteName
					, @ServcoNo
					, @MmChangeDate
					, 1
					, 0
					, @ModifiedBy
					, GETUTCDATE()
					, @ModifiedBy
					, GETUTCDATE()
				); 
			END
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- Return row
	SELECT * FROM [dbo].[MS_MonitronicsEntityPartialBatches] WHERE (WsiBatchNoID = @WsiBatchNoID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityPartialBatchesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityPartialBatchesSave */