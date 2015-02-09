USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_EventsPagging')
	BEGIN
		PRINT 'Dropping Procedure custGS_EventsPagging'
		DROP  Procedure  dbo.custGS_EventsPagging
	END
GO

PRINT 'Creating Procedure custGS_EventsPagging'
GO
/******************************************************************************
**		File: custGS_EventsPagging.sql
**		Name: custGS_EventsPagging
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
CREATE Procedure dbo.custGS_EventsPagging
(
	@AccountID BIGINT
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
		, V1.AccountId
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
			, GSIn.AccountId
			, GSIn.EventName
			, GSIn.EventDate
			, GSIn.Lattitude
			, GSIn.Longitude
			, ROW_NUMBER() OVER (PARTITION BY GSIn.AccountId ORDER BY GSIn.EventDate DESC) AS RowNum
		FROM
			[dbo].vwGS_Events AS GSIn
		WHERE 
			(GSIn.AccountId = @AccountID)
			--AND (@StartDate IS NULL OR GSIn.EventDate >= @StartDate)
			--AND (@EndDate IS NULL OR GSIn.EventDate <= @EndDate)
			AND (GSIn.EventDate BETWEEN @StartDate AND @EndDate)
	) AS V1
	WHERE
		(V1.RowNum BETWEEN @StartRow AND @EndRow);
		
END
GO

GRANT EXEC ON dbo.custGS_EventsPagging TO PUBLIC
GO

EXEC dbo.custGS_EventsPagging 100164, '1/1/2012', '9/14/2012', 4, 2