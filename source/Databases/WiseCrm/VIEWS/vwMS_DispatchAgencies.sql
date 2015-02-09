USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_DispatchAgencies')
	BEGIN
		PRINT 'Dropping VIEW vwMS_DispatchAgencies'
		DROP VIEW dbo.vwMS_DispatchAgencies
	END
GO

PRINT 'Creating VIEW vwMS_DispatchAgencies'
GO

/****** Object:  View [dbo].[vwMS_DispatchAgencies]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_DispatchAgencies.sql
**		Name: vwMS_DispatchAgencies
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
**		Date: 12/02/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/02/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_DispatchAgencies]
AS
	-- Enter Query here
	SELECT
		DAGC.*
		, DTYP.DispatchAgencyType
	FROM 
		[dbo].[MS_DispatchAgencies] AS DAGC WITH (NOLOCK) 
		INNER JOIN [dbo].[MS_DispatchAgencyTypes] AS DTYP WITH (NOLOCK)
		ON
			(DAGC.DispatchAgencyTypeId = DTYP.DispatchAgencyTypeId)

GO
/* TEST */
-- SELECT * FROM vwMS_DispatchAgencies
