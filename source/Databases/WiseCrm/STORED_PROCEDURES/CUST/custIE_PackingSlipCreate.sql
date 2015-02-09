USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custIE_PackingSlipCreate')
	BEGIN
		PRINT 'Dropping Procedure custIE_PackingSlipCreate'
		DROP  Procedure  dbo.custIE_PackingSlipCreate
	END
GO

PRINT 'Creating Procedure custIE_PackingSlipCreate'
GO
/******************************************************************************
**		File: custIE_PackingSlipCreate.sql
**		Name: custIE_PackingSlipCreate
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
**	06/13/2014	Reagan		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custIE_PackingSlipCreate
(
	 @PackingSlipNumber VARCHAR(25)
	, @PurchaseOrderId BIGINT
	, @GPEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** DECLARATIONS */
	DECLARE @PackingSlipID INT, @CreateNewPS BIT

	BEGIN TRY
		BEGIN TRANSACTION
			/** Get PurchaseOrderId information */

			-- check if there is an existing null for @PackingSlipNumber
			IF @PackingSlipNumber IS NULL
			BEGIN
				SET @PackingSlipID = ISNULL((SELECT PackingSlipID FROM [dbo].[IE_PackingSlips] WHERE (PurchaseOrderId = @PurchaseOrderId AND PackingSlipNumber IS NULL)),0)
				IF @PackingSlipID <> 0
				BEGIN
					SELECT * FROM [dbo].[IE_PackingSlips] WHERE (PackingSlipID = @PackingSlipID);
				END
				ELSE
				BEGIN
					SET @PackingSlipID = 0
				END
			
			END
			ELSE
			BEGIN
				SET @PackingSlipID = ISNULL((SELECT PackingSlipID FROM [dbo].[IE_PackingSlips] WHERE (PurchaseOrderId = @PurchaseOrderId AND PackingSlipNumber  = @PackingSlipNumber)),0)
				IF @PackingSlipID <> 0 
				BEGIN
					SELECT * FROM [dbo].[IE_PackingSlips] WHERE (PackingSlipID = @PackingSlipID);
				END
			END


		/** Create PackingSlip */
			IF @PackingSlipID = 0
			BEGIN

				INSERT [dbo].[IE_PackingSlips](
					[PurchaseOrderId],
					PackingSlipNumber,
					ModifiedBy,
					CreatedBy
				) VALUES (
					@PurchaseOrderId, 
					@PackingSlipNumber,
					@GPEmployeeId,
					@GPEmployeeId
				);

				/** Get Identity */
				SET @PackingSlipID = SCOPE_IDENTITY();

				/** Return result. */
				SELECT * FROM [dbo].[IE_PackingSlips] WHERE (PackingSlipID = @PackingSlipID);
			END



		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	
END
GO

GRANT EXEC ON dbo.custIE_PackingSlipCreate TO PUBLIC
GO

/** Testing
EXEC dbo.custIE_PackingSlipCreate  NULL,3301,'MSTR001' */

