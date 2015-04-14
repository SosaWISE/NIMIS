USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountHoldsCreate')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountHoldsCreate'
		DROP  Procedure  dbo.custMS_AccountHoldsCreate
	END
GO

PRINT 'Creating Procedure custMS_AccountHoldsCreate'
GO
/******************************************************************************
**		File: custMS_AccountHoldsCreate.sql
**		Name: custMS_AccountHoldsCreate
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
**		Date: 04/14/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/14/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountHoldsCreate
(
	@AccountId BIGINT = NULL
	, @Catg2Id INT
	, @HoldDescription VARCHAR(4000)
	, @CreatedBy VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @AccountHoldID BIGINT;

	/** INITIALIZATION */
	IF (@HoldDescription IS NULL)
	BEGIN
		SELECT @HoldDescription = CatgDescription FROM [dbo].[MS_AccountHoldCatg2] WHERE (Catg2ID = @Catg2Id);
	END
	
	BEGIN TRY
		BEGIN TRANSACTION
		INSERT INTO [dbo].[MS_AccountHolds] (
			[AccountId]
			, [Catg2Id]
			, [HoldDescription]
			, [CreatedBy]
			, [ModifiedBy]
		) VALUES (
			@AccountID -- AccountId - bigint
			, @Catg2Id -- Catg2Id - int
			, @HoldDescription -- HoldDescription - nvarchar(4000)
			, @CreatedBy -- CreatedBy - nvarchar(50)
			, @CreatedBy -- ModifiedBy - nvarchar(50)
		);

		SET @AccountHoldID = SCOPE_IDENTITY();

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result */
	SELECT * FROM [dbo].[MS_AccountHolds] WHERE (AccountHoldID = @AccountHoldID);

END
GO

GRANT EXEC ON dbo.custMS_AccountHoldsCreate TO PUBLIC
GO

/** EXEC dbo.custMS_AccountHoldsCreate */