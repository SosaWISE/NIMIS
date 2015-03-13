USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerMasterFileGeneralSearch')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerMasterFileGeneralSearch'
		DROP  Procedure  dbo.custAE_CustomerMasterFileGeneralSearch
	END
GO

PRINT 'Creating Procedure custAE_CustomerMasterFileGeneralSearch'
GO
/******************************************************************************
**		File: custAE_CustomerMasterFileGeneralSearch.sql
**		Name: custAE_CustomerMasterFileGeneralSearch
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
**		Date: 03/14/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/14/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerMasterFileGeneralSearch
(
	@DealerId BIGINT = 5000
	, @City NVARCHAR(50) = NULL
	, @StateId VARCHAR(4) = NULL
	, @PostalCode VARCHAR(5) = NULL
	, @Email VARCHAR(256) = NULL
	, @FirstName NVARCHAR(50) = NULL
	, @LastName NVARCHAR(50) = NULL
	, @PhoneNumber VARCHAR(30) = NULL
	, @PageSize INT = 30
	, @PageNumber INT = 1
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Initialize. */
	SET @FirstName = REPLACE(@FirstName, '*', '%');
	SET @LastName = REPLACE(@LastName, '*', '%');
	SET @City = REPLACE(@City, '*', '%');

	BEGIN TRY
		/** Transfer data */
		;WITH TotalData AS (
		SELECT
			Data1.*
			, ROW_NUMBER() OVER(ORDER BY Data1.CustomerMasterFileID) AS RNum
		FROM
		(
		SELECT DISTINCT TOP 100
			CMF.CustomerMasterFileID
			, CST.CustomerID AS FkId
			, 'CUSTOMER' AS ResultType
			--, MCA.AccountID
			--, MCA.AccountTypeId
			--, MCAT.AccountTypeName
			, ICN.ICONS AS AccountTypes
			, [dbo].fxGetCustomerFullName('C', CST.Prefix, CST.FirstName, CST.MiddleName, CST.LastName, CST.Postfix) AS [Fullname]
			, [dbo].fxGetAddressCityStatePostalCode(ADRS1.City, ADRS1.StateId, ADRS1.PostalCode, ADRS1.PlusFour) AS [City]
			, [dbo].fxGetPhoneNumberByPriority(ISNULL(CST.PhoneHome, ADRS1.Phone), CST.PhoneWork, CST.PhoneMobile) AS [Phone]
			, CST.Email
		FROM
			[dbo].[AE_CustomerMasterFiles] AS CMF WITH (NOLOCK)
			INNER JOIN dbo.fxGetCustomerMasterIcons() AS ICN
			ON
				(ICN.CustomerMasterFileID = CMF.CustomerMasterFileID)
			-- Main McAccount Customer
			INNER JOIN [dbo].[AE_Customers] AS CST WITH (NOLOCK)
			ON
				(CST.CustomerMasterFileId = CMF.CustomerMasterFileID)
			INNER JOIN [dbo].[MC_Addresses] AS ADRS1 WITH (NOLOCK)
			ON
				(ADRS1.AddressID = CST.AddressId)
				AND (ADRS1.IsActive = 1 AND ADRS1.IsDeleted = 0)
			INNER JOIN [dbo].[AE_CustomerAccounts] AS ACA WITH (NOLOCK)
			ON
				(ACA.CustomerId = CST.CustomerID)
				AND (CST.IsActive = 1 AND CST.IsDeleted = 0)
			LEFT OUTER JOIN [dbo].[AE_CustomerAddress] AS ACAD WITH (NOLOCK)
			ON
				(ACAD.CustomerId = CST.CustomerID)
			LEFT OUTER JOIN [dbo].[MC_Addresses] AS ADRS WITH (NOLOCK)
			ON
				(ADRS.AddressID = ACAD.AddressId)
				AND (ADRS.IsActive = 1 AND ADRS.IsDeleted = 0)
			INNER JOIN [dbo].[MC_Accounts] AS MCA WITH (NOLOCK)
			ON
				(MCA.AccountID = ACA.AccountId)
			INNER JOIN [dbo].[MC_AccountTypes] AS MCAT WITH (NOLOCK)
			ON
				(MCAT.AccountTypeID = MCA.AccountTypeId)
			INNER JOIN [dbo].[AE_CustomerAccounts] AS MCAC WITH (NOLOCK)
			ON
				(MCAC.AccountId = MCA.AccountID)
		WHERE
			(CMF.DealerId = @DealerId)
			AND ((@City IS NULL OR ADRS1.City LIKE @City)
				AND (@StateId IS NULL OR ADRS1.StateId = @StateId)
				AND (@PostalCode IS NULL OR ADRS1.PostalCode = @PostalCode)
				AND (@Email IS NULL OR CST.Email = @Email)
				AND (@FirstName IS NULL OR CST.FirstName LIKE @FirstName)
				AND (@LastName IS NULL OR CST.LastName LIKE @LastName)
				AND (@PhoneNumber IS NULL 
					OR ISNULL(CST.PhoneHome, ADRS1.Phone) = @PhoneNumber
					OR CST.PhoneWork = @PhoneNumber
					OR CST.PhoneMobile = @PhoneNumber)
			)
	UNION
		SELECT
			LED.CustomerMasterFileId
			, LED.LeadID AS FkId
			, CAST('LEAD' AS VARCHAR(20)) AS ResultType
			--, CAST('Customer Lead' AS VARCHAR(50)) AS AccountTypeName
			, ';LEAD' AS AccountTypes
			, [dbo].fxGetCustomerFullName('L', LED.Salutation, LED.FirstName, LED.MiddleName, LED.LastName, LED.Suffix) AS [Fullname]
			, [dbo].fxGetAddressCityStatePostalCode(ADRS1.City, ADRS1.StateId, ADRS1.PostalCode, ADRS1.PlusFour) AS [City]
			, [dbo].fxGetPhoneNumberByPriority(ISNULL(LED.PhoneHome, ADRS1.Phone), LED.PhoneWork, LED.PhoneMobile) AS [Phone]
			, LED.Email
		FROM
			[dbo].[QL_Leads] AS LED WITH (NOLOCK)
			INNER JOIN [dbo].[QL_Address] AS ADRS1 WITH (NOLOCK)
			ON
				(ADRS1.AddressID = LED.AddressId)
		WHERE
				(LED.DealerId = @DealerId)
				-- Exclude all customers
				AND (LED.LeadID NOT IN (SELECT LeadId FROM [dbo].[AE_Customers]))
				AND ((@City IS NULL OR ADRS1.City LIKE @City)
					AND (@StateId IS NULL OR ADRS1.StateId = @StateId)
					AND (@PostalCode IS NULL OR ADRS1.PostalCode = @PostalCode)
					AND (@Email IS NULL OR LED.Email = @Email)
					AND (@FirstName IS NULL OR LED.FirstName LIKE @FirstName)
					AND (@LastName IS NULL OR LED.LastName LIKE @LastName)
					AND (@PhoneNumber IS NULL 
						OR ISNULL(LED.PhoneHome, ADRS1.Phone) = @PhoneNumber
						OR LED.PhoneWork = @PhoneNumber
						OR LED.PhoneMobile = @PhoneNumber)
				)
			) AS Data1
		)
		SELECT
			*
		FROM
			TotalData
		WHERE
			(RNum BETWEEN ((@PageNumber -1) * @PageSize + 1) AND (@PageNumber * @PageSize));

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerMasterFileGeneralSearch TO PUBLIC
GO

/** EXEC dbo.custAE_CustomerMasterFileGeneralSearch @PageSize=200, @PageNumber=2; */