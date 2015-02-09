USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_RequirementGetIncompleteCompanyLicensing')
	BEGIN
		PRINT 'Dropping Procedure custLM_RequirementGetIncompleteCompanyLicensing'
		DROP  Procedure  dbo.custLM_RequirementGetIncompleteCompanyLicensing
	END
GO

PRINT 'Creating Procedure custLM_RequirementGetIncompleteCompanyLicensing'
GO
/******************************************************************************
**		File: custLM_RequirementGetIncompleteCompanyLicensing.sql
**		Name: custLM_RequirementGetIncompleteCompanyLicensing
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
CREATE Procedure [dbo].[custLM_RequirementGetIncompleteCompanyLicensing]
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		--Missing Company Requirements and incomplete licenses
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
				, LOC.LocationName
				, CASE 
					WHEN LML.LicenseID IS NULL
					THEN 'Requirement Not Met'
					ELSE 'License Incomplete'
					END AS [Description]
		FROM 
			LM_Requirements AS LMR WITH (NOLOCK)
			INNER JOIN LM_Locations AS LOC WITH (NOLOCK)
			ON
				LMR.LocationID = LOC.LocationID
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LML.RequirementID = LMR.RequirementID
		WHERE (LML.IsActive = 1 OR LML.IsActive IS NULL)
			AND (LML.IsDeleted = 0 OR LML.IsDeleted IS NULL)
			AND (LML.RequirementsAreMet = 0 OR LML.RequirementsAreMet IS NULL)
			AND LMR.IsActive = 1
			AND LMR.IsDeleted = 0
			AND LMR.RequirementTypeID = 1--Company Requirement

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_RequirementGetIncompleteCompanyLicensing TO PUBLIC
GO

/** EXEC dbo.custLM_RequirementGetIncompleteCompanyLicensing */