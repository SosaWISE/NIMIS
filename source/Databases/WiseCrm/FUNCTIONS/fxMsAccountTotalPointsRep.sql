USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxMsAccountTotalPointsRep')
	BEGIN
		PRINT 'Dropping FUNCTION fxMsAccountTotalPointsRep'
		DROP FUNCTION  dbo.fxMsAccountTotalPointsRep
	END
GO

PRINT 'Creating FUNCTION fxMsAccountTotalPointsRep'
GO
/******************************************************************************
**		File: fxMsAccountTotalPointsRep.sql
**		Name: fxMsAccountTotalPointsRep
**		Desc: 
**		Point calculation is based on the package, activation fee, monitoring
**	rate.
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés E. Sosa
**		Date: 04/09/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/09/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxMsAccountTotalPointsRep
(
	@AccountID BIGINT	
)
RETURNS DECIMAL(5,2)
AS
BEGIN
	/** Declarations */
	DECLARE @TotalPointsRep DECIMAL(5,2);

	SELECT
		@TotalPointsRep = SUM(AEII.SystemPoints)
	FROM
		[dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
		INNER JOIN [dbo].[AE_Invoices] AS AEI WITH (NOLOCK)
		ON
			(AEI.InvoiceID = AEII.InvoiceId)
			AND (AEI.InvoiceTypeId = 'INSTALL')
			AND (AEI.AccountId = @AccountID)
			AND (AEI.IsActive = 1 AND AEI.IsDeleted = 0)
			AND (AEII.IsActive = 1 AND AEII.IsDeleted = 0)
			AND (AEII.SalesmanId IS NOT NULL)
			AND (AEII.IsCustomerPaying = 0)
		INNER JOIN [dbo].[MS_Equipments] AS MSE WITH (NOLOCK)
		ON
			(AEII.ItemId = MSE.EquipmentID)
		INNER JOIN [dbo].[MS_EquipmentTypes] AS MSET WITH (NOLOCK)
		ON
			(MSET.EquipmentTypeID = MSE.EquipmentTypeId)
			AND (MSET.EquipmentType <> 'Cell' AND MSET.EquipmentType <> 'Panel');


	RETURN @TotalPointsRep;
END
GO

/** 
DECLARE @AccountID BIGINT = 191230
	, @SeasonID INT = 4;
SELECT dbo.fxMsAccountTotalPointsRep(@AccountID);

	SELECT
		AEII.InvoiceItemID
		, AEII.SystemPoints
		, AEII.SalesmanId
		, MSE.GPItemNmbr
		, AEII.IsCustomerPaying
		, MSE.ItemDescription
		, AEII.ProductBarcodeId
	FROM
		[dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
		INNER JOIN [dbo].[AE_Invoices] AS AEI WITH (NOLOCK)
		ON
			(AEI.InvoiceID = AEII.InvoiceId)
			AND (AEI.InvoiceTypeId = 'INSTALL')
			AND (AEI.AccountId = @AccountID)
			AND (AEI.IsActive = 1 AND AEI.IsDeleted = 0)
			AND (AEII.IsActive = 1 AND AEII.IsDeleted = 0)
			AND (AEII.SalesmanId IS NOT NULL)
		INNER JOIN [dbo].[MS_Equipments] AS MSE WITH (NOLOCK)
		ON
			(AEII.ItemId = MSE.EquipmentID)
		INNER JOIN [dbo].[MS_EquipmentTypes] AS MSET WITH (NOLOCK)
		ON
			(MSET.EquipmentTypeID = MSE.EquipmentTypeId)
			AND (MSET.EquipmentType <> 'Cell' AND MSET.EquipmentType <> 'Panel');
 */