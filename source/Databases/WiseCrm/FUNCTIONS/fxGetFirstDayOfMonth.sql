USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetFirstDayOfMonth')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetFirstDayOfMonth'
		DROP FUNCTION  dbo.fxGetFirstDayOfMonth
	END
GO

PRINT 'Creating FUNCTION fxGetFirstDayOfMonth'
GO
/******************************************************************************
**		File: fxGetFirstDayOfMonth.sql
**		Name: fxGetFirstDayOfMonth
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
CREATE FUNCTION dbo.fxGetFirstDayOfMonth
(
	@Date DATETIME
)
RETURNS DATETIME
AS
BEGIN
	RETURN DATEADD(month, DATEDIFF(month, 0, @Date), 0)
END
GO

/*
SELECT dbo.fxGetFirstDayOfMonth('1/28/2015');
SELECT dbo.fxGetFirstDayOfMonth('2/28/2015');
SELECT dbo.fxGetFirstDayOfMonth('3/28/2015');
SELECT dbo.fxGetFirstDayOfMonth('4/28/2015');
SELECT dbo.fxGetFirstDayOfMonth('5/28/2015');
SELECT dbo.fxGetFirstDayOfMonth('6/28/2015');
SELECT dbo.fxGetFirstDayOfMonth('7/28/2015');
SELECT dbo.fxGetFirstDayOfMonth('8/28/2015');
SELECT dbo.fxGetFirstDayOfMonth('9/28/2015');
SELECT dbo.fxGetFirstDayOfMonth('10/28/2015');
SELECT dbo.fxGetFirstDayOfMonth('11/28/2015');
SELECT dbo.fxGetFirstDayOfMonth('12/28/2015');
*/