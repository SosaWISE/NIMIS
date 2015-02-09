USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_GpsClientToCustomerMaster')
	BEGIN
		PRINT 'Dropping VIEW vwAE_GpsClientToCustomerMaster'
		DROP VIEW dbo.vwAE_GpsClientToCustomerMaster
	END
GO

PRINT 'Creating VIEW vwAE_GpsClientToCustomerMaster'
GO

/****** Object:  View [dbo].[vwAE_GpsClientToCustomerMaster]    Script Date: 07/08/2013 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_GpsClientToCustomerMaster.sql
**		Name: vwAE_GpsClientToCustomerMaster
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
**		Date: 07/08/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	07/08/2013	Andres Sosa	Created
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_GpsClientToCustomerMaster]
AS

	/** Enter Query here */
	SELECT
		CUST.[CustomerID]
		, CAST('false' AS BIT) AS [IsCurrent]
		, CUST.[CustomerTypeId]
		, 'user' AS [CustomerTypeUi]
		, GPSC.[CustomerMasterFileId]
		, CUST.[DealerId]
		, DLR.[DealerName]
		, CUST.[AddressId]
		, CUST.[LeadId]
		, CUST.[LocalizationId]
		, LOC.[LocalizationName]
		, CUST.[Prefix]
		, CUST.[FirstName]
		, CUST.[MiddleName]
		, CUST.[LastName]
		, CUST.[Postfix]
		, CUST.[Gender]
		, CUST.[PhoneHome]
		, CUST.[PhoneWork]
		, CUST.[PhoneMobile]
		, CUST.[Email]
		, CUST.[DOB]
		, CUST.[SSN]
		, CUST.[Username]
		, CUST.[Password]
--		, CAST(NULL AS DATETIME) AS [LastLoginOn]
		, CGPS.LastLoginOn
		, CUST.[IsActive]
		, CUST.[IsDeleted]
		, CUST.[ModifiedOn]
		, CUST.[ModifiedBy]
		, CUST.[CreatedOn]
		, CUST.[CreatedBy]
		, CUST.[DEX_ROW_TS]
	FROM
		[dbo].AE_GpsClientToCustomerMaster AS GPSC WITH (NOLOCK)
		INNER JOIN [dbo].AE_CustomerGpsClients AS CGPS WITH (NOLOCK)
		ON
			(GPSC.CustomerGpsClientId = CGPS.CustomerGpsClientID)
		INNER JOIN [dbo].AE_Customers AS CUST WITH (NOLOCK)
		ON
			(CGPS.CustomerID = CUST.CustomerID)
		INNER JOIN [dbo].AE_CustomerTypes AS CTYP WITH (NOLOCK)
		ON
			(CUST.CustomerTypeId = CTYP.CustomerTypeID)
			AND (CTYP.CustomerTypeID = 'GPSCLNT') -- Gps Client Customer
		INNER JOIN [dbo].AE_Dealers AS DLR WITH (NOLOCK)
		ON
			(CUST.DealerId = DLR.DealerID)
		INNER JOIN [dbo].MC_Localization AS LOC WITH (NOLOCK)
		ON
			(CUST.LocalizationId = LOC.LocalizationID)
UNION
	SELECT 
		VW.[CustomerID]
		, CAST('false' AS BIT) AS [IsCurrent]
		, VW.[CustomerTypeId]
		, 'user' AS [CustomerTypeUi]
		, VW.[CustomerMasterFileId]
		, VW.[DealerId]
		, DLR.[DealerName]
		, VW.[AddressId]
		, VW.[LeadId]
		, VW.[LocalizationId]
		, LOC.[LocalizationName]
		, VW.[Prefix]
		, VW.[FirstName]
		, VW.[MiddleName]
		, VW.[LastName]
		, VW.[Postfix]
		, VW.[Gender]
		, VW.[PhoneHome]
		, VW.[PhoneWork]
		, VW.[PhoneMobile]
		, VW.[Email]
		, VW.[DOB]
		, VW.[SSN]
		, VW.[Username]
		, VW.[Password]
		, dbo.fxGetCustomerGpsClientLastLoginOn(VW.CustomerID) AS [LastLoginOn]
		, VW.[IsActive]
		, VW.[IsDeleted]
		, VW.[ModifiedOn]
		, VW.[ModifiedBy]
		, VW.[CreatedOn]
		, VW.[CreatedBy]
		, VW.[DEX_ROW_TS]
	FROM
		vwAE_CustomerGpsClients AS VW
		INNER JOIN [dbo].AE_Dealers AS DLR WITH (NOLOCK)
		ON
			(VW.DealerId = DLR.DealerID)
		INNER JOIN [dbo].MC_Localization AS LOC WITH (NOLOCK)
		ON
			(VW.LocalizationId = LOC.LocalizationID)
GO
/* TEST */

SELECT * FROM vwAE_GpsClientToCustomerMaster WHERE CustomerID = 100195;