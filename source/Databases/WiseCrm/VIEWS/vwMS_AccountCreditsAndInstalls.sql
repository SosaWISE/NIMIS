USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountCreditsAndInstalls')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountCreditsAndInstalls'
		DROP VIEW dbo.vwMS_AccountCreditsAndInstalls
	END
GO

PRINT 'Creating VIEW vwMS_AccountCreditsAndInstalls'
GO

/****** Object:  View [dbo].[vwMS_AccountCreditsAndInstalls]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountCreditsAndInstalls.sql
**		Name: vwMS_AccountCreditsAndInstalls
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
**		Date: 03/16/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/16/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountCreditsAndInstalls]
AS
		SELECT 
			Credits_qry.LeadID
			, RU_TeamLocations.TeamLocationID AS [OfficeID]
			, RU_TeamLocations.Description AS [OfficeName]
			, RU_Teams.TeamID
			, RU_Teams.Description AS TeamName
			, RU_Users.GPEmployeeId AS SalesRepID
			, RU_Users.FullName as SalesRepName
			, CASE
				WHEN RU_Users.IsActive = 'TRUE' THEN 'Active'
				ELSE 'Inactive'
			  END AS Active
			, CAST(NULL AS INT) AS NumInstalls
			, CAST(NULL AS INT) AS NumCredits
			, Installs_qry.InstallDate
			, Credits_qry.CreditDate
		FROM
			-- RU_Users
			RU_Users WITH(NOLOCK)

			-- RU_Recruits
			JOIN RU_Recruits WITH(NOLOCK)
			ON
				RU_Users.UserID = RU_Recruits.UserId
				AND RU_Recruits.IsDeleted = 'FALSE'

			-- RU_Teams
			JOIN RU_Teams WITH(NOLOCK)
			ON
				RU_Recruits.TeamId = RU_Teams.TeamID
			JOIN RU_TeamLocations WITH (NOLOCK)
			ON
				(RU_TeamLocations.TeamLocationID = RU_Teams.TeamLocationId)
--				AND (@OfficeID IS NULL OR RU_TeamLocations.TeamLocationID = @OfficeID)

			/**********************
			***  INSTALLATIONS  ***
			***********************/
			LEFT JOIN 
			(
			SELECT
				vwMS_AccountSalesInformations.SalesRepId,
				vwMS_AccountSalesInformations.AccountID
				, vwMS_AccountSalesInformations.InstallDate
				-- ,COUNT(*) AS NumInstalls
			FROM
				-- MC_Customers
				dbo.MC_Accounts WITH(NOLOCK)

				-- MS_AccountCustomers
				JOIN dbo.AE_CustomerAccounts WITH(NOLOCK)
				ON
					MC_Accounts.AccountID = AE_CustomerAccounts.AccountId

				--MS_Accounts
				JOIN dbo.MS_Accounts WITH(NOLOCK)
				ON
					AE_CustomerAccounts.AccountId = MS_Accounts.AccountID
					AND MS_Accounts.IsDeleted = 'FALSE'
					AND MS_Accounts.ContractId IS NOT NULL

				-- MS_AccountSalesInformations
				--JOIN dbo.MS_AccountSalesInformations WITH(NOLOCK)
					--ON MS_Accounts.AccountID = MS_AccountSalesInformations.AccountID
				JOIN vwMS_AccountSalesInformations
				ON
					MS_Accounts.AccountID = vwMS_AccountSalesInformations.AccountID
					AND vwMS_AccountSalesInformations.InstallDate IS NOT NULL
			--WHERE
			--	(CONVERT(DATE,vwMS_AccountSalesInformations.InstallDate) BETWEEN @begindate AND @enddate)
			--GROUP BY
			--	vwMS_AccountSalesInformations.SalesRepId
			) AS Installs_qry
			ON RU_Users.GPEmployeeId = Installs_qry.SalesRepId

			/********************
			***  CREDITS RUN  ***
			*********************/
			LEFT JOIN
			(
			SELECT
				QL_Leads.SalesRepId
				, QL_Leads.LeadID
				, MIN(dbo.QL_CreditReports.CreatedOn) AS [CreditDate]
				--, COUNT(*)
				-- , COUNT(DISTINCT QL_Leads.LeadID) AS NumCredits
			FROM

				-- QL_CreditReports
				dbo.QL_CreditReports WITH(NOLOCK)

				-- QL_Leads
				JOIN dbo.QL_Leads WITH(NOLOCK)
					ON QL_CreditReports.LeadId = QL_Leads.LeadID
					AND QL_Leads.IsDeleted = 'FALSE'
			WHERE 
				QL_CreditReports.IsDeleted = 'FALSE'
				-- AND QL_CreditReports.CreatedOn BETWEEN @begindate AND @enddate
			GROUP BY 
				QL_Leads.SalesRepId
				, QL_Leads.LeadID
			) AS Credits_qry
			ON RU_Users.GPEmployeeId = Credits_qry.SalesRepId
		WHERE
			RU_Users.UserEmployeeTypeId = 'SALESREP'
			AND RU_Users.IsDeleted = 'FALSE'
			--AND 
			--(
				--RU_Users.IsActive = 'TRUE'
				--OR
				--(
				--	RU_Users.IsActive = 'FALSE' 
				--	AND (Installs_qry.NumInstalls > 0 OR Credits_qry.NumCredits > 0)
				--)
			--)
		--ORDER BY 
		--	 RU_TeamLocations.Description
		--	,RU_Users.FullName

GO
/* TEST */
-- SELECT * FROM vwMS_AccountCreditsAndInstalls
