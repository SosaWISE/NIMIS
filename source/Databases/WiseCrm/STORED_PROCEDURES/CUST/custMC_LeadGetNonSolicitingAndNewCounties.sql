USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_LeadGetNonSolicitingAndNewCounties')
	BEGIN
		PRINT 'Dropping Procedure custMC_LeadGetNonSolicitingAndNewCounties'
		DROP  Procedure  dbo.custMC_LeadGetNonSolicitingAndNewCounties
	END
GO

PRINT 'Creating Procedure custMC_LeadGetNonSolicitingAndNewCounties'
GO
/******************************************************************************
**		File: custMC_LeadGetNonSolicitingAndNewCounties.sql
**		Name: custMC_LeadGetNonSolicitingAndNewCounties
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
CREATE Procedure dbo.custMC_LeadGetNonSolicitingAndNewCounties
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY

		--Counties
		SELECT 
			DISTINCT MCA.County + ' (' + MPS.StateAB + ')' AS [Name]
			, CASE 
				WHEN LOC.LocationID IS NULL
				THEN 'New County'
				ELSE 'Illegal Solicitation in County'
			  END AS [Description]
		FROM 
			MC_Accounts AS MCL WITH (NOLOCK)
			INNER JOIN [dbo].[vwMC_AddressesMsPremise] AS MCA WITH (NOLOCK)
			ON
				(MCL.AccountID = MCA.AccountId)
			INNER JOIN MC_PoliticalStates AS MPS WITH (NOLOCK)
			ON	
				(MCA.StateID = MPS.StateID)
			LEFT OUTER JOIN [NXSE_Licensing].dbo.LM_Locations AS LOC WITH (NOLOCK)
			ON
				(MCA.County = LOC.LocationName)
				OR (MCA.County + ' County' = LOC.LocationName)
		WHERE
			(LOC.CanSolicit = 0 OR LOC.CanSolicit IS NULL)
			AND (LOC.LocationTypeID = 4 OR LOC.LocationTypeID IS NULL)--County Type
			AND (MCA.County IS NOT NULL)
			AND (MCA.County NOT IN ('USA', '', ' '))--weird values that are not counties
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMC_LeadGetNonSolicitingAndNewCounties TO PUBLIC
GO

/** EXEC dbo.custMC_LeadGetNonSolicitingAndNewCounties */