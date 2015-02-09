USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGS_EventsLast')
	BEGIN
		PRINT 'Dropping VIEW vwGS_EventsLast'
		DROP VIEW dbo.vwGS_EventsLast
	END
GO

PRINT 'Creating VIEW vwGS_EventsLast'
GO

/****** Object:  View [dbo].[vwGS_EventsLast]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwGS_EventsLast.sql
**		Name: vwGS_EventsLast
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
**		Date: 06/26/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	----------	------------	-----------------------------------------------
**	06/26/2013	Andres Sosa		Created By
*******************************************************************************/
CREATE VIEW [dbo].[vwGS_EventsLast]
AS

	/** Enter Query here */
	SELECT
		GEN.EventID
		, GEN.EventTypeId
		, GEN.AccountId
		, GEN.EventName
		, GEN.EventDate
		, GEN.Lattitude
		, GEN.Longitude
		, GEN.Speed
		, GEN.Course
		, GEN.CurrentMilage
	FROM
		(
			SELECT
				*
				, ROW_NUMBER() OVER (PARTITION BY AccountID ORDER BY EventDate DESC) AS RN
			FROM
				[WISE_GPSTRACKING].[dbo].[GS_Events]
		) AS GEN
	WHERE
		(GEN.RN = 1);	

GO
/* TEST */
-- SELECT * FROM vwGS_EventsLast
