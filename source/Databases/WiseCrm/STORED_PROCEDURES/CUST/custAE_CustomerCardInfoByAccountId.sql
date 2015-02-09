USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerCardInfoByAccountId')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerCardInfoByAccountId'
		DROP  Procedure  dbo.custAE_CustomerCardInfoByAccountId
	END
GO

--PRINT 'Creating Procedure custAE_CustomerCardInfoByAccountId'
--GO
--/******************************************************************************
--**		File: custAE_CustomerCardInfoByAccountId.sql
--**		Name: custAE_CustomerCardInfoByAccountId
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
--**		Date: 06/03/2014
--*******************************************************************************
--**	Change History
--*******************************************************************************
--**	Date:		Author:			Description:
--**	-----------	---------------	-----------------------------------------------
--**	06/03/2014	Andres Sosa		Created By
--**	
--*******************************************************************************/
--CREATE Procedure dbo.custAE_CustomerCardInfoByAccountId
--(
--	@AccountId BIGINT
--)
--AS
--BEGIN
--	/** SET NO COUNTING */
--	SET NOCOUNT ON

--	/** DECLARATIONS */
--	DECLARE @CMFID BIGINT = NULL;

--	/** Find the customer by the AccountId */
--	SELECT
--		@CMFID = CST.CustomerMasterFileId
--	FROM
--		[dbo].[MS_AccountCustomers] AS MAC WITH (NOLOCK)
--		INNER JOIN [dbo].[AE_Customers] AS CST WITH (NOLOCK)
--		ON
--			(CST.CustomerID = MAC.CustomerId)
--	WHERE
--		(MAC.AccountId = @AccountId);

--	/** Check result */
--	IF (@CMFID IS NULL)
--	BEGIN
--		RETURN;
--	END

--	/** Check to see if this is a customer or a lead */
--	IF (EXISTS(SELECT * FROM [dbo].[AE_Customers] WHERE CustomerMasterFileId = @CMFID AND CustomerTypeId = 'PRI' AND IsDeleted = 0))
--	BEGIN
--		/** EXECUTE */
--		SELECT * FROM vwAE_CustomerCardInfo WHERE CustomerMasterFileID = @CMFID;
--	END
--	ELSE
--	BEGIN
--		SELECT 
--			LED.LeadID AS CustomerID
--			, 'LEAD' AS [ResultType]
--			, LED.CustomerMasterFileId
--			, LED.Salutation AS Prefix
--			, LED.FirstName
--			, LED.MiddleName
--			, LED.LastName
--			, LED.Suffix AS Postfix
--			, [dbo].[fxGetCustomerFullName] (NULL, LED.Salutation, LED.FirstName, LED.MiddleName, LED.LastName, LED.Suffix) AS FullName
--			, LED.Gender
--			, LED.PhoneHome
--			, LED.PhoneWork
--			, LED.PhoneMobile
--			, LED.Email
--			, LED.DOB
--			, LED.SSN
--			, NULL AS 'Username'
--			, ADR.AddressID
--			, ADR.StreetAddress
--			, ADR.StreetAddress2
--			, ADR.City
--			, ADR.StateId
--			, ADR.PostalCode
--			, ADR.PlusFour
--			, [dbo].[fxGetAddressCityStatePostalCode](ADR.City, ADR.StateId, ADR.PostalCode, ADR.PlusFour) AS [CityStateZip]
--		FROM
--			[dbo].[QL_Leads] AS LED WITH (NOLOCK)
--			INNER JOIN [dbo].[QL_Address] AS ADR WITH (NOLOCK)
--			ON
--				(ADR.AddressID = LED.AddressId)
--		WHERE
--			(LED.CustomerMasterFileId = @CMFID);
--	END		
--END
--GO

--GRANT EXEC ON dbo.custAE_CustomerCardInfoByAccountId TO PUBLIC
--GO

--/** EXEC dbo.custAE_CustomerCardInfoByAccountId 2234234 */