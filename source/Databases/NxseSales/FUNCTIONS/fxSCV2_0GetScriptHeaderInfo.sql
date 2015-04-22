USE [NXSE_Sales]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxSCV2_0GetScriptHeaderInfo')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCV2_0GetScriptHeaderInfo'
		DROP FUNCTION  dbo.fxSCV2_0GetScriptHeaderInfo
	END
GO

PRINT 'Creating FUNCTION fxSCV2_0GetScriptHeaderInfo'
GO
/******************************************************************************
**		File: fxSCV2_0GetScriptHeaderInfo.sql
**		Name: fxSCV2_0GetScriptHeaderInfo
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
**		Auth: Andr�s E. Sosa
**		Date: 04/21/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/21/2015	Andr�s E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCV2_0GetScriptHeaderInfo
(
)
RETURNS 
@ResultList table
(
	CommissionPeriodID BIGINT
	, CommissionEngineID VARCHAR(10)
	, CommissionPeriodStrDate DATETIME
	, CommissionPeriodEndDate DATETIME
	, DEBUG_MODE VARCHAR(20)
	, [TRUNCATE] VARCHAR(20)
)
AS
BEGIN
	/** LOCALS */
	DECLARE @CommissionPeriodID BIGINT
		, @CommissionEngineID VARCHAR(10) = 'SCv2.0'
		, @CommissionPeriodStrDate DATETIME
		, @CommissionPeriodEndDate DATETIME
		, @DEBUG_MODE VARCHAR(20) = 'OFF'
		, @TRUNCATE VARCHAR(20) = 'OFF';

	SELECT @DEBUG_MODE = GlobalPropertyValue FROM [dbo].[SC_GlobalProperties] WHERE (GlobalPropertyID = 'DEBUG_MODE');
	SELECT @TRUNCATE   = GlobalPropertyValue FROM [dbo].[SC_GlobalProperties] WHERE (GlobalPropertyID = 'TRUNCATE');

	SELECT TOP 1
		@CommissionPeriodID = CommissionPeriodID
		, @CommissionPeriodEndDate = CommissionPeriodEndDate
		, @CommissionPeriodStrDate = DATEADD(d, -7, CommissionPeriodEndDate)
	FROM
		NXSE_Sales.dbo.SC_CommissionPeriods
	WHERE
		(CommissionEngineID = @CommissionEngineID)
	ORDER BY
		IsCurrent DESC
		, CommissionPeriodID DESC;

	/** INSERT INTO  */
	INSERT INTO @ResultList (
		CommissionPeriodID
		, CommissionEngineID
		, CommissionPeriodStrDate
		, CommissionPeriodEndDate
		, DEBUG_MODE
		, [TRUNCATE]
	) VALUES (
		@CommissionPeriodID
		, @CommissionEngineID
		, @CommissionPeriodStrDate
		, @CommissionPeriodEndDate
		, @DEBUG_MODE
		, @TRUNCATE
	);
	
	RETURN
END
GO