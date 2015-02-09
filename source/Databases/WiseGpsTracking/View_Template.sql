USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'View_Template')
	BEGIN
		PRINT 'Dropping VIEW View_Template'
		DROP VIEW dbo.View_Template
	END
GO

PRINT 'Creating VIEW View_Template'
GO

/****** Object:  View [dbo].[View_Template]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: View_Template.sql
**		Name: View_Template
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
**		Date: 12/03/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	----------	------------	-----------------------------------------------
**	12/03/2012	Andres Sosa		Created By
*******************************************************************************/
CREATE VIEW [dbo].[View_Template]
AS

	/** Enter Query here */
	SELECT
		*
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
-- SELECT * FROM View_Template
