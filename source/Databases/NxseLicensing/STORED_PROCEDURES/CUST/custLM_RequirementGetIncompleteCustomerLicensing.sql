USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_RequirementGetIncompleteCustomerLicensing')
	BEGIN
		PRINT 'Dropping Procedure custLM_RequirementGetIncompleteCustomerLicensing'
		DROP  Procedure  dbo.custLM_RequirementGetIncompleteCustomerLicensing
	END
GO

PRINT 'Creating Procedure custLM_RequirementGetIncompleteCustomerLicensing'
GO
/******************************************************************************
**		File: custLM_RequirementGetIncompleteCustomerLicensing.sql
**		Name: custLM_RequirementGetIncompleteCustomerLicensing
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
CREATE Procedure [dbo].[custLM_RequirementGetIncompleteCustomerLicensing]
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY


		--Missing Customer Requirements and Incomplete Licenses
		SELECT 
		LMR.RequirementID
		, LMR.RequirementName
		, CASE 
			WHEN LML.LicenseID IS NULL
			THEN 0
			ELSE LML.LicenseID
		  END AS LicenseID
		, LML.IssueDate
		, LML.ExpirationDate
		, LML.LicenseNumber
		, ARPL.LocationName + ', ' + MPS.StateName AS LocationName
		, MSA.AccountID
		, CASE 
			WHEN LML.LicenseID IS NULL
			THEN 'Requirement Not Met'
			ELSE 'License Incomplete'
		  END AS [Description]
		FROM
			[WISE_CRM].[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
			INNER JOIN [WISE_CRM].[dbo].[vwMC_AddressesMsPremise] AS MCA WITH (NOLOCK)
			ON
				(MCA.AccountId = MSA.AccountID)
			INNER JOIN vwAllRequirementsPerLocation AS ARPL WITH (NOLOCK)
			ON
				MCA.City = ARPL.LocationName
				AND ARPL.LocationTypeID = 5 -- City Type
			INNER JOIN [WISE_CRM].dbo.MC_PoliticalStates AS MPS WITH (NOLOCK)
			ON
				MCA.StateID = MPS.StateID
			INNER JOIN vwRequirementLocations AS RL WITH (NOLOCK)
			ON
				ARPL.RequirementID = RL.RequirementID
				AND MPS.StateName = RL.State
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				RL.RequirementID = LMR.RequirementID
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMR.RequirementID = LML.RequirementID
				AND MSA.AccountID = LML.AccountID
		WHERE 
			(LML.IsActive = 1 OR LML.IsActive IS NULL)
			AND (LML.IsDeleted = 0 OR LML.IsDeleted IS NULL)
			AND (LML.RequirementsAreMet = 0 OR LML.RequirementsAreMet IS NULL)
			AND LMR.RequirementTypeID = 4 --Customer Requirement Type
			AND LMR.IsActive = 1
			AND LMR.IsDeleted = 0

		UNION 

		-- Global Customer Requirements
		SELECT
			LMR.RequirementID
			, LMR.RequirementName
			, LML.LicenseID
			, LML.IssueDate
			, LML.ExpirationDate
			, LML.LicenseNumber
			, LOC.LocationName
			, LML.AccountID
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
			INNER JOIN LM_Locations AS LOC WITH (NOLOCK)
			ON
				LMR.LocationID = LOC.LocationID
		WHERE (LML.IsActive = 1 OR LML.IsActive IS NULL)
			AND (LML.IsDeleted = 0 OR LML.IsDeleted IS NULL)
			AND (LML.RequirementsAreMet = 0 OR LML.RequirementsAreMet IS NULL)
			AND LMR.RequirementTypeID = 4 --Customer Requirement Type
			AND LMR.IsActive = 1
			AND LMR.IsDeleted = 0

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_RequirementGetIncompleteCustomerLicensing TO PUBLIC
GO

/** EXEC dbo.custLM_RequirementGetIncompleteCustomerLicensing */