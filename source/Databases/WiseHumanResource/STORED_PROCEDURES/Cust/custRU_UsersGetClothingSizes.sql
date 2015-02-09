USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetClothingSizes')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetClothingSizes'
		DROP  Procedure  dbo.custRU_UsersGetClothingSizes
	END
GO

PRINT 'Creating Procedure custRU_UsersGetClothingSizes'
GO
/******************************************************************************
**		File: custRU_UsersGetClothingSizes.sql
**		Name: custRU_UsersGetClothingSizes
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
CREATE Procedure dbo.custRU_UsersGetClothingSizes
(@SeasonID INT
	, @SizeType NVARCHAR(10)
	, @IsSales BIT
	, @IsTech BIT
	, @IsFemale BIT
	, @IsMale BIT
)
AS
BEGIN


--	DECLARE @SeasonID INT
--	SET @SeasonID = 6
--
--	DECLARE @SizeType NVARCHAR(10)
--	SET @SizeType = 'Hats'
--
--	DECLARE @IsSales BIT
--	SET @IsSales = 1
--
--	DECLARE @IsTech BIT
--	SET @IsTech = 0
--
--	DECLARE @IsFemale BIT
--	SET @IsFemale = 1
--
--	DECLARE @IsMale BIT
--	SET @IsMale = 0




	DECLARE @ShirtSizes TABLE
	(
		SizeID INT
		, SizeText NVARCHAR(25)
	)
	INSERT INTO @ShirtSizes VALUES (1, 'XXS')
	INSERT INTO @ShirtSizes VALUES (2, 'XS')
	INSERT INTO @ShirtSizes VALUES (3, 'S')
	INSERT INTO @ShirtSizes VALUES (4, 'M')
	INSERT INTO @ShirtSizes VALUES (5, 'L')
	INSERT INTO @ShirtSizes VALUES (6, 'XL')
	INSERT INTO @ShirtSizes VALUES (7, 'XXL')
	INSERT INTO @ShirtSizes VALUES (8, 'XXXL')


	DECLARE @HatSizes TABLE
	(
		SizeID INT
		, SizeText NVARCHAR(25)
	)
	INSERT INTO @HatSizes VALUES (1, 'S')
	INSERT INTO @HatSizes VALUES (2, 'M')
	INSERT INTO @HatSizes VALUES (3, 'L')

	DECLARE @Genders TABLE
	(
		ID INT
		, Sex NVARCHAR(25)
	)
	INSERT INTO @Genders VALUES (1, 'Male')
	INSERT INTO @Genders VALUES (2, 'Female')


	DECLARE @Recruits TABLE
	(
		OfficeName NVARCHAR(200)
		, TeamName NVARCHAR(200)
		, TeamID INT
		, ManagerFullName NVARCHAR(200)
		, ManagerUserID INT
		, RecruitName NVARCHAR(200)
		, UserID INT
		, ShirtSize INT
		, HatSize INT
		, Sex INT
		, SexText NVARCHAR(25)
	)
	INSERT INTO @Recruits
	SELECT
		RUTL.Description
		, RT.Description
		, Tree.TeamID
		, Manager.ManagerFullName
		, Manager.ManagerUserID
		, RU.FullName AS RecruitName
		, RU.UserID INT
		, RU.ShirtSize
		, RU.HatSize
		, RU.Sex
		, G.Sex
	FROM GetReportingTree('ReportingLevel', NULL, NULL, NULL, @SeasonID) AS Tree
	--Team
	LEFT OUTER JOIN RU_Teams AS RT WITH(NOLOCK)
	ON
		Tree.TeamID = RT.TeamID
	LEFT OUTER JOIN RU_TeamLocations AS RUTL WITH(NOLOCK)
	ON
		RT.TeamLocationID = RUTL.TeamLocationID

	--Recruit
	INNER JOIN RU_Users AS RU WITH(NOLOCK)
	ON
		Tree.UserID = RU.UserID
		AND (RU.IsDeleted = 0)
	INNER JOIN RU_Recruits AS RR WITH(NOLOCK)
	ON
		(RU.UserID = RR.UserID)
		AND (RR.IsDeleted = 0)
		AND (RR.SeasonID = @SeasonID)
	INNER JOIN RU_UserType AS RUT WITH(NOLOCK)
	ON
		RR.UserTypeID = RUT.UserTypeID
	INNER JOIN @Genders AS G
	ON
		RU.Sex = G.ID

	--Manager
	LEFT OUTER JOIN
	(
		SELECT
			RR.RecruitID
			, RU.UserID AS ManagerUserID
			, RU.FullName AS ManagerFullName
		FROM RU_Recruits AS RR WITH(NOLOCK)
		INNER JOIN RU_Users AS RU WITH(NOLOCK)
		ON
			RR.UserID = RU.UserID
	) AS Manager
	ON
		Tree.ReportsToID = Manager.RecruitID
	WHERE
		(
			((@IsSales = 1) AND (RUT.RoleLocationID = 1))
			OR
			((@IsTech = 1) AND (RUT.RoleLocationID = 2))
		)
		AND
		(
			((@IsMale = 1) AND (RU.Sex = 1))
			OR
			((@IsFemale = 1) AND (RU.Sex = 2))
		)




	IF @SizeType = 'Hats'
	BEGIN
		--Hat Sizes
		SELECT
			Rec.OfficeName
			, Rec.TeamName
			, Rec.TeamID
			, Rec.ManagerFullName
			, Rec.ManagerUserID
			, Rec.RecruitName
			, Rec.UserID
			, Sz.SizeText AS Size
		FROM @Recruits AS Rec
		INNER JOIN @HatSizes AS Sz
		ON
			Rec.HatSize = Sz.SizeID
		ORDER BY
			OfficeName
			, TeamName
			, ManagerFullName
			, Rec.ShirtSize
	END
	ELSE
		--Shirt Sizes
		SELECT
			Rec.OfficeName
			, Rec.TeamName
			, Rec.TeamID
			, Rec.ManagerFullName
			, Rec.ManagerUserID
			, Rec.RecruitName
			, Rec.UserID
			, Sz.SizeText AS Size
		FROM @Recruits AS Rec
		INNER JOIN @ShirtSizes AS Sz
		ON
			Rec.ShirtSize = Sz.SizeID
		ORDER BY
			OfficeName
			, TeamName
			, ManagerFullName
			, Rec.ShirtSize

END
GO

GRANT EXEC ON dbo.custRU_UsersGetClothingSizes TO PUBLIC
GO