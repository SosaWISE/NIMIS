USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxRepts_InstallsByRepIdOfficeIdAll')
	BEGIN
		PRINT 'Dropping FUNCTION fxRepts_InstallsByRepIdOfficeIdAll'
		DROP FUNCTION  dbo.fxRepts_InstallsByRepIdOfficeIdAll
	END
GO

PRINT 'Creating FUNCTION fxRepts_InstallsByRepIdOfficeIdAll'
GO

/****** Object:  UserDefinedFunction [dbo].[fxRepts_InstallsByRepIdOfficeIdAll]    Script Date: 7/30/2015 11:23:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: fxRepts_InstallsByRepIdOfficeIdAll.sql
**		Name: fxRepts_InstallsByRepIdOfficeIdAll
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
**		Date: 07/21/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	07/21/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION [dbo].[fxRepts_InstallsByRepIdOfficeIdAll]
(
	@officeId INT = NULL
	, @salesRepId VARCHAR(50) = NULL
	, @DealerId INT = 5000
	, @startDate DATETIME
	, @endDate DATETIME
)
RETURNS 
@ParsedList table
(
	--[CustomerMasterFileID] [bigint] NOT NULL
	--, [AccountId] [bigint] NOT NULL
	[OfficeId] [INT] NOT NULL
	, [Installs] [INT] NOT NULL
	, [Term] [INT] NULL
	, [CloseRate] [INT] NULL
	, [SetupFee] [MONEY] NULL
	, [FirstMonth] [MONEY] NULL
	, [Over3Months] [MONEY] NULL
	, [PackageSoldId] [INT] NULL
)
AS
BEGIN
	-- EXECUTE 
	INSERT INTO @ParsedList (
		--CustomerMasterFileID
		--, AccountId
		OfficeId
		, Installs
		, Term
		, CloseRate
		, SetupFee
		, [FirstMonth]
		, Over3Months
		, PackageSoldId
	)
	VALUES (@officeId, 0, 0, 0, 0, 0, 0, NULL)

	UPDATE OFFICE SET
		OFFICE.Installs = ISNULL(AGR.Installs, 0)
		, OFFICE.Term = ISNULL(AGR.Term, 0)
		, OFFICE.CloseRate = ISNULL(AGR.CloseRate, 0)
		, OFFICE.SetupFee = ISNULL(AGR.SetupFee, 0)
		, OFFICE.[FirstMonth] = ISNULL(AGR.[FirstMonth], 0)
		, OFFICE.Over3Months = ISNULL(AGR.Over3Months, 0)
		, OFFICE.PackageSoldId = ISNULL(AGR.PackageSoldId, 0)
	FROM
		@ParsedList AS OFFICE
		LEFT OUTER JOIN
		(SELECT
			PERFM.OfficeId
			, COUNT(PERFM.AccountID) AS Installs
			, AVG(PERFM.Term) AS Term
			, 0 AS [CloseRate]
			, AVG(PERFM.SetupFee) AS [SetupFee]
			, AVG(PERFM.[FirstMonth]) AS [FirstMonth]
			, COUNT(PERFM.Over3Months) AS [Over3Months]
			, COUNT(PERFM.[PackageSoldId]) AS [PackageSoldId]
		FROM
			@ParsedList AS OFFICE
			INNER JOIN vwReports_Performance AS PERFM
			ON
				(OFFICE.OfficeId = PERFM.OfficeId)
		WHERE
			((PERFM.SubmitAccountOnline BETWEEN @StartDate AND @EndDate) OR (PERFM.InstallDate BETWEEN @startDate AND @endDate))
			AND (@dealerId IS NULL OR (PERFM.DealerId = @DealerId))
			AND (@officeId IS NULL OR (PERFM.OfficeId = @officeId))
			AND (@SalesRepID IS NULL OR (PERFM.SalesRepId = @SalesRepID))
		GROUP BY
			PERFM.OfficeId
		) AS AGR
		ON
			(AGR.OfficeId = OFFICE.OfficeId)
/**/
/*		(SELECT
			--AEC.CustomerMasterFileId
			--, MSASI.AccountID
			MSASI.TeamLocationId AS OfficeId
			, COUNT(MSASI.AccountID) AS Installs
			, AVG(MSASI.ContractLength) AS Term
			, 0 AS [CloseRate]
			, AVG(MSASI.SetupFee) AS [SetupFee]
			, AVG(MSASI.SetupFee1stMonth) AS [FirstMonth]
			, COUNT(MSASI.Over3Months) AS [Over3Months]
			, COUNT(MSASI.AccountPackageId) AS [PackageSoldId]
		FROM 
			@ParsedList AS OFFICE
			INNER JOIN [WISE_CRM].[dbo].vwMS_AccountSalesInformations AS MSASI WITH (NOLOCK)
			ON
				(OFFICE.OfficeId = MSASI.TeamLocationId)
			INNER JOIN [WISE_CRM].[dbo].[MS_AccountSetupCheckLists] AS MSASCL WITH (NOLOCK)
			ON
				(MSASCL.AccountID = MSASI.AccountID)
			INNER JOIN [WISE_CRM].[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
			ON
				(AECA.AccountId = MSASI.AccountID)
				AND (AECA.CustomerTypeId = 'PRI')
			INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS AEC WITH (NOLOCK)
			ON
				(AEC.CustomerID = AECA.CustomerId)
		WHERE
			((MSASCL.SubmitAccountOnline BETWEEN @StartDate AND @EndDate) OR (MSASI.InstallDate BETWEEN @startDate AND @endDate))
			AND (@dealerId IS NULL OR (MSASI.DealerId = @DealerId))
			AND (@officeId IS NULL OR (MSASI.TeamLocationId = @officeId))
			AND (@SalesRepID IS NULL OR (MSASI.SalesRepId = @SalesRepID))
		GROUP BY
			--AEC.CustomerMasterFileId
			--, MSASI.AccountID
			MSASI.TeamLocationId) AS AGR
		ON
			(AGR.OfficeId = OFFICE.OfficeId)
*/

	RETURN;
