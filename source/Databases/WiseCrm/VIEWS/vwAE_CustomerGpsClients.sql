USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerGpsClients')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerGpsClients'
		DROP VIEW dbo.vwAE_CustomerGpsClients
	END
GO

PRINT 'Creating VIEW vwAE_CustomerGpsClients'
GO

/****** Object:  View [dbo].[vwAE_CustomerGpsClients]    Script Date: 06/05/2012 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerGpsClients.sql
**		Name: vwAE_CustomerGpsClients
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
**		Date: 06/05/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	06/05/2012	Andres Sosa	Created
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_CustomerGpsClients]
AS

	/** Enter Query here */
	SELECT
		CUST.*
	FROM
		[dbo].AE_Customers AS CUST WITH (NOLOCK)
		INNER JOIN [dbo].AE_CustomerTypes AS CTYP WITH (NOLOCK)
		ON
			(CUST.CustomerTypeId = CTYP.CustomerTypeID)
			AND (CTYP.CustomerTypeID = 'GPSCLNT') -- Gps Client Customer
UNION
	SELECT
		CUST.*
	FROM
		[dbo].AE_Customers AS CUST WITH (NOLOCK)
		INNER JOIN [dbo].AE_CustomerMasterToCustomer AS CTYP WITH (NOLOCK)
		ON
			(CUST.CustomerID = CTYP.CustomerId)
			AND (CTYP.CustomerTypeID = 'GPSCLNT') -- Gps Client Customer
GO
/* TEST */
-- SELECT * FROM vwAE_CustomerGpsClients