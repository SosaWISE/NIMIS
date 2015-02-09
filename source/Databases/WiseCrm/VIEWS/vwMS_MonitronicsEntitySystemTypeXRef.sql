USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_MonitronicsEntitySystemTypeXRef')
	BEGIN
		PRINT 'Dropping VIEW vwMS_MonitronicsEntitySystemTypeXRef'
		DROP VIEW dbo.vwMS_MonitronicsEntitySystemTypeXRef
	END
GO

PRINT 'Creating VIEW vwMS_MonitronicsEntitySystemTypeXRef'
GO

/****** Object:  View [dbo].[vwMS_MonitronicsEntitySystemTypeXRef]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_MonitronicsEntitySystemTypeXRef.sql
**		Name: vwMS_MonitronicsEntitySystemTypeXRef
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
**		Date: 01/10/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/10/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_MonitronicsEntitySystemTypeXRef]
AS
	-- Enter Query here
	SELECT
		MEST.[SystemTypeXRefID]
		, MEST.[DigitalSystemTypeId]
		, DIGT.Description AS [DIG DECS]
		, MEST.[TwoWayDeviceId]
		, TWOW.Description AS [TWO DESC]
		, MEST.[CellSystemTypeId]
		, CELL.Description AS [CELL DESC]
	FROM
		[dbo].[MS_MonitronicsEntitySystemTypeXRef] AS MEST WITH (NOLOCK)
		INNER JOIN [dbo].[MS_MonitronicsEntitySystemTypes] AS DIGT WITH (NOLOCK)
		ON
			(MEST.DigitalSystemTypeId = DIGT.SystemTypeID)
		LEFT OUTER JOIN [dbo].[MS_MonitronicsEntitySystemTypes] AS TWOW WITH (NOLOCK)
		ON
			(MEST.TwoWayDeviceId = TWOW.SystemTypeID)
		LEFT OUTER JOIN [dbo].[MS_MonitronicsEntitySystemTypes] AS CELL WITH (NOLOCK)
		ON
			(MEST.CellSystemTypeId = CELL.SystemTypeID)

GO
/* TEST 
SELECT * FROM vwMS_MonitronicsEntitySystemTypeXRef
SELECT * FROM MS_Equipments WHERE EquipmentID = 'CELL_SRV_TG'
SELECT * FROM AE_Items WHERE ItemID = 'CELL_SRV_TG'

*/