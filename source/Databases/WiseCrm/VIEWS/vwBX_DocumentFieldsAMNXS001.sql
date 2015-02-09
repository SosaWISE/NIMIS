USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwBX_DocumentFieldsAMNXS001')
	BEGIN
		PRINT 'Dropping VIEW vwBX_DocumentFieldsAMNXS001'
		DROP VIEW dbo.vwBX_DocumentFieldsAMNXS001
	END
GO

PRINT 'Creating VIEW vwBX_DocumentFieldsAMNXS001'
GO

/****** Object:  View [dbo].[vwBX_DocumentFieldsAMNXS001]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwBX_DocumentFieldsAMNXS001.sql
**		Name: vwBX_DocumentFieldsAMNXS001
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 09/09/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	09/09/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwBX_DocumentFieldsAMNXS001]
AS
	-- Enter Query here
	SELECT
		CUST1.CustomerMasterFileId
		, MSA.AccountID
		, ABC.BillingCustomerID
		, CUST1.CustomerID AS CustomerOwnerID
		, CUST2.CustomerID AS CustomerSpouseID
		, CAST(CUST1.CustomerMasterFileId AS VARCHAR) + CAST(MSA.AccountID AS VARCHAR) AS AccountNumber
		, dbo.fxGetCustomerPreFirstNameMiddleName(CUST1.PreFix, CUST1.FirstName, CUST1.MiddleName) AS OwnerFirstName
		, dbo.fxGetCustomerLastNamePost(CUST1.LastName, CUST1.Postfix) AS OwnerLastName
		, dbo.fxGetCustomerPreFirstNameMiddleName(CUST2.PreFix, CUST2.FirstName, CUST2.MiddleName) AS SpouseFirstName
		, dbo.fxGetCustomerLastNamePost(CUST2.LastName, CUST2.Postfix) AS SpouseLastName
		, REPLICATE('0', 2-LEN(DATEPART(MM, CNTRC.EffectiveDate))) + CAST (DATEPART(MM, CNTRC.EffectiveDate) AS VARCHAR) AS EffDateMM
		, REPLICATE('0', 2-LEN(DATEPART(DD, CNTRC.EffectiveDate))) + CAST (DATEPART(DD, CNTRC.EffectiveDate) AS VARCHAR) AS EffDateDD
		, REPLICATE('0', 4-LEN(DATEPART(YYYY, CNTRC.EffectiveDate))) + CAST (DATEPART(YYYY, CNTRC.EffectiveDate) AS VARCHAR) AS EffDateYY
		, CASE
			WHEN dbo.fxGetExtendedServiceOption(MSA.AccountID) = 1 THEN 'Yes'
			ELSE NULL
		  END AS ExtendedServiceOption_Yes
		, CASE
			WHEN dbo.fxGetExtendedServiceOption(MSA.AccountID) = 0 THEN 'Yes'
			ELSE NULL
		  END AS ExtendedServiceOption_No
		, CUST1.BusinessName AS NameOfBusiness
		, CUST1.SSN AS SSO
		, CUST2.SSN AS SSR
		, PADRS.AddressID AS PremiseAddressID
		, PADRS.StreetAddress + CASE
			WHEN PADRS.StreetAddress2 IS NOT NULL THEN '; ' + PADRS.StreetAddress2
			ELSE ''
		  END AS PremiseAddress
		, PADRS.City AS PremiseCity
		, PADRS.StateId AS PremiseState
		, PADRS.PostalCode AS PremiseZip
		, PADRS.PlusFour AS PremiseZip4
		, BADR.AddressID AS BillingAddressID
		, BADR.StreetAddress + CASE
			WHEN BADR.StreetAddress2 IS NOT NULL THEN '; ' + BADR.StreetAddress2
			ELSE ''
		  END AS BillingAddress
		, BADR.City AS BillingCity
		, BADR.StateId AS BillingState
		, BADR.PostalCode AS BillingZip
		, BADR.PlusFour AS BillingZip4
		, CASE
			WHEN PADRS.Phone IS NOT NULL THEN SUBSTRING(PADRS.Phone, 1, 3)
			ELSE NULL
		  END AS PremiseAreaCode
		, CASE
			WHEN PADRS.Phone IS NOT NULL THEN SUBSTRING(PADRS.Phone, 4, 7)
			ELSE NULL
		  END AS PremisePhone
--		, BADR.Phone AS SpousePhone
		, CASE
			WHEN BADR.Phone IS NOT NULL THEN SUBSTRING(BADR.Phone, 1, 3)
			ELSE NULL
		  END AS SpouseAreaCode
		, CASE
			WHEN BADR.Phone IS NOT NULL THEN SUBSTRING(BADR.Phone, 4, 7)
			ELSE NULL
		  END AS SpousePhone
		, CASE 
			WHEN CUST1.Email IS NOT NULL THEN CUST1.Email
			ELSE MASI.Email
		  END AS OwnerEmail
		--, ABC.MonthlyFee
		--, dbo.fxInvoiceCalActivationFee(InstallInvoice.InvoiceID) AS ActivationFee
		--, dbo.fxInvoiceCalMonthlyMonitoringRate(InstallInvoice.InvoiceID) AS MonthlyFee
		, [dbo].fxMsAccountSetupFeeGet(MSA.AccountID, 0) AS [ActivationFee]
		--, [dbo].fxMsAccountSetupFeeGet(MSA.AccountID, 1) AS [SetupFee1stMonth]
		, [dbo].fxMsAccountMMRGet(MSA.AccountID) AS [MMR]
		, [dbo].fxMsAccountO3MGet(MSA.AccountID) AS [ActivationFee_3Installments]
		, CASE
			WHEN [dbo].fxMsAccountCellUnitTypeGet(MSA.CellPackageItemId) = 'Alarm.com' THEN 'X'
			ELSE ''
		  END AS CellularProvider_ADC
		, CASE
			WHEN [dbo].fxMsAccountCellUnitTypeGet(MSA.CellPackageItemId) = 'Telguard' THEN 'X'
			ELSE ''
		  END AS CellularProvider_TLG
		, CASE
			WHEN [dbo].fxMsAccountCellUnitTypeGet(MSA.CellPackageItemId) = 'HW AlarmNet' THEN 'X'
			ELSE ''
		  END AS CellularProvider_ANT
		, ABC.BkRoutingNumber AS ChkAccount_ABARoutingNumber
		, ABC.BkAccountNumber AS ChkAccount_Number
		, ABC.CcNameOn AS CC_NameOn
		, ABC.CcNumber AS CC_Number
		, CASE
			WHEN ABC.CCType = 'VISA' THEN 'X'
			ELSE NULL
		  END AS CC_TypeVisa
		, CASE
			WHEN ABC.CCType = 'MSTR' THEN 'X'
			ELSE NULL
		  END AS CC_TypeMstr
		, CASE
			WHEN ABC.CCType = 'AMEX' THEN 'X'
			ELSE NULL
		  END AS CC_TypeAmex
		, CASE
			WHEN ABC.CCType = 'DISC' THEN 'X'
			ELSE NULL
		  END AS CC_TypeDisc
		, ABC.CcExpMonth AS CC_ExpM
		, ABC.CcExpYear AS CC_ExpY
		, ABC.BillingTypeId
		, CNTRT.Readable AS InitialTerm0
		, CNTRT.ContractLength AS InitialTerm1
		, CNTRC.MonthlyFee AS MonthlyFee
		, CAST(CNTRT.ContractLength * CNTRC.MonthlyFee AS MONEY) AS TotalPayments
		, SALER.LastName AS RepLastName
		, CASE
			WHEN MSA.SystemTypeId = '2WAY' THEN 'X'
			WHEN MSA.SystemTypeId = '2WYCELL' THEN 'X'
			ELSE NULL
		  END AS MonitoringType_TwoWay
		, CASE
			WHEN MSA.SystemTypeId = 'DIGI' THEN 'X'
			ELSE NULL
		  END AS MonitoringType_Digital
		, MSA.SystemTypeId
		, MASI.InstallDate
		, DATEPART(m, MASI.InstallDate) AS DateInstalled_MM
		, DATEPART(d, MASI.InstallDate) AS DateInstalled_DD
		, SUBSTRING(CAST(DATEPART(yy, MASI.InstallDate) AS VARCHAR), 3, 2) AS DateInstalled_YY
		, MASI.BillingDay
		, [dbo].fxGetLastNOCDate(CNTRC.EffectiveDate) AS LastCancelDate
		--, DATEPART(m, [dbo].fxGetLastNOCDate(CNTRC.EffectiveDate)) AS DateNOC_MM
		--, DATEPART(d, [dbo].fxGetLastNOCDate(CNTRC.EffectiveDate)) AS DateNOC_DD
		--, DATEPART(yy, [dbo].fxGetLastNOCDate(CNTRC.EffectiveDate)) AS DateNOC_YYYY

	FROM
		[dbo].[MC_Accounts] AS MCA WITH (NOLOCK)
		INNER JOIN [dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
		ON
			(MSA.AccountID = MCA.AccountID)
		LEFT OUTER JOIN [dbo].[MS_AccountSalesInformations] AS MASI WITH (NOLOCK)
		ON
			(MASI.AccountID = MSA.AccountID)
		INNER JOIN [dbo].[MS_AccountCustomers] AS MSAC1 WITH (NOLOCK)
		ON
			(MSAC1.AccountId = MCA.AccountID)
			--AND ((MSAC1.AccountCustomerTypeId = 'MONI') OR (MSAC1.AccountCustomerTypeId = 'PRI'))
			AND (MSAC1.AccountCustomerTypeId = 'MONI')
		INNER JOIN [dbo].[AE_Customers] AS CUST1 WITH (NOLOCK)
		ON
			(CUST1.CustomerID = MSAC1.CustomerId)
		LEFT OUTER JOIN [dbo].[MS_AccountCustomers] AS MSAC2 WITH (NOLOCK)
		ON
			(MSAC2.AccountId = MCA.AccountID)
			AND (MSAC2.AccountCustomerTypeId = 'SEC')
		LEFT OUTER JOIN [dbo].[AE_Customers] AS CUST2 WITH (NOLOCK)
		ON
			(CUST2.CustomerID = MSAC2.CustomerId)
		INNER JOIN [dbo].[AE_Contracts] AS CNTRC WITH (NOLOCK)
		ON
			(CNTRC.ContractID = MSA.ContractId)
		INNER JOIN [dbo].[AE_ContractTemplates] AS CNTRT WITH (NOLOCK)
		ON
			(CNTRT.ContractTemplateID = CNTRC.ContractTemplateId)
		INNER JOIN [dbo].[MC_Addresses] AS PADRS WITH (NOLOCK)
		ON
			(PADRS.AddressID = CUST1.AddressId)
		LEFT OUTER JOIN [dbo].[AE_BillingCustomers] AS ABC WITH (NOLOCK)
		ON
			(ABC.ContractId = MSA.ContractId)
		LEFT OUTER JOIN [dbo].[AE_Customers] AS BCUST WITH (NOLOCK)
		ON
			(BCUST.CustomerID = ABC.CustomerId)
		LEFT OUTER JOIN [dbo].[MC_Addresses] AS BADR WITH (NOLOCK)
		ON
			(BADR.AddressID = BCUST.AddressId)
		INNER JOIN [dbo].[AE_Invoices] AS InstallInvoice WITH (NOLOCK)
		ON
			(InstallInvoice.AccountId = MCA.AccountID)
			AND (InstallInvoice.InvoiceTypeId = 'INSTALL')
		INNER JOIN [dbo].[QL_Leads] AS LEDS WITH (NOLOCK)
		ON
			(LEDS.LeadID = MSAC1.LeadId)
		LEFT OUTER JOIN [WISE_HumanResource].[dbo].[RU_Users] AS SALER WITH (NOLOCK)
		ON
			(SALER.GPEmployeeID = LEDS.SalesRepId)
		
GO
SELECT * FROM vwBX_DocumentFieldsAMNXS001 WHERE AccountId = 130532;
/* TEST 
SELECT COUNT(*) AS [Num Of Cols]
FROM INFORMATION_SCHEMA.COLUMNS
WHERE (table_schema = 'dbo') -- the database
    AND (table_name = 'vwBX_DocumentFieldsAMNXS001');
*/

--BEGIN TRANSACTION 

--UPDATE [dbo].[BX_BarcodeTypes] SET
--	SqlStatement = 'SELECT * FROM vwBX_DocumentFieldsAMNXS001 WHERE AccountId = @AccountId'
--WHERE
--	(BarcodeTypeID = 'AMNXS001');

--ROLLBACK TRANSACTION