USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLP_CommandMessageEAVRSP4ProcessByUnitID')
	BEGIN
		PRINT 'Dropping Procedure custLP_CommandMessageEAVRSP4ProcessByUnitID'
		DROP  Procedure  dbo.custLP_CommandMessageEAVRSP4ProcessByUnitID
	END
GO

PRINT 'Creating Procedure custLP_CommandMessageEAVRSP4ProcessByUnitID'
GO
/******************************************************************************
**		File: custLP_CommandMessageEAVRSP4ProcessByUnitID.sql
**		Name: custLP_CommandMessageEAVRSP4ProcessByUnitID
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
**		Date: 12/03/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	----------	------------	-----------------------------------------------
**	12/03/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custLP_CommandMessageEAVRSP4ProcessByUnitID
(
	@UnitID BIGINT
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
		dbo.vwLP_CommandMessageEAVRSP4s AS CMD
	WHERE
		(CMD.UnitID = @UnitID)
		AND (CMD.Processed IS NULL)
		AND (CMD.DEX_ROW_TS > @DateRange)))
	BEGIN

		BEGIN TRY
			BEGIN TRANSACTION
			/** Flag tuples as processed */
			UPDATE [dbo].LP_CommandMessageEAVRSP4s SET 
				Processed = @ProcessDate
			WHERE
				(UnitID = @UnitID)
				AND (Processed IS NULL)
				AND (DEX_ROW_TS > @DateRange);
				
			/** Create or Update a GS_AccountGeoFenceRectangle. */
			COMMIT TRANSACTION
		END TRY
		
		BEGIN CATCH
			ROLLBACK TRANSACTION
			EXEC dbo.wiseSP_ExceptionsThrown
		END CATCH
		
		/** Return result */
		SELECT * FROM [dbo].vwLP_CommandMessageEAVRSP4s WHERE (UnitID = @UnitID) AND (Processed = @ProcessDate) AND (DEX_ROW_TS > @DateRange);
	END 
END
GO

GRANT EXEC ON dbo.custLP_CommandMessageEAVRSP4ProcessByUnitID TO PUBLIC
GO

/** 
SELECT * FROM dbo.vwLP_CommandMessageEAVRSP4s AS CMD
	WHERE
		(CMD.UnitID = 80003902)
		AND (CMD.Processed IS NULL)
		AND (CMD.DEX_ROW_TS > @DateRange)))
*/