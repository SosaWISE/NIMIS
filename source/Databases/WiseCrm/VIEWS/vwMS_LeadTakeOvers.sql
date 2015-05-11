USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_LeadTakeOvers')
	BEGIN
		PRINT 'Dropping VIEW vwMS_LeadTakeOvers'
		DROP VIEW dbo.vwMS_LeadTakeOvers
	END
GO

PRINT 'Creating VIEW vwMS_LeadTakeOvers'
GO

/****** Object:  View [dbo].[vwMS_LeadTakeOvers]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_LeadTakeOvers.sql
**		Name: vwMS_LeadTakeOvers
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
**		Date: 05/07/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	05/07/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_LeadTakeOvers]
AS
	-- Enter Query here
	SELECT
		AECA.AccountId
		, QLL.LeadID
		, [dbo].fxGetCustomerFullName(NULL, QLL.Salutation, QLL.FirstName, QLL.MiddleName, QLL.LastName, QLL.Suffix) AS FullName
		, QLAD.StreetAddress
		, dbo.fxGetAddressCityStatePostalCode(QLAD.City, QLAD.StateId, QLAD.PostalCode, QLAD.PlusFour) AS CityStZip
		, MLT.AlarmCompanyId
		, MSAC.AlarmCompanyName
	FROM 
		[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
		INNER JOIN [dbo].[MS_LeadTakeOvers] AS MLT WITH (NOLOCK)
		ON
			(MLT.LeadId = AECA.LeadId)
			AND (AECA.CustomerTypeId = 'MONI')
		INNER JOIN [dbo].[MS_AlarmCompanies] AS MSAC WITH (NOLOCK)
		ON
			(MSAC.AlarmCompanyID = MLT.AlarmCompanyId)
		INNER JOIN [dbo].[QL_Leads] AS QLL WITH (NOLOCK)
		ON
			(QLL.LeadID = MLT.LeadId)
		INNER JOIN [dbo].[QL_Address] AS QLAD WITH (NOLOCK)
		ON
			(QLAD.AddressID = MLT.AddressId)
GO
/* TEST */
SELECT * FROM vwMS_LeadTakeOvers