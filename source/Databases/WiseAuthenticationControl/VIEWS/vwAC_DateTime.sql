USE [WISE_AuthenticationControl]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAC_DateTime')
	BEGIN
		PRINT 'Dropping VIEW vwAC_DateTime'
		DROP VIEW dbo.vwAC_DateTime
	END
GO

PRINT 'Creating VIEW vwAC_DateTime'
GO

/****** Object:  View [dbo].[vwAC_DateTime]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAC_DateTime.sql
**		Name: vwAC_DateTime
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
**		Date: 01/18/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/18/2012	Andres Sosa		Created  by
*******************************************************************************/
CREATE VIEW [dbo].[vwAC_DateTime]
AS
	-- Enter Query here
	SELECT 
		GETDATE() AS [LocalDateTime]
		, GETUTCDATE() AS [UTCDateTime];
GO
/* TEST */
--SELECT * FROM vwAC_DateTime
