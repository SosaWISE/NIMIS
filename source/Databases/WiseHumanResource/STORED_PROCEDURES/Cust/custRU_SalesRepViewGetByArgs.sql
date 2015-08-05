USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_SalesRepViewGetByArgs')
	BEGIN
		PRINT 'Dropping Procedure custRU_SalesRepViewGetByArgs'
		DROP  Procedure  dbo.custRU_SalesRepViewGetByArgs
	END
GO

PRINT 'Creating Procedure custRU_SalesRepViewGetByArgs'
GO
/******************************************************************************
**		File: custRU_SalesRepViewGetByArgs.sql
**		Name: custRU_SalesRepViewGetByArgs
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
**		Date: 03/28/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/28/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custRU_SalesRepViewGetByArgs
(
	@SalesRepID VARCHAR(50) = NULL
	, @SeasonId INT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		SELECT 
			*
		FROM
			vwRU_SalesReps AS RSR
		WHERE
			(@SalesRepID IS NULL OR (RSR.SalesRepID = @SalesRepID))
			AND (@SeasonId IS NULL OR (RSR.RepSeasonId = @SeasonId))
		ORDER BY
			RSR.RepFirstName
			, RSR.RepLastName;
		
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custRU_SalesRepViewGetByArgs TO PUBLIC
GO

/**  */
EXEC dbo.custRU_SalesRepViewGetByArgs @SalesRepId = NULL, @SeasonId = NULL;
