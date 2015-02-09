USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwLP_CommandMessageEAVRSP4s')
	BEGIN
		PRINT 'Dropping VIEW vwLP_CommandMessageEAVRSP4s'
		DROP VIEW dbo.vwLP_CommandMessageEAVRSP4s
	END
GO

PRINT 'Creating VIEW vwLP_CommandMessageEAVRSP4s'
GO

/****** Object:  View [dbo].[vwLP_CommandMessageEAVRSP4s]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwLP_CommandMessageEAVRSP4s.sql
**		Name: vwLP_CommandMessageEAVRSP4s
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
CREATE VIEW [dbo].[vwLP_CommandMessageEAVRSP4s]
AS

	/** Enter Query here */
	SELECT
		CMD.*
	FROM
		[dbo].LP_CommandMessageEAVRSP4s AS CMD WITH (NOLOCK)
	

GO
/* TEST */
-- SELECT * FROM vwLP_CommandMessageEAVRSP4s
