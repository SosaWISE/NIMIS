USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND (name = 'fxGetDeviceName'))
	BEGIN
		PRINT 'Dropping FUNCTION fxGetDeviceName'
		DROP FUNCTION  dbo.fxGetDeviceName
	END
GO

PRINT 'Creating FUNCTION fxGetDeviceName'
GO
/******************************************************************************
**		File: fxGetDeviceName.sql
**		Name: fxGetDeviceName
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
**		Date: 06/14/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	06/14/2013	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetDeviceName
(
	@SystemTypeId VARCHAR(20)
	, @PanelTypeId VARCHAR(20)
	, @AccountName NVARCHAR(100) = NULL
)
RETURNS NVARCHAR(50)
AS
BEGIN
	/** Declarations. */
	DECLARE @Result NVARCHAR(50) = '[No Name Set]';
	DECLARE @SystemTypeName VARCHAR(50);
	DECLARE @PanelTypeName  VARCHAR(50);

	/** Check to see if AccountName is set. */
	if (@AccountName IS NOT NULL) RETURN @AccountName;

	/** Get the System Type. */
	SELECT @SystemTypeName = SystemTypeName FROM [dbo].MS_AccountSystemTypes WHERE (SystemTypeID = @SystemTypeId);
	SELECT @PanelTypeName = PanelTypeName FROM [dbo].MS_AccountPanelTypes WHERE (PanelTypeID = @PanelTypeId);

	/** Set the name. */
	SET @Result = @PanelTypeName + ' [' + @SystemTypeName + ']';

	/** Return result. */
	RETURN @Result;
END
GO