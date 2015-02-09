USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityEquipmentTypesNuke')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityEquipmentTypesNuke'
		DROP  Procedure  dbo.custMS_MonitronicsEntityEquipmentTypesNuke
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityEquipmentTypesNuke'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityEquipmentTypesNuke.sql
**		Name: custMS_MonitronicsEntityEquipmentTypesNuke
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
**		Auth: Jake Jenne
**		Date: 12/2/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/2/2014	Jake Jenne		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityEquipmentTypesNuke
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			UPDATE [dbo].[MS_MonitronicsEntityEquipmentTypes] SET
				IsActive = 0
				, IsDeleted = 1;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- Return row
	SELECT * FROM [dbo].[MS_MonitronicsEntityEquipmentTypes] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityEquipmentTypesNuke TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityEquipmentTypesNuke */