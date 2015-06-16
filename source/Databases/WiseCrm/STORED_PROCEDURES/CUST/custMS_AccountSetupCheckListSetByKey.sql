USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountSetupCheckListSetByKey')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountSetupCheckListSetByKey'
		DROP  Procedure  dbo.custMS_AccountSetupCheckListSetByKey
	END
GO

PRINT 'Creating Procedure custMS_AccountSetupCheckListSetByKey'
GO
/******************************************************************************
**		File: custMS_AccountSetupCheckListSetByKey.sql
**		Name: custMS_AccountSetupCheckListSetByKey
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
**		Date: 06/03/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/03/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountSetupCheckListSetByKey
(
	@AccountID BIGINT
	, @Key VARCHAR(100)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Check to make sure that there is a row for this account */
	IF (NOT EXISTS(SELECT * FROM dbo.MS_AccountSetupCheckLists WHERE (AccountID = @AccountID)))
	BEGIN
		INSERT INTO dbo.MS_AccountSetupCheckLists (AccountID) VALUES (@AccountID);
	END

	BEGIN TRY
		IF (@Key = 'Qualify')
		BEGIN
			UPDATE dbo.MS_AccountSetupCheckLists SET Qualify = GETUTCDATE() WHERE (AccountID = @AccountID);
		END

		IF (@Key = 'SalesInfo')
		BEGIN
			UPDATE dbo.MS_AccountSetupCheckLists SET SalesInfo = GETUTCDATE() WHERE (AccountID = @AccountID);
		END

		IF (@Key = 'PreSurvey')
		BEGIN
			UPDATE dbo.MS_AccountSetupCheckLists SET PreSurvey = GETUTCDATE() WHERE (AccountID = @AccountID);
		END

		IF (@Key = 'IndustryNumbers')
		BEGIN
			UPDATE dbo.MS_AccountSetupCheckLists SET IndustryNumbers = GETUTCDATE() WHERE (AccountID = @AccountID);
		END

		IF (@Key = 'EmergencyContacts')
		BEGIN
			UPDATE dbo.MS_AccountSetupCheckLists SET EmergencyContacts = GETUTCDATE() WHERE (AccountID = @AccountID);
		END

		IF (@Key = 'SystemDetails')
		BEGIN
			UPDATE dbo.MS_AccountSetupCheckLists SET SystemDetails = GETUTCDATE() WHERE (AccountID = @AccountID);
		END

		IF (@Key = 'RegisterCell')
		BEGIN
			UPDATE dbo.MS_AccountSetupCheckLists SET RegisterCell = GETUTCDATE() WHERE (AccountID = @AccountID);
		END

		IF (@Key = 'SystemTest')
		BEGIN
			UPDATE dbo.MS_AccountSetupCheckLists SET SystemTest = GETUTCDATE() WHERE (AccountID = @AccountID);
		END

		IF (@Key = 'TechInspection')
		BEGIN
			UPDATE dbo.MS_AccountSetupCheckLists SET TechInspection = GETUTCDATE() WHERE (AccountID = @AccountID);
		END

		IF (@Key = 'PostSurvey')
		BEGIN
			UPDATE dbo.MS_AccountSetupCheckLists SET PostSurvey = GETUTCDATE() WHERE (AccountID = @AccountID);
		END

		IF (@Key = 'InitialPayment')
		BEGIN
			UPDATE dbo.MS_AccountSetupCheckLists SET InitialPayment = GETUTCDATE() WHERE (AccountID = @AccountID);
		END

		IF (@Key = 'SubmitAccountOnline')
		BEGIN
			UPDATE dbo.MS_AccountSetupCheckLists SET SubmitAccountOnline = GETUTCDATE() WHERE (AccountID = @AccountID);
		END

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	SELECT * FROM dbo.MS_AccountSetupCheckLists WHERE (AccountID = @AccountID);
END
GO

GRANT EXEC ON dbo.custMS_AccountSetupCheckListSetByKey TO PUBLIC
GO

/** EXEC dbo.custMS_AccountSetupCheckListSetByKey 191269, 'Qualify' */