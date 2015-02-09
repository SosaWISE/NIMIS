USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerMasterFileSearchCustomers')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerMasterFileSearchCustomers'
		DROP  Procedure  dbo.custAE_CustomerMasterFileSearchCustomers
	END
GO

PRINT 'Creating Procedure custAE_CustomerMasterFileSearchCustomers'
GO
/******************************************************************************
**		File: custAE_CustomerMasterFileSearchCustomers.sql
**		Name: custAE_CustomerMasterFileSearchCustomers
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
**		Auth: Andres E. Sosa
**		Date: 01/10/2011
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/10/2011	Andres E. Sosa	Created By
**	SELECT * FROM EXEC [dbo].[custAE_CustomerMasterFileSearchCustomers] 'Andres', 1, NULL, 1, NULL
*******************************************************************************/
CREATE Procedure [dbo].[custAE_CustomerMasterFileSearchCustomers]
(
	@FirstName NVARCHAR(50) = NULL,
	@FirstNameExact BIT = 1,
	@LastName NVARCHAR(50) = NULL,
	@LastNameExact BIT = 1,
	@PremisePhone VARCHAR(50) = NULL,
	@DateOfBirth DATETIME = NULL,
	@CSID NVARCHAR(20) = NULL,
	@Email NVARCHAR(50) = NULL,
	@Street NVARCHAR(50) = NULL,
	@StreetExact BIT = 1,
	@City NVARCHAR(50) = NULL,
	@ZipCode NVARCHAR(5) = NULL,
	@StateAB NVARCHAR(2) = NULL
)
AS
BEGIN
	/* Local Declarations*/	
	DECLARE @CustTable AS TABLE (CustomerMasterFileID BIGINT)

	/** Create a list of already existing customers to remove from the leads table */
	INSERT INTO @CustTable
	SELECT
		CMF.CustomerMasterFileID
	FROM
		dbo.AE_CustomerMasterFiles AS CMF WITH (NOLOCK)
		INNER JOIN AE_Customers AS CUST WITH (NOLOCK)
		ON 
			(CMF.CustomerMasterFileID = CUST.CustomerMasterFileId)
			AND
			(
				(@FirstName IS NULL)
				OR (@FirstNameExact = 1 AND CUST.FirstName = @FirstName) 
				OR (COALESCE(@FirstNameExact, 0) = 0 AND CUST.FirstName LIKE @FirstName)
			)		
			AND
			(
				(@LastName IS NULL)
				OR (@LastNameExact = 1 AND CUST.LastName = @LastName) 
				OR (COALESCE(@LastNameExact, 0) = 0 AND CUST.LastName LIKE @LastName)
			)
		LEFT OUTER JOIN dbo.AE_CustomerAccounts AS CA WITH (NOLOCK)
		ON
			(CUST.CustomerID = CA.CustomerId)
		LEFT OUTER JOIN vwMS_IndustryAccountNumbers AS IAN WITH (NOLOCK)
		ON
			CA.AccountID = IAN.AccountID
		LEFT OUTER JOIN dbo.AE_CustomerAddress AS ACA WITH (NOLOCK)
		ON
			(CA.CustomerId = ACA.CustomerId)
		LEFT OUTER JOIN dbo.MC_Address AS ADR WITH (NOLOCK)
		ON
			(ACA.AddressId = ADR.AddressID)
			AND
			(
				(@Street IS NULL)
				OR (@StreetExact = 1 AND ADR.StreetAddress = @Street) 
				OR (COALESCE(@StreetExact, 0) = 0 AND ADR.StreetAddress LIKE @Street)
			)
		LEFT OUTER JOIN MC_PoliticalStates AS PS WITH (NOLOCK)
		ON
			(ADR.StateID = PS.StateID)
	WHERE
		(@PremisePhone IS NULL OR ADR.Phone = @PremisePhone)
		-- AND (@SimProductBarcodeId IS NULL OR ACT.SimProductBarcodeId = @SimProductBarcodeId)
		AND (@DateOfBirth IS NULL OR CUST.DOB = @DateOfBirth)
		AND (@CSID IS NULL OR IAN.IndustryNumber = @CSID)
		AND (@Email IS NULL OR CUST.Email = @Email)
		-- AND (@Street IS NULL OR ADR.StreetAddress = @Street)
		AND (@City IS NULL OR ADR.City = @City)
		AND (@ZipCode IS NULL OR ADR.PostalCode = @ZipCode)
		AND (@StateAB IS NULL OR PS.StateAB = @StateAB)

	ORDER BY
		CMF.CustomerMasterFileID
		
	/** Return output */
	SELECT
		CMF.CustomerMasterFileID
		, ADR.Phone AS [PremisePhone]
		, dbo.fxFormatFullName(CUST.Salutation, CUST.FirstName, CUST.MiddleName, CUST.LastName, CUST.Suffix) AS Cust1FullName
		, CUST.FirstName + ' ' + CUST.LastName AS Customer2Name
		, ADR.StreetAddress
		, CUST.Email
		, ADR.City
		, ADR.PostalCode
		, PS.StateAB
	FROM
		dbo.AE_CustomerMasterFiles AS CMF WITH (NOLOCK)
		INNER JOIN QL_Leads AS CUST WITH (NOLOCK)
		ON 
			(CMF.CustomerMasterFileID = CUST.CustomerMasterFileId)
			AND
			(
				(@FirstName IS NULL)
				OR (@FirstNameExact = 1 AND CUST.FirstName = @FirstName) 
				OR (COALESCE(@FirstNameExact, 0) = 0 AND CUST.FirstName LIKE @FirstName)
			)		
			AND
			(
				(@LastName IS NULL)
				OR (@LastNameExact = 1 AND CUST.LastName = @LastName) 
				OR (COALESCE(@LastNameExact, 0) = 0 AND CUST.LastName LIKE @LastName)
			)
		LEFT OUTER JOIN dbo.QL_CustomerLeads AS CL WITH (NOLOCK)
		ON
			(CUST.LeadID = CL.LeadId)
		LEFT OUTER JOIN vwMS_IndustryAccountNumbers AS IAN WITH (NOLOCK)
		ON
			CMF.CustomerMasterFileID = IAN.CustomerMasterFileId
		LEFT OUTER JOIN dbo.QL_LeadAddress AS ACL WITH (NOLOCK)
		ON
			(CL.LeadId = ACL.LeadId)
		LEFT OUTER JOIN dbo.QL_Address AS ADR WITH (NOLOCK)
		ON
			(ACL.AddressId = ADR.AddressID)
			AND
			(
				(@Street IS NULL)
				OR (@StreetExact = 1 AND ADR.StreetAddress = @Street) 
				OR (COALESCE(@StreetExact, 0) = 0 AND ADR.StreetAddress LIKE @Street)
			)
		LEFT OUTER JOIN MC_PoliticalStates AS PS WITH (NOLOCK)
		ON
			(ADR.StateID = PS.StateID)
	WHERE
		(CMF.CustomerMasterFileID NOT IN (SELECT CustomerMasterFileID FROM @CustTable))
		AND (@PremisePhone IS NULL OR ADR.Phone = @PremisePhone)
		-- AND (@SimProductBarcodeId IS NULL OR ACT.SimProductBarcodeId = @SimProductBarcodeId)
		AND (@DateOfBirth IS NULL OR CUST.DOB = @DateOfBirth)
		AND (@CSID IS NULL OR IAN.IndustryNumber = @CSID)
		AND (@Email IS NULL OR CUST.Email = @Email)
		-- AND (@Street IS NULL OR ADR.StreetAddress = @Street)
		AND (@City IS NULL OR ADR.City = @City)
		AND (@ZipCode IS NULL OR ADR.PostalCode = @ZipCode)
		AND (@StateAB IS NULL OR PS.StateAB = @StateAB)
	UNION 
	SELECT
		CMF.CustomerMasterFileID
		, ADR.Phone AS [PremisePhone]
		, dbo.fxFormatFullName(CUST.Salutation, CUST.FirstName, CUST.MiddleName, CUST.LastName, CUST.Suffix) AS Cust1FullName
		, CUST.FirstName + ' ' + CUST.LastName AS Customer2Name
		, ADR.StreetAddress
		, CUST.Email
		, ADR.City
		, ADR.PostalCode
		, PS.StateAB
	FROM
		dbo.AE_CustomerMasterFiles AS CMF WITH (NOLOCK)
		INNER JOIN AE_Customers AS CUST WITH (NOLOCK)
		ON 
			(CMF.CustomerMasterFileID = CUST.CustomerMasterFileId)
			AND
			(
				(@FirstName IS NULL)
				OR (@FirstNameExact = 1 AND CUST.FirstName = @FirstName) 
				OR (COALESCE(@FirstNameExact, 0) = 0 AND CUST.FirstName LIKE @FirstName)
			)		
			AND
			(
				(@LastName IS NULL)
				OR (@LastNameExact = 1 AND CUST.LastName = @LastName) 
				OR (COALESCE(@LastNameExact, 0) = 0 AND CUST.LastName LIKE @LastName)
			)
		LEFT OUTER JOIN dbo.AE_CustomerAccounts AS CA WITH (NOLOCK)
		ON
			(CUST.CustomerID = CA.CustomerId)
		LEFT OUTER JOIN vwMS_IndustryAccountNumbers AS IAN WITH (NOLOCK)
		ON
			CA.AccountID = IAN.AccountID
		LEFT OUTER JOIN dbo.AE_CustomerAddress AS ACA WITH (NOLOCK)
		ON
			(CA.CustomerId = ACA.CustomerId)
		LEFT OUTER JOIN dbo.MC_Address AS ADR WITH (NOLOCK)
		ON
			(ACA.AddressId = ADR.AddressID)
			AND
			(
				(@Street IS NULL)
				OR (@StreetExact = 1 AND ADR.StreetAddress = @Street) 
				OR (COALESCE(@StreetExact, 0) = 0 AND ADR.StreetAddress LIKE @Street)
			)
		LEFT OUTER JOIN MC_PoliticalStates AS PS WITH (NOLOCK)
		ON
			(ADR.StateID = PS.StateID)
	WHERE
		(@PremisePhone IS NULL OR ADR.Phone = @PremisePhone)
		-- AND (@SimProductBarcodeId IS NULL OR ACT.SimProductBarcodeId = @SimProductBarcodeId)
		AND (@DateOfBirth IS NULL OR CUST.DOB = @DateOfBirth)
		AND (@CSID IS NULL OR IAN.IndustryNumber = @CSID)
		AND (@Email IS NULL OR CUST.Email = @Email)
		-- AND (@Street IS NULL OR ADR.StreetAddress = @Street)
		AND (@City IS NULL OR ADR.City = @City)
		AND (@ZipCode IS NULL OR ADR.PostalCode = @ZipCode)
		AND (@StateAB IS NULL OR PS.StateAB = @StateAB)

	ORDER BY
		CMF.CustomerMasterFileID

END
GO

GRANT EXEC ON dbo.custAE_CustomerMasterFileSearchCustomers TO PUBLIC
GO