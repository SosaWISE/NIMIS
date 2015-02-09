USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetServicePercent')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetServicePercent'
		DROP  Procedure  dbo.custRU_TeamsGetServicePercent
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetServicePercent'
GO
/******************************************************************************
**		File: custRU_TeamsGetServicePercent.sql
**		Name: custRU_TeamsGetServicePercent
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
CREATE Procedure dbo.custRU_TeamsGetServicePercent
(
	@SeasonID INT
	, @TeamIDList NVARCHAR(MAX) = NULL
	, @RecruitIDList NVARCHAR(MAX) = NULL
)
AS
BEGIN



	DECLARE @StartDate DATETIME
	DECLARE @EndDate DATETIME

	SELECT
		@StartDate = StartDate
		, @EndDate = EndDate
	FROM RU_Season AS RS
	WHERE
		RS.SeasonID = @SeasonID
	--SELECT @StartDate, @EndDate

	SELECT
		TicketInfo.TicketID 
		, TicketInfo.IsCountedAgainstTech
		, RecUser.GPEmployeeID AS CompanyID
		, RecUser.UserID
		, RecUser.FullName
		, RecUser.PublicFullName
		, RUTL.Description AS [Office]
		, RT.Description AS [Team]
		, TicketInfo.ServiceTicketCreatedDate
		, AI.AccountID
		, ML.FirstName + ' ' + ML.LastName AS CustomerName
		, AI.InstallDate
		, TicketInfo.NoteForTech
		, TicketInfo.ServiceTicketTypeName
		, Ranks.CompanyRank
		, RT.TeamID
	FROM VW_RecruitUser AS RecUser
	INNER JOIN SAE_RecruitTeamMappings AS RTM
	ON
		RecUser.RecruitID = RTM.RecruitID
	INNER JOIN RU_Teams AS RT
	ON
		RTM.TeamID = RT.TeamID
	INNER JOIN RU_TeamLocations AS RUTL
	ON
		RT.TeamLocationID = RUTL.TeamLocationID
	INNER JOIN SAE_AccountsInstalled AS AI
	ON
		RecUser.UserID = AI.TechnicianUserID
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_Account AS MSA
	ON
		AI.AccountID = MSA.AccountID
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MC_Lead AS ML
	ON
		MSA.Customer1ID = ML.LeadID
	LEFT JOIN
	(
		SELECT
			TIC.AccountID
			, TIC.TicketID
			, STIC.NoteForTech
			, STIC.IsCountedAgainstTech
			, ST.ServiceTicketTypeName
			, STIC.CreatedOn AS ServiceTicketCreatedDate
		FROM Platinum_Protection_InterimCRM.dbo.TS_Ticket AS TIC
		INNER JOIN Platinum_Protection_InterimCRM.dbo.TS_ServiceTickets AS STIC
		ON
			TIC.TicketID = STIC.TicketID
		INNER JOIN Platinum_Protection_InterimCRM.dbo.TS_ServiceTicketTypes AS ST
		ON
			STIC.ServiceTicketTypeID = ST.ServiceTicketTypeID
		WHERE
			(STIC.TicketID IS NOT NULL OR TIC.TicketID IS NULL)
	) AS TicketInfo
	ON
		AI.AccountID = TicketInfo.AccountID
	LEFT JOIN 
	(
		SELECT
			RU.UserID
			, ROW_NUMBER() OVER (ORDER BY SUM(ISNULL(CONVERT(TINYINT, STIC.IsCountedAgainstTech), 0)) / CONVERT(REAL, COUNT(DISTINCT AI.AccountID))
			, COUNT(DISTINCT AI.AccountID) DESC
		) AS CompanyRank
		FROM RU_Users AS RU
		INNER JOIN SAE_AccountsInstalled AS AI
		ON
			RU.UserID = AI.TechnicianUserID
		LEFT JOIN Platinum_Protection_InterimCRM.dbo.TS_Ticket AS TIC
		ON
			AI.AccountID = TIC.AccountID
		LEFT JOIN Platinum_Protection_InterimCRM.dbo.TS_ServiceTickets AS STIC
		ON
			TIC.TicketID = STIC.TicketID
		WHERE
			(AI.InstallDate BETWEEN @StartDate AND @EndDate)
			AND (STIC.TicketID IS NOT NULL OR TIC.TicketID IS NULL)
		GROUP BY
			RU.UserID
	) Ranks
	ON
		RecUser.UserID = Ranks.UserID
	WHERE
		RecUser.SeasonID = @SeasonID
		AND (@TeamIDList IS NULL OR RTM.TeamID IN (SELECT ID FROM dbo.SplitIntList(@TeamIDList)))
		AND (@RecruitIDList IS NULL OR RecUser.RecruitID IN (SELECT ID FROM dbo.SplitIntList(@RecruitIDList)))
		
		AND (AI.InstallDate BETWEEN @StartDate AND @EndDate)

	ORDER BY
		RecUser.PublicFullName
		, AI.AccountID
		, TicketInfo.ServiceTicketCreatedDate



END
GO

GRANT EXEC ON dbo.custRU_TeamsGetServicePercent TO PUBLIC
GO