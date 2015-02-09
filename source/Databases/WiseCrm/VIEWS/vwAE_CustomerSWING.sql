USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerSWING')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerSWING'
		DROP VIEW dbo.vwAE_CustomerSWING
	END
GO

PRINT 'Creating VIEW vwAE_CustomerSWING'
GO

/****** Object:  View [dbo].[vwAE_CustomerSWING]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerSWING.sql
**		Name: vwAE_CustomerSWING
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
**	04/21/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_CustomerSWING]
AS

	-- Enter Query here
	SELECT
		CAST('' AS VARCHAR(50)) AS Salutation
		, CAST('' AS VARCHAR(50)) AS FirstName
		, CAST('' AS VARCHAR(50)) AS MiddleName
		, CAST('' AS VARCHAR(50)) AS LastName
		, CAST('' AS VARCHAR(50)) AS Suffix
		, CAST('' AS VARCHAR(50)) AS SSN
		, CAST(GetDate() AS DATETIME) AS DOB
		, CAST('' AS VARCHAR(255)) AS Email

GO
/* TEST */
-- SELECT * FROM vwAE_CustomerSWING
