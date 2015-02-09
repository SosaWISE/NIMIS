USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSE_ScheduleTicketTechUpdate')
	BEGIN
		PRINT 'Dropping Procedure custSE_ScheduleTicketTechUpdate'
		DROP  Procedure  dbo.custSE_ScheduleTicketTechUpdate
	END
GO

PRINT 'Creating Procedure custSE_ScheduleTicketTechUpdate'
GO
/******************************************************************************
**		File: custSE_ScheduleTicketTechUpdate.sql
**		Name: custSE_ScheduleTicketTechUpdate
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
CREATE Procedure dbo.custSE_ScheduleTicketTechUpdate
(
	 @BlockId BIGINT
	, @TechnicianId VARCHAR(30)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON


	BEGIN TRY
		BEGIN TRANSACTION
			
			-- TECH ID on SE_Tickets
			UPDATE [dbo].[SE_Tickets]
			SET
			[TechnicianId] = @TechnicianId
			WHERE
			TicketID IN (SELECT SEST.TicketId FROM [dbo].[SE_ScheduleTickets] SEST WHERE (SEST.BlockId = @BlockId AND SEST.IsDeleted = 0))

			
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	
END
GO

GRANT EXEC ON dbo.custSE_ScheduleTicketTechUpdate TO PUBLIC
GO

/** Testing
EXEC dbo.custSE_ScheduleTicketTechUpdate  */

