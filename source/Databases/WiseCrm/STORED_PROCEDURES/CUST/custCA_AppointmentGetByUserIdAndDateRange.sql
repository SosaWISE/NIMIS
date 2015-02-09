USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custCA_AppointmentGetByUserIdAndDateRange')
	BEGIN
		PRINT 'Dropping Procedure custCA_AppointmentGetByUserIdAndDateRange'
		DROP  Procedure  dbo.custCA_AppointmentGetByUserIdAndDateRange
	END
GO

PRINT 'Creating Procedure custCA_AppointmentGetByUserIdAndDateRange'
GO
/******************************************************************************
**		File: custCA_AppointmentGetByUserIdAndDateRange.sql
**		Name: custCA_AppointmentGetByUserIdAndDateRange
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
**		Date: 06/02/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	----------	---------------	-------------------------------------------
**	06/02/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custCA_AppointmentGetByUserIdAndDateRange
(
	@DealerUserId INT
	, @StartDate DATETIME
	, @EndDate DATETIME
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	BEGIN TRY
		BEGIN TRANSACTION
		/** Execute Selelct */
		SELECT
			APT.*
		FROM
			dbo.CA_Appointments AS APT WITH (NOLOCK)
		WHERE
			(APT.DealerUserId = @DealerUserId)
			--AND (APT.StartDateTime BETWEEN @StartDate AND @EndDate)
			--AND (APT.EndDateTime BETWEEN @StartDate AND @EndDate)
			AND ((APT.StartDateTime BETWEEN @StartDate AND @EndDate)
				OR (APT.EndDateTime BETWEEN @EndDate AND @StartDate))
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
		RETURN
	END CATCH
	
	PRINT '|*** END   SP: dbo.custCA_AppointmentGetByUserIdAndDateRange'

END
GO

GRANT EXEC ON dbo.custCA_AppointmentGetByUserIdAndDateRange TO PUBLIC
GO

EXEC dbo.custCA_AppointmentGetByUserIdAndDateRange @DealerUserId = 1, -- int
    @StartDate = '2012-07-01 05:44:43', -- datetime
    @EndDate = '2012-08-05 05:44:43' -- datetime
