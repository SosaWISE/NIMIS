USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountCreditsAndInstallsBySalesRepByDate')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountCreditsAndInstallsBySalesRepByDate'
		DROP  Procedure  dbo.custMS_AccountCreditsAndInstallsBySalesRepByDate
	END
GO

PRINT 'Creating Procedure custMS_AccountCreditsAndInstallsBySalesRepByDate'
GO
/******************************************************************************
**		File: custMS_AccountCreditsAndInstallsBySalesRepByDate.sql
**		Name: custMS_AccountCreditsAndInstallsBySalesRepByDate
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
**		Auth: Andres Sosa
**		Date: 02/13/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/13/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountCreditsAndInstallsBySalesRepByDate
(
	@begindate datetime,
	@enddate datetime
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT
			 RU_Teams.Description as TeamName
			 ,RU_Users.GPEmployeeId AS SalesRepID
			,RU_Users.FullName as SalesRepName
			,CASE
				WHEN RU_Users.IsActive = 'TRUE' THEN 'Active'
				ELSE 'Inactive'
			END AS Active
			,ISNULL(Installs_qry.NumInstalls,0) AS NumInstalls
			,ISNULL(Credits_qry.NumCredits,0) AS NumCredits
		FROM
			-- RU_Users
			WISE_HumanResource.dbo.RU_Users WITH(NOLOCK)

			-- RU_Recruits
			JOIN WISE_HumanResource.dbo.RU_Recruits WITH(NOLOCK)
				ON RU_Users.UserID = RU_Recruits.UserId
				AND RU_Recruits.IsDeleted = 'FALSE'

			-- RU_Teams
			JOIN WISE_HumanResource.dbo.RU_Teams WITH(NOLOCK)
				ON RU_Recruits.TeamId = RU_Teams.TeamID

			/**********************
			***  INSTALLATIONS  ***
			***********************/
			LEFT JOIN 
			(
			SELECT 
				vwMS_AccountSalesInformations.SalesRepId
				,COUNT(*) AS NumInstalls
			FROM
				-- MC_Customers
				WISE_CRM.dbo.MC_Accounts WITH(NOLOCK)

				-- MS_AccountCustomers
				JOIN WISE_CRM.dbo.MS_AccountCustomers WITH(NOLOCK)
					ON MC_Accounts.AccountID = MS_AccountCustomers.AccountId

				--MS_Accounts
				JOIN WISE_CRM.dbo.MS_Accounts WITH(NOLOCK)
					ON MS_AccountCustomers.AccountId = MS_Accounts.AccountID
					AND MS_Accounts.IsDeleted = 'FALSE'
					AND MS_Accounts.ContractId IS NOT NULL

				-- MS_AccountSalesInformations
				--JOIN WISE_CRM.dbo.MS_AccountSalesInformations WITH(NOLOCK)
					--ON MS_Accounts.AccountID = MS_AccountSalesInformations.AccountID
				JOIN vwMS_AccountSalesInformations
					ON MS_Accounts.AccountID = vwMS_AccountSalesInformations.AccountID

			WHERE
				(CONVERT(DATE,vwMS_AccountSalesInformations.InstallDate) BETWEEN @begindate AND @enddate)
			GROUP BY
				vwMS_AccountSalesInformations.SalesRepId
			) AS Installs_qry
			ON RU_Users.GPEmployeeId = Installs_qry.SalesRepId

			/********************
			***  CREDITS RUN  ***
			*********************/
			LEFT JOIN
			(
			SELECT 
				QL_Leads.SalesRepId
				, COUNT(*) AS NumCredits
			FROM

				-- QL_CreditReports
				WISE_CRM.dbo.QL_CreditReports WITH(NOLOCK)

				-- QL_Leads
				JOIN WISE_CRM.dbo.QL_Leads WITH(NOLOCK)
					ON QL_CreditReports.LeadId = QL_Leads.LeadID
					AND QL_Leads.IsDeleted = 'FALSE'
			WHERE 
				QL_CreditReports.CreatedOn BETWEEN @begindate AND @enddate
				AND QL_CreditReports.IsDeleted = 'FALSE'
			GROUP BY 
				QL_Leads.SalesRepId
			) AS Credits_qry
			ON RU_Users.GPEmployeeId = Credits_qry.SalesRepId
		WHERE
			RU_Users.UserEmployeeTypeId = 'SALESREP'
			AND RU_Users.IsDeleted = 'FALSE'
			AND 
			(
				RU_Users.IsActive = 'TRUE'
				OR
				(
					RU_Users.IsActive = 'FALSE' 
					AND (Installs_qry.NumInstalls > 0 OR Credits_qry.NumCredits > 0)
				)
			)
		ORDER BY 
			 RU_Teams.Description
			,RU_Users.FullName

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_AccountCreditsAndInstallsBySalesRepByDate TO PUBLIC
GO

/** EXEC dbo.custMS_AccountCreditsAndInstallsBySalesRepByDate '02/01/15','03/31/15' */