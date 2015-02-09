USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_PoliticalStateGetCreditsRanByStateName')
	BEGIN
		PRINT 'Dropping Procedure custMC_PoliticalStateGetCreditsRanByStateName'
		DROP  Procedure  dbo.custMC_PoliticalStateGetCreditsRanByStateName
	END
GO

PRINT 'Creating Procedure custMC_PoliticalStateGetCreditsRanByStateName'
GO
/******************************************************************************
**		File: custMC_PoliticalStateGetCreditsRanByStateName.sql
**		Name: custMC_PoliticalStateGetCreditsRanByStateName
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
CREATE Procedure dbo.custMC_PoliticalStateGetCreditsRanByStateName
(
	@StateName NVARCHAR(100) = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT 
			RUU.FullName
			, Count(*) AS CreditsRan
		FROM 
			MC_PoliticalStates AS MPS WITH (NOLOCK)
			INNER JOIN [dbo].[vwMC_AddressesMsPremise] AS MCA WITH (NOLOCK)
			ON
				(MPS.StateID = MCA.StateID)
			INNER JOIN [dbo].[vwAeCustomersMsPrimary] AS CUST WITH (NOLOCK)
			ON
				(CUST.AccountId = MCA.AccountId)
			INNER JOIN [dbo].[QL_Leads] AS QLD WITH (NOLOCK)
			ON
				(CUST.LeadId = QLD.LeadID)
			INNER JOIN [WISE_HumanResource].dbo.RU_Users AS RUU WITH (NOLOCK)
			ON
				(QLD.SalesRepID = RUU.GPEmployeeID)
		--WHERE 
		--	(MPS.StateName = @StateName)
		GROUP BY 
			RUU.FullName
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMC_PoliticalStateGetCreditsRanByStateName TO PUBLIC
GO

/** EXEC dbo.custMC_PoliticalStateGetCreditsRanByStateName 'UT' */