USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwFE_Packets')
	BEGIN
		PRINT 'Dropping VIEW vwFE_Packets'
		DROP VIEW dbo.vwFE_Packets
	END
GO

PRINT 'Creating VIEW vwFE_Packets'
GO

/****** Object:  View [dbo].[vwFE_Packets]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwFE_Packets.sql
**		Name: vwFE_Packets
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
**		Date: 03/25/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/25/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwFE_Packets]
AS
	-- Enter Query here
	SELECT
		FEP.PacketID
		, FEC.CriteriaName
		, FEP.CriteriaId
		, FEPE.PurchaserID
		, FEPE.PurchaserName
		, FEP.SubmittedOn
		, FEP.SubmittedBy
		, FEP.IsDeleted
		, FEP.CreatedOn
		, FEP.CreatedBy
	FROM
		[dbo].[FE_Packets] AS FEP WITH (NOLOCK)
		INNER JOIN [dbo].[FE_Criterias] AS FEC WITH (NOLOCK)
		ON
			(FEC.CriteriaID = FEP.CriteriaId)
		INNER JOIN [dbo].[FE_Purchasers] AS FEPE WITH (NOLOCK)
		ON
			(FEC.PurchaserId = FEPE.PurchaserID)
GO
/* TEST */
-- SELECT * FROM vwFE_Packets
