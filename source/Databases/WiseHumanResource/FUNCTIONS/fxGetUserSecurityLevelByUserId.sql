USE [WISE_HumanResource]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetUserSecurityLevelByUserId')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetUserSecurityLevelByUserId'
		DROP FUNCTION  dbo.fxGetUserSecurityLevelByUserId
	END
GO

PRINT 'Creating FUNCTION fxGetUserSecurityLevelByUserId'
GO
/******************************************************************************
**		File: fxGetUserSecurityLevelByUserId.sql
**		Name: fxGetUserSecurityLevelByUserId
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
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	09/18/2014	reagan descartin	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetUserSecurityLevelByUserId
(
	@UserId BIGINT
)
RETURNS TINYINT
AS
BEGIN
	-- Locals
	DECLARE @SecurityLevel TINYINT

	/* Init values */
	SELECT @SecurityLevel =
		(
			SELECT TOP 1
				RUUT.SecurityLevel
			FROM [dbo].[RU_Recruits]  RUR
			INNER JOIN [dbo].[RU_UserType] RUUT
			ON
				RUR.[UserTypeId] = RUUT.[UserTypeID]
			WHERE (RUR.UserID = @UserId)
			ORDER BY
				RUUT.SecurityLevel DESC
		)
	
	/** Check that the season ID was found. */
	IF (@SecurityLevel IS NULL) SET @SecurityLevel = 0

	-- Return result
	RETURN @SecurityLevel
END
GO


/*


SELECT  
*
FROM 
[dbo].[RU_Recruits]  RUR
INNER JOIN
[dbo].[RU_UserType] RUUT
ON
RUR.[UserTypeId]=RUUT.[UserTypeID]

*/