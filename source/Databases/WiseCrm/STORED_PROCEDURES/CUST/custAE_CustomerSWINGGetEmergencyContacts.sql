USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGGetEmergencyContacts')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGGetEmergencyContacts'
		DROP  Procedure  dbo.custAE_CustomerSWINGGetEmergencyContacts
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGGetEmergencyContacts'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGGetEmergencyContacts.sql
**		Name: custAE_CustomerSWINGGetEmergencyContacts
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
**	04/22/2014	Junryl/Reagan		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerSWINGGetEmergencyContacts
(
    @InterimAccountID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Return records from Emergency Contact **/
		SELECT
	      FirstName
		  ,MiddleInit
		  ,LastName
		  ,Relationship
		  ,PhoneNumber1
		FROM
		[Platinum_Protection_InterimCRM].dbo.[MS_EmergencyContact] MS_EmergencyContact
		WHERE
		  MS_EmergencyContact.AccountID = @InterimAccountID 
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerSWINGGetEmergencyContacts TO PUBLIC
GO

/** EXEC dbo.custAE_CustomerSWINGGetEmergencyContacts 100000 */

/**QUERIES NOTES 

SELECT TOP 10 * FROM [Platinum_Protection_InterimCRM].dbo.[MS_EmergencyContact]  


**/