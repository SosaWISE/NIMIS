USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_IndustryNumberByCallerId')
	BEGIN
		PRINT 'Dropping VIEW vwMS_IndustryNumberByCallerId'
		DROP VIEW dbo.vwMS_IndustryNumberByCallerId
	END
GO

PRINT 'Creating VIEW vwMS_IndustryNumberByCallerId'
GO

/****** Object:  View [dbo].[vwMS_IndustryNumberByCallerId]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_IndustryNumberByCallerId.sql
**		Name: vwMS_IndustryNumberByCallerId
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
CREATE VIEW [dbo].[vwMS_IndustryNumberByCallerId]
AS
	/** Query */
	SELECT
		MAS.AccountID
		, IAN.IndustryAccountID
		, MRL.ReceiverLineID
		, MRLB.ReceiverLineBlockID
		, GAS.GpsWatchPhoneNumber
		, IAN.Csid
		, MMS.MonitoringStationsID
		, MMS.MonitoringStationName
		, MMSO.MonitoringStationOSID
		, MMSO.OSDescription
		, IAN.InTestMode
	FROM
		dbo.MS_Accounts AS MAS WITH (NOLOCK)
		INNER JOIN dbo.MS_IndustryAccounts AS IAN WITH (NOLOCK)
		ON
			(MAS.IndustryAccountId = IAN.IndustryAccountID)
			AND (MAS.IsActive = 1) AND (MAS.IsDeleted = 0)
			AND (IAN.IsActive = 1) AND (IAN.IsDeleted = 0)
		INNER JOIN dbo.MS_ReceiverLines AS MRL WITH (NOLOCK)
		ON
			(IAN.ReceiverLineId = MRL.ReceiverLineID)
		INNER JOIN dbo.MS_ReceiverLineBlocks AS MRLB WITH (NOLOCK)
		ON
			(IAN.ReceiverLineBlockId = MRLB.ReceiverLineBlockID)
			AND (MRLB.IsActive = 1) AND (MRLB.IsDeleted = 0)
		INNER JOIN dbo.MS_MonitoringStationOSs AS MMSO WITH (NOLOCK)
		ON
			(MRL.MonitoringStationOSId = MMSO.MonitoringStationOSID)
		INNER JOIN dbo.MS_MonitoringStations AS MMS WITH (NOLOCK)
		ON
			(MMSO.MonitoringStationId = MMS.MonitoringStationsID)
		LEFT OUTER JOIN dbo.GS_Accounts AS GAS WITH (NOLOCK)
		ON
			(GAS.AccountID = MAS.AccountID)
	

GO


