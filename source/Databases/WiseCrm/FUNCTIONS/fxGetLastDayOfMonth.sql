USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetLastDayOfMonth')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetLastDayOfMonth'
		DROP FUNCTION  dbo.fxGetLastDayOfMonth
	END
GO

PRINT 'Creating FUNCTION fxGetLastDayOfMonth'
GO
/******************************************************************************
**		File: fxGetLastDayOfMonth.sql
**		Name: fxGetLastDayOfMonth
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
**		Date: 07/30/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	07/30/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetLastDayOfMonth
(
	@Date DATETIME
)
RETURNS DATETIME
AS
BEGIN
	RETURN DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, @Date) + 1, 0));
END
GO

/*
SELECT dbo.fxGetLastDayOfMonth('1/28/2015');
SELECT dbo.fxGetLastDayOfMonth('2/28/2015');
SELECT dbo.fxGetLastDayOfMonth('3/28/2015');
SELECT dbo.fxGetLastDayOfMonth('4/28/2015');
SELECT dbo.fxGetLastDayOfMonth('5/28/2015');
SELECT dbo.fxGetLastDayOfMonth('6/28/2015');
SELECT dbo.fxGetLastDayOfMonth('7/28/2015');
SELECT dbo.fxGetLastDayOfMonth('8/28/2015');
SELECT dbo.fxGetLastDayOfMonth('9/28/2015');
SELECT dbo.fxGetLastDayOfMonth('10/28/2015');
SELECT dbo.fxGetLastDayOfMonth('11/28/2015');
SELECT dbo.fxGetLastDayOfMonth('12/28/2015');
*/