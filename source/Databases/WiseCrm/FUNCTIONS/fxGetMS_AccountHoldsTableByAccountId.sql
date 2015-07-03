USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxGetMS_AccountHoldsTableByAccountId')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetMS_AccountHoldsTableByAccountId'
		DROP FUNCTION  dbo.fxGetMS_AccountHoldsTableByAccountId
	END
GO

PRINT 'Creating FUNCTION fxGetMS_AccountHoldsTableByAccountId'
GO
/******************************************************************************
**		File: fxGetMS_AccountHoldsTableByAccountId.sql
**		Name: fxGetMS_AccountHoldsTableByAccountId
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
**		Date: 07/02/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	07/02/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetMS_AccountHoldsTableByAccountId
(
	@AccountID BIGINT
)
RETURNS 
@ParsedList table
(
	[AccountHoldID] [bigint] NOT NULL,
	[AccountId] [bigint] NOT NULL,
	[Catg1ID] INT NOT NULL,
	[Catg2Id] [int] NOT NULL,
	[Catg1] [NVARCHAR](MAX) NOT NULL,
	[Catg2] [NVARCHAR](MAX) NOT NULL,
	[HoldDescription] [nvarchar](4000) NULL,
	[FixedNote] [nvarchar](4000) NULL,
	[FixedBy] [nvarchar](50) NULL,
	[FixedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](50) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL
)
AS
BEGIN

	INSERT INTO @ParsedList (
		[AccountHoldID] 
		, AccountId
		, Catg1ID
		, Catg2Id
		, Catg1
		, Catg2
		, HoldDescription
		, FixedNote
		, FixedBy
		, FixedOn
		, IsActive
		, CreatedBy
		, CreatedOn
		, ModifiedBy
		, ModifiedOn
	)
	SELECT 
		MAH.AccountHoldID
		, MAH.AccountId
		, MAH.Catg1ID
		, MAH.Catg2Id
		, MAH.Catg1
		, MAH.Catg2
		, MAH.HoldDescription
		, MAH.FixedNote
		, MAH.FixedBy
		, MAH.FixedOn
		, MAH.IsActive
		, MAH.CreatedBy
		, MAH.CreatedOn
		, MAH.ModifiedBy
		, MAH.ModifiedOn
	FROM
		dbo.vwMS_AccountHolds AS MAH
	WHERE
		(MAH.AccountID = @AccountID);

	RETURN
END
GO