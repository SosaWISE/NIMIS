USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'wiseMC_ChangeTeamLocationIdByAccountID')
	BEGIN
		PRINT 'Dropping Procedure wiseMC_ChangeTeamLocationIdByAccountID'
		DROP  Procedure  dbo.wiseMC_ChangeTeamLocationIdByAccountID
	END
GO

PRINT 'Creating Procedure wiseMC_ChangeTeamLocationIdByAccountID'
GO
/******************************************************************************
**		File: wiseMC_ChangeTeamLocationIdByAccountID.sql
**		Name: wiseMC_ChangeTeamLocationIdByAccountID
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
**		Date: 07/30/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/30/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.wiseMC_ChangeTeamLocationIdByAccountID
(
	@AccountID BIGINT
	, @TeamLocationID INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** CHECK THAT THIS IS A LEGIT REP ID. */
	IF(NOT EXISTS (SELECT * FROM [dbo].[MS_Accounts] WHERE AccountID = @AccountID))
	BEGIN
		PRINT 'THAT MS Account does not exits';
		RETURN;
	END
	IF(NOT EXISTS (SELECT * FROM [WISE_HumanResource].[dbo].[RU_TeamLocations] WHERE @TeamLocationID = @TeamLocationID))
	BEGIN
		PRINT 'THAT TeamLocation does not exits';
		RETURN;
	END

	BEGIN TRY
		BEGIN TRANSACTION
			/** GET LEAD ID */
			UPDATE [dbo].[QL_Leads] SET TeamLocationId = @TeamLocationID WHERE (LeadID IN (SELECT LeadID FROM dbo.vwAE_CustomerAccounts WHERE (AccountId = @AccountID)));
			UPDATE [dbo].[QL_Address] SET TeamLocationId = @TeamLocationID WHERE (AddressID IN (SELECT AddressID FROM dbo.QL_Leads WHERE (LeadID IN (SELECT LeadID FROM dbo.vwAE_CustomerAccounts WHERE (AccountId = @AccountID)))));

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	--SELECT LeadID, TeamLocationId FROM dbo.QL_Leads WHERE (LeadID = @LeadID);
	--SELECT AddressID, TeamLocationId FROM dbo.QL_Address WHERE (AddressID = @AddressID);
	--SELECT AccountID, TeamLocationId FROM dbo.vwMS_AccountSalesInformations WHERE (AccountID = @AccountID);
END
GO

GRANT EXEC ON dbo.wiseMC_ChangeTeamLocationIdByAccountID TO PUBLIC
GO

/** */
EXEC dbo.wiseMC_ChangeTeamLocationIdByAccountID 191201, 7;