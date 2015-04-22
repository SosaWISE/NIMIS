USE [WISE_HumanResource]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxRU_UsersGetRecruitsBySalesRepID')
	BEGIN
		PRINT 'Dropping FUNCTION fxRU_UsersGetRecruitsBySalesRepID'
		DROP FUNCTION  dbo.fxRU_UsersGetRecruitsBySalesRepID
	END
GO

PRINT 'Creating FUNCTION fxRU_UsersGetRecruitsBySalesRepID'
GO
/******************************************************************************
**		File: fxRU_UsersGetRecruitsBySalesRepID.sql
**		Name: fxRU_UsersGetRecruitsBySalesRepID
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
**	12/05/2013	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxRU_UsersGetRecruitsBySalesRepID
(
	@GPEmployeeId NVARCHAR(25)
)
RETURNS 
@ResultList table
(
	UserID INT
	, FullName NVARCHAR(101)
	, GPEmployeeId NVARCHAR(25)
)
AS
BEGIN
	/** Get UserID  */
	INSERT INTO @ResultList (UserID, FullName, GPEmployeeId)
	SELECT
		RU1.UserID
		, RU1.FullName
		, RU1.GPEmployeeId
	FROM
		[dbo].[RU_Users] AS RU1 WITH (NOLOCK)
		INNER JOIN (
			SELECT TOP 1
				UserID
			FROM
				[dbo].[RU_Users] AS RU WITH (NOLOCK)
			WHERE
				(RU.GPEmployeeID = @GPEmployeeId)) AS RU2
		ON
			(RU2.UserID = RU1.RecruitedById);

	/** Return result */
	RETURN
END
GO

/** TEST 
SELECT * FROM [dbo].fxRU_UsersGetRecruitsBySalesRepID('NEISB001');

SELECT 
	RU1.RecruitedById
	, RU.GPEmployeeId
	, COUNT(*)
FROM
	[dbo].[RU_Users] AS RU WITH (NOLOCK)
	INNER JOIN [dbo].[RU_Users] AS RU1 WITH (NOLOCK)
	ON
		(RU1.RecruitedById = RU.UserID)
GROUP BY
	RU1.RecruitedById
	, RU.GPEmployeeId
ORDER BY
	COUNT(*) DESC;
*/