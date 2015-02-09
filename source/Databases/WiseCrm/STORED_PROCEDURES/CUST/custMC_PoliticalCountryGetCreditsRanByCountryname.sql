USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_PoliticalCountryGetCreditsRanByCountryname')
	BEGIN
		PRINT 'Dropping Procedure custMC_PoliticalCountryGetCreditsRanByCountryname'
		DROP  Procedure  dbo.custMC_PoliticalCountryGetCreditsRanByCountryname
	END
GO

PRINT 'Creating Procedure custMC_PoliticalCountryGetCreditsRanByCountryname'
GO
/******************************************************************************
**		File: custMC_PoliticalCountryGetCreditsRanByCountryname.sql
**		Name: custMC_PoliticalCountryGetCreditsRanByCountryname
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
**		Date: 10/17/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	10/17/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMC_PoliticalCountryGetCreditsRanByCountryname
(
	@CountryName NVARCHAR(100) = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY

		SELECT
			RUU.FullName
			, Count(*) AS CreditsRan
		FROM
			MC_PoliticalCountrys AS MPC WITH (NOLOCK)
			INNER JOIN [dbo].[vwMC_AddressesMsPremise] AS MCA WITH (NOLOCK)
			ON
				(MPC.CountryID = MCA.CountryID)
			INNER JOIN MC_Accounts MSA WITH (NOLOCK)
			ON
				(MSA.AccountID = MCA.AccountId)
			INNER JOIN [dbo].[vwAeCustomersMsPrimary] AS CUST WITH (NOLOCK)
			ON
				(CUST.AccountId = MSA.AccountID)
			INNER JOIN [dbo].[QL_Leads] AS QLD WITH (NOLOCK)
			ON
				(CUST.LeadId = QLD.LeadID)
			INNER JOIN [WISE_HumanResource].dbo.RU_Users AS RUU WITH (NOLOCK)
			ON
				(QLD.SalesRepId = RUU.GPEmployeeID)
		WHERE
			(MPC.CountryName = @CountryName)
		GROUP BY 
			RUU.FullName
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMC_PoliticalCountryGetCreditsRanByCountryname TO PUBLIC
GO

/** EXEC dbo.custMC_PoliticalCountryGetCreditsRanByCountryname */