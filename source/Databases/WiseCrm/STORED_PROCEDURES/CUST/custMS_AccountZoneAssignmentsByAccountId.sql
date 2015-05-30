USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountZoneAssignmentsByAccountId')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountZoneAssignmentsByAccountId'
		DROP  Procedure  dbo.custMS_AccountZoneAssignmentsByAccountId
	END
GO

PRINT 'Creating Procedure custMS_AccountZoneAssignmentsByAccountId'
GO
/******************************************************************************
**		File: custMS_AccountZoneAssignmentsByAccountId.sql
**		Name: custMS_AccountZoneAssignmentsByAccountId
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
**		Date: 03/26/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/26/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountZoneAssignmentsByAccountId
(
	@AccountId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Transfer data */
		SELECT
			AZA.*
		FROM
			[dbo].MS_AccountZoneAssignments AS AZA WITH (NOLOCK)
			INNER JOIN [dbo].[MS_AccountEquipment] AS MAE WITH (NOLOCK)
			ON
				(MAE.AccountEquipmentID = AZA.AccountEquipmentId)
				AND (AZA.IsActive = 1 AND AZA.IsDeleted = 0)
				AND (MAE.IsActive = 1 AND AZA.IsDeleted = 0)
		WHERE
			(MAE.AccountId = @AccountId);
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_AccountZoneAssignmentsByAccountId TO PUBLIC
GO

/** 

DECLARE @AccountID BIGINT = 191243;
EXEC dbo.custMS_AccountZoneAssignmentsByAccountId 191243
SELECT * FROM dbo.MS_AccountEquipment WHERE AccountId = @AccountID AND AccountEquipmentID = 43158;
SELECT * FROM dbo.MS_Equipments WHERE EquipmentID = 'EQPM_INVT30';
		SELECT
			MEQ.*
			, AZA.*
		FROM
			[dbo].MS_AccountZoneAssignments AS AZA WITH (NOLOCK)
			INNER JOIN [dbo].[MS_AccountEquipment] AS MAE WITH (NOLOCK)
			ON
				(MAE.AccountEquipmentID = AZA.AccountEquipmentId)
			INNER JOIN [dbo].[MS_Equipments] AS MEQ WITH (NOLOCK)
			ON
				(MAE.EquipmentID = MEQ.EquipmentID)
		WHERE
			(MAE.AccountId = 181051);

			SELECT * FROM MS_AccountEquipment WHERE AccountID = 181051
 */