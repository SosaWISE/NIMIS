USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_RequirementsGetTechLicenseByLocation')
	BEGIN
		PRINT 'Dropping Procedure custLM_RequirementsGetTechLicenseByLocation'
		DROP  Procedure  dbo.custLM_RequirementsGetTechLicenseByLocation
	END
GO

PRINT 'Creating Procedure custLM_RequirementsGetTechLicenseByLocation'
GO
/******************************************************************************
**		File: custLM_RequirementsGetTechLicenseByLocation.sql
**		Name: custLM_RequirementsGetTechLicenseByLocation
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
CREATE Procedure dbo.custLM_RequirementsGetTechLicenseByLocation
(
	@CountryName NVARCHAR(MAX) = NULL
	, @StateName NVARCHAR(MAX) = NULL
	, @CountyName NVARCHAR(MAX) = NULL
	, @CityName NVARCHAR(MAX) = NULL
	, @TownshipName NVARCHAR(MAX) = NULL
	, @GPEmployeeID NVARCHAR(10) = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
	
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
			
				ELSE 'Licensing Complete'

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
				AND LML.IsDeleted = 0
		WHERE
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND 
			LMR.LocationID = 2 AND--Global
			LMR.RequirementTypeID = 3 -- Tech Requirement

		UNION

		--Countries
		SELECT
			LMR.RequirementID
			, LMR.RequirementName
			, LMR.CallCenterMessage
			, CASE
				WHEN Country.CanSolicit = 0
				THEN 'No Solicitation'

				WHEN LML.LicenseID IS NULL
				THEN 'Missing License'

				WHEN LML.RequirementsAreMet = 0
				THEN 'License Incomplete'

				WHEN GETDATE() < LML.IssueDate
				THEN 'License not active yet'

				WHEN GETDATE() > LML.ExpirationDate
				THEN 'License Expired'
			
				ELSE 'Licensing Complete'

				END AS [Status]
			, CASE 
				WHEN LML.LicenseID IS NULL
				THEN 0
				ELSE LML.LicenseID
				END AS LicenseID
		FROM
			LM_Locations AS Country WITH (NOLOCK)
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				Country.LocationID = LMR.LocationID
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMR.RequirementID = LML.RequirementID
				AND LML.GPEmployeeID = @GPEmployeeID
				AND LML.IsDeleted = 0
		WHERE
			Country.LocationName = @CountryName AND
			Country.LocationTypeID = 2 AND -- Country
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND 
			LMR.RequirementTypeID = 3 -- Tech Requirement

		UNION

		--State
		SELECT
			LMR.RequirementID
			, LMR.RequirementName
			, LMR.CallCenterMessage
			, CASE
				WHEN [State].CanSolicit = 0
				THEN 'No Solicitation'

				WHEN LML.LicenseID IS NULL
				THEN 'Missing License'

				WHEN LML.RequirementsAreMet = 0
				THEN 'License Incomplete'

				WHEN GETDATE() < LML.IssueDate
				THEN 'License not active yet'

				WHEN GETDATE() > LML.ExpirationDate
				THEN 'License Expired'
			
				ELSE 'Licensing Complete'

				END AS [Status]
			, CASE 
				WHEN LML.LicenseID IS NULL
				THEN 0
				ELSE LML.LicenseID
				END AS LicenseID
		FROM
			LM_Locations AS Country WITH (NOLOCK)
			INNER JOIN LM_Locations AS [State] WITH (NOLOCK)
			ON
				Country.LocationID = [State].ParentLocationID
				AND [State].LocationName = @StateName
				AND [State].LocationTypeID = 3 -- State
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				[State].LocationID = LMR.LocationID
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMR.RequirementID = LML.RequirementID
				AND LML.GPEmployeeID = @GPEmployeeID
				AND LML.IsDeleted = 0
		WHERE	
			Country.LocationName = @CountryName AND
			Country.LocationTypeID = 2 AND -- Country
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND 
			LMR.RequirementTypeID = 3 -- Tech Requirement

		UNION

		--County
		SELECT
			LMR.RequirementID
			, LMR.RequirementName
			, LMR.CallCenterMessage
			, CASE
				WHEN County.CanSolicit = 0
				THEN 'No Solicitation'

				WHEN LML.LicenseID IS NULL
				THEN 'Missing License'

				WHEN LML.RequirementsAreMet = 0
				THEN 'License Incomplete'

				WHEN GETDATE() < LML.IssueDate
				THEN 'License not active yet'

				WHEN GETDATE() > LML.ExpirationDate
				THEN 'License Expired'
			
				ELSE 'Licensing Complete'

				END AS [Status]
			, CASE 
				WHEN LML.LicenseID IS NULL
				THEN 0
				ELSE LML.LicenseID
				END AS LicenseID
		FROM
			LM_Locations AS Country WITH (NOLOCK)
			INNER JOIN LM_Locations AS [State] WITH (NOLOCK)
			ON
				Country.LocationID = [State].ParentLocationID
				AND [State].LocationName = @StateName
				AND [State].LocationTypeID = 3 -- State
			INNER JOIN LM_Locations AS County WITH (NOLOCK)
			ON
				[State].LocationID = County.ParentLocationID
				AND County.LocationName = @CountyName
				AND County.LocationTypeID = 4 -- County
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				County.LocationID = LMR.LocationID
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMR.RequirementID = LML.RequirementID
				AND LML.GPEmployeeID = @GPEmployeeID
				AND LML.IsDeleted = 0
		WHERE
			Country.LocationName = @CountryName AND
			Country.LocationTypeID = 2 AND -- Country
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND 
			LMR.RequirementTypeID = 3 -- Tech Requirement

		UNION

		--City
		SELECT
			LMR.RequirementID
			, LMR.RequirementName
			, LMR.CallCenterMessage
			, CASE
				WHEN City.CanSolicit = 0
				THEN 'No Solicitation'

				WHEN LML.LicenseID IS NULL
				THEN 'Missing License'

				WHEN LML.RequirementsAreMet = 0
				THEN 'License Incomplete'

				WHEN GETDATE() < LML.IssueDate
				THEN 'License not active yet'

				WHEN GETDATE() > LML.ExpirationDate
				THEN 'License Expired'
			
				ELSE 'Licensing Complete'

				END AS [Status]
			, CASE 
				WHEN LML.LicenseID IS NULL
				THEN 0
				ELSE LML.LicenseID
				END AS LicenseID
		FROM
			LM_Locations AS Country WITH (NOLOCK)
			INNER JOIN LM_Locations AS [State] WITH (NOLOCK)
			ON
				Country.LocationID = [State].ParentLocationID
				AND [State].LocationName = @StateName
				AND [State].LocationTypeID = 3 -- State
			INNER JOIN LM_Locations AS County WITH (NOLOCK)
			ON
				[State].LocationID = County.ParentLocationID
				AND County.LocationName = @CountyName
				AND County.LocationTypeID = 4 -- County
			INNER JOIN LM_Locations AS City WITH (NOLOCK)
			ON
				County.LocationID = City.ParentLocationID
				AND City.LocationName = @CityName
				AND City.LocationTypeID = 5 -- City
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				City.LocationID = LMR.LocationID
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMR.RequirementID = LML.RequirementID
				AND LML.GPEmployeeID = @GPEmployeeID
				AND LML.IsDeleted = 0
		WHERE 
			Country.LocationName = @CountryName AND
			Country.LocationTypeID = 2 AND -- Country
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND 
			LMR.RequirementTypeID = 3 -- Tech Requirement

		UNION

		--Township
		SELECT 
			LMR.RequirementID
			, LMR.RequirementName
			, LMR.CallCenterMessage
			, CASE
				WHEN Township.CanSolicit = 0
				THEN 'No Solicitation'

				WHEN LML.LicenseID IS NULL
				THEN 'Missing License'

				WHEN LML.RequirementsAreMet = 0
				THEN 'License Incomplete'

				WHEN GETDATE() < LML.IssueDate
				THEN 'License not active yet'

				WHEN GETDATE() > LML.ExpirationDate
				THEN 'License Expired'
			
				ELSE 'Licensing Complete'

				END AS [Status]
			, CASE 
				WHEN LML.LicenseID IS NULL
				THEN 0
				ELSE LML.LicenseID
				END AS LicenseID
		FROM
			LM_Locations AS Country WITH (NOLOCK)
			INNER JOIN LM_Locations AS [State] WITH (NOLOCK)
			ON
				Country.LocationID = [State].ParentLocationID
				AND [State].LocationName = @StateName
				AND [State].LocationTypeID = 3 -- State
			INNER JOIN LM_Locations AS County WITH (NOLOCK)
			ON
				[State].LocationID = County.ParentLocationID
				AND County.LocationName = @CountyName
				AND County.LocationTypeID = 4 -- County
			INNER JOIN LM_Locations AS City WITH (NOLOCK)
			ON
				County.LocationID = City.ParentLocationID
				AND City.LocationName = @CityName
				AND City.LocationTypeID = 5 -- City
			INNER JOIN LM_Locations AS Township WITH (NOLOCK)
			ON
				City.LocationID = Township.ParentLocationID
				AND Township.LocationName = @TownshipName
				AND Township.LocationTypeID = 6 -- Township
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				Township.LocationID = LMR.LocationID
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMR.RequirementID = LML.RequirementID
				AND LML.GPEmployeeID = @GPEmployeeID
				AND LML.IsDeleted = 0
		WHERE 
			Country.LocationName = @CountryName AND
			Country.LocationTypeID = 2 AND -- Country
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND 
			LMR.RequirementTypeID = 3 -- Tech Requirement

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_RequirementsGetTechLicenseByLocation TO PUBLIC
GO

/** EXEC dbo.custLM_RequirementsGetTechLicenseByLocation */