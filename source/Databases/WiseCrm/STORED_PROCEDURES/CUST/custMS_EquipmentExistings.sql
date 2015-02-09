USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_EquipmentExistings')
	BEGIN
		PRINT 'Dropping Procedure custMS_EquipmentExistings'
		DROP  Procedure  dbo.custMS_EquipmentExistings
	END
GO

PRINT 'Creating Procedure custMS_EquipmentExistings'
GO
/******************************************************************************
**		File: custMS_EquipmentExistings.sql
**		Name: custMS_EquipmentExistings
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
CREATE Procedure dbo.custMS_EquipmentExistings
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	SELECT
		ITM.*
	FROM
		[dbo].vwMS_Equipments AS ITM WITH(NOLOCK)
		INNER JOIN [dbo].[MS_EquipmentExistings] AS EEX WITH (NOLOCK)
		ON
			(ITM.EquipmentID = EEX.EquipmentID)
	WHERE
		(ITM.IsExisting = 1)
	ORDER BY
		EEX.OrderNumber;
END
GO

GRANT EXEC ON dbo.custMS_EquipmentExistings TO PUBLIC
GO

--EXEC dbo.custMS_EquipmentExistings

--SELECT * FROM MS_Equipments WHERE EquipmentID LIKE 'EQPM_EXST%' AND IsExisting = 0