USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustPayrollGetTotalSalaryPayments')
	BEGIN
		PRINT 'Dropping Procedure ppCustPayrollGetTotalSalaryPayments'
		DROP  Procedure  dbo.ppCustPayrollGetTotalSalaryPayments
	END
GO

PRINT 'Creating Procedure ppCustPayrollGetTotalSalaryPayments'
GO
/******************************************************************************
**		File: ppCustPayrollGetTotalSalaryPayments.sql
**		Name: ppCustPayrollGetTotalSalaryPayments
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
CREATE Procedure dbo.ppCustPayrollGetTotalSalaryPayments
(
	@GPEmployeeID NVARCHAR(20)
	, @StartDate DATETIME
	, @EndDate DATETIME
)
AS
BEGIN

	SELECT
		COALESCE(SUM(Amount), 0) AS Amount
	FROM
		vwManagerSalaryPayments
	WHERE
		(GPEmployeeID = @GPEmployeeID)
		AND (CheckDate BETWEEN @StartDate AND @EndDate)
	
END
GO

GRANT EXEC ON dbo.ppCustPayrollGetTotalSalaryPayments TO PUBLIC
GO