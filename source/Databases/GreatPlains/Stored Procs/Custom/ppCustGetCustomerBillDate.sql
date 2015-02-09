 USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetCustomerBillDate')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetCustomerBillDate'
		DROP  Procedure  dbo.ppCustGetCustomerBillDate
	END
GO

PRINT 'Creating Procedure ppCustGetCustomerBillDate'
GO
/******************************************************************************
**		File: ppCustGetCustomerBillDate.sql
**		Name: ppCustGetCustomerBillDate
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
**		Date: 01/19/2010
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/19/2010	Brett Kotter	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustGetCustomerBillDate
(
	@CustomerNumber NVARCHAR(50)
)
AS
BEGIN

	SELECT TOP 1 
			BILONDY AS [BillDate]
	FROM SVC00600 WITH (NOLOCK)
		WHERE custnmbr = @CustomerNumber
		AND dscriptn = 'Monitoring'
	ORDER BY Credit_Hold

END
GO

GRANT EXEC ON dbo.ppCustGetCustomerBillDate TO PUBLIC
GO