USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetCompetitiveRankings')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetCompetitiveRankings'
		DROP  Procedure  dbo.custRU_RecruitsGetCompetitiveRankings
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetCompetitiveRankings'
GO
/******************************************************************************
**		File: custRU_RecruitsGetCompetitiveRankings.sql
**		Name: custRU_RecruitsGetCompetitiveRankings
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
CREATE Procedure dbo.custRU_RecruitsGetCompetitiveRankings
(
	@UserID INT
	, @StartDate DateTime
	, @EndDate DateTime
	, @SeasonIDList NVARCHAR(MAX)
	, @RoleLocationID INT
)
WITH RECOMPILE
AS
BEGIN


	DECLARE @Rankings TABLE(Rank INT, Accounts INT, FullName NVARCHAR(101), PublicFullName NVARCHAR(101), UserID INT, Description NVARCHAR(50), TeamName NVARCHAR(50), OfficeName NVARCHAR(50)) 
	INSERT INTO @Rankings
	EXEC custRU_RecruitsGetRankings @StartDate, @EndDate, @SeasonIDList, @RoleLocationID, NULL, NULL, NULL

	DECLARE @RowIndex INT
	SELECT
	@RowIndex = RowIndex
	FROM
	(
		SELECT
		ROW_NUMBER() OVER (ORDER BY [Rank], [FullName]) AS RowIndex
		, UserID
		FROM @Rankings
	) AS Rankings
	WHERE
		UserID = @UserID
	--SELECT @RowIndex



	--RESULTS

	--Self
	SELECT
	*
	FROM
	(
		SELECT
		ROW_NUMBER() OVER (ORDER BY [Rank], [FullName]) AS RowIndex
		, *
		FROM @Rankings
	) AS Rankings
	WHERE
		(RowIndex = @RowIndex)
		
		
	--One Up
	SELECT
	*
	FROM
	(
		SELECT
		ROW_NUMBER() OVER (ORDER BY [Rank], [FullName]) AS RowIndex
		, *
		FROM @Rankings
	) AS Rankings
	WHERE
		(RowIndex = (@RowIndex - 1))

		
	--One Down
	SELECT
	*
	FROM
	(
		SELECT
		ROW_NUMBER() OVER (ORDER BY [Rank], [FullName]) AS RowIndex
		, *
		FROM @Rankings
	) AS Rankings
	WHERE
		(RowIndex = (@RowIndex + 1))


END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetCompetitiveRankings TO PUBLIC
GO