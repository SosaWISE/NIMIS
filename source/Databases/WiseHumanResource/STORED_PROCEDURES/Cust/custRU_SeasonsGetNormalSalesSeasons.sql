USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_SeasonsGetNormalSalesSeasons')
	BEGIN
		PRINT 'Dropping Procedure custRU_SeasonsGetNormalSalesSeasons'
		DROP  Procedure  dbo.custRU_SeasonsGetNormalSalesSeasons
	END
GO

PRINT 'Creating Procedure custRU_SeasonsGetNormalSalesSeasons'
GO
/******************************************************************************
**		File: custRU_SeasonsGetNormalSalesSeasons.sql
**		Name: custRU_SeasonsGetNormalSalesSeasons
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
**		Auth: 
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:				Description:
**	-----------	-------------		-------------------------------------------
**	12/05/2013	Carly Christiansen	Created by
*******************************************************************************/
CREATE Procedure dbo.custRU_SeasonsGetNormalSalesSeasons
AS
BEGIN

	SELECT
		*
	FROM
		RU_Season
	WHERE 
		(IsInsideSales = 0)
		AND (IsDeleted = 0);
END
GO

GRANT EXEC ON dbo.custRU_SeasonsGetNormalSalesSeasons TO PUBLIC
GO