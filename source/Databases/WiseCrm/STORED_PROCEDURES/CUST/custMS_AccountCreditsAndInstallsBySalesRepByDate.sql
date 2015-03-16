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
**		Auth: Bob McFadden
**		Date: 02/16/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/16/2015	Bob McFadden	Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountCreditsAndInstallsBySalesRepByDate
(
	@OfficeID INT
	, @SalesRepId VARCHAR(20)
	, @begindate DATETIME
	, @enddate DATETIME
    , @GpEmployeeId VARCHAR(20)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT DISTINCT
			--ACI.LeadID
			ACI.OfficeID
			, ACI.OfficeName
			, ACI.TeamID
			, ACI.TeamName
			, ACI.SalesRepID
			, ACI.SalesRepName
			, ACI.Active
<<<<<<< HEAD
			, ISNULL(Installs_qry.NumInstalls,0) as NumInstalls
			, ISNULL(Credits_qry.NumCredits,0) as NumCredits
=======
			, COUNT(ACI.NumInstalls) AS NumInstalls
			, COUNT(ACI.NumCredits) AS NumCredits
			, ACI.InstallDate
			, ACI.CreditDate
>>>>>>> origin/master
		FROM
			[dbo].vwMS_AccountCreditsAndInstalls AS ACI

			/**********************
			***  INSTALLATIONS  ***
			***********************/
			LEFT JOIN 
			(
			SELECT 
				ACI.SalesRepID
				,COUNT(*) AS NumInstalls
			FROM
				dbo.vwMS_AccountCreditsAndInstalls as ACI
			WHERE
				(CONVERT(DATE,ACI.InstallDate) BETWEEN @begindate AND @enddate)
			GROUP BY
				ACI.SalesRepID
			) AS Installs_qry
			ON 
				ACI.SalesRepID = Installs_qry.SalesRepId

			/********************
			***  CREDITS RUN  ***
			*********************/
			LEFT JOIN
			(
			SELECT
				vwMS_AccountCreditsAndInstalls.SalesRepId
				, COUNT(*) AS NumCredits
				--, COUNT(DISTINCT QL_Leads.LeadID) AS NumCredits
			FROM
				vwMS_AccountCreditsAndInstalls
			WHERE 
				vwMS_AccountCreditsAndInstalls.CreditDate BETWEEN @begindate AND @enddate
			GROUP BY 
				vwMS_AccountCreditsAndInstalls.SalesRepId
			) AS Credits_qry
			ON ACI.SalesRepID = Credits_qry.SalesRepId
		WHERE
			(@OfficeID IS NULL OR ACI.OfficeID = @OfficeID)
			AND (ACI.CreditDate BETWEEN @begindate AND @enddate)
			--AND (
			--	ACI.Active = 'TRUE'
			--	OR
			--	(
			--	ACI.Active = 'FALSE' 
			--	AND (Installs_qry.NumInstalls > 0 OR Credits_qry.NumCredits > 0)
			--	)
			--)
		--ORDER BY 
		--	 RU_TeamLocations.Description
		--	,RU_Users.FullName

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_AccountCreditsAndInstallsBySalesRepByDate TO PUBLIC
GO

<<<<<<< HEAD
/** EXEC dbo.custMS_AccountCreditsAndInstallsBySalesRepByDate null, '03/01/15','03/31/15' */
=======
/** EXEC dbo.custMS_AccountCreditsAndInstallsBySalesRepByDate 1, null, '02/01/15','03/31/15', null */
>>>>>>> origin/master
