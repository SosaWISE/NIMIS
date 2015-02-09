USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamLocationsCopyOfficeStateMappings')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamLocationsCopyOfficeStateMappings'
		DROP  Procedure  dbo.custRU_TeamLocationsCopyOfficeStateMappings
	END
GO

PRINT 'Creating Procedure custRU_TeamLocationsCopyOfficeStateMappings'
GO
/******************************************************************************
**		File: custRU_TeamLocationsCopyOfficeStateMappings.sql
**		Name: custRU_TeamLocationsCopyOfficeStateMappings
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
CREATE Procedure dbo.custRU_TeamLocationsCopyOfficeStateMappings
(
	@FromTeamLocationID INT
	, @ToTeamLocationID INT
	, @SeasonID INT
)
AS
BEGIN


	INSERT INTO RU_TeamLocationStateMappings (TeamLocationId, SeasonId, StateId)
	SELECT
		@ToTeamLocationID
		, @SeasonID
		, StateId
	FROM RU_TeamLocationStateMappings
	WHERE
		TeamLocationID = @FromTeamLocationID


END
GO

GRANT EXEC ON dbo.custRU_TeamLocationsCopyOfficeStateMappings TO PUBLIC
GO