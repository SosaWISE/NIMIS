USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerSWINGPremiseAddress')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerSWINGPremiseAddress'
		DROP VIEW dbo.vwAE_CustomerSWINGPremiseAddress
	END
GO

PRINT 'Creating VIEW vwAE_CustomerSWINGPremiseAddress'
GO

/****** Object:  View [dbo].[vwAE_CustomerSWINGPremiseAddress]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerSWINGPremiseAddress.sql
**		Name: vwAE_CustomerSWINGPremiseAddress
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
**	04/22/2014	Junryl/Reagan		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_CustomerSWINGPremiseAddress]
AS

	-- Enter Query here
	SELECT
		CAST('' AS VARCHAR(50)) AS StreetAddress1
		, CAST('' AS VARCHAR(50)) AS StreetAddress2
		, CAST('' AS VARCHAR(50)) AS City
		, CAST('' AS VARCHAR(50)) AS County
		, CAST('' AS VARCHAR(50)) AS PostalCode
		, CAST('' AS VARCHAR(50)) AS State		

GO
/* TEST */
-- SELECT * FROM vwAE_CustomerSWINGPremiseAddress
