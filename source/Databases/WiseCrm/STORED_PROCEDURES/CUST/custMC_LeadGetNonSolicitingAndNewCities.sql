USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_LeadGetNonSolicitingAndNewCities')
	BEGIN
		PRINT 'Dropping Procedure custMC_LeadGetNonSolicitingAndNewCities'
		DROP  Procedure  dbo.custMC_LeadGetNonSolicitingAndNewCities
	END
GO

PRINT 'Creating Procedure custMC_LeadGetNonSolicitingAndNewCities'
GO
/******************************************************************************
**		File: custMC_LeadGetNonSolicitingAndNewCities.sql
**		Name: custMC_LeadGetNonSolicitingAndNewCities
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
**		Date: 10/06/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	10/06/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMC_LeadGetNonSolicitingAndNewCities
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		--Cities
		SELECT DISTINCT 
			MCA.City + ', ' + MPS.StateAB  AS [Name]
			, CASE 
				WHEN LOC.LocationID IS NULL
				THEN 'New City'
				ELSE 'Illegal Solicitation in City'
			  END AS [Description]
		FROM
			MC_Accounts AS MCL WITH (NOLOCK)
			INNER JOIN [dbo].[vwMC_AddressesMsPremise] AS MCA WITH (NOLOCK)
			ON
				MCL.AccountID = MCA.AccountId
			INNER JOIN MC_PoliticalStates AS MPS WITH (NOLOCK)
			ON	
				MCA.StateID = MPS.StateID
			LEFT OUTER JOIN [NXSE_Licensing].dbo.LM_Locations AS LOC WITH (NOLOCK)
			ON
				MCA.City = LOC.LocationName
		WHERE (LOC.CanSolicit = 0 OR LOC.CanSolicit IS NULL)
				AND (LOC.LocationTypeID = 5 OR LOC.LocationTypeID IS NULL)--City Type
	
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMC_LeadGetNonSolicitingAndNewCities TO PUBLIC
GO

/** EXEC dbo.custMC_LeadGetNonSolicitingAndNewCities */