USE [WISE_AuthenticationControl]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAC_ApplicationsForSession')
	BEGIN
		PRINT 'Dropping Procedure custAC_ApplicationsForSession'
		DROP  Procedure  dbo.custAC_ApplicationsForSession
	END
GO

PRINT 'Creating Procedure custAC_ApplicationsForSession'
GO
/******************************************************************************
**		File: custAC_ApplicationsForSession.sql
**		Name: custAC_ApplicationsForSession
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
**		Auth: Aaron Shumway
**		Date: 10/08/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	
*******************************************************************************/
CREATE Procedure dbo.custAC_ApplicationsForSession
(
	@SessionID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	DECLARE @UserID INT
	DECLARE @GroupList NVARCHAR(MAX)
	
	SELECT
		@UserID = UserId
		, @GroupList = Groups
	FROM AC_Sessions
	WHERE
		SessionID = @SessionID
	
	SELECT DISTINCT
		A.*
		--, PERMS.*
	FROM AC_Applications AS A
	INNER JOIN
	(
		-- UserID
		SELECT
			A.ApplicationId
		FROM AC_UserACLs AS A
		WHERE
			(A.IsActive = 1 AND A.IsDeleted = 0)
			AND UserId = @UserID
		
		UNION ALL
		
		-- User Groups
		SELECT
			A.ApplicationId
		FROM AC_GroupApplications AS A
		INNER JOIN dbo.fxSplitStringList(@GroupList) G -- each group name must be 100 chars or less
		ON
			A.GroupName = G.ID
		WHERE
			(A.IsActive = 1 AND A.IsDeleted = 0)
	) AS PERMS
	ON
		A.ApplicationID = PERMS.ApplicationId

END
GO

GRANT EXEC ON dbo.custAC_ApplicationsForSession TO PUBLIC
GO

/**

EXEC custAC_ApplicationsForSession 194757

 */