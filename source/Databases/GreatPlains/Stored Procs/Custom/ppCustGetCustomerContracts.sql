USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetCustomerContracts')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetCustomerContracts'
		DROP  Procedure  dbo.ppCustGetCustomerContracts
	END
GO

PRINT 'Creating Procedure ppCustGetCustomerContracts'
GO
/******************************************************************************
**		File: ppCustGetCustomerContracts.sql
**		Name: ppCustGetCustomerContracts
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
**		Auth: 
**		Date: 
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**			
*******************************************************************************/
CREATE Procedure dbo.ppCustGetCustomerContracts
(
	@PARAM INT
	, @PARAM2 NVARCHAR(25) OUTPUT
)
AS
BEGIN

	-- Enter CODE HERE>
	
END
GO

GRANT EXEC ON dbo.ppCustGetCustomerContracts TO PUBLIC
GO