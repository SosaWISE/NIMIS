USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_EquipmentMostFrequents')
	BEGIN
		PRINT 'Dropping Procedure custMS_EquipmentMostFrequents'
		DROP  Procedure  dbo.custMS_EquipmentMostFrequents
	END
GO

PRINT 'Creating Procedure custMS_EquipmentMostFrequents'
GO
/******************************************************************************
**		File: custMS_EquipmentMostFrequents.sql
**		Name: custMS_EquipmentMostFrequents
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
CREATE Procedure dbo.custMS_EquipmentMostFrequents
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	SELECT
		ITM.*
	FROM
		[dbo].vwMS_Equipments AS ITM WITH(NOLOCK)
		INNER JOIN [dbo].MS_EquipmentMostFrequents AS IMF WITH (NOLOCK)
		ON
			(ITM.EquipmentID = IMF.EquipmentId)
	ORDER BY
		IMF.OrderNumber;

END
GO

GRANT EXEC ON dbo.custMS_EquipmentMostFrequents TO PUBLIC
GO

EXEC dbo.custMS_EquipmentMostFrequents