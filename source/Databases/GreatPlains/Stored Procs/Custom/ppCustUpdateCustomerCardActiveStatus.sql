USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustUpdateCustomerCardActiveStatus')
	BEGIN
		PRINT 'Dropping Procedure ppCustUpdateCustomerCardActiveStatus'
		DROP  Procedure  dbo.ppCustUpdateCustomerCardActiveStatus
	END
GO

PRINT 'Creating Procedure ppCustUpdateCustomerCardActiveStatus'
GO
/******************************************************************************
**		File: ppCustUpdateCustomerCardActiveStatus.sql
**		Name: ppCustUpdateCustomerCardActiveStatus
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
**		Date: 06/25/2010
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	06/25/2010	Brett Kotter	Created	
*******************************************************************************/
CREATE Procedure dbo.ppCustUpdateCustomerCardActiveStatus
(
	@InactiveStatus BIT
	, @HoldStatus BIT
	, @AccountIDs NVARCHAR(MAX) 
)
AS
BEGIN
	UPDATE RM00101
		SET
            INACTIVE=@InactiveStatus
            , HOLD=@HoldStatus
        WHERE
		CUSTNMBR IN (
						SELECT
							Convert(NVARCHAR(10),ID)
						FROM dbo.SplitIntList(@AccountIDs))	
END
GO

GRANT EXEC ON dbo.ppCustUpdateCustomerCardActiveStatus TO PUBLIC
GO