USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_ContractTemplatesGetByInvoiceTemplateId')
	BEGIN
		PRINT 'Dropping Procedure custAE_ContractTemplatesGetByInvoiceTemplateId'
		DROP  Procedure  dbo.custAE_ContractTemplatesGetByInvoiceTemplateId
	END
GO

PRINT 'Creating Procedure custAE_ContractTemplatesGetByInvoiceTemplateId'
GO
/******************************************************************************
**		File: custAE_ContractTemplatesGetByInvoiceTemplateId.sql
**		Name: custAE_ContractTemplatesGetByInvoiceTemplateId
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
**		Date: 00/00/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	00/00/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_ContractTemplatesGetByInvoiceTemplateId
(
	@InvoiceTemplateId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Transfer data */
	SELECT
		*
	FROM
		[dbo].AE_ContractTemplates AS ACT WITH (NOLOCK)
	ORDER BY
		OrderNumber;
	
END
GO

GRANT EXEC ON dbo.custAE_ContractTemplatesGetByInvoiceTemplateId TO PUBLIC
GO