USE [NXSE_Sales]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwSC_WorkAccounts')
	BEGIN
		PRINT 'Dropping VIEW vwSC_WorkAccounts'
		DROP VIEW dbo.vwSC_WorkAccounts
	END
GO

PRINT 'Creating VIEW vwSC_WorkAccounts'
GO

/****** Object:  View [dbo].[vwSC_WorkAccounts]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwSC_WorkAccounts.sql
**		Name: vwSC_WorkAccounts
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
**		Date: 04/21/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/21/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwSC_WorkAccounts]
AS
	-- Enter Query here
	SELECT
		SCWA.WorkAccountID
		, SCWA.CommissionPeriodId
		, DATEADD(d, -7, SCP.CommissionPeriodEndDate) AS CommissionPeriodStartDate
		, SCP.CommissionPeriodEndDate
		, SCWA.AccountID
		, SCWA.CustomerMasterFileId
		, SCWA.AccountPackageId
		, SCWA.SalesRepId
		, RU.FullName AS [Sales Name]
		, SCWA.TechId
		, RUT.FullName AS [Tech Name]
		, SCWA.FriendsAndFamilyTypeId
		, SCWA.QualifyDate
		, SCWA.SaleDate
		, SCWA.PostSurveyDate
		, SCWA.InstallDate
		, SCWA.AMASignedDate
		, SCWA.NOCDateCalculated
		, SCWA.ApprovedDate
		, SCWA.ApproverId
		, SCWA.SeasonId
		, SCWA.DealerId
		, SCWA.CreditScore
		, SCWA.CreditCustomerType
		, SCWA.ContractLength
		, SCWA.PaymentType
		, SCWA.PointsOfProtection
		, SCWA.PointsAllowed
		, SCWA.PointsAssignedToRep
		, SCWA.ActivationFee
		, SCWA.RMR
		, SCWA.DoNotPay
		, SCWA.Waive1stMonth
		, SCWA.NotOwner
		, SCWA.ApprovedPaperWork
		, SCWA.NOCPeriodNotExp
		, SCWA.Cancelled
		, SCWA.HasHolds
		, SCWA.ContractLengthLess36
		, SCWA.NoneCcOrAch
		, SCWA.SetupFeeNotQualified
		, SCWA.IgnoreAllRules
	FROM
		[dbo].[SC_WorkAccountsAll] AS SCWA WITH (NOLOCK)
		INNER JOIN [dbo].[SC_CommissionPeriods] AS SCP WITH (NOLOCK)
		ON
			(SCP.CommissionPeriodID = SCWA.CommissionPeriodId)
		LEFT OUTER JOIN [WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
		ON
			(RU.GPEmployeeId = SCWA.SalesRepId)
		LEFT OUTER JOIN [WISE_HumanResource].[dbo].[RU_Users] AS RUT WITH (NOLOCK)
		ON
			(RUT.GPEmployeeId = SCWA.TechId)
GO
/* TEST */
SELECT * FROM vwSC_WorkAccounts