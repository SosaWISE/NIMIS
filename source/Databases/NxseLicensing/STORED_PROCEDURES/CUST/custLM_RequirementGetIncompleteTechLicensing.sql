USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_RequirementGetIncompleteTechLicensing')
	BEGIN
		PRINT 'Dropping Procedure custLM_RequirementGetIncompleteTechLicensing'
		DROP  Procedure  dbo.custLM_RequirementGetIncompleteTechLicensing
	END
GO

PRINT 'Creating Procedure custLM_RequirementGetIncompleteTechLicensing'
GO
/******************************************************************************
**		File: custLM_RequirementGetIncompleteTechLicensing.sql
**		Name: custLM_RequirementGetIncompleteTechLicensing
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
CREATE Procedure [dbo].[custLM_RequirementGetIncompleteTechLicensing]
(
	@SeasonID INT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		--Missing Tech Requirements and incomplete licenses
		SELECT LMR.RequirementID
				, LMR.RequirementName
				, CASE 
					WHEN LML.LicenseID IS NULL
					THEN 0
					ELSE LML.LicenseID
					END AS LicenseID
				, LML.IssueDate
				, LML.ExpirationDate
				, LML.LicenseNumber
				, ARPL.LocationName
				, RUU.FullName
				, RUU.UserID
				, CASE 
					WHEN LML.LicenseID IS NULL
					THEN 'Requirement Not Met'
					ELSE 'License Incomplete'
					END AS [Description]
		FROM [WISE_HumanResource].dbo.RU_Users AS RUU WITH (NOLOCK)
			INNER JOIN [WISE_HumanResource].dbo.RU_Recruits AS RUR WITH (NOLOCK)
			ON
				RUU.UserID = RUR.UserID 
				AND RUR.SeasonID = @SeasonID 
				AND RUR.IsDeleted = 0
				AND RUR.IsActive = 1
			INNER JOIN [WISE_HumanResource].dbo.RU_Teams AS RUT WITH (NOLOCK)
			ON 
				RUR.TeamID = RUT.TeamID
			INNER JOIN [WISE_HumanResource].dbo.RU_TeamLocations AS RUL WITH (NOLOCK)
			ON
				RUT.TeamLocationID = RUL.TeamLocationID
			INNER JOIN vwAllRequirementsPerLocation AS ARPL WITH (NOLOCK)
			ON
				RUL.City = ARPL.LocationName
				AND ARPL.LocationTypeID = 5 -- City Type
			INNER JOIN LM_Requirements LMR WITH (NOLOCK)
			ON
				ARPL.RequirementID = LMR.RequirementID
				AND LMR.RequirementTypeID = 3 --Tech Type
			LEFT OUTER JOIN LM_Licenses LML WITH (NOLOCK)
			ON
				LMR.RequirementID = LML.RequirementID
				AND RUU.GPEmployeeID = LML.GPEmployeeID

		WHERE (LML.IsActive = 1 OR LML.IsActive IS NULL)
			AND (LML.IsDeleted = 0 OR LML.IsDeleted IS NULL)
			AND (LML.RequirementsAreMet = 0 OR LML.RequirementsAreMet IS NULL)
			AND LMR.IsActive = 1
			AND LMR.IsDeleted = 0

		UNION

		--Global Tech Requirements
		SELECT LMR.RequirementID
				, LMR.RequirementName
				, LML.LicenseID
				, LML.IssueDate
				, LML.ExpirationDate
				, LML.LicenseNumber
				, LOC.LocationName
				, RUU.FullName
				, RUU.UserID
				, CASE 
					WHEN LML.LicenseID IS NULL
					THEN 'Requirement Not Met'
					ELSE 'License Incomplete'
					END AS [Description]
		FROM LM_Requirements AS LMR WITH (NOLOCK)
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMR.RequirementID = LML.RequirementID
			INNER JOIN [WISE_HumanResource].dbo.RU_Users AS RUU WITH (NOLOCK)
			ON
				LML.GPEmployeeID = RUU.GPEmployeeID
			INNER JOIN LM_Locations AS LOC WITH (NOLOCK)
			ON
				LMR.LocationID = LOC.LocationID
				AND LOC.LocationTypeID = 1 --Global Type
		WHERE (LML.IsActive = 1 OR LML.IsActive IS NULL)
			AND (LML.IsDeleted = 0 OR LML.IsDeleted IS NULL)
			AND (LML.RequirementsAreMet = 0 OR LML.RequirementsAreMet IS NULL)
			AND LMR.RequirementTypeID = 3 --Tech Requirement Type
			AND LMR.IsActive = 1
			AND LMR.IsDeleted = 0

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_RequirementGetIncompleteTechLicensing TO PUBLIC
GO

/** EXEC dbo.custLM_RequirementGetIncompleteTechLicensing */