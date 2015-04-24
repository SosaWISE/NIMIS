USE [WISE_HumanResource]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxRU_UsersViewGetTeamsByManagerSalesRepID')
	BEGIN
		PRINT 'Dropping FUNCTION fxRU_UsersViewGetTeamsByManagerSalesRepID'
		DROP FUNCTION  dbo.fxRU_UsersViewGetTeamsByManagerSalesRepID
	END
GO

PRINT 'Creating FUNCTION fxRU_UsersViewGetTeamsByManagerSalesRepID'
GO
/******************************************************************************
**		File: fxRU_UsersViewGetTeamsByManagerSalesRepID.sql
**		Name: fxRU_UsersViewGetTeamsByManagerSalesRepID
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
**		Date: 04/23/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/23/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxRU_UsersViewGetTeamsByManagerSalesRepID
(
	@ManSalesRepId NVARCHAR(25)
	, @SeasonId INT
)
RETURNS 
@ResultList table
(
	UserID INT
	, FullName NVARCHAR(101)
	, SalesRepId NVARCHAR(25)
	, SeasonId INT
	, HiredDate DATETIME
)
AS
BEGIN
	/** Declarations */
	DECLARE @TeamID INT;

	/** Get TeamID */
	SELECT 
		@TeamId = RUR.TeamId
	FROM
		dbo.RU_Users AS RU WITH (NOLOCK)
		INNER JOIN dbo.RU_Recruits AS RUR WITH (NOLOCK)
		ON
			(RUR.UserId = RU.UserID)
			AND (RUR.UserTypeId IN (2,3,11,18,19))
			AND (RU.GPEmployeeId = @ManSalesRepId)

	/** Build the Team */
	INSERT INTO @ResultList (
		FullName ,
		UserID ,
		SalesRepId ,
		HiredDate ,
		SeasonId
	) --VALUES (
	SELECT
		RU.FullName -- nvarchar(101)
		, RU.UserID -- UserID -- int
		, RU.GPEmployeeId -- SalesRepId -- nvarchar(25)
		, ISNULL(RUR.HireDate, RUR.CreatedOn) -- HiredDate -- datetime
		, RUR.SeasonId -- int
	FROM
		dbo.RU_Users AS RU WITH (NOLOCK)
		INNER JOIN dbo.RU_Recruits AS RUR WITH (NOLOCK)
		ON
			(RUR.UserId = RU.UserID)
			AND (RUR.UserTypeId IN (2,3,11,18,19)) -- Only show reps
			AND (RUR.SeasonId = @SeasonId)
			AND (RUR.TeamId = @TeamID)
			AND (RUR.IsActive = 1 AND RUR.IsDeleted = 0)

	/** Return result */
	RETURN
END
GO

/** TEST 
SELECT * FROM [WISE_HumanResource].[dbo].fxRU_UsersViewGetTeamsByManagerSalesRepID('LEDUM001', 4);
SELECT * FROM [WISE_HumanResource].[dbo].fxRU_UsersViewGetTeamsByManagerSalesRepID('VAZQA001', 4);
*/