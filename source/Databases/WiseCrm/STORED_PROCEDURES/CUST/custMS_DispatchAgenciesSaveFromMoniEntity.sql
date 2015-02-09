USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_DispatchAgenciesSaveFromMoniEntity')
	BEGIN
		PRINT 'Dropping Procedure custMS_DispatchAgenciesSaveFromMoniEntity'
		DROP  Procedure  dbo.custMS_DispatchAgenciesSaveFromMoniEntity
	END
GO

PRINT 'Creating Procedure custMS_DispatchAgenciesSaveFromMoniEntity'
GO
/******************************************************************************
**		File: custMS_DispatchAgenciesSaveFromMoniEntity.sql
**		Name: custMS_DispatchAgenciesSaveFromMoniEntity
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
**		Date: 12/02/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/02/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_DispatchAgenciesSaveFromMoniEntity
(
	@EntityAgenciesID INT
	, @ModifiedBy NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
		IF (EXISTS(SELECT * FROM [dbo].[MS_DispatchAgencies] WHERE (DispatchAgencyOsId = @EntityAgenciesID) AND (MonitoringStationOSId = 'MI_MASTER') AND (IsDeleted = 0)))
		BEGIN

			UPDATE [dbo].[MS_DispatchAgencies] SET
				[DispatchAgencyTypeId] = TDAT.DispatchAgencyTypeId
				, [MonitoringStationOSId] = 'MI_MASTER'
				, [DispatchAgencyName] = MEA.AgencyName
				, [MsAgencyNumber] = MEA.AgencyNumberID
				, [City] = MEA.CityName
				, [State] = MEA.StateId
				, [ZipCode] = MEA.ZipCode
				, [Phone1] = MEA.Phone1
				, [IsActive] = 1
				, [ModifiedBy] = @ModifiedBy
				, [ModifiedOn] = GETUTCDATE()
			FROM 
				[dbo].[MS_DispatchAgencies] AS DA WITH (NOLOCK)
				INNER JOIN [dbo].[MS_MonitronicsEntityAgencies] AS MEA WITH (NOLOCK)
				ON
					(MEA.EntityAgenciesID = DA.DispatchAgencyOsId)
				INNER JOIN [dbo].fxGetXferTableDispatchAgencyTypes() AS TDAT
				ON
					(TDAT.AgencyTypeID = MEA.AgencyTypeId)
			WHERE
				(DA.DispatchAgencyOsId = @EntityAgenciesID)
				AND (DA.MonitoringStationOSId = 'MI_MASTER')
				AND (DA.IsDeleted = 0);

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
				,[IsActive]
				,[IsDeleted]
				,[CreatedBy]
				,[CreatedOn]
				,[ModifiedBy]
				,[ModifiedOn])
			SELECT
			   TDAT.DispatchAgencyTypeId
			   , 'MI_MASTER' AS MonitoringStationOSId
			   , MDA.EntityAgenciesID AS DispatchAgencyOsId
			   , MDA.AgencyName AS DispatchAgencyName
			   , MDA.AgencyNumberID AS MsAgencyNumber
			   , NULL AS Address1
			   , NULL AS Address2
			   , MDA.CityName AS City
			   , MDA.StateId AS [State]
			   , MDA.ZipCode AS ZipCode
			   , MDA.Phone1
			   , 1 AS IsActive
			   , 0 AS IsDeleted
			   , @ModifiedBy AS CreatedBy
			   , GETUTCDATE() AS CreatedOn
			   , @ModifiedBy AS ModifiedBy
			   , GETUTCDATE() AS ModifiedOn
			FROM
				[dbo].MS_MonitronicsEntityAgencies AS MDA WITH (NOLOCK)
				INNER JOIN [dbo].fxGetXferTableDispatchAgencyTypes() AS TDAT
				ON
					(TDAT.AgencyTypeID = MDA.AgencyTypeId)
			WHERE
				(MDA.EntityAgenciesID = @EntityAgenciesID);

		END
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	/** Return result */
	SELECT * FROM [dbo].[MS_DispatchAgencies] WHERE (DispatchAgencyOsId = @EntityAgenciesID) AND (MonitoringStationOSId = 'MI_MASTER') AND (IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custMS_DispatchAgenciesSaveFromMoniEntity TO PUBLIC
GO

/** EXEC dbo.custMS_DispatchAgenciesSaveFromMoniEntity 41125, 'SYSTEM'; 

SELECT * FROM [dbo].fxGetXferTableDispatchAgencyTypes() AS TDAT
*/