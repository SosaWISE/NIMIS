USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetCustomerAgeing')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetCustomerAgeing'
		DROP  Procedure  dbo.ppCustGetCustomerAgeing
	END
GO

PRINT 'Creating Procedure ppCustGetCustomerAgeing'
GO
/******************************************************************************
**		File: ppCustGetCustomerAgeing.sql
**		Name: ppCustGetCustomerAgeing
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
**		Auth: 02/23/2009
**		Date: Todd Carlson
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	02/23/2009	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustGetCustomerAgeing
(
	@CustomerNumber NVARCHAR(50)
)
AS
BEGIN

	SELECT
		AGPERAMT_1
		, AGPERAMT_2
		, AGPERAMT_3
		, AGPERAMT_4
		, AGPERAMT_5
		, AGPERAMT_6
		, CUSTBLNC 
	FROM
		RM00103 WITH (NOLOCK)
	WHERE
		(CUSTNMBR = @CustomerNumber)
	
END
GO

GRANT EXEC ON dbo.ppCustGetCustomerAgeing TO PUBLIC
GO