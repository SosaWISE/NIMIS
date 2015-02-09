USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwQL_LeadSearchResult')
	BEGIN
		PRINT 'Dropping VIEW vwQL_LeadSearchResult'
		DROP VIEW dbo.vwQL_LeadSearchResult
	END
GO

PRINT 'Creating VIEW vwQL_LeadSearchResult'
GO

/****** Object:  View [dbo].[vwQL_LeadSearchResult]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwQL_LeadSearchResult.sql
**		Name: vwQL_LeadSearchResult
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
**		Date: 01/10/2011
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	04/25/2012	Andres Sosa	Created
*******************************************************************************/
CREATE VIEW [dbo].[vwQL_LeadSearchResult]
AS
	/** Enter Query here */
	SELECT DISTINCT
		LD.CustomerMasterFileId
		, LD.LeadID
		, LD.DealerId
		, LD.LocalizationId
		, LD.LeadDispositionId
		, LDD.LeadDisposition
		, LD.LeadDispositionDateChange
		, LD.LeadSourceId
		, LDS.LeadSource
		, LD.FirstName
		, LD.LastName
		, LD.PhoneHome
		, LD.PhoneMobile
		, LD.PhoneWork
		, LD.DOB
		, LD.SalesRepId
		, LD.SSN
		, LD.DL
		, LD.DLStateID
		, LD.Email
		, CASE
			WHEN CUST.CustomerID IS NOT NULL THEN CAST(1 AS BIT)
			ELSE  CAST(0 AS BIT)
		  END AS 'IsCustomer'
		, CAST(0 AS INT) AS RowNum
	FROM
		[dbo].QL_Leads AS LD WITH (NOLOCK)
		INNER JOIN dbo.AE_CustomerMasterFiles AS CMF WITH (NOLOCK)
		ON
			(LD.CustomerMasterFileId = CMF.CustomerMasterFileID)
		LEFT OUTER JOIN [dbo].AE_Customers AS CUST WITH (NOLOCK)
		ON
			(CMF.CustomerMasterFileID = CUST.CustomerMasterFileId)
			AND (LD.LeadID = CUST.LeadId)
		INNER JOIN dbo.QL_LeadDispositions AS LDD WITH (NOLOCK)
		ON
			(LD.LeadDispositionId = LDD.LeadDispositionID)
		INNER JOIN dbo.QL_LeadSources AS LDS WITH (NOLOCK)
		ON
			(LD.LeadSourceId = LDS.LeadSourceID)
GO
/* TEST */
-- SELECT * FROM vwQL_LeadSearchResult