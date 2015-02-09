USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwRU_UsersTech')
	BEGIN
		PRINT 'Dropping VIEW vwRU_UsersTech'
		DROP VIEW dbo.vwRU_UsersTech
	END
GO

PRINT 'Creating VIEW vwRU_UsersTech'
GO

/****** Object:  View [dbo].[vwRU_UsersTech]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwRU_UsersTech.sql
**		Name: vwRU_UsersTech
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
**		Date: 02/05/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/05/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE VIEW [dbo].[vwRU_UsersTech]
AS
	-- Enter Query here
	SELECT DISTINCT
		RU.*
	FROM
		[dbo].RU_Users AS RU WITH (NOLOCK)
		INNER JOIN [dbo].RU_Recruits AS RR WITH (NOLOCK)
		ON
			(RR.UserID = RU.UserID)
			AND (RR.UserTypeId IN (6,7,8,10,20,22))
			AND (RR.IsActive = 1) AND (RR.IsDeleted = 0)
			AND (RU.IsActive = 1) AND (RU.IsDeleted = 0)
GO
/* TEST */
SELECT * FROM vwRU_UsersTech
