USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountMonitorInformationsByAccountID')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountMonitorInformationsByAccountID'
		DROP  Procedure  dbo.custMS_AccountMonitorInformationsByAccountID
	END
GO

PRINT 'Creating Procedure custMS_AccountMonitorInformationsByAccountID'
GO
/******************************************************************************
**		File: custMS_AccountMonitorInformationsByAccountID.sql
**		Name: custMS_AccountMonitorInformationsByAccountID
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
**		Date: 06/24/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/24/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountMonitorInformationsByAccountID
(
	@AccountID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		SELECT * FROM [dbo].[vwMS_AccountMonitorInformations] WHERE (AccountID = @AccountID);
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_AccountMonitorInformationsByAccountID TO PUBLIC
GO

/** EXEC dbo.custMS_AccountMonitorInformationsByAccountID 130532 */