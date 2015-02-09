USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_PoliticalStateGetByStateAB')
	BEGIN
		PRINT 'Dropping Procedure custMC_PoliticalStateGetByStateAB'
		DROP  Procedure  dbo.custMC_PoliticalStateGetByStateAB
	END
GO

PRINT 'Creating Procedure custMC_PoliticalStateGetByStateAB'
GO
/******************************************************************************
**		File: custMC_PoliticalStateGetByStateAB.sql
**		Name: custMC_PoliticalStateGetByStateAB
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
**		Date: 10/23/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	10/23/2013	Andres Sosa		Created By
**			
*******************************************************************************/
CREATE Procedure dbo.custMC_PoliticalStateGetByStateAB
(
	@StateAB CHAR(2)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY
		SELECT * FROM dbo.MC_PoliticalStates WHERE (StateAB = @StateAB);
	END TRY

	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown
		RETURN	
	END CATCH	

END
GO

GRANT EXEC ON dbo.custMC_PoliticalStateGetByStateAB TO PUBLIC
GO