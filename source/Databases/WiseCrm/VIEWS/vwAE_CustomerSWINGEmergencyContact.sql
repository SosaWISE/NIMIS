USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerSWINGEmergencyContact')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerSWINGEmergencyContact'
		DROP VIEW dbo.vwAE_CustomerSWINGEmergencyContact
	END
GO

PRINT 'Creating VIEW vwAE_CustomerSWINGEmergencyContact'
GO

/****** Object:  View [dbo].[vwAE_CustomerSWINGEmergencyContact]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerSWINGEmergencyContact.sql
**		Name: vwAE_CustomerSWINGEmergencyContact
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
**		Date: 04/21/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/22/2014	Junryl/Reagan		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_CustomerSWINGEmergencyContact]
AS

	-- Enter Query here
	SELECT		
		  CAST('' AS VARCHAR(50)) AS FirstName
		, CAST('' AS VARCHAR(50)) AS MiddleInit
		, CAST('' AS VARCHAR(50)) AS LastName
		, CAST('' AS VARCHAR(50)) AS Relationship
		, CAST('' AS VARCHAR(50)) AS PhoneNumber1

GO
/* TEST */
-- SELECT * FROM vwAE_CustomerSWINGEmergencyContact
