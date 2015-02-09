USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsGetOfficesForActiveSeasons')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsGetOfficesForActiveSeasons'
		DROP  Procedure  dbo.custRU_TeamLocationsGetOfficesForActiveSeasons
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsGetOfficesForActiveSeasons'
GO
/******************************************************************************
**		File: custRU_TeamLocationsGetOfficesForActiveSeasons.sql
**		Name: custRU_TeamLocationsGetOfficesForActiveSeasons
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
CREATE Procedure dbo.custRU_TeamLocationsGetOfficesForActiveSeasons
AS
BEGIN

	SELECT
		TL.*
	FROM
		RU_TeamLocations AS TL WITH (NOLOCK)
		INNER JOIN RU_Season AS RS WITH (NOLOCK)	
		ON
			(TL.SeasonID = RS.SeasonID)
	WHERE
		(RS.IsActive = 1)
		AND (RS.IsDeleted = 0)
		AND (TL.IsActive = 1)
		AND (TL.IsDeleted = 0)
	ORDER BY
		RS.SeasonName
		, TL.Description
		
	
END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsGetOfficesForActiveSeasons TO PUBLIC
GO