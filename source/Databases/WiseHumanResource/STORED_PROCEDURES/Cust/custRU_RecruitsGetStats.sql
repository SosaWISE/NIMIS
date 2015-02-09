USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetStats')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetStats'
		DROP  Procedure  dbo.custRU_RecruitsGetStats
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetStats'
GO
/******************************************************************************
**		File: custRU_RecruitsGetStats.sql
**		Name: custRU_RecruitsGetStats
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
CREATE Procedure dbo.custRU_RecruitsGetStats
(
	@RecruitID INT
)
AS
BEGIN


	--DECLARE @RecruitID INT
	--SET @RecruitID = 27

	DECLARE @IsRepOrTech INT
	DECLARE @UserID INT
	DECLARE @SeasonID INT
	DECLARE @PassCreditScoreThreshold INT
	SELECT
		@IsRepOrTech = (CASE WHEN RecUser.RoleLocationID = 2 THEN 0 ELSE 1 END)
		, @UserID = RecUser.UserID
		, @SeasonID = RecUser.SeasonID
		, @PassCreditScoreThreshold = RS.PassCreditScoreThreshold
	FROM VW_RecruitUser AS RecUser WITH(NOLOCK)
	INNER JOIN RU_Season AS RS WITH(NOLOCK)
	ON
		RecUser.SeasonID = RS.SeasonID
	WHERE
		RecruitID = @RecruitID
		
	--SELECT @RoleLocationID
	--SELECT @UserID
	--SELECT @SeasonID

	SELECT
		COUNT(*) AS NGross
		, SUM(CASE
				WHEN @IsRepOrTech = 1 AND AI.CreditScore >= @PassCreditScoreThreshold AND AI.Status <> 'OK' THEN 1
				ELSE 0
			END) AS NCancels
		, SUM(CASE
				WHEN @IsRepOrTech = 1 AND AI.CreditScore < @PassCreditScoreThreshold AND AI.Status = 'OK' THEN 1
				ELSE 0
			END) AS NSubs
		, SUM(CASE
				WHEN @IsRepOrTech = 1 AND AI.CreditScore < @PassCreditScoreThreshold AND AI.Status <> 'OK' THEN 1
				ELSE 0
			END) AS NSubCancels
		, SUM(CASE
				WHEN @IsRepOrTech = 1 AND AI.ActivationFee = 0 THEN 1
				ELSE 0
			END) AS NActivationWaives
			
		, 0 AS NFrontEndHolds
		, 0 AS NBackEndHolds
		, RecUser.RecruitID
	FROM SAE_AccountsInstalled AS AI WITH(NOLOCK)
	INNER JOIN VW_RecruitUser AS RecUser WITH(NOLOCK)
	ON
		(
			(@IsRepOrTech = 0 AND AI.TechnicianUserID = RecUser.UserID)
			OR (@IsRepOrTech = 1 AND AI.SalesRepUserID = RecUser.UserID)
		)
		AND AI.SeasonID = RecUser.SeasonID
		AND RecUser.RecruitID = @RecruitID
	GROUP BY
		RecUser.RecruitID


END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetStats TO PUBLIC
GO