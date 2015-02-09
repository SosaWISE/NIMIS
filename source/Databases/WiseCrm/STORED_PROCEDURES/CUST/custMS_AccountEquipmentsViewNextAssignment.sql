USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountEquipmentsViewNextAssignment')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountEquipmentsViewNextAssignment'
		DROP  Procedure  dbo.custMS_AccountEquipmentsViewNextAssignment
	END
GO

PRINT 'Creating Procedure custMS_AccountEquipmentsViewNextAssignment'
GO
/******************************************************************************
**		File: custMS_AccountEquipmentsViewNextAssignment.sql
**		Name: custMS_AccountEquipmentsViewNextAssignment
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
**		Date: 02/26/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/26/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountEquipmentsViewNextAssignment
(
	@AccountId BIGINT
	, @ItemSKU VARCHAR(20)
	, @GpEmployeeId VARCHAR(50)
	, @CrmUserId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @ItemID VARCHAR(50)
		, @AccountEquipmentID BIGINT
		, @AccountZoneAssignmentID BIGINT
		, @NextZone VARCHAR(3);

	/** Initialize */
	SELECT @ItemID = ItemID FROM [dbo].[AE_Items] WITH (NOLOCK) WHERE ItemSKU = @ItemSKU;
	IF (@ItemID IS NULL) RETURN;
	
	BEGIN TRY
		BEGIN TRANSACTION;

		/** Insert equipment */
		INSERT INTO dbo.MS_AccountEquipment (
			AccountId 
			, ItemId 
			, EquipmentLocationId 
			, GPEmployeeId 
			, OfficeReconciliationItemId 
			, AccountEquipmentUpgradeTypeId 
			, Points 
			, ActualPoints 
			, Price 
			, IsExisting 
			, IsServiceUpgrade 
			, IsExistingWiring 
			, IsMainPanel 
			, ModifiedBy 
			, CreatedBy 
		)
		SELECT
			@AccountId AS AccountId -- AccountId - bigint
			, ITM.ItemID
			, NULL -- EquipmentLocationId - int
			, @GpEmployeeId -- GPEmployeeId - varchar(50)
			, NULL -- OfficeReconciliationItemId - int
			, 'CUST' -- AccountEquipmentUpgradeTypeId - varchar(10)
			, ITM.SystemPoints AS Points
			, ITM.SystemPoints AS ActualPoints
			, ITM.Price
			, 0 -- IsExisting - bit
			, 0 -- IsServiceUpgrade - bit
			, 0 -- IsExistingWiring - bit
			, 0 -- IsMainPanel - bit
			, @CrmUserId -- ModifiedBy - nvarchar(50)
			, @CrmUserId -- CreatedBy - nvarchar(50)
		FROM
			[dbo].[AE_Items] AS ITM WITH (NOLOCK)
		WHERE
			(ITM.ItemSKU = @ItemSKU);
		-- Get IDENTITY
		SET @AccountEquipmentID = SCOPE_IDENTITY();

		/** Insert INTO MS_AccountZoneAssignments */
		SELECT @NextZone = [dbo].fxGetMsAccountLastAvailableZone(@AccountId);
		INSERT INTO [dbo].[MS_AccountZoneAssignments] (
			[AccountEquipmentId]
			, [Zone]
			, [ModifiedBy]
			, [CreatedBy]
		) VALUES (
			@AccountEquipmentID -- AccountEquipmentId - bigint
			, @NextZone-- Zone - varchar(3)
			, @CrmUserId -- ModifiedBy - nvarchar(50)
			, @CrmUserId -- CreatedBy - nvarchar(50)
		);
		-- Get Identity
		SET @AccountZoneAssignmentID = SCOPE_IDENTITY();
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result. */
	SELECT *
	FROM
		[dbo].[vwMS_AccountEquipments] AS AEQ WITH (NOLOCK)
	WHERE
		(AEQ.AccountZoneAssignmentID = @AccountZoneAssignmentID);
	
END
GO

GRANT EXEC ON dbo.custMS_AccountEquipmentsViewNextAssignment TO PUBLIC
GO

/** EXEC dbo.custMS_AccountEquipmentsViewNextAssignment 130532, 'asfasdfasd', NULL, 'MSTR001'; */