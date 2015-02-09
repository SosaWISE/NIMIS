USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetPossibleReportTos')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetPossibleReportTos'
		DROP  Procedure  dbo.custRU_RecruitsGetPossibleReportTos
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetPossibleReportTos'
GO
/******************************************************************************
**		File: custRU_RecruitsGetPossibleReportTos.sql
**		Name: custRU_RecruitsGetPossibleReportTos
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
**		Auth: 
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:				Description:
**	-----------	-------------		-------------------------------------------
**	12/05/2013	Carly Christiansen	Created by
*******************************************************************************/
CREATE Procedure dbo.custRU_RecruitsGetPossibleReportTos
(
	@SeasonID INT
	, @UserTypeID INT
)
AS
BEGIN
	
	--DECLARE @SeasonID INT
	--SET @SeasonID = 6
	--
	--DECLARE @UserTypeID INT
	--SET @UserTypeID = 5
		
	DECLARE @ReportingLevel INT
	DECLARE @SecurityLevel INT
	DECLARE @RoleLocationID INT

	SELECT
		@ReportingLevel = RUT.ReportingLevel
		, @SecurityLevel = RUT.SecurityLevel
		, @RoleLocationID = RUT.RoleLocationID
	FROM RU_UserType AS RUT WITH(NOLOCK)
	WHERE
		RUT.UserTypeID = @UserTypeID
/* Show results */
Print '@UserTypeID=' + CAST(@UserTypeID AS VARCHAR)
Print '@ReportingLevel=' + CAST(@ReportingLevel AS VARCHAR)
Print '@SecurityLevel=' + CAST(@SecurityLevel AS VARCHAR)
Print '@RoleLocationID=' + CAST(@RoleLocationID AS VARCHAR)

	SELECT 
		RU.UserID
		, RU.FullName
		, RR.RecruitID
		, RU.FullName + ' (' + RUT.Description + ')' AS FullDescription
	FROM [RU_Users] AS RU WITH (NOLOCK)
	INNER JOIN [RU_Recruits] AS RR WITH (NOLOCK)
	ON
		RU.UserID = RR.UserID
	INNER JOIN [RU_UserType] AS RUT WITH (NOLOCK)
	ON
		RR.UserTypeID = RUT.UserTypeID
	WHERE
		(
			(RUT.ReportingLevel > @ReportingLevel)
			AND (RUT.RoleLocationID = @RoleLocationID)
		)
		AND (RU.IsDeleted = 0)--((RU.IsActive = 1) AND (RU.IsDeleted = 0))
		AND (RR.IsDeleted = 0)--((RR.IsActive = 1) AND (RR.IsDeleted = 0))
		AND (RR.SeasonID = @SeasonID)
		--OR
		--(
		--	(RU.UserID = 5799)
		--	AND (RUT.RoleLocationID = @RoleLocationID)
		--)
	ORDER BY
		RU.FullName

END

GO

GRANT EXEC ON dbo.custRU_RecruitsGetPossibleReportTos TO PUBLIC
GO