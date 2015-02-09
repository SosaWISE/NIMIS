USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxMsAccountCellUnitTypeGet')
	BEGIN
		PRINT 'Dropping FUNCTION fxMsAccountCellUnitTypeGet'
		DROP FUNCTION  dbo.fxMsAccountCellUnitTypeGet
	END
GO

PRINT 'Creating FUNCTION fxMsAccountCellUnitTypeGet'
GO
/******************************************************************************
**		File: fxMsAccountCellUnitTypeGet.sql
**		Name: fxMsAccountCellUnitTypeGet
**		Desc: 
**		Type of Service		0 to 9
**		Provider			10 to 11
**		Package				13 to 14
**
**		SELECT SUBSTRING('CELL_SRV_AC_AI', 0, 9);  -- Service Type
**		SELECT SUBSTRING('CELL_SRV_AC_AI', 10, 2); -- Provider
**		SELECT SUBSTRING('CELL_SRV_AC_AI', 13, 2); -- Package
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
**		Date: 06/07/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	06/07/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxMsAccountCellUnitTypeGet
(
	@CellPackageItemId VARCHAR(50)
)
RETURNS VARCHAR(50)
AS
BEGIN
	-- DECLARATIONS
	DECLARE @Result VARCHAR(50) = '[NO CELL]'
		, @Len INT = 0;

	-- Make sure that the length is correct.
	SELECT @Len = LEN(@CellPackageItemId);
	IF (@Len < 11) RETURN NULL;

	-- Check that there this is a cell service type
	SELECT @Result = SUBSTRING(@CellPackageItemId, 0, 9);
	IF (@Result <> 'CELL_SRV') RETURN NULL;

	-- Get the provider
	SELECT @Result = SUBSTRING(@CellPackageItemId, 10, 2);

	SELECT
		@Result = CASE
			WHEN @Result = 'AC' THEN 'Alarm.com'
			WHEN @Result = 'TG' THEN 'Telguard'
			WHEN @Result = 'HW' THEN 'HW AlarmNet'
		END

	-- Return Result
	RETURN @Result;
END
GO
/*
SELECT [dbo].fxMsAccountCellUnitTypeGet('CELL_SRV_AC_AI') AS [Alarm COM];
SELECT [dbo].fxMsAccountCellUnitTypeGet('CELL_SRV_AC_BI') AS [Alarm COM];
SELECT [dbo].fxMsAccountCellUnitTypeGet('CELL_SRV_AC_IG') AS [Alarm COM];
SELECT [dbo].fxMsAccountCellUnitTypeGet('CELL_SRV_AC_WSF') AS [Alarm COM];
SELECT [dbo].fxMsAccountCellUnitTypeGet('CELL_SRV_TG') AS [Tellgaurd];
SELECT [dbo].fxMsAccountCellUnitTypeGet('CELL_SRV_HW') AS [Alarm Net];
SELECT [dbo].fxMsAccountCellUnitTypeGet('42423') AS [Alarm Type];
SELECT [dbo].fxMsAccountCellUnitTypeGet('CELL_VRS_HW') AS [Alarm Type];

*/