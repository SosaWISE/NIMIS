USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwFE_Criterias')
	BEGIN
		PRINT 'Dropping VIEW vwFE_Criterias'
		DROP VIEW dbo.vwFE_Criterias
	END
GO

PRINT 'Creating VIEW vwFE_Criterias'
GO

/****** Object:  View [dbo].[vwFE_Criterias]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwFE_Criterias.sql
**		Name: vwFE_Criterias
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
**		Auth: Andres Sosa
**		Date: 03/24/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/24/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwFE_Criterias]
AS
	-- Enter Query here
	SELECT
		FEC.CriteriaID
		, FEC.PurchaserId
		, FEP.PurchaserName
		, FEC.CriteriaName
		, FEC.DESCRIPTION
        , FEC.FilterString
		, FEC.CreatedBy
		, FEC.CreatedOn
	FROM
		[dbo].[FE_Criterias] AS FEC WITH (NOLOCK)
		INNER JOIN [dbo].[FE_Purchasers] AS FEP WITH (NOLOCK)
		ON
			(FEP.PurchaserID = FEC.PurchaserId)
	WHERE
		(FEC.IsDeleted = 0)
GO
/* TEST */
-- SELECT * FROM vwFE_Criterias
