USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetAllUsersByRoleLocationID')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetAllUsersByRoleLocationID'
		DROP  Procedure  dbo.custRU_UsersGetAllUsersByRoleLocationID
	END
GO

PRINT 'Creating Procedure custRU_UsersGetAllUsersByRoleLocationID'
GO
/******************************************************************************
**		File: custRU_UsersGetAllUsersByRoleLocationID.sql
**		Name: custRU_UsersGetAllUsersByRoleLocationID
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
CREATE Procedure dbo.custRU_UsersGetAllUsersByRoleLocationID
(
	@RoleLocationID INT
	, @SeasonID INT = NULL
	, @DeletionStatus NVARCHAR(10) = NULL
)
AS
BEGIN

	--DECLARE @RoleLocationID INT
	--SET @RoleLocationID = 1
	--DECLARE @SeasonID INT
	--SET @SeasonID = 7
	--DECLARE @DeletionStatus NVARCHAR(10)
	--SET @DeletionStatus = 'NotDeleted'

	SELECT DISTINCT
		RU.*
		, RR.*
	FROM RU_Users AS RU WITH(NOLOCK)
	INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
	ON
		(RU.UserID = RR.UserID)
	INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
	ON
		RR.UserTypeId = RUT.UserTypeID
	WHERE
		(RU.GPEmployeeID IS NOT NULL)
		AND ((@SeasonID IS NULL) OR (RR.SeasonID = @SeasonID))
		AND (RUT.RoleLocationID = @RoleLocationID)
		AND (RU.IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus)))
		AND (RR.IsDeleted IN (SELECT IsDeleted FROM dbo.GetDeletionStatus(@DeletionStatus)))
	ORDER BY
		RU.FullName
		
END
GO

GRANT EXEC ON dbo.custRU_UsersGetAllUsersByRoleLocationID TO PUBLIC
GO