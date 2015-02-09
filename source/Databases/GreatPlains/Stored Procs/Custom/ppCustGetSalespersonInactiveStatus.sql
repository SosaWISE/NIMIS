USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetSalespersonInactiveStatus')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetSalespersonInactiveStatus'
		DROP  Procedure  dbo.ppCustGetSalespersonInactiveStatus
	END
GO

PRINT 'Creating Procedure ppCustGetSalespersonInactiveStatus'
GO
/******************************************************************************
**		File: ppCustGetSalespersonInactiveStatus.sql
**		Name: ppCustGetSalespersonInactiveStatus
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
**		Date: 06/29/2010
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	06/29/2010	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustGetSalespersonInactiveStatus
(
	@SalespersonID NVARCHAR(15)
)
AS
BEGIN

	DECLARE @Result BIT

	SELECT
		@Result = INACTIVE
	FROM
		RM00301
	WHERE
		SLPRSNID = @SalespersonID


	SELECT CAST(COALESCE(@Result, 1) AS BIT) AS IsInactive
		
END
GO

GRANT EXEC ON dbo.ppCustGetSalespersonInactiveStatus TO PUBLIC
GO