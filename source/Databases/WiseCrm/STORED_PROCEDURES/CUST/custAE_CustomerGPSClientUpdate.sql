USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerGPSClientUpdate')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerGPSClientUpdate'
		DROP  Procedure  dbo.custAE_CustomerGPSClientUpdate
	END
GO

PRINT 'Creating Procedure custAE_CustomerGPSClientUpdate'
GO
/******************************************************************************
**		File: custAE_CustomerGPSClientUpdate.sql
**		Name: custAE_CustomerGPSClientUpdate
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
**		Auth: Carly Christiansen
**		Date: 08/23/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	08/23/2013	Carly Chris		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerGPSClientUpdate
(
	@LocalizationId VARCHAR(20) = 'en-US'
	, @Prefix NVARCHAR(50)
	, @FirstName NVARCHAR(50)
	, @LastName NVARCHAR(50)
	, @Gender NVARCHAR(10) = 'NOT SET'
	, @PhoneHome VARCHAR(20) = NULL
	, @PhoneWork VARCHAR(30) = NULL
	, @PhoneMobile VARCHAR(20) = NULL
	, @Email VARCHAR(256)
	, @DOB DATETIME
	, @SSN VARCHAR(50)
	, @Username NVARCHAR(50)
	, @Password NVARCHAR(50)
	, @StateId VARCHAR(4)
	, @CountryId NVARCHAR(10)
	, @TimeZoneId INT
	, @StreetAddress NVARCHAR(50)
	, @StreetAddress2 NVARCHAR(50)
	, @County NVARCHAR(50)
	, @City NVARCHAR (50)
	, @PostalCode VARCHAR(5)
	, @PlusFour VARCHAR(4)
	, @Phone VARCHAR (20)
)
AS
BEGIN
	/** Initialize Locals. */
	DECLARE @CustomerMasterFileID BIGINT
		, @CustomerID BIGINT
		, @AddressId BIGINT
		, @CustomerAddressTypeId VARCHAR(20) = 'PREM'
		, @LeadId BIGINT
		, @EmailInUse BIT = 0;

	/** Get the Address ID. */
	SELECT @AddressId = AddressId FROM [dbo].AE_Customers WHERE (CustomerID = @CustomerID);

	BEGIN TRY

		BEGIN TRANSACTION;

		/** Check parameters. */
		SELECT @EmailInUse = dbo.fxCustomerGpsClientsCheckDuplicateEmail(@Email);
		PRINT 'Email In Use is ' + CASE WHEN @EmailInUse = 1 THEN 'True' ELSE 'False' END;

		IF (@EmailInUse = 1)
		BEGIN
			DECLARE @msg NVARCHAR(256);
			SET @msg = 'The email ' + @Email + ' is in use already.';
			THROW 51000, @msg, 1;
		END

		/** Update Customer Information. */
		UPDATE [dbo].AE_Customers SET
			Prefix = @Prefix
			, FirstName = @FirstName
			, LastName = @LastName
			, Gender = @Gender
			, PhoneHome = @PhoneHome
			, PhoneWork = @PhoneWork
			, PhoneMobile = @PhoneMobile
			, Email = @Email
			, DOB = @DOB
			, SSN = @SSN
			, Username = @Username
			, [Password] = @Password

		WHERE
			(CustomerID = @CustomerID);

		/** Update CustomerAddress. */
		UPDATE [dbo].MC_Addresses SET 
			StateId = @StateId
			, CountryId = @CountryId
			, TimeZoneId = @TimeZoneId
			, StreetAddress = @StreetAddress
			, StreetAddress2 = @StreetAddress2
			, County = @County
			, City = @City
			, PostalCode = @PostalCode
			, PlusFour = @PlusFour
			, Phone = @Phone
			/** Fill in the columns you want to change */
		WHERE 
			(AddressId = @AddressID);

		/** Update a CustomerGPS Client. */
		UPDATE [dbo].[AE_CustomerGpsClients] SET
			Username = @Username
			, [Password] = @Password
		WHERE
			(CustomerID = @CustomerID);

		COMMIT TRANSACTION;

		/** Return result. */
		SELECT * FROM dbo.vwAE_CustomerGpsClients WHERE CustomerID = @CustomerID;

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerGPSClientUpdate TO PUBLIC
GO