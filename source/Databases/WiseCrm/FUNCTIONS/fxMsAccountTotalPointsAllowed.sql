USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxMsAccountTotalPointsAllowed')
	BEGIN
		PRINT 'Dropping FUNCTION fxMsAccountTotalPointsAllowed'
		DROP FUNCTION  dbo.fxMsAccountTotalPointsAllowed
	END
GO

PRINT 'Creating FUNCTION fxMsAccountTotalPointsAllowed'
GO
/******************************************************************************
**		File: fxMsAccountTotalPointsAllowed.sql
**		Name: fxMsAccountTotalPointsAllowed
**		Desc: 
**		Point calculation is based on the package, activation fee, monitoring
**	rate.
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
**		Auth: Andrés E. Sosa
**		Date: 04/08/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/08/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxMsAccountTotalPointsAllowed
(
	@AccountID BIGINT	
)
RETURNS DECIMAL(5,2)
AS
BEGIN
	/** Declarations */
	DECLARE @SeasonID INT
		, @TotalPointsAllowed DECIMAL(5,2)
		, @RMRIncreasePoints INT
		, @BasePoints DECIMAL(5,2)
		, @ActivationFee MONEY
		, @BaseRMR MONEY
		, @ActlRMR MONEY
		, @CRScore INT
		, @AccountPackageId INT
		, @CRScoreGroup VARCHAR(20);

	/** Init function */
	SELECT @SeasonID = dbo.fxGetSeasonIDByAccountID(@AccountID);
	SELECT 
		@AccountPackageId = ISNULL(AccountPackageID, 1) 
		, @RMRIncreasePoints = ISNULL(RMRIncreasePoints, 0)
	FROM [dbo].[MS_AccountSalesInformations] WHERE (AccountID = @AccountID) 

	/** Execute actions. */
	-- Get Base Points
	-- Get Base RMR
	SELECT 
		@BasePoints = MSAP.BasePoints
		, @BaseRMR = MSAP.BaseRMR
	FROM
		[dbo].[MS_AccountPackages] AS MSAP WITH (NOLOCK)
	WHERE
		(MSAP.AccountPackageID = @AccountPackageId);

	-- Activation Fee
	SELECT
		@ActivationFee = SUM(AEII.RetailPrice)
	FROM
			[dbo].[AE_Invoices] AS AEI WITH (NOLOCK)
			INNER JOIN [dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
			ON
				(AEII.InvoiceId = AEI.InvoiceID)
				AND (AEI.InvoiceTypeId = 'INSTALL')
				AND (AEI.IsActive = 1 AND AEI.IsDeleted = 0)
				AND (AEII.IsActive = 1 AND AEII.IsDeleted = 0)
				AND (AEI.AccountId = @AccountID)
			INNER JOIN [dbo].[AE_Items] AS ITM WITH (NOLOCK)
			ON
				(ITM.ItemID = AEII.ItemId)
				AND (ITM.ItemTypeId <> 'SETUP_FEE_OVR3') -- Exclude the over three months.
	WHERE
		(AEII.ItemId LIKE 'SETUP_FEE%');

	-- Get Actual RMR
	SELECT
		@ActlRMR = SUM(AEII.RetailPrice)
	FROM
			[dbo].[AE_Invoices] AS AEI WITH (NOLOCK)
			INNER JOIN [dbo].[AE_InvoiceItems] AS AEII WITH (NOLOCK)
			ON
				(AEII.InvoiceId = AEI.InvoiceID)
				AND (AEI.InvoiceTypeId = 'INSTALL')
				AND (AEI.IsActive = 1 AND AEI.IsDeleted = 0)
				AND (AEII.IsActive = 1 AND AEII.IsDeleted = 0)
				AND (AEI.AccountId = @AccountID)
			INNER JOIN [dbo].[AE_Items] AS ITM WITH (NOLOCK)
			ON
				(ITM.ItemID = AEII.ItemId)
	WHERE
		(AEII.ItemId LIKE 'MON_CONT%')
		OR (AEII.ItemId LIKE 'MMR_SREP%');

	-- Get Base RMR
	--SELECT
	--	@BaseRMR = MSAP.BaseRMR
	--FROM
	--	[dbo].[MS_AccountSalesInformations] AS MSASI WITH (NOLOCK)
	--	INNER JOIN [dbo].[MS_AccountPackages] AS MSAP WITH (NOLOCK)
	--	ON
	--		(MSAP.AccountPackageID = MSASI.AccountPackageID)
	--		AND (MSASI.AccountID = @AccountID);

	-- Get Credit Score
	SELECT @CRScore = dbo.fxQlCreditReportGetScoreByMsAccountID(@AccountID);
	SELECT
		@CRScoreGroup = CASE 
			WHEN @CRScore >= RUS.ExcellentCreditScoreThreshold THEN 'EXCELLENT'
			WHEN @CRScore >= RUS.PassCreditScoreThreshold THEN 'GOOD'
			WHEN @CRScore >= RUS.SubCreditScoreThreshold THEN 'SUB'
			ELSE 'POOR'
		END
	FROM
		[WISE_HumanResource].[dbo].[RU_Season] AS RUS WITH (NOLOCK)
	WHERE
		(RUS.SeasonID = @SeasonID)

	/**
	FOR Good or Excellent Credit
		IF $99 Activation Then give 1 point
		IF $199 Activation Then give 5 points
		IF ActualRMR > BaseRMR Then every whole $1 you get a point
	*/
	SELECT
		@TotalPointsAllowed = CASE
			WHEN @ActivationFee = 69.00 AND (@CRScoreGroup = 'EXCELLENT' OR @CRScoreGroup = 'GOOD') THEN @BasePoints
			WHEN @ActivationFee = 99.00 AND (@CRScoreGroup = 'EXCELLENT' OR @CRScoreGroup = 'GOOD') THEN @BasePoints + 1
			WHEN @ActivationFee = 199 AND (@CRScoreGroup = 'EXCELLENT' OR @CRScoreGroup = 'GOOD') THEN @BasePoints + 5
			ELSE @BasePoints
		END;

	/** Calculate points on RMR fluctuations. */
	SELECT
		@TotalPointsAllowed = @TotalPointsAllowed + CAST(@RMRIncreasePoints AS DECIMAL(5,2));

	RETURN @TotalPointsAllowed;
END
GO

/** */
DECLARE @AccountID BIGINT = 191168
	, @SeasonID INT = 4;
SELECT dbo.fxMsAccountTotalPointsAllowed(@AccountID);

--SELECT 
--	MSASI.AccountID
--	, MSASI.CsConfirmationNumber
--	, MSAP.BasePoints
--FROM
--	dbo.[MS_AccountSalesInformations] AS MSASI WITH (NOLOCK)
--	INNER JOIN [dbo].[MS_AccountPackages] AS MSAP WITH (NOLOCK)
--	ON
--		(MSAP.AccountPackageID = MSASI.AccountPackageId)
--WHERE
--	(MSASI.AccountID = @AccountID);

