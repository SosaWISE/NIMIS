USE [WISE_SurveyEngine]
GO
/****** Object:  Trigger [dbo].[MS_AccountInstallationStatus_trgHistory]    Script Date: 07/19/2008 14:04:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT * FROM dbo.sysobjects WHERE name = 'AE_CustomerAccountsTrg'
	AND OBJECTPROPERTY(id, 'IsTrigger') = 1)
BEGIN
	PRINT 'DROPPING TRIGGER AE_CustomerAccountsTrg'
	DROP TRIGGER [dbo].[AE_CustomerAccountsTrg]
END
GO

PRINT 'CREATING TRIGGER AE_CustomerAccountsTrg'
GO
/******************************************************************************
**		File: MS_AccountInstallationStatus_trgHistory.sql
**		Name: MS_AccountInstallationStatus_trgHistory
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
**		Date: 03/14/2011
**			
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	03/14/2011	Andrés Sosa		Created by
*******************************************************************************/
CREATE TRIGGER [dbo].[AE_CustomerAccountsTrg]
   ON  [dbo].[AE_CustomerAccounts]
   FOR INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

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