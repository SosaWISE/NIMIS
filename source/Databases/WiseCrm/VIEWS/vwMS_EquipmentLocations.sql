USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_EquipmentLocations')
	BEGIN
		PRINT 'Dropping VIEW vwMS_EquipmentLocations'
		DROP VIEW dbo.vwMS_EquipmentLocations
	END
GO

PRINT 'Creating VIEW vwMS_EquipmentLocations'
GO

/****** Object:  View [dbo].[vwMS_EquipmentLocations]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_EquipmentLocations.sql
**		Name: vwMS_EquipmentLocations
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
**		Date: 02/26/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/26/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_EquipmentLocations]
AS
	-- Enter Query here
	SELECT 
		[EquipmentLocationID]
		, [EquipmentLocationDesc]
		, [MonitronicsCode]
		, [CriticomCode]
		, [AvantGuardCode]
		, CAST('' AS VARCHAR(10)) AS [LocationCode]
		, [IsActive]
		, [IsDeleted]
	FROM 
		[dbo].[MS_EquipmentLocations] AS MEL WITH (NOLOCK)
GO
/* TEST
SELECT * FROM vwMS_EquipmentLocations
 */
