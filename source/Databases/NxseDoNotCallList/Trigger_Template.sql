USE [NXSE_DoNotCallList]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT * FROM dbo.sysobjects WHERE name = 'trigger_name'
	AND OBJECTPROPERTY(id, 'IsTrigger') = 1)
BEGIN
	PRINT 'DROPPING TRIGGER trigger_name'
	DROP TRIGGER [dbo].[trigger_name]
END
GO

PRINT 'CREATING TRIGGER trigger_name'
GO
/******************************************************************************
**		File: trigger_name.sql
**		Name: trigger_name
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
**		Date: 04/22/2014
**			
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	04/22/2014	Andrés Sosa		Created
*******************************************************************************/
CREATE TRIGGER [dbo].[trigger_name]
   ON  --tablename
   FOR INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Example: Save Historical Changes
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