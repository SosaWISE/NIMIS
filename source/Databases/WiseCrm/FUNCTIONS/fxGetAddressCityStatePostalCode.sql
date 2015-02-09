USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetAddressCityStatePostalCode')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetAddressCityStatePostalCode'
		DROP FUNCTION  dbo.fxGetAddressCityStatePostalCode
	END
GO

PRINT 'Creating FUNCTION fxGetAddressCityStatePostalCode'
GO
/******************************************************************************
**		File: fxGetAddressCityStatePostalCode.sql
**		Name: fxGetAddressCityStatePostalCode
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
**	03/14/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetAddressCityStatePostalCode
(
	@City NVARCHAR(50)
	, @State NVARCHAR(50)
	, @PostalCode VARCHAR(5)
	, @PlusFour VARCHAR(4)
)
RETURNS NVARCHAR(150)
AS
BEGIN
	/** Declarations */
	DECLARE @Result NVARCHAR(150);

	/** Initialize */
	SET @City = LTRIM(RTRIM(@City));
	SET @State = LTRIM(RTRIM(@State));
	SET @PostalCode = LTRIM(RTRIM(@PostalCode));
	SET @PlusFour = LTRIM(RTRIM(@PlusFour));

	/** Build fullname */
	SELECT @Result =
		@City
		+
		CASE
			WHEN @State IS NULL OR @State = '' THEN ''
			ELSE ' ' + @State
		END
		+
		CASE
			WHEN @PostalCode IS NULL OR @PostalCode = '' THEN ''
			ELSE ' ' + @PostalCode
		END
		+
		CASE
			WHEN @PlusFour IS NULL OR @PlusFour = '' THEN ''
			ELSE '-' + @PlusFour
		END
	
	RETURN @Result;
END
GO

SELECT [dbo].fxGetAddressCityStatePostalCode('  Miami Beach Florida   ', '   FL   ', '84097  ', '3689      ') AS [CityStZip];