USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_EquipmentAccountZoneTypes')
	BEGIN
		PRINT 'Dropping VIEW vwMS_EquipmentAccountZoneTypes'
		DROP VIEW dbo.vwMS_EquipmentAccountZoneTypes
	END
GO

PRINT 'Creating VIEW vwMS_EquipmentAccountZoneTypes'
GO

/****** Object:  View [dbo].[vwMS_EquipmentAccountZoneTypes]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_EquipmentAccountZoneTypes.sql
**		Name: vwMS_EquipmentAccountZoneTypes
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
**		Date: 07/22/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/22/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_EquipmentAccountZoneTypes]
AS
	-- Enter Query here
	SELECT
		EAZT.EquipmentAccountZoneTypeID
		, EQM.EquipmentID
		, EAZT.AccountZoneTypeId
		, AZT.AccountZoneType
		, CAST(0 AS BIT) AS [IsDefault]
	FROM
		[dbo].[MS_Equipments] AS EQM WITH (NOLOCK)
		INNER JOIN [dbo].[MS_EquipmentAccountZoneTypes] AS EAZT WITH (NOLOCK)
		ON
			(EAZT.EquipmentId = EQM.EquipmentID)
		INNER JOIN [dbo].[MS_AccountZoneTypes] AS AZT WITH (NOLOCK)
		ON
			(AZT.AccountZoneTypeID = EAZT.AccountZoneTypeId)
	
GO
/* TEST */
-- SELECT * FROM vwMS_EquipmentAccountZoneTypes WHERE EquipmentID = 'EQPM_INVT206';
