USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsFindOffices')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsFindOffices'
		DROP  Procedure  dbo.custRU_TeamLocationsFindOffices
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsFindOffices'
GO
/******************************************************************************
**		File: custRU_TeamLocationsFindOffices.sql
**		Name: custRU_TeamLocationsFindOffices
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
CREATE Procedure dbo.custRU_TeamLocationsFindOffices
(
	@Top INT = NULL
	, @TeamLocationID INT = NULL
	, @OfficeName VARCHAR(50) = NULL
	, @SeasonID INT = NULL
	, @SeasonName NVARCHAR(50) = NULL
	, @MarketID INT = NULL
	, @MarketName NVARCHAR(50) = NULL
	, @City VARCHAR(50) = NULL
	, @StateAB CHAR(2) = NULL
	, @TimeZoneID INT = NULL
	, @TimeZoneName VARCHAR(50) = NULL
)
AS
BEGIN

	SELECT
		RT.*
	FROM vwRU_TeamLocation AS RT WITH(NOLOCK)
	INNER JOIN
	(
		SELECT
			RR.TeamLocationID
			, ROW_NUMBER() OVER(ORDER BY RR.[Description]) AS Row			
--			, ROW_NUMBER() OVER(ORDER BY RU.FullName ASC) AS Row
		FROM 
			vwRU_TeamLocation AS RR WITH(NOLOCK)
		WHERE
			(@TeamLocationID IS NULL OR (RR.TeamLocationID = @TeamLocationID))
			AND (@OfficeName IS NULL OR (RR.[Description] LIKE @OfficeName))
			AND (@SeasonID IS NULL OR (RR.SeasonID = @SeasonID))
			AND (@SeasonName IS NULL OR (RR.SeasonName LIKE @SeasonName))
			AND (@MarketID IS NULL OR (RR.MarketID = @MarketID))
			AND (@MarketName IS NULL OR (RR.MarketName LIKE @MarketName))
			AND (@City IS NULL OR (RR.City LIKE @City ))
			AND (@StateAB IS NULL OR (RR.StateAB LIKE @StateAB ))
			AND (@TimeZoneID IS NULL OR (RR.TimeZoneID = @TimeZoneID))
			AND (@TimeZoneName IS NULL OR (RR.TimeZoneName LIKE @TimeZoneName))

	) AS Results
	ON
		RT.TeamLocationID= Results.TeamLocationID
	WHERE
		(@Top IS NULL OR Results.Row <= @Top)
	ORDER BY
		RT.[Description] ASC


END

GO

GRANT EXEC ON dbo.custRU_TeamLocationsFindOffices TO PUBLIC
GO