USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxTranslateInterimEquipment')
	BEGIN
		PRINT 'Dropping FUNCTION fxTranslateInterimEquipment'
		DROP FUNCTION  dbo.fxTranslateInterimEquipment
	END
GO

PRINT 'Creating FUNCTION fxTranslateInterimEquipment'
GO
/******************************************************************************
**		File: fxTranslateInterimEquipment.sql
**		Name: fxTranslateInterimEquipment
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
**		Date: 05/24/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	05/24/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxTranslateInterimEquipment
(
	@EquipmentID INT
)
RETURNS VARCHAR(50)
AS
BEGIN
	/** Declarations. */
	DECLARE @ItemID VARCHAR(50) = 'EQPM_EXST_MS' + CAST(@EquipmentID AS VARCHAR)

	/** Init values. */
	RETURN @ItemID
END
GO