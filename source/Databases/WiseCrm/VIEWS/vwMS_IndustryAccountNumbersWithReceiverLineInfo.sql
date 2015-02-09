USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_IndustryAccountNumbersWithReceiverLineInfo')
	BEGIN
		PRINT 'Dropping VIEW vwMS_IndustryAccountNumbersWithReceiverLineInfo'
		DROP VIEW dbo.vwMS_IndustryAccountNumbersWithReceiverLineInfo
	END
GO

PRINT 'Creating VIEW vwMS_IndustryAccountNumbersWithReceiverLineInfo'
GO

/****** Object:  View [dbo].[vwMS_IndustryAccountNumbersWithReceiverLineInfo]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_IndustryAccountNumbersWithReceiverLineInfo.sql
**		Name: vwMS_IndustryAccountNumbersWithReceiverLineInfo
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
**		Date: 01/21/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/21/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_IndustryAccountNumbersWithReceiverLineInfo]
AS
	-- Enter Query here
	SELECT
		MIA.IndustryAccountID
		, MIA.AccountId
		, MIA.ReceiverNumber
		, MIA.Designator
		, MIA.SubscriberNumber
		, MIA.IndustryAccount
		, MSOS.MonitoringStationOSID
		, MSOS.OSDescription
		, MMS.MonitoringStationName
		, CASE 
			WHEN MAC.IndustryAccountId = MIA.IndustryAccountID THEN 'Yes'
			ELSE 'No'
		  END AS [PrimaryCSID]
		, CASE 
			WHEN MAC.IndustryAccount2Id = MIA.IndustryAccountID THEN 'Yes'
			ELSE 'No'
		  END AS [SecondaryCSID]
	FROM 
		dbo.vwMS_IndustryAccountNumbers AS MIA WITH (NOLOCK)
		INNER JOIN dbo.MS_Accounts AS MAC WITH (NOLOCK)
		ON
			(MAC.AccountID = MIA.AccountId)
			--AND MIA.AccountId = 170964
		INNER JOIN dbo.MS_ReceiverLines AS RL WITH (NOLOCK)
		ON
			(MIA.ReceiverLineId = RL.ReceiverLineID)
		INNER JOIN dbo.MS_MonitoringStationOSs AS MSOS WITH (NOLOCK)
		ON
			(MSOS.MonitoringStationOSID = RL.MonitoringStationOSId)
		INNER JOIN dbo.MS_MonitoringStations AS MMS WITH (NOLOCK)
		ON
			(MMS.MonitoringStationsID = MSOS.MonitoringStationId)
GO
/* TEST */
-- SELECT * FROM vwMS_IndustryAccountNumbersWithReceiverLineInfo
