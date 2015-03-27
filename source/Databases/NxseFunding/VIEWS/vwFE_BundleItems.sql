USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwFE_BundleItems')
	BEGIN
		PRINT 'Dropping VIEW vwFE_BundleItems'
		DROP VIEW dbo.vwFE_BundleItems
	END
GO

PRINT 'Creating VIEW vwFE_BundleItems'
GO

/****** Object:  View [dbo].[vwFE_BundleItems]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwFE_BundleItems.sql
**		Name: vwFE_BundleItems
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
**		Date: 03/27/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/27/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwFE_BundleItems]
AS
	-- Enter Query here
	SELECT
		FEBI.BundleItemID
		, FEBI.BundleId
		, FEBI.PacketId
		, FEBI.IsDeleted
		, FEBI.CreatedOn
		, FEBI.CreatedBy
		, FEP.SubmittedOn AS PSubmittedOn
		, FEP.SubmittedBy AS PSubmittedBy
		, FEP.CreatedOn AS PCreatedOn
		, FEP.CreatedBy AS PCreatedBy
	FROM
		[dbo].[FE_BundleItems] AS FEBI WITH (NOLOCK)
		INNER JOIN [dbo].[FE_Packets] AS FEP WITH (NOLOCK)
		ON
			(FEP.PacketID = FEBI.PacketId)
GO
/* TEST */
-- SELECT * FROM vwFE_BundleItems
