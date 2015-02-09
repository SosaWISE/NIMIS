USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_EventsPaggingMaster')
	BEGIN
		PRINT 'Dropping Procedure custGS_EventsPaggingMaster'
		DROP  Procedure  dbo.custGS_EventsPaggingMaster
	END
GO

PRINT 'Creating Procedure custGS_EventsPaggingMaster'
GO
/******************************************************************************
**		File: custGS_EventsPaggingMaster.sql
**		Name: custGS_EventsPaggingMaster
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
**		Auth: Andrés Sosa
**		Date: 09/12/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	09/12/2012	Andrés Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custGS_EventsPaggingMaster
(
	@CustomerMasterFileID BIGINT
	, @CustomerId BIGINT = NULL
	, @StartDate DATETIME
	, @EndDate DATETIME
	, @PageSize INT = 5
	, @PageNumber INT = 1
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON;
	
	/** Initialize. */
	DECLARE @StartRow INT;
	DECLARE @EndRow INT;

	-- Set the Start Row
	SET @StartRow = (@PageSize * @PageNumber) - @PageSize + 1;
	SET @EndRow = (@StartRow + @PageSize - 1);
	
	PRINT '@StartRow= ' + CAST(@StartRow AS VARCHAR);
	PRINT '@EndRow= ' + CAST(@EndRow AS VARCHAR);
	/** Build Query */
	SELECT
		V1.EventID
		, V1.EventTypeId
		, V1.EventType
		, V1.EventTypeUi
		, V1.EventShortDesc
		, V1.AccountId
		, V1.CustomerID
		, V1.CustomerMasterFileId
		, V1.AccountName
		, V1.EventName
		, V1.EventDate
		, V1.Lattitude
		, V1.Longitude
	FROM
	(	
		SELECT 
			GSIn.EventID
			, GSIn.EventTypeId
			, GSIn.EventType
			, GSIn.EventTypeUi
			, GSIn.EventShortDesc
			, GSIn.AccountId
			, GSIn.CustomerID
			, GSIn.CustomerMasterFileId
			, GSIn.AccountName
			, GSIn.EventName
			, GSIn.EventDate
			, GSIn.Lattitude
			, GSIn.Longitude
			, ROW_NUMBER() OVER (PARTITION BY GSIn.CustomerMasterFileId ORDER BY GSIn.EventDate DESC) AS RowNum
		FROM
			[dbo].vwGS_Events AS GSIn
		WHERE 
			(GSIn.CustomerMasterFileId = @CustomerMasterFileID)
			AND ((@CustomerId IS NULL) OR (GSIn.CustomerID = @CustomerId))
			--AND (@StartDate IS NULL OR GSIn.EventDate >= @StartDate)
			--AND (@EndDate IS NULL OR GSIn.EventDate <= @EndDate)
			AND (GSIn.EventDate BETWEEN @StartDate AND @EndDate)
	) AS V1
	WHERE
		(V1.RowNum BETWEEN @StartRow AND @EndRow);
		
END
GO

GRANT EXEC ON dbo.custGS_EventsPaggingMaster TO PUBLIC
GO

/**
EXEC dbo.custGS_EventsPaggingMaster 3000035, 100195, '5/19/2013', '6/19/2013', 24, 2
EXEC dbo.custGS_EventsPaggingMaster 3000035, null, '5/19/2013', '6/19/2013', 24, 2

**/