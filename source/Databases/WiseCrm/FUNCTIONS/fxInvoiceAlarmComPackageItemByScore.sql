USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxInvoiceAlarmComPackageItemByScore')
	BEGIN
		PRINT 'Dropping FUNCTION fxInvoiceAlarmComPackageItemByScore'
		DROP FUNCTION  dbo.fxInvoiceAlarmComPackageItemByScore
	END
GO

PRINT 'Creating FUNCTION fxInvoiceAlarmComPackageItemByScore'
GO
/******************************************************************************
**		File: fxInvoiceAlarmComPackageItemByScore.sql
**		Name: fxInvoiceAlarmComPackageItemByScore
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
CREATE FUNCTION dbo.fxInvoiceAlarmComPackageItemByScore
(
	@Score INT
	, @DefaultItemId VARCHAR(50) = 'CELL_SRV_AC_BI'
)
RETURNS VARCHAR(50)
AS
BEGIN
	IF (@Score IS NULL) RETURN @DefaultItemId;
	
	IF (@Score < 650) RETURN 'CELL_SRV_AC_BI';

	RETURN 'CELL_SRV_AC_AI';
END
GO

/** 
SELECT dbo.fxInvoiceAlarmComPackageItemByScore(NULL, 'MON_CONT_5003') AS ItemID;
SELECT dbo.fxInvoiceAlarmComPackageItemByScore(300, 'MON_CONT_5003') AS ItemID;
SELECT dbo.fxInvoiceAlarmComPackageItemByScore(650, 'MON_CONT_5003') AS ItemID;
 */