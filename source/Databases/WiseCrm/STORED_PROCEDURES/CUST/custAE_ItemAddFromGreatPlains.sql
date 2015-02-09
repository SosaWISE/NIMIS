USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_ItemAddFromGreatPlains')
	BEGIN
		PRINT 'Dropping Procedure custAE_ItemAddFromGreatPlains'
		DROP  Procedure  dbo.custAE_ItemAddFromGreatPlains
	END
GO

PRINT 'Creating Procedure custAE_ItemAddFromGreatPlains'
GO
/******************************************************************************
**		File: custAE_ItemAddFromGreatPlains.sql
**		Name: custAE_ItemAddFromGreatPlains
**
**		Desc: 
**		Item SKUs get entered into Great Plains.  This stored procedure will pull
**		new SKUs into the AE_Items table.  It will also update 
**
**		Return values:
**		None
** 
**		Called by:  
**		Run by Job Agent each hour 
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**		None							None
**
**		Auth: Bob McFadden
**		Date: 07/01/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/01/2014	Bob McFadden	Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_ItemAddFromGreatPlains
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @MAXItemID INT
	
	BEGIN TRY
		BEGIN TRANSACTION;

	-- Item IDs are inserted with a default ItemID of EQPM_INVT###.
	-- Set @MaxItemID to the highest ### already used
	SELECT @MAXItemID = 
		CASE
			WHEN MAX(RIGHT(ItemID,3)) IS NOT NULL THEN MAX(RIGHT(ItemID,3))
			ELSE 0
		END
	FROM WISE_CRM.dbo.AE_ITEMS
	WHERE 
		ItemTypeId = 'EQPM_INVT'
		AND ISNUMERIC(RIGHT(ItemID,3)) = 1

	/************************************
	***  INSERT NEW SKUs ON AE_ITEMS  ***
	*************************************/
	INSERT WISE_CRM.DBO.AE_Items (
		ItemID,
		ItemTypeId,
		TaxOptionId,
		ItemSKU,
		ItemDesc,
		Cost,
		Price,
		SystemPoints,
		ModifiedOn,
		CreatedOn
		-- Columns which will use the default
		--AccountZoneTypeId ,
		--ItemFKID ,
		--IsCatalogItem ,
		--IsActive ,
		--IsDeleted ,
		--ModifiedBy ,
		--CreatedBy ,
		)
	SELECT
		'EQPM_INVT' + CONVERT(VARCHAR,ROW_NUMBER() OVER (ORDER BY GP_SKUs.ITEMNMBR) + @MAXItemID) AS ItemID,
		'EQPM_INVT' AS ItemTypeID,
		CASE
			WHEN GP_SKUs.ITMTSHID IS NULL THEN 'TAX'
			WHEN GP_SKUs.ITMTSHID = '' THEN 'TAX'
			ELSE 'EXT'
		END AS TaxOptionID,
		LTRIM(RTRIM(GP_SKUs.ITEMNMBR)) AS ItemSKU,
		LTRIM(RTRIM(GP_SKUs.ITEMDESC)) AS ItemDesc,
		CONVERT(MONEY,
			CASE
				WHEN GP_SKUs.CURRCOST IS NULL THEN 0
				ELSE GP_SKUs.CURRCOST
			END) AS Cost,
		CONVERT(MONEY,
			CASE
				WHEN GP_SKUs.CURRCOST IS NULL THEN 0
				ELSE GP_SKUs.CURRCOST
			END) AS Price,
		CONVERT(DECIMAL(9,1),LTRIM(RTRIM(
			CASE 
				WHEN ISNUMERIC(LTRIM(RTRIM(GP_SKUs.USCATVLS_2))) = 1 THEN CONVERT(DECIMAL(9,1),LTRIM(RTRIM(GP_SKUs.USCATVLS_2)))
				ELSE 0
			END))) AS SystemPoints,
		GP_SKUs.MODIFDT AS ModifiedOn,
		GP_SKUs.CREATDDT AS CreatedOn
	FROM 
		[DYSNEYDAD].NEX.dbo.IV00101 AS GP_SKUs WITH(NOLOCK)
		LEFT JOIN dbo.AE_ITEMS AS CRM_SKUs WITH(NOLOCK)
			ON GP_SKUs.ITEMNMBR = CRM_SKUs.ItemSKU
			AND GP_SKUs.INACTIVE = 0
	WHERE 
		CRM_SKUs.ItemSKU IS NULL

	/*****************************************
	***  UPDATE MODIFIED SKUs ON AE_ITEMS  ***
	******************************************/
	UPDATE CRM_SKUs
	SET 
		TaxOptionId = 
			CASE
				WHEN GP_SKUs.ITMTSHID IS NULL THEN 'TAX'
				WHEN GP_SKUs.ITMTSHID = '' THEN 'TAX'
				ELSE 'EXT'
			END,
		ItemDesc = LTRIM(RTRIM(GP_SKUs.ITEMDESC)),
		Cost = CONVERT(MONEY,
			CASE
				WHEN GP_SKUs.CURRCOST IS NULL THEN 0
				ELSE GP_SKUs.CURRCOST
			END),
		Price = CONVERT(MONEY,
			CASE
				WHEN GP_SKUs.CURRCOST IS NULL THEN 0
				ELSE GP_SKUs.CURRCOST
			END),
		SystemPoints = CONVERT(DECIMAL(9,1),LTRIM(RTRIM(
			CASE
				WHEN ISNUMERIC(LTRIM(RTRIM(GP_SKUs.USCATVLS_2))) = 1 THEN CONVERT(DECIMAL(9,1),LTRIM(RTRIM(GP_SKUs.USCATVLS_2)))
				ELSE 0
			END))),
		isActive =
			CASE
				WHEN GP_SKUs.INACTIVE = 1 THEN 0
				ELSE 1
			END,
		ModifiedOn = GP_SKUs.MODIFDT
	FROM 
		[DYSNEYDAD].NEX.dbo.IV00101 AS GP_SKUs WITH(NOLOCK)
		JOIN WISE_CRM.dbo.AE_ITEMS AS CRM_SKUs WITH(NOLOCK)
			ON GP_SKUs.ITEMNMBR = CRM_SKUs.ItemSKU
			and GP_SKUs.MODIFDT > CRM_SKUs.ModifiedOn
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_ItemAddFromGreatPlains TO PUBLIC
GO

/** EXEC dbo.custAE_ItemAddFromGreatPlains */