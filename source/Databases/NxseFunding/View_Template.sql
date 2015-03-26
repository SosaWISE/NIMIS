USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountOnlineStatusInfo')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountOnlineStatusInfo'
		DROP VIEW dbo.vwMS_AccountOnlineStatusInfo
	END
GO

PRINT 'Creating VIEW vwMS_AccountOnlineStatusInfo'
GO

/****** Object:  View [dbo].[vwMS_AccountOnlineStatusInfo]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountOnlineStatusInfo.sql
**		Name: vwMS_AccountOnlineStatusInfo
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
**		Date: 03/16/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/16/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwFE_Packets]
AS
	-- Enter Query here
	SELECT
		 FEP.PacketID ,
		        FEP.CriteriaId ,
		        FEP.SubmittedOn ,
		        FEP.SubmittedBy ,
		        FEP.CreatedOn ,
		        FEP.CreatedBy
	FROM
		[dbo].[FE_Packets] AS FEP WITH (NOLOCK)
		INNER JOIN [dbo].[FE_Criterias] AS FEC WITH (NOLOCK)
		ON
			(FEC.CriteriaID = FEP.CriteriaId)
GO
/* TEST */
-- SELECT * FROM vwMS_AccountOnlineStatusInfo
