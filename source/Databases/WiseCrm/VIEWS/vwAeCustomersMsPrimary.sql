USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAeCustomersMsPrimary')
	BEGIN
		PRINT 'Dropping VIEW vwAeCustomersMsPrimary'
		DROP VIEW dbo.vwAeCustomersMsPrimary
	END
GO

PRINT 'Creating VIEW vwAeCustomersMsPrimary'
GO

/****** Object:  View [dbo].[vwAeCustomersMsPrimary]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAeCustomersMsPrimary.sql
**		Name: vwAeCustomersMsPrimary
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
**		Date: 10/14/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	10/14/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAeCustomersMsPrimary]
AS
	-- Enter Query here
	SELECT
		MAC.AccountId
		, CUST.*
	FROM
		[dbo].[MS_AccountCustomers] AS MAC WITH (NOLOCK)
		INNER JOIN [dbo].AE_Customers AS CUST WITH (NOLOCK)
		ON
			(CUST.CustomerId = MAC.CustomerId)
			AND (MAC.AccountCustomerTypeId = 'PRI')

GO
/* TEST */
-- SELECT * FROM vwAeCustomersMsPrimary
