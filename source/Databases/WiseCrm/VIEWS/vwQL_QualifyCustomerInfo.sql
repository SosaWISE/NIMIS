USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwQL_QualifyCustomerInfo')
	BEGIN
		PRINT 'Dropping VIEW vwQL_QualifyCustomerInfo'
		DROP VIEW dbo.vwQL_QualifyCustomerInfo
	END
GO

PRINT 'Creating VIEW vwQL_QualifyCustomerInfo'
GO

/****** Object:  View [dbo].[vwQL_QualifyCustomerInfo]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwQL_QualifyCustomerInfo.sql
**		Name: vwQL_QualifyCustomerInfo
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
**		Date: 04/21/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/21/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwQL_QualifyCustomerInfo]
AS
	-- Enter Query here
	SELECT
		LED.LeadID
		, LED.SeasonId
		, [dbo].fxFormatFullName(LED.Salutation, LED.FirstName, LED.MiddleName, LED.LastName, LED.Suffix) AS [CustomerName]
		, LED.Email AS [CustomerEmail]
		, LED.DOB
		, ADR.AddressID
		, ADR.StreetAddress
		, ADR.StreetAddress2
		, ADR.City
		, ADR.StateId
		, ADR.County
		, ADR.TimeZoneId
		, TZ.TimeZoneName
		, ADR.PostalCode
		, ADR.Phone
		, CR.CreditReportID
		, CR.IsHit
		, CASE
			WHEN CR.IsHit = 1 THEN 'Report Found'
			ELSE 'Report Not Found'
		  END AS CRStatus
		, CR.Score
		, CRB.BureauName
		, SREP.UserID
		, SREP.GPEmployeeID AS [CompanyID]
		, SREP.FirstName
		, SREP.MiddleName
		, SREP.LastName
		, SREP.PreferredName
		, SREP.Email AS [RepEmail]
		, SREP.PhoneCell
		, SREP.PhoneCellCarrierID
		, PCC.Description AS PhoneCellCarrier
		, SES.SeasonName
		, SES.ExcellentCreditScoreThreshold
		, SES.PassCreditScoreThreshold
		, SES.SubCreditScoreThreshold
	FROM
		[dbo].[QL_Leads] AS LED WITH (NOLOCK)
		INNER JOIN [dbo].[QL_Address] AS ADR WITH (NOLOCK)
		ON
			(ADR.AddressID = LED.AddressId)
		INNER JOIN [dbo].[MC_PoliticalTimeZones] AS TZ WITH (NOLOCK)
		ON
			(TZ.TimeZoneID = ADR.TimeZoneId)
		INNER JOIN [dbo].[QL_CreditReports] AS CR WITH (NOLOCK)
		ON
			(CR.LeadId = LED.LeadID)
		INNER JOIN [dbo].QL_CreditReportBureaus AS CRB WITH (NOLOCK)
		ON
			(CRB.BureauID = CR.BureauId)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_Users] AS SREP WITH (NOLOCK)
		ON
			(LED.SalesRepID = SREP.GPEmployeeID)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_Season] AS SES WITH (NOLOCK)
		ON
			(SES.SeasonID = LED.SeasonId)
		LEFT OUTER JOIN [WISE_HumanResource].[dbo].[RU_PhoneCellCarrier] AS PCC WITH (NOLOCK)
		ON
			(PCC.PhoneCellCarrierID = SREP.PhoneCellCarrierID)
GO
/* TEST 
SELECT * FROM vwQL_QualifyCustomerInfo WHERE LeadID = 1050563
SELECT TOP 1 LeadID FROM dbo.QL_Leads ORDER BY LeadID DESC;*/