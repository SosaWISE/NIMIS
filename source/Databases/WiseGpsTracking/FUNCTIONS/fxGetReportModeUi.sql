USE [WISE_GPSTRACKING]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND (name = 'fxGetReportModeUi'))
	BEGIN
		PRINT 'Dropping FUNCTION fxGetReportModeUi'
		DROP FUNCTION  dbo.fxGetReportModeUi
	END
GO

PRINT 'Creating FUNCTION fxGetReportModeUi'
GO
/******************************************************************************
**		File: fxGetReportModeUi.sql
**		Name: fxGetReportModeUi
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
**		Date: 07/02/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	07/02/2013	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetReportModeUi
(
	@ReportModeID VARCHAR(3)
)
RETURNS NVARCHAR(6)
AS
BEGIN
	/** Declarations. */
	DECLARE @Result NVARCHAR(50) = 'unknown';

	/** Get the System Type. */
	SET @Result = CASE
			WHEN @ReportModeID = '0' THEN 'UNCERT' -- Uncertain
			WHEN @ReportModeID = '1' THEN 'INCLUS' -- Exit Alert	Inclusion Zone
			WHEN @ReportModeID = '2' THEN 'EXCLUS' -- Enter Alert	Exclusion Zone
			WHEN @ReportModeID = '3' THEN 'INCEXC' -- Exit Enter Alert	Both Inclusion and Exclusion Zone
			WHEN @ReportModeID = '4' THEN 'DELETE' -- Flagged for deletion from the device.
			ELSE 'UNDEF'
		END 

	/** Return result. */
	RETURN @Result;
END
GO