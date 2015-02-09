USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSE_ScheduleBlockCreate')
	BEGIN
		PRINT 'Dropping Procedure custSE_ScheduleBlockCreate'
		DROP  Procedure  dbo.custSE_ScheduleBlockCreate
	END
GO

PRINT 'Creating Procedure custSE_ScheduleBlockCreate'
GO
/******************************************************************************
**		File: custSE_ScheduleBlockCreate.sql
**		Name: custSE_ScheduleBlockCreate
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
**	07/25/2014	Reagan		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custSE_ScheduleBlockCreate
(
	@Block VARCHAR(15)
    , @ZipCode VARCHAR(20)
    , @MaxRadius FLOAT
    , @Distance FLOAT
    , @StartTime DATETIME
    , @EndTime DATETIME
    , @AvailableSlots INT
    , @TechnicianId VARCHAR(20)
	, @IsTechConfirmed BIT
	, @Color VARCHAR(50)
	, @IsBlocked BIT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** DECLARATIONS */
	DECLARE @BlockID BIGINT

	BEGIN TRY
		BEGIN TRANSACTION
			INSERT [dbo].[SE_ScheduleBlocks](
				  [Block]
				  ,[ZipCode]
				  ,[MaxRadius]
				  ,[Distance]
				  ,[StartTime]
				  ,[EndTime]
				  ,[AvailableSlots]
				  ,[TechnicianId]
				  ,[IsTechConfirmed]
				  ,[Color]
				  ,[IsBlocked]

			) VALUES (
				@Block
				, @ZipCode 
				, @MaxRadius 
				, @Distance
				--, @StartTime 
				--, @EndTime 
				, dbo.fxAddTimeZoneToDate(@StartTime,@ZipCode)  
				, dbo.fxAddTimeZoneToDate(@EndTime,@ZipCode)
				, @AvailableSlots 
				, @TechnicianId
				, @IsTechConfirmed
				, @Color
				, @IsBlocked 
			);

			/** Get Identity */
			SET @BlockID = SCOPE_IDENTITY();

			/** Return result. */
			SELECT * FROM [dbo].[SE_ScheduleBlocks] WHERE (BlockID = @BlockID);
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	
END
GO

GRANT EXEC ON dbo.custSE_ScheduleBlockCreate TO PUBLIC
GO

/** Testing
EXEC dbo.custSE_ScheduleBlockCreate  */

