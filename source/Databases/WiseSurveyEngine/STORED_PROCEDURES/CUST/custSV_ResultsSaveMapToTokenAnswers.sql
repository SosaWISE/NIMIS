USE [WISE_SurveyEngine]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSV_ResultsSaveMapToTokenAnswers')
	BEGIN
		PRINT 'Dropping Procedure custSV_ResultsSaveMapToTokenAnswers'
		DROP  Procedure  dbo.custSV_ResultsSaveMapToTokenAnswers
	END
GO

PRINT 'Creating Procedure custSV_ResultsSaveMapToTokenAnswers'
GO
/******************************************************************************
**		File: custSV_ResultsSaveMapToTokenAnswers.sql
**		Name: custSV_ResultsSaveMapToTokenAnswers
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
**		Auth: Aaron Shumway
**		Date: 07/07/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	
*******************************************************************************/
CREATE Procedure dbo.custSV_ResultsSaveMapToTokenAnswers
(
	@Username NVARCHAR(50)
	, @AccountId BIGINT
	, @PrimaryCustomer_Email VARCHAR(256) = NULL
	, @SystemDetails_Password NVARCHAR(50) = NULL
	, @ContractTerms_BillingDate SMALLINT = NULL
	-- BEGIN PrimaryCustomer.FullName
	, @PrimaryCustomer_FullName_Prefix NVARCHAR(50) = NULL
	, @PrimaryCustomer_FullName_FirstName NVARCHAR(50) = NULL
	, @PrimaryCustomer_FullName_MiddleName NVARCHAR(50) = NULL
	, @PrimaryCustomer_FullName_LastName NVARCHAR(50) = NULL
	, @PrimaryCustomer_FullName_Postfix NVARCHAR(50) = NULL
	-- END PrimaryCustomer.FullName
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	-- No Transaction needed here since it should already be called within another transaction...

	
	IF ((@PrimaryCustomer_FullName_FirstName IS NOT NULL) OR (@PrimaryCustomer_Email IS NOT NULL)) BEGIN
		DECLARE @PrimaryCustomerID BIGINT

		-- similar to WISE_CRM.dbo.custAE_CustomerGetByAccountID
		SELECT
			@PrimaryCustomerID=CUST.CustomerID
		FROM 
			WISE_CRM.dbo.AE_Customers AS CUST WITH (NOLOCK)
			--INNER JOIN WISE_CRM.dbo.MS_AccountCustomers AS MAC WITH (NOLOCK)
			--ON
			--	(CUST.CustomerID = MAC.CustomerId)
			INNER JOIN WISE_CRM.dbo.AE_CustomerAccounts AS AC WITH (NOLOCK)
			ON
				(AC.CustomerId = CUST.CustomerID)
			INNER JOIN WISE_CRM.dbo.MS_Accounts AS MSA WITH (NOLOCK)
			ON
				(AC.AccountId = MSA.AccountID)
		WHERE
			(MSA.AccountID = @AccountId)
			AND (AC.CustomerTypeId = 'PRI')
	END
	
	--
	--
	--
	IF (@PrimaryCustomer_FullName_FirstName IS NOT NULL) BEGIN -- only need to check FirstName since it is required
		UPDATE WISE_CRM.dbo.AE_Customers SET
			Prefix = @PrimaryCustomer_FullName_Prefix
			, FirstName = @PrimaryCustomer_FullName_FirstName
			, MiddleName = @PrimaryCustomer_FullName_MiddleName
			, LastName = @PrimaryCustomer_FullName_LastName
			, Postfix = @PrimaryCustomer_FullName_Postfix
			, ModifiedBy = @Username
			, ModifiedOn = GetUtcDate()
		WHERE (CustomerID = @PrimaryCustomerID)
	END
	
	--
	--
	--
	IF (@PrimaryCustomer_Email IS NOT NULL) BEGIN		
		UPDATE WISE_CRM.dbo.AE_Customers SET
			Email = @PrimaryCustomer_Email
			, ModifiedBy = @Username
			, ModifiedOn = GetUtcDate()
		WHERE (CustomerID = @PrimaryCustomerID)
	END

	--
	--
	--
	IF (@SystemDetails_Password IS NOT NULL) BEGIN
		UPDATE WISE_CRM.dbo.MS_Accounts SET
			AccountPassword = @SystemDetails_Password
			, ModifiedBy = @Username
			, ModifiedOn = GetUtcDate()
		WHERE (AccountID = @AccountId)
	END

	--
	--
	--
	IF (@ContractTerms_BillingDate IS NOT NULL) BEGIN
		UPDATE WISE_CRM.dbo.MS_AccountSalesInformations SET
			BillingDay = @ContractTerms_BillingDate
			, ModifiedBy = @Username
			, ModifiedOn = GetUtcDate()
		WHERE (AccountID = @AccountId)
	END
		
END
GO

GRANT EXEC ON dbo.custSV_ResultsSaveMapToTokenAnswers TO PUBLIC
GO

/*

DECLARE @AccountID BIGINT
SET @AccountID = 100290

SELECT Email, ModifiedBy, ModifiedOn FROM WISE_CRM.dbo.AE_Customers WHERE CustomerID = 100300
SELECT AccountPassword, ModifiedBy, ModifiedOn FROM WISE_CRM.dbo.MS_Accounts WHERE AccountID = @AccountID
SELECT BillingDay, ModifiedBy, ModifiedOn FROM WISE_CRM.dbo.MS_AccountSalesInformations WHERE AccountID = @AccountID

EXEC custSV_ResultsSaveMapToTokenAnswers 'me', @AccountID
	, 'sammy2@sam2.com' -- @PrimaryCustomer_Email
	, 'this is a password' -- @SystemDetails_Password
	, 25 -- @ContractTerms_BillingDate

SELECT Email, ModifiedBy, ModifiedOn FROM WISE_CRM.dbo.AE_Customers WHERE CustomerID = 100300
SELECT AccountPassword, ModifiedBy, ModifiedOn FROM WISE_CRM.dbo.MS_Accounts WHERE AccountID = @AccountID
SELECT BillingDay, ModifiedBy, ModifiedOn FROM WISE_CRM.dbo.MS_AccountSalesInformations WHERE AccountID = @AccountID

*/