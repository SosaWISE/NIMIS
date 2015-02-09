USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustActivateSalespersonIfInactive')
	BEGIN
		PRINT 'Dropping Procedure ppCustActivateSalespersonIfInactive'
		DROP  Procedure  dbo.ppCustActivateSalespersonIfInactive
	END
GO

PRINT 'Creating Procedure ppCustActivateSalespersonIfInactive'
GO
/******************************************************************************
**		File: ppCustActivateSalespersonIfInactive.sql
**		Name: ppCustActivateSalespersonIfInactive
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Todd Carlson
**		Date: 02/18/2008
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	02/18/2008	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustActivateSalespersonIfInactive
(
	@SalespersonID NVARCHAR(15)
)
AS
BEGIN

	DECLARE @OriginalInactiveValue BIT
	SELECT
		@OriginalInactiveValue = INACTIVE
	FROM
		RM00301
	WHERE
		SLPRSNID = @SalespersonID
		
	UPDATE
		RM00301
	SET
		INACTIVE = 0
	WHERE
		SLPRSNID = @SalespersonID
		
	DECLARE @Changed BIT
	IF @OriginalInactiveValue = 1
		SET @Changed = 1
	ELSE
		SET @Changed = 0
	
	SELECT @Changed AS ValueChanged
		
END
GO

GRANT EXEC ON dbo.ppCustActivateSalespersonIfInactive TO PUBLIC
GO