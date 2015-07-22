USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwRU_UserSalesInfoConnext')
	BEGIN
		PRINT 'Dropping VIEW vwRU_UserSalesInfoConnext'
		DROP VIEW dbo.vwRU_UserSalesInfoConnext
	END
GO

PRINT 'Creating VIEW vwRU_UserSalesInfoConnext'
GO

/****** Object:  View [dbo].[vwRU_UserSalesInfoConnext]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwRU_UserSalesInfoConnext.sql
**		Name: vwRU_UserSalesInfoConnext
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
CREATE VIEW [dbo].[vwRU_UserSalesInfoConnext]
AS
	-- Enter Query here
	SELECT
		RU.UserID
		, RU.FirstName
		, RU.MiddleName
		, RU.LastName
		, CAST(1 AS BIGINT) AS MLMDepth
		, CAST(1 AS BIT) AS ManagerHasOwnTeam
		, 'Team Name' AS TeamName
	FROM
		[dbo].[RU_Users] AS RU WITH (NOLOCK)


GO