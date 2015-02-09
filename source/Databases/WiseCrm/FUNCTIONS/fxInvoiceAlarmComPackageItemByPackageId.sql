USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxInvoiceAlarmComPackageItemByPackageId')
	BEGIN
		PRINT 'Dropping FUNCTION fxInvoiceAlarmComPackageItemByPackageId'
		DROP FUNCTION  dbo.fxInvoiceAlarmComPackageItemByPackageId
	END
GO

PRINT 'Creating FUNCTION fxInvoiceAlarmComPackageItemByPackageId'
GO
/******************************************************************************
**		File: fxInvoiceAlarmComPackageItemByPackageId.sql
**		Name: fxInvoiceAlarmComPackageItemByPackageId
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
CREATE FUNCTION dbo.fxInvoiceAlarmComPackageItemByPackageId
(
	@AlarmComPackageId NVARCHAR(50)
	, @DealerId INT
)
RETURNS VARCHAR(50)
AS
BEGIN
	/** Declarations */
	DECLARE @Result VARCHAR(50);
	SELECT
		@Result = CASE
			WHEN @AlarmComPackageId = 'ADVINT' THEN 'CELL_SRV_AC_AI'
			WHEN @AlarmComPackageId = 'BSCINT' THEN 'CELL_SRV_AC_BI'
			WHEN @AlarmComPackageId = 'INTGLD' THEN 'CELL_SRV_AC_IG'
			WHEN @AlarmComPackageId = 'WRLFWN' THEN 'CELL_SRV_AC_WSF'
			ELSE 'CELL_SRV_AC_WSF'
		END

	RETURN @Result;
END
GO

/** 
 DECLARE @ID NVARCHAR(50) = '';
SELECT
 CASE
	WHEN @ID = 'ADVINT' THEN 'CELL_SRV_AC_AI'
	WHEN @ID = 'BSCINT' THEN 'CELL_SRV_AC_BI'
	WHEN @ID = 'INTGLD' THEN 'CELL_SRV_AC_IG'
	WHEN @ID = 'WRLFWN' THEN 'CELL_SRV_AC_WSF'
	ELSE 'CELL_SRV_AC_WSF'
END
 */