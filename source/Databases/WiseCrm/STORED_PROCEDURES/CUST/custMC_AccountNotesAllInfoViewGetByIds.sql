USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMC_AccountNotesAllInfoViewGetByIds')
	BEGIN
		PRINT 'Dropping Procedure custMC_AccountNotesAllInfoViewGetByIds'
		DROP  Procedure  dbo.custMC_AccountNotesAllInfoViewGetByIds
	END
GO

PRINT 'Creating Procedure custMC_AccountNotesAllInfoViewGetByIds'
GO
/******************************************************************************
**		File: custMC_AccountNotesAllInfoViewGetByIds.sql
**		Name: custMC_AccountNotesAllInfoViewGetByIds
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
**		Date: 05/12/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	05/12/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMC_AccountNotesAllInfoViewGetByIds
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
			NVW.*
			--NTE.NoteID
			--, NTE.NoteTypeId
			--, NTE.CustomerMasterFileId
			--, NTE.CustomerId
			--, NTE.LeadId
			--, NTE.NoteCategory1Id
			--, NTE.NoteCategory2Id
			--, NTE.Note
			--, NTE.CreatedBy
			--, NTE.CreatedOn
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
			INNER JOIN [dbo].vwMC_AccountNotesAllInfo AS NVW
			ON
				(NVW.NoteID = NTE.NoteID)
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

GRANT EXEC ON dbo.custMC_AccountNotesAllInfoViewGetByIds TO PUBLIC
GO

/** Test 
EXEC dbo.custMC_AccountNotesAllInfoViewGetByIds @CMFID = NULL, --3000023, -- bigint
    @CustomerId = NULL, -- bigint
    @LeadId = NULL, -- bigint
    @PageSize = 10, -- int
    @PageNumber = 1; -- int
*/