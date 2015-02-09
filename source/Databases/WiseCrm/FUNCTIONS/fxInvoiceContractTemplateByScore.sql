USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxInvoiceContractTemplateByScore')
	BEGIN
		PRINT 'Dropping FUNCTION fxInvoiceContractTemplateByScore'
		DROP FUNCTION  dbo.fxInvoiceContractTemplateByScore
	END
GO

PRINT 'Creating FUNCTION fxInvoiceContractTemplateByScore'
GO
/******************************************************************************
**		File: fxInvoiceContractTemplateByScore.sql
**		Name: fxInvoiceContractTemplateByScore
**		Desc: 
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
**		Date: 02/11/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/11/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxInvoiceContractTemplateByScore
(
	@Score INT
	, @DefaultItemId INT = 2
)
RETURNS INT
AS
BEGIN
	IF (@Score IS NULL) RETURN @DefaultItemId;
	
	IF (@Score < 650) RETURN 2;

	RETURN 1;
END
GO

/** 
SELECT dbo.fxInvoiceContractTemplateByScore(NULL, 0) AS ItemID;
SELECT dbo.fxInvoiceContractTemplateByScore(300, 1) AS ItemID;
SELECT dbo.fxInvoiceContractTemplateByScore(650, 2) AS ItemID;
 */