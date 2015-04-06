USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountEquipmentsViewAddExistingEquipment')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountEquipmentsViewAddExistingEquipment'
		DROP  Procedure  dbo.custMS_AccountEquipmentsViewAddExistingEquipment
	END
GO

PRINT 'Creating Procedure custMS_AccountEquipmentsViewAddExistingEquipment'
GO
/******************************************************************************
**		File: custMS_AccountEquipmentsViewAddExistingEquipment.sql
**		Name: custMS_AccountEquipmentsViewAddExistingEquipment
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
**		Date: 07/18/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/18/2014	Andres Sosa		Created By
**	04/02/2015	Andres Sosa		This PROC is flagged as OBSOLETE and not used
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountEquipmentsViewAddExistingEquipment
(
	@AccountId BIGINT
	, @EquipmentID VARCHAR(50)
	, @EquipmentLocationId INT
	, @ZoneEventTypeId INT
	, @Zone VARCHAR(3)
	, @Comments VARCHAR(MAX)
	, @IsExisting BIT
	, @IsExistingWiring BIT
	, @IsMainPanel BIT
	, @GpEmployeeID VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @AccountEquipmentID BIGINT
		, @AccountEventId INT
		, @AccountZoneAssignmentID BIGINT;

	/** Initialization. */
	-- Find AccountEventTypeId.
	SELECT @AccountEventId = AccountEventId FROM [dbo].[MS_EquipmentTypesZoneEventTypes] WHERE (EquipmentTypesZoneEventTypeID = @ZoneEventTypeId)
	
	BEGIN TRY
		BEGIN TRANSACTION;
		INSERT INTO [dbo].[MS_AccountEquipment] (
			[AccountId]
			, [EquipmentId]
			, [EquipmentLocationId]
			, [Points]
			, [Price]
			, [IsExisting]
			, [IsServiceUpgrade]
			, [IsExistingWiring]
			, [IsMainPanel]
			, [IsActive]
			, [IsDeleted]
			, [ModifiedBy]
			, [CreatedBy]
		) 
		--VALUES (
		--	@AccountId -- AccountId - bigint
		--	, @EquipmentID -- ItemId - varchar(50)
		--	, @EquipmentLocationId -- EquipmentLocationId - int
		--	, 0 -- Points - int
		--	, 0 -- Price - money
		--	, 1 -- IsExisting - bit
		--	, 0 -- IsServiceUpgrade - bit
		--	, @IsExistingWiring -- IsExistingWiring - bit
		--	, NULL -- IsMainPanel - bit
		--	, 1 -- IsActive - bit
		--	, 0 -- IsDeleted - bit
		--	, @GpEmployeeID -- ModifiedBy - nvarchar(50)
		--	, @GpEmployeeID -- CreatedBy - nvarchar(50)
		--);
		SELECT 
			@AccountId AS AccountId
			, EquipmentID AS ItemId
			, @EquipmentLocationId AS EquipmentLocationId
			, EQP.Points
			, EQP.RetailPrice AS Price
			, 1 AS IsExisting
			, 0 AS IsServiceUpgrade
			, @IsExistingWiring AS IsExistingWiring
			, @IsMainPanel
			, 1 -- IsActive - bit
			, 0 -- IsDeleted - bit
			, @GpEmployeeID -- ModifiedBy - nvarchar(50)
			, @GpEmployeeID -- CreatedBy - nvarchar(50)
		FROM
			[dbo].[MS_Equipments] AS EQP WITH (NOLOCK)
		WHERE
			(EQP.EquipmentID = @EquipmentID);

		-- Get Identity
		SET @AccountEquipmentID = SCOPE_IDENTITY();

		/** Insert into Zone table. */
		INSERT INTO MS_AccountZoneAssignments (AccountEquipmentId, AccountEventId, Zone, Comments, IsExisting)
		VALUES
		(@AccountEquipmentID, @AccountEventId, @Zone, @Comments, @IsExisting);

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
	SELECT * FROM [dbo].vwMS_AccountEquipments WHERE (AccountZoneAssignmentID = @AccountZoneAssignmentID);
END
GO

GRANT EXEC ON dbo.custMS_AccountEquipmentsViewAddExistingEquipment TO PUBLIC
GO

/** EXEC dbo.custMS_AccountEquipmentsViewAddExistingEquipment */