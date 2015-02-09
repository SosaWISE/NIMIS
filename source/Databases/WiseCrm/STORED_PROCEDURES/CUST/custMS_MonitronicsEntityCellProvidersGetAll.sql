USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityCellProvidersGetAll')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityCellProvidersGetAll'
		DROP  Procedure  dbo.custMS_MonitronicsEntityCellProvidersGetAll
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityCellProvidersGetAll'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityCellProvidersGetAll.sql
**		Name: custMS_MonitronicsEntityCellProvidersGetAll
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
**		Date: 12/4/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/4/2014	Jake Jenne		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityCellProvidersGetAll
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	-- Return rows
	SELECT * FROM [dbo].[MS_MonitronicsEntityCellProviders] WHERE (IsActive = 1 AND IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityCellProvidersGetAll TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityCellProvidersGetAll */