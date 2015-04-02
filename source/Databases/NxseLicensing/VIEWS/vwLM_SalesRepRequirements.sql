USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwLM_SalesRepRequirements')
	BEGIN
		PRINT 'Dropping VIEW vwLM_SalesRepRequirements'
		DROP VIEW dbo.vwLM_SalesRepRequirements
	END
GO

PRINT 'Creating VIEW vwLM_SalesRepRequirements'
GO

/****** Object:  View [dbo].[vwLM_SalesRepRequirements]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwLM_SalesRepRequirements.sql
**		Name: vwLM_SalesRepRequirements
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
CREATE VIEW [dbo].[vwLM_SalesRepRequirements]
AS
	-- Enter Query here
	SELECT
		LMR.RequirementID
		, LMR.RequirementTypeName
		, LMR.LocationTypeName
		, LMR.RequirementName
		, LMR.LockID
		, LMR.[LockTypeName]
		, LMR.CallCenterMessage
		, CAST(NULL AS VARCHAR(100)) AS [Status]
		, CAST(NULL AS INT) AS [LicenseID]
	FROM
		[dbo].[vwLM_Requirements] AS LMR WITH (NOLOCK)
GO
/* TEST */
-- SELECT * FROM vwLM_SalesRepRequirements
