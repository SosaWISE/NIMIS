USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxNocCalculationByAccountID')
	BEGIN
		PRINT 'Dropping FUNCTION fxNocCalculationByAccountID'
		DROP FUNCTION  dbo.fxNocCalculationByAccountID
	END
GO

PRINT 'Creating FUNCTION fxNocCalculationByAccountID'
GO
/******************************************************************************
**		File: fxNocCalculationByAccountID.sql
**		Name: fxNocCalculationByAccountID
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
CREATE FUNCTION dbo.fxNocCalculationByAccountID
(
	@AccountID BIGINT
)
RETURNS DATETIME
AS
BEGIN
	/** Declarations */
	DECLARE @DateTimeResult DATETIME
		, @Count INT = 0;

	/** Initialize */
	SELECT 
		@DateTimeResult = ISNULL(MSASI.ContractSignedDate, ISNULL(MSASI.InstallDate, MSASI.CreatedOn))
	FROM
		[dbo].[MS_AccountSalesInformations] AS MSASI WITH (NOLOCK)
	WHERE
		(AccountID = @AccountID);

	/** Loop to check for holidays */
	WHILE(@Count < 3)
	BEGIN
		SELECT @DateTimeResult = DATEADD(dd, 1, @DateTimeResult);

		IF(NOT EXISTS(SELECT * FROM [dbo].[MC_Holidays] WHERE (CAST(HolidayDate AS DATETIME) = CAST(@DateTimeResult AS DATETIME))))
		BEGIN
			/** Check to make sure it is not a Saturday or Sunday.*/
			DECLARE @DayOfWeek VARCHAR(30);

			SET @DayOfWeek  = DATENAME(dw, @DateTimeResult);
			IF(@DayOfWeek <> 'SATURDAY' AND @DayOfWeek <> 'SUNDAY')
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
----			SET @Count = @Count + 1;
--		END

	END

	RETURN @DateTimeResult;
END
GO

SELECT [dbo].fxNocCalculationByAccountID(191203)