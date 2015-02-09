USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_IndustryAccountFindByCsID')
	BEGIN
		PRINT 'Dropping Procedure custMS_IndustryAccountFindByCsID'
		DROP  Procedure  dbo.custMS_IndustryAccountFindByCsID
	END
GO

PRINT 'Creating Procedure custMS_IndustryAccountFindByCsID'
GO
/******************************************************************************
**		File: custMS_IndustryAccountFindByCsID.sql
**		Name: custMS_IndustryAccountFindByCsID
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
**		Date: 11/17/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	11/17/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_IndustryAccountFindByCsID
(
	@Csid VARCHAR(15)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
	
		SELECT * FROM [dbo].[MS_IndustryAccounts] AS IND WITH (NOLOCK) WHERE (Csid = @Csid);
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_IndustryAccountFindByCsID TO PUBLIC
GO

/** EXEC dbo.custMS_IndustryAccountFindByCsID */