USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_AgingStepByCMFID')
	BEGIN
		PRINT 'Dropping Procedure custAE_AgingStepByCMFID'
		DROP  Procedure  dbo.custAE_AgingStepByCMFID
	END
GO

PRINT 'Creating Procedure custAE_AgingStepByCMFID'
GO
/******************************************************************************
**		File: custAE_AgingStepByCMFID.sql
**		Name: custAE_AgingStepByCMFID
**		Desc: Returns the aging information for the CustomerMasterFileID passed
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
**		CustomerMasterFileID			Aging steps along with the Aging balance
**										for the specified CustomerMasterFileID
**		Auth: Andres Sosa
**		Date: 01/02/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/02/2014	Andres Sosa		Created By
**	08/04/2014	Bob McFaddeen	Modified query to pull aging for the specified
**								CustomerMasterFileID we pass as a parameter
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_AgingStepByCMFID
(
	@CMFID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */

--OLD QUERY - WAS NOT WORKING
	DECLARE @tblResult TABLE ([CustomerMasterFileID] BIGINT, [AgingStepID] VARCHAR(10), [AgingStep] VARCHAR(50), [ValueDue] MONEY, [StepOrder] SMALLINT);

	INSERT INTO @tblResult (CustomerMasterFileID, AgingStepID, AgingStep, ValueDue, StepOrder)
	SELECT 
		@CMFID AS CustomerMasterFileID
		, AGS.AgingStepID
		, AGS.AgingStep
		, 0 AS [ValueDue]
		, AGS.StepOrder
	FROM
		[dbo].AE_AgingSteps AS AGS WITH (NOLOCK)
	ORDER BY
		AGS.StepOrder;

	/** Update the table */
	UPDATE @tblResult SET
		RST.[ValueDue] = AGG.ValueDue
    FROM
		@tblResult AS RST
		INNER JOIN dbo.SAE_Aging AS AGG WITH (NOLOCK)
		ON
			(RST.AgingStepID = AGG.AgingStepId)
			AND (AGG.CustomerMasterFileID = @CMFID);
	/** Execute result */
	SELECT * FROM @tblResult;
END
GO

GRANT EXEC ON dbo.custAE_AgingStepByCMFID TO PUBLIC
GO

/** Testing
EXEC dbo.custAE_AgingStepByCMFID 3000023
*/