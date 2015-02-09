USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custQL_LeadSearchResultViewSearch')
	BEGIN
		PRINT 'Dropping Procedure custQL_LeadSearchResultViewSearch'
		DROP  Procedure  dbo.custQL_LeadSearchResultViewSearch
	END
GO

PRINT 'Creating Procedure custQL_LeadSearchResultViewSearch'
GO
/******************************************************************************
**		File: custQL_LeadSearchResultViewSearch.sql
**		Name: custQL_LeadSearchResultViewSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**			
*******************************************************************************/
CREATE Procedure dbo.custQL_LeadSearchResultViewSearch
(
	@FirstName NVARCHAR(50)
	, @LastName NVARCHAR(50)
	, @Phone NVARCHAR(20)
	, @DealerId INT
	, @Email NVARCHAR(256)
	, @LeadId BIGINT
	, @LeadDispositionId INT
	, @LeadSourceId INT
	, @PageSize INT = 30
	, @PageNumber INT = 1
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** DEBUG */
	PRINT 'EMAIL: ' + @Email;
	
	/** Calculate StartRow and EndRow. */
	DECLARE @StartRow INT
	DECLARE @EndRow INT
	-- Calculations 
	SET @StartRow = ((@PageNumber - 1) * @PageSize) + 1;
	SET @EndRow = @PageNumber * @PageSize;
	
	/** Create SQL Statement */
	SELECT 
		LSRO.[CustomerMasterFileId]
		, LSRO.[LeadID]
		, LSRO.[DealerId]
		, LSRO.[LocalizationId]
		, LSRO.[LeadDispositionId]
		, LSRO.[LeadDisposition]
		, LSRO.[LeadSourceId]
		, LSRO.[LeadSource]
		, LSRO.[FirstName]
		, LSRO.[LastName]
		, LSRO.[PhoneHome]
		, LSRO.[PhoneMobile]
		, LSRO.[PhoneWork]
		, LSRO.[DOB]
		, LSRO.[SalesRepId]
		, LSRO.[SSN]
		, LSRO.[DL]
		, LSRO.[DLStateID]
		, LSRO.[Email]
		, LSRO.[IsCustomer]
		, LSRO.[RowNum]
	FROM (
		SELECT 
			LSR.[CustomerMasterFileId]
			, LSR.[LeadID]
			, LSR.[DealerId]
			, LSR.[LocalizationId]
			, LSR.[LeadDispositionId]
			, LSR.[LeadDisposition]
			, LSR.[LeadSourceId]
			, LSR.[LeadSource]
			, LSR.[FirstName]
			, LSR.[LastName]
			, LSR.[PhoneHome]
			, LSR.[PhoneMobile]
			, LSR.[PhoneWork]
			, LSR.[DOB]
			, LSR.[SalesRepId]
			, LSR.[SSN]
			, LSR.[DL]
			, LSR.[DLStateID]
			, LSR.[Email]
			, LSR.[IsCustomer]
			, ROW_NUMBER() OVER (ORDER BY LSR.LastName, LSR.FirstName) AS RowNum
		FROM 
			[dbo].vwQL_LeadSearchResult AS LSR
		WHERE
			(@FirstName IS NULL OR LSR.FirstName LIKE '%' + @FirstName + '%')
			AND (@LastName IS NULL OR LSR.LastName LIKE '%' + @LastName + '%')
			AND (@Phone IS NULL OR LSR.PhoneHome = @Phone)
			AND (@Phone IS NULL OR LSR.PhoneMobile = @Phone)
			AND (@Phone IS NULL OR LSR.PhoneWork = @Phone)
			AND (@DealerId IS NULL OR LSR.DealerId = @DealerId)
			AND (@Email IS NULL OR LSR.Email = @Email)
			AND (@LeadDispositionId IS NULL OR LSR.LeadDispositionId = @LeadDispositionId)
			AND (@LeadSourceId IS NULL OR LSR.LeadSourceId= @LeadSourceId)
			AND (@LeadId IS NULL OR LSR.LeadId = @LeadId OR LSR.CustomerMasterFileId = @LeadId)
	) AS LSRO
	WHERE
		LSRO.RowNum BETWEEN @StartRow AND @EndRow	
	ORDER BY
		LSRO.LastName
		, LSRO.FirstName
END
GO

GRANT EXEC ON dbo.custQL_LeadSearchResultViewSearch TO PUBLIC
GO

/***/ EXEC dbo.custQL_LeadSearchResultViewSearch 
	NULL --@FirstName NVARCHAR(50)
	, NULL -- @LastName NVARCHAR(50)
	, NULL -- @Phone NVARCHAR(20)
	, NULL -- @DealerId INT
	, NULL --'andres@wisearchitect.com' -- @Email NVARCHAR(256)
	, NULL -- @LeadId BIGINT
	, 30
	, 1