USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_LicensesGetTechCertifiedCount')
	BEGIN
		PRINT 'Dropping Procedure custLM_LicensesGetTechCertifiedCount'
		DROP  Procedure  dbo.custLM_LicensesGetTechCertifiedCount
	END
GO

PRINT 'Creating Procedure custLM_LicensesGetTechCertifiedCount'
GO
/******************************************************************************
**		File: custLM_LicensesGetTechCertifiedCount.sql
**		Name: custLM_LicensesGetTechCertifiedCount
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
**		Date: 10/13/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	10/13/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure [dbo].[custLM_LicensesGetTechCertifiedCount]
(
	@ServiceTechID INT
	, @RequirementID INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		--DECLARE @ServiceTechID INT
		--SET @ServiceTechID = 99

	
		DECLARE @ServiceTechTypeID INT
		SELECT
			@ServiceTechTypeID = ServiceTechTypeID--User
		FROM Platinum_Protection_InterimCRM.dbo.TS_ServiceTechs
		WHERE
			ServiceTechID = @ServiceTechID
		

		DECLARE @UserIDs TABLE (UserID INT)

		IF(@ServiceTechTypeID = 1)--User
		BEGIN
			PRINT 'User'
		
			INSERT INTO @UserIDs
			SELECT
				UserID
			FROM Platinum_Protection_InterimCRM.dbo.TS_ServiceTechs
			WHERE
				ServiceTechID = @ServiceTechID
		END
		ELSE IF(@ServiceTechTypeID = 2)--Office
		BEGIN
			PRINT 'Office'
		
			DECLARE @SeasonID INT
			DECLARE @TeamLocationID INT
		
			--get Season and TeamLocationID
			SELECT
				@SeasonID = RUTL.SeasonID
				, @TeamLocationID = STEC.TeamLocationID
			FROM [WISE_HumanResource].dbo.RU_TeamLocations AS RUTL WITH(NOLOCK)
			INNER JOIN Platinum_Protection_InterimCRM.dbo.TS_ServiceTechs AS STEC WITH(NOLOCK)
			ON
				(RUTL.TeamLocationID = STEC.TeamLocationID)
			WHERE
				ServiceTechID = @ServiceTechID

			PRINT 'SeasonID ' + CAST(@SeasonID AS NVARCHAR(10))
			PRINT 'TeamLocationID ' +  CAST(@TeamLocationID AS NVARCHAR(10))


			INSERT INTO @UserIDs
			SELECT
				Tree.UserID
			FROM [WISE_HumanResource].dbo.fxGetReportingTree(null,null,null,null,@SeasonID) AS Tree
			INNER JOIN [WISE_HumanResource].dbo.RU_Teams AS RT WITH(NOLOCK)
			ON
				(Tree.TeamID = RT.TeamID)
			INNER JOIN [WISE_HumanResource].dbo.RU_TeamLocations AS RUTL WITH(NOLOCK)
			ON
				(RT.TeamLocationID = RUTL.TeamLocationID)
			INNER JOIN [WISE_HumanResource].dbo.RU_UserType AS RUT WITH(NOLOCK)
			ON
				(Tree.UserTypeId = RUT.UserTypeID)
			WHERE
				(RUTL.TeamLocationID = @TeamLocationID)
				AND (RUT.RoleLocationID = 2)--techs
				AND (RUT.UserTypeID <> 6)--don't count lead tech???
		END
		ELSE IF(@ServiceTechTypeID = 3)--Contractor
		BEGIN
			PRINT 'Contractor'
		
			--do nothing
		END

		--SELECT * FROM @UserIDs
		SELECT
			COUNT(*) AS SmokeAlarmCertifiedCount
		FROM [WISE_HumanResource].dbo.RU_Users AS RU WITH(NOLOCK)
		INNER JOIN dbo.LM_Licenses AS LIC WITH(NOLOCK)
		ON
			(RU.GPEmployeeID = LIC.GPEmployeeID)
		WHERE
			((LIC.RequirementID = 27) AND (LIC.RequirementsAreMet = 1))--Texas_Fire_Alarm_Technician_License is met
			AND (RU.UserID IN (SELECT * FROM @UserIDs))
			AND ((RU.IsActive = 1) AND (RU.IsDeleted = 0))

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_LicensesGetTechCertifiedCount TO PUBLIC
GO

/** EXEC dbo.custLM_LicensesGetTechCertifiedCount */