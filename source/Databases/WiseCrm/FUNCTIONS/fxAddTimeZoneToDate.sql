USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxAddTimeZoneToDate')
	BEGIN
		PRINT 'Dropping FUNCTION fxAddTimeZoneToDate'
		DROP FUNCTION  dbo.fxAddTimeZoneToDate
	END
GO

PRINT 'Creating FUNCTION fxAddTimeZoneToDate'
GO
/******************************************************************************
**		File: fxAddTimeZoneToDate.sql
**		Name: fxAddTimeZoneToDate
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
**		Date: 03/14/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	03/14/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxAddTimeZoneToDate
(
	@DateTime DATETIME,
	@ZipCode VARCHAR(5)
)
RETURNS DATETIME
AS
BEGIN
	/** Declarations */
	DECLARE @DateTimeResult DATETIME
			,@TimeZone	INT

	/** Initialize */

	SET @TimeZone = (SELECT [TimeZone] FROM [dbo].[SE_ZipCodes] SEZ WHERE (SEZ.ZipCode = @ZipCode))

	/** Build fullname */
	SET @DateTimeResult = (SELECT DATEADD(HOUR,@TimeZone,@DateTime))
	
	
	RETURN @DateTimeResult;
END
GO

