USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custIE_ProductBarcodesReconcileLostEquipment')
	BEGIN
		PRINT 'Dropping Procedure custIE_ProductBarcodesReconcileLostEquipment'
		DROP  Procedure  dbo.custIE_ProductBarcodesReconcileLostEquipment
	END
GO

PRINT 'Creating Procedure custIE_ProductBarcodesReconcileLostEquipment'
GO
/******************************************************************************
**		File: custIE_ProductBarcodesReconcileLostEquipment.sql
**		Name: custIE_ProductBarcodesReconcileLostEquipment
**		Desc:
**			This reconciles a given ProductBarcodeTrackingId.
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
**		Auth: Andrés Sosa
**		Date: 06/09/2008
**
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	06/09/2008	Andrés Sosa		Created by
**	06/25/2008	Todd Carlson	Modified queries so that the install date must
**								be less than the @LostDate.
**	05/08/2009	Todd Carlson	Modified so that it won't reconcile to canceled
								accounts.
								TechLostReport.aspx?SeasonID=12
**	06/03/2010	Brett Kotter	Removed the reconiling by Type
**	05/14/2015	ACLS			Updated to work with new table structure
*******************************************************************************/
CREATE Procedure dbo.custIE_ProductBarcodesReconcileLostEquipment
(
	@Barcode NVARCHAR(20)
	, @UserId INT
	, @EquipmentId VARCHAR(50)
	, @LostDate DATETIME
)
AS
BEGIN
	DECLARE @CompanyID NVARCHAR(25)
	DECLARE @LastCheckOutDate DATETIME
	DECLARE @EquipmentTypeId INT
	DECLARE	@AccountEquipmentID INT
	DECLARE @AccountID INT

	-- Get the GP Company ID
	SELECT @CompanyID = GPEmployeeID FROM [WISE_HumanResource].[dbo].RU_Users WHERE (UserID = @UserId)

	-- Find the last date this piece of equipment was checked out to this recruit.
	SELECT TOP(1)
		@LastCheckOutDate = PBT.CreatedOn
	FROM dbo.IE_ProductBarcodes AS PB
	INNER JOIN dbo.IE_ProductBarcodeTracking AS PBT
	ON
		PB.LastProductBarcodeTrackingId = PBT.ProductBarcodeTrackingID
	WHERE
		(PB.ProductBarcodeID = @Barcode)
		AND (PBT.LocationTypeId = 'Technician')
		AND (PBT.LocationId = @UserId)
		AND (PBT.ProductBarcodeTrackingTypeId IN ('REC', 'AUD-MISS'))
	ORDER BY
		PBT.CreatedOn DESC

	SELECT TOP(1) @EquipmentTypeId = EquipmentTypeId FROM MS_Equipments WHERE EquipmentId = @EquipmentId
	PRINT 'EquipmentTypeId:' + CAST(@EquipmentTypeId AS VARCHAR(20))

	IF EXISTS (SELECT * FROM MS_Equipments WHERE EquipmentId = @EquipmentId AND AccountZoneTypeId <> 'NOZONE')
	BEGIN
		PRINT '|-- ZONE EQUIPMENT -->'
		-- Check that there was a checkout date
		IF (@LastCheckOutDate IS NULL)
		BEGIN
			PRINT 'No Created On date...'
		END
		ELSE
		BEGIN
			PRINT 'Last Check Out Date: ' + Cast(@LastCheckOutDate AS NVARCHAR(20))
			PRINT 'Lost Date: ' + Cast(@LostDate AS NVARCHAR(20))
			PRINT 'Company ID: ' + @CompanyID

			-- Get the first item that matches this condition.
			SELECT TOP(1)
				@AccountEquipmentID = AE.AccountEquipmentID
				, @AccountID = MAS.AccountID
			FROM dbo.MS_Accounts AS MAS
			LEFT OUTER JOIN dbo.MS_AccountSalesInformations AS ASI
			ON
				(MAS.AccountID = ASI.AccountID)
			INNER JOIN dbo.MS_AccountEquipment AS AE
			ON
				(MAS.AccountID = AE.AccountID)
				AND (AE.EquipmentId = @EquipmentId) -- This ties it to the right SKU.
				AND (AE.IsExisting = 0)
			WHERE
				(LTRIM(RTRIM(ASI.TechId)) = LTRIM(RTRIM(@CompanyID)))
				AND (ASI.InstallDate IS NOT NULL) AND (ASI.InstallDate > '04/01/2008')
				AND (ASI.InstallDate < @LostDate) -- Make sure the account was installed before the item was marked as lost
				AND (ASI.NOCDate IS NULL)
	--			AND (ASI.InstallDate BETWEEN @LastCheckOutDate AND GetDate())
				AND (AE.BarcodeId IS NULL) -- This will show those pieces of inventory not accounted for.
				AND (@EquipmentTypeId <> 13) -- Exclude EquipmentTypeID = 13 which is tool
			ORDER BY
				MAS.AccountID
				, AE.AccountEquipmentID

			IF (@AccountEquipmentID IS NULL)
			BEGIN
				PRINT 'No equipment found'
			END
			ELSE
			BEGIN
				-- Update MS_AccountEquipment
				UPDATE dbo.MS_AccountEquipment SET BarcodeId = @Barcode WHERE AccountEquipmentID = @AccountEquipmentID

				-- Create a new Transaction
				INSERT INTO dbo.IE_ProductBarcodeTracking (
					ProductBarcodeTrackingTypeId
					, ProductBarcodeId
					, LocationTypeId
					, LocationId
					, CreatedOn
					, CreatedBy
				) VALUES (
					'CUST'
					, @Barcode
					, 'Sold'
					, @AccountID
					, GetDate()
					, 'Sys:AutoRec:EquipmentId'
				)
				PRINT 'SUCCESS: Updated By Equipment Id to Lead ID ' + Cast (@AccountID AS NVARCHAR(20))
				--SELECT * FROM dbo.MS_AccountEquipment WHERE AccountEquipmentID = @AccountEquipmentID
				--SELECT * FROM dbo.IE_ProductBarcodeTracking WHERE PartBarcodeTrackId = @@IDENTITY
				RETURN CAST(1 AS BIT) -- return true
			END
		END
	END
	ELSE
	BEGIN
		PRINT '|-- NONE ZONE EQUIPMENT -->'
		-- Get the first item that matches this condition.
		SELECT TOP(1)
			@AccountEquipmentID = AE.AccountEquipmentID
			, @AccountID = MAS.AccountID
			, @EquipmentId = ME.EquipmentId
		FROM dbo.MS_Accounts AS MAS
		LEFT OUTER JOIN dbo.MS_AccountSalesInformations AS ASI
		ON
			(MAS.AccountID = ASI.AccountID)
		INNER JOIN dbo.MS_AccountEquipment AS AE
		ON
			(MAS.AccountID = AE.AccountID)
			AND (AE.IsExisting = 0)
		INNER JOIN dbo.MS_Equipments AS ME
		ON
			(AE.EquipmentId = ME.EquipmentId)
			AND (ME.EquipmentTypeID <> 13) -- Exclude EquipmentTypeID = 13 which is tool
		WHERE
			(LTRIM(RTRIM(ASI.TechId)) = LTRIM(RTRIM(@CompanyID)))
			AND (ASI.InstallDate IS NOT NULL) AND (ASI.InstallDate > '04/01/2008')
			AND (ASI.InstallDate < @LostDate) -- Make sure the account was installed before the item was marked as lost
			AND (ASI.NOCDate IS NULL) AND (ASI.CancelDate IS NULL)
			AND (AE.BarcodeId IS NULL) -- This will show those pieces of inventory not accounted for.
			AND (ME.IsActive = 1) AND (ME.IsDeleted = 0)
		ORDER BY
			MAS.AccountID
			, AE.AccountEquipmentID

		IF(@AccountEquipmentID IS NULL)
		BEGIN
			PRINT 'No equipment found'
		END
		ELSE
		BEGIN
			-- Update the inventory ID with BarcodeID and the Equipment type used
			UPDATE dbo.MS_AccountEquipment SET
				BarcodeId = @Barcode
				, EquipmentId = @EquipmentId
			WHERE
				AccountEquipmentID = @AccountEquipmentID

			-- Create a new Transaction
			INSERT INTO dbo.IE_ProductBarcodeTracking (
				ProductBarcodeTrackingTypeId
				, ProductBarcodeId
				, LocationTypeId
				, LocationId
				, CreatedOn
				, CreatedBy
			) VALUES (
				'CUST'
				, @Barcode
				, 'Sold'
				, @AccountID
				, GetDate()
				, 'Sys:AutoRec:GPItemNmbr'
			)
			PRINT 'SUCCESS: Updated None Zone Equipment By GPItemNmbr to Lead ID ' + CAST(@AccountID AS NVARCHAR(20))
			RETURN CAST(1 AS BIT) -- return true
		END
	END
	RETURN CAST(0 AS BIT) -- return false
END
GO

GRANT EXEC ON dbo.custIE_ProductBarcodesReconcileLostEquipment TO PUBLIC
GO


/*
BEGIN TRAN
EXEC dbo.custIE_ProductBarcodesReconcileLostEquipment '716329515', 1160, 'EQPM_INVT961', '6/25/2009 11:30:25 AM'
ROLLBACK TRAN
*/