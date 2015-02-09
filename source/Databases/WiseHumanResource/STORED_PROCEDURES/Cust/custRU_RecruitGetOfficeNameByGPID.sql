USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitGetOfficeNameByGPID')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitGetOfficeNameByGPID'
		DROP  Procedure  dbo.custRU_RecruitGetOfficeNameByGPID
	END
GO

PRINT 'Creating Procedure custRU_RecruitGetOfficeNameByGPID'
GO
/******************************************************************************
**		File: custRU_RecruitGetOfficeNameByGPID.sql
**		Name: custRU_RecruitGetOfficeNameByGPID
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
**		Auth: Carly Christiansen
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:				Description:
**	-----------	-------------		-------------------------------------------
**	12/05/2013	Carly Christiansen	Created by
*******************************************************************************/
CREATE Procedure dbo.custRU_RecruitGetOfficeNameByGPID
(
	@GPEmployeeID NVARCHAR(30)
	, @SeasonID INT
)
AS
BEGIN

	SELECT
		COALESCE(OfficeName, 'Not Assigned to an Office') AS OfficeName
	FROM
		fxGetPayrollRoster(@SeasonID)
	WHERE
		GPEmployeeID = @GPEmployeeID
	
END
GO

GRANT EXEC ON dbo.custRU_RecruitGetOfficeNameByGPID TO PUBLIC
GO