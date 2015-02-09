USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_LocationsLoadActiveLocations')
	BEGIN
		PRINT 'Dropping Procedure custLM_LocationsLoadActiveLocations'
		DROP  Procedure  dbo.custLM_LocationsLoadActiveLocations
	END
GO

PRINT 'Creating Procedure custLM_LocationsLoadActiveLocations'
GO
/******************************************************************************
**		File: custLM_LocationsLoadActiveLocations.sql
**		Name: custLM_LocationsLoadActiveLocations
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
CREATE Procedure [dbo].[custLM_LocationsLoadActiveLocations]
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		SELECT
			*
		FROM
			LM_Locations
		WHERE
			(IsActive = 1)
			AND (IsDeleted) = 0
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_LocationsLoadActiveLocations TO PUBLIC
GO

/** EXEC dbo.custLM_LocationsLoadActiveLocations */