USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerMonitoredParty')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerMonitoredParty'
		DROP VIEW dbo.vwAE_CustomerMonitoredParty
	END
GO

PRINT 'Creating VIEW vwAE_CustomerMonitoredParty'
GO

/****** Object:  View [dbo].[vwAE_CustomerMonitoredParty]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerMonitoredParty.sql
**		Name: vwAE_CustomerMonitoredParty
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
**		Date: 01/10/2011
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/10/2011	Andres Sosa	Created
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_CustomerMonitoredParty]
AS

	/** Enter Query here */
	SELECT 
		CUST.*
	FROM
		[dbo].vwAE_CustomerGpsClients AS VW
		INNER JOIN [dbo].AE_Customers AS CUST WITH (NOLOCK)
		ON
			(VW.CustomerID = CUST.CustomerID)
			AND (VW.CustomerTypeId = 'GPSCLNT')

GO
/* TEST */
-- SELECT * FROM vwAE_CustomerMonitoredParty
