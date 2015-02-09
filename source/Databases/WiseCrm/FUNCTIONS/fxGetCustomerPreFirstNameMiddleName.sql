USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetCustomerPreFirstNameMiddleName')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetCustomerPreFirstNameMiddleName'
		DROP FUNCTION  dbo.fxGetCustomerPreFirstNameMiddleName
	END
GO

PRINT 'Creating FUNCTION fxGetCustomerPreFirstNameMiddleName'
GO
/******************************************************************************
**		File: fxGetCustomerPreFirstNameMiddleName.sql
**		Name: fxGetCustomerPreFirstNameMiddleName
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
**		Date: 09/09/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	09/09/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetCustomerPreFirstNameMiddleName
(
	@Prefix NVARCHAR(50)
	, @FirstName NVARCHAR(50)
	, @MiddleName NVARCHAR(50)
)
RETURNS NVARCHAR(250)
AS
BEGIN
	/** Declarations */
	DECLARE @Result NVARCHAR(250);

	/** Initialize */
	SET @Prefix = LTRIM(RTRIM(@Prefix));
	SET @FirstName = LTRIM(RTRIM(@FirstName));
	SET @MiddleName = LTRIM(RTRIM(@MiddleName));

	/** Build fullname */
	SELECT @Result =
		CASE
			WHEN @Prefix IS NULL OR @Prefix = '' THEN ''
			ELSE @Prefix + ' '
		END
		+
		@FirstName
		+
		CASE
			WHEN @MiddleName IS NULL OR @MiddleName = '' THEN ''
			ELSE ' ' + @MiddleName
		END
	
	RETURN @Result;
END
GO

-- SELECT [dbo].fxGetCustomerPreFirstNameMiddleName('MRS   ', 'Andres', '  EFraim ') AS [FullName];