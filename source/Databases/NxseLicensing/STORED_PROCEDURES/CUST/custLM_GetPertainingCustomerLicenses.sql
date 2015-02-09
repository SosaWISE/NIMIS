USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_GetPertainingCustomerLicenses')
	BEGIN
		PRINT 'Dropping Procedure custLM_GetPertainingCustomerLicenses'
		DROP  Procedure  dbo.custLM_GetPertainingCustomerLicenses
	END
GO

PRINT 'Creating Procedure custLM_GetPertainingCustomerLicenses'
GO
/******************************************************************************
**		File: custLM_GetPertainingCustomerLicenses.sql
**		Name: custLM_GetPertainingCustomerLicenses
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
CREATE Procedure [dbo].[custLM_GetPertainingCustomerLicenses]
(
	@CountryName NVARCHAR(MAX) = NULL
	, @StateName NVARCHAR(MAX) = NULL
	, @CountyName NVARCHAR(MAX) = NULL
	, @CityName NVARCHAR(MAX) = NULL
	, @TownshipName NVARCHAR(MAX) = NULL
	, @GPRepID NVARCHAR(10) = NULL
	, @GPTechID NVARCHAR(10) = NULL
	, @AccountID INT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		--Temp Tables
		DECLARE  @LMCompanyLicenses TABLE(RequirementID INT, RequirementName NVARCHAR(MAX), CallCenterMessage NVARCHAR(MAX), [Status] NVARCHAR(50), LicenseID INT) 
		DECLARE  @LMRepLicenses TABLE(RequirementID INT, RequirementName NVARCHAR(MAX), CallCenterMessage NVARCHAR(MAX), [Status] NVARCHAR(50), LicenseID INT) 
		DECLARE  @LMTechLicenses TABLE(RequirementID INT, RequirementName NVARCHAR(MAX), CallCenterMessage NVARCHAR(MAX), [Status] NVARCHAR(50), LicenseID INT) 
		DECLARE  @LMCustomerLicenses TABLE(RequirementID INT, RequirementName NVARCHAR(MAX), CallCenterMessage NVARCHAR(MAX), [Status] NVARCHAR(50), LicenseID INT, Fee MONEY, RequiredForFunding BIT) 

		--Fill Tables
		INSERT INTO @LMCompanyLicenses
		EXEC custLM_RequirementsGetCompanyLicenseByLocation @CountryName, @StateName, @CountyName, @CityName, @TownshipName

		INSERT INTO @LMRepLicenses
		EXEC custLM_RequirementsGetRepLicenseByLocation @CountryName, @StateName, @CountyName, @CityName, @TownshipName, @GPRepID

		INSERT INTO @LMTechLicenses
		EXEC custLM_RequirementsGetTechLicenseByLocation @CountryName, @StateName, @CountyName, @CityName, @TownshipName, @GPTechID

		INSERT INTO @LMCustomerLicenses
		EXEC custLM_RequirementsGetCustomerLicenseByLocation @CountryName, @StateName, @CountyName, @CityName, @TownshipName, @AccountID

		--Company Licenses
		SELECT 'Nexsense' AS FullName
				, LMR.RequirementName + ' (' + LOC.LocationName + ')' AS RequirementName
				, LRT.RequirementTypeName
				, LML.LicenseNumber
				, LMR.ApplicationDescription
				, LM.[Status]
				, LM.RequirementID
				, LM.LicenseID
				, LML.IssueDate
				, LML.ExpirationDate
		FROM 
			LM_Requirements AS LMR WITH (NOLOCK)
			INNER JOIN @LMCompanyLicenses AS LM
			ON
				LM.RequirementID = LMR.RequirementID
			INNER JOIN LM_RequirementTypes AS LRT WITH (NOLOCK)
			ON
				LRT.RequirementTypeID = LMR.RequirementTypeID
			INNER JOIN LM_Locations AS LOC WITH (NOLOCK)
			ON 
				LMR.LocationID = LOC.LocationID
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LML.LicenseID = LM.LicenseID
		
		UNION

		--Rep Licenses
		SELECT RUU.FullName
				, LMR.RequirementName + ' (' + LOC.LocationName + ')' AS RequirementName
				, LRT.RequirementTypeName
				, LML.LicenseNumber
				, LMR.ApplicationDescription
				, LM.[Status]
				, LM.RequirementID
				, LM.LicenseID
				, LML.IssueDate
				, LML.ExpirationDate
		FROM LM_Requirements AS LMR WITH (NOLOCK)
			INNER JOIN @LMRepLicenses AS LM
			ON
				LM.RequirementID = LMR.RequirementID
			INNER JOIN LM_RequirementTypes AS LRT WITH (NOLOCK)
			ON
				LRT.RequirementTypeID = LMR.RequirementTypeID
			INNER JOIN LM_Locations AS LOC WITH (NOLOCK)
			ON 
				LMR.LocationID = LOC.LocationID
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LML.LicenseID = LM.LicenseID
			INNER JOIN [WISE_HumanResource].dbo.RU_Users AS RUU WITH (NOLOCK)
			ON
				RUU.GPEmployeeID = LML.GPEmployeeID
		
		UNION

		--Tech Licenses
		SELECT RUU.FullName
				, LMR.RequirementName + ' (' + LOC.LocationName + ')' AS RequirementName
				, LRT.RequirementTypeName
				, LML.LicenseNumber
				, LMR.ApplicationDescription
				, LM.[Status]
				, LM.RequirementID
				, LM.LicenseID
				, LML.IssueDate
				, LML.ExpirationDate
		FROM LM_Requirements AS LMR WITH (NOLOCK)
			INNER JOIN @LMTechLicenses AS LM
			ON
				LM.RequirementID = LMR.RequirementID
			INNER JOIN LM_RequirementTypes AS LRT WITH (NOLOCK)
			ON
				LRT.RequirementTypeID = LMR.RequirementTypeID
			INNER JOIN LM_Locations AS LOC WITH (NOLOCK)
			ON 
				LMR.LocationID = LOC.LocationID
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LML.LicenseID = LM.LicenseID
			INNER JOIN [WISE_HumanResource].dbo.RU_Users AS RUU WITH (NOLOCK)
			ON
				RUU.GPEmployeeID = LML.GPEmployeeID	
		
		UNION

		--Customer Licenses
		SELECT CUST1.FirstName + ' ' + CUST1.LastName AS FullName
				, LMR.RequirementName + ' (' + LOC.LocationName + ')' AS RequirementName
				, LRT.RequirementTypeName
				, LML.LicenseNumber
				, LMR.ApplicationDescription
				, LM.[Status]
				, LM.RequirementID
				, LM.LicenseID
				, LML.IssueDate
				, LML.ExpirationDate
		FROM LM_Requirements AS LMR WITH (NOLOCK)
			INNER JOIN @LMCustomerLicenses LM
			ON
				LM.RequirementID = LMR.RequirementID
			INNER JOIN LM_RequirementTypes AS LRT WITH (NOLOCK)
			ON
				LRT.RequirementTypeID = LMR.RequirementTypeID
			INNER JOIN LM_Locations AS LOC WITH (NOLOCK)
			ON 
				LMR.LocationID = LOC.LocationID
			LEFT OUTER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LML.LicenseID = LM.LicenseID
			INNER JOIN [WISE_CRM].dbo.MS_Accounts AS MSA WITH (NOLOCK)
			ON
				MSA.AccountID = @AccountID
			INNER JOIN [WISE_CRM].[dbo].[vwAeCustomersMsPrimary] AS CUST1 WITH (NOLOCK)
			ON
				(MSA.AccountID = CUST1.AccountId)


	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_GetPertainingCustomerLicenses TO PUBLIC
GO

/** EXEC dbo.custLM_GetPertainingCustomerLicenses */