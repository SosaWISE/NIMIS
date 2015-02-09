USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custQL_LeadSourceGetByDealerId')
	BEGIN
		PRINT 'Dropping Procedure custQL_LeadSourceGetByDealerId'
		DROP  Procedure  dbo.custQL_LeadSourceGetByDealerId
	END
GO

PRINT 'Creating Procedure custQL_LeadSourceGetByDealerId'
GO
/******************************************************************************
**		File: custQL_LeadSourceGetByDealerId.sql
**		Name: custQL_LeadSourceGetByDealerId
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
CREATE Procedure dbo.custQL_LeadSourceGetByDealerId
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
		QL_LeadSources AS QLS WITH (NOLOCK)
	WHERE
		(QLS.DealerId IS NULL OR QLS.DealerId = @DealerId)
		AND (QLS.IsActive = 1) AND (QLS.IsDeleted = 0)
	ORDER BY
		QLS.LeadSource ASC
	
END
GO

GRANT EXEC ON dbo.custQL_LeadSourceGetByDealerId TO PUBLIC
GO

/** Execute dbo.custQL_LeadSourceGetByDealerId 5000 */