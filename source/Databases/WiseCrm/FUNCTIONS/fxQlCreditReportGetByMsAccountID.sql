USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxQlCreditReportGetByMsAccountID')
	BEGIN
		PRINT 'Dropping FUNCTION fxQlCreditReportGetByMsAccountID'
		DROP FUNCTION  dbo.fxQlCreditReportGetByMsAccountID
	END
GO

PRINT 'Creating FUNCTION fxQlCreditReportGetByMsAccountID'
GO
/******************************************************************************
**		File: fxQlCreditReportGetByMsAccountID.sql
**		Name: fxQlCreditReportGetByMsAccountID
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
**		Auth: Andrés E. Sosa
**		Date: 02/11/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/11/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxQlCreditReportGetByMsAccountID
(
	@AccountID BIGINT
)
RETURNS 
@ParsedList table
(
	[CreditReportID] [bigint] NOT NULL,
	[LeadId] [bigint] NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[BureauId] [char](2) NOT NULL,
	[SeasonId] [int] NOT NULL,
	[CreditReportVendorId] [char](4) NOT NULL,
	[CreditReportVendorAbaraId] [bigint] NULL,
	[CreditReportVendorMicrobiltId] [bigint] NULL,
	[CreditReportVendorEasyAccessId] [bigint] NULL,
	[CreditReportVendorManualId] [bigint] NULL,
	[Prefix] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Suffix] [nvarchar](50) NULL,
	[SSN] [varchar](50) NULL,
	[DOB] [datetime] NULL,
	[Score] [int] NOT NULL,
	[ReportID] [int] NULL,
	[ReportGuid] [uniqueidentifier] NULL,
	[IsSelected] [bit] NOT NULL,
	[IsScored] [bit] NOT NULL,
	[IsHit] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[CreatedOn] [datetime] NOT NULL
)
AS
BEGIN
	INSERT INTO @ParsedList(
		[CreditReportID],
		[LeadId],
		[AddressId],
		[BureauId],
		[SeasonId],
		[CreditReportVendorId],
		[CreditReportVendorAbaraId],
		[CreditReportVendorMicrobiltId],
		[CreditReportVendorEasyAccessId],
		[CreditReportVendorManualId],
		[Prefix],
		[FirstName],
		[MiddleName],
		[LastName],
		[Suffix],
		[SSN],
		[DOB],
		[Score],
		[ReportID],
		[ReportGuid],
		[IsSelected],
		[IsScored],
		[IsHit],
		[IsActive],
		[IsDeleted],
		[CreatedBy],
		[CreatedOn]
	)
	SELECT TOP 1
		QCR.CreditReportID
		, QCR.LeadId
		, QCR.AddressId
		, QCR.BureauId
		, QCR.SeasonId
		, QCR.CreditReportVendorId
		, QCR.CreditReportVendorAbaraId
		, QCR.CreditReportVendorMicrobiltId
		, QCR.CreditReportVendorEasyAccessId
		, QCR.CreditReportVendorManualId
		, QCR.Prefix
		, QCR.FirstName
		, QCR.MiddleName
		, QCR.LastName
		, QCR.Suffix
		, QCR.SSN
		, QCR.DOB
		, QCR.Score
		, QCRVA.ReportID
		, QCRVA.ReportGuid
		, QCR.IsSelected
		, QCR.IsScored
		, QCR.IsHit
		, QCR.IsActive
		, QCR.IsDeleted
		, QCR.CreatedBy
		, QCR.CreatedOn
	FROM
		[dbo].[AE_CustomerAccounts] AS ACA WITH (NOLOCK)
		INNER JOIN [dbo].[QL_Leads] AS LED WITH (NOLOCK)
		ON
			(LED.LeadID = ACA.LeadId)
		INNER JOIN [dbo].[MC_Accounts] AS MCA WITH (NOLOCK)
		ON
			(MCA.AccountID = ACA.AccountId)
			AND (MCA.AccountID = @AccountID)
			AND (MCA.AccountTypeId = 'ALRM')
		INNER JOIN [dbo].[QL_CreditReports] AS QCR WITH (NOLOCK)
		ON
			(QCR.LeadId = LED.LeadID)
		LEFT OUTER JOIN [dbo].[QL_CreditReportVendorAbara] AS QCRVA WITH (NOLOCK)
		ON
			(QCRVA.CreditReportId = QCR.CreditReportID)
	WHERE
		(QCR.Score <> 999)
	ORDER BY
		QCR.Score DESC;

	RETURN 
END
GO

--SELECT * FROM [dbo].fxQlCreditReportGetByMsAccountID(100222);