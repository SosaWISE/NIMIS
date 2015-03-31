USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_Accounts')
	BEGIN
		PRINT 'Dropping VIEW vwMS_Accounts'
		DROP VIEW dbo.vwMS_Accounts
	END
GO

PRINT 'Creating VIEW vwMS_Accounts'
GO

/****** Object:  View [dbo].[vwMS_Accounts]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_Accounts.sql
**		Name: vwMS_Accounts
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
CREATE VIEW [dbo].[vwMS_Accounts]
AS
	-- Enter Query here
	SELECT
		MSA.AccountID
		, CUST.CustomerMasterFileId
		, AECA.CustomerId
		, MSIA.Csid
	FROM
		[WISE_CRM].[dbo].[MS_Accounts] AS MSA WITH (NOLOCK)
		INNER JOIN [WISE_CRM].[dbo].[AE_CustomerAccounts] AS AECA WITH (NOLOCK)
		ON
			(AECA.AccountId = MSA.AccountID)
		INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS CUST WITH (NOLOCK)
		ON
			(CUST.CustomerID = AECA.CustomerId)
		INNER JOIN [WISE_CRM].[dbo].[MS_IndustryAccounts] AS MSIA WITH (NOLOCK)
		ON
			(MSIA.IndustryAccountID = MSA.IndustryAccountId)

GO
/* TEST
SELECT * FROM vwMS_Accounts
 */