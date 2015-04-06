USE [NXSE_Licensing]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetLicenseStatus')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetLicenseStatus'
		DROP FUNCTION  dbo.fxGetLicenseStatus
	END
GO

PRINT 'Creating FUNCTION fxGetLicenseStatus'
GO
/******************************************************************************
**		File: fxGetLicenseStatus.sql
**		Name: fxGetLicenseStatus
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
**		Auth: Andrés E. Sosa
**		Date: 04/01/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/01/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetLicenseStatus
(
	@CanSolicit BIT
	, @LicenseID INT
	, @RequirementsAreMet BIT
	, @IssueDate DATETIME
	, @ExpirationDate DATETIME
)
RETURNS VARCHAR(100)
AS
BEGIN
	/** Declarations */
	DECLARE @LicenseStatus VARCHAR(100);

	/** Execute actions. */
	SELECT
		@LicenseStatus = CASE
			WHEN @CanSolicit = 0
				THEN 'No Solicitation'

			WHEN @LicenseID IS NULL
				THEN 'Missing License'

			WHEN @RequirementsAreMet = 0
				THEN 'License Incomplete'

			WHEN GETDATE() < @IssueDate
				THEN 'License not active yet'

			WHEN GETDATE() > @ExpirationDate
				THEN 'License Expired'
			
			ELSE 'Licensing Complete'
		END


	RETURN @LicenseStatus;
END
GO