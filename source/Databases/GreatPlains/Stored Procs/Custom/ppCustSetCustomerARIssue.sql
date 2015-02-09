USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustSetCustomerARIssue')
	BEGIN
		PRINT 'Dropping Procedure ppCustSetCustomerARIssue'
		DROP  Procedure  dbo.ppCustSetCustomerARIssue
	END
GO

PRINT 'Creating Procedure ppCustSetCustomerARIssue'
GO
/******************************************************************************
**		File: ppCustSetCustomerARIssue.sql
**		Name: ppCustSetCustomerARIssue
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
**		Date: 01/18/2010
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/18/2010	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustSetCustomerARIssue
(
	@CustomerNumber NVARCHAR(30),
	@ARIssue NVARCHAR(30)
)
AS
BEGIN

	UPDATE
		RM00101
	SET
		USERDEF2 = @ARIssue
	WHERE
		CUSTNMBR = @CustomerNumber

END
GO

GRANT EXEC ON dbo.ppCustSetCustomerARIssue TO PUBLIC
GO