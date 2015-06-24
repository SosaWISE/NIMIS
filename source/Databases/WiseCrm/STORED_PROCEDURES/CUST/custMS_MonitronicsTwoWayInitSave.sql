USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsTwoWayInitSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsTwoWayInitSave'
		DROP  Procedure  dbo.custMS_MonitronicsTwoWayInitSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsTwoWayInitSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsTwoWayInitSave.sql
**		Name: custMS_MonitronicsTwoWayInitSave
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
**		Date: 06/23/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/23/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsTwoWayInitSave
(
	@IndustryAccountID BIGINT = NULL
	, @TwoWayTestStartedOn DATETIME
	, @ConfirmedOn DATETIME
	, @ConfirmedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

	IF (EXISTS(SELECT * FROM dbo.MS_MonitronicsTwoWayInit WHERE (IndustryAccountID = @IndustryAccountID)))
	BEGIN
		UPDATE [dbo].[MS_MonitronicsTwoWayInit] SET
			[TwoWayTestStartedOn] = @TwoWayTestStartedOn
			,[ConfirmedOn] = @ConfirmedOn
			,[ConfirmedBy] = @ConfirmedBy
		 WHERE 
			([IndustryAccountID] = @IndustryAccountID);
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[MS_MonitronicsTwoWayInit] (
			[IndustryAccountID]
			,[TwoWayTestStartedOn]
			,[ConfirmedOn]
			,[ConfirmedBy]
		) VALUES (
			@IndustryAccountID
			, @TwoWayTestStartedOn
			, @ConfirmedOn
			, @ConfirmedBy
		);
	END

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	SELECT * FROM [dbo].[MS_MonitronicsTwoWayInit] WHERE (IndustryAccountID = @IndustryAccountID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsTwoWayInitSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsTwoWayInitSave */