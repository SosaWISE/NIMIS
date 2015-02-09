USE [WISE_HumanResource]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND (name = 'fxRU_UserGenerateGPEmployeeID'))
	BEGIN
		PRINT 'Dropping FUNCTION fxRU_UserGenerateGPEmployeeID'
		DROP FUNCTION  dbo.fxRU_UserGenerateGPEmployeeID
	END
GO

PRINT 'Creating FUNCTION fxRU_UserGenerateGPEmployeeID'
GO
/******************************************************************************
**		File: fxRU_UserGenerateGPEmployeeID.sql
**		Name: fxRU_UserGenerateGPEmployeeID
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
**		Date: 01/17/2011
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/17/2011	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxRU_UserGenerateGPEmployeeID
(
	@FirstName NVARCHAR(50)
	, @LastName NVARCHAR(50)
)
RETURNS NVARCHAR(25)
AS
BEGIN
	/** Local Declaretions */
	DECLARE @GPEmployeeID NVARCHAR(10);
	DECLARE @Counter INT;
	DECLARE @CounterString NVARCHAR(3);

	/** Create first part of the GPEmployee ID */
	SET @FirstName = UPPER(@FirstName);
	SET @LastName  = UPPER(@LastName);
	SET @GPEmployeeID = SUBSTRING(@LastName, 1,4) + SUBSTRING(@FirstName, 1, 1);

	/** Start Counting */
	SET @Counter = 1;
	SET @CounterString = REPLICATE('0', 3 - LEN(CAST(@Counter AS VARCHAR))) + CAST(@Counter AS VARCHAR);

	WHILE (EXISTS(SELECT * FROM dbo.RU_Users WHERE GPEmployeeID = @GPEmployeeID + @CounterString))
	BEGIN
		/** Increase counter */
		SET @Counter = @Counter + 1;
		SET @CounterString = REPLICATE('0', 3 - LEN(CAST(@Counter AS VARCHAR))) + CAST(@Counter AS VARCHAR);
	END

	/** Return result. */
	RETURN @GPEmployeeID + @CounterString;
END
GO