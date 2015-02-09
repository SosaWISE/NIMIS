USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGMS_EmergencyContact')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGMS_EmergencyContact'
		DROP  Procedure  dbo.custAE_CustomerSWINGMS_EmergencyContact
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGMS_EmergencyContact'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGMS_EmergencyContact.sql
**		Name: custAE_CustomerSWINGMS_EmergencyContact
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
CREATE Procedure dbo.custAE_CustomerSWINGMS_EmergencyContact
(
	@InterimAccountID BIGINT,
	@Customer1IDNew BIGINT,
	@AccountID BIGINT

)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
			/*step 9.Swinging of Emergency Contacts happens here - but no documentation for this
							*/	
							
				-- swing emergency contacts	
				INSERT INTO [dbo].[MS_EmergencyContacts]
					   ([CustomerId]
					   ,[AccountId]
					   ,[RelationshipId]
					   ,[OrderNumber]
					   ,[Allergies]
					   ,[MedicalConditions]
					   ,[HasKey]
					   ,[DOB]
					   ,[Prefix]
					   ,[FirstName]
					   ,[MiddleName]
					   ,[LastName]
					   ,[Postfix]
					   ,[Email]
					   ,[Password]
					   ,[Phone1]
					   ,[Phone1TypeId]
					   ,[Phone2]
					   ,[Phone2TypeId]
					   ,[Phone3]
					   ,[Phone3TypeId]
					   ,[Comment1]
					   ,[IsActive]
					   ,[IsDeleted]
					   ,[ModifiedOn]
					   ,[ModifiedBy]
					   ,[CreatedOn]
					   ,[CreatedBy]
					)
				SELECT 
					   @Customer1IDNew  -- not sure if we will be using only Customer1ID
					  ,@AccountID
					  ,(SELECT RelationshipID FROM [WISE_CRM].[dbo].[MS_EmergencyContactRelationships] WHERE MsRelationshipId = MSECR.RelationshipName ) AS [RelationshipId]
						-- need to verify on the relationshipid 
					  ,[ContactOrder]
					  , NULL AS [Allergies]
					  , NULL AS [MedicalConditions]
					  ,[HasKeys]
					  , NULL AS [DOB]
					  ,[Prefix]
					  ,[FirstName]
					  ,[MiddleInit]
					  ,[LastName]
					  ,[Suffix]
					  ,[Email]
					  ,[Passcode]
					  ,[PhoneNumber1]
					  ,(SELECT MSECPT.PhoneTypeID FROM [WISE_CRM].[dbo].[MS_EmergencyContactPhoneTypes]  MSECPT  INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContactPhoneType] MSECPT2 ON MSECPT2.AvantGuardCode = MSECPT.MsPhoneTypeId WHERE MSECPT2.[EmergencyContactPhoneTypeId] = MSEC.PhoneTypeId1)
					  ,[PhoneNumber2]
					  ,(SELECT MSECPT.PhoneTypeID FROM [WISE_CRM].[dbo].[MS_EmergencyContactPhoneTypes]  MSECPT  INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContactPhoneType] MSECPT2 ON MSECPT2.AvantGuardCode = MSECPT.MsPhoneTypeId WHERE MSECPT2.[EmergencyContactPhoneTypeId] = MSEC.PhoneTypeId2)
					  ,[PhoneNumber3]
					  ,(SELECT MSECPT.PhoneTypeID FROM [WISE_CRM].[dbo].[MS_EmergencyContactPhoneTypes]  MSECPT  INNER JOIN [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContactPhoneType] MSECPT2 ON MSECPT2.AvantGuardCode = MSECPT.MsPhoneTypeId WHERE MSECPT2.[EmergencyContactPhoneTypeId] = MSEC.PhoneTypeId3)
					  , NULL AS [Comment1]
					  , 1 AS [IsActive]
					  , 0 AS [IsDeleted]
					  ,[ModifiedByDate]
					  ,[ModifiedByID]
					  ,[CreatedByDate]
					  ,[CreatedByID]
		
				  FROM	[Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContact] MSEC
						INNER JOIN 
						[Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContactRelationships] MSECR
						ON
						MSEC.[EmergencyContactRelationshipId] = MSECR.[EmergencyContactRelationshipId]
					--	INNER JOIN
					--	[WISE_CRM].[dbo].[MS_EmergencyContactRelationships] WMSECR
					--	ON
					--	WMSECR.MsRelationshipId = MSECR.RelationshipName
					
				--	WHERE	MSEC.[AccountID] = 100003
				  WHERE	MSEC.[AccountID] = @InterimAccountID

						
				/*
					mapping helper for emergency contacts

					SELECT *  FROM [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContactRelationships]
					SELECT * FROM [dbo].[MS_EmergencyContactRelationships]

					--select  Top(100)  * from [WISE_CRM].[dbo].[MS_EmergencyContacts]
					--select Top(100) * from [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContact]

					SELECT * FROM [dbo].[MS_EmergencyContactPhoneTypes]
					SELECT * FROM [Platinum_Protection_InterimCRM].[dbo].[MS_EmergencyContactPhoneType]

				*/			


	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerSWINGMS_EmergencyContact TO PUBLIC
GO

/** EXEC dbo.SPROC_NAME */