USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountClientDetails')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountClientDetails'
		DROP VIEW dbo.vwMS_AccountClientDetails
	END
GO

PRINT 'Creating VIEW vwMS_AccountClientDetails'
GO

/****** Object:  View [dbo].[vwMS_AccountClientDetails]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountClientDetails.sql
**		Name: vwMS_AccountClientDetails
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
**		Date: 08/18/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	08/18/2012	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountClientDetails]
AS

	/** Query */
	SELECT
		[AccountCustomerID]
		, ACA.[CustomerId]
		, ACA.[AccountId]
		--, CST.CustomerID
		, CST.CustomerTypeId
		, CST.CustomerMasterFileId
		, CST.DealerId
		, CST.AddressId
		, CAD.StreetAddress
		, CAD.StreetAddress2
		, CAD.City
		, CAD.StateId
		, CAD.PostalCode
		, CAD.PlusFour
		, CAD.County
		, CAD.CountryId
		, CST.LeadId
		, CST.LocalizationId
		, CST.Prefix
		, CST.FirstName
		, CST.MiddleName
		, CST.LastName
		, CST.Postfix
		, CST.Gender
		, CST.PhoneHome
		, CST.PhoneWork
		, CST.PhoneMobile
		, CST.Email
		, CST.DOB
		, CST.SSN
		, CST.Username
		, CST.Password
		, CST.IsActive AS [CustomerIsActive]
		--, ACT.AccountID
		, ACT.IndustryAccountId
		, ACT.SystemTypeId
		, ACT.CellularTypeId
		, ACT.PanelTypeId
		, ACT.SimProductBarcodeId
		--, ACT.GpsWatchProductBarcodeId
		--, ACT.GpsWatchPhoneNumber
		--, ACT.GpsWatchUnitID
		, ACT.IsActive AS [MsAccountIsActive]
		--, IAN.IndustryAccountID
		--, IAN.AccountID
		, IAN.IndustryAccount
		, IAN.Designator
		, IAN.SubscriberNumber
	FROM
		[WISE_CRM].[dbo].[MS_AccountCustomers] AS ACA WITH (NOLOCK)
		INNER JOIN [WISE_CRM].[dbo].AE_Customers AS CST WITH(NOLOCK)
		ON
			(ACA.CustomerId = CST.CustomerID)
			AND (CST.IsDeleted = 0)
		INNER JOIN [WISE_CRM].[dbo].MC_Addresses AS CAD WITH (NOLOCK)
		ON
			(CST.AddressId = CAD.AddressID)
		INNER JOIN [WISE_CRM].[dbo].MS_Accounts AS ACT WITH(NOLOCK)
		ON
			(ACA.AccountId = ACT.AccountID)
			AND (ACT.IsDeleted = 0)
		INNER JOIN [WISE_CRM].[dbo].vwMS_IndustryAccountNumbers AS IAN
		ON
			(ACT.IndustryAccountId = IAN.IndustryAccountID)
GO
/* TEST */
-- SELECT * FROM vwMS_AccountClientDetails
