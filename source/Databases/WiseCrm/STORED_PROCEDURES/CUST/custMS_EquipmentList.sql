USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_EquipmentList')
	BEGIN
		PRINT 'Dropping Procedure custMS_EquipmentList'
		DROP  Procedure  dbo.custMS_EquipmentList
	END
GO

PRINT 'Creating Procedure custMS_EquipmentList'
GO
/******************************************************************************
**		File: custMS_EquipmentList.sql
**		Name: custMS_EquipmentList
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 01/10/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/10/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_EquipmentList
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	SELECT
		ITM.*
	FROM
		[dbo].vwMS_Equipments AS ITM WITH(NOLOCK)
	WHERE
		(ITM.EquipmentID NOT IN (SELECT ItemID FROM dbo.AE_ItemInterims))
		AND (ITM.IsExisting = 0);

END
GO

GRANT EXEC ON dbo.custMS_EquipmentList TO PUBLIC
GO

--EXEC dbo.custMS_EquipmentList

--SELECT * FROM MS_Equipments WHERE EquipmentID LIKE 'EQPM_EXST%' AND IsExisting = 0