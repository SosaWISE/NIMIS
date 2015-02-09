USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustEnsureContractPricingSetup')
	BEGIN
		PRINT 'Dropping Procedure ppCustEnsureContractPricingSetup'
		DROP  Procedure  dbo.ppCustEnsureContractPricingSetup
	END
GO

PRINT 'Creating Procedure ppCustEnsureContractPricingSetup'
GO
/******************************************************************************
**		File: ppCustEnsureContractPricingSetup.sql
**		Name: ppCustEnsureContractPricingSetup
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
**		Auth: Todd Carlson
**		Date: 12/16/2008
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	12/16/2008	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustEnsureContractPricingSetup
(
	@I_vPriceSchedule CHAR(11)
	, @I_vItemSku CHAR(31)
)
AS
BEGIN
	
	IF NOT EXISTS (SELECT * FROM SVC00650 WHERE PRICSHED = @I_vPriceSchedule)
	BEGIN
		INSERT INTO
			SVC00650
			(
				PRICSHED
				, PRSCHDSC
			)
		VALUES
			(
				@I_vPriceSchedule
				, @I_vPriceSchedule
			)
	END

	IF NOT EXISTS(SELECT * FROM SVC00654 WHERE PRICSHED = @I_vPriceSchedule AND ITEMNMBR = @I_vItemSku)
	BEGIN
		INSERT INTO
			SVC00654
			(
				PRICSHED
				, ITMCLSCD
				, ITEMNMBR
				, CUSTCLAS
				, CUSTNMBR
				, UNITCOST
				, UNITPRCE
			)
		VALUES
			(
				@I_vPriceSchedule
				, ''
				, @I_vItemSku
				, ''
				, ''
				, 0.00
				, CAST(@I_vPriceSchedule AS MONEY) * 12
			)
	END
		
END
GO

GRANT EXEC ON dbo.ppCustEnsureContractPricingSetup TO PUBLIC
GO