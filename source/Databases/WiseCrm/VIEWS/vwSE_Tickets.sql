USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwSE_Tickets')
	BEGIN
		PRINT 'Dropping VIEW vwSE_Tickets'
		DROP VIEW dbo.vwSE_Tickets
	END
GO

PRINT 'Creating VIEW vwSE_Tickets'
GO

/****** Object:  View [dbo].[vwSE_Tickets]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwSE_Tickets.sql
**		Name: vwSE_Tickets
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
**	07/22/2014	Reagan Descartin		Created by
**  08/26/2014  Reagan				    Added customer info (name, address)
**	10/22/2014	Peter Fry				Commented out "AND (MSAC.[AccountCustomerTypeID]='PRI')" so all tickets would be returned
*******************************************************************************/
CREATE VIEW [dbo].[vwSE_Tickets]
AS
	-- Enter Query here
	
		SELECT
		TICKET.TicketID
		, TICKET.AccountId
		, TICKET.MonitoringStationNo
		, TICKET.TicketTypeId
		, SETT.TicketTypeName
		, TICKET.StatusCodeId
		, SETSS.StatusCode
		, TICKET.MoniConfirmation
		----, SESB.[IsTechConfirmed]
		----, SESB.[DateTechConfirmed]
		----, SESB.TechnicianId
	--	,SESB.[TechnicianId]
		--,RUT.[FullName] AS 'TechnicianName'
		,TICKET.TechnicianId
		,[dbo].[fxGetTechnicianFullNameByTechnicianId](TICKET.[TechnicianId]) AS 'TechnicianName'
		, TICKET.TripCharges
		, TICKET.Appointment
		, TICKET.AgentConfirmation
		, TICKET.ExpirationDate
		, TICKET.Notes
		, TICKET.IsTechEnRoute
		, TICKET.IsTechDelayed
		, TICKET.IsTechCompleted
		, TICKET.ConfirmationNo
		, TICKET.ClosingNote
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
		----,SEST.[BlockId]
		----,SEST.[AppointmentDate]
		----,SEST.[TravelTime]
		----,SESB.[ZipCode]
		----,SESB.[MaxRadius]
		----,SESB.[Distance]
		----,SESB.[StartTime]
		----,SESB.[EndTime]


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
--			(
			(TICKET.AccountID = MSAC.AccountId)  
--			AND (MSAC.[AccountCustomerTypeID]='PRI')
--			)
		INNER JOIN [dbo].[AE_Customers] AS AEC WITH (NOLOCK)
		ON
			(AEC.[CustomerID] = MSAC.[CustomerId]) 
		INNER JOIN [dbo].[MC_Addresses] AS MCA WITH (NOLOCK)
		ON
		(AEC.[AddressId] = MCA.[AddressID])
		
		WHERE
		((AEC.[CustomerTypeId] = 'PRI') AND (TICKET.IsDeleted = 0))
		
		





	
	
	
GO
/* TEST */
--SELECT * FROM vwSE_Tickets WHERE TicketId = 10302

/*

	select * from [dbo].[SE_Tickets]

	select * from [dbo].[MS_AccountCustomerTypes]

	select * from [dbo].[MS_AccountCustomers]
	select * from [dbo].[MS_Accounts]

	select * from [dbo].[AE_Customers]

	select * from [dbo].[QL_Leads]

	select * from vwSE_Tickets where ticketid=10420
	select * from vwSE_AccountTickets where ticketid=10420

	select * from vwSE_Tickets where accountid=151147

	select * from [dbo].[MS_AccountCustomers] where accountid=151147
	
	customer number  = 3051442


*/

/*SELECT
		TICKET.TicketID
		, TICKET.AccountId
		, TICKET.MonitoringStationNo
		, TICKET.TicketTypeId
		, SETT.TicketTypeName
		, TICKET.StatusCodeId
		, SETSS.StatusCode
		, TICKET.MoniConfirmation
		----, SESB.[IsTechConfirmed]
		----, SESB.[DateTechConfirmed]
		----, SESB.TechnicianId
	--	,SESB.[TechnicianId]
		--,RUT.[FullName] AS 'TechnicianName'
		,TICKET.TechnicianId
		,[dbo].[fxGetTechnicianFullNameByTechnicianId](TICKET.[TechnicianId]) AS 'TechnicianName'
		, TICKET.TripCharges
		, TICKET.Appointment
		, TICKET.AgentConfirmation
		, TICKET.ExpirationDate
		, TICKET.Notes
		, TICKET.IsTechEnRoute
		, TICKET.IsTechDelayed
		, TICKET.IsTechCompleted
		, TICKET.ConfirmationNo
		, TICKET.ClosingNote
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
		----,SEST.[BlockId]
		----,SEST.[AppointmentDate]
		----,SEST.[TravelTime]
		----,SESB.[ZipCode]
		----,SESB.[MaxRadius]
		----,SESB.[Distance]
		----,SESB.[StartTime]
		----,SESB.[EndTime]


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
		((MSAC.[AccountCustomerTypeId] = MSACT.[AccountCustomerTypeID])  AND (MSACT.[AccountCustomerTypeID]='PRI'))
		LEFT JOIN [dbo].[AE_Customers] AS AEC WITH (NOLOCK)
		ON
			(AEC.[CustomerID] = MSAC.[CustomerId]) --AND (AEC.[CustomerTypeId] = 'PRI') 
		LEFT JOIN [dbo].[MC_Addresses] AS MCA WITH (NOLOCK)
		ON
		(AEC.[AddressId] = MCA.[AddressID])
		
		WHERE
		((AEC.[CustomerTypeId] = 'PRI') AND (TICKET.IsDeleted = 0))
		
		*/

