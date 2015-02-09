USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId'
		DROP  Procedure  dbo.custMS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId
	END
GO

PRINT 'Creating Procedure custMS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId'
GO
/******************************************************************************
**		File: custMS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId.sql
**		Name: custMS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId
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
**		Date: 01/02/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/02/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId
(
	@DispatchAgencyId INT
	, @AccountId BIGINT
	, @IndustryAccountId BIGINT
	, @GpEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @DispatchAgencyAssignmentID BIGINT;

	/** Initialize */
	SELECT @IndustryAccountId = IndustryAccountId FROM [dbo].[MS_Accounts] WHERE (AccountID = @AccountId);
	
	BEGIN TRY
		BEGIN TRANSACTION;

			/** Save the assigned DA to the account */
			INSERT INTO [dbo].[MS_AccountDispatchAgencyAssignments] (
				[DispatchAgencyId]
				,[DispatchAgencyTypeId]
				,[AccountId]
				,[IndustryAccountId]
				,[DispatchAgencyName]
				,[Phone1]
				,[IsVerified])
			SELECT
				MDA.DispatchAgencyID
				, MDA.DispatchAgencyTypeId
				, @AccountId
				, @IndustryAccountId
				, MDA.DispatchAgencyName
				, MDA.Phone1
				, 1 AS IsVerified
			FROM
				[dbo].[MS_DispatchAgencies] AS MDA WITH (NOLOCK)
			WHERE
				(MDA.DispatchAgencyID = @DispatchAgencyId)
			-- // Get Scope
			SET @DispatchAgencyAssignmentID = SCOPE_IDENTITY();
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** RETURN RESULT */
	SELECT * FROM [dbo].[vwMS_AccountDispatchAgencyAssignment] WHERE (DispatchAgencyAssignmentID = @DispatchAgencyAssignmentID);
END
GO

GRANT EXEC ON dbo.custMS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId TO PUBLIC
GO

/** EXEC dbo.custMS_AccountDispatchAgencyAssignmentSaveByDispatchAgencyId 
	130532
	, 86055
	, 'MI_MASTER'
	, 'SOSA001'

*/