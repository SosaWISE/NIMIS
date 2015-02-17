USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountEquipmentInfoToGP')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountEquipmentInfoToGP'
		DROP VIEW dbo.vwMS_AccountEquipmentInfoToGP
	END
GO

PRINT 'Creating VIEW vwMS_AccountEquipmentInfoToGP'
GO

/****** Object:  View [dbo].[vwMS_AccountEquipmentInfoToGP]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountEquipmentInfoToGP.sql
**		Name: vwMS_AccountEquipmentInfoToGP
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
CREATE VIEW [dbo].[vwMS_AccountEquipmentInfoToGP]
AS
	-- Enter Query here
	SELECT
		CAST(NULL AS BIGINT) AS [AccountID]
		, CAST(NULL AS BIGINT) AS [CustomerMasterFileID]
		, CAST(NULL AS BIGINT) AS [Invoice ID]
		, CAST(NULL AS VARCHAR(20)) AS [Invoice Type]
		, CAST(NULL AS VARCHAR(100)) AS [Product Sku]
		, CAST(NULL AS BIT) AS [Is Upgrade]
		, CAST(NULL AS BIT) AS [Is Existing Equipment]
		, CAST(NULL AS VARCHAR(100)) AS [Rep Tech Cust Upgrade]  -- This shows who is paying for the upgrade.
		
GO
/* TEST */
SELECT * FROM vwMS_AccountEquipmentInfoToGP
