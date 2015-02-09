USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetActiveSeasonAccounts')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetActiveSeasonAccounts'
		DROP  Procedure  dbo.custRU_RecruitsGetActiveSeasonAccounts
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetActiveSeasonAccounts'
GO
/******************************************************************************
**		File: custRU_RecruitsGetActiveSeasonAccounts.sql
**		Name: custRU_RecruitsGetActiveSeasonAccounts
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
CREATE Procedure dbo.custRU_RecruitsGetActiveSeasonAccounts
(
	@GPEmployeeID NVarChar(25)
	, @SeasonID INT
	, @IsCanceled BIT = NULL
	, @IsDelinquent BIT = NULL
	, @HasRepHold BIT = NULL
	, @HasTechHold BIT = NULL
)
WITH RECOMPILE
AS
BEGIN
	
--	DECLARE @GPEmployeeID NVarChar(25)
--	SET @GPEmployeeID = 'ROGE001'
--
--	DECLARE @SeasonID INT
--	SET @SeasonID = 3

	DECLARE @RoleLocationID NVarChar(25)
	SET @RoleLocationID = dbo.EmployeeRoleLocation(@GPEmployeeID, @SeasonID)

	SELECT
		MSA.AccountID
		, ML.FirstName + ' ' + ML.LastName AS CustomerName
		, ML.PhoneHome AS CustomerPhone
		, MAD.StreetAddress
		, MAD.StreetAddress2
		, MAD.City
		, MPS.StateAB
		, MaxScoreTable.CreditScore AS Credit
		, CONVERT(CHAR(11), MSA.CreatedByDate,106) AS SaleDate
		, MSA.CreatedByDate AS SaleDateData
		, CONVERT(CHAR(11), AI.InstallDate,106) AS InstallDate
		, AI.InstallDate AS InstallDateData
		, ISNULL(AI.Status, 'OK') AS Status
		, MACT.AccountCellularType
		, MSA.SalesRepPointBank
		, MAMT.MonitoringType
		, MSA.TechUpgradeFee
		, CASE
			WHEN @RoleLocationID = 2 THEN MSA.TechUpgradeFee
			ELSE MSA.UpgradeFee
		END AS UpgradeFee
		, ST.SubmittedToGPDate
		, MSA.MonthlyFee
		, AI.ContractLength
		, AI.ActivationFee
		, CAST(CASE
			WHEN COALESCE(ADS.IsThirtyDaysPastDue, 0) = 1
					OR COALESCE(ADS.IsSixtyDaysPastDue, 0) = 1
					OR COALESCE(ADS.IsNinetyPlusDaysPastDue, 0) = 1 
				THEN 1
			ELSE 0
		END AS BIT) AS IsDelinquent
		, COALESCE(HLD.HasRepFrontEndHold, 0) AS HasRepFrontEndHold
		, COALESCE(HLD.HasRepBackEndHold, 0) AS HasRepBackEndHold
		, COALESCE(HLD.HasTechFrontEndHold, 0) AS HasTechFrontEndHold
		, COALESCE(HLD.HasTechBackEndHold, 0) AS HasTechBackEndHold
	FROM SAE_AccountsInstalled AS AI WITH(NOLOCK)
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_Account MSA WITH (NOLOCK)
	ON
		AI.AccountID = MSA.AccountID
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_AccountStatus AS ST WITH (NOLOCK)
	ON
		MSA.AccountID = ST.AccountID
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MC_Lead AS ML WITH (NOLOCK)
	ON 
		MSA.Customer1ID = ML.LeadID
	INNER JOIN dbo.SAE_MaxCredit AS MaxScoreTable WITH (NOLOCK)
	ON
		MSA.AccountID = MaxScoreTable.AccountID
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MC_Address MAD WITH (NOLOCK)
	ON
		MSA.PremiseAddressID = MAD.AddressID
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalState MPS WITH (NOLOCK)
	ON
		MAD.StateID = MPS.StateID

	INNER JOIN RU_TeamLocations AS TeamLocs WITH (NOLOCK)
	ON
		(MSA.TeamLocationID = TeamLocs.TeamLocationID)

	INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_AccountCellularType AS MACT WITH (NOLOCK)
	ON
		MSA.AccountCellularTypeId = MACT.AccountCellularTypeId
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_IndustryAccount AS MIA WITH(NOLOCK)
	ON
		MSA.IndustryAccountID = MIA.IndustryAccountID
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_ReceiverLine AS MRL WITH (NOLOCK)
	ON
		MIA.ReceiverLineID = MRL.ReceiverLineID 
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_AccountMonitoringType AS MAMT WITH (NOLOCK)
	ON
		MSA.MonitoringTypeID = MAMT.MonitoringTypeID
	LEFT JOIN Platinum_Protection_InterimCRM.dbo.SAE_AccountDelinquentStatus AS ADS WITH (NOLOCK)
	ON
		(ADS.AccountID = AI.AccountID)
	LEFT JOIN Platinum_Protection_InterimCRM.dbo.vwMC_AccountHoldAccountHoldStatus AS HLD WITH (NOLOCK)
	ON
		(HLD.AccountID = AI.AccountID)
	WHERE
		(ST.InstallDate IS NOT NULL)
		AND (TeamLocs.SeasonID = @SeasonID) -- Filter TeamLocation By Season
		AND
		(
			-- 2 is a Tech, 1 is a Sales Rep, other than 1 and 2... we shouldn't get that
			((@RoleLocationID = 2) AND (MSA.GPTechnicianID = @GPEmployeeID))
			OR
			((@RoleLocationID IS NULL OR @RoleLocationID <> 2) AND (MSA.GPSalesRepID = @GPEmployeeID))
		)
		AND (@IsCanceled IS NULL OR (@IsCanceled = 0 AND AI.Status = 'OK') OR (@IsCanceled = 1 AND AI.Status <> 'OK'))
		AND (@IsDelinquent IS NULL OR
				(COALESCE(ADS.IsThirtyDaysPastDue, 0) = @IsDelinquent
					OR COALESCE(ADS.IsSixtyDaysPastDue, 0) = @IsDelinquent
					OR COALESCE(ADS.IsNinetyPlusDaysPastDue, 0) = @IsDelinquent))
		AND (@HasRepHold IS NULL OR (COALESCE(HLD.HasRepFrontEndHold, 0) = @HasRepHold OR COALESCE(HLD.HasRepBackEndHold, 0) = @HasRepHold))
		AND (@HasTechHold IS NULL OR (COALESCE(HLD.HasRepFrontEndHold, 0) = @HasTechHold OR COALESCE(HLD.HasRepBackEndHold, 0) = @HasTechHold))
	ORDER BY
		ST.InstallDate DESC
END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetActiveSeasonAccounts TO PUBLIC
GO