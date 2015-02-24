USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetMS_AccountEquipmentPoints')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetMS_AccountEquipmentPoints'
		DROP FUNCTION  dbo.fxGetMS_AccountEquipmentPoints
	END
GO

PRINT 'Creating FUNCTION fxGetMS_AccountEquipmentPoints'
GO
/******************************************************************************
**		File: fxGetMS_AccountEquipmentPoints.sql
**		Name: fxGetMS_AccountEquipmentPoints
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
CREATE FUNCTION dbo.fxGetMS_AccountEquipmentPoints
(
	@AccountId BIGINT
)
RETURNS FLOAT
AS
BEGIN
	/** Declarations */
	DECLARE @Points FLOAT;

	/** Execute actions. */
	SELECT @Points = SUM(ActualPoints) FROM [dbo].[MS_AccountEquipment] WHERE (AccountId = @AccountId) AND (IsExisting = 1) AND (IsActive = 1) AND (IsDeleted = 0)

	RETURN @Points;
END
GO