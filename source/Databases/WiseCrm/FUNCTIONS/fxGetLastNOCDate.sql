USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetLastNOCDate')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetLastNOCDate'
		DROP FUNCTION  dbo.fxGetLastNOCDate
	END
GO

PRINT 'Creating FUNCTION fxGetLastNOCDate'
GO
/******************************************************************************
**		File: fxGetLastNOCDate.sql
**		Name: fxGetLastNOCDate
**		Desc: When and order is placed the customer has 3 business days to cancel.
**		This function accepts the date of the order and returns a date 3 days after
**		the date passed.
**
**		This template can be customized:
**              
**		Return values: Date 3 business days after the date passed to the function
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**		Order Date (@OrderDate)			3 Business days after Order Date
**										(@CancellationDeadlineDate)
**
**		Auth: Bob McFadden
**		Date: 09/09/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	09/09/2014	Bob McFadden	Created
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetLastNOCDate
(
	@OrderDate DATETIME
)
RETURNS DATETIME
AS
BEGIN
	/** Declarations */
	DECLARE @NumBusinessDays INT
	DECLARE @CancellationDeadlineDate DATETIME
	DECLARE @daysAdded INT

	/** Execute actions */
	SET @NumBusinessDays = 3

	/* INITIALIZE */
	SET @CancellationDeadlineDate = @OrderDate
	SET @daysAdded = 1

	/* LOOP */
	WHILE @daysAdded <= @NumBusinessDays
	-- stop once the number of business days is reached
	BEGIN

		-- increment @CancellationDeadlineDate a day at a time.
		SET @CancellationDeadlineDate = DateAdd(DAY, 1, @CancellationDeadlineDate)

		-- increment @daysAdded if it's a weekday and not a Federal holiday.
		IF DATEPART(DW, @CancellationDeadlineDate) IN (2,3,4,5,6) -- Monday thru Friday
			AND @CancellationDeadlineDate NOT IN (SELECT HolidayDate FROM WISE_CRM.dbo.MC_Holidays WHERE HolidayDate = @CancellationDeadlineDate)
		SET @daysAdded = @daysAdded + 1
	END

	RETURN @CancellationDeadlineDate;
END
GO

/* TEST THE FUNCTION
SELECT 
dbo.fxGetLastNOCDate('11/21/14'),
dbo.fxGetLastNOCDate('11/22/14'),
dbo.fxGetLastNOCDate('11/23/14'),
dbo.fxGetLastNOCDate('11/24/14'),
dbo.fxGetLastNOCDate('11/25/14'),
dbo.fxGetLastNOCDate('11/26/14')
*/