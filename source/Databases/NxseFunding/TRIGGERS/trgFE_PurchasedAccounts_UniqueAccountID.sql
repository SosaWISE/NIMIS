USE [NXSE_Funding]
GO
/****** Object:  Trigger [dbo].[trgFE_PurchasedAccounts_UniqueAccountID]    Script Date: 07/19/2008 14:04:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT * FROM dbo.sysobjects WHERE name = 'trgFE_PurchasedAccounts_UniqueAccountID'
	AND OBJECTPROPERTY(id, 'IsTrigger') = 1)
BEGIN
	PRINT 'DROPPING TRIGGER trgFE_PurchasedAccounts_UniqueAccountID'
	DROP TRIGGER [dbo].[trgFE_PurchasedAccounts_UniqueAccountID]
END
GO

PRINT 'CREATING TRIGGER trgFE_PurchasedAccounts_UniqueAccountID'
GO
/******************************************************************************
**		File: trgFE_PurchasedAccounts_UniqueAccountID.sql
**		Name: trgFE_PurchasedAccounts_UniqueAccountID
**		Desc: 
**
**		This trigger will save everytime there is a change in the alarmnet table
**			this is used for looking back at all the serial numbers used.
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés Sosa
**		Date: 03/20/2015
**			
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/20/2015	Andrés Sosa		Created by
*******************************************************************************/
CREATE TRIGGER [dbo].[trgFE_PurchasedAccounts_UniqueAccountID]
   ON  [dbo].[FE_PurchaseContracts]
   FOR INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Check to see if AccountID is already here.
	-- Save Historical Changes
	INSERT INTO dbo.MS_AccountInstallationStatusHistory
	( 
		[AccountID]
		,[TwoWayTestSent]
		,[TwoWayTestSentBy]
		,[TwoWayTestCompleted]
		,[TwoWayTestConfNumber]
		,[TwoWayTestCompletedBy]
		,[ZoneSignalTestCompleted]
		,[ZoneSignalTestConfNumber]
		,[ZoneSignalTestCompletedBy]
		,[AccountCompleted]
		,[AccountCompletedConfNumber]
		,[AccountCompleteBy]
		,[PartialInstallTechID]
		,[PartialInstallDate]
		,[ParialInstallBy]
		,[SystemPullDate]
		,[SystemPullBy]
		,[SystemOOSDate]
		,[SystemOOSConfNumber]
		,[SystemOOSBy]
		,[InstallDate]
		,[InstallBySalesRepID]
		,[InstallBy]
	)
	SELECT
		[AccountID]
		,[TwoWayTestSent]
		,[TwoWayTestSentBy]
		,[TwoWayTestCompleted]
		,[TwoWayTestConfNumber]
		,[TwoWayTestCompletedBy]
		,[ZoneSignalTestCompleted]
		,[ZoneSignalTestConfNumber]
		,[ZoneSignalTestCompletedBy]
		,[AccountCompleted]
		,[AccountCompletedConfNumber]
		,[AccountCompleteBy]
		,[PartialInstallTechID]
		,[PartialInstallDate]
		,[ParialInstallBy]
		,[SystemPullDate]
		,[SystemPullBy]
		,[SystemOOSDate]
		,[SystemOOSConfNumber]
		,[SystemOOSBy]
		,[InstallDate]
		,[InstallBySalesRepID]
		,[InstallBy]
	FROM
		INSERTED;

END