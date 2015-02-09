USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsShowSeasonStatusesByUserId')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsShowSeasonStatusesByUserId'
		DROP  Procedure  dbo.custRU_RecruitsShowSeasonStatusesByUserId
	END
GO

PRINT 'Creating Procedure custRU_RecruitsShowSeasonStatusesByUserId'
GO
/******************************************************************************
**		File: custRU_RecruitsShowSeasonStatusesByUserId.sql
**		Name: custRU_RecruitsShowSeasonStatusesByUserId
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
CREATE Procedure dbo.custRU_RecruitsShowSeasonStatusesByUserId
(@UserID INT
)
AS
BEGIN
	/* Build Query*/
	SELECT
		RR.RecruitId
		, RS.SeasonName AS [Season Name]
		, RR.IsActive
		, RR.IsDeleted
--		, RR.ModifiedBy
--		, RR.ModifiedOn
--		, RR.CreatedBy
--		, RR.CreatedOn
	FROM
		RU_Recruits AS RR WITH (NOLOCK)
		INNER JOIN RU_Season AS RS WITH (NOLOCK)
		ON
			(RR.SeasonId = RS.SeasonID)
	WHERE
		(RR.UserID = @UserID)

END
GO

GRANT EXEC ON dbo.custRU_RecruitsShowSeasonStatusesByUserId TO PUBLIC
GO