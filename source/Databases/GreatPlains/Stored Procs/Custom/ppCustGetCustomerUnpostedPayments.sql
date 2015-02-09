USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetCustomerUnpostedPayments')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetCustomerUnpostedPayments'
		DROP  Procedure  dbo.ppCustGetCustomerUnpostedPayments
	END
GO

PRINT 'Creating Procedure ppCustGetCustomerUnpostedPayments'
GO
/******************************************************************************
**		File: ppCustGetCustomerUnpostedPayments.sql
**		Name: ppCustGetCustomerUnpostedPayments
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
**		Auth: Brett Kotter
**		Date: 09/30/2009
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	09/30/2009	Brett Kotter	Created
**	10/07/2009	Brett Kotter	Updated sproc to show correct amount with tax
*******************************************************************************/
CREATE Procedure dbo.ppCustGetCustomerUnpostedPayments
(
	@AccountId NVARCHAR(15)
)
AS
BEGIN
	SELECT
		DOCAMNT  AS [Amount]
		, DUEDATE AS [DueDate]
		, DOCID AS [Reason]
FROM
      SOP10100 WITH (NOLOCK)
WHERE
      (CUSTNMBR=@AccountId)


END
GO

GRANT EXEC ON dbo.ppCustGetCustomerUnpostedPayments TO PUBLIC
GO 