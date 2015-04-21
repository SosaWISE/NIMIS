USE [NXSE_Sales]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountOnlineStatusInfo')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountOnlineStatusInfo'
		DROP VIEW dbo.vwMS_AccountOnlineStatusInfo
	END
GO

PRINT 'Creating VIEW vwMS_AccountOnlineStatusInfo'
GO

/****** Object:  View [dbo].[vwMS_AccountOnlineStatusInfo]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountOnlineStatusInfo.sql
**		Name: vwMS_AccountOnlineStatusInfo
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
**		Date: 04/21/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/21/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountOnlineStatusInfo]
AS
	-- Enter Query here
	SELECT
		CAST(NULL AS VARCHAR(100)) AS KeyName
		, CAST(NULL AS VARCHAR(100)) AS [Value]
		, CAST(NULL AS VARCHAR(100)) AS [Status]
GO
/* TEST */
-- SELECT * FROM vwMS_AccountOnlineStatusInfo
