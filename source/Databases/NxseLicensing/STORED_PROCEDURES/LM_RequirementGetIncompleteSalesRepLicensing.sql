USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'LM_RequirementGetIncompleteSalesRepLicensing')
	BEGIN
		PRINT 'Dropping Procedure LM_RequirementGetIncompleteSalesRepLicensing'
		DROP  Procedure  dbo.LM_RequirementGetIncompleteSalesRepLicensing
	END
GO

PRINT 'Creating Procedure LM_RequirementGetIncompleteSalesRepLicensing'
GO
/******************************************************************************
**		File: LM_RequirementGetIncompleteSalesRepLicensing.sql
**		Name: LM_RequirementGetIncompleteSalesRepLicensing
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
**		Date: 10/15/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	10/15/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.LM_RequirementGetIncompleteSalesRepLicensing
(
	@SeasonID INT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		--Missing Rep Requirements and incomplete licenses
		SELECT 
			LMR.RequirementID
			, LMR.RequirementName
			, LML.LicenseID
			, LML.IssueDate
			, LML.ExpirationDate
			, LML.LicenseNumber
			, ARPL.LocationName
			, RUU.FullName
			, CASE 
				WHEN LML.LicenseID IS NULL
				THEN 'Requirement Not Met'
				ELSE 'License Incomplete'
			  END AS [Description]
		FROM 
			[WISE_HumanResource].[dbo].RU_Users AS RUU WITH (NOLOCK)
			INNER JOIN [WISE_HumanResource].[dbo].RU_Recruits AS RUR WITH (NOLOCK)
			ON
				RUU.UserID = RUR.UserID 
				AND RUR.SeasonID = @SeasonID 
				AND RUR.IsDeleted = 0
				AND RUR.IsActive = 1
			INNER JOIN [WISE_HumanResource].[dbo].RU_Teams AS RUT WITH (NOLOCK)
			ON 
				RUR.TeamID = RUT.TeamID
			INNER JOIN [WISE_HumanResource].[dbo].RU_TeamLocations AS RUL WITH (NOLOCK)
			ON
				RUT.TeamLocationID = RUL.TeamLocationID
			INNER JOIN vwAllRequirementsPerLocation AS ARPL WITH (NOLOCK)
			ON
				RUL.City = ARPL.LocationName
				AND ARPL.LocationTypeID = 5 -- City Type
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				ARPL.RequirementID = LMR.RequirementID
				AND LMR.RequirementTypeID = 2 --Rep Type
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMR.RequirementID = LML.RequirementID
		WHERE
			(LML.IsActive = 1 OR LML.IsActive IS NULL)
			AND (LML.IsDeleted = 0 OR LML.IsDeleted IS NULL)
			AND (LML.RequirementsAreMet = 0 OR LML.RequirementsAreMet IS NULL)

		UNION

		--Global Sales Rep Requirements
		SELECT
			LMR.RequirementID
			, LMR.RequirementName
			, LML.LicenseID
			, LML.IssueDate
			, LML.ExpirationDate
			, LML.LicenseNumber
			, LOC.LocationName
			, RUU.FullName
			, CASE 
				WHEN LML.LicenseID IS NULL
				THEN 'Requirement Not Met'
				ELSE 'License Incomplete'
			  END AS [Description]
		FROM
			LM_Requirements AS LMR WITH (NOLOCK)
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMR.RequirementID = LML.RequirementID
			INNER JOIN [WISE_HumanResource].[dbo].RU_Users AS RUU WITH (NOLOCK)
			ON
				LML.GPEmployeeID = RUU.GPEmployeeID
			INNER JOIN LM_Locations AS LOC WITH (NOLOCK)
			ON
				LMR.LocationID = LOC.LocationID
				AND LOC.LocationTypeID = 1 --Global Type
		WHERE
			(LML.IsActive = 1 OR LML.IsActive IS NULL)
			AND (LML.IsDeleted = 0 OR LML.IsDeleted IS NULL)
			AND (LML.RequirementsAreMet = 0 OR LML.RequirementsAreMet IS NULL)
			AND LMR.RequirementTypeID = 2 --Sales Rep Requirement Type

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.LM_RequirementGetIncompleteSalesRepLicensing TO PUBLIC
GO

/** EXEC dbo.LM_RequirementGetIncompleteSalesRepLicensing */