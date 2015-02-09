USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'FUNCTION_TEMPLATE')
	BEGIN
		PRINT 'Dropping FUNCTION FUNCTION_TEMPLATE'
		DROP FUNCTION  dbo.FUNCTION_TEMPLATE
	END
GO

PRINT 'Creating FUNCTION FUNCTION_TEMPLATE'
GO
/******************************************************************************
**		File: FUNCTION_TEMPLATE.sql
**		Name: FUNCTION_TEMPLATE
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
CREATE FUNCTION dbo.FUNCTION_TEMPLATE
(
	@InterimPanelTypeID NVARCHAR(10)
)
RETURNS VARCHAR(20)
AS
BEGIN
	/** Declarations */
	DECLARE @PanelTypeID VARCHAR(20);

	/** Execute actions. */
	SELECT
		@PanelTypeID = PanelTypeId
	FROM
		[dbo].[SAE_InterimPanelTypeMap]
	WHERE
		(InterimPanelTypeID = @InterimPanelTypeID);

	RETURN @PanelTypeID;
END
GO