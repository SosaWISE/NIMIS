USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountSalesInformationSaveSalesRepID')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountSalesInformationSaveSalesRepID'
		DROP  Procedure  dbo.custMS_AccountSalesInformationSaveSalesRepID
	END
GO

PRINT 'Creating Procedure custMS_AccountSalesInformationSaveSalesRepID'
GO
/******************************************************************************
**		File: custMS_AccountSalesInformationSaveSalesRepID.sql
**		Name: custMS_AccountSalesInformationSaveSalesRepID
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
**		Date: 03/13/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/13/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountSalesInformationSaveSalesRepID
(
	@CustomerMasterFileID BIGINT
	, @AccountID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** INITIALIZE */
	DECLARE @SalesRepID VARCHAR(20)
		, @LeadID BIGINT;

	SELECT TOP 1 @LeadID = LeadId FROM [dbo].[AE_CustomerAccounts] WHERE (@CustomerMasterFileID = @CustomerMasterFileID AND AccountId = @AccountID);

	BEGIN TRY
		BEGIN TRANSACTION
			/** Get Sales Rep ID*/
			SELECT @SalesRepID = SalesRepID FROM [dbo].[QL_Leads] WHERE (LeadID = @LeadId);

			IF (@SalesRepID IS NOT NULL)
			BEGIN
				IF(EXISTS(SELECT * FROM [dbo].[MS_AccountSalesInformations] WHERE (AccountID = @AccountID)))
				BEGIN
					UPDATE [dbo].[MS_AccountSalesInformations] SET SalesRepId = @SalesRepID WHERE (AccountID = @AccountID);
				END
				ELSE
				BEGIN
					INSERT INTO [dbo].[MS_AccountSalesInformations] (AccountID, SalesRepId) VALUES (@AccountID, @SalesRepID);
				END
			END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- Return result
	SELECT * FROM [dbo].[vwMS_AccountSalesInformations] WHERE (AccountID = @AccountID);
END
GO

GRANT EXEC ON dbo.custMS_AccountSalesInformationSaveSalesRepID TO PUBLIC
GO

/** EXEC dbo.custMS_AccountSalesInformationSaveSalesRepID 181051 */