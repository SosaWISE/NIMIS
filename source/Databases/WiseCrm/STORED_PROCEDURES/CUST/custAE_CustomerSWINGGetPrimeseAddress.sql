USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGGetPrimeseAddress')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGGetPrimeseAddress'
		DROP  Procedure  dbo.custAE_CustomerSWINGGetPrimeseAddress
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGGetPrimeseAddress'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGGetPrimeseAddress.sql
**		Name: custAE_CustomerSWINGGetPrimeseAddress
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
**	04/22/2014	Junryl/Reagan		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerSWINGGetPrimeseAddress
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
	
		/** Return PremiseID by AccountID **/
		SELECT 
		    MC_Address.StreetAddress AS StreetAddress1		   
           ,MC_Address.StreetAddress2
           ,MC_Address.City
           ,MC_Address.County
           ,MC_Address.PostalCode
		   ,MC_PoliticalState.StateAB AS [State]	
		FROM
		[Platinum_Protection_InterimCRM].dbo.[MC_Address] AS MC_Address
		INNER JOIN [Platinum_Protection_InterimCRM].dbo.[Ms_Account] AS MS_Account
		ON
		   (MC_Address.AddressID = MS_Account.PremiseAddressID)

		INNER JOIN [Platinum_Protection_InterimCRM].dbo.[MC_PoliticalState] AS MC_PoliticalState
		ON
		   (MC_Address.StateID = MC_PoliticalState.StateID)

		WHERE
		   (MS_Account.AccountID = @InterimAccountID);
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerSWINGGetPrimeseAddress TO PUBLIC
GO

/** EXEC dbo.custAE_CustomerSWINGGetPrimeseAddress 100000 */

/**QUERIES NOTES 

SELECT TOP 10 * FROM [Platinum_Protection_InterimCRM].dbo.[MS_Account]  
SELECT TOP 10 * FROM [Platinum_Protection_InterimCRM].dbo.[MC_Address] 
SELECT TOP 10 * FROM [Platinum_Protection_InterimCRM].dbo.[MC_PoliticalState] 

**/
