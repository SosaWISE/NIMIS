USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustCalculateContractPayoffAmount')
	BEGIN
		PRINT 'Dropping Procedure ppCustCalculateContractPayoffAmount'
		DROP  Procedure  dbo.ppCustCalculateContractPayoffAmount
	END
GO

PRINT 'Creating Procedure ppCustCalculateContractPayoffAmount'
GO
/******************************************************************************
**		File: ppCustCalculateContractPayoffAmount.sql
**		Name: ppCustCalculateContractPayoffAmount
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
**		Date: 03/02/2009
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	03/02/2009	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustCalculateContractPayoffAmount
(
	@CustomerNumber NVARCHAR(50)
)
AS
BEGIN

	SELECT
		PayoffAmount
	FROM
		vwContractPayoffAmounts
	WHERE
		CUSTNMBR = @CustomerNumber

END
GO

GRANT EXEC ON dbo.ppCustCalculateContractPayoffAmount TO PUBLIC
GO