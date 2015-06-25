USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerAccounts')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerAccounts'
		DROP VIEW dbo.vwAE_CustomerAccounts
	END
GO

PRINT 'Creating VIEW vwAE_CustomerAccounts'
GO

/****** Object:  View [dbo].[vwAE_CustomerAccounts]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerAccounts.sql
**		Name: vwAE_CustomerAccounts
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
**		Date: 06/25/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/25/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_CustomerAccounts]
AS
	-- Enter Query here
	SELECT
		AEC.CustomerMasterFileId,
		AECA.CustomerAccountID ,
		AECA.LeadId ,
		AECA.AccountId ,
		AECA.CustomerId ,
		AECA.CustomerTypeId ,
		AECA.CreatedOn ,
		AECA.CreatedBy ,
		AECA.DEX_ROW_TS
	FROM
		dbo.AE_CustomerAccounts AS AECA WITH (NOLOCK)
		INNER JOIN dbo.AE_Customers AS AEC WITH (NOLOCK)
		ON
			(AEC.CustomerID = AECA.CustomerId)
			
GO
/* TEST */
-- SELECT * FROM vwAE_CustomerAccounts