END

GO
/*
DECLARE @officeId INT = 2
	, @salesRepId VARCHAR(50) = NULL
	, @DealerId INT = 5000
	, @startDate DATETIME = '1/1/2013'
	, @endDate DATETIME = '2015-08-01 05:00:00';

SELECT
	FXF.OfficeId
	, FXF.Installs
	, FXF.Term
	, FXF.CloseRate
	, FXF.SetupFee
	, FXF.[FirstMonth]
	, FXF.Over3Months
	, FXF.PackageSoldId
FROM
	[dbo].fxRepts_InstallsByRepIdOfficeIdAll(@officeId, @salesRepId, @dealerId, @startDate, @endDate) AS [FXF]

SELECT 
	COUNT(*)
FROM 
	dbo.vwMS_AccountSalesInformations AS MSASI WITH (NOLOCK)
	INNER JOIN [WISE_CRM].[dbo].[MS_AccountSetupCheckLists] AS MSASCL WITH (NOLOCK)
	ON
		(MSASCL.AccountID = MSASI.AccountID)
	INNER JOIN [WISE_CRM].[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
	ON
		(AECA.AccountId = MSASI.AccountID)
		AND (AECA.CustomerTypeId = 'PRI')
	INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS AEC WITH (NOLOCK)
	ON
		(AEC.CustomerID = AECA.CustomerId)
WHERE 
	(MSASI.InstallDate BETWEEN @startDate AND @endDate)
	AND (@dealerId IS NULL OR (MSASI.DealerId = @DealerId))
	AND (@officeId IS NULL OR (MSASI.TeamLocationId = @officeId))
	AND (@SalesRepID IS NULL OR (MSASI.SalesRepId = @SalesRepID));
*/