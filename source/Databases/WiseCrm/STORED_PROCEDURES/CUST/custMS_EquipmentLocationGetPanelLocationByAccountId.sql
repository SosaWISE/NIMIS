USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_EquipmentLocationGetPanelLocationByAccountId')
	BEGIN
		PRINT 'Dropping Procedure custMS_EquipmentLocationGetPanelLocationByAccountId'
		DROP  Procedure  dbo.custMS_EquipmentLocationGetPanelLocationByAccountId
	END
GO

PRINT 'Creating Procedure custMS_EquipmentLocationGetPanelLocationByAccountId'
GO
/******************************************************************************
**		File: custMS_EquipmentLocationGetPanelLocationByAccountId.sql
**		Name: custMS_EquipmentLocationGetPanelLocationByAccountId
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
**		Date: 01/28/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/28/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_EquipmentLocationGetPanelLocationByAccountId
(
	@AccountId BIGINT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

	SELECT
		MEL.*
	FROM
		[dbo].[MS_AccountEquipment] AS MEQ WITH (NOLOCK)
		INNER JOIN [dbo].[vwMS_EquipmentLocations] AS MEL WITH (NOLOCK)
		ON
			(MEL.EquipmentLocationID = MEQ.EquipmentLocationId)
	WHERE
		(MEQ.AccountID = @AccountId)
		AND (MEQ.IsMainPanel = 1)
		AND (MEQ.IsActive = 1) AND (MEQ.IsDeleted = 0);

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_EquipmentLocationGetPanelLocationByAccountId TO PUBLIC
GO

/** 
DECLARE @AccountID BIGINT = 191257
	SELECT
		MEQ.*
	FROM
		[dbo].[MS_AccountEquipment] AS MEQ WITH (NOLOCK)
		--INNER JOIN [dbo].[vwMS_EquipmentLocations] AS MEL WITH (NOLOCK)
		--ON
		--	(MEL.EquipmentLocationID = MEQ.EquipmentLocationId)
	WHERE
		(MEQ.AccountID = @AccountId)
		AND (MEQ.IsMainPanel = 1)
		AND (MEQ.IsActive = 1) AND (MEQ.IsDeleted = 0);

EXEC dbo.custMS_EquipmentLocationGetPanelLocationByAccountId 191257 
*/