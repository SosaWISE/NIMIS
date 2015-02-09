USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cust_MC_DealerUserGetByDealerID')
	BEGIN
		PRINT 'Dropping Procedure cust_MC_DealerUserGetByDealerID'
		DROP  Procedure  dbo.cust_MC_DealerUserGetByDealerID
	END
GO

PRINT 'Creating Procedure cust_MC_DealerUserGetByDealerID'
GO
/******************************************************************************
**		File: cust_MC_DealerUserGetByDealerID.sql
**		Name: cust_MC_DealerUserGetByDealerID
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
CREATE Procedure dbo.cust_MC_DealerUserGetByDealerID
(
	@DealerID INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	SELECT
		MDU.*
	FROM
		[WISE_CRM].[dbo].MC_DealerUsers AS MDU WITH (NOLOCK)
	WHERE
		(MDU.DealerId = @DealerID)
		AND (MDU.IsActive = 1 AND MDU.IsDeleted = 0)
	ORDER BY
		MDU.FullName ASC
	
END
GO

GRANT EXEC ON dbo.cust_MC_DealerUserGetByDealerID TO PUBLIC
GO