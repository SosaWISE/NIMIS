USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerCardInfo')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerCardInfo'
		DROP VIEW dbo.vwAE_CustomerCardInfo
	END
GO

--PRINT 'Creating VIEW vwAE_CustomerCardInfo'
--GO

--/****** Object:  View [dbo].[vwAE_CustomerCardInfo]    Script Date: 01/10/2011 15:18:27 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--/******************************************************************************
--**		File: vwAE_CustomerCardInfo.sql
--**		Name: vwAE_CustomerCardInfo
--**		Desc: 
--**
--**		This template can be customized:
--**              
--**		Return values: Table of IDs/Ints
--** 
--**		Called by:   
--**              
--**		Parameters:
--**		Input							Output
--**     ----------						-----------
--**
--**		Auth: Andres Sosa
--**		Date: 04/29/2014
--*******************************************************************************
--**	Change History
--*******************************************************************************
--**	Date:		Author:			Description:
--**	-----------	---------------	-----------------------------------------------
--**	04/29/2014	Andres Sosa		Created by
--*******************************************************************************/
--CREATE VIEW [dbo].[vwAE_CustomerCardInfo]
--AS
--	-- Enter Query here
--	SELECT
--		CST.CustomerID
--		, 'CUSTOMER' AS [ResultType]
--		, CST.CustomerMasterFileId
--		, CST.Prefix
--		, CST.FirstName
--		, CST.MiddleName
--		, CST.LastName
--		, CST.Postfix
--		, [dbo].[fxGetCustomerFullName] (NULL, CST.Prefix, CST.FirstName, CST.MiddleName, CST.LastName, CST.Postfix) AS FullName
--		, CST.Gender
--		, CST.PhoneHome
--		, CST.PhoneWork
--		, CST.PhoneMobile
--		, CST.Email
--		, CST.DOB
--		, CST.SSN
--		, CST.Username
--		, ADR.AddressID
--		, ADR.StreetAddress
--		, ADR.StreetAddress2
--		, ADR.City
--		, ADR.StateId
--		, ADR.PostalCode
--		, ADR.PlusFour
--		, [dbo].[fxGetAddressCityStatePostalCode](ADR.City, ADR.StateId, ADR.PostalCode, ADR.PlusFour) AS [CityStateZip]
--	FROM
--		[dbo].[AE_Customers] AS CST WITH (NOLOCK)
--		INNER JOIN [dbo].[AE_CustomerMasterFiles] AS CMF WITH (NOLOCK)
--		ON
--			(CMF.CustomerMasterFileID = CST.CustomerMasterFileId)
--			AND (CST.CustomerTypeId = 'PRI')
--			AND (CST.IsDeleted = 0)
--		INNER JOIN [dbo].[MC_Addresses] AS ADR
--		ON
--			(ADR.AddressID = CST.AddressId)
--		INNER JOIN [dbo].[QL_Leads] AS LED WITH (NOLOCK)
--		ON
--			(LED.LeadID = CST.LeadId)
--		INNER JOIN [dbo].[QL_CreditReports] AS CR WITH (NOLOCK)
--		ON
--			(CR.LeadId = LED.LeadID)
--		INNER JOIN [dbo].QL_CreditReportBureaus AS CRB WITH (NOLOCK)
--		ON
--			(CRB.BureauID = CR.BureauId)
--GO
--/* TEST */
