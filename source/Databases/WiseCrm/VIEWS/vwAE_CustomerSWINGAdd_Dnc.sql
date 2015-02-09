USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerSWINGAdd_Dnc')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerSWINGAdd_Dnc'
		DROP VIEW dbo.vwAE_CustomerSWINGAdd_Dnc
	END
GO

PRINT 'Creating VIEW vwAE_CustomerSWINGAdd_Dnc'
GO

/****** Object:  View [dbo].[vwAE_CustomerSWINGAdd_Dnc]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerSWINGAdd_Dnc.sql
**		Name: vwAE_CustomerSWINGAdd_Dnc
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
**	05/23/2014	Junryl/Reagan		Created by
*******************************************************************************/	
CREATE VIEW [dbo].[vwAE_CustomerSWINGAdd_Dnc]
AS

	-- Enter Query here
	SELECT
		CAST('' AS VARCHAR(50)) AS Dnc_Status
GO
/* TEST */
-- SELECT * FROM View_Template
