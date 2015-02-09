USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_RequirementsGetCustomerPermitLetterData')
	BEGIN
		PRINT 'Dropping Procedure custLM_RequirementsGetCustomerPermitLetterData'
		DROP  Procedure  dbo.custLM_RequirementsGetCustomerPermitLetterData
	END
GO

PRINT 'Creating Procedure custLM_RequirementsGetCustomerPermitLetterData'
GO
/******************************************************************************
**		File: custLM_RequirementsGetCustomerPermitLetterData.sql
**		Name: custLM_RequirementsGetCustomerPermitLetterData
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
CREATE Procedure [dbo].[custLM_RequirementsGetCustomerPermitLetterData]
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT
			1 AS Letter
			, ACCT.AccountID AS Account
			, CUST1.FirstName + ' ' + CUST1.LastName AS Customer
			, RTRIM(ADDR.StreetAddress + ' ' + ADDR.StreetAddress2) AS Address
			, ADDR.City
			, ST.StateAB AS State
			, ADDR.PostalCode AS Zip
			, DATENAME(MM, GETDATE()) + ' ' + CAST(DATEPART(DD, GETDATE()) AS VARCHAR) + ', ' + CAST(DATEPART(YYYY, GETDATE()) AS VARCHAR) AS Date
			, Reqs.LocationName AS Jurisdiction
			, Reqs.Fee
		FROM
			(
				SELECT
					LMR.RequirementID
					, LMR.RequirementName
					, LMR.Fee
					, LOC.LocationName
					, ST.Abbreviation AS StateAB
					, LCTYP.LocationTypeID
					, LCTYP.LocationTypeName
				FROM
					LM_Requirements AS LMR WITH (NOLOCK)
					INNER JOIN LM_Locations AS LOC WITH (NOLOCK)
					ON
						(LOC.LocationID = LMR.LocationID)
					INNER JOIN LM_LocationTypes AS LCTYP WITH (NOLOCK)
					ON
						(LCTYP.LocationTypeID = LOC.LocationTypeID)
					LEFT JOIN LM_Locations AS ST WITH (NOLOCK)
					ON
						(ST.LocationID = LOC.ParentStateID)
				WHERE 
					LMR.IsActive = 1
					AND LMR.IsDeleted = 0
					AND LMR.RequirementTypeID = 4 -- Customer Permit
					AND COALESCE(LMR.RequiredForFunding, 0) = 1
					AND LMR.Fee > 0
			) AS Reqs
			INNER JOIN [WISE_CRM].[dbo].[vwMC_AddressesMsPremise] AS ADDR WITH (NOLOCK)
			ON
				(Reqs.LocationTypeID = 5 AND ADDR.City = Reqs.LocationName)
				OR (Reqs.LocationTypeID = 4 AND Reqs.LocationName = (ADDR.County + ' COUNTY'))
			INNER JOIN [WISE_CRM].dbo.MC_PoliticalStates AS ST WITH (NOLOCK)
			ON
				(ST.StateID = ADDR.StateID)
				AND (ST.StateAB = Reqs.StateAB)
			INNER JOIN [WISE_CRM].dbo.MS_Accounts AS ACCT WITH (NOLOCK)
			ON
				(ACCT.AccountID = ADDR.AccountId)
			INNER JOIN [WISE_CRM].[dbo].MS_AccountSalesInformations AS AST WITH (NOLOCK)
			ON
				(AST.AccountID = ACCT.AccountID)
			INNER JOIN [WISE_CRM].[dbo].[vwAeCustomersMsPrimary] AS CUST1 WITH (NOLOCK)
			ON
				(ACCT.AccountID = CUST1.AccountId)
			LEFT JOIN LM_Licenses AS LIC WITH (NOLOCK)
			ON
				(LIC.RequirementID = Reqs.RequirementID)
				AND (ACCT.AccountID = LIC.AccountID)
			LEFT JOIN [WISE_CRM].dbo.MC_AccountHolds AS HLD WITH (NOLOCK)
			ON
				(HLD.AccountID = ACCT.AccountID)
				AND (HLD.Catg2ID = 39) -- Permit Paperwork Hold
		WHERE
			COALESCE(LIC.RequirementsAreMet, 0) = 1
			AND (AST.InstallDate > '04/15/2009')
			AND (AST.SubmittedToGPDate IS NOT NULL)
			AND
			(
				(HLD.AccountHoldID IS NULL AND DATEDIFF(dd, AST.SubmittedToGPDate, GETDATE()) >= 14)
				OR (HLD.FixedByDate IS NOT NULL AND DATEDIFF(dd, HLD.FixedByDate, GETDATE()) >= 14)
			)

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_RequirementsGetCustomerPermitLetterData TO PUBLIC
GO

/** EXEC dbo.custLM_RequirementsGetCustomerPermitLetterData */