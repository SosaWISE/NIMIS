USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerSWINGSystemDetail')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerSWINGSystemDetail'
		DROP VIEW dbo.vwAE_CustomerSWINGSystemDetail
	END
GO

PRINT 'Creating VIEW vwAE_CustomerSWINGSystemDetail'
GO

/****** Object:  View [dbo].[vwAE_CustomerSWINGSystemDetail]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerSWINGSystemDetail.sql
**		Name: vwAE_CustomerSWINGSystemDetail
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
**		Date: 04/21/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	05/09/2014	reagan/junryll		Created by
*******************************************************************************/	
CREATE VIEW [dbo].[vwAE_CustomerSWINGSystemDetail]
AS

	-- Enter Query here
	SELECT
		CAST('' AS VARCHAR(50)) AS ServiceType
		, CAST('' AS VARCHAR(50)) AS CellularType
		, CAST('' AS VARCHAR(50)) AS PassPhrase
		, CAST('' AS VARCHAR(50)) AS PanelType
		, CAST('' AS VARCHAR(50)) AS DslSeizure

GO
/* TEST */
-- SELECT * FROM View_Template
