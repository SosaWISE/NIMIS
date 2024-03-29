USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerSWINGEquipment')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerSWINGEquipment'
		DROP VIEW dbo.vwAE_CustomerSWINGEquipment
	END
GO

PRINT 'Creating VIEW vwAE_CustomerSWINGEquipment'
GO

/****** Object:  View [dbo].[vwAE_CustomerSWINGEquipment]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerSWINGEquipment.sql
**		Name: vwAE_CustomerSWINGEquipment
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
**	04/22/2014	Junryl/Reagan	Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_CustomerSWINGEquipment]
AS

	-- Enter Query here
	SELECT
		CAST('' AS VARCHAR(50)) AS FullName
		, CAST('' AS CHAR(3)) AS ZoneNumber
		, CAST('' AS VARCHAR(50)) AS ZoneTypeName
		, CAST('' AS VARCHAR(50)) AS EquipmentLocationDesc
		, CAST(0 AS INT) AS [RowNumber]

GO
/* TEST */
-- SELECT * FROM vwAE_CustomerSWINGEquipment
