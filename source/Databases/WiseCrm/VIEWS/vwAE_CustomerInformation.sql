USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerInformation')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerInformation'
		DROP VIEW dbo.vwAE_CustomerInformation
	END
GO

PRINT 'Creating VIEW vwAE_CustomerInformation'
GO

/****** Object:  View [dbo].[vwAE_CustomerInformation]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerInformation.sql
**		Name: vwAE_CustomerInformation
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
**		Auth: Todd Carlson
**		Date: 
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	06/28/2010	Andres Sosa		Created
**	07/08/2014	Andres Sosa		Added Credit report with score and group.
*******************************************************************************/
CREATE View [dbo].[vwAE_CustomerInformation] AS

	SELECT
		CUST.CustomerID
		, CUST.CustomerMasterFileId
		, CUST.LeadId
		, MCAS.DealerAccountId
		, CSTT.CustomerType
		, CUST.DealerId
		, CUST.AddressId
		, CUST.Prefix
		, CUST.FirstName
		, CUST.MiddleName
		, CUST.LastName
		, CUST.Postfix
		, CASE
			WHEN CUST.Prefix IS NULL THEN ''
			ELSE CUST.Prefix + ' '
		  END +
		  CUST.FirstName + ' ' +
		  CASE 
			WHEN CUST.MiddleName IS NULL THEN ''
			ELSE CUST.MiddleName + ' '
		  END +
		  CUST.LastName +
		  CASE
			WHEN CUST.Postfix IS NULL THEN ''
			ELSE ' ' + CUST.Postfix
		  END AS FullName
		, ADR.StreetAddress
		, ADR.City
		, ADR.PostalCode
		, PS.StateAB AS [State]
		, CUST.PhoneHome
		, CUST.PhoneWork
		, CUST.PhoneMobile
		, CUST.Email
		, CUST.DOB
		, CUST.SSN
		, MAS.SimProductBarcodeId
		, IND.IndustryAccountID
		, IND.Csid
	FROM
		AE_Customers AS CUST WITH (NOLOCK)
		INNER JOIN AE_CustomerTypes AS CSTT WITH (NOLOCK)
		ON
			(CUST.CustomerTypeId = CSTT.CustomerTypeID)
		LEFT OUTER JOIN MC_Accounts AS MCAS WITH (NOLOCK)
		ON
			(CUST.CustomerID = MCAS.AccountID)
		LEFT OUTER JOIN MS_Accounts AS MAS WITH (NOLOCK)
		ON
			(CUST.CustomerID = MAS.AccountID)
		LEFT OUTER JOIN MS_IndustryAccounts AS IND WITH (NOLOCK)
		ON
			(MAS.IndustryAccountId = IND.IndustryAccountID)
		LEFT OUTER JOIN MC_Addresses AS ADR WITH (NOLOCK)
		ON
			(Cust.AddressId = ADR.AddressID)
		LEFT OUTER JOIN dbo.MC_PoliticalStates AS PS WITH (NOLOCK)
		ON
			(ADR.StateId = PS.StateID)
	WHERE
		((CUST.IsActive IS NULL OR CUST.IsActive = 1) AND (CUST.IsDeleted IS NULL OR CUST.IsDeleted = 0))
		AND ((MAS.IsActive IS NULL OR MAS.IsActive = 1) AND (MAS.IsDeleted IS NULL OR MAS.IsDeleted = 0))
		AND ((IND.IsActive IS NULL OR IND.IsActive = 1) AND (IND.IsDeleted IS NULL OR IND.IsDeleted = 0))


GO


