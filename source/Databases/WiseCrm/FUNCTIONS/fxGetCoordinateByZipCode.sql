USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetCoordinateByZipCode')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetCoordinateByZipCode'
		DROP FUNCTION  dbo.fxGetCoordinateByZipCode
	END
GO

PRINT 'Creating FUNCTION fxGetCoordinateByZipCode'
GO
/******************************************************************************
**		File: fxGetCoordinateByZipCode.sql
**		Name: fxGetCoordinateByZipCode
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
**		Date: 03/14/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	09/09/2014	Reagan Descartin	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetCoordinateByZipCode
(
	@ZipCode VARCHAR(10)
	,@CoordinateName VARCHAR(10)
)
RETURNS FLOAT
AS
BEGIN
	/** Declarations */
	DECLARE @Coordinate FLOAT;

	IF @CoordinateName ='Latitude'
	BEGIN
		SET @Coordinate = ISNULL( (SELECT SEZC.Latitude FROM [dbo].[SE_ZipCodes] SEZC WHERE SEZC.ZipCode = @ZipCode), NULL);
	END
	ELSE
	BEGIN
		SET @Coordinate = ISNULL( (SELECT SEZC.Longitude FROM [dbo].[SE_ZipCodes] SEZC WHERE SEZC.ZipCode = @ZipCode), NULL);
	END

	RETURN @Coordinate;
END
GO


/*
SELECT 'Latitude' FROM [dbo].[SE_ZipCodes] SEZC WHERE SEZC.ZipCode =00501

*/

