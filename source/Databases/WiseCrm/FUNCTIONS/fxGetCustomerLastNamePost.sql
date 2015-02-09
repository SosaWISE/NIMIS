USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetCustomerLastNamePost')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetCustomerLastNamePost'
		DROP FUNCTION  dbo.fxGetCustomerLastNamePost
	END
GO

PRINT 'Creating FUNCTION fxGetCustomerLastNamePost'
GO
/******************************************************************************
**		File: fxGetCustomerLastNamePost.sql
**		Name: fxGetCustomerLastNamePost
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
CREATE FUNCTION dbo.fxGetCustomerLastNamePost
(
	@LastName NVARCHAR(50)
	, @PostFix NVARCHAR(50)
)
RETURNS NVARCHAR(250)
AS
BEGIN
	/** Declarations */
	DECLARE @Result NVARCHAR(250);

	/** Initialize */
	SET @LastName = LTRIM(RTRIM(@LastName));
	SET @PostFix = LTRIM(RTRIM(@PostFix));
	
	/** Build fullname */
	SELECT @Result =
		@LastName
		+
		CASE
			WHEN @PostFix IS NULL OR @PostFix = '' THEN ''
			ELSE ' ' + @PostFix
		END
	
	RETURN @Result;
END
GO

-- SELECT [dbo].fxGetCustomerLastNamePost('Sosa  ', 'III') AS [FullName];