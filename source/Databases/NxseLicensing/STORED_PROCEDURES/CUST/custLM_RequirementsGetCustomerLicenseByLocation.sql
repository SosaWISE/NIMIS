USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_RequirementsGetCustomerLicenseByLocation')
	BEGIN
		PRINT 'Dropping Procedure custLM_RequirementsGetCustomerLicenseByLocation'
		DROP  Procedure  dbo.custLM_RequirementsGetCustomerLicenseByLocation
	END
GO

PRINT 'Creating Procedure custLM_RequirementsGetCustomerLicenseByLocation'
GO
/******************************************************************************
**		File: custLM_RequirementsGetCustomerLicenseByLocation.sql
**		Name: custLM_RequirementsGetCustomerLicenseByLocation
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
CREATE Procedure [dbo].[custLM_RequirementsGetCustomerLicenseByLocation]
(
	@CountryName NVARCHAR(MAX) = NULL
	, @StateName NVARCHAR(MAX) = NULL
	, @CountyName NVARCHAR(MAX) = NULL
	, @CityName NVARCHAR(MAX) = NULL
	, @TownshipName NVARCHAR(MAX) = NULL
	, @AccountID INT = NULL
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
			, LMR.Fee
			, CAST(COALESCE(LMR.RequiredForFunding, 0) AS BIT) AS RequiredForFunding
		FROM 
			LM_Requirements AS LMR WITH (NOLOCK)
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON	
				LMR.RequirementID = LML.RequirementID
				AND LML.AccountID = @AccountID
				AND LML.IsDeleted = 0
		WHERE 	
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND 
			LMR.LocationID = 2 AND--Global
			LMR.RequirementTypeID = 4 -- Customer Permit

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
			, LMR.Fee
			, CAST(COALESCE(LMR.RequiredForFunding, 0) AS BIT) AS RequiredForFunding
		FROM
			LM_Locations AS Country WITH (NOLOCK)
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				Country.LocationID = LMR.LocationID
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMR.RequirementID = LML.RequirementID
				AND LML.AccountID = @AccountID
				AND LML.IsDeleted = 0
		WHERE 
			Country.LocationName = @CountryName AND
			Country.LocationTypeID = 2 AND -- Country
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND 
			LMR.RequirementTypeID = 4 -- Customer Permit

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
			, LMR.Fee
			, CAST(COALESCE(LMR.RequiredForFunding, 0) AS BIT) AS RequiredForFunding
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
				AND LML.AccountID = @AccountID
				AND LML.IsDeleted = 0
		WHERE	
			Country.LocationName = @CountryName AND
			Country.LocationTypeID = 2 AND -- Country
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND 
			LMR.RequirementTypeID = 4 -- Customer Permit

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
			, LMR.Fee
			, CAST(COALESCE(LMR.RequiredForFunding, 0) AS BIT) AS RequiredForFunding
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
				AND LML.AccountID = @AccountID
				AND LML.IsDeleted = 0

		WHERE 
			Country.LocationName = @CountryName AND
			Country.LocationTypeID = 2 AND -- Country
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND 
			LMR.RequirementTypeID = 4 -- Customer Permit

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
			, LMR.Fee
			, CAST(COALESCE(LMR.RequiredForFunding, 0) AS BIT) AS RequiredForFunding
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
				AND LML.AccountID = @AccountID
				AND LML.IsDeleted = 0
		WHERE 
			Country.LocationName = @CountryName AND
			Country.LocationTypeID = 2 AND -- Country
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND 
			LMR.RequirementTypeID = 4 -- Customer Permit

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
			, LMR.Fee
			, CAST(COALESCE(LMR.RequiredForFunding, 0) AS BIT) AS RequiredForFunding
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
				AND LML.AccountID = @AccountID
				AND LML.IsDeleted = 0
		WHERE 
			Country.LocationName = @CountryName AND
			Country.LocationTypeID = 2 AND -- Country
			LMR.IsActive = 1 AND
			LMR.IsDeleted = 0 AND 
			LMR.RequirementTypeID = 4 -- Customer Permit

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_RequirementsGetCustomerLicenseByLocation TO PUBLIC
GO

/** EXEC dbo.custLM_RequirementsGetCustomerLicenseByLocation */