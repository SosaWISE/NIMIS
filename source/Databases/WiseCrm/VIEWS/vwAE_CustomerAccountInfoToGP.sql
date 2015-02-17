USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerAccountInfoToGP')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerAccountInfoToGP'
		DROP VIEW dbo.vwAE_CustomerAccountInfoToGP
	END
GO

PRINT 'Creating VIEW vwAE_CustomerAccountInfoToGP'
GO

/****** Object:  View [dbo].[vwAE_CustomerAccountInfoToGP]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerAccountInfoToGP.sql
**		Name: vwAE_CustomerAccountInfoToGP
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
**		Date: 02/17/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/17/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_CustomerAccountInfoToGP]
AS
	-- Enter Query here
	SELECT
		CAST(NULL AS BIGINT) AS [CustomerMasterFileID]
		, CAST(NULL AS BIGINT) AS [CustomerID]
		, CAST(NULL AS BIGINT) AS [AccountID]
		, CAST(NULL AS DateTime) AS [AMA Sign Date]
		, CAST(NULL AS VARCHAR(50)) AS [Sales Rep ID]
		, CAST(NULL AS DateTime) AS [Install Date]
		, CAST(NULL AS VARCHAR(50)) AS [Tech ID]
		, CAST(NULL AS MONEY) AS RMR
		, CAST(NULL AS SMALLINT) AS [Billing Day]
		, CAST(NULL AS SMALLINT) AS [Contract Length]
		, CAST(NULL AS VARCHAR) AS [Panel Type]
		, CAST(NULL AS VARCHAR) AS [System Type]  -- 2-way; cellular; cell/interactvie
		, CAST(NULL AS BIT) AS [Activation Collected]
		, CAST(NULL AS MONEY) AS [Activation Fee]
		, CAST(NULL AS VARCHAR) AS [Paid Full / 3 Months]
		, CAST(NULL AS DATETIME) AS [Cancelled Date]
		, CAST(NULL AS VARCHAR) AS [Cancelled Reason]
		, CAST(NULL AS BIT) AS [Take Over]
		, CAST(NULL AS BIT) AS [Has Existing Equipment]
		, CAST(NULL AS SMALLINT) AS [Credit Score]
		, CAST(NULL AS SMALLINT) AS [Points]
GO
/* TEST */
SELECT * FROM vwAE_CustomerAccountInfoToGP
