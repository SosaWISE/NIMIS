USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_RequirementGetRequirementByLocationIDAndRequirementTypeID')
	BEGIN
		PRINT 'Dropping Procedure custLM_RequirementGetRequirementByLocationIDAndRequirementTypeID'
		DROP  Procedure  dbo.custLM_RequirementGetRequirementByLocationIDAndRequirementTypeID
	END
GO

PRINT 'Creating Procedure custLM_RequirementGetRequirementByLocationIDAndRequirementTypeID'
GO
/******************************************************************************
**		File: custLM_RequirementGetRequirementByLocationIDAndRequirementTypeID.sql
**		Name: custLM_RequirementGetRequirementByLocationIDAndRequirementTypeID
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
CREATE Procedure [dbo].[custLM_RequirementGetRequirementByLocationIDAndRequirementTypeID]
(
	@LocationID INT = NULL
	, @RequirementTypeID INT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY


			--Requirements
			SELECT 
				LMR.RequirementID
				, LMR.RequirementName
				, CASE 
					WHEN LMA.AgencyName IS NULL
					THEN 'N/A'
					ELSE LMA.AgencyName
					END AS AgencyName
				, LMR.ApplicationDescription
		FROM LM_Requirements AS LMR WITH (NOLOCK)
			LEFT OUTER JOIN LM_Agencies AS LMA WITH (NOLOCK)
			ON
				LMR.AgencyID = LMA.AgencyID
		WHERE 
			LMR.IsActive = 1
			AND LMR.IsDeleted = 0
			AND LMR.LocationID = @LocationID
			AND LMR.RequirementTypeID = @RequirementTypeID

			--Notes
			SELECT 
				LMN.NoteID
				, LMN.ForeignKeyID
				, LMN.Note
				, LMN.CreatedByID
				, LMN.CreatedByDate
		FROM LM_Notes AS LMN WITH (NOLOCK)
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				LMN.ForeignKeyID = LMR.RequirementID
		WHERE LMR.LocationID = @LocationID
				AND LMR.RequirementTypeID = @RequirementTypeID

			--Items
			SELECT 
				LRI.RequirementItemID
				, LRI.RequirementID
				, LRI.Name
				, LRI.Description
				, LRI.CreatedByID
				, LRI.CreatedByDate
		FROM LM_RequirementItems AS LRI WITH (NOLOCK)
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				LRI.RequirementID = LMR.RequirementID
		WHERE LMR.LocationID = @LocationID
				AND LMR.RequirementTypeID = @RequirementTypeID
		

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
		FROM LM_Attachments AS LMA WITH (NOLOCK)
			INNER JOIN LM_RequirementItems AS LRI WITH (NOLOCK)
			ON
				LMA.ForeignKeyID = LRI.RequirementItemID
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				LRI.RequirementID = LMR.RequirementID	
		WHERE LMR.LocationID = @LocationID
				AND LMR.RequirementTypeID = @RequirementTypeID

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_RequirementGetRequirementByLocationIDAndRequirementTypeID TO PUBLIC
GO

/** EXEC dbo.custLM_RequirementGetRequirementByLocationIDAndRequirementTypeID */