USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_LeadGetNonSolicitingAndNewStates')
	BEGIN
		PRINT 'Dropping Procedure custMC_LeadGetNonSolicitingAndNewStates'
		DROP  Procedure  dbo.custMC_LeadGetNonSolicitingAndNewStates
	END
GO

PRINT 'Creating Procedure custMC_LeadGetNonSolicitingAndNewStates'
GO
/******************************************************************************
**		File: custMC_LeadGetNonSolicitingAndNewStates.sql
**		Name: custMC_LeadGetNonSolicitingAndNewStates
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
CREATE Procedure dbo.custMC_LeadGetNonSolicitingAndNewStates
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY

		--States
		SELECT DISTINCT 
			MPS.StateName AS [Name]
			, CASE 
				WHEN LOC.LocationID IS NULL
				THEN 'New State'
				ELSE 'Illegal Solicitation in State'
				END AS [Description]
		FROM 
			[dbo].MC_Accounts AS MCL WITH (NOLOCK)
			INNER JOIN dbo.vwMC_AddressesMsPremise AS MCA WITH (NOLOCK)
			ON
				(MCA.AccountId = MCL.AccountID)
			INNER JOIN MC_PoliticalStates AS MPS WITH (NOLOCK)
			ON	
				MCA.StateID = MPS.StateID
			LEFT OUTER JOIN [NXSE_Licensing].dbo.LM_Locations AS LOC WITH (NOLOCK)
			ON
				(MPS.StateName = LOC.LocationName)
		WHERE 
			(LOC.CanSolicit = 0 OR LOC.CanSolicit IS NULL)
			AND (LOC.LocationTypeID = 3 OR LOC.LocationTypeID IS NULL)--State Type
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMC_LeadGetNonSolicitingAndNewStates TO PUBLIC
GO

/** EXEC dbo.custMC_LeadGetNonSolicitingAndNewStates */