USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwIE_Location')
	BEGIN
		PRINT 'Dropping VIEW vwIE_Location'
		DROP VIEW dbo.vwIE_Location
	END
GO

PRINT 'Creating VIEW vwIE_Location'
GO

/****** Object:  View [dbo].[vwIE_Location]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwIE_Location.sql
**		Name: vwIE_Location
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
**	06/24/2014	Reagan	Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwIE_Location]
AS

	-- Enter Query here
	SELECT
			CAST('' AS VARCHAR(50)) AS LocationID
		,	CAST('' AS VARCHAR(250)) AS LocationName


GO
/* TEST */
-- SELECT * FROM vwIE_Location
