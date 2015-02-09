USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_AccountNoteCat1ByDepartmentId')
	BEGIN
		PRINT 'Dropping Procedure custMC_AccountNoteCat1ByDepartmentId'
		DROP  Procedure  dbo.custMC_AccountNoteCat1ByDepartmentId
	END
GO

PRINT 'Creating Procedure custMC_AccountNoteCat1ByDepartmentId'
GO
/******************************************************************************
**		File: custMC_AccountNoteCat1ByDepartmentId.sql
**		Name: custMC_AccountNoteCat1ByDepartmentId
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
**		Auth: Andres Sosa
**		Date: 12/31/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/31/2013	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMC_AccountNoteCat1ByDepartmentId
(
	@DepartmentId VARCHAR(20)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Execute Statement */

	SELECT
		CAT1.*
	FROM
		[dbo].MC_AccountNoteCat1 AS CAT1 WITH (NOLOCK)
		INNER JOIN [dbo].MC_DepartmentAccountNoteCat1 AS DCAT WITH (NOLOCK)
		ON
			(CAT1.NoteCategory1ID = DCAT.NoteCategory1ID)
	WHERE
		(DCAT.DepartmentID = @DepartmentId);
	
END
GO

GRANT EXEC ON dbo.custMC_AccountNoteCat1ByDepartmentId TO PUBLIC
GO