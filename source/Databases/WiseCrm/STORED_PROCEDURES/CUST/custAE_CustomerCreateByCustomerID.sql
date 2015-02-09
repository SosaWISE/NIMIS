USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerCreateByCustomerID')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerCreateByCustomerID'
		DROP  Procedure  dbo.custAE_CustomerCreateByCustomerID
	END
GO

PRINT 'Creating Procedure custAE_CustomerCreateByCustomerID'
GO
/******************************************************************************
**		File: custAE_CustomerCreateByCustomerID.sql
**		Name: custAE_CustomerCreateByCustomerID
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
**		Auth: Andres Sosa
**		Date: 05/27/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/27/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerCreateByCustomerID
(
	@CustomerID BIGINT
	, @CustomerTypeId VARCHAR(20)
	, @BillingAddressId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	BEGIN TRY
		BEGIN TRANSACTION
		/** Transfer data */
		
		INSERT INTO dbo.AE_Customers (
			CustomerTypeId
			, CustomerMasterFileId
			, DealerId
			, AddressId
			, LeadId
			, LocalizationId
			, Prefix
			, FirstName
			, MiddleName
			, LastName
			, Postfix
			, Gender
			, PhoneHome
			, PhoneWork
			, PhoneMobile
			, Email
			, DOB
			, SSN
		)
		SELECT
			@CustomerTypeId AS CustomerTypeId
			, CUST.CustomerMasterFileId
			, CUST.DealerId
			, @BillingAddressId AS AddressId
			, CUST.LeadId
			, CUST.LocalizationId
			, CUST.Prefix
			, CUST.FirstName
			, CUST.MiddleName
			, CUST.LastName
			, CUST.Postfix
			, CUST.Gender
			, CUST.PhoneHome
			, CUST.PhoneWork
			, CUST.PhoneMobile
			, CUST.Email
			, CUST.DOB
			, CUST.SSN
		FROM
			AE_Customers AS CUST WITH (NOLOCK)
		WHERE
			(CUST.CustomerID = @CustomerID)
		-- Get Identity
		DECLARE @NewCustomerID BIGINT;
		SET @NewCustomerID = SCOPE_IDENTITY();
		
		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
		RETURN
	END CATCH

	/** Return result */
	SELECT * FROM dbo.AE_Customers WHERE CustomerID = @NewCustomerID
END
GO

GRANT EXEC ON dbo.custAE_CustomerCreateByCustomerID TO PUBLIC
GO