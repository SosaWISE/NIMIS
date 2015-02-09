USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_ReceiverBlockCellDeviceInfoViewGetByAccountID')
	BEGIN
		PRINT 'Dropping Procedure custMS_ReceiverBlockCellDeviceInfoViewGetByAccountID'
		DROP  Procedure  dbo.custMS_ReceiverBlockCellDeviceInfoViewGetByAccountID
	END
GO

PRINT 'Creating Procedure custMS_ReceiverBlockCellDeviceInfoViewGetByAccountID'
GO
/******************************************************************************
**		File: custMS_ReceiverBlockCellDeviceInfoViewGetByAccountID.sql
**		Name: custMS_ReceiverBlockCellDeviceInfoViewGetByAccountID
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
**		Date: 01/15/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/15/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_ReceiverBlockCellDeviceInfoViewGetByAccountID
(
	@AccountId BIGINT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @EquipmentID VARCHAR(50)
		, @CellularProvidorName VARCHAR(50)

	BEGIN TRY

		/** Figure out the Vendor */
		SELECT DISTINCT TOP 1
			@EquipmentID = MAE.EquipmentId
			, @CellularProvidorName = EQV.CellularProvidorName
		FROM
			[dbo].[MS_AccountEquipment] AS MAE WITH (NOLOCK)
			INNER JOIN [dbo].[MS_Equipments] AS EQM WITH (NOLOCK)
			ON
				(EQM.EquipmentID = MAE.EquipmentId)
				AND (MAE.IsActive = 1) AND (MAE.IsDeleted = 0)
			INNER JOIN [dbo].[MS_EquipmentCellularVendors] AS EQV WITH (NOLOCK)
			ON
				(EQM.EquipmentID = EQV.EquipmentID)
			--LEFT OUTER JOIN [dbo].[MS_EquipmentTypes] AS MET WITH (NOLOCK)
			--ON
			--	(EQM.EquipmentTypeId = MET.EquipmentTypeID)
		WHERE
			(MAE.AccountId = @AccountID)
			AND (EQM.IsCellUnit = 1);

		SELECT
			ASGD.*
		FROM
			[dbo].[MS_Accounts] AS MAS WITH (NOLOCK)
			INNER JOIN [dbo].[MS_IndustryAccounts] AS IAN WITH (NOLOCK)
			ON
				(MAS.AccountID = IAN.AccountID)
			INNER JOIN [dbo].[vwMS_ReceiverBlockCellDeviceInfo] AS ASGD WITH (NOLOCK)
			ON
				(IAN.ReceiverLineBlockId = ASGD.ReceiverLineBlockId)
		WHERE
			(MAS.AccountID = @AccountId)

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_ReceiverBlockCellDeviceInfoViewGetByAccountID TO PUBLIC
GO

/** EXEC dbo.custMS_ReceiverBlockCellDeviceInfoViewGetByAccountID 150927 
SELECT * FROM [dbo].[vwMS_ReceiverBlockCellDeviceInfo]
*/