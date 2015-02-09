USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersFindUsers')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersFindUsers'
		DROP  Procedure  dbo.custRU_UsersFindUsers
	END
GO

PRINT 'Creating Procedure custRU_UsersFindUsers'
GO
/******************************************************************************
**		File: custRU_UsersFindUsers.sql
**		Name: custRU_UsersFindUsers
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
CREATE Procedure dbo.custRU_UsersFindUsers
(
	@Top INT = 5
	, @FirstName NVARCHAR(50) = NULL
	, @LastName NVARCHAR(50) = NULL
	, @CompanyID NVARCHAR(25) = NULL
	, @SSN NVARCHAR(50) = NULL
	, @PhoneCell NVARCHAR(25) = NULL
	, @PhoneHome NVARCHAR(25) = NULL
	, @Email NVARCHAR(100) = NULL
	, @UserName NVARCHAR(50) = NULL
	, @UserID INT = NULL
	, @UserEmployeeTypeID NVARCHAR(25) = NULL
	, @RecruitID INT = NULL
	, @SeasonID INT = NULL
	, @UserTypeID SMALLINT = NULL
)
AS
BEGIN

	--DECLARE @FirstName NVARCHAR(50)
	--DECLARE @LastName NVARCHAR(50)
	--DECLARE @CompanyID NVARCHAR(25)
	--DECLARE @SSN NVARCHAR(50)
	--DECLARE @PhoneCell NVARCHAR(25)
	--DECLARE @PhoneHome NVARCHAR(25)
	--DECLARE @Email NVARCHAR(100)
	--DECLARE @UserName NVARCHAR(50)
	--DECLARE @UserID INT
	--DECLARE @RecruitID INT
	--DECLARE @SeasonID INT
	--DECLARE @UserTypeID SMALLINT
	
	--SET @FirstName = 'aa%'
	--SET @LastName = NULL
	--SET @CompanyID = NULL
	--SET @SSN = NULL
	--SET @PhoneCell = NULL
	--SET @PhoneHome = NULL
	--SET @UserName = NULL
	--SET @UserID = NULL
	--SET @RecruitID = NULL
	--SET @SeasonID = NULL
	--SET @UserTypeID = NULL
	

	SELECT TOP 300
		RU.*
	FROM RU_Users AS RU WITH(NOLOCK)
	INNER JOIN
	(
		SELECT
			RU.UserID
			, ROW_NUMBER() OVER(ORDER BY RU.FullName ASC) AS Row
		FROM RU_Users AS RU WITH(NOLOCK)
		WHERE
			RU.UserID IN
			(
				SELECT DISTINCT
					RU.UserID
				FROM RU_Users AS RU WITH(NOLOCK)
				LEFT OUTER JOIN RU_Recruits AS RR WITH(NOLOCK)
				ON
					RU.UserID = RR.UserID
				WHERE
					--User
					(@FirstName IS NULL OR ((RU.FirstName LIKE @FirstName) OR (RU.PreferredName LIKE @FirstName)))
					AND (@LastName IS NULL OR (RU.LastName LIKE @LastName))
					AND (@CompanyID IS NULL OR (RU.GPEmployeeID LIKE @CompanyID))
					AND (@SSN IS NULL OR (RU.SSN = @SSN))
					AND (@PhoneCell IS NULL OR (RU.PhoneCell LIKE @PhoneCell))
					AND (@PhoneHome IS NULL OR (RU.PhoneHome LIKE @PhoneHome))
					AND (@Email IS NULL OR ((RU.Email LIKE @Email) OR (RU.CorporateEmail LIKE @Email)))
					AND (@UserName IS NULL OR (RU.UserName LIKE @UserName))
					AND (@UserID IS NULL OR (RU.UserID = @UserID))
					AND (@UserEmployeeTypeID IS NULL OR (RU.UserEmployeeTypeId = @UserEmployeeTypeID))
					--Recruit
					AND (@RecruitID IS NULL OR (RR.RecruitID = @RecruitID))
					AND (@SeasonID IS NULL OR (RR.SeasonID = @SeasonID))
					AND (@UserTypeID IS NULL OR (RR.UserTypeID = @UserTypeID))
			)
	) AS Results
	ON
		RU.UserID = Results.UserID
	WHERE
		(@Top IS NULL OR Results.Row <= @Top)
	ORDER BY
		RU.FullName ASC


END
GO

GRANT EXEC ON dbo.custRU_UsersFindUsers TO PUBLIC
GO