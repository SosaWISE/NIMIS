USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetCustomerGpsClientLastLoginOn')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetCustomerGpsClientLastLoginOn'
		DROP FUNCTION  dbo.fxGetCustomerGpsClientLastLoginOn
	END
GO

PRINT 'Creating FUNCTION fxGetCustomerGpsClientLastLoginOn'
GO
/******************************************************************************
**		File: fxGetCustomerGpsClientLastLoginOn.sql
**		Name: fxGetCustomerGpsClientLastLoginOn
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
**		Date: 07/11/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	07/11/2013	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetCustomerGpsClientLastLoginOn
(
	@CustomerID BIGINT
)
RETURNS DATETIME
AS
BEGIN
	DECLARE
		@Result DATETIME;

	/** Check for the date. */
	SELECT @Result = LastLoginOn FROM [dbo].AE_CustomerGpsClients WHERE (CustomerID = @CustomerID);

	RETURN @Result;
END
GO