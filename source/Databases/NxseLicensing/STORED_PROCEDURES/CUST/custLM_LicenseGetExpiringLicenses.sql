USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_LicenseGetExpiringLicenses')
	BEGIN
		PRINT 'Dropping Procedure custLM_LicenseGetExpiringLicenses'
		DROP  Procedure  dbo.custLM_LicenseGetExpiringLicenses
	END
GO

PRINT 'Creating Procedure custLM_LicenseGetExpiringLicenses'
GO
/******************************************************************************
**		File: custLM_LicenseGetExpiringLicenses.sql
**		Name: custLM_LicenseGetExpiringLicenses
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
CREATE Procedure [dbo].[custLM_LicenseGetExpiringLicenses]
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		--Company Licenses
		SELECT LML.LicenseID
				, LMR.RequirementName
				, LML.IssueDate
				, LML.ExpirationDate
				, LML.LicenseNumber
				, LOC.LocationName
				, CASE 
					WHEN LML.ExpirationDate <= GETDATE()
					THEN 'Expired'
					ELSE 'Will Expire Soon'
					END AS [Status]
		FROM LM_Licenses LML
			INNER JOIN LM_Requirements LMR WITH (NOLOCK)
			ON
				LML.RequirementID = LMR.RequirementID 
			INNER JOIN LM_Locations LOC WITH (NOLOCK)
			ON
				LMR.LocationID = LOC.LocationID
		WHERE LML.IsActive = 1
				AND LML.IsDeleted = 0
				AND LML.ExpirationDate <= DATEADD(MONTH, 3, GETDATE())--Will expire in 3 months
				AND LMR.RequirementTypeID = 1 --Company Req type
				AND LMR.IsActive = 1
				AND LMR.IsDeleted = 0

		--Rep Licenses
		SELECT LML.LicenseID
				, LMR.RequirementName
				, LML.IssueDate
				, LML.ExpirationDate
				, LML.LicenseNumber
				, LOC.LocationName
				, RUU.FullName
				, CASE 
					WHEN LML.ExpirationDate <= GETDATE()
					THEN 'Expired'
					ELSE 'Will Expire Soon'
					END AS [Status]
		FROM LM_Licenses LML
			INNER JOIN LM_Requirements LMR WITH (NOLOCK)
			ON
				LML.RequirementID = LMR.RequirementID 
			INNER JOIN LM_Locations LOC WITH (NOLOCK)
			ON
				LMR.LocationID = LOC.LocationID
			INNER JOIN [WISE_HumanResource].dbo.RU_Users RUU WITH (NOLOCK)
			ON
				LML.GPEmployeeID = RUU.GPEmployeeID
		WHERE LML.IsActive = 1
				AND LML.IsDeleted = 0
				AND LML.ExpirationDate <= DATEADD(MONTH, 3, GETDATE())--Will expire in 3 months
				AND LMR.RequirementTypeID = 2 --Sales Rep Req type
				AND LMR.IsActive = 1
				AND LMR.IsDeleted = 0

		--Tech Licenses
		SELECT LML.LicenseID
				, LMR.RequirementName
				, LML.IssueDate
				, LML.ExpirationDate
				, LML.LicenseNumber
				, LOC.LocationName
				, RUU.FullName
				, CASE 
					WHEN LML.ExpirationDate <= GETDATE()
					THEN 'Expired'
					ELSE 'Will Expire Soon'
					END AS [Status]
		FROM LM_Licenses LML
			INNER JOIN LM_Requirements LMR WITH (NOLOCK)
			ON
				LML.RequirementID = LMR.RequirementID 
			INNER JOIN LM_Locations LOC WITH (NOLOCK)
			ON
				LMR.LocationID = LOC.LocationID
			INNER JOIN [WISE_HumanResource].dbo.RU_Users RUU WITH (NOLOCK)
			ON
				LML.GPEmployeeID = RUU.GPEmployeeID
		WHERE LML.IsActive = 1
				AND LML.IsDeleted = 0
				AND LML.ExpirationDate <= DATEADD(MONTH, 3, GETDATE())--Will expire in 3 months
				AND LMR.RequirementTypeID = 3 --Tech Req type
				AND LMR.IsActive = 1
				AND LMR.IsDeleted = 0

		--Customer Licenses
		SELECT LML.LicenseID
				, LMR.RequirementName
				, LML.IssueDate
				, LML.ExpirationDate
				, LML.LicenseNumber
				, LOC.LocationName
				, LML.AccountID
				, CASE 
					WHEN LML.ExpirationDate <= GETDATE()
					THEN 'Expired'
					ELSE 'Will Expire Soon'
					END AS [Status]
		FROM LM_Licenses LML
			INNER JOIN LM_Requirements LMR WITH (NOLOCK)
			ON
				LML.RequirementID = LMR.RequirementID 
			INNER JOIN LM_Locations LOC WITH (NOLOCK)
			ON
				LMR.LocationID = LOC.LocationID
		WHERE LML.IsActive = 1
				AND LML.IsDeleted = 0
				AND LML.ExpirationDate <= DATEADD(MONTH, 3, GETDATE())--Will expire in 3 months
				AND LMR.RequirementTypeID = 4 --Customer Req type
				AND LMR.IsActive = 1
				AND LMR.IsDeleted = 0

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_LicenseGetExpiringLicenses TO PUBLIC
GO

/** EXEC dbo.custLM_LicenseGetExpiringLicenses */