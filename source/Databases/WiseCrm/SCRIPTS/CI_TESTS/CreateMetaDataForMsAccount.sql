/*****
* Create Account information for testing
*/

/** ARGUMENTS */
DECLARE @CMFID BIGINT = 3000001
	, @CustomerID BIGINT
	, @AddressID BIGINT
	, @LeadID BIGINT
	, @AccountID BIGINT
	, @CustomerTypeID VARCHAR(20) = 'BILL';

--SELECT 
--	* 
--FROM 
--	dbo.AE_CustomerAccounts AS CAC WITH(NOLOCK)
--	INNER JOIN dbo.AE_Customers AS CST WITH (NOLOCK)
--	ON
--		(CST.CustomerID = CAC.CustomerId)
--	INNER JOIN dbo.MC_Accounts AS MAC WITH (NOLOCK)
--	ON
--		(MAC.AccountID = CAC.AccountId)
--WHERE
--	(CST.CustomerMasterFileId = @CMFID);
--SELECT * FROM dbo.AE_CUstomers WHERE CustomerMasterFileId = @CMFID;
SELECT @LeadID = LeadID FROM dbo.QL_Leads WHERE CustomerMasterFileId = @CMFID;

BEGIN TRY
	BEGIN TRANSACTION
		/** Check to see if there is a customer already. */
		IF (EXISTS(SELECT * FROM dbo.AE_Customers AS CST WITH (NOLOCK)
				INNER JOIN dbo.AE_CustomerAccounts AS CAC WITH (NOLOCK)
				ON CAC.CustomerId = CST.CustomerID
			WHERE (CST.CustomerMasterFileId = @CMFID)))
		BEGIN
			SELECT @CustomerID = CST.CustomerID FROM dbo.AE_Customers AS CST WITH (NOLOCK)
				INNER JOIN dbo.AE_CustomerAccounts AS CAC WITH (NOLOCK)
				ON CAC.CustomerId = CST.CustomerID
			WHERE (CST.CustomerMasterFileId = @CMFID)
		END
		ELSE
		BEGIN
			/** Create a customer from the original lead. */
			EXEC dbo.custAE_CustomerCreateFromLead @LeadID, -- bigint
				@CustomerTypeID, -- varchar(20)
				@CustomerID OUTPUT; -- bigint
		END

		PRINT 'CustomerID: ' + CAST(@CustomerID AS VARCHAR);
		-- Get Infor
		SELECT @AddressID = AddressID FROM dbo.AE_Customers WHERE CustomerID = @CustomerID;

		/** Create an MC Account */
		INSERT INTO dbo.MC_Accounts (
			AccountTypeId
			, ShipContactId
			, ShipAddressId
			, DealerAccountId
			, ShipContactSameAsCustomer
			, ShipAddressSameAsCustomer
			, AccountName
			, AccountDesc
		) VALUES (
			'ALRM' , -- AccountTypeId - varchar(20)
			@CustomerID , -- ShipContactId - bigint
			@AddressID , -- ShipAddressId - bigint
			N'SOMEDEALIERACCT ID' , -- DealerAccountId - nvarchar(50)
			1 , -- ShipContactSameAsCustomer - bit
			1 , -- ShipAddressSameAsCustomer - bit
			N'My main Home Security System' , -- AccountName - nvarchar(100)
			N'This is the system that is installed in my NY Penthouse.' -- AccountDesc - nvarchar(max)
		);

		SET @AccountID = SCOPE_IDENTITY();
		PRINT 'AccountID: ' + CAST(@AccountID AS VARCHAR);

		/** Set the Customer as the billing for the account*/
		INSERT INTO dbo.AE_CustomerAccounts ( CustomerId, AccountId )
		VALUES  ( @CustomerID, @AccountID);

		/** Create the Customer type in the list of cutomer for this type of account. */
		INSERT INTO dbo.MC_AccountCustomers(
			AccountId
			, CustomerId
			, CustomerTypeId
		) VALUES (
			@AccountID -- AccountId - bigint
			, @CustomerId -- - bigint
			, 'BILL' -- CustomerTypeId - varchar(20)
		);

	COMMIT TRANSACTION
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH


SELECT 
	ACT.*
FROM
	dbo.MC_AccountCustomers AS ACT WITH (NOLOCK)
	INNER JOIN dbo.AE_Customers AS CST WITH (NOLOCK)
	ON
		(CST.CustomerID = ACT.CustomerId)
		AND (CST.CustomerMasterFileId = 3000001);