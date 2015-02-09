USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwSE_AccountTickets')
	BEGIN
		PRINT 'Dropping VIEW vwSE_AccountTickets'
		DROP VIEW dbo.vwSE_AccountTickets
	END
GO

PRINT 'Creating VIEW vwSE_AccountTickets'
GO

/****** Object:  View [dbo].[vwSE_AccountTickets]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwSE_AccountTickets.sql
**		Name: vwSE_AccountTickets
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
**	09/18/2014	Reagan Descartin		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwSE_AccountTickets]
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
		--, [dbo].[fxFormatFullName](
		--	AEC.[Prefix]
		--	, AEC.[FirstName]
		--	, AEC.[MiddleName]
		--	, AEC.[LastName]
		--	, AEC.[Postfix]
		--	) AS 'CustomerFullName'
		--, AEC.[CustomerMasterFileId]
		--,[dbo].[fxGetAddressCityStatePostalCode](
		--		MCA.[City]
		--		, MCA.[County]
		--		, MCA.[PostalCode]
		--		, MCA.[PlusFour]
		--	) AS 'Address'
		--,MCA.[StreetAddress]
		--,MCA.[StreetAddress2]
		--,MCA.[County]
		--,MCA.[City]
		--,MCA.[PostalCode]
		--,[dbo].[fxGetAddressCityStatePostalCode](MCA.City, MCA.StateId, MCA.PostalCode, null) AS [CityStateZip]
		--,MCA.[StreetAddress]+' '+ MCA.[City]+', '+MCA.[StateId]+' '+MCA.[PostalCode] AS [CompleteAddress] 
		--,MCA.[Latitude]
		--,MCA.[Longitude]
		--,AEC.PhoneHome
		--,AEC.PhoneMobile
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
		--INNER JOIN [dbo].[MS_AccountCustomers] AS MSAC WITH (NOLOCK)
		--ON
		--	(TICKET.AccountID = MSAC.AccountId)
		--INNER JOIN [dbo].[MS_AccountCustomerTypes] MSACT WITH (NOLOCK)
		--ON
		--((MSAC.[AccountCustomerTypeId] = MSACT.[AccountCustomerTypeID])  AND (MSACT.[AccountCustomerTypeID]='PRI'))
		--LEFT JOIN [dbo].[AE_Customers] AS AEC WITH (NOLOCK)
		--ON
		--	(AEC.[CustomerID] = MSAC.[CustomerId]) --AND (AEC.[CustomerTypeId] = 'PRI') 
		--LEFT JOIN [dbo].[MC_Addresses] AS MCA WITH (NOLOCK)
		--ON
		--(AEC.[AddressId] = MCA.[AddressID])
		
		--WHERE
		--((AEC.[CustomerTypeId] = 'PRI') AND (TICKET.IsDeleted = 0))
		
	
	
	
GO
/* TEST */
--SELECT * FROM vwSE_AccountTickets WHERE TicketId = 10302

/*

	select * from [dbo].[SE_Tickets]
*/

