USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustSetupEFT')
	BEGIN
		PRINT 'Dropping Procedure ppCustSetupEFT'
		DROP  Procedure  dbo.ppCustSetupEFT
	END
GO

PRINT 'Creating Procedure ppCustSetupEFT'
GO
/******************************************************************************
**		File: ppCustSetupEFT.sql
**		Name: ppCustSetupEFT
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
**		Date: 12/09/2008
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	12/09/2008	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustSetupEFT
(
	@CustomerNumber CHAR(15)
	, @AccountNumber CHAR(30)
	, @RoutingNumber CHAR(30)
)
AS
BEGIN

	-- Set output parameters to default values
	SELECT
		@O_iErrorState = 0
		, @oErrString = ''

	IF EXISTS (SELECT CUSTNMBR FROM SY06000 WHERE CustomerVendor_ID = @I_vCustomerNumber)
	BEGIN
		-- EFT already exists for the customer, update it
		UPDATE SY06000 SET
			EFTAccountType = @I_vAccountType
			, EFTBankAcct = @I_vAccountNumber
			, EFTTransitRoutingNo = @I_vRoutingNumber
			, ADRSCODE = @I_vBillingAddressCode
		WHERE
			CUSTNMBR = @I_vCustomerNumber
	END
	ELSE
	BEGIN
		-- EFT doesn't yet exist for customer, insert a record
		INSERT INTO SY06000
		(
			SERIES
			, CustomerVendor_ID
			, CUSTNMBR
			, ADRSCODE
			, EFTUseMasterID
			, EFTBankType
			, FRGNBANK
			, INACTIVE
			, EFTBankAcct
			, GIROPostType
			, BankInfo7
			, EFTTransitRoutingNo
			, EFTTransferMethod
			, EFTAccountType
			, EFTPrenoteDate
			, EFTTerminationDate
		)
		VALUES
		(
			3 -- SERIES
			, @I_vCustomerNumber
			, @I_vCustomerNumber
			, @I_vBillingAddressCode
			, 1 -- EFTUseMasterID
			, 31 -- EFTBankType (USA)
			, 0 -- FRGNBANK
			, 0 -- INACTIVE
			, @I_vAccountNumber
			, 0 -- GIROPostType
			, 0 --BankInfo7
			, @I_vRoutingNumber
			, 1 -- EFTTransferMethod
			, @I_vAccountType
			, '2008-01-01 00:00:00.000'
			, '1900-01-01 00:00:00.000'
		)
	END

END
GO

GRANT EXEC ON dbo.ppCustSetupEFT TO PUBLIC
GO