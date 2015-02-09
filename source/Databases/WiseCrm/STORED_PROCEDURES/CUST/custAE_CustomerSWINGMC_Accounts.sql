USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGMC_Accounts')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGMC_Accounts'
		DROP  Procedure  dbo.SPROC_NAME
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGMC_Accounts'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGMC_Accounts.sql
**		Name: custAE_CustomerSWINGMC_Accounts
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
**		Date: 03/28/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/28/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerSWINGMC_Accounts
(
	@InterimAccountID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/*step 3. Create the WCM.MC_Accounts
		*/

		INSERT INTO [WISE_CRM].[dbo].[MC_Accounts]
		(
		   [AccountTypeId]
		  ,[ShipContactId]
		  ,[ShipAddressId]
		  ,[DealerAccountId]
		  ,[ShipContactSameAsCustomer]
		  ,[ShipAddressSameAsCustomer]
		  ,[AccountName]
		  ,[AccountDesc]
		)
		SELECT
			'ALRM',
			NULL,
			NULL,
			NULL,
			1,
			1,
			([MC_Lead].FirstName + ' ' + [MC_Lead].LastName),
			'This is a swung account'
		FROM 
		[Platinum_Protection_InterimCRM].[dbo].[MS_Account]
		INNER JOIN
		[Platinum_Protection_InterimCRM].[dbo].[MC_Lead]
		ON
		[MS_Account].Customer1ID = [MC_Lead].LeadID
		WHERE 
		[MS_Account].AccountID = @InterimAccountID

		--SET @AccountID = SCOPE_IDENTITY()
		
		--PRINT CAST (@AccountID AS VARCHAR(10)) + '- AccountID'
		SELECT SCOPE_IDENTITY()




	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerSWINGMC_Accounts TO PUBLIC
GO

/** EXEC dbo.SPROC_NAME */