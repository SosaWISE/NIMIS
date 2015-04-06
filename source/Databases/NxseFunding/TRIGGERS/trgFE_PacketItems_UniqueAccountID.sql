USE [NXSE_Funding]
GO
/****** Object:  Trigger [dbo].[MS_AccountInstallationStatus_trgHistory]    Script Date: 07/19/2008 14:04:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT * FROM dbo.sysobjects WHERE name = 'trgFE_PacketItems_UniqueAccountID'
	AND OBJECTPROPERTY(id, 'IsTrigger') = 1)
BEGIN
	PRINT 'DROPPING TRIGGER trgFE_PacketItems_UniqueAccountID'
	DROP TRIGGER [dbo].[trgFE_PacketItems_UniqueAccountID]
END
GO

PRINT 'CREATING TRIGGER trgFE_PacketItems_UniqueAccountID'
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
**		Date: 04/22/2014
**			
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	04/22/2014	Andrés Sosa		Created by
*******************************************************************************/
CREATE TRIGGER [dbo].[trgFE_PacketItems_UniqueAccountID]
   ON  [dbo].[FE_PacketItems]
   FOR INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	/** Get Acount ID*/
	DECLARE @AccountID BIGINT;
	DECLARE @PacketItmeID BIGINT;
	SELECT @AccountID = AccountID, @PacketItmeID = PacketItemID FROM INSERTED;

	-- Check that there is no duplicate account
	IF(EXISTS(SELECT * FROM [dbo].[FE_PacketItems] WHERE (PacketItemID <> @PacketItmeID) AND (AccountId = @AccountID) AND (ReturnAccountFundingStatusId IS NULL)))
	BEGIN
		RAISERROR ('The AccountID is already in use.', 16, 1);
		ROLLBACK TRANSACTION;
	END

END