USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxSCv2_0GetPointInformationByAccountID')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv2_0GetPointInformationByAccountID'
		DROP FUNCTION  dbo.fxSCv2_0GetPointInformationByAccountID
	END
GO

PRINT 'Creating FUNCTION fxSCv2_0GetPointInformationByAccountID'
GO
/******************************************************************************
**		File: fxSCv2_0GetPointInformationByAccountID.sql
**		Name: fxSCv2_0GetPointInformationByAccountID
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
**		Auth: Peter Fry
**		Date: 05/21/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	05/21/2015	Peter Fry		Created function
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCv2_0GetPointInformationByAccountID
(
	@AccountID BIGINT
	--, @ActlRMR MONEY
)
RETURNS 
@ResultList table
(
	[CommissionDeductionID] VARCHAR(20)
	, [CommissionBonusID] VARCHAR(20)
	, [PointAllowed] DECIMAL (5,2)
	, [PointsAssignedToRep] DECIMAL (5,2)
	, [AdjustmentAmount] MONEY
)
AS
BEGIN
	/** DECLARATIONS */
	DECLARE @Delta MONEY
		, @CommissionDeductionID VARCHAR(20) = NULL
		, @CommissionBonusID VARCHAR(20) = NULL
		, @PointsAllowed DECIMAL (5,2)
		, @PointsAssignedToRep DECIMAL (5,2)
		, @AdjustmentAmount MONEY
		, @CommissionAdjustmentAmount MONEY;

	SET @PointsAllowed = (SELECT dbo.fxMsAccountTotalPointsAllowed(@AccountID));
	SET @PointsAssignedToRep = (SELECT dbo.fxMsAccountTotalPointsRep(@AccountID));

	/** Check for Type of commission or deduction */
	SET @Delta = (@PointsAssignedToRep - @PointsAllowed);

	IF (@Delta > 0)
	BEGIN
		-- Figure out the # of points above the allowed
		SET @CommissionDeductionID = 'POINTSOVERALLOWED';
		SELECT @CommissionAdjustmentAmount = DeductionAmount FROM [NXSE_Commissions].[dbo].[SC_CommissionDeductions] WHERE (CommissionDeductionID = @CommissionDeductionID);
		SET @Delta = CEILING(@PointsAssignedToRep - @PointsAllowed) * (-1);
		SET @AdjustmentAmount = @Delta * @CommissionAdjustmentAmount;
	END
	ELSE IF (@Delta < 0)
	BEGIN
		-- Figure out the # of points below the allowed
		SET @CommissionBonusID= 'POINTSUNDERALLOWED';
		SELECT @CommissionAdjustmentAmount = BonusAmount FROM [NXSE_Commissions].[dbo].[SC_CommissionBonus] WHERE (CommissionBonusID = @CommissionBonusID);
		SET @Delta = FLOOR(@PointsAllowed - @PointsAssignedToRep);
		SET @AdjustmentAmount = @Delta * @CommissionAdjustmentAmount;
	END

	INSERT INTO @ResultList (CommissionDeductionID, CommissionBonusID,PointAllowed, PointsAssignedToRep, AdjustmentAmount) VALUES (
		@CommissionDeductionID
		, @CommissionBonusID
		, @PointsAllowed -- decimal
		, @PointsAssignedToRep -- decimal
		, @AdjustmentAmount
	);

	RETURN;
END
GO

/** 
DECLARE @AccountID BIGINT = 191233
SELECT * from dbo.vwMS_AccountSalesInformations WHERE AccountID = @AccountID
SELECT * FROM dbo.fxSCv2_0GetPointInformationByAccountID(@AccountID)
*/
