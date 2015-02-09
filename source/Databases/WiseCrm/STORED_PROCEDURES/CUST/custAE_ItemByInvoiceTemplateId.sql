USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_ItemByInvoiceTemplateId')
	BEGIN
		PRINT 'Dropping Procedure custAE_ItemByInvoiceTemplateId'
		DROP  Procedure  dbo.custAE_ItemByInvoiceTemplateId
	END
GO

PRINT 'Creating Procedure custAE_ItemByInvoiceTemplateId'
GO
/******************************************************************************
**		File: custAE_ItemByInvoiceTemplateId.sql
**		Name: custAE_ItemByInvoiceTemplateId
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
**		Date: 12/19/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	12/19/2013	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_ItemByInvoiceTemplateId
(
	@InvoiceTemplateId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Build Statement */	
	SELECT
		ITM.*
	FROM
		[dbo].AE_Items AS ITM WITH (NOLOCK)
		INNER JOIN [dbo].AE_InvoiceTemplates AS ITMP WITH (NOLOCK)
		ON
			(ITM.ItemID = ITMP.ActivationItemId)
			OR (ITM.ItemID = ITMP.ActivationItemId)
			OR (ITM.ItemID = ITMP.ActivationDiscountItemId)
			OR (ITM.ItemID = ITMP.MMRItemId)
			OR (ITM.ItemID = ITMP.MMRDiscountItemId)
			OR (ITM.ItemID = ITMP.ActivationOverThreeMonthsId)
	WHERE
		(ITMP.InvoiceTemplateID = @InvoiceTemplateId)
	UNION
	SELECT
		ITM.*
	FROM
		[dbo].AE_Items AS ITM WITH (NOLOCK)
		INNER JOIN [dbo].AE_InvoiceTemplateItems AS ITMPI WITH (NOLOCK)
		ON
			(ITM.ItemID = ITMPI.ItemId)
	WHERE
		(ITMPI.InvoiceTemplateId = @InvoiceTemplateId);


END
GO

GRANT EXEC ON dbo.custAE_ItemByInvoiceTemplateId TO PUBLIC
GO

/** TESTING 
EXEC dbo.custAE_ItemByInvoiceTemplateId 1
*/