USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGGetInfo')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGGetInfo'
		DROP  Procedure  dbo.custAE_CustomerSWINGGetInfo
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGGetInfo'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGGetInfo.sql
**		Name: custAE_CustomerSWINGGetInfo
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
**	03/28/2014	Junryl/Reagan	Created By
**	
*******************************************************************************/
CREATE Procedure [dbo].[custAE_CustomerSWINGGetInfo]
(
	@InterimAccountID BIGINT
	, @CustomerType VARCHAR(3)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Return Customer Information of Customer 1 **/
		--PRINT @CustomerType + 'CUSTOMER TYPE'

	    IF @CustomerType = 'PRI'
		BEGIN

		    SELECT
					MC_Lead.Salutation
					, MC_Lead.FirstName
					, MC_Lead.MiddleName
					, MC_Lead.LastName
					, MC_Lead.Suffix
					, MC_Lead.SSN
					, MC_Lead.DOB
					, MC_Lead.Email		
	     	FROM
		    	[Platinum_Protection_InterimCRM].[dbo].[MC_Lead] MC_Lead 
			    INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MS_Account] MS_Account
		    	ON
			    	(MS_Account.Customer1ID = MC_Lead.LeadID)
			WHERE
				(MS_Account.AccountID = @InterimAccountID);
		END

		/** Return Customer Information of Customer 2 **/
	    IF @CustomerType = 'SEC'
		BEGIN

		    SELECT
				MC_Lead.Salutation,
				MC_Lead.FirstName,
				MC_Lead.MiddleName,
				MC_Lead.LastName,
				MC_Lead.Suffix,
				MC_Lead.SSN,
				MC_Lead.DOB,
				MC_Lead.Email	
	     	FROM
		    	[Platinum_Protection_InterimCRM].[dbo].[MC_Lead] MC_Lead
			    INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MS_Account] MS_Account
		    	ON
			    	(MS_Account.Customer2ID = MC_Lead.LeadID)
			WHERE
				(MS_Account.AccountID = @InterimAccountID);
		END

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END

GRANT EXEC ON dbo.custAE_CustomerSWINGGetInfo TO PUBLIC
GO

/** EXEC dbo.SPROC_NAME */
/** EXEC dbo.[custAE_CustomerSWINGGetInfo] 100000, 'PRI' */
/** EXEC dbo.[custAE_CustomerSWINGGetInfo] 100000, 'SEC' */


/**QUERIES NOTES 

SELECT TOP 10 * FROM [Platinum_Protection_InterimCRM].dbo.[MS_Account] 
SELECT TOP 10 * FROM [Platinum_Protection_InterimCRM].dbo.[MS_AccountSetupStatus] 
SELECT TOP 10 * FROM [Platinum_Protection_InterimCRM].dbo.[MC_Lead] 

**/