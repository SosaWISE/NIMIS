USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_SeasonGetAllSeasonsForAUserByUserID')
	BEGIN
		PRINT 'Dropping Procedure custRU_SeasonGetAllSeasonsForAUserByUserID'
		DROP  Procedure  dbo.custRU_SeasonGetAllSeasonsForAUserByUserID
	END
GO

PRINT 'Creating Procedure custRU_SeasonGetAllSeasonsForAUserByUserID'
GO
/******************************************************************************
**		File: custRU_SeasonGetAllSeasonsForAUserByUserID.sql
**		Name: custRU_SeasonGetAllSeasonsForAUserByUserID
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
CREATE Procedure dbo.custRU_SeasonGetAllSeasonsForAUserByUserID
(
	@UserID INT
)
AS
BEGIN
	-- Select Code
	SELECT
		RS.*
	FROM
		RU_Season AS RS WITH (NOLOCK)
		INNER JOIN RU_Recruits AS RR WITH (NOLOCK)
		ON
			RS.SeasonID = RR.SeasonID
			AND (RR.UserID = @UserID)
END
GO

GRANT EXEC ON dbo.custRU_SeasonGetAllSeasonsForAUserByUserID TO PUBLIC
GO