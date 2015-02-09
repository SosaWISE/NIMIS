USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSE_ScheduleTicketCreate')
	BEGIN
		PRINT 'Dropping Procedure custSE_ScheduleTicketCreate'
		DROP  Procedure  dbo.custSE_ScheduleTicketCreate
	END
GO

PRINT 'Creating Procedure custSE_ScheduleTicketCreate'
GO
/******************************************************************************
**		File: custSE_ScheduleTicketCreate.sql
**		Name: custSE_ScheduleTicketCreate
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
**	07/22/2014	Reagan		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custSE_ScheduleTicketCreate
(
	 @TicketId BIGINT
	, @BlockId BIGINT
	, @AppointmentDate DATETIME
	, @TravelTime INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** DECLARATIONS */
	DECLARE @ScheduleTicketID BIGINT

	BEGIN TRY
		BEGIN TRANSACTION
			INSERT [dbo].[SE_ScheduleTickets](
				  [TicketId]
				, [BlockId]
				, [AppointmentDate]
				, [TravelTime]
				
			) VALUES (
				  @TicketId 
				, @BlockId
				, @AppointmentDate
				, @TravelTime
			);

			-- TECH ID on SE_Tickets
			UPDATE [dbo].[SE_Tickets]
			SET
			[TechnicianId] = (SELECT SESB.TechnicianId FROM [dbo].[SE_ScheduleBlocks] SESB WHERE (SESB.[BlockID] = @BlockId) )
			WHERE
			(TicketID = @TicketId)

			-- set the currentticketid as 
			UPDATE  [dbo].[SE_ScheduleBlocks]
			SET
			[CurrentTicketId] = @TicketId
			WHERE
			([BlockID]=@BlockId)
			
			
			/** Get Identity */
			SET @ScheduleTicketID = SCOPE_IDENTITY();

			/** Return result. */
			SELECT * FROM [dbo].[SE_ScheduleTickets] WHERE (ScheduleTicketID = @ScheduleTicketID);
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	
END
GO

GRANT EXEC ON dbo.custSE_ScheduleTicketCreate TO PUBLIC
GO

/** Testing
EXEC dbo.custSE_ScheduleTicketCreate  */

