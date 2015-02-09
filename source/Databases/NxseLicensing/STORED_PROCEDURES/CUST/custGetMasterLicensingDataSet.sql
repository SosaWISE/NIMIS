USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGetMasterLicensingDataSet')
	BEGIN
		PRINT 'Dropping Procedure custGetMasterLicensingDataSet'
		DROP  Procedure  dbo.custGetMasterLicensingDataSet
	END
GO

PRINT 'Creating Procedure custGetMasterLicensingDataSet'
GO
/******************************************************************************
**		File: custGetMasterLicensingDataSet.sql
**		Name: custGetMasterLicensingDataSet
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
CREATE Procedure dbo.custGetMasterLicensingDataSet
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
			LML.LicenseID
			, LMR.RequirementName AS LicenseName
			, LML.LicenseNumber
			, LML.ExpirationDate		
		FROM 
			LM_Licenses AS LML WITH (NOLOCK)
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				(LML.RequirementID = LMR.RequirementID)
		WHERE 
			(LML.IsActive = 1)
			AND (LML.IsDeleted = 0)
			AND (LMR.RequirementTypeID = 1) -- This is the 'Company' Requirement Type
			AND (LMR.LocationID = @LocationID);
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custGetMasterLicensingDataSet TO PUBLIC
GO

/** EXEC dbo.custGetMasterLicensingDataSet */