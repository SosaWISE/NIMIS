USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxInvoiceMonthlyMonitoringRateItemByScore')
	BEGIN
		PRINT 'Dropping FUNCTION fxInvoiceMonthlyMonitoringRateItemByScore'
		DROP FUNCTION  dbo.fxInvoiceMonthlyMonitoringRateItemByScore
	END
GO

PRINT 'Creating FUNCTION fxInvoiceMonthlyMonitoringRateItemByScore'
GO
/******************************************************************************
**		File: fxInvoiceMonthlyMonitoringRateItemByScore.sql
**		Name: fxInvoiceMonthlyMonitoringRateItemByScore
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
**		Date: 02/11/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/11/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxInvoiceMonthlyMonitoringRateItemByScore
(
	@Score INT
	, @DefaultItemId VARCHAR(50) = 'MON_CONT_5000'
)
RETURNS VARCHAR(50)
AS
BEGIN
	IF (@Score IS NULL) RETURN @DefaultItemId;
	
	IF (@Score < 650) RETURN 'MON_CONT_5000';

	RETURN 'MON_CONT_5001';
END
GO

/** 
SELECT dbo.fxInvoiceMonthlyMonitoringRateItemByScore(NULL, 'MON_CONT_5003') AS ItemID;
SELECT dbo.fxInvoiceMonthlyMonitoringRateItemByScore(300, 'MON_CONT_5003') AS ItemID;
SELECT dbo.fxInvoiceMonthlyMonitoringRateItemByScore(650, 'MON_CONT_5003') AS ItemID;
 */