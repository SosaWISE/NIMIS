USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountSearchAccounts')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountSearchAccounts'
		DROP  Procedure  dbo.custMS_AccountSearchAccounts
	END
GO

PRINT 'Creating Procedure custMS_AccountSearchAccounts'
GO
/******************************************************************************
**		File: custMS_AccountSearchAccounts.sql
**		Name: custMS_AccountSearchAccounts
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
**			
*******************************************************************************/
CREATE Procedure [dbo].[custMS_AccountSearchAccounts]
(
	@FirstName NVARCHAR(50) = NULL,
	@FirstNameExact BIT = 1,
	@LastName NVARCHAR(50) = NULL,
	@LastNameExact BIT = 1,
	@DevicePhone NVARCHAR(20) = NULL,
	@SimProductBarcodeId VARCHAR(50) = NULL,
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
	
	SELECT DISTINCT 
		ACT.AccountID
--		, ACT.GpsWatchPhoneNumber AS [DevicePhone]
		, CAST(NULL AS VARCHAR) AS [DevicePhone]
		, ACT.SimProductBarcodeId
		, dbo.fxFormatFullName(CUST.Prefix, CUST.FirstName, CUST.MiddleName, CUST.LastName, CUST.PostFix) AS Cust1FullName
		, CUST.Email
		, ADR.City
		, ADR.PostalCode
		, PS.StateAB
	FROM
		MS_Accounts AS ACT WITH (NOLOCK)
		INNER JOIN AE_Customers AS CUST WITH (NOLOCK)
		ON
			ACT.AccountID = CUST.CustomerID
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
		LEFT OUTER JOIN vwMS_IndustryAccountNumbers AS IAN WITH (NOLOCK)
		ON
			ACT.AccountID = IAN.AccountID
		LEFT OUTER JOIN MC_Addresses AS ADR WITH (NOLOCK)
		ON
			(CUST.AddressID = ADR.AddressID)
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
		--(@DevicePhone IS NULL OR ACT.GpsWatchPhoneNumber = @DevicePhone)
		(@SimProductBarcodeId IS NULL OR ACT.SimProductBarcodeId = @SimProductBarcodeId)
		AND (@DateOfBirth IS NULL OR CUST.DOB = @DateOfBirth)
		AND (@CSID IS NULL OR IAN.IndustryAccount = @CSID)
		AND (@Email IS NULL OR CUST.Email = @Email)
		AND (@Street IS NULL OR ADR.StreetAddress = @Street)
		AND (@City IS NULL OR ADR.City = @City)
		AND (@ZipCode IS NULL OR ADR.PostalCode = @ZipCode)
		AND (@StateAB IS NULL OR PS.StateAB = @StateAB)
	ORDER BY
		ACT.AccountID

END
GO

GRANT EXEC ON dbo.custMS_AccountSearchAccounts TO PUBLIC
GO