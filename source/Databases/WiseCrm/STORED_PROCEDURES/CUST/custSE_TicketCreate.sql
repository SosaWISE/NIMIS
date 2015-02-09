USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSE_TicketCreate')
	BEGIN
		PRINT 'Dropping Procedure custSE_TicketCreate'
		DROP  Procedure  dbo.custSE_TicketCreate
	END
GO

PRINT 'Creating Procedure custSE_TicketCreate'
GO
/******************************************************************************
**		File: custSE_TicketCreate.sql
**		Name: custSE_TicketCreate
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
**	07/18/2014	Reagan		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custSE_TicketCreate
(
	@AccountId BIGINT
	, @MonitoringStationNo BIGINT
	, @TicketTypeId INT
	, @StatusCodeId INT
	, @MoniConfirmation NVARCHAR(50)
	, @TechnicianId VARCHAR(30)
	, @TripCharges MONEY
	, @Appointment NVARCHAR(50)
	, @AgentConfirmation NVARCHAR(50)
	, @ExpirationDate DATETIME
	, @Notes NVARCHAR(1000)

)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** DECLARATIONS */
	DECLARE @TicketID BIGINT

	BEGIN TRY
		BEGIN TRANSACTION
			INSERT [dbo].[SE_Tickets](
				  [AccountId]
				, [MonitoringStationNo]
				, [TicketTypeId]
				, [StatusCodeId]
				, [MoniConfirmation]
				, [TechnicianId]
				, [TripCharges]
				, [Appointment]
				, [AgentConfirmation]
				, [ExpirationDate]
				, [Notes]

			) VALUES (
				  @AccountId 
				, @MonitoringStationNo 
				, @TicketTypeId 
				, @StatusCodeId 
				, @MoniConfirmation 
				, @TechnicianId 
				, @TripCharges 
				, @Appointment
				, @AgentConfirmation 
				, @ExpirationDate
				, @Notes 
			);

			/** Get Identity */
			SET @TicketID = SCOPE_IDENTITY();
			/** Return result. */
			--SELECT * FROM [dbo].[SE_Tickets] WHERE (TicketID = @TicketID);
			 SELECT * FROM [dbo].[vwSE_Tickets] WHERE (TicketID = @TicketID);
			-- SELECT * FROM [dbo].[vwSE_AccountTickets] WHERE (TicketID = @TicketID);
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	
END
GO

GRANT EXEC ON dbo.custSE_TicketCreate TO PUBLIC
GO

/** Testing
EXEC dbo.custSE_TicketCreate  */

