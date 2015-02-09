USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetTechnicianFullNameByTechnicianId')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetTechnicianFullNameByTechnicianId'
		DROP FUNCTION  dbo.fxGetTechnicianFullNameByTechnicianId
	END
GO

PRINT 'Creating FUNCTION fxGetTechnicianFullNameByTechnicianId'
GO
/******************************************************************************
**		File: fxGetTechnicianFullNameByTechnicianId.sql
**		Name: fxGetTechnicianFullNameByTechnicianId
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
**		Date: 03/14/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	09/09/2014	Reagan Descartin	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetTechnicianFullNameByTechnicianId
(
	@TechnicianId VARCHAR(30)

)
RETURNS NVARCHAR(250)
AS
BEGIN
	/** Declarations */
	DECLARE @FullName NVARCHAR(250);

	SET @FullName = ISNULL( (SELECT FullName FROM [WISE_HumanResource].[dbo].[RU_Users] WHERE GPEmployeeId = @TechnicianId), NULL);


	RETURN @FullName;
END
GO

