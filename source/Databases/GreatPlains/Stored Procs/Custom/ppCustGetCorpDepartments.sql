USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetCorpDepartments')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetCorpDepartments'
		DROP  Procedure  dbo.ppCustGetCorpDepartments
	END
GO

PRINT 'Creating Procedure ppCustGetCorpDepartments'
GO
/******************************************************************************
**		File: ppCustGetCorpDepartments.sql
**		Name: ppCustGetCorpDepartments
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
**		Auth: Brett Kotter
**		Date: 06/23/2010
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	06/23/2010	Brett Kotter	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustGetCorpDepartments
AS
BEGIN

SELECT *
FROM UPR40301 AS DEPT WITH(NOLOCK)
WHERE DEPT.JOBTITLE IN
 (SELECT EMP.JOBTITLE
	FROM UPR00100 AS EMP WITH(NOLOCK)
		WHERE EMP.INACTIVE = 0
			AND EMP.EMPLCLAS = 'CORP'
	GROUP BY EMP.JOBTITLE)
ORDER BY DEPT.DSCRIPTN

END
GO

GRANT EXEC ON dbo.ppCustGetCorpDepartments TO PUBLIC
GO