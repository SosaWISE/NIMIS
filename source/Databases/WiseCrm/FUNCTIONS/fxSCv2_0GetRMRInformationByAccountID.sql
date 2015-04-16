USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxSCv2_0GetRMRInformationByAccountID')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv2_0GetRMRInformationByAccountID'
		DROP FUNCTION  dbo.fxSCv2_0GetRMRInformationByAccountID
	END
GO

PRINT 'Creating FUNCTION fxSCv2_0GetRMRInformationByAccountID'
GO
/******************************************************************************
**		File: fxSCv2_0GetRMRInformationByAccountID.sql
**		Name: fxSCv2_0GetRMRInformationByAccountID
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
**		Auth: Andrés E. Sosa
**		Date: 04/15/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/15/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCv2_0GetRMRInformationByAccountID
(
	@AccountID BIGINT
)
RETURNS 
@ResultList table
(
	[CommissionsAdjustmentID] VARCHAR(20)
	, [AccountPackageID] INT
	, [ActualRMR] MONEY
	, [BaseRMR] MONEY
	, [MinRMR] MONEY
	, [MaxRMR] MONEY
	, [AdjustmentAmount] MONEY
)
AS
BEGIN
	/** DECLARATIONS */
	DECLARE @Delta MONEY
		, @CommissionsAdjustmentID VARCHAR(20) = 'NONE'
		, @ActlRMR MONEY
		, @MaxRMR MONEY
		, @BaseRMR MONEY
		, @MinRMR MONEY
		, @AdjustmentAmount MONEY
		, @AccountPackageID INT
		, @CommissionAdjustmentAmount MONEY;

	SELECT
		@AccountPackageID = MSAP.AccountPackageID
		, @BaseRMR = MSAP.BaseRMR
		, @MinRMR = MSAP.MinRMR
		, @MaxRMR = MSAP.MaxRMR
	FROM
		[dbo].[MS_AccountSalesInformations] AS MSASI WITH (NOLOCK)
		INNER JOIN [dbo].[MS_AccountPackages] AS MSAP WITH (NOLOCK)
		ON
			(MSAP.AccountPackageID = MSASI.AccountPackageId)
	WHERE
		(MSASI.AccountID = @AccountID);

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

	/** Check for Type of commission or deduction */
	SET @Delta = (@ActlRMR - @BaseRMR);

	IF (@Delta >= 1)
	BEGIN
		-- Check if this is OUT OF RANGE
		IF (@ActlRMR > @MaxRMR) 
		BEGIN
			SET @CommissionsAdjustmentID = 'RMRUPPOUTRANGE';
			SET @Delta = FLOOR(@Delta);
		END
		ELSE
			SET @CommissionsAdjustmentID = 'RMRUPPINRANGE';

		IF (@Delta > 0) 
		BEGIN
			SET @CommissionsAdjustmentID = 'UPGRADE';
		END
	END
	ELSE IF (@Delta < 0)
	BEGIN
		-- Check if this is OUT OF RANGE
		IF (@ActlRMR < @MinRMR)
		BEGIN
			-- Figure out Our of Range
			DECLARE @DeltaOOR MONEY = FLOOR(@ActlRMR - @MinRMR);
			SET @CommissionsAdjustmentID = 'RMRLOWOUTRANGE';
			SELECT @CommissionAdjustmentAmount = CommissionAdjustmentAmount FROM [NXSE_Sales].[dbo].[SC_CommissionsAdjustments] WHERE (CommissionsAdjustmentID = @CommissionsAdjustmentID);
			DECLARE @OORTotal MONEY = @DeltaOOR * @CommissionAdjustmentAmount;

			-- Figure out the In Range
			SET @Delta = FLOOR(@MinRMR - @BaseRMR);
			SELECT @CommissionAdjustmentAmount = CommissionAdjustmentAmount FROM [NXSE_Sales].[dbo].[SC_CommissionsAdjustments] WHERE (CommissionsAdjustmentID = 'RMRLOWINRANGE');
			SET @AdjustmentAmount = @Delta * @CommissionAdjustmentAmount;

			-- Finish calculation
			SET @AdjustmentAmount = @AdjustmentAmount + @OORTotal;
		END
		ELSE
		BEGIN
			SET @CommissionsAdjustmentID = 'RMRLOWINRANGE';
			SET @Delta = FLOOR(@ActlRMR - @BaseRMR);
			SELECT @CommissionAdjustmentAmount = CommissionAdjustmentAmount FROM [NXSE_Sales].[dbo].[SC_CommissionsAdjustments] WHERE (CommissionsAdjustmentID = @CommissionsAdjustmentID);
			SET @AdjustmentAmount = @Delta * @CommissionAdjustmentAmount;
		END
	END

	INSERT INTO @ResultList (CommissionsAdjustmentID, AccountPackageID, BaseRMR, ActualRMR, MaxRMR, MinRMR, AdjustmentAmount) VALUES (
		@CommissionsAdjustmentID
		, @AccountPackageID
		, @BaseRMR -- money
		, @ActlRMR -- money
		, @MaxRMR -- money
		, @MinRMR -- money
		, @AdjustmentAmount
	);

	RETURN;
END
GO

/** SELECT * FROM dbo.fxSCv2_0GetRMRInformationByAccountID(191205) */

SELECT FLOOR(-1.0)