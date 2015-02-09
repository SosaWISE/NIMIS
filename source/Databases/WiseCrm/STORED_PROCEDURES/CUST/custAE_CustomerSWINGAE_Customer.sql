USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGAE_Customer')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGAE_Customer'
		DROP  Procedure  dbo.custAE_CustomerSWINGAE_Customer
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGAE_Customer'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGAE_Customer.sql
**		Name: custAE_CustomerSWINGAE_Customer
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
**	03/28/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerSWINGAE_Customer
(
	@CustomerIDOld BIGINT,
	@CustomerMasterFileID BIGINT,
	@LeadID BIGINT,
	@AddressID BIGINT

)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		INSERT INTO [WISE_CRM].[dbo].[AE_Customers]
				(
					[CustomerTypeId],
					[CustomerMasterFileId],
					[DealerId],
					[AddressId],
					[LeadId],
					[LocalizationId],
					[Prefix],
					[FirstName],
					[MiddleName],
					[LastName],
					[Postfix],
					[Gender],
					[PhoneHome],
					[PhoneWork], 
					[PhoneMobile],
					[DOB],
					[SSN],
					[Email],
					[Username],
					[Password]
				)
				SELECT
					'PRI',
					@CustomerMasterFileID,
					5000,
					@AddressID,
					@LeadID,
					(SELECT [LocalizationId] FROM [WISE_CRM].[dbo].[QL_Leads] WHERE LeadID = @LeadID) [LocalizationId],
					[Salutation],
					[FirstName],
		  			[MiddleName],
					[LastName],
					[Suffix],
					'N',
					[PhoneHome],
					[PhoneWork]+[PhoneWorkExt], 
					[PhoneCell],
					[DOB],
					[SSN],
					[Email],
					NULL,
					NULL

			  FROM [Platinum_Protection_InterimCRM].[dbo].[MC_Lead]
			  WHERE [MC_Lead].LeadID =  @CustomerIDOld

			  SELECT SCOPE_IDENTITY()




	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerSWINGAE_Customer TO PUBLIC
GO

/** EXEC dbo.SPROC_NAME */