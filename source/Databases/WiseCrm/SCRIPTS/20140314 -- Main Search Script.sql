USE [WISE_CRM]
GO

/** Arguments */
DECLARE @DealerId BIGINT = 5000
	, @City NVARCHAR(50) = NULL
	, @StateId VARCHAR(4) = NULL
	, @PostalCode VARCHAR(5) = NULL
	, @Email VARCHAR(256) = NULL
	, @FirstName NVARCHAR(50) = 'B*'
	, @LastName NVARCHAR(50) = NULL
	, @PhoneNumber VARCHAR(30) = NULL;

/** Initialize. */
SET @FirstName = REPLACE(@FirstName, '*', '%');
SET @LastName = REPLACE(@LastName, '*', '%');
SET @City = REPLACE(@City, '*', '%');


--SELECT DISTINCT TOP 100
--	CMF.CustomerMasterFileID
--	, MCA.AccountID
--	, MCA.AccountTypeId
--	, MCAT.AccountTypeName
--	, [dbo].fxGetCustomerFullName('C', CST.Prefix, CST.FirstName, CST.MiddleName, CST.LastName, CST.Postfix) AS [Fullname]
--	, [dbo].fxGetAddressCityStatePostalCode(ADRS1.City, ADRS1.StateId, ADRS1.PostalCode, ADRS1.PlusFour) AS [City]
--	, [dbo].fxGetPhoneNumberByPriority(CST.PhoneHome, CST.PhoneWork, CST.PhoneMobile) AS [Phone]
--	, CST.Email

--FROM
--	[dbo].[AE_CustomerMasterFiles] AS CMF WITH (NOLOCK)
--	-- Main McAccount Customer
--	INNER JOIN [dbo].[AE_Customers] AS CST WITH (NOLOCK)
--	ON
--		(CST.CustomerMasterFileId = CMF.CustomerMasterFileID)
--	INNER JOIN [dbo].[MC_Addresses] AS ADRS1 WITH (NOLOCK)
--	ON
--		(ADRS1.AddressID = CST.AddressId)
--		AND (ADRS1.IsActive = 1 AND ADRS1.IsDeleted = 0)
--	INNER JOIN [dbo].[AE_CustomerAccounts] AS ACA WITH (NOLOCK)
--	ON
--		(ACA.CustomerId = CST.CustomerID)
--		AND (CST.IsActive = 1 AND CST.IsDeleted = 0)
--	LEFT OUTER JOIN [dbo].[AE_CustomerAddress] AS ACAD WITH (NOLOCK)
--	ON
--		(ACAD.CustomerId = CST.CustomerID)
--	LEFT OUTER JOIN [dbo].[MC_Addresses] AS ADRS WITH (NOLOCK)
--	ON
--		(ADRS.AddressID = ACAD.AddressId)
--		AND (ADRS.IsActive = 1 AND ADRS.IsDeleted = 0)
--	INNER JOIN [dbo].[MC_Accounts] AS MCA WITH (NOLOCK)
--	ON
--		(MCA.AccountID = ACA.AccountId)
--	INNER JOIN [dbo].[MC_AccountTypes] AS MCAT WITH (NOLOCK)
--	ON
--		(MCAT.AccountTypeID = MCA.AccountTypeId)
--	INNER JOIN [dbo].[MC_AccountCustomers] AS MCAC WITH (NOLOCK)
--	ON
--		(MCAC.AccountId = MCA.AccountID)
--WHERE
--	(CMF.DealerId = @DealerId)
--	AND ((@City IS NULL OR ADRS1.City LIKE @City)
--		AND (@StateId IS NULL OR ADRS1.StateId = @StateId)
--		AND (@PostalCode IS NULL OR ADRS1.PostalCode = @PostalCode)
--		AND (@Email IS NULL OR CST.Email = @Email)
--		AND (@FirstName IS NULL OR CST.FirstName LIKE @FirstName)
--		AND (@LastName IS NULL OR CST.LastName LIKE @LastName)
--		AND (@PhoneNumber IS NULL 
--			OR CST.PhoneHome = @PhoneNumber
--			OR CST.PhoneWork = @PhoneNumber
--			OR CST.PhoneMobile = @PhoneNumber)
--	);

--SELECT
--	LED.CustomerMasterFileId
--	, LED.LeadID AS AccountID
--	, CAST('LEAD' AS VARCHAR(20)) AS AccountTypeId
--	, CAST('Customer Lead' AS VARCHAR(50)) AS AccountTypeName
--	, [dbo].fxGetCustomerFullName('L', LED.Salutation, LED.FirstName, LED.MiddleName, LED.LastName, LED.Suffix) AS [Fullname]
--	, [dbo].fxGetAddressCityStatePostalCode(ADRS1.City, ADRS1.StateId, ADRS1.PostalCode, ADRS1.PlusFour) AS [City]
--	, [dbo].fxGetPhoneNumberByPriority(LED.PhoneHome, LED.PhoneWork, LED.PhoneMobile) AS [Phone]
--	, LED.Email
--FROM
--	[dbo].[QL_Leads] AS LED WITH (NOLOCK)
--	INNER JOIN [dbo].[QL_Address] AS ADRS1 WITH (NOLOCK)
--	ON
--		(ADRS1.AddressID = LED.AddressId)
--WHERE
--		(LED.DealerId = @DealerId)
--		AND ((@City IS NULL OR ADRS1.City LIKE @City)
--			AND (@StateId IS NULL OR ADRS1.StateId = @StateId)
--			AND (@PostalCode IS NULL OR ADRS1.PostalCode = @PostalCode)
--			AND (@Email IS NULL OR LED.Email = @Email)
--			AND (@FirstName IS NULL OR LED.FirstName LIKE @FirstName)
--			AND (@LastName IS NULL OR LED.LastName LIKE @LastName)
--			AND (@PhoneNumber IS NULL 
--				OR LED.PhoneHome = @PhoneNumber
--				OR LED.PhoneWork = @PhoneNumber
--				OR LED.PhoneMobile = @PhoneNumber)
--		);
--		SELECT * FROM QL_Leads WHERE LeadID = 1020130;

/**
I would like to figure out how to show in one row all the accounts that a Master Account may have.
So given the following:
	CMFID
Show the following:

	CMFID | Account Types (as html fonts ) | 

*/
SELECT
	-- CustomerMasterFileID, 'ALRM' AS [ALRM], 'INSEC' AS [INSEC]
	PVT.CustomerMasterFileID
	, CASE 
		WHEN PVT.ALRM > 0 THEN ' &#xe007;' -- Alarm System
		ELSE ''
	  END +
	  CASE
		WHEN PVT.INSEC > 0 THEN ' &#xe000;'  -- Internet Security
		ELSE ''
	  END +
	  CASE
		WHEN PVT.LFLCK > 0 THEN ' &#xe003;'  -- Life Lock
		ELSE ''
	  END +
	  CASE
		WHEN PVT.NUMAN > 0 THEN ' &#xe01c;'  -- Nu Mana
		ELSE ''
	  END +
	  CASE
		WHEN PVT.PERS > 0 THEN ' &#xe002;'  -- Nu Mana
		ELSE ''
	  END +
	  CASE
		WHEN PVT.SKPLT > 0 THEN ' &#xe01a;'  -- Strike Plate
		ELSE ''
	  END +
	  CASE
		WHEN PVT.WNFIL > 0 THEN ' &#xe00e;'  -- Window Film
		ELSE ''
	  END
	 AS [ICONS] 
	-- , PVT.*
FROM
(SELECT
	MCA.AccountID
	, MCA.AccountTypeId
	, CMF.CustomerMasterFileID
FROM
	[dbo].[AE_CustomerMasterFiles] AS CMF WITH (NOLOCK)
	INNER JOIN [dbo].[AE_Customers] AS CST WITH (NOLOCK)
	ON
		(CST.CustomerMasterFileId = CMF.CustomerMasterFileID)
	INNER JOIN [dbo].[AE_CustomerAccounts] AS ACA WITH (NOLOCK)
	ON
		(ACA.CustomerId = CST.CustomerID)
	INNER JOIN [dbo].[MC_Accounts] AS MCA WITH (NOLOCK)
	ON
		(MCA.AccountID = ACA.AccountId)) AS MST
PIVOT
(
	COUNT(AccountID)
	FOR AccountTypeId IN ([ALRM], [INSEC], [LFLCK], [NUMAN], [PERS], [SKPLT], [WNFIL]) 
) AS PVT
ORDER BY
	PVT.CustomerMasterFileID;