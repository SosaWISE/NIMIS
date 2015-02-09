USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountDispatchAgencyAssignmentSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountDispatchAgencyAssignmentSave'
		DROP  Procedure  dbo.custMS_AccountDispatchAgencyAssignmentSave
	END
GO

PRINT 'Creating Procedure custMS_AccountDispatchAgencyAssignmentSave'
GO
/******************************************************************************
**		File: custMS_AccountDispatchAgencyAssignmentSave.sql
**		Name: custMS_AccountDispatchAgencyAssignmentSave
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
**		Date: 12/05/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/05/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountDispatchAgencyAssignmentSave
(
	@AccountId BIGINT
	, @DispatchAgencyOsId INT
	, @MonitoringStationOSId VARCHAR(50)
	, @GpEmployeeId VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @IndustryAccountID BIGINT
		, @DispatchAgencyID INT
		, @DispatchAgencyAssignmentID BIGINT;

	/** Initialize */
	SELECT @IndustryAccountID = IndustryAccountId FROM [dbo].[MS_Accounts] WHERE (AccountID = @AccountId);
	
	BEGIN TRY
		BEGIN TRANSACTION;

			-- ** Find the DA from the DA General list.
			IF (EXISTS(SELECT * FROM [dbo].[MS_DispatchAgencies] WITH (NOLOCK) WHERE (MonitoringStationOSId = @MonitoringStationOSId AND DispatchAgencyOsID = @DispatchAgencyOsId)))
			BEGIN
				PRINT 'Made it here'
				/** Get the DispatchAgencyID and save it. */
				SELECT @DispatchAgencyID = DispatchAgencyID FROM [dbo].[MS_DispatchAgencies] WITH (NOLOCK) WHERE (MonitoringStationOSId = @MonitoringStationOSId AND DispatchAgencyOsID = @DispatchAgencyOsId);
			END
			ELSE
			BEGIN 

				INSERT INTO [dbo].[MS_DispatchAgencies] (
					[DispatchAgencyTypeId]
					,[MonitoringStationOSId]
					,[DispatchAgencyOsId]
					,[DispatchAgencyName]
					,[MsAgencyNumber]
					,[Address1]
					,[Address2]
					,[City]
					,[State]
					,[ZipCode]
					,[Phone1]
					,[Phone2])
				SELECT 
					DAT.DispatchAgencyTypeId
					, @MonitoringStationOSId
					, MEA.EntityAgenciesID AS DispatchAgencyOsId
					, MEA.AgencyName AS DispatchAgencyName
					, MEA.AgencyNumberID AS MsAgencyNumber
					, NULL AS Address1
					, NULL AS Address2
					, MEA.CityName AS City
					, MEA.StateId AS State
					, MEA.ZipCode AS ZipCode
					, MEA.Phone1
					, NULL AS Phone2
				FROM
					[dbo].[MS_MonitronicsEntityAgencies] AS MEA WITH (NOLOCK)
					INNER JOIN [dbo].[fxGetXferTableDispatchAgencyTypes]() AS XREF
					ON
						(XREF.AgencyTypeID = MEA.AgencyTypeId)
					INNER JOIN [dbo].[MS_DispatchAgencyTypes] AS DAT WITH (NOLOCK)
					ON
						(DAT.DispatchAgencyTypeID = XREF.DispatchAgencyTypeId)
				WHERE
					(MEA.EntityAgenciesID = @DispatchAgencyOsId);
				-- // Get Scope
				SET @DispatchAgencyID = SCOPE_IDENTITY();
			END

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
				(MDA.DispatchAgencyID = @DispatchAgencyID)
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

GRANT EXEC ON dbo.custMS_AccountDispatchAgencyAssignmentSave TO PUBLIC
GO

/** EXEC dbo.custMS_AccountDispatchAgencyAssignmentSave 
	130532
	, 86055
	, 'MI_MASTER'
	, 'SOSA001'

*/