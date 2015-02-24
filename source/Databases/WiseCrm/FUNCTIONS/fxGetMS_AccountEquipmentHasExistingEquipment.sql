USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetMS_AccountEquipmentHasExistingEquipment')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetMS_AccountEquipmentHasExistingEquipment'
		DROP FUNCTION  dbo.fxGetMS_AccountEquipmentHasExistingEquipment
	END
GO

PRINT 'Creating FUNCTION fxGetMS_AccountEquipmentHasExistingEquipment'
GO
/******************************************************************************
**		File: fxGetMS_AccountEquipmentHasExistingEquipment.sql
**		Name: fxGetMS_AccountEquipmentHasExistingEquipment
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
**		Date: 02/23/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/23/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetMS_AccountEquipmentHasExistingEquipment
(
	@AccountId BIGINT
)
RETURNS BIT
AS
BEGIN
	/** Declarations */
	DECLARE @HasExistingEquipment BIT;

	/** Execute actions. */
	IF(EXISTS(SELECT * FROM [dbo].[MS_AccountEquipment] WHERE (AccountId = @AccountId) AND (IsExisting = 1) AND (IsActive = 1) AND (IsDeleted = 0)))
	BEGIN
		SET @HasExistingEquipment = 1;
	END
	ELSE
	BEGIN
		SET @HasExistingEquipment = 0;
	END

	RETURN @HasExistingEquipment;
END
GO