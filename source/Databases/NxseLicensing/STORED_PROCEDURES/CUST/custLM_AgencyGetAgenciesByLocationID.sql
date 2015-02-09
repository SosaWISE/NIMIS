USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_AgencyGetAgenciesByLocationID')
	BEGIN
		PRINT 'Dropping Procedure custLM_AgencyGetAgenciesByLocationID'
		DROP  Procedure  dbo.custLM_AgencyGetAgenciesByLocationID
	END
GO

PRINT 'Creating Procedure custLM_AgencyGetAgenciesByLocationID'
GO
/******************************************************************************
**		File: custLM_AgencyGetAgenciesByLocationID.sql
**		Name: custLM_AgencyGetAgenciesByLocationID
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
CREATE Procedure [dbo].[custLM_AgencyGetAgenciesByLocationID]
(
	@LocationID INT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT
			LMAG.AgencyID
			, LMAG.AgencyName
			, LMAG.Contact
			, LMAG.Website
			, LMAG.Phone1		
		FROM
			LM_Agencies AS LMAG WITH (NOLOCK)
		WHERE 
			LMAG.IsActive = 1
			AND LMAG.IsDeleted = 0	
			AND LMAG.LocationID = @LocationID

		--Notes
		SELECT
			LMN.NoteID
			, LMN.ForeignKeyID
			, LMN.Note
			, LMN.CreatedByID
			, LMN.CreatedByDate
		FROM 
			LM_Notes AS LMN WITH (NOLOCK)
			INNER JOIN LM_Agencies AS LMAG WITH (NOLOCK)
			ON
				LMN.ForeignKeyID = LMAG.AgencyID
		WHERE 
			LMAG.LocationID = @LocationID

		--Attachments
		SELECT 
			LMA.AttachmentID
			, LMA.AttachmentName
			, LMA.Description
			, LMA.FileType
			, LMA.FileName
			, LMA.FileImage
			, LMA.Size
			, LMA.CreatedByID
			, LMA.CreatedByDate
		FROM 
			LM_Attachments AS LMA WITH (NOLOCK)
			INNER JOIN LM_Agencies AS LMAG WITH (NOLOCK)
			ON
				LMA.ForeignKeyID = LMAG.AgencyID
		WHERE 
			(LMAG.LocationID = @LocationID)

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_AgencyGetAgenciesByLocationID TO PUBLIC
GO

/** EXEC dbo.custLM_AgencyGetAgenciesByLocationID */