USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custReport_CreditAndInstalls')
	BEGIN
		PRINT 'Dropping Procedure custReport_CreditAndInstalls'
		DROP  Procedure  dbo.custReport_CreditAndInstalls
	END
GO

PRINT 'Creating Procedure custReport_CreditAndInstalls'
GO
/******************************************************************************
**		File: custReport_CreditAndInstalls.sql
**		Name: custReport_CreditAndInstalls
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
**		Date: 
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	
*******************************************************************************/
CREATE Procedure dbo.custReport_CreditAndInstalls
(
	@officeId INT
	, @startDate DATETIME
	, @endDate DATETIME
)
AS
BEGIN

	SELECT
		TL.TeamLocationID
		, RT.Description AS TeamName
		, TL.Description AS TeamLocation
		, RU.GPEmployeeId AS SalesRepID
		, RU.FullName as SalesRepName
		, CASE
			WHEN RU.IsActive = 'TRUE' THEN 'Active'
			ELSE 'Inactive'
		END AS ACTIVE
        , ISNULL(NContacts.NumContacts, 0) AS NumContacts
		, ISNULL(NCredits.NumCredits, 0) AS NumCredits
		, ISNULL(NInstalls.NumInstalls, 0) AS NumInstalls
	FROM 
		RU_Users AS RU
		INNER JOIN RU_Recruits AS RR
		ON
			(RU.UserID = RR.UserId)
			AND (RR.IsDeleted = 'FALSE')
		JOIN RU_Teams AS RT
		ON
			(RR.TeamId = RT.TeamID)
		JOIN RU_TeamLocations AS TL
		ON
			(RT.TeamLocationId = TL.TeamLocationID)
			AND (@officeId IS NULL OR @officeId = 0 OR TL.TeamLocationID = @officeId)
		LEFT OUTER JOIN (
			SELECT 
				SLC.RepCompanyID AS [SalesRepID]
				, COUNT(SLCN.ContactId) AS [NumContacts]
			FROM
				[NXSE_Sales].[dbo].[SL_Contacts] AS SLC WITH (NOLOCK)
				INNER JOIN [NXSE_Sales].[dbo].[SL_ContactNotes] AS SLCN WITH (NOLOCK)
				ON
					(SLCN.ContactId = SLC.ID)
			WHERE
				(SLCN.CreatedOn BETWEEN @startDate AND @endDate)
			GROUP BY
				SLC.RepCompanyID
		) AS NContacts
		ON
			(RU.GPEmployeeId = NContacts.SalesRepID)

		/**********************
		***  INSTALLATIONS  ***
		***********************/
		LEFT JOIN
		(
			SELECT
				ASI.SalesRepId AS SalesRepID
				, ASI.TeamLocationId
				, COUNT(*) AS NumInstalls
			FROM 
				WISE_CRM.dbo.vwMS_AccountSalesInformations AS ASI
				JOIN WISE_CRM.dbo.MS_Accounts AS A
				ON
					(ASI.AccountID = A.AccountID)
					AND (A.IsDeleted = 'FALSE')
			WHERE
				(CONVERT(DATE, ASI.InstallDate) BETWEEN @startDate AND @endDate)
			GROUP BY 
				ASI.SalesRepId
				, ASI.TeamLocationId
		) AS NInstalls
		ON
			RU.GPEmployeeId = NInstalls.SalesRepId

		/********************
		***  CREDITS RUN  ***
		*********************/
		LEFT JOIN
		(
			SELECT
				L.SalesRepId as SalesRepID
				, L.TeamLocationId
				, COUNT(DISTINCT CR.LeadId) AS NumCredits
			FROM 
				WISE_CRM.dbo.QL_CreditReports AS CR
				INNER JOIN WISE_CRM.dbo.QL_Leads AS L
				ON
					(CR.LeadId = L.LeadID)
					AND (L.IsDeleted = 'FALSE')
				JOIN
				(
					SELECT
						LeadId
						, MIN(CreatedOn) AS MinCreatedOn  -- This picks the first credit run.
					FROM
						WISE_CRM.dbo.QL_CreditReports
					WHERE
						(IsDeleted = 'FALSE')
					GROUP BY
						LeadId
				) AS MaxDate_qry
				ON
					(CR.LeadID = MaxDate_qry.LeadID)
					AND (CR.CreatedOn = MaxDate_qry.MinCreatedOn)
				WHERE
					(CR.CreatedOn BETWEEN @startDate AND @endDate)
					AND (CR.IsDeleted = 'FALSE')
				GROUP BY
					L.SalesRepId
					, L.TeamLocationId
		) AS NCredits
		ON
			(RU.GPEmployeeId = NCredits.SalesRepId)
	WHERE
		(RU.UserEmployeeTypeId = 'SALESREP')
		AND (RU.IsDeleted = 'FALSE')
		AND (
			RU.IsActive = 'TRUE'
			OR (NInstalls.NumInstalls > 0 OR NCredits.NumCredits > 0)
		)
	ORDER BY
		RT.Description
		, TL.Description
		, RU.FullName


END
GO

GRANT EXEC ON dbo.custReport_CreditAndInstalls TO PUBLIC
GO

/*

EXEC dbo.custReport_CreditAndInstalls @officeId=0, @startDate='2015-06-01 05:00:00', @endDate='1/2/2003'

*/
