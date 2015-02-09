USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetRecruitSeasonSalesTotals')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetRecruitSeasonSalesTotals'
		DROP  Procedure  dbo.custRU_RecruitsGetRecruitSeasonSalesTotals
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetRecruitSeasonSalesTotals'
GO
/******************************************************************************
**		File: custRU_RecruitsGetRecruitSeasonSalesTotals.sql
**		Name: custRU_RecruitsGetRecruitSeasonSalesTotals
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
CREATE Procedure dbo.custRU_RecruitsGetRecruitSeasonSalesTotals
(
	@GPEmployeeID NVarChar(25)
	, @SeasonID INT
)
AS
BEGIN
	
	--DECLARE @GPEmployeeID NVarChar(25)
	--SET @GPEmployeeID = 'ELDR004'--'DAHL001'
	--
	--DECLARE @SeasonID INT
	--SET @SeasonID = 7


	DECLARE @UserID INT
	SELECT @UserID = UserID FROM RU_Users WHERE GPEmployeeID = @GPEmployeeID

	DECLARE @RoleLocationID NVarChar(25)
	SET @RoleLocationID = dbo.EmployeeRoleLocation(@GPEmployeeID, @SeasonID)

	DECLARE @PassNum INT
	SET @PassNum = 600	
			
	SELECT
		@GPEmployeeID AS GPEmployeeID
		, COALESCE(SUM(CASE
						WHEN AI.CreditScore IS NOT NULL THEN 1
						ELSE 0
					END) ,0) AS TotalSeasonSales
		, COALESCE(SUM(CASE
						WHEN AI.CreditScore >= 1 AND AI.CreditScore < @PassNum THEN 1
						ELSE 0
					END) ,0) AS SubCredit
		, COALESCE(SUM(CASE
						WHEN (AI.Status <> 'OK') AND (AI.CreditScore >= @PassNum) THEN 1
						ELSE 0
					END) ,0) AS Cancels
		, COALESCE(SUM(CASE
						WHEN (AI.Status <> 'OK') AND (AI.CreditScore >= 1 AND AI.CreditScore < @PassNum) THEN 1
						ELSE 0
					END) ,0) AS SubCancels
	FROM SAE_AccountsInstalled AS AI WITH (NOLOCK)
	WHERE
		(AI.InstallDate IS NOT NULL)
		AND (AI.SeasonID = @SeasonID)
		AND
		(
			-- 2 is a Tech, 1 is a Sales Rep
			((@RoleLocationID = 2) AND (AI.TechnicianUserID = @UserID))
			OR
			((@RoleLocationID <> 2) AND (AI.SalesRepUserID = @UserID))
		)

END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetRecruitSeasonSalesTotals TO PUBLIC
GO