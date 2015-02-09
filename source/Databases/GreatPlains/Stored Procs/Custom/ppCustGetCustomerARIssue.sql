USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetCustomerARIssue')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetCustomerARIssue'
		DROP  Procedure  dbo.ppCustGetCustomerARIssue
	END
GO

PRINT 'Creating Procedure ppCustGetCustomerARIssue'
GO
/******************************************************************************
**		File: ppCustGetCustomerARIssue.sql
**		Name: ppCustGetCustomerARIssue
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
**		Date: 08/20/2009
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	08/20/2009	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustGetCustomerARIssue
(
	@CustomerNumber NVARCHAR(30)
)
AS
BEGIN

	SELECT
		USERDEF2 AS ARIssue
	FROM
		RM00101
	WHERE
		CUSTNMBR = @CustomerNumber

END
GO

GRANT EXEC ON dbo.ppCustGetCustomerARIssue TO PUBLIC
GO