USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGS_Events')
	BEGIN
		PRINT 'Dropping VIEW vwGS_Events'
		DROP VIEW dbo.vwGS_Events
	END
GO

PRINT 'Creating VIEW vwGS_Events'
GO

/****** Object:  View [dbo].[vwGS_Events]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwGS_Events.sql
**		Name: vwGS_Events
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
**		Date: 09/12/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	09/12/2012	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwGS_Events]
AS
	/** Enter Query here */
	SELECT 
		EVN.EventID
		, EVN.EventTypeId
		, EVT.EventType
		, dbo.fxGetEventTypeUi(EVT.EventTypeId) AS EventTypeUi
		, dbo.fxGetEventDescUi([WISE_CRM].[dbo].fxGetDeviceName(ACT.SystemTypeId, ACT.PanelTypeId, MCA.AccountName), EVT.EventTypeId) [EventShortDesc]
		, EVN.AccountId
		, CUST.CustomerID
		, CUST.CustomerMasterFileId
		, CAST(0 AS BIGINT) AS [GeoFenceId]
		, [WISE_CRM].[dbo].fxGetDeviceName(ACT.SystemTypeId, ACT.PanelTypeId, MCA.AccountName) [AccountName]
		, EVN.EventName
		, EVN.EventDate
		, EVN.Lattitude
		, EVN.Longitude
	FROM
		[dbo].GS_Events AS EVN WITH (NOLOCK)
		INNER JOIN [dbo].GS_EventTypes AS EVT WITH (NOLOCK)
		ON
			(EVN.EventTypeId = EVT.EventTypeID)
		INNER JOIN [WISE_CRM].dbo.MS_Accounts AS ACT WITH (NOLOCK)
		ON
			(EVN.AccountId = ACT.AccountID)
		INNER JOIN [WISE_CRM].[dbo].MC_Accounts AS MCA WITH (NOLOCK)
		ON
			(ACT.AccountID = MCA.AccountID)
		INNER JOIN [WISE_CRM].[dbo].[AE_CustomerAccounts] AS ACA WITH (NOLOCK)
		ON
			(ACT.AccountID = ACA.AccountId)
		INNER JOIN [WISE_CRM].[dbo].[AE_Customers] AS CUST WITH (NOLOCK)
		ON
			(ACA.CustomerId = CUST.CustomerID);

GO
/* TEST */
-- SELECT * FROM vwGS_Events