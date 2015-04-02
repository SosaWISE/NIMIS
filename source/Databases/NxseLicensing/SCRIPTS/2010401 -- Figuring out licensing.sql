USE [NXSE_Licensing]
GO

	/** Arguments */
	DECLARE @CountryName NVARCHAR(MAX) = 'UNITED STATES OF AMERICA'
	, @StateName NVARCHAR(MAX) = 'VIRGINIA'
	, @CountyName NVARCHAR(MAX) = 'PORTSMOUTH CITY COUNTY'
	, @CityName NVARCHAR(MAX) = 'PORTSMOUTH'
	, @TownshipName NVARCHAR(MAX) = 'Andres Township'
	, @GPEmployeeID NVARCHAR(10) = 'SOSAA001';

	/** DECLARATIONS */
	DECLARE @SalesRepReqTypeID INT = 2
		, @NexsenseLLCLocationID INT = 2
		, @CountryLocationTypeID INT = 2
		, @StateLocationTypeID INT = 3
		, @CountyLocationTypeID INT = 4
		, @CityLocationTypeID INT = 5
		, @TownshipLocationTypeID INT = 6;

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
