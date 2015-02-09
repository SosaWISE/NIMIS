USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_ReceiverBlockCellDeviceInfo')
	BEGIN
		PRINT 'Dropping VIEW vwMS_ReceiverBlockCellDeviceInfo'
		DROP VIEW dbo.vwMS_ReceiverBlockCellDeviceInfo
	END
GO

PRINT 'Creating VIEW vwMS_ReceiverBlockCellDeviceInfo'
GO

/****** Object:  View [dbo].[vwMS_ReceiverBlockCellDeviceInfo]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_ReceiverBlockCellDeviceInfo.sql
**		Name: vwMS_ReceiverBlockCellDeviceInfo
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
**		Date: 01/10/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/10/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_ReceiverBlockCellDeviceInfo]
AS
	-- Enter Query here
	SELECT
		ReceiverLineBlockID
		, 'ALARMNET' AS Vendor
		, MACAddress
		, CRCNumber
		, CAST(NULL AS BIT) AS IsTwoWay
		, RegisteredDate
		, UnRegisteredDate
	FROM
		[dbo].[MS_ReceiverLineBlockAlarmnet] AS ALMN WITH (NOLOCK)
	UNION
	SELECT
		ReceiverLineBlockID
		, 'ALARMCOM' AS Vendor
		, SerialNumber AS [MACAddress]
		, CAST(CustomerId AS VARCHAR(50)) AS [CRCNumber]
		, IsTwoWay
		, RegisteredDate
		, UnRegisteredDate
	FROM
		[dbo].[MS_ReceiverLineBlockAlarmCom] AS RLBA WITH (NOLOCK)
	UNION
	SELECT
		ReceiverLineBlockID
		, 'TELGUARD' AS Vendor
		, UnitNumber AS [MACAddress]
		, SubcriberNumber AS [CRCNumber]
		, CAST(NULL AS BIT) AS IsTwoWay
		, RegisteredDate
		, UnRegisteredDate
	FROM
		[dbo].[MS_ReceiverLineBlockTelguard] AS RLTG WITH (NOLOCK)

GO
/* TEST */
-- SELECT * FROM vwMS_ReceiverBlockCellDeviceInfo
