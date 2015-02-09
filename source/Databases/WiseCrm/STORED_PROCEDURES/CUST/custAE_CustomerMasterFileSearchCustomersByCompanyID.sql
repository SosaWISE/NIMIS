USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerMasterFileSearchCustomersByCompanyID')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerMasterFileSearchCustomersByCompanyID'
		DROP  Procedure  dbo.custAE_CustomerMasterFileSearchCustomersByCompanyID
	END
GO

PRINT 'Creating Procedure custAE_CustomerMasterFileSearchCustomersByCompanyID'
GO
/******************************************************************************
**		File: custAE_CustomerMasterFileSearchCustomersByCompanyID.sql
**		Name: custAE_CustomerMasterFileSearchCustomersByCompanyID
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
**		Date: 04/18/2011
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	04/18/2011	Andres E. Sosa	Created By
**	EXEC [dbo].[custAE_CustomerMasterFileSearchCustomersByCompanyID] 'SOSA001'
*******************************************************************************/
CREATE Procedure [dbo].[custAE_CustomerMasterFileSearchCustomersByCompanyID]
(
	@CompanyID VARCHAR(10)
)
AS
BEGIN
	/** Local Declarations */
	DECLARE @CustTable AS TABLE (CustomerMasterFileID BIGINT)
	
	/** Query */
	INSERT INTO @CustTable
	SELECT CustomerMasterFileID FROM [dbo].[vwAE_CustomerSalesRepStats] WHERE (SalesRepID = @CompanyID OR TechnicianID = @CompanyID)
	
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
		LEFT OUTER JOIN MC_PoliticalStates AS PS WITH (NOLOCK)
		ON
			(ADR.StateID = PS.StateID)
	WHERE
		(CMF.CustomerMasterFileID NOT IN (SELECT CustomerMasterFileID FROM @CustTable))
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
		LEFT OUTER JOIN MC_PoliticalStates AS PS WITH (NOLOCK)
		ON
			(ADR.StateID = PS.StateID)
	WHERE
		(CMF.CustomerMasterFileID NOT IN (SELECT CustomerMasterFileID FROM @CustTable))

	ORDER BY
		CMF.CustomerMasterFileID
	
	
END
GO

GRANT EXEC ON dbo.custAE_CustomerMasterFileSearchCustomersByCompanyID TO PUBLIC
GO