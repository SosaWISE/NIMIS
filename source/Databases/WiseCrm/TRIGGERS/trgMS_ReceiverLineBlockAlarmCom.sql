USE [WISE_CRM]
GO
/****** Object:  Trigger [dbo].[MS_AccountInstallationStatus_trgHistory]    Script Date: 07/19/2008 14:04:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT * FROM dbo.sysobjects WHERE name = 'MS_ReceiverLineBlockAlarmComTrg'
	AND OBJECTPROPERTY(id, 'IsTrigger') = 1)
BEGIN
	PRINT 'DROPPING TRIGGER MS_ReceiverLineBlockAlarmComTrg'
	DROP TRIGGER [dbo].[MS_ReceiverLineBlockAlarmComTrg]
END
GO

PRINT 'CREATING TRIGGER MS_ReceiverLineBlockAlarmComTrg'
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
**		Date: 01/27/2015
**			
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/27/2015	Andrés Sosa		Created by
*******************************************************************************/
CREATE TRIGGER [dbo].[MS_ReceiverLineBlockAlarmComTrg]
   ON  [dbo].[MS_ReceiverLineBlockAlarmCom]
   FOR INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Save Historical Changes
	INSERT INTO [dbo].[MS_ReceiverLineBlockAlarmComHistory] (
		[ReceiverLineBlockID]
		, [SerialNumber]
		, [CustomerId]
		, [IsTwoWay]
		, [RegisteredDate]
		, [UnRegisteredDate])
	SELECT
		*
	FROM
		INSERTED;

END