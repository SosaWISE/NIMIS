USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountAndLeadInfoCreate')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountAndLeadInfoCreate'
		DROP  Procedure  dbo.custMS_AccountAndLeadInfoCreate
	END
GO

--PRINT 'Creating Procedure custMS_AccountAndLeadInfoCreate'
--GO
--/******************************************************************************
--**		File: custMS_AccountAndLeadInfoCreate.sql
--**		Name: custMS_AccountAndLeadInfoCreate
--**		Desc: 
--**
--**		This template can be customized:
--**              
--**		Return values:
--** 
--**		Called by:   
--**              
--**		Parameters:
--**		Input							Output
--**     ----------						-----------
--**
--**		Auth: Andres Sosa
--**		Date: 01/13/2014
--*******************************************************************************
--**	Change History
--*******************************************************************************
--**	Date:		Author:			Description:
--**	-----------	---------------	-----------------------------------------------
--**	01/13/2014	Andres Sosa		Created By
--**	
--*******************************************************************************/
--CREATE Procedure dbo.custMS_AccountAndLeadInfoCreate
--(
--	@LeadID BIGINT
--	, @GPEmployeeId NVARCHAR(25)
--)
--AS
--BEGIN
--	/** SET NO COUNTING */
--	SET NOCOUNT ON

--	/** DECLARATIONS */
--	DECLARE @CustomerId BIGINT
--		, @AccountId BIGINT
--		, @CMFID BIGINT
--		, @PremiseAddressId BIGINT;

--	/** Inititlize. */
--	SELECT @CMFID = CustomerMasterFileId FROM [dbo].[QL_Leads] WHERE (LeadID = @LeadID);

--	BEGIN TRY
--		BEGIN TRANSACTION;
		
--		/** Create Customer */	
--		EXEC [dbo].[custAE_CustomerCreateFromLead]
--			@LeadID = @LeadID,
--			@CustomerTypeID = N'PRI',
--			@CustomerAddressTypeID = N'PRI',
--			@HideResult = 1,
--			@CustomerID = @CustomerID OUTPUT;

--		/** Get Premise Address ID */
--		SELECT @PremiseAddressId = AddressId FROM [dbo].[AE_Customers] WHERE (CustomerID = @CustomerId);

--		/** Create the MC_Account */
--		INSERT INTO [dbo].MC_Accounts (
--			AccountTypeId
--			, ShipContactId
--			, ShipContactSameAsCustomer
--			, ShipAddressSameAsCustomer
--			, ModifiedBy
--			, CreatedBy
--		) VALUES (
--			'ALRM' -- AccountTypeId - varchar(20)
--			, @CustomerID -- ShipContactId - bigint
--			, 1 -- ShipContactSameAsCustomer - bit
--			, 1 -- ShipAddressSameAsCustomer - bit
--			, @GPEmployeeId -- ModifiedBy - nvarchar(50)
--			, @GPEmployeeId  -- CreatedBy - nvarchar(50)
--		);
--		SET @AccountId = SCOPE_IDENTITY();

--		/** Create the Customer Accounts record. */
--		INSERT INTO [dbo].[MS_AccountCustomers] ([AccountCustomerTypeId], [LeadId], [CustomerId], [AccountId]) VALUES ('MONI', @LeadID, @CustomerID, @AccountId);
--		INSERT INTO [dbo].[AE_CustomerAccounts] 
--			([LeadId], [AccountId], [CustomerId], [CustomerTypeId], [CreatedBy])
--		VALUES  (@LeadID, @AccountId, @CustomerID, 'PRI', @GPEmployeeId);

--		/** Create the MsAccount record. */
--		INSERT INTO [dbo].[MS_Accounts] (
--			[AccountID]
--			, PremiseAddressID
--			, [ModifiedBy]
--			, [CreatedBy]
--		) VALUES (
--			@AccountId
--			, @PremiseAddressId
--			, @GPEmployeeId
--			, @GPEmployeeId
--		);

--		INSERT INTO [dbo].[SAE_BillingInfoSummary](
--			[CustomerMasterFileId] ,
--			[AccountId]
--		) VALUES  (
--			@CMFID , -- CustomerMasterFileId - bigint
--			@AccountId -- AccountId - bigint
--		);
	
--		COMMIT TRANSACTION;
--	END TRY
--	BEGIN CATCH
--		ROLLBACK TRANSACTION;
--		EXEC dbo.wiseSP_ExceptionsThrown;
--		RETURN;
--	END CATCH

--	/** Return result. */
--	SELECT * FROM [dbo].[vwMS_AccountAndLeadInfo] WHERE AccountID = @AccountId;
--END
--GO

--GRANT EXEC ON dbo.custMS_AccountAndLeadInfoCreate TO PUBLIC
--GO

--/** EXEC dbo.custMS_AccountAndLeadInfoCreate 1020126, 'SOSA001'; 

--SELECT * FROM dbo.SAE_BillingInfoSummary WHERE CustomerMasterFileId = 3020173;

--SELECT * FROM dbo.MS_EmergencyContacts WHERE EmergencyContactID =  100215;
--*/