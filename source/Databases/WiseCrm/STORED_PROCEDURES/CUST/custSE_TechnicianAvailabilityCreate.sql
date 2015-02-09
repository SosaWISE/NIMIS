USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSE_TechnicianAvailabilityCreate')
	BEGIN
		PRINT 'Dropping Procedure custSE_TechnicianAvailabilityCreate'
		DROP  Procedure  dbo.custSE_TechnicianAvailabilityCreate
	END
GO

PRINT 'Creating Procedure custSE_TechnicianAvailabilityCreate'
GO
/******************************************************************************
**		File: custSE_TechnicianAvailabilityCreate.sql
**		Name: custSE_TechnicianAvailabilityCreate
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
**	08/26/2014	Reagan		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custSE_TechnicianAvailabilityCreate
(
	@TechnicianId VARCHAR(20)
	, @StartDateTime DATETIME
	, @EndDateTime DATETIME
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** DECLARATIONS */
	DECLARE @TechnicianAvailabilityID BIGINT

	BEGIN TRY
		BEGIN TRANSACTION
			INSERT [dbo].[SE_TechnicianAvailability](
				  [TechnicianId]
				, [StartDateTime]
				, [EndDateTime]

			) VALUES (
					@TechnicianId
					, @StartDateTime
					, @EndDateTime
			);

			/** Get Identity */
			SET @TechnicianAvailabilityID = SCOPE_IDENTITY();
			/** Return result. */
			SELECT * FROM [dbo].[SE_TechnicianAvailability] WHERE (TechnicianAvailabilityID = @TechnicianAvailabilityID);
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	
END
GO

GRANT EXEC ON dbo.custSE_TechnicianAvailabilityCreate TO PUBLIC
GO

/** Testing
EXEC dbo.custSE_TechnicianAvailabilityCreate  */

