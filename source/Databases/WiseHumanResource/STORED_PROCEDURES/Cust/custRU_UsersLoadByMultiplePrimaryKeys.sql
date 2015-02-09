USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersLoadByMultiplePrimaryKeys')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersLoadByMultiplePrimaryKeys'
		DROP  Procedure  dbo.custRU_UsersLoadByMultiplePrimaryKeys
	END
GO

PRINT 'Creating Procedure custRU_UsersLoadByMultiplePrimaryKeys'
GO
/******************************************************************************
**		File: custRU_UsersLoadByMultiplePrimaryKeys.sql
**		Name: custRU_UsersLoadByMultiplePrimaryKeys
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
CREATE Procedure dbo.custRU_UsersLoadByMultiplePrimaryKeys
(
	@UserIDList NVARCHAR(MAX)
)
AS
BEGIN

	SELECT
		RU.*
	FROM RU_Users AS RU WITH (NOLOCK)
	INNER JOIN dbo.SplitIntList(@UserIDList) AS Ids
	ON
		RU.UserID = Ids.ID
	ORDER BY
		RU.FullName ASC
		
END
GO

GRANT EXEC ON dbo.custRU_UsersLoadByMultiplePrimaryKeys TO PUBLIC
GO