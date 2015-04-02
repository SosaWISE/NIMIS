USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'View_Template')
	BEGIN
		PRINT 'Dropping VIEW View_Template'
		DROP VIEW dbo.View_Template
	END
GO

PRINT 'Creating VIEW View_Template'
GO

/****** Object:  View [dbo].[View_Template]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: View_Template.sql
**		Name: View_Template
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
**		Date: 04/01/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/01/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[View_Template]
AS
	-- Enter Query here

GO
/* TEST */
-- SELECT * FROM View_Template
