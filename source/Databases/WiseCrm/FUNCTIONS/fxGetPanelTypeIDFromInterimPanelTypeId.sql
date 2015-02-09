USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetPanelTypeIDFromInterimPanelTypeId')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetPanelTypeIDFromInterimPanelTypeId'
		DROP FUNCTION  dbo.fxGetPanelTypeIDFromInterimPanelTypeId
	END
GO

PRINT 'Creating FUNCTION fxGetPanelTypeIDFromInterimPanelTypeId'
GO
/******************************************************************************
**		File: fxGetPanelTypeIDFromInterimPanelTypeId.sql
**		Name: fxGetPanelTypeIDFromInterimPanelTypeId
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
**		Date: 07/10/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	07/10/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetPanelTypeIDFromInterimPanelTypeId
(
	@InterimPanelTypeID NVARCHAR(10)
)
RETURNS VARCHAR(20)
AS
BEGIN
	/** Declarations */
	DECLARE @PanelTypeID VARCHAR(20) = '2GIG';

	/** Execute actions. */
	IF (EXISTS(SELECT * FROM [dbo].[SAE_InterimPanelTypeMap] WHERE (InterimPanelTypeID = @InterimPanelTypeID)))
	BEGIN
		SELECT
			@PanelTypeID = PanelTypeId
		FROM
			[dbo].[SAE_InterimPanelTypeMap]
		WHERE
			(InterimPanelTypeID = @InterimPanelTypeID);
	END

	RETURN @PanelTypeID;
END
GO

/** TEST
SELECT dbo.fxGetPanelTypeIDFromInterimPanelTypeId('FA-148 C') AS PanelTypeID
*/