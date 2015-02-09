USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountCellularTypesGet')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountCellularTypesGet'
		DROP  Procedure  dbo.custMS_AccountCellularTypesGet
	END
GO

PRINT 'Creating Procedure custMS_AccountCellularTypesGet'
GO
/******************************************************************************
**		File: custMS_AccountCellularTypesGet.sql
**		Name: custMS_AccountCellularTypesGet
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
**		Date: 12/17/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	12/17/2013	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountCellularTypesGet
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Execute Statement */
	SELECT
		ITM.*
	FROM
		[dbo].MS_AccountCellularTypes AS ITM WITH (NOLOCK)
	WHERE
		(ITM.IsActive = 1 AND ITM.IsDeleted = 0);

END
GO

GRANT EXEC ON dbo.custMS_AccountCellularTypesGet TO PUBLIC
GO