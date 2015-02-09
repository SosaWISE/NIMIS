USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerSWUNGInfo')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerSWUNGInfo'
		DROP VIEW dbo.vwAE_CustomerSWUNGInfo
	END
GO

PRINT 'Creating VIEW vwAE_CustomerSWUNGInfo'
GO

/****** Object:  View [dbo].[vwAE_CustomerSWUNGInfo]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerSWUNGInfo.sql
**		Name: vwAE_CustomerSWUNGInfo
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
**		Date: 04/21/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	05/21/2014	Junryl/Reagan	Created by
**	05/26/2014	Junryl			Fields changes to pull
**	05/29/2014	Andres Sosa		Just calling the table.
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_CustomerSWUNGInfo]
AS
	SELECT 
		*
	FROM
		[dbo].[MS_AccountSwungInfo]

GO
/* TEST */
-- SELECT * FROM vwAE_CustomerSWUNGInfo
