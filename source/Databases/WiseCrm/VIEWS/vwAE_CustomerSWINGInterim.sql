USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerSWINGInterim')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerSWINGInterim'
		DROP VIEW dbo.vwAE_CustomerSWINGInterim
	END
GO

PRINT 'Creating VIEW vwAE_CustomerSWINGInterim'
GO

/****** Object:  View [dbo].[vwAE_CustomerSWINGInterim]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerSWINGInterim.sql
**		Name: vwAE_CustomerSWINGInterim
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
**	05/02/2014	Junryl/Reagan		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_CustomerSWINGInterim]
AS
	-- Enter Query here
	SELECT 
		*
	FROM
		[dbo].[MS_AccountSwungInfo]
GO
/* TEST */
-- SELECT * FROM vwAE_CustomerSWINGInterim
