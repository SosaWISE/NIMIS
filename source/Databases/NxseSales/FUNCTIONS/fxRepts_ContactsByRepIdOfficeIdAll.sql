USE [NXSE_Sales]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxRepts_ContactsByRepIdOfficeIdAll')
	BEGIN
		PRINT 'Dropping FUNCTION fxRepts_ContactsByRepIdOfficeIdAll'
		DROP FUNCTION  dbo.fxRepts_ContactsByRepIdOfficeIdAll
	END
GO

PRINT 'Creating FUNCTION fxRepts_ContactsByRepIdOfficeIdAll'
GO
/******************************************************************************
**		File: fxRepts_ContactsByRepIdOfficeIdAll.sql
**		Name: fxRepts_ContactsByRepIdOfficeIdAll
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
CREATE FUNCTION dbo.fxRepts_ContactsByRepIdOfficeIdAll
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
	[ContactID] [int] NOT NULL
	, [RepCompanyID] [nvarchar](50) NOT NULL
	, [TeamLocationId] [int] NULL
	, [Latitude] [decimal](9, 6) NOT NULL
	, [Longitude] [decimal](9, 6) NOT NULL
	, [FirstName] [VARCHAR](30) NOT NULL
	, [LastName] [VARCHAR](30) NOT NULL
)
AS
BEGIN
	-- EXECUTE 
	INSERT INTO @ParsedList (
		ContactID
		, RepCompanyID
		, TeamLocationId
		, Longitude
		, Latitude
		, FirstName
		, LastName
	)
	SELECT 
		SLCN.ContactID
		, RepCompanyID
		, TeamLocationId
		, Longitude
		, Latitude
		, ISNULL(SLCN.FirstName, '') AS FirstName
		, ISNULL(SLCN.LastName, '') AS LastName
	FROM
		[NXSE_SALES].[dbo].[SL_Contacts] AS SLC WITH (NOLOCK)
		INNER JOIN [NXSE_SALES].[dbo].[SL_ContactNotes] AS SLCN WITH (NOLOCK)
		ON
			(SLCN.ContactID = SLC.ID)
	WHERE
		(SLC.CreatedOn BETWEEN @StartDate AND @EndDate)
		AND (@officeId IS NULL OR (SLC.TeamLocationId = @officeId))
		AND (@SalesRepID IS NULL OR (SLC.RepCompanyID = @SalesRepID));

	RETURN;
END
GO

/** Execute */
SELECT * FROM [dbo].fxRepts_ContactsByRepIdOfficeIdAll(NULL, NULL, NULL, '7/9/2015', '7/18/2015');