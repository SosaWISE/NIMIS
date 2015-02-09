USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custIE_PackingSlipItemCreate')
	BEGIN
		PRINT 'Dropping Procedure custIE_PackingSlipItemCreate'
		DROP  Procedure  dbo.custIE_PackingSlipItemCreate
	END
GO

PRINT 'Creating Procedure custIE_PackingSlipItemCreate'
GO
/******************************************************************************
**		File: custIE_PackingSlipItemCreate.sql
**		Name: custIE_PackingSlipItemCreate
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
**	06/16/2014	Reagan		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custIE_PackingSlipItemCreate
(
	 @PackingSlipId INT
	,@ProductSkwId VARCHAR(50)
	,@ItemId VARCHAR(50)
	,@Quantity INT
	,@GPEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** DECLARATIONS */
	DECLARE @PackingSlipItemID INT

	BEGIN TRY
		BEGIN TRANSACTION
			/** Get PurchaseOrderId information */
			IF (NOT EXISTS(SELECT * FROM [dbo].[IE_PackingSlipItems] IEPSI  WHERE (IEPSI.PackingSlipId = @PackingSlipId AND IEPSI.ItemId=@ItemId)))
			BEGIN
				/** Create PackingSli */
				INSERT [dbo].[IE_PackingSlipItems](
					[PackingSlipId],
					[ProductSkwId],
					[ItemId],
					[Quantity],
					[ModifiedBy],
					CreatedBy
				) VALUES (
					@PackingSlipId, 
					@ProductSkwId,
					@ItemId,
					@Quantity,
					@GPEmployeeId,
					@GPEmployeeId
				);

				/** Get Identity */
				SET @PackingSlipItemID = SCOPE_IDENTITY();

				/** Return result. */
				SELECT * FROM [dbo].[IE_PackingSlipItems] WHERE (PackingSlipItemID = @PackingSlipItemID);
			END
			ELSE
			BEGIN
				/** Return result. */
				SELECT * FROM [dbo].[IE_PackingSlipItems] IEPSI  WHERE (IEPSI.PackingSlipId = @PackingSlipId AND IEPSI.ItemId=@ItemId)
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

GRANT EXEC ON dbo.custIE_PackingSlipItemCreate TO PUBLIC
GO

/** Testing
EXEC dbo.custIE_PackingSlipItemCreate  1,1 */