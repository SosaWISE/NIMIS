USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwLM_Requirements')
	BEGIN
		PRINT 'Dropping VIEW vwLM_Requirements'
		DROP VIEW dbo.vwLM_Requirements
	END
GO

PRINT 'Creating VIEW vwLM_Requirements'
GO

/****** Object:  View [dbo].[vwLM_Requirements]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwLM_Requirements.sql
**		Name: vwLM_Requirements
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
**		Date: 10/08/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	10/08/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwLM_Requirements]
AS
	-- Enter Query here
	SELECT
		*
	FROM
		[dbo].[LM_Requirements]

GO
/* TEST */
-- SELECT * FROM vwLM_Requirements
