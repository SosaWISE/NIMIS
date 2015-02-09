USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGMC_Lead')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGMC_Lead'
		DROP  Procedure  dbo.custAE_CustomerSWINGMC_Lead
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGMC_Lead'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGMC_Lead.sql
**		Name: custAE_CustomerSWINGMC_Lead
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
CREATE Procedure dbo.custAE_CustomerSWINGMC_Lead
(
	@CustomerIDOld BIGINT,
	@CustomerMasterFileID BIGINT,
	@AddressID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		

			INSERT INTO [WISE_CRM].[dbo].[QL_Leads]
			(
				[AddressId],
				[CustomerTypeId],
				[CustomerMasterFileId],
				[DealerId],
				[LocalizationId],
				[TeamLocationId],
				[SeasonId],
				[SalesRepId],
				[LeadSourceId],
				[LeadDispositionId],
				[LeadDispositionDateChange],
				[Salutation],
		  		[FirstName],
		  		[MiddleName],
				[LastName],
				[Suffix],
				[Gender],
				[SSN],
				[DOB],
				[DL],
				[DLStateID],
				[Email],
				[PhoneHome],
				[PhoneWork], 
				[PhoneMobile]
			)
			SELECT
				@AddressID,
				'LEAD',
				@CustomerMasterFileID,
				5000,
				'en-US',
				0,
				0,
				'SYSSWING',
				15,
			--	14,
				10,
			--	9,	
				GETDATE(),
				[Salutation],
				[FirstName],
		  		[MiddleName],
				[LastName],
				[Suffix],
				'N',
				[SSN],
				[DOB],
				[DL],
				[DLStateID],
				[Email],
				[PhoneHome],
				[PhoneWork]+[PhoneWorkExt], 
				[PhoneCell]

		  FROM [Platinum_Protection_InterimCRM].[dbo].[MC_Lead]
		  WHERE [MC_Lead].LeadID = @CustomerIDOld

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

GRANT EXEC ON dbo.custAE_CustomerSWINGMC_Lead TO PUBLIC
GO

/** EXEC dbo.SPROC_NAME */