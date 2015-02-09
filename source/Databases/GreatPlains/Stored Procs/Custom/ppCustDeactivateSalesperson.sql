USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustDeactivateSalesperson')
	BEGIN
		PRINT 'Dropping Procedure ppCustDeactivateSalesperson'
		DROP  Procedure  dbo.ppCustDeactivateSalesperson
	END
GO

PRINT 'Creating Procedure ppCustDeactivateSalesperson'
GO
/******************************************************************************
**		File: ppCustDeactivateSalesperson.sql
**		Name: ppCustDeactivateSalesperson
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
CREATE Procedure dbo.ppCustDeactivateSalesperson
(
	@SalespersonID NVARCHAR(15)
)
AS
BEGIN
		
	UPDATE
		RM00301
	SET
		INACTIVE = 1
	WHERE
		SLPRSNID = @SalespersonID
		
END
GO

GRANT EXEC ON dbo.ppCustDeactivateSalesperson TO PUBLIC
GO