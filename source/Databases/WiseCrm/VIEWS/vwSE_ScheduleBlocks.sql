USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwSE_ScheduleBlocks')
	BEGIN
		PRINT 'Dropping VIEW vwSE_ScheduleBlocks'
		DROP VIEW dbo.vwSE_ScheduleBlocks
	END
GO

PRINT 'Creating VIEW vwSE_ScheduleBlocks'
GO

/****** Object:  View [dbo].[vwSE_ScheduleBlocks]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwSE_ScheduleBlocks.sql
**		Name: vwSE_ScheduleBlocks
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
**		Date: 02/26/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/29/2014	Reagan	Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwSE_ScheduleBlocks]
AS
	-- Enter Query here
	SELECT
		SESB.[BlockID]
		,SESB.[Block]
		,SESB.[ZipCode]
		,SESB.[MaxRadius]
		,SESB.[Distance]
		--,SESB.[StartTime]
		--,SESB.[EndTime]
		,dbo.fxSubtractTimeZoneToDate(SESB.[StartTime],SESB.[ZipCode]) AS 'StartTime'
		,dbo.fxSubtractTimeZoneToDate(SESB.[EndTime],SESB.[ZipCode]) AS 'EndTime'
		,SESB.[AvailableSlots]
		,SESB.[TechnicianId]
		--,RUT.[FullName] AS 'TechnicianName'
		,[dbo].[fxGetTechnicianFullNameByTechnicianId](SESB.[TechnicianId]) AS 'TechnicianName'
		,SESB.[IsTechConfirmed]
		,SESB.[DateTechConfirmed]
		--,SEZC.[Latitude] 'BlockLatitude'
		, [dbo].[fxGetCoordinateByZipCode] (SESB.[ZipCode], 'Latitude') AS 'BlockLatitude'
		--,SEZC.[Longitude] 'BlockLongitude'
		, [dbo].[fxGetCoordinateByZipCode] (SESB.[ZipCode], 'Longitude') AS 'BlockLongitude'
		
		---,TICKET.[Latitude] 'TicketLatitude'
		, [dbo].[fxGetTicketCoordinateByTicketId] (SESB.[CurrentTicketId], 'Latitude') AS 'TicketLatitude'
		
		--,TICKET.[Longitude] 'TicketLongitude'
		, [dbo].[fxGetTicketCoordinateByTicketId] (SESB.[CurrentTicketId], 'Longitude') AS 'TicketLongitude'
		
		,SESB.[CurrentTicketId]
		,SESB.[IsRed]
		,SESB.[Color]
		,SESB.[IsBlocked]
		, [dbo].[fxGetSeTicketCountByBlockId](SESB.[BlockId]) 'NoOfTickets'
		, SESB.IsDeleted
	FROM
		[dbo].[SE_ScheduleBlocks] SESB
		--INNER JOIN
		--[dbo].[SE_ZipCodes] SEZC
		--ON
		--SESB.ZipCode = SEZC.[ZipCode]
		--LEFT JOIN
		--[WISE_HumanResource].[dbo].[vwRU_Technicians] RUT
		--ON
		--SESB.TechnicianId = RUT.TechnicianId
		--LEFT JOIN
		--[dbo].[vwSE_Tickets] TICKET
		--ON
		--TICKET.[TicketID]=SESB.[CurrentTicketId]
	WHERE
		(SESB.IsDeleted = 0)


GO


-- select * from [dbo].[vwSE_ScheduleBlocks]

