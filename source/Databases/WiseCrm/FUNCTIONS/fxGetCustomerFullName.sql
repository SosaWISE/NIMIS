USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetCustomerFullName')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetCustomerFullName'
		DROP FUNCTION  dbo.fxGetCustomerFullName
	END
GO

PRINT 'Creating FUNCTION fxGetCustomerFullName'
GO
/******************************************************************************
**		File: fxGetCustomerFullName.sql
**		Name: fxGetCustomerFullName
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
CREATE FUNCTION dbo.fxGetCustomerFullName
(
	@Label VARCHAR(10)
	, @Prefix NVARCHAR(50)
	, @FirstName NVARCHAR(50)
	, @MiddleName NVARCHAR(50)
	, @LastName NVARCHAR(50)
	, @PostFix NVARCHAR(50)
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
	SET @LastName = LTRIM(RTRIM(@LastName));
	SET @PostFix = LTRIM(RTRIM(@PostFix));
	IF (@Label IS NULL)
	BEGIN
		SET @Label = '';
	END 
	ELSE
	BEGIN
		SET @Label = '[' + @Label + ']: ';
	END

	/** Build fullname */
	SELECT @Result =
--		'[' + @Label + ']: ' +
		@Label +
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
		+
		' ' + @LastName
		+
		CASE
			WHEN @PostFix IS NULL OR @PostFix = '' THEN ''
			ELSE ' ' + @PostFix
		END
	
	RETURN @Result;
END
GO

SELECT [dbo].fxGetCustomerFullName('C', 'MRS   ', 'Andres', '  EFraim ', 'Sosa  ', 'III') AS [FullName];