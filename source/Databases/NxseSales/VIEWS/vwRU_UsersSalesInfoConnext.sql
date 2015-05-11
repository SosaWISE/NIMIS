USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwRU_UsersSalesInfoConnext')
	BEGIN
		PRINT 'Dropping VIEW vwRU_UsersSalesInfoConnext'
		DROP VIEW dbo.vwRU_UsersSalesInfoConnext
	END
GO

PRINT 'Creating VIEW vwRU_UsersSalesInfoConnext'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwRU_UsersSalesInfoConnext.sql
**		Name: vwRU_UsersSalesInfoConnext
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
**		Auth: Andres Sosa
**		Date: 02/05/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	02/05/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE VIEW [dbo].[vwRU_UsersSalesInfoConnext]
AS
	-- Enter Query here
	SELECT
		RU.UserID
		, RU.FirstName
		, RU.MiddleName
		, RU.LastName
		, '//www.wix.com/blog/wp-content/uploads/2013/05/Instagram_10_56.jpg' AS PhotoURL
		, CAST(1 AS BIGINT) AS MLMDepth
		, CAST(1 AS BIT) AS ManagerHasOwnTeam
		, S.RegionName as RegionName
		, S.OfficeName AS OfficeName
		, S.TeamName AS TeamName
		, CAST(129 AS BIGINT) AS CurrentNationalRank
		, CAST(136 AS BIGINT) AS PreviousNationalRank
		, CAST(11 AS BIGINT) AS CurrentRegionalRank
		, CAST(5 AS BIGINT) AS PreviousRegionalRank
		, CAST(7 AS BIGINT) AS CurrentOfficeRank
		, CAST(10 AS BIGINT) AS PreviousOfficeRank
		, CAST(3 AS BIGINT) AS CurrentTeamRank
		, CAST(5 AS BIGINT) AS PreviousTeamRank
		, CAST('1/2/14' AS datetime) AS StartDate
		, CASE 
			WHEN RU.CorporateEmail IS NOT NULL THEN RU.CorporateEmail 
			WHEN RU.Email IS NOT NULL THEN RU.Email
			ELSE ''
		  END AS Email
	FROM
		dbo.RU_Users AS RU WITH (NOLOCK)
		JOIN dbo.RU_Salesperson as S WITH (NOLOCK)
			ON RU.UserId = S.UserID

GO
/* TEST */
-- SELECT * FROM vwRU_UsersSalesInfoConnext
