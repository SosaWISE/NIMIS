USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_RequirementsGetRepLicenseByLocation')
	BEGIN
		PRINT 'Dropping Procedure custLM_RequirementsGetRepLicenseByLocation'
		DROP  Procedure  dbo.custLM_RequirementsGetRepLicenseByLocation
	END
GO

PRINT 'Creating Procedure custLM_RequirementsGetRepLicenseByLocation'
GO
/******************************************************************************
**		File: custLM_RequirementsGetRepLicenseByLocation.sql
**		Name: custLM_RequirementsGetRepLicenseByLocation
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
** EXEC dbo.custLM_RequirementsGetRepLicenseByLocation 'UNITED STATES OF AMERICA', 'VIRGINIA', 'PORTSMOUTH CITY COUNTY', 'PORTSMOUTH', NULL, 'SOSAA001'
*******************************************************************************/
CREATE Procedure dbo.custLM_RequirementsGetRepLicenseByLocation
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
	DECLARE @SalesRepReqTypeID INT = 2
		, @NexsenseLLCLocationID INT = 2
		, @CountryLocationTypeID INT = 2
		, @StateLocationTypeID INT = 3
		, @CountyLocationTypeID INT = 4
		, @CityLocationTypeID INT = 5
		, @TownshipLocationTypeID INT = 6;
	
	BEGIN TRY

		--NEXSENSE requirement
		SELECT
			LMR.RequirementID
			, LMR.RequirementTypeName
			, LMR.LocationTypeName
			, LMR.RequirementName
			, LMR.LockID
			, LMR.[LockTypeName]
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
				WHEN LML.LicenseID IS NULL THEN 0
				ELSE LML.LicenseID
			  END AS LicenseID
		FROM 
			vwLM_Requirements AS LMR WITH (NOLOCK)
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON	
				(LMR.RequirementID = LML.RequirementID)
				AND (LML.IsActive = 1)
				AND (LML.IsDeleted = 0)
				AND (LMR.IsActive = 1)
				AND (LMR.IsDeleted = 0)
		WHERE
				(LMR.LocationID = @NexsenseLLCLocationID)
				AND (LMR.RequirementTypeID = @SalesRepReqTypeID)
				AND (LML.GPEmployeeID = @GPEmployeeID)

		UNION

		--Countries
		SELECT 
			LMR.RequirementID
			, LMR.RequirementTypeName
			, LMR.LocationTypeName
			, LMR.RequirementName
			, LMR.LockID
			, LMR.[LockTypeName]
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
				WHEN LML.LicenseID IS NULL THEN 0
				ELSE LML.LicenseID
			  END AS LicenseID
		FROM
			LM_Locations AS Country WITH (NOLOCK)
			INNER JOIN vwLM_Requirements AS LMR WITH (NOLOCK)
			ON
				(Country.LocationID = LMR.LocationID)
				AND (LMR.IsActive = 1)
				AND (LMR.IsDeleted = 0)
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				(LMR.RequirementID = LML.RequirementID)
				AND (LML.IsActive = 1)
				AND (LML.IsDeleted = 0)
		WHERE
			(Country.LocationName = @CountryName)
			AND (LML.GPEmployeeID = @GPEmployeeID)
			AND (Country.LocationTypeID = @CountryLocationTypeID) -- Country
			AND (LMR.RequirementTypeID = @SalesRepReqTypeID) -- Rep Requirement

		UNION

		--State
		SELECT 
			LMR.RequirementID
			, LMR.RequirementTypeName
			, LMR.LocationTypeName
			, LMR.RequirementName
			, LMR.LockID
			, LMR.[LockTypeName]
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
				WHEN LML.LicenseID IS NULL THEN 0
				ELSE LML.LicenseID
			  END AS LicenseID
		FROM 
			LM_Locations AS Country WITH (NOLOCK)
			INNER JOIN LM_Locations AS [State] WITH (NOLOCK)
			ON
				(Country.LocationID = [State].ParentLocationID)
				AND ([State].LocationName = @StateName)
				AND ([State].LocationTypeID = @StateLocationTypeID) -- State
			INNER JOIN vwLM_Requirements AS LMR WITH (NOLOCK)
			ON
				([State].LocationID = LMR.LocationID)
				AND (LMR.IsActive = 1)
				AND (LMR.IsDeleted = 0)
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				(LMR.RequirementID = LML.RequirementID)
				AND (LML.GPEmployeeID = @GPEmployeeID)
				AND (LML.IsActive = 1)
				AND (LML.IsDeleted = 0)
		WHERE
			(Country.LocationName = @CountryName)
			AND (Country.LocationTypeID = @CountryLocationTypeID) -- Country
			AND (LMR.RequirementTypeID = @SalesRepReqTypeID) -- Rep Requirement

		UNION

		--County
		SELECT
			LMR.RequirementID
			, LMR.RequirementTypeName
			, LMR.LocationTypeName
			, LMR.RequirementName
			, LMR.LockID
			, LMR.[LockTypeName]
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
				WHEN LML.LicenseID IS NULL THEN 0
				ELSE LML.LicenseID
			  END AS LicenseID
		FROM 
			LM_Locations AS Country WITH (NOLOCK)
			INNER JOIN LM_Locations AS [State] WITH (NOLOCK)
			ON
				(Country.LocationID = [State].ParentLocationID)
				AND (Country.LocationTypeID = @CountryLocationTypeID) -- Country
				AND (Country.LocationName = @CountryName)
				AND ([State].LocationName = @StateName)
				AND ([State].LocationTypeID = @StateLocationTypeID) -- State
			INNER JOIN LM_Locations AS County WITH (NOLOCK)
			ON
				([State].LocationID = County.ParentLocationID)
				AND (County.LocationName = @CountyName)
				AND (County.LocationTypeID = @CountyLocationTypeID) -- County
			INNER JOIN vwLM_Requirements AS LMR WITH (NOLOCK)
			ON
				(County.LocationID = LMR.LocationID)
				AND (LMR.RequirementTypeID = @SalesRepReqTypeID) -- Rep Requirement
				AND (LMR.IsActive = 1)
				AND (LMR.IsDeleted = 0)
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				(LMR.RequirementID = LML.RequirementID)
				AND (LML.GPEmployeeID = @GPEmployeeID)
				AND (LML.IsActive = 1)
				AND (LML.IsDeleted = 0)

		UNION

		--City
		SELECT
			LMR.RequirementID
			, LMR.RequirementTypeName
			, LMR.LocationTypeName
			, LMR.RequirementName
			, LMR.LockID
			, LMR.[LockTypeName]
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
				WHEN LML.LicenseID IS NULL THEN 0
				ELSE LML.LicenseID
			  END AS LicenseID
		FROM 
			LM_Locations AS Country WITH (NOLOCK)
			INNER JOIN LM_Locations AS [State] WITH (NOLOCK)
			ON
				(Country.LocationID = [State].ParentLocationID)
				AND ([State].LocationName = @StateName)
				AND ([State].LocationTypeID = @StateLocationTypeID) -- State
			INNER JOIN LM_Locations AS County WITH (NOLOCK)
			ON
				([State].LocationID = County.ParentLocationID)
				AND (County.LocationName = @CountyName)
				AND (County.LocationTypeID = @CountyLocationTypeID) -- County
			INNER JOIN LM_Locations AS City WITH (NOLOCK)
			ON
				(County.LocationID = City.ParentLocationID)
				AND (City.LocationName = @CityName)
				AND (City.LocationTypeID = @CityLocationTypeID) -- City
			INNER JOIN vwLM_Requirements AS LMR WITH (NOLOCK)
			ON
				(City.LocationID = LMR.LocationID)
				AND (LMR.IsActive = 1)
				AND (LMR.IsDeleted = 0)
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				(LMR.RequirementID = LML.RequirementID)
				AND (LML.GPEmployeeID = @GPEmployeeID)
				AND (LML.IsActive = 1)
				AND (LML.IsDeleted = 0)
		WHERE 
			(Country.LocationName = @CountryName)
			AND (Country.LocationTypeID = @CountryLocationTypeID) -- Country
			AND (LMR.RequirementTypeID = @SalesRepReqTypeID) -- Rep Requirement

		UNION

		--Township
		SELECT
			LMR.RequirementID
			, LMR.RequirementTypeName
			, LMR.LocationTypeName
			, LMR.RequirementName
			, LMR.LockID
			, LMR.[LockTypeName]
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
				WHEN LML.LicenseID IS NULL THEN 0
				ELSE LML.LicenseID
			  END AS LicenseID
		FROM
			LM_Locations AS Country WITH (NOLOCK)
			INNER JOIN LM_Locations AS [State] WITH (NOLOCK)
			ON
				Country.LocationID = [State].ParentLocationID
				AND [State].LocationName = @StateName
				AND [State].LocationTypeID = @StateLocationTypeID -- State
			INNER JOIN LM_Locations AS County WITH (NOLOCK)
			ON
				[State].LocationID = County.ParentLocationID
				AND County.LocationName = @CountyName
				AND County.LocationTypeID = @CountyLocationTypeID -- County
			INNER JOIN LM_Locations AS City WITH (NOLOCK)
			ON
				County.LocationID = City.ParentLocationID
				AND City.LocationName = @CityName
				AND City.LocationTypeID = @CityLocationTypeID -- City
			INNER JOIN LM_Locations AS Township WITH (NOLOCK)
			ON
				City.LocationID = Township.ParentLocationID
				AND Township.LocationName = @TownshipName
				AND Township.LocationTypeID = @TownshipLocationTypeID -- Township
			INNER JOIN vwLM_Requirements AS LMR WITH (NOLOCK)
			ON
				Township.LocationID = LMR.LocationID
				AND LMR.IsActive = 1
				AND LMR.IsDeleted = 0 
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMR.RequirementID = LML.RequirementID
				AND LML.GPEmployeeID = @GPEmployeeID
				AND LML.IsActive = 1
				AND LML.IsDeleted = 0
		WHERE 
			(Country.LocationName = @CountryName)
			AND (Country.LocationTypeID = @CountryLocationTypeID) -- Country
			AND (LMR.RequirementTypeID = @SalesRepReqTypeID) -- Rep Requirement
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_RequirementsGetRepLicenseByLocation TO PUBLIC
GO

/** EXEC dbo.custLM_RequirementsGetRepLicenseByLocation */