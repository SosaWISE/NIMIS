USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_AccountNotesGetByIds')
	BEGIN
		PRINT 'Dropping Procedure custMC_AccountNotesGetByIds'
		DROP  Procedure  dbo.custMC_AccountNotesGetByIds
	END
GO

PRINT 'Creating Procedure custMC_AccountNotesGetByIds'
GO
/******************************************************************************
**		File: custMC_AccountNotesGetByIds.sql
**		Name: custMC_AccountNotesGetByIds
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
**		Date: 12/30/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	12/30/2013	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMC_AccountNotesGetByIds
(
	@CMFID BIGINT = NULL
	, @CustomerId BIGINT = NULL
	, @LeadId BIGINT = NULL
	, @PageSize INT = 10
	, @PageNumber INT = 1
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Calculate StartRow and EndRow. */
	DECLARE @StartRow INT
	DECLARE @EndRow INT
	-- Calculations 
	SET @StartRow = ((@PageNumber - 1) * @PageSize) + 1;
	SET @EndRow = @PageNumber * @PageSize;

	BEGIN TRY
	/** Argument Validation */	
	IF (@CMFID IS NULL AND @CustomerId IS NULL AND @LeadId IS NULL)
	BEGIN
		RAISERROR (N'@CMFID, @CustomerId, and @LeadId can not all be null at once..', 18, 1);
	END

	/** EXECUTE */
		SELECT
			NTE.NoteID
			, NTE.NoteTypeId
			, NTE.CustomerMasterFileId
			, NTE.CustomerId
			, NTE.LeadId
			, NTE.NoteCategory1Id
			, NTE.NoteCategory2Id
			, NTE.Note
			, NTE.CreatedBy
			, NTE.CreatedOn
		FROM
			(SELECT
				MAN.*
				, ROW_NUMBER() OVER (ORDER BY MAN.CreatedOn DESC) AS ROWNUN
			FROM
				[dbo].[MC_AccountNotes] AS MAN WITH (NOLOCK)
			WHERE
				((@CMFID IS NULL) OR MAN.CustomerMasterFileId = @CMFID)
				AND ((@CustomerId IS NULL) OR (MAN.CustomerId = @CustomerId))
				AND ((@LeadId IS NULL) OR (MAN.LeadId = @LeadId))) AS NTE
		WHERE
			(NTE.ROWNUN BETWEEN @StartRow AND @EndRow)
		ORDER BY
			NTE.CreatedOn DESC;
	
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		EXEC dbo.wiseSP_ExceptionsThrown
		RETURN
	END CATCH

END
GO

GRANT EXEC ON dbo.custMC_AccountNotesGetByIds TO PUBLIC
GO

/** Test 
EXEC dbo.custMC_AccountNotesGetByIds @CMFID = NULL, --3000023, -- bigint
    @CustomerId = NULL, -- bigint
    @LeadId = NULL, -- bigint
    @PageSize = 10, -- int
    @PageNumber = 1; -- int
*/