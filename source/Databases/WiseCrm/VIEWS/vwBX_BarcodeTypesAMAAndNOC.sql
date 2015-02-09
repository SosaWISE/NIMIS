USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwBX_BarcodeTypesAMAAndNOC')
	BEGIN
		PRINT 'Dropping VIEW vwBX_BarcodeTypesAMAAndNOC'
		DROP VIEW dbo.vwBX_BarcodeTypesAMAAndNOC
	END
GO

PRINT 'Creating VIEW vwBX_BarcodeTypesAMAAndNOC'
GO

/****** Object:  View [dbo].[vwBX_BarcodeTypesAMAAndNOC]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwBX_BarcodeTypesAMAAndNOC.sql
**		Name: vwBX_BarcodeTypesAMAAndNOC
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
**		Date: 06/07/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/07/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwBX_BarcodeTypesAMAAndNOC]
AS
	-- Enter Query here
	SELECT
		CAST(NULL AS BIGINT) AS AccountID
		, CAST(NULL AS VARCHAR) AS AMABarcodeNumber
		, CAST(NULL AS BIGINT) AS AMABarcodeID
		, CAST(NULL AS VARCHAR) AS NOCBarcodeNumber
		, CAST(NULL AS BIGINT) AS NOCBarcodeID
GO
/* TEST */
-- SELECT * FROM vwBX_BarcodeTypesAMAAndNOC
