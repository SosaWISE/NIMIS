USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMC_Addresses')
	BEGIN
		PRINT 'Dropping VIEW vwMC_Addresses'
		DROP VIEW dbo.vwMC_Addresses
	END
GO

PRINT 'Creating VIEW vwMC_Addresses'
GO

/****** Object:  View [dbo].[vwMC_Addresses]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMC_Addresses.sql
**		Name: vwMC_Addresses
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
**		Date: 06/18/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/18/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMC_Addresses]
AS
	SELECT
		ADR.AddressID
		, ADR.DealerId
		, MPS.CountryId
		, MPC.CountryName
		, ADR.TimeZoneId
		, PTZ.TimeZoneAB
		, PTZ.TimeZoneName
		, ADR.StreetAddress
		, ADR.StreetAddress2
		, ADR.City
		, ADR.StateId
		, MPS.StateAB
		, ADR.PostalCode
		, ADR.PlusFour
		, ADR.County
		, ADR.Phone
		, ADR.Latitude
		, ADR.Longitude
		, ADR.CrossStreet
		--, ADTY.*
	FROM
		[dbo].MC_Addresses AS ADR WITH (NOLOCK)
		--INNER JOIN [dbo].[MC_AddressTypes] AS ADTY WITH (NOLOCK)
		--ON
		--	(ADTY.AddressTypeID = ADR.AddressTypeId)
		INNER JOIN [dbo].[MC_PoliticalCountrys] AS MPC WITH (NOLOCK)
		ON
			(MPC.CountryID = ADR.CountryId)
		INNER JOIN [dbo].[MC_PoliticalStates] AS MPS WITH (NOLOCK)
		ON
			(MPS.StateID = ADR.StateId)
		INNER JOIN [dbo].[MC_PoliticalTimeZones] AS PTZ WITH (NOLOCK)
		ON
			(PTZ.TimeZoneID = ADR.TimeZoneId)

GO
/* TEST */
-- SELECT * FROM vwMC_Addresses
