USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'wiseMC_ChangeSalesRepIdOnAccounts')
	BEGIN
		PRINT 'Dropping Procedure wiseMC_ChangeSalesRepIdOnAccounts'
		DROP  Procedure  dbo.wiseMC_ChangeSalesRepIdOnAccounts
	END
GO

PRINT 'Creating Procedure wiseMC_ChangeSalesRepIdOnAccounts'
GO
/******************************************************************************
**		File: wiseMC_ChangeSalesRepIdOnAccounts.sql
**		Name: wiseMC_ChangeSalesRepIdOnAccounts
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
**		Date: 06/11/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/11/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.wiseMC_ChangeSalesRepIdOnAccounts
(
	@SalesRepID VARCHAR(50) = 'FLFA000'
	, @CMFID BIGINT = 3091655
	, @FullName VARCHAR(100)
	, @DealerID INT = 5003
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @LeadID BIGINT
		, @CustomerID BIGINT
		, @CustomerName VARCHAR(100)
		, @AccountID BIGINT;

	/** CHECK THAT THIS IS A LEGIT REP ID. */
	IF(NOT EXISTS (SELECT * FROM [WISE_HumanResource].[dbo].RU_Users WHERE GPEmployeeId = @SalesRepID))
	BEGIN
		PRINT 'THAT REP DOES NOT EXISTS...';
		RETURN;
	END
	
	BEGIN TRY
		BEGIN TRANSACTION

		SELECT @LeadID = LeadID FROM dbo.QL_Leads WHERE (CustomerMasterFileId = @CMFID);
		SELECT DISTINCT @AccountID = AccountID, @CustomerID = CustomerID FROM dbo.AE_CustomerAccounts WHERE (LeadId = @LeadID);
		PRINT 'LeadID: ' + CAST(@LeadID AS VARCHAR) + ' | AccountID: ' + CAST(@AccountID AS VARCHAR)+ ' | CustomerID: ' + CAST(@CustomerID AS VARCHAR);

		/** Check to see that the name is OK. */
		IF (NOT EXISTS(SELECT * FROM dbo.AE_Customers WHERE (FirstName + ' ' + LastName = @FullName) AND (CustomerID = @CustomerID)))
		BEGIN
			SELECT @CustomerName = FirstName + ' ' + LastName FROM dbo.AE_Customers WHERE (CustomerID = @CustomerID)
			PRINT 'The name passed "' + @FullName + '" does not match the customer name "' + @CustomerName + '"';
			RETURN;
		END

		UPDATE dbo.QL_Leads SET SalesRepId = @SalesRepID, DealerId = @DealerID WHERE (LeadID = @LeadID);
		UPDATE dbo.MS_AccountSalesInformations SET SalesRepId = @SalesRepID WHERE (AccountID = @AccountID);
		UPDATE ADR SET 
			ADR.SalesRepId = @SalesRepID
			, ADR.DealerId = @DealerID
		FROM
			dbo.QL_Address AS ADR WITH (NOLOCK)
			INNER JOIN dbo.QL_Leads AS QLD WITH (NOLOCK)
			ON
				(QLD.AddressId = ADR.AddressID)
		WHERE
			(QLD.LeadID = @LeadID);
		UPDATE dbo.AE_Customers SET DealerId = @DealerID WHERE (CustomerID = @CustomerID);
		UPDATE MCAD SET
			MCAD.DealerId = @DealerID
		FROM
			dbo.MC_Addresses AS MCAD WITH (NOLOCK)
			INNER JOIN dbo.AE_Customers AS AEC WITH (NOLOCK)
			ON
				(AEC.AddressId = MCAD.AddressID)
		WHERE
			(AEC.CustomerID = @CustomerID);

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.wiseMC_ChangeSalesRepIdOnAccounts TO PUBLIC
GO

/** EXEC dbo.wiseMC_ChangeSalesRepIdOnAccounts 'CARTB001', 3091669, 'Little Bethel COGIC Little Bethel COGIC', 5000;  */