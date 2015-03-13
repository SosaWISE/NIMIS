USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_AddressGetPremiseByAccountId')
	BEGIN
		PRINT 'Dropping Procedure custMC_AddressGetPremiseByAccountId'
		DROP  Procedure  dbo.custMC_AddressGetPremiseByAccountId
	END
GO

PRINT 'Creating Procedure custMC_AddressGetPremiseByAccountId'
GO
/******************************************************************************
**		File: custMC_AddressGetPremiseByAccountId.sql
**		Name: custMC_AddressGetPremiseByAccountId
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
**		Date: 06/18/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/18/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMC_AddressGetPremiseByAccountId
(
	@AccountID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @AddressID BIGINT;
	
	BEGIN TRY
		/** Check that the Premis is assigned the new way. */
		IF(EXISTS(SELECT * FROM [dbo].[MS_Accounts] WHERE((AccountID = @AccountID) AND (PremiseAddressId IS NOT NULL))))
		BEGIN
			SELECT @AddressID = PremiseAddressId FROM [dbo].[MS_Accounts] WHERE (AccountID = @AccountID);
		END
		ELSE
		BEGIN
			SELECT TOP 1
				@AddressID = ADR.AddressID
			FROM
				[dbo].AE_CustomerAccounts AS MAC WITH (NOLOCK)
				INNER JOIN [dbo].[AE_Customers] AS CST WITH (NOLOCK)
				ON
					(CST.CustomerID = MAC.CustomerId)
					AND (MAC.CustomerTypeId IN ('MONI','PRI'))
				INNER JOIN [dbo].MC_Addresses AS ADR WITH (NOLOCK)
				ON
					(ADR.AddressID = CST.AddressId)
			WHERE
				(MAC.AccountId = @AccountID)
			ORDER BY
				MAC.CustomerTypeId -- MONI comes first

			/** SET it to the MS_Accounts table */
			UPDATE [dbo].[MS_Accounts] SET PremiseAddressId = @AddressID WHERE (AccountID = @AccountID);
		END

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** RETURN RESULT */
	SELECT * FROM [dbo].[vwMC_Addresses] WHERE AddressID = @AddressID;
END
GO

GRANT EXEC ON dbo.custMC_AddressGetPremiseByAccountId TO PUBLIC
GO

/** EXEC dbo.custMC_AddressGetPremiseByAccountId 181258 */