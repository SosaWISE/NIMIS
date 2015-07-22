USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxRepts_QualifiedByRepIdOfficeIdAll')
	BEGIN
		PRINT 'Dropping FUNCTION fxRepts_QualifiedByRepIdOfficeIdAll'
		DROP FUNCTION  dbo.fxRepts_QualifiedByRepIdOfficeIdAll
	END
GO

PRINT 'Creating FUNCTION fxRepts_QualifiedByRepIdOfficeIdAll'
GO
/******************************************************************************
**		File: fxRepts_QualifiedByRepIdOfficeIdAll.sql
**		Name: fxRepts_QualifiedByRepIdOfficeIdAll
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
**		Date: 07/17/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	07/17/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxRepts_QualifiedByRepIdOfficeIdAll
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
	LeadID BIGINT
	, CustomerMasterFileId BIGINT
	, CustomerTypeId VARCHAR(20)
	, TeamLocationId INT
	, DealerId INT
	, SeasonId INT
	, SalesRepId VARCHAR(10)
	, FirstName VARCHAR(50)
	, LastName VARCHAR(50)
)
AS
BEGIN

	INSERT INTO @ParsedList (
		LeadID
		, CustomerMasterFileId
		, CustomerTypeId
		, TeamLocationId
		, DealerId
		, SeasonId
		, SalesRepId
		, FirstName
		, LastName
	)
	SELECT
		QL.LeadID
		, QL.CustomerMasterFileId
		, QL.CustomerTypeId
		, QL.TeamLocationId
		, QL.DealerId
		, QL.SeasonId
		, QL.SalesRepId
		, QL.FirstName
		, QL.LastName
	FROM
		[WISE_CRM].[dbo].[QL_Leads] AS QL
	WHERE
		(QL.CreatedOn BETWEEN @StartDate AND @EndDate)
		AND (@dealerId IS NULL OR (QL.DealerId = @dealerId))
		AND (QL.CustomerTypeId = 'PRI')
		AND (LeadID NOT IN (SELECT LeadId FROM [WISE_CRM].[dbo].[AE_CUstomers])) -- This is a customer already.
		AND (@officeId IS NULL OR (QL.TeamLocationId = @officeId))
		AND (@salesRepID IS NULL OR (QL.SalesRepId = @salesRepID));

	RETURN;
END
GO

/** Execute */
SELECT * FROM [dbo].fxRepts_QualifiedByRepIdOfficeIdAll(NULL, NULL, NULL, '7/9/2015', '7/18/2015');