USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwSE_ScheduleBlockTickets')
	BEGIN
		PRINT 'Dropping VIEW vwSE_ScheduleBlockTickets'
		DROP VIEW dbo.vwSE_ScheduleBlockTickets
	END
GO

PRINT 'Creating VIEW vwSE_ScheduleBlockTickets'
GO

/****** Object:  View [dbo].[vwSE_ScheduleBlockTickets]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwSE_ScheduleBlockTickets.sql
**		Name: vwSE_ScheduleBlockTickets
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
**	09/09/2014	Reagan Descartin		Created by
**	10/22/2014	Peter Fry				Commented out "AND (MSAC.[AccountCustomerTypeID]='PRI')" so all tickets would be returned
*******************************************************************************/
CREATE VIEW [dbo].[vwSE_ScheduleBlockTickets]
AS
	-- Enter Query here
	SELECT
		TICKET.TicketID
		, TICKET.AccountId
		, TICKET.MonitoringStationNo
		, TICKET.TicketTypeId
		, SETT.TicketTypeName
		, SETT.[Weight]
		, TICKET.StatusCodeId
		, SETSS.StatusCode
		, TICKET.MoniConfirmation
		, SESB.[IsTechConfirmed]
		, SESB.[DateTechConfirmed]
		, SESB.TechnicianId
		, TICKET.TripCharges
		, TICKET.Appointment
		, TICKET.AgentConfirmation
		, TICKET.ExpirationDate
		, TICKET.Notes
		, TICKET.IsTechEnRoute
		, TICKET.IsTechDelayed
		, TICKET.IsTechCompleted
		, TICKET.IsDeleted
		, [dbo].[fxFormatFullName](
			AEC.[Prefix]
			, AEC.[FirstName]
			, AEC.[MiddleName]
			, AEC.[LastName]
			, AEC.[Postfix]
			) AS 'CustomerFullName'
		, AEC.[CustomerMasterFileId]
		,[dbo].[fxGetAddressCityStatePostalCode](
				MCA.[City]
				, MCA.[County]
				, MCA.[PostalCode]
				, MCA.[PlusFour]
			) AS 'Address'
		,MCA.[StreetAddress]
		,MCA.[StreetAddress2]
		,MCA.[County]
		,MCA.[City]
		,MCA.[PostalCode]
		,[dbo].[fxGetAddressCityStatePostalCode](MCA.City, MCA.StateId, MCA.PostalCode, null) AS [CityStateZip]
		,MCA.[StreetAddress]+' '+ MCA.[City]+', '+MCA.[StateId]+' '+MCA.[PostalCode] AS [CompleteAddress] 
		,MCA.[Latitude]
		,MCA.[Longitude]
		,AEC.PhoneHome
		,AEC.PhoneMobile
		,SEST.[BlockId]
		,SEST.[AppointmentDate]
		,SEST.[TravelTime]
		,SESB.[ZipCode]
		,SESB.[MaxRadius]
		,SESB.[Distance]
		,SESB.[StartTime]
		,SESB.[EndTime]
		,SEST.ScheduleTicketID AS 'ScheduleTicketId'
		,SEST.IsDeleted AS 'ScheduleTicketDeleted'


	FROM
		[dbo].[SE_Tickets] AS TICKET WITH (NOLOCK)
		INNER JOIN [dbo].[SE_TicketTypes] AS SETT WITH (NOLOCK)
		ON
			(TICKET.TicketTypeID = SETT.TicketTypeID)
		INNER JOIN [dbo].[SE_TicketStatusCodes] AS SETSS WITH (NOLOCK)
		ON
			(TICKET.StatusCodeID = SETSS.[StatusCodeID]) 
		INNER JOIN [dbo].[MS_AccountCustomers] AS MSAC WITH (NOLOCK)
		ON
			(TICKET.AccountID = MSAC.AccountId)
		INNER JOIN [dbo].[MS_AccountCustomerTypes] MSACT WITH (NOLOCK)
		ON
--		(
		(MSAC.[AccountCustomerTypeId] = MSACT.[AccountCustomerTypeID])  
--		AND (MSACT.[AccountCustomerTypeID]='PRI')
--		)
		INNER JOIN [dbo].[AE_Customers] AS AEC WITH (NOLOCK)
		ON
			(AEC.[CustomerID] =  MSAC.[CustomerId]) AND (AEC.[CustomerTypeId] = 'PRI') 
		LEFT JOIN [dbo].[MC_Addresses] AS MCA WITH (NOLOCK)
		ON
			(AEC.[AddressId] = MCA.[AddressID])
		LEFT JOIN [dbo].[SE_ScheduleTickets] SEST WITH (NOLOCK)
		ON
			((SEST.[TicketId] = TICKET.[TicketID]) AND (SEST.[IsDeleted] = 0))
		LEFT JOIN [dbo].[SE_ScheduleBlocks] SESB WITH (NOLOCK)
		ON
			((SESB.[BlockID] = SEST.[BlockId]) AND (SESB.[IsDeleted]=0))

	WHERE
		AEC.[CustomerTypeId] = 'PRI'

	

		
GO
/* TEST
--SELECT * FROM vwSE_ScheduleBlockTickets where ticketId in (
select [TicketID] FROM [dbo].[vwSE_ScheduleBlockTickets] WHERE TechnicianId='SYST001' order by ticketid


 */