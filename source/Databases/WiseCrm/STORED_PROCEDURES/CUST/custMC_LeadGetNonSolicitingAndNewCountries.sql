USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_LeadGetNonSolicitingAndNewCountries')
	BEGIN
		PRINT 'Dropping Procedure custMC_LeadGetNonSolicitingAndNewCountries'
		DROP  Procedure  dbo.custMC_LeadGetNonSolicitingAndNewCountries
	END
GO

PRINT 'Creating Procedure custMC_LeadGetNonSolicitingAndNewCountries'
GO
/******************************************************************************
**		File: custMC_LeadGetNonSolicitingAndNewCountries.sql
**		Name: custMC_LeadGetNonSolicitingAndNewCountries
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
CREATE Procedure dbo.custMC_LeadGetNonSolicitingAndNewCountries
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		--Countries
		SELECT DISTINCT 
			MPC.CountryName AS [Name]
			, CASE 
				WHEN LOC.LocationID IS NULL
				THEN 'New Country'
				ELSE 'Illegal Solicitation in Country'
			  END AS [Description]
		FROM 
			[dbo].MC_Accounts AS MCL WITH (NOLOCK)
			INNER JOIN [dbo].vwMC_AddressesMsPremise AS MCA WITH (NOLOCK)
			ON
				(MCL.AccountID = MCA.AccountId)
			INNER JOIN MC_PoliticalCountrys AS MPC WITH (NOLOCK)
			ON	
				(MCA.CountryID = MPC.CountryID)
			LEFT OUTER JOIN [NXSE_Licensing].dbo.LM_Locations AS LOC WITH (NOLOCK)
			ON
				(MPC.CountryName = LOC.LocationName)
		WHERE 
			(LOC.CanSolicit = 0 OR LOC.CanSolicit IS NULL)
			AND (LOC.LocationTypeID = 2 OR LOC.LocationTypeID IS NULL)--Country Type
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMC_LeadGetNonSolicitingAndNewCountries TO PUBLIC
GO

/** EXEC dbo.custMC_LeadGetNonSolicitingAndNewCountries */