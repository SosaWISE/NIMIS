USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_RequirementsGetRepLicenseByCityName')
	BEGIN
		PRINT 'Dropping Procedure custLM_RequirementsGetRepLicenseByCityName'
		DROP  Procedure  dbo.custLM_RequirementsGetRepLicenseByCityName
	END
GO

PRINT 'Creating Procedure custLM_RequirementsGetRepLicenseByCityName'
GO
/******************************************************************************
**		File: custLM_RequirementsGetRepLicenseByCityName.sql
**		Name: custLM_RequirementsGetRepLicenseByCityName
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
CREATE Procedure dbo.custLM_RequirementsGetRepLicenseByCityName
(
	@CityName NVARCHAR(MAX) = NULL
	, @GPEmployeeID NVARCHAR(10) = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
	
		SELECT
			LMR.RequirementID
			, LMR.RequirementName
			, LMR.CallCenterMessage
			, CASE
				WHEN LML.LicenseID IS NULL
				THEN 'Missing License'

				WHEN LML.RequirementsAreMet = 0
				THEN 'License Incomplete'

				WHEN GETDATE() < LML.IssueDate
				THEN 'License not active yet'

				WHEN GETDATE() > LML.ExpirationDate
				THEN 'License Expired'

				END AS [Status]
			, CASE 
				WHEN LML.LicenseID IS NULL
				THEN 0
				ELSE LML.LicenseID
				END AS LicenseID
		FROM 
			LM_Requirements AS LMR WITH (NOLOCK)
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON	
				LMR.RequirementID = LML.RequirementID
				AND LML.GPEmployeeID = @GPEmployeeID
			INNER JOIN vwAllRequirementsPerLocation AS ARPL WITH (NOLOCK)
			ON	
				LMR.RequirementID = ARPL.RequirementID
				AND ARPL.RequirementTypeID = 2 -- Rep Requirement
				AND ARPL.LocationName = @CityName
				AND (ARPL.LocationTypeID = 5 OR ARPL.LocationTypeID = 6)
		WHERE
			(LML.LicenseID IS NULL OR
			LML.RequirementsAreMet = 0 OR
			GETDATE() < LML.IssueDate OR
			GETDATE() > LML.ExpirationDate) AND
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 

		UNION 

		--Global requirements
		SELECT
			LMR.RequirementID
			, LMR.RequirementName
			, LMR.CallCenterMessage
			, CASE
				WHEN LML.LicenseID IS NULL
				THEN 'Missing License'

				WHEN LML.RequirementsAreMet = 0
				THEN 'License Incomplete'

				WHEN GETDATE() < LML.IssueDate
				THEN 'License not active yet'

				WHEN GETDATE() > LML.ExpirationDate
				THEN 'License Expired'

				END AS [Status]
			, CASE 
				WHEN LML.LicenseID IS NULL
				THEN 0
				ELSE LML.LicenseID
				END AS LicenseID
		FROM
			LM_Requirements AS LMR WITH (NOLOCK)
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON	
				LMR.RequirementID = LML.RequirementID
				AND LML.GPEmployeeID = @GPEmployeeID
		WHERE
			(LML.LicenseID IS NULL OR
			LML.RequirementsAreMet = 0 OR
			GETDATE() < LML.IssueDate OR
			GETDATE() > LML.ExpirationDate) AND
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND
			LMR.RequirementTypeID = 2 --Rep Requirement
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_RequirementsGetRepLicenseByCityName TO PUBLIC
GO

/** EXEC dbo.custLM_RequirementsGetRepLicenseByCityName */