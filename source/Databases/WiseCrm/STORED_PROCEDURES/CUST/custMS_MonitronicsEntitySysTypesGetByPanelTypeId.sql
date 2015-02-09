USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntitySysTypesGetByPanelTypeId')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntitySysTypesGetByPanelTypeId'
		DROP  Procedure  dbo.custMS_MonitronicsEntitySysTypesGetByPanelTypeId
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntitySysTypesGetByPanelTypeId'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntitySysTypesGetByPanelTypeId.sql
**		Name: custMS_MonitronicsEntitySysTypesGetByPanelTypeId
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
**		Date: 11/24/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/24/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntitySysTypesGetByPanelTypeId
(
	@PanelTypeId VARCHAR(20)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @SystemTypeID VARCHAR(50);
	
	BEGIN TRY
	
		SELECT 
			@SystemTypeID = CASE
				WHEN @PanelTypeId = '2GIG' THEN '20S001'
				ELSE NULL
			END;

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- ** Result
	SELECT * FROM [dbo].[MS_MonitronicsEntitySystemTypes] WHERE (SystemTypeID = @SystemTypeID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntitySysTypesGetByPanelTypeId TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntitySysTypesGetByPanelTypeId '2GIG' */