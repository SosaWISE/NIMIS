USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsLoadByRawSQL')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsLoadByRawSQL'
		DROP  Procedure  dbo.custRU_RecruitsLoadByRawSQL
	END
GO

PRINT 'Creating Procedure custRU_RecruitsLoadByRawSQL'
GO
/******************************************************************************
**		File: custRU_RecruitsLoadByRawSQL.sql
**		Name: custRU_RecruitsLoadByRawSQL
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
CREATE Procedure dbo.custRU_RecruitsLoadByRawSQL
(
	@FirstName NVARCHAR(50) = NULL
	, @LastName NVARCHAR(50) = NULL
	, @PhoneCell NVARCHAR(50) = NULL
	, @PhoneHome NVARCHAR(50) = NULL
	, @SSN NVARCHAR(50) = NULL
	, @BirthDate VARCHAR(10) = NULL
	, @SeasonId INT = 0 -- 2007 Summer
	, @Mode VARCHAR(20) = 'DEFAULT'
)
AS
BEGIN
	-- Locals
	DECLARE @SQLStmt NVARCHAR(1000)
	DECLARE @WhereClause NVARCHAR(1000)
	
	-- Set initial values
	SET @WhereClause = ''
	SET @SQLStmt = '
SELECT 
	RU.*
	, RR.*
	, RS.SeasonName
FROM 
	[RU_Users] AS RU WITH (NOLOCK)
	INNER JOIN [RU_Recruits] AS RR WITH (NOLOCK)
	ON
		RU.UserID = RR.UserID
--		AND (RU.IsActive = 1)
		AND (RU.IsDeleted = 0)
	INNER JOIN [RU_UserType] AS RUT WITH (NOLOCK)
	ON
		RR.UserTypeID = RUT.UserTypeID
--		AND (RR.IsActive = 1)
		AND (RR.IsDeleted = 0)
	INNER JOIN [RU_RoleLocations] AS RRL WITH (NOLOCK)
	ON
		RUT.RoleLocationID = RRL.RoleLocationID
	INNER JOIN [RU_Season] AS RS WITH (NOLOCK)
	ON
		RR.SeasonID = RS.SeasonID
WHERE
	@SEASON@
	@WHERECLAUSE@
ORDER BY
	RU.FullName
	, RS.SeasonName
	';

	DECLARE @SQLSeason VARCHAR(500)
	-- Add Season ID Constraint
	IF (@SeasonId = 0) OR (@Mode = 'RecruitedBy') OR (@Mode = 'ApprovedBy') OR (@Mode = 'Default')
	BEGIN
		SET @SQLSeason = '(1 = 1)'
	END
	ELSE
	BEGIN
		SET @SQLSeason = REPLACE ('(RR.SeasonID = @SeasonID@)', '@SeasonID@', @SeasonId)
	END
	
	SET @SQLStmt = REPLACE (@SQLStmt, '@SEASON@', @SQLSeason)
	
	-- Build Where Clause
	-- -- First Name
	IF (@FirstName IS NOT NULL)
	BEGIN 
		IF (CHARINDEX('*', @FirstName) >= 0)
		BEGIN
			SET @WhereClause = @WhereClause + ' AND (RU.FirstName LIKE ''' + REPLACE(@FirstName, '*', '%') + ''')';
		END
		ELSE
		BEGIN
			SET @WhereClause = @WhereClause + ' AND (RU.FirstName = ''' + @FirstName + ''')';
		END
	END
	-- -- Last Name
	IF (@LastName IS NOT NULL)
	BEGIN
		IF (CHARINDEX('*', @LastName) >= 0)
		BEGIN
			SET @WhereClause = @WhereClause + ' AND (RU.LastName LIKE ''' + REPLACE(@LastName, '*', '%') + ''')';
		END
		ELSE
		BEGIN
			SET @WhereClause = @WhereClause + ' AND (RU.LastName = ''' + @LastName + ''')';
		END
	END
	-- -- PhoneCell
	IF (@PhoneCell IS NOT NULL)
	BEGIN
		SET @WhereClause = @WhereClause + ' AND (RU.PhoneCell = ''' + @PhoneCell + ''')';
	END
	-- -- PhoneHome
	IF (@PhoneHome IS NOT NULL)
	BEGIN
		SET @WhereClause = @WhereClause + ' AND (RU.PhoneHome = ''' + @PhoneHome + ''')';
	END
	-- -- SSN
	IF (@SSN IS NOT NULL)
	BEGIN
		SET @WhereClause = @WhereClause + ' AND (RU.SSN = ''' + @SSN + ''')';
	END
	-- -- BirthDate
	IF (@BirthDate IS NOT NULL)
	BEGIN
		SET @WhereClause = @WhereClause + ' AND (RU.BirthDate = CONVERT(DATETIME, ''' + @BirthDate + ''', 102))';
	END
	
	-- Set the where clause
	SET @SQLStmt = REPLACE (@SQLStmt, '@WHERECLAUSE@', @WhereClause)
	
	PRINT @SQLStmt 

	-- Execute
	EXEC sp_executesql @SQLStmt 
END
GO

GRANT EXEC ON dbo.custRU_RecruitsLoadByRawSQL TO PUBLIC
GO