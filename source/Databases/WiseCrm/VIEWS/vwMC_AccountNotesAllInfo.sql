USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMC_AccountNotesAllInfo')
	BEGIN
		PRINT 'Dropping VIEW vwMC_AccountNotesAllInfo'
		DROP VIEW dbo.vwMC_AccountNotesAllInfo
	END
GO

PRINT 'Creating VIEW vwMC_AccountNotesAllInfo'
GO

/****** Object:  View [dbo].[vwMC_AccountNotesAllInfo]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMC_AccountNotesAllInfo.sql
**		Name: vwMC_AccountNotesAllInfo
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andres Sosa
**		Date: 05/04/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	05/04/2012	Andres Sosa	Created
*******************************************************************************/
CREATE VIEW [dbo].[vwMC_AccountNotesAllInfo]
AS
	/** Enter Query here. */
	SELECT
		NTS.NoteID
		, ANT.NoteTypeId
		, ANT.NoteType
		, NTS.CustomerMasterFileId
		, NTS.CustomerId
		, NTS.LeadId
		, CAT1.NoteCategory1Id
		, CAT1.Category AS Category1
		, CAT1.[Description] AS Desc1
		, CAT2.NoteCategory2Id
		, CAT2.Category AS Category2
		, CAT2.[Description] AS Desc2
		, NTS.Note
		, NTS.CreatedBy
		, NTS.CreatedOn
	FROM
		[dbo].MC_AccountNotes AS NTS WITH (NOLOCK)
		INNER JOIN [dbo].MC_AccountNoteTypes AS ANT WITH (NOLOCK)
		ON
			(NTS.NoteTypeId = ANT.NoteTypeID)
		INNER JOIN [dbo].MC_AccountNoteCat1 AS CAT1 WITH (NOLOCK)
		ON
			(NTS.NoteCategory1Id = CAT1.NoteCategory1ID)
		INNER JOIN [dbo].MC_AccountNoteCat2 AS CAT2 WITH (NOLOCK)
		ON
			(NTS.NoteCategory2Id = CAT2.NoteCategory2ID)

GO
/* TEST */
SELECT * FROM vwMC_AccountNotesAllInfo
