USE [WISE_CRM]
GO

BEGIN TRANSACTION

/** Declarations */
DECLARE @ItemId VARCHAR(50),
	@ItemIdOld VARCHAR(50),
	@GPItemNmbr VARCHAR(31),
	@ItemDescription NVARCHAR(101),
	@ShortName VARCHAR(15),
	@GenDescription NVARCHAR(11);

/**
 * REMOVE OLD GENERIC Equipment from AE_Items table.
 */
SET @ItemId = 'EQPM_EXST_MS352';
SET @ItemIdOld = 'EQPM_EXST_MS115';  
UPDATE dbo.AE_InvoiceItems SET ItemId = @ItemId WHERE ItemId = @ItemIdOld;
UPDATE dbo.IE_PackingSlipItems SET ItemId = @ItemId WHERE ItemId = @ItemIdOld;
UPDATE dbo.IE_PurchaseOrderItems SET ItemId = @ItemId WHERE ItemId = @ItemIdOld;
DELETE dbo.AE_Items WHERE ItemID = @ItemIdOld;

/** Update Existing Equipment EQPM_EXST_MS116 */
SET @ItemId = 'EQPM_EXST_MS357';
SET @ItemIdOld = 'EQPM_EXST_MS116';  
UPDATE dbo.AE_InvoiceItems SET ItemId = @ItemId WHERE ItemId = @ItemIdOld;
DELETE dbo.AE_Items WHERE ItemID = @ItemIdOld;

/** Update Existing Equipment EQPM_EXST_MS117 */
SET @ItemId = 'EQPM_EXST_MS353';
SET @ItemIdOld = 'EQPM_EXST_MS117';  
UPDATE dbo.AE_InvoiceItems SET ItemId = @ItemId WHERE ItemId = @ItemIdOld;
DELETE dbo.AE_Items WHERE ItemID = @ItemIdOld;

/** Update Existing Equipment EQPM_EXST_MS118 */
SET @ItemId = 'EQPM_EXST_MS354';
SET @ItemIdOld = 'EQPM_EXST_MS118';  
UPDATE dbo.AE_InvoiceItems SET ItemId = @ItemId WHERE ItemId = @ItemIdOld;
DELETE dbo.AE_Items WHERE ItemID = @ItemIdOld;

/** Update Existing Equipment EQPM_EXST_MS119 */
SET @ItemId = 'EQPM_EXST_MS359';
SET @ItemIdOld = 'EQPM_EXST_MS119';  
UPDATE dbo.AE_InvoiceItems SET ItemId = @ItemId WHERE ItemId = @ItemIdOld;
DELETE dbo.AE_Items WHERE ItemID = @ItemIdOld;

/** Update Existing Equipment EQPM_EXST_MS120 */
SET @ItemId = 'EQPM_EXST_MS355';
SET @ItemIdOld = 'EQPM_EXST_MS120';  
UPDATE dbo.AE_InvoiceItems SET ItemId = @ItemId WHERE ItemId = @ItemIdOld;
DELETE dbo.AE_Items WHERE ItemID = @ItemIdOld;

/** Update Existing Equipment EQPM_EXST_MS121 */
SET @ItemId = 'EQPM_EXST_MS355';
SET @ItemIdOld = 'EQPM_EXST_MS121';  
UPDATE dbo.AE_InvoiceItems SET ItemId = @ItemId WHERE ItemId = @ItemIdOld;
DELETE dbo.AE_Items WHERE ItemID = @ItemIdOld;

/** Update Existing Equipment EQPM_EXST_MS122 */
SET @ItemId = 'EQPM_EXST_MS356';
SET @ItemIdOld = 'EQPM_EXST_MS122';  
UPDATE dbo.AE_InvoiceItems SET ItemId = @ItemId WHERE ItemId = @ItemIdOld;
DELETE dbo.AE_Items WHERE ItemID = @ItemIdOld;

/** UPATE ItemSKU of GENERIC EXISTING EQUIPMENT. */
/** Updating EQPM_EXST_MS352 */
SET @ItemId = 'EQPM_EXST_MS352';
SET @GPItemNmbr = 'EEQ-GEN-DOORWINDOW';
UPDATE dbo.AE_Items SET ItemSKU = @GPItemNmbr WHERE (ItemID = @ItemId);
UPDATE dbo.MS_Equipments SET GPItemNmbr = @GPItemNmbr WHERE (EquipmentID = @ItemId);


/** Updating EQPM_EXST_MS353 */
SET @ItemId = 'EQPM_EXST_MS353';
SET @GPItemNmbr = 'EEQ-GEN-KEYFOB';
UPDATE dbo.AE_Items SET ItemSKU = @GPItemNmbr WHERE (ItemID = @ItemId);
UPDATE dbo.MS_Equipments SET GPItemNmbr = @GPItemNmbr WHERE (EquipmentID = @ItemId);


/** Updating EQPM_EXST_MS354 */
SET @ItemId = 'EQPM_EXST_MS354';
SET @GPItemNmbr = 'EEQ-GEN-KEYPAD';
UPDATE dbo.AE_Items SET ItemSKU = @GPItemNmbr WHERE (ItemID = @ItemId);
UPDATE dbo.MS_Equipments SET GPItemNmbr = @GPItemNmbr WHERE (EquipmentID = @ItemId);


/** Updating EQPM_EXST_MS355 */
SET @ItemId = 'EQPM_EXST_MS355';
SET @GPItemNmbr = 'EEQ-GEN-MOTION';
UPDATE dbo.AE_Items SET ItemSKU = @GPItemNmbr WHERE (ItemID = @ItemId);
UPDATE dbo.MS_Equipments SET GPItemNmbr = @GPItemNmbr WHERE (EquipmentID = @ItemId);


/** Updating EQPM_EXST_MS356 */
SET @ItemId = 'EQPM_EXST_MS356';
SET @GPItemNmbr = 'EEQ-GEN-SMOKE';
UPDATE dbo.AE_Items SET ItemSKU = @GPItemNmbr WHERE (ItemID = @ItemId);
UPDATE dbo.MS_Equipments SET GPItemNmbr = @GPItemNmbr WHERE (EquipmentID = @ItemId);


/** Updating EQPM_EXST_MS357 */
SET @ItemId = 'EQPM_EXST_MS357';
SET @GPItemNmbr = 'EEQ-GEN-GLASS';
UPDATE dbo.AE_Items SET ItemSKU = @GPItemNmbr WHERE (ItemID = @ItemId);
UPDATE dbo.MS_Equipments SET GPItemNmbr = @GPItemNmbr WHERE (EquipmentID = @ItemId);


/** Updating EQPM_EXST_MS359 */
SET @ItemId = 'EQPM_EXST_MS359';
SET @GPItemNmbr = 'EEQ-GEN-MEDICALPEN';
UPDATE dbo.AE_Items SET ItemSKU = @GPItemNmbr WHERE (ItemID = @ItemId);
UPDATE dbo.MS_Equipments SET GPItemNmbr = @GPItemNmbr WHERE (EquipmentID = @ItemId);


/** Updating EQPM_EXST_MS360 */
SET @ItemId = 'EQPM_EXST_MS360';
SET @GPItemNmbr = 'EEQ-GEN-CARBON';
UPDATE dbo.AE_Items SET ItemSKU = @GPItemNmbr WHERE (ItemID = @ItemId);
UPDATE dbo.MS_Equipments SET GPItemNmbr = @GPItemNmbr WHERE (EquipmentID = @ItemId);


/** Updating EQPM_EXST_MS361 */
SET @ItemId = 'EQPM_EXST_MS361';
SET @GPItemNmbr = 'EEQ-GEN-FLOOD';
UPDATE dbo.AE_Items SET ItemSKU = @GPItemNmbr WHERE (ItemID = @ItemId);
UPDATE dbo.MS_Equipments SET GPItemNmbr = @GPItemNmbr WHERE (EquipmentID = @ItemId);


/** Updating EQPM_EXST_MS362 */
SET @ItemId = 'EQPM_EXST_MS362';
SET @GPItemNmbr = 'EEQ-GEN-FREEZE';
UPDATE dbo.AE_Items SET ItemSKU = @GPItemNmbr WHERE (ItemID = @ItemId);
UPDATE dbo.MS_Equipments SET GPItemNmbr = @GPItemNmbr WHERE (EquipmentID = @ItemId);


/** Updating EQPM_EXST_MS363 */
SET @ItemId = 'EQPM_EXST_MS363';
SET @GPItemNmbr = 'EEQ-GEN-DSLFILTER';
UPDATE dbo.AE_Items SET ItemSKU = @GPItemNmbr WHERE (ItemID = @ItemId);
UPDATE dbo.MS_Equipments SET GPItemNmbr = @GPItemNmbr WHERE (EquipmentID = @ItemId);


/** Updating EQPM_EXST_MS364 */
SET @ItemId = 'EQPM_EXST_MS364';
SET @GPItemNmbr = 'EEQ-GEN-ZONEEXPANDER';
UPDATE dbo.AE_Items SET ItemSKU = @GPItemNmbr WHERE (ItemID = @ItemId);
UPDATE dbo.MS_Equipments SET GPItemNmbr = @GPItemNmbr WHERE (EquipmentID = @ItemId);

ROLLBACK TRANSACTION