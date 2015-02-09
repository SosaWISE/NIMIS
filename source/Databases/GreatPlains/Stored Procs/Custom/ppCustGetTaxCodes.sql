USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetTaxCodes')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetTaxCodes'
		DROP  Procedure  dbo.ppCustGetTaxCodes
	END
GO

PRINT 'Creating Procedure ppCustGetTaxCodes'
GO
/******************************************************************************
**		File: ppCustGetTaxCodes.sql
**		Name: ppCustGetTaxCodes
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
**		Auth: Todd Carlson
**		Date: 03/15/2010
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**			
*******************************************************************************/
CREATE Procedure dbo.ppCustGetTaxCodes
AS
BEGIN

	SELECT
		RTRIM(CLS.ClassID) AS ClassID
		, RTRIM(CLS.CLASDSCR) AS Description
		, RTRIM(TAXM.TAXDTLID) AS Code
		, TXDTLPCT AS Rate
	FROM
		RM00201 AS CLS WITH (NOLOCK)
		INNER JOIN TX00201 AS TAXM WITH (NOLOCK)
		ON
			(TAXM.TAXDTLID = CLS.TAXSCHID)
	
END
GO

GRANT EXEC ON dbo.ppCustGetTaxCodes TO PUBLIC
GO