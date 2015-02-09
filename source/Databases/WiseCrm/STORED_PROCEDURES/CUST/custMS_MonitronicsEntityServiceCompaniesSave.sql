USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityServiceCompaniesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityServiceCompaniesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityServiceCompaniesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityServiceCompaniesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityServiceCompaniesSave.sql
**		Name: custMS_MonitronicsEntityServiceCompaniesSave
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
**		Auth: Andrew McFadden
**		Date: 12/9/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/9/2014	Andrew McFadden		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityServiceCompaniesSave
(
	@ServCoNumberID VARCHAR(50)
	, @ServCoName VARCHAR(50)
	, @ServCoAddress1 VARCHAR(50)
	, @ServCoAddress2 VARCHAR(50)
	, @CityName VARCHAR(50)
	, @StateId VARCHAR(50)
	, @ZipCode VARCHAR(50)
	, @Phone1 VARCHAR(50)
	, @ModifiedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
			-- Check if exists
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityServiceCompanies] WHERE (ServCoNumberID = @ServCoNumberID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityServiceCompanies] SET
					ServCoName = @ServCoName
					, ServCoAddress1 = @ServCoAddress1
					, ServCoAddress2 = @ServCoAddress2
					, CityName = @CityName
					, StateId = @StateId
					, ZipCode = @ZipCode
					, Phone1 = @Phone1
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(ServCoNumberID = @ServCoNumberID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityServiceCompanies] (
					ServCoNumberID
					, ServCoName
					, ServCoAddress1
					, ServCoAddress2
					, CityName
					, StateId
					, ZipCode
					, Phone1
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@ServCoNumberID
					, @ServCoName
					, @ServCoAddress1
					, @ServCoAddress2
					, @CityName
					, @StateId
					, @ZipCode
					, @Phone1
					, 1
					, 0
					, @ModifiedBy
					, GETUTCDATE()
					, @ModifiedBy
					, GETUTCDATE()
				); 
			END
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	-- Return row
	SELECT * FROM [dbo].[MS_MonitronicsEntityServiceCompanies] WHERE (ServCoNumberID = @ServCoNumberID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityServiceCompaniesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityServiceCompaniesSave */