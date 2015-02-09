USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_EventsReporting')
	BEGIN
		PRINT 'Dropping Procedure custGS_EventsReporting'
		DROP  Procedure  dbo.custGS_EventsReporting
	END
GO

PRINT 'Creating Procedure custGS_EventsReporting'
GO
/******************************************************************************
**		File: custGS_EventsReporting.sql
**		Name: custGS_EventsReporting
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
**		Auth: Andres Sosa
**		Date: 00/00/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	00/00/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custGS_EventsReporting
(
	@CustomerMasterFileID BIGINT = NULL
	, @AccountID BIGINT = NULL
	, @EventTypeID VARCHAR(20) = NULL
	, @GeoFenceID BIGINT = NULL
	, @StartDate DATETIME = NULL
	, @EndDate DATETIME = NULL
	, @PageSize INT = 5
	, @PageNumber INT = 1
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Initialize. */
	DECLARE @StartRow INT;
	DECLARE @EndRow INT;

	-- Set the Start Row
	SET @StartRow = (@PageSize * @PageNumber) - @PageSize + 1;
	SET @EndRow = (@StartRow + @PageSize - 1);
	
	PRINT '@StartRow= ' + CAST(@StartRow AS VARCHAR);
	PRINT '@EndRow= ' + CAST(@EndRow AS VARCHAR);
	
	/** Execute Query */
	SELECT
		V1.EventID
		, V1.EventTypeId
		, V1.EventType
		, V1.EventTypeUi
		, V1.EventShortDesc
		, V1.AccountId
		, V1.CustomerID
		, V1.CustomerMasterFileID
		, V1.GeoFenceId
		, V1.AccountName
		, V1.EventName
		, V1.EventDate
		, V1.Lattitude
		, V1.Longitude
		, V1.RowNum
	FROM
		(
		SELECT
			*
			, ROW_NUMBER() OVER (PARTITION BY EVN.CustomerMasterFileId ORDER BY EVN.EventDate DESC) AS RowNum
		FROM
			[dbo].vwGS_Events AS EVN WITH (NOLOCK)
		WHERE
			((@CustomerMasterFileID IS NULL) OR (EVN.CustomerMasterFileId = @CustomerMasterFileID))
			AND ((@AccountID IS NULL) OR (EVN.AccountId = @AccountID))
			AND ((@GeoFenceID IS NULL) OR (EVN.GeoFenceId = @GeoFenceID))
			AND ((@StartDate IS NULL) OR (@EndDate IS NULL) OR (EVN.EventDate BETWEEN @StartDate AND @EndDate))
		) AS V1
	WHERE
		(V1.RowNum BETWEEN @StartRow AND @EndRow);
END
GO

GRANT EXEC ON dbo.custGS_EventsReporting TO PUBLIC
GO

EXEC dbo.custGS_EventsReporting @CustomerMasterFileID = 3000089;
--SELECT * FROM [dbo].vwGS_Events AS EVN WITH (NOLOCK)