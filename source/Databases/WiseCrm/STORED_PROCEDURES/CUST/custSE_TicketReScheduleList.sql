USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSE_TicketReScheduleList')
	BEGIN
		PRINT 'Dropping Procedure custSE_TicketReScheduleList'
		DROP  Procedure  dbo.custSE_TicketReScheduleList
	END
GO

PRINT 'Creating Procedure custSE_TicketReScheduleList'
GO
/******************************************************************************
**		File: custSE_TicketReScheduleList.sql
**		Name: custSE_TicketReScheduleList
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
**	10/22/2014	Peter Fry	Updated to show tickets that are NOT Scheduled or past their schedule block end time.
**	
*******************************************************************************/
CREATE Procedure dbo.custSE_TicketReScheduleList
(
	@HoursPassed INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** DECLARATIONS */
	DECLARE @CurrentUTCDate DATETIME
	SET @CurrentUTCDate = GETUTCDATE()

	BEGIN TRY
		BEGIN TRANSACTION
	

			SELECT 
			* 
			FROM 
			[dbo].[vwSE_ScheduleBlockTickets] SSBT
			WHERE
			SSBT.IsDeleted = 0 AND
			SSBT.StatusCodeId <> 5 AND
			(SSBT.StartTime IS NULL OR SSBT.EndTime <= @CurrentUTCDate)
			

			/* SELECT 
				VWSET.* 
			 FROM 
			 [dbo].[vwSE_Tickets] VWSET
			 INNER JOIN [dbo].[SE_ScheduleTickets] SEST
			 ON
			 VWSET.TicketID = SEST.TicketId AND SEST.IsDeleted = 0

			 WHERE
			 DATEADD(HOUR,@HoursPassed,SEST)) 
			 */


/*
			 SELECT * FROM  [dbo].[SE_ScheduleTickets]
*/
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	
END
GO

GRANT EXEC ON dbo.custSE_TicketReScheduleList TO PUBLIC
GO

/** Testing
EXEC dbo.custSE_TicketReScheduleList 4  */

