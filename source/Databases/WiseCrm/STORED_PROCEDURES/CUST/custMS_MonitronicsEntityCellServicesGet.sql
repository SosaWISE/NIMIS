USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityCellServicesGet')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityCellServicesGet'
		DROP  Procedure  dbo.custMS_MonitronicsEntityCellServicesGet
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityCellServicesGet'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityCellServicesGet.sql
**		Name: custMS_MonitronicsEntityCellServicesGet
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
**		Date: 01/16/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/16/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityCellServicesGet
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
			MEMC.*
		FROM
			[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
			INNER JOIN [dbo].[MS_EquipmentMonitronicsCellServices] AS MEMC WITH (NOLOCK)
			ON
				(MSA.CellPackageItemId = MEMC.EquipmentID)
		WHERE
			(MSA.AccountID = @AccountId);

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityCellServicesGet TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityCellServicesGet */