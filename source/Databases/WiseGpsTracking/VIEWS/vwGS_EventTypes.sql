USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGS_EventTypes')
	BEGIN
		PRINT 'Dropping VIEW vwGS_EventTypes'
		DROP VIEW dbo.vwGS_EventTypes
	END
GO

PRINT 'Creating VIEW vwGS_EventTypes'
GO

/****** Object:  View [dbo].[vwGS_EventTypes]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwGS_EventTypes.sql
**		Name: vwGS_EventTypes
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
**		Date: 09/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	----------	------------	-----------------------------------------------
**	09/05/2013	Andres Sosa		Created By
*******************************************************************************/
CREATE VIEW [dbo].[vwGS_EventTypes]
AS

	/** Enter Query here */
	SELECT
		*
	FROM
		[dbo].GS_EventTypes;	

GO
/* TEST */
-- SELECT * FROM vwGS_EventTypes
