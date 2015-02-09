USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custUI_ApplicationGetCurrentApplicationPermissions')
	BEGIN
		PRINT 'Dropping Procedure custUI_ApplicationGetCurrentApplicationPermissions'
		DROP  Procedure  dbo.custUI_ApplicationGetCurrentApplicationPermissions
	END
GO

PRINT 'Creating Procedure custUI_ApplicationGetCurrentApplicationPermissions'
GO
/******************************************************************************
**		File: custUI_ApplicationGetCurrentApplicationPermissions.sql
**		Name: custUI_ApplicationGetCurrentApplicationPermissions
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
**		Date: 10/06/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	10/06/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure [dbo].[custUI_ApplicationGetCurrentApplicationPermissions]
(
	@ApplicationID INT
)
AS
BEGIN

	SELECT
		MIP.PermissionTypeID
		, AM.ActionName
		, AM.IsOverrideable
		, MIP.PrincipalName
		, MIP.AllowAccess
	FROM vwUI_ApplicationMenu AS AM
	INNER JOIN vwUI_ApplicationVersionsCurrentVersions AS AVCV
	ON
		(AM.ApplicationVersionID = AVCV.ApplicationVersionID)
	INNER JOIN UI_MenuItemPermissions AS MIP
	ON
		AM.MenuItemID = MIP.MenuItemID
	WHERE
		AM.ActionName IS NOT NULL
		AND (AM.ApplicationID = @ApplicationID)
		
END
GO

GRANT EXEC ON dbo.custUI_ApplicationGetCurrentApplicationPermissions TO PUBLIC
GO

/** EXEC dbo.custUI_ApplicationGetCurrentApplicationPermissions 5 

SELECT * FROM vwUI_ApplicationMenu
SELECT * FROM vwUI_ApplicationVersionsCurrentVersions
SELECT * FROM UI_MenuItemPermissions

SELECT
	--MIP.PermissionTypeID
	AM.ActionName
	, AM.IsOverrideable
	, AM.ActionID
	--, MIP.PrincipalName
	--, MIP.AllowAccess
FROM vwUI_ApplicationMenu AS AM
INNER JOIN vwUI_ApplicationVersionsCurrentVersions AS AVCV
ON
	(AM.ApplicationVersionID = AVCV.ApplicationVersionID)
INNER JOIN UI_MenuItemPermissions AS MIP
ON
	AM.MenuItemID = MIP.MenuItemID
WHERE
	AM.ActionName IS NOT NULL
	AND (AM.ApplicationID = 5)
*/