USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustSetCustomerEFTInfo')
	BEGIN
		PRINT 'Dropping Procedure ppCustSetCustomerEFTInfo'
		DROP  Procedure  dbo.ppCustSetCustomerEFTInfo
	END
GO

PRINT 'Creating Procedure ppCustSetCustomerEFTInfo'
GO
/******************************************************************************
**		File: ppCustSetCustomerEFTInfo.sql
**		Name: ppCustSetCustomerEFTInfo
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
CREATE Procedure dbo.ppCustSetCustomerEFTInfo
(
	@CustomerNumber CHAR(15)
	, @AccountType SMALLINT
	, @AccountNumber CHAR(30)
	, @RoutingNumber CHAR(30)
)
AS
BEGIN

	IF @AccountType IS NOT NULL
	BEGIN
		UPDATE SY06000 SET
			EFTAccountType = @AccountType
		WHERE
			CUSTNMBR = @CustomerNumber
	END
	
	IF @AccountNumber IS NOT NULL
	BEGIN
		UPDATE SY06000 SET
			EFTBankAcct = @AccountNumber
		WHERE
			CUSTNMBR = @CustomerNumber
	END
	
	IF @RoutingNumber IS NOT NULL
	BEGIN
		UPDATE SY06000 SET
			EFTTransitRoutingNo = @RoutingNumber
		WHERE
			CUSTNMBR = @CustomerNumber
	END

END
GO

GRANT EXEC ON dbo.ppCustSetCustomerEFTInfo TO PUBLIC
GO