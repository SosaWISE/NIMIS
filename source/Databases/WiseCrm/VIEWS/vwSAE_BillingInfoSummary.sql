USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwSAE_BillingInfoSummary')
	BEGIN
		PRINT 'Dropping VIEW vwSAE_BillingInfoSummary'
		DROP VIEW dbo.vwSAE_BillingInfoSummary
	END
GO

PRINT 'Creating VIEW vwSAE_BillingInfoSummary'
GO

/****** Object:  View [dbo].[vwSAE_BillingInfoSummary]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwSAE_BillingInfoSummary.sql
**		Name: vwSAE_BillingInfoSummary
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
**		Date: 01/02/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/02/2014	Andres Sosa		Created
*******************************************************************************/
CREATE VIEW [dbo].[vwSAE_BillingInfoSummary]
AS
	-- Enter Query here
	SELECT
		BIS.SummaryID
		, BIS.CustomerMasterFileId
		, BIS.AccountId
		, MAC.AccountName
		, MAC.AccountDesc
		, BIS.AmountDue
		, BIS.DueDate
		, BIS.NumberOfUnites
	FROM
		[dbo].SAE_BillingInfoSummary AS BIS WITH (NOLOCK)
		INNER JOIN [dbo].MC_Accounts AS MAC WITH (NOLOCK)
		ON
			(MAC.AccountID = BIS.AccountId);
		
GO
/* TEST
SELECT * FROM vwSAE_BillingInfoSummary  */
