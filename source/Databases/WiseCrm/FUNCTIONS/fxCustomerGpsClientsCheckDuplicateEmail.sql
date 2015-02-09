USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxCustomerGpsClientsCheckDuplicateEmail')
	BEGIN
		PRINT 'Dropping FUNCTION fxCustomerGpsClientsCheckDuplicateEmail'
		DROP FUNCTION  dbo.fxCustomerGpsClientsCheckDuplicateEmail
	END
GO

PRINT 'Creating FUNCTION fxCustomerGpsClientsCheckDuplicateEmail'
GO
/******************************************************************************
**		File: fxCustomerGpsClientsCheckDuplicateEmail.sql
**		Name: fxCustomerGpsClientsCheckDuplicateEmail
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
**		Date: 05/26/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/26/2012	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxCustomerGpsClientsCheckDuplicateEmail
(
	@Email NVARCHAR(256)
)
RETURNS BIT
AS
BEGIN
	IF (EXISTS(SELECT * FROM dbo.AE_Customers WHERE Email = @Email)) RETURN 1;
	RETURN 0;
END
GO


--SELECT * FROM dbo.AE_Customers WHERE Email = 'beck@something.com';