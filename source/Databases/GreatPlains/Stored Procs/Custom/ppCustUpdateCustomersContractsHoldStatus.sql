USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustUpdateCustomersContractsHoldStatus')
	BEGIN
		PRINT 'Dropping Procedure ppCustUpdateCustomersContractsHoldStatus'
		DROP  Procedure  dbo.ppCustUpdateCustomersContractsHoldStatus
	END
GO

PRINT 'Creating Procedure ppCustUpdateCustomersContractsHoldStatus'
GO
/******************************************************************************
**		File: ppCustUpdateCustomersContractsHoldStatus.sql
**		Name: ppCustUpdateCustomersContractsHoldStatus
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
**		Auth: Brett Kotter
**		Date: 10/01/2009
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	10/01/2009	Brett Kotter	Created	
**	10-19-2009	Brett Kotter	Updated sproc to use a different way of splitting ints
*******************************************************************************/
CREATE Procedure dbo.ppCustUpdateCustomersContractsHoldStatus
(
	@CreditHold bit
	, @AccountIDs NVARCHAR(MAX) 
)
AS
BEGIN
	UPDATE SVC00600
		SET
            CREDIT_HOLD=@CreditHold
            , PORDNMBR=''
        WHERE
		CUSTNMBR IN (
						SELECT
							Convert(NVARCHAR(10),ID)
						FROM dbo.SplitIntList(@AccountIDs))
		AND CNTTYPE != 'TERMINATE'	
END
GO

GRANT EXEC ON dbo.ppCustUpdateCustomersContractsHoldStatus TO PUBLIC
GO