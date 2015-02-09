USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityPrefixesSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityPrefixesSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntityPrefixesSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityPrefixesSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityPrefixesSave.sql
**		Name: custMS_MonitronicsEntityPrefixesSave
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
**		Auth: Jake Jenne
**		Date: 12/2/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/2/2014	Jake Jenne		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityPrefixesSave
(
	@CellFlagID VARCHAR(50)
	, @CsNoLength TINYINT
	, @CmPurchase VARCHAR(50)
	, @ServCoNO INT
	, @CellProvider VARCHAR(50)
	, @SystemTypeId VARCHAR(50)
	, @CoNo SMALLINT
	, @BrandedFlag VARCHAR(50)
	, @ReceiverPhone VARCHAR(50)
	, @AlarmNetCityCs VARCHAR(50)
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
			IF (EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntityPrefixes] WHERE (CellFlagID = @CellFlagID)))
			BEGIN
				UPDATE [dbo].[MS_MonitronicsEntityPrefixes] SET
					CsNoLength = @CsNoLength
					, CmPurchase = @CmPurchase
					, ServCoNO = @ServCoNO
					, CellProvider = @CellProvider
					, SystemTypeId = @SystemTypeId
					, CoNo = @CoNo
					, BrandedFlag = @BrandedFlag
					, ReceiverPhone = @ReceiverPhone
					, AlarmNetCityCs = @AlarmNetCityCs
					, IsActive = 1
					, IsDeleted = 0
					, ModifiedBy = @ModifiedBy
					, ModifiedOn = GETUTCDATE()
				WHERE
					(CellFlagID = @CellFlagID);
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[MS_MonitronicsEntityPrefixes] (
					CellFlagID
					, CsNoLength
					, CmPurchase
					, ServCoNO
					, CellProvider
					, SystemTypeId
					, CoNo
					, BrandedFlag
					, ReceiverPhone
					, AlarmNetCityCs
					, IsActive
					, IsDeleted
					, CreatedBy
					, CreatedOn
					, ModifiedBy
					, ModifiedOn
				) VALUES (
					@CellFlagID
					, @CsNoLength
					, @CmPurchase
					, @ServCoNO
					, @CellProvider
					, @SystemTypeId
					, @CoNo
					, @BrandedFlag
					, @ReceiverPhone
					, @AlarmNetCityCs
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
	SELECT * FROM [dbo].[MS_MonitronicsEntityPrefixes] WHERE (CellFlagID = @CellFlagID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityPrefixesSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityPrefixesSave */