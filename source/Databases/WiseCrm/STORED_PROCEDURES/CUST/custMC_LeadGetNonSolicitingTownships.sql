USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_LeadGetNonSolicitingTownships')
	BEGIN
		PRINT 'Dropping Procedure custMC_LeadGetNonSolicitingTownships'
		DROP  Procedure  dbo.custMC_LeadGetNonSolicitingTownships
	END
GO

PRINT 'Creating Procedure custMC_LeadGetNonSolicitingTownships'
GO
/******************************************************************************
**		File: custMC_LeadGetNonSolicitingTownships.sql
**		Name: custMC_LeadGetNonSolicitingTownships
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
CREATE Procedure dbo.custMC_LeadGetNonSolicitingTownships
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY
	
		--Township (illegal solicitation only)
		SELECT DISTINCT 
			MCA.City + ', ' + MPS.StateAB  AS [Name]
			,'Illegal Solicitation in Township' AS [Description]
		FROM
			[dbo].[MC_Accounts] AS MCL WITH (NOLOCK)
			INNER JOIN [dbo].[vwMC_AddressesMsPremise] AS MCA WITH (NOLOCK)
			ON
				(MCA.AccountId = MCL.AccountID)
			INNER JOIN MC_PoliticalStates AS MPS WITH (NOLOCK)
			ON	
				(MCA.StateID = MPS.StateID)
			INNER JOIN [NXSE_Licensing].dbo.LM_Locations AS LOC WITH (NOLOCK)
			ON
				(MCA.City = LOC.LocationName)
		WHERE 
			(LOC.CanSolicit = 0)
			AND (LOC.LocationTypeID = 6)--Township Type
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMC_LeadGetNonSolicitingTownships TO PUBLIC
GO

/** EXEC dbo.custMC_LeadGetNonSolicitingTownships */