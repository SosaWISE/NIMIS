USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGEquipment')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGEquipment'
		DROP  Procedure  dbo.custAE_CustomerSWINGEquipment
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGEquipment'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGEquipment.sql
**		Name: custAE_CustomerSWINGEquipment
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
**		Date: 05/06/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	05/06/2014	Andres Sosa		Created By
**	05/26/2014	Junryl			Modified update query on MS_AccountInfoSwung and comment out select for SwingStatus
*******************************************************************************/
CREATE Procedure [dbo].[custAE_CustomerSWINGEquipment]
(
	@InterimAccountID BIGINT
	, @CustomerMasterFileID BIGINT
	, @MsAccountID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @SwingStatus VARCHAR(2);
	DECLARE @AccountEquipmentID BIGINT
		, @AccountInventoryId INT
		, @ItemId VARCHAR(50)
		, @EquipmentLocationId INT
		, @GPEmployeeId VARCHAR(50)
		, @OfficeReconciliationItemId INT
		, @AccountEquipmentUpgradeTypeId VARCHAR(10)
		, @Points INT
		, @ActualPoints FLOAT
		, @Price MONEY
		, @IsExisting BIT
		, @BarcodeId VARCHAR(25)
		, @IsServiceUpgrade BIT
		, @IsExistingWiring BIT
		, @IsMainPanel BIT;
	
	BEGIN TRY
		BEGIN TRANSACTION;

			DECLARE db_cursor CURSOR FOR 
			SELECT
				MAI.AccountInventoryID
				, [dbo].fxTranslateInterimEquipment(MAI.EquipmentID) AS ItemId
				, MAI.EquipmentLocationId AS EquipmentLocationId
				, @GpEmployeeId As GPEmployeeId
				, NULL AS OfficeReconciliationItemId
				, 'CUST' AS AccountEquipmentUpgradeTypeId
				, MAI.Points
				, MAI.ActualPoints
				, MAI.Price
				, 1 AS IsExisting
				, NULL AS BarcodeId
				, 0 AS IsServiceUpgrade
				, 1 AS IsExistingWiring
				, CASE
					WHEN EQ.EquipmentPanelTypeId IS NOT NULL THEN 1
					ELSE 0 
				  END As IsMainPanel
			FROM
				[Platinum_Protection_InterimCRM].[dbo].[MS_AccountInventory] AS MAI WITH (NOLOCK)
				INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MS_Equipment] AS EQ WITH (NOLOCK)
				ON
					(EQ.EquipmentID = MAI.EquipmentID)
			WHERE
				(MAI.AccountID = @InterimAccountID);

			-- ** Open and Fetch Next cursor
			OPEN db_cursor
			FETCH NEXT FROM db_cursor INTO 
				@AccountInventoryId
				, @ItemId
				, @EquipmentLocationId
				, @GPEmployeeId
				, @OfficeReconciliationItemId
				, @AccountEquipmentUpgradeTypeId
				, @Points
				, @ActualPoints
				, @Price
				, @IsExisting
				, @BarcodeId
				, @IsServiceUpgrade
				, @IsExistingWiring
				, @IsMainPanel;

			-- ** Loop cursor
			WHILE @@FETCH_STATUS = 0
			BEGIN
				-- ** Save Fetch into Account Equipment
				INSERT INTO [dbo].[MS_AccountEquipment] (
					[AccountId]
					,[ItemId]
					,[EquipmentLocationId]
					,[GPEmployeeId]
					,[OfficeReconciliationItemId]
					,[AccountEquipmentUpgradeTypeId]
					,[Points]
					,[ActualPoints]
					,[Price]
					,[IsExisting]
					,[BarcodeId]
					,[IsServiceUpgrade]
					,[IsExistingWiring]
					,[IsMainPanel]
					,[IsActive]
					,[IsDeleted]
				) VALUES (
					@MsAccountID
					, @ItemId
					, @EquipmentLocationId
					, @GPEmployeeId
					, @OfficeReconciliationItemId
					, @AccountEquipmentUpgradeTypeId
					, @Points
					, @ActualPoints
					, @Price
					, @IsExisting
					, @BarcodeId
					, @IsServiceUpgrade
					, @IsExistingWiring
					, @IsMainPanel
					, 1
					, 0
				);
				-- ** Get PK of MS_AccountEquipment
				SET @AccountEquipmentID = SCOPE_IDENTITY();

				-- ** Save Zone Assignments
				INSERT INTO [dbo].[MS_AccountZoneAssignments] (
					[AccountEquipmentId]
					,[AccountEventId]
					,[Zone]
					,[Comments]
					,[IsExisting]
				)
				SELECT
					@AccountEquipmentID AS [AccountEquipmentId]
					, AZA.AccountEventId
					, AZA.Zone
					, AZA.Comments
					, AZA.IsExisting
				FROM 
					[Platinum_Protection_InterimCRM].[dbo].[MS_AccountZoneAssignment] AS AZA WITH (NOLOCK)
				WHERE 
					(AZA.AccountInventoryID = @AccountInventoryID);

				-- ** Fetch Next
				FETCH NEXT FROM db_cursor INTO 
					@AccountInventoryId
					, @ItemId
					, @EquipmentLocationId
					, @GPEmployeeId
					, @OfficeReconciliationItemId
					, @AccountEquipmentUpgradeTypeId
					, @Points
					, @ActualPoints
					, @Price
					, @IsExisting
					, @BarcodeId
					, @IsServiceUpgrade
					, @IsExistingWiring
					, @IsMainPanel;
			END
			
			CLOSE db_cursor
			DEALLOCATE db_cursor

			-- ** Save information of successfull equipment swung
			--UPDATE [dbo].MC_AccountSwungInfo SET [EquipmentSwung] = GETUTCDATE() WHERE AccountID_OLD = @InterimAccountID;
			UPDATE [dbo].MS_AccountSwungInfo SET [EquipmentSwung] = GETUTCDATE() WHERE InterimAccountID = @InterimAccountID;

			SET @SwingStatus = 'OK';			

		--ROLLBACK TRANSACTION;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		SET @SwingStatus = '0';
		SELECT @SwingStatus as SwingStatus;
		EXEC dbo.wiseSP_ExceptionsThrown;		
		RETURN;
	END CATCH

	SELECT @SwingStatus as SwingStatus;
	PRINT 'Swing Status:' + @SwingStatus;

END

GRANT EXEC ON dbo.custAE_CustomerSWINGEquipment TO PUBLIC
GO

-- EXEC [dbo].[custAE_CustomerSWINGEquipment] 583602, 3000200, 150762