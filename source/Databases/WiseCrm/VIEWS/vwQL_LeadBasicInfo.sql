USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwQL_LeadBasicInfo')
	BEGIN
		PRINT 'Dropping VIEW vwQL_LeadBasicInfo'
		DROP VIEW dbo.vwQL_LeadBasicInfo
	END
GO

PRINT 'Creating VIEW vwQL_LeadBasicInfo'
GO

/****** Object:  View [dbo].[vwQL_LeadBasicInfo]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwQL_LeadBasicInfo.sql
**		Name: vwQL_LeadBasicInfo
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
**		Date: 02/29/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	02/29/2012	Andres Sosa		Created By
*******************************************************************************/
CREATE VIEW [dbo].[vwQL_LeadBasicInfo]
AS
	/** Enter Query here */
	SELECT
		LDS.LeadID
		, CMF.CustomerMasterFileID
		, ADR.AddressID
		, LDS.DealerId
		, LDS.LocalizationId
		, LDS.TeamLocationId
		, LDS.CustomerTypeId
		, LDS.SeasonId
		, LDS.SalesRepId
		, LDS.Salutation
		, LDS.FirstName
		, LDS.MiddleName
		, LDS.LastName
		, LDS.Suffix
		, LDS.SSN
		, LDS.DOB
		, LDS.DL
		, LDS.DLStateID
		, LDS.Email
		, LDS.PhoneHome
		, LDS.PhoneWork
		, LDS.PhoneMobile
		, ADR.StreetAddress
		, ADR.City
		, ADR.StateId
		, ADR.PostalCode
		, ADR.PlusFour
		, ADR.Phone AS PremisePhone
		, LDS.IsActive
		, LDS.IsDeleted
	FROM
		dbo.QL_Leads AS LDS WITH (NOLOCK)
		INNER JOIN dbo.AE_CustomerMasterFiles AS CMF WITH (NOLOCK)
		ON
			(LDS.CustomerMasterFileId = CMF.CustomerMasterFileID)
		INNER JOIN dbo.QL_Address AS ADR WITH (NOLOCK)
		ON
			(LDS.AddressId = ADR.AddressID)

GO
/* Unit TEST */
-- SELECT * FROM vwQL_LeadBasicInfo
