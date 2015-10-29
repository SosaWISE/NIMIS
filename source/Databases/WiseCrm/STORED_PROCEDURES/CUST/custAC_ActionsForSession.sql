USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAC_ActionsForSession')
	BEGIN
		PRINT 'Dropping Procedure custAC_ActionsForSession'
		DROP  Procedure  dbo.custAC_ActionsForSession
	END
GO

PRINT 'Creating Procedure custAC_ActionsForSession'
GO
/******************************************************************************
**		File: custAC_ActionsForSession.sql
**		Name: custAC_ActionsForSession
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
CREATE Procedure dbo.custAC_ActionsForSession
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
	FROM AC_Actions AS A
	INNER JOIN
	(
		-- UserID
		SELECT
			A.ActionId
		FROM AC_UserActions AS A
		WHERE
			(A.IsActive = 1 AND A.IsDeleted = 0)
			AND UserId = @UserID
		
		UNION ALL

		-- User Groups
		SELECT
			A.ActionId
		FROM AC_GroupActions AS A
		INNER JOIN dbo.fxSplitStringList(@GroupList) G -- each group name must be 100 chars or less
		ON
			A.GroupName = G.ID
		WHERE
			(A.IsActive = 1 AND A.IsDeleted = 0)
	) AS PERMS
	ON
		A.ActionID = PERMS.ActionId
	WHERE
		(A.IsActive = 1 AND A.IsDeleted = 0)

END
GO

GRANT EXEC ON dbo.custAC_ActionsForSession TO PUBLIC
GO

/**

EXEC custAC_ActionsForSession 194757

 */