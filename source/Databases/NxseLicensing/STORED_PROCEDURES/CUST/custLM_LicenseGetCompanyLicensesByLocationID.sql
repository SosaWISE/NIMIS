USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLM_LicenseGetCompanyLicensesByLocationID')
	BEGIN
		PRINT 'Dropping Procedure custLM_LicenseGetCompanyLicensesByLocationID'
		DROP  Procedure  dbo.custLM_LicenseGetCompanyLicensesByLocationID
	END
GO

PRINT 'Creating Procedure custLM_LicenseGetCompanyLicensesByLocationID'
GO
/******************************************************************************
**		File: custLM_LicenseGetCompanyLicensesByLocationID.sql
**		Name: custLM_LicenseGetCompanyLicensesByLocationID
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
CREATE Procedure [dbo].[custLM_LicenseGetCompanyLicensesByLocationID]
(
	@LocationID INT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT LML.LicenseID
				, LMR.RequirementName AS LicenseName
				, LML.LicenseNumber
				, LML.ExpirationDate		
		FROM LM_Licenses LML
			INNER JOIN LM_Requirements LMR
			ON
				LML.RequirementID = LMR.RequirementID
		WHERE LML.IsActive = 1
			AND LML.IsDeleted = 0
			AND LMR.RequirementTypeID = 1 -- This is the 'Company' Requirement Type
			AND LMR.LocationID = @LocationID

		--Notes
		SELECT LMN.NoteID
				, LMN.ForeignKeyID
				, LMN.Note
				, LMN.CreatedByID
				, LMN.CreatedByDate
		FROM LM_Notes LMN
			INNER JOIN LM_Licenses LML
			ON
				LMN.ForeignKeyID = LML.LicenseID
			INNER JOIN LM_Requirements LMR
			ON
				LML.RequirementID = LMR.RequirementID
		WHERE LMR.LocationID = @LocationID

		--Items
		SELECT LMI.LicenseItemID
				, LMI.LicenseID
				, LMI.Name
				, LMI.Description
				, LMI.IsCompleted
				, LMI.CreatedByID
				, LMI.CreatedByDate
		FROM LM_LicenseItems LMI
			INNER JOIN LM_Licenses LML
			ON
				LMI.LicenseID = LML.LicenseID
			INNER JOIN LM_Requirements LMR
			ON
				LML.RequirementID = LMR.RequirementID
		WHERE LMR.LocationID = @LocationID
		

		--Attachments
		SELECT LMA.AttachmentID
				, LMA.AttachmentName
				, LMA.Description
				, LMA.FileType
				, LMA.FileName
				, LMA.FileImage
				, LMA.Size
				, LMA.CreatedByID
				, LMA.CreatedByDate
		FROM LM_Attachments LMA
			INNER JOIN LM_LicenseItems LMI
			ON
				LMA.ForeignKeyID = LMI.LicenseItemID
			INNER JOIN LM_Licenses LML
			ON
				LMI.LicenseID = LML.LicenseID	
			INNER JOIN LM_Requirements LMR
			ON
				LML.RequirementID = LMR.RequirementID
		WHERE LMR.LocationID = @LocationID

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custLM_LicenseGetCompanyLicensesByLocationID TO PUBLIC
GO

/** EXEC dbo.custLM_LicenseGetCompanyLicensesByLocationID */