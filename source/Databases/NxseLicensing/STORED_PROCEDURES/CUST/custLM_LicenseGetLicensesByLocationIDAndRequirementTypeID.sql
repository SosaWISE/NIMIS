USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_LicenseGetLicensesByLocationIDAndRequirementTypeID')
	BEGIN
		PRINT 'Dropping Procedure custLM_LicenseGetLicensesByLocationIDAndRequirementTypeID'
		DROP  Procedure  dbo.custLM_LicenseGetLicensesByLocationIDAndRequirementTypeID
	END
GO

PRINT 'Creating Procedure custLM_LicenseGetLicensesByLocationIDAndRequirementTypeID'
GO
/******************************************************************************
**		File: custLM_LicenseGetLicensesByLocationIDAndRequirementTypeID.sql
**		Name: custLM_LicenseGetLicensesByLocationIDAndRequirementTypeID
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
CREATE Procedure [dbo].[custLM_LicenseGetLicensesByLocationIDAndRequirementTypeID]
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

		-- Licenses
		SELECT LML.LicenseID
				, LMR.RequirementName AS LicenseName
				, LML.LicenseNumber
				, LML.ExpirationDate
				, LML.SubmissionDate
				, CASE 
					WHEN RUU.FullName IS NOT NULL
					THEN RUU.FullName
					Else CAST(LML.AccountID AS NVARCHAR(50))
				END AS OwnerID		
		FROM LM_Licenses AS LML WITH (NOLOCK)
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				LML.RequirementID = LMR.RequirementID
			LEFT OUTER JOIN WISE_HumanResource.dbo.RU_Users AS RUU WITH (NOLOCK)
			ON
				LML.GPEmployeeID = RUU.GPEmployeeID
		WHERE LML.IsActive = 1
			AND LML.IsDeleted = 0
			AND LMR.RequirementTypeID = @RequirementTypeID
			AND LMR.LocationID = @LocationID

		--Notes
		SELECT LMN.NoteID
				, LMN.ForeignKeyID
				, LMN.Note
				, LMN.CreatedByID
				, LMN.CreatedByDate
		FROM LM_Notes AS LMN WITH (NOLOCK)
			INNER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMN.ForeignKeyID = LML.LicenseID
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				LML.RequirementID = LMR.RequirementID
		WHERE LMR.LocationID = @LocationID
			AND LMN.NoteTypeID = 3 -- License Note Type
			AND LMR.RequirementTypeID = @RequirementTypeID
			AND LML.IsDeleted = 0
			AND LML.IsActive = 1


		--Items
		SELECT LMI.LicenseItemID
				, LMI.LicenseID
				, LMI.Name
				, LMI.Description
				, LMI.IsCompleted
				, LMI.CreatedByID
				, LMI.CreatedByDate
		FROM LM_LicenseItems AS LMI WITH (NOLOCK)
			INNER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMI.LicenseID = LML.LicenseID
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				LML.RequirementID = LMR.RequirementID
		WHERE LMR.LocationID = @LocationID
			AND LMR.RequirementTypeID = @RequirementTypeID
			AND LML.IsDeleted = 0
			AND LML.IsActive = 1
		

		--Attachments
		SELECT LMA.AttachmentID
				, LMA.ForeignKeyID
				, LMA.AttachmentName
				, LMA.Description
				, LMA.FileType
				, LMA.FileName
				, LMA.FileImage
				, LMA.Size
				, LMA.CreatedByID
				, LMA.CreatedByDate
		FROM LM_Attachments AS LMA WITH (NOLOCK)
			INNER JOIN LM_LicenseItems AS LMI WITH (NOLOCK)
			ON
				LMA.ForeignKeyID = LMI.LicenseItemID
			INNER JOIN LM_Licenses AS LML WITH (NOLOCK)
			ON
				LMI.LicenseID = LML.LicenseID	
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				LML.RequirementID = LMR.RequirementID
		WHERE LMR.LocationID = @LocationID
			AND LMA.AttachmentTypeID = 2 --License Item attachment type
			AND LMR.RequirementTypeID = @RequirementTypeID
			AND LML.IsDeleted = 0
			AND LML.IsActive = 1

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_LicenseGetLicensesByLocationIDAndRequirementTypeID TO PUBLIC
GO

/** EXEC dbo.custLM_LicenseGetLicensesByLocationIDAndRequirementTypeID */