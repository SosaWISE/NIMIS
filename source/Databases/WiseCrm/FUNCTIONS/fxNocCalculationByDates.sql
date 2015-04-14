USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxNocCalculationByDates')
	BEGIN
		PRINT 'Dropping FUNCTION fxNocCalculationByDates'
		DROP FUNCTION  dbo.fxNocCalculationByDates
	END
GO

PRINT 'Creating FUNCTION fxNocCalculationByDates'
GO
/******************************************************************************
**		File: fxNocCalculationByDates.sql
**		Name: fxNocCalculationByDates
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
**		Date: 04/11/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/11/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxNocCalculationByDates
(
	@ContractSignedDate DATETIME
	, @InstallDate DATETIME
)
RETURNS DATETIME
AS
BEGIN
	/** Declarations */
	DECLARE @DateTimeResult DATETIME
		, @Count INT = 0;

	/** Initialize */
	SET @DateTimeResult = ISNULL(@ContractSignedDate, ISNULL(@InstallDate, NULL))

	/** Check that there is a Date */
	IF (@DateTimeResult IS NOT NULL)
	BEGIN

		/** Loop to check for holidays */
		WHILE(@Count < 3)
		BEGIN
--			PRINT 'COUNT: ' + CAST (@Count AS VARCHAR);
			SELECT @DateTimeResult = DATEADD(dd, 1, @DateTimeResult);

			IF(NOT EXISTS(SELECT * FROM [dbo].[MC_Holidays] WHERE (CAST(HolidayDate AS DATETIME) = CAST(@DateTimeResult AS DATETIME)) AND (IsActive = 1 AND IsDeleted = 0)))
			BEGIN
				/** Check to make sure it is not a Saturday or Sunday.*/
				DECLARE @DayOfWeek VARCHAR(30);

				SET @DayOfWeek  = DATENAME(dw, @DateTimeResult);
				IF(/*@DayOfWeek <> 'SATURDAY' AND */@DayOfWeek <> 'SUNDAY')
				BEGIN
					SET @Count = @Count + 1;
				END
				--ELSE
				--BEGIN
				--	PRINT 'SKIP BECAUSE ITs Saturday or Sunday';
				--END
			END
	--		ELSE
	--		BEGIN
	--			PRINT 'SKIP because of Holiday';
	--		END

		END
	END

	RETURN @DateTimeResult;
END
GO

SELECT [dbo].fxNocCalculationByDates(NULL, '4/13/2015')