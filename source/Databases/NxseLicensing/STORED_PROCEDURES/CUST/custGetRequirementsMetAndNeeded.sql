USE [NXSE_Licensing]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGetRequirementsMetAndNeeded')
	BEGIN
		PRINT 'Dropping Procedure custGetRequirementsMetAndNeeded'
		DROP  Procedure  dbo.custGetRequirementsMetAndNeeded
	END
GO

PRINT 'Creating Procedure custGetRequirementsMetAndNeeded'
GO
/******************************************************************************
**		File: custGetRequirementsMetAndNeeded.sql
**		Name: custGetRequirementsMetAndNeeded
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
CREATE Procedure dbo.custGetRequirementsMetAndNeeded
(
	@GPEmployeeID NVARCHAR(100)
	, @RequirementTypeID INT
	, @LocationName NVARCHAR(100)
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
	(
		--LICENSES
		SELECT
			LML.LicenseID
			, LMR.RequirementID
			, LMR.RequirementName
			, LMR.LockID
			, LML.LicenseNumber
			, CAST(LML.ExpirationDate AS NVARCHAR(20)) AS ExpirationDate
			, LOC.LocationName
			, CAST(LML.RequirementsAreMet AS BIT) AS RequirementsAreMet
		FROM
			LM_Licenses AS LML WITH (NOLOCK)
			INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
			ON
				LML.RequirementID = LMR.RequirementID
				AND LMR.IsDeleted = 0
				AND LMR.RequirementTypeID = @RequirementTypeID
			INNER JOIN LM_Locations AS LOC WITH (NOLOCK)
			ON
				(LMR.LocationID = LOC.LocationID)
		WHERE
			LML.GPEmployeeID = @GPEmployeeID
			AND LOC.LocationName LIKE @LocationName
			--AND LML.RequirementsAreMet = 0

		UNION

		--GLOBAL REQUIREMENTS
		SELECT
			0 AS LicenseID
			, LMR.RequirementID
			, LMR.RequirementName
			, LMR.LockID
			, 'N/A' AS LicenseNumber
			, 'N/A' AS ExpirationDate
			, LOC.LocationName
			, CAST(0 AS BIT) AS RequirementsAreMet
		FROM
			LM_Requirements AS LMR WITH (NOLOCK)
			INNER JOIN LM_Locations AS LOC WITH (NOLOCK)
			ON
				LMR.LocationID = LOC.LocationID
		WHERE
			LMR.RequirementTypeID = @RequirementTypeID
			AND LOC.LocationTypeID = 1 -- Global Type
			AND LMR.IsDeleted = 0
			AND LMR.RequirementID NOT IN (--LICENSES
											SELECT LMR.RequirementID
											FROM LM_Licenses AS LML WITH (NOLOCK)
												INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
												ON
													LML.RequirementID = LMR.RequirementID
													AND LMR.IsDeleted = 0
											WHERE LML.GPEmployeeID = @GPEmployeeID) 

		UNION

		--LOCATION REQUIREMENTS
		SELECT
			0 AS LicenseID
			, LMR.RequirementID
			, LMR.RequirementName
			, LMR.LockID
			, 'N/A' AS LicenseNumber
			, 'N/A' AS ExpirationDate
			, ARPL.LocationName
			, CAST(0 AS BIT) AS RequirementsAreMet
		FROM
			LM_Requirements AS LMR WITH (NOLOCK)
			INNER JOIN vwAllRequirementsPerLocation AS ARPL WITH (NOLOCK)
			ON
				LMR.RequirementID = ARPL.RequirementID
		WHERE
			LMR.RequirementTypeID = @RequirementTypeID
			AND (ARPL.LocationName LIKE @LocationName OR ARPL.Abbreviation LIKE @LocationName)
			AND LMR.IsDeleted = 0
			AND ARPL.RequirementID NOT IN (--LICENSES
											SELECT 
												LMR.RequirementID
											FROM
												LM_Licenses AS LML WITH (NOLOCK)
												INNER JOIN LM_Requirements AS LMR WITH (NOLOCK)
												ON
													LML.RequirementID = LMR.RequirementID
													AND LMR.IsDeleted = 0
											WHERE LML.GPEmployeeID = @GPEmployeeID)
	) AS Results
	ORDER BY
		RequirementsAreMet DESC
		, LocationName

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custGetRequirementsMetAndNeeded TO PUBLIC
GO

/** EXEC dbo.custGetRequirementsMetAndNeeded */