USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetByAgemniInvoiceID')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetByAgemniInvoiceID'
		DROP  Procedure  dbo.custRU_UsersGetByAgemniInvoiceID
	END
GO

PRINT 'Creating Procedure custRU_UsersGetByAgemniInvoiceID'
GO
/******************************************************************************
**		File: custRU_UsersGetByAgemniInvoiceID.sql
**		Name: custRU_UsersGetByAgemniInvoiceID
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
**		Auth: 
**		Date: 
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**			
*******************************************************************************/
CREATE Procedure dbo.custRU_UsersGetByAgemniInvoiceID
(
	@AgemniInvoiceID INT
)
AS
BEGIN
	/** Turn counting Off */
	SET NOCOUNT ON
	
	/** Execute Query */
	SELECT
		RU.*
	FROM
		RU_Users AS RU WITH (NOLOCK)
		INNER JOIN [SLS_AgemniBridge].[dbo].AG_Employee_RU_Users AS EMP WITH (NOLOCK)
		ON
			(RU.UserID = EMP.UserID)
		INNER JOIN [SLS_AgemniBackup].[dbo].Appointment AS APT WITH (NOLOCK)
		ON
			(EMP.ID = APT.TechID)
	WHERE
		(APT.InvoiceID = @AgemniInvoiceID)
	
END
GO

GRANT EXEC ON dbo.custRU_UsersGetByAgemniInvoiceID TO PUBLIC
GO