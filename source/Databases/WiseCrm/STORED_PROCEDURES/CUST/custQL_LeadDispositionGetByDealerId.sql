USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custQL_LeadDispositionGetByDealerId')
	BEGIN
		PRINT 'Dropping Procedure custQL_LeadDispositionGetByDealerId'
		DROP  Procedure  dbo.custQL_LeadDispositionGetByDealerId
	END
GO

PRINT 'Creating Procedure custQL_LeadDispositionGetByDealerId'
GO
/******************************************************************************
**		File: custQL_LeadDispositionGetByDealerId.sql
**		Name: custQL_LeadDispositionGetByDealerId
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
**		Auth: Andres Sosa
**		Date: 00/00/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	00/00/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custQL_LeadDispositionGetByDealerId
(
	@DealerId INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Execute Statement */	
	SELECT
		*
	FROM
		QL_LeadDispositions AS QLD WITH (NOLOCK)
	WHERE
		(QLD.DealerId IS NULL OR QLD.DealerId = @DealerId)
		AND (QLD.IsActive = 1) AND (QLD.IsDeleted = 0)
	ORDER BY
		QLD.LeadDisposition ASC
	
END
GO

GRANT EXEC ON dbo.custQL_LeadDispositionGetByDealerId TO PUBLIC
GO

/** Execute dbo.custQL_LeadDispositionGetByDealerId 5000 */