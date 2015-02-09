USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGAdd_Dnc')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGAdd_Dnc'
		DROP  Procedure  dbo.custAE_CustomerSWINGAdd_Dnc
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGAdd_Dnc'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGAdd_Dnc.sql
**		Name: custAE_CustomerSWINGAdd_Dnc
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
**		Date: 03/28/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	05/23/2014	Junryl/Reagan		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerSWINGAdd_Dnc
(
	@Creator VARCHAR(50)
	,@PhoneNumber VARCHAR(10)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */		
	DECLARE @Dnc_Status VARCHAR(max)
	DECLARE @AreaCodeId CHAR(3)
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Transfer data */

		SELECT @AreaCodeId = AreaCodeID FROM [dbo].[DC_AreaCodes] WHERE  AreaCodeID = SUBSTRING(@PhoneNumber, 1, 3);
		PRINT @AreaCodeId   + ' - AreaCodeId';

		IF @AreaCodeId IS NOT NULL
		BEGIN
			INSERT INTO [dbo].[DC_CompanyPhoneNumbers]
			(
				[PhoneNumberID]
				,[AreaCodeId]			
				,[PhoneNumber]
				,[CompanyId]
				,[CreatedOn]	
			)
			VALUES
			(
				@PhoneNumber
				,@AreaCodeId
				--,(SELECT AreaCodeID FROM [dbo].[DC_AreaCodes] WHERE  AreaCodeID = SUBSTRING(@PhoneNumber, 1, 3))
				,SUBSTRING(@PhoneNumber, 4, 8)
				,@Creator
				,GETDATE()
			)
		
			SET @Dnc_Status ='Success';	
			PRINT  @Dnc_Status;
		END
		ELSE
		BEGIN
			SET @Dnc_Status ='Invalid Phone Number';	
			PRINT  @Dnc_Status;
		END	
		

		--ROLLBACK TRANSACTION;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
			
		SET @Dnc_Status = ERROR_MESSAGE();
		SELECT @Dnc_Status AS Dnc_Status;

		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	SELECT @Dnc_Status AS Dnc_Status;

END
GO

GRANT EXEC ON dbo.custAE_CustomerSWINGAdd_Dnc TO PUBLIC
GO

/** EXEC dbo.custAE_CustomerSWINGAdd_Dnc DevUser, 3854567898 */