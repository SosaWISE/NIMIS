USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwUI_ApplicationMenu')
	BEGIN
		PRINT 'Dropping VIEW vwUI_ApplicationMenu'
		DROP VIEW dbo.vwUI_ApplicationMenu
	END
GO

PRINT 'Creating VIEW vwUI_ApplicationMenu'
GO

/****** Object:  View [dbo].[vwUI_ApplicationMenu]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwUI_ApplicationMenu.sql
**		Name: vwUI_ApplicationMenu
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
**		Auth: Andres Sosa
**		Date: 01/10/2011
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/10/2011	Andres Sosa	Created
*******************************************************************************/
CREATE VIEW [dbo].[vwUI_ApplicationMenu] AS

	SELECT
		UAPP.ApplicationID
		, UAPP.FriendlyName AS ApplicationFriendlyName
		
		, UMI.MenuItemID
		, UMI.MenuID
		, UMI.ActionID
		, UMI.ParentItemID
		, UMI.SourceMenuItemID
		, UMI.Label
		, UMI.ToolTip
		, UMI.IsVisible
		, UMI.ShowInDashboard
		, UMI.ActionPriority
		
		, UAV.ApplicationVersionID
		, UAC.ActionName
		, UAPP.SmallIconFileID
		
		, UMI.IsOverrideable
		
	FROM
		UI_ApplicationVersions AS UAV WITH (NOLOCK)
		INNER JOIN UI_Applications AS UAPP WITH (NOLOCK)
		ON
			(UAPP.ApplicationID = UAV.ApplicationID)
		INNER JOIN vwUI_MenusCurrentMenus AS UMCM WITH (NOLOCK)
		ON
			(UMCM.ApplicationVersionID = UAV.ApplicationVersionID)
		INNER JOIN UI_MenuItems AS UMI WITH (NOLOCK)
		ON
			(UMI.MenuID = UMCM.MenuID)
		LEFT JOIN UI_Actions AS UAC WITH (NOLOCK)
		ON
			(UMI.ActionID = UAC.ActionID)
			

GO

/* TEST */
-- SELECT * FROM vwUI_ApplicationMenu