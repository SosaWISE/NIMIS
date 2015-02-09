USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwTS_ServiceTicketStatus')
	BEGIN
		PRINT 'Dropping VIEW vwTS_ServiceTicketStatus'
		DROP VIEW dbo.vwTS_ServiceTicketStatus
	END
GO

PRINT 'Creating VIEW vwTS_ServiceTicketStatus'
GO

/****** Object:  View [dbo].[vwTS_ServiceTicketStatus]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwTS_ServiceTicketStatus.sql
**		Name: vwTS_ServiceTicketStatus
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
**		Auth: Aaron Shumway
**		Date: 1/01/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	
*******************************************************************************/
CREATE VIEW [dbo].[vwTS_ServiceTicketStatus]
AS
	SELECT
		ST.ID
		, ST.CreatedOn
		, ST.CreatedBy
		, ST.ModifiedOn
		, ST.ModifiedBy
		, ST.IsDeleted
		, ST.[Version]
		, ST.ServiceTypeId
		, ST.AccountId
		, ST.CurrentAppointmentId
		, ST.Notes
		, ST.CompletedNote
		, ST.CompletedOn
		, ST.MSTicketNum
		, ST.MSConfirmation
		, ST.DealerConfirmation

		-- Appointment
		, APPT.ID AS AppointmentId
		, APPT.TechId
		, RU.FullName AS TechFullName
		, APPT.StartOn
		, APPT.EndOn
		, APPT.TravelTime
		, APPT.TechEnRouteOn

		-- Master File
		, C.CustomerMasterFileId
		--
		--, dbo.fxFormatFullName(C.Prefix, C.FirstName, C.MiddleName, C.LastName, C.Postfix) AS CustomerName
		, C.FirstName
		, C.MiddleName
		, C.LastName
		--
		, A.StreetAddress
		, A.StreetAddress2
		, A.City
		, A.StateId
		, A.PostalCode
		--
		, A.Latitude
		, A.Longitude

		-- Tech
		, RU.GPEmployeeID AS TechGPEmployeeID

		-- Status
		, (CASE
			WHEN (ST.IsDeleted = 1) THEN 8 -- Deleted
			--WHEN (ST.CompletedOn IS NOT NULL) AND (ST.EquipmentAdded = 1 AND ST.EquipmentNotificationNeeded IS NULL) THEN 7 -- Waiting Paperwork
			WHEN (ST.CompletedOn IS NOT NULL) THEN 5 -- Completed
			--WHEN (ST.TechConfirmOn IS NOT NULL) THEN 4 -- TechConfirmation
			WHEN (APPT.ID IS NULL) THEN 1 -- Open
			WHEN DATEADD(HOUR, 4, APPT.EndOn) < GETUTCDATE() THEN 3 -- OverDue
			--WHEN (APPT.IsDeleted = 0) AND (T.TechTypeId = 3) THEN 6 -- Pending Contractor
			WHEN (APPT.IsDeleted = 0) THEN 2 -- Pending
			ELSE NULL
		END) AS StatusCodeId
	FROM TS_ServiceTickets AS ST WITH(NOLOCK)
	
	-- Master File
	INNER JOIN AE_CustomerAccounts AS CA
	ON
		ST.AccountId = CA.AccountId
	INNER JOIN AE_Customers AS C
	ON
		CA.CustomerId = C.CustomerID
		AND (C.[CustomerTypeId] = 'PRI')
	INNER JOIN [dbo].[MC_Addresses] AS A WITH (NOLOCK)
	ON
		(C.AddressId = A.AddressID)
	
	-- Appointment
	LEFT OUTER JOIN TS_Appointments AS APPT WITH(NOLOCK)
	ON
		(ST.CurrentAppointmentId = APPT.ID)
	LEFT OUTER JOIN TS_Techs AS T WITH(NOLOCK)
	ON
		(APPT.TechId = T.ID)
	LEFT OUTER JOIN [WISE_HumanResource].[dbo].[RU_Recruits] AS RR WITH (NOLOCK)
	ON
		(T.RecruitId = RR.RecruitID)
	LEFT OUTER JOIN [WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
	ON
		(RR.UserID = RU.UserID)

	--WHERE
	--	(ST.IsDeleted = 0)


GO
/* TEST */
-- SELECT * FROM vwTS_ServiceTicketStatus



