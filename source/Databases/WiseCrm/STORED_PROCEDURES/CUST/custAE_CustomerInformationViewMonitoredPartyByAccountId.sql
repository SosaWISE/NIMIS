USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerInformationViewMonitoredPartyByAccountId')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerInformationViewMonitoredPartyByAccountId'
		DROP  Procedure  dbo.custAE_CustomerInformationViewMonitoredPartyByAccountId
	END
GO

PRINT 'Creating Procedure custAE_CustomerInformationViewMonitoredPartyByAccountId'
GO
/******************************************************************************
**		File: custAE_CustomerInformationViewMonitoredPartyByAccountId.sql
**		Name: custAE_CustomerInformationViewMonitoredPartyByAccountId
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
CREATE Procedure dbo.custAE_CustomerInformationViewMonitoredPartyByAccountId
(
	@AccountId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @CustomerID BIGINT;
	
	BEGIN TRY
		SELECT TOP 1
			@CustomerID = MAC.CustomerId
		FROM
			[dbo].[AE_CustomerAccounts] AS MAC WITH (NOLOCK)
			INNER JOIN [dbo].[AE_Customers] AS CST WITH (NOLOCK)
			ON
				(CST.CustomerID = MAC.CustomerId)
				AND (MAC.CustomerTypeId IN ('MONI','PRI'))
			INNER JOIN [dbo].MC_Addresses AS ADR WITH (NOLOCK)
			ON
				(ADR.AddressID = CST.AddressId)
		WHERE
			(MAC.AccountId = @AccountId)
		ORDER BY
			MAC.CustomerTypeId -- MONI comes first
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** RETURN RESULT */
	SELECT * FROM [dbo].[vwAE_CustomerInformation] WHERE CustomerID = @CustomerID;
END
GO

GRANT EXEC ON dbo.custAE_CustomerInformationViewMonitoredPartyByAccountId TO PUBLIC
GO

/** EXEC dbo.custAE_CustomerInformationViewMonitoredPartyByAccountId 181258 */