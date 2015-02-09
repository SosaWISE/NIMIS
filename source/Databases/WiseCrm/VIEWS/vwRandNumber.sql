USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwRandNumber')
	BEGIN
		PRINT 'Dropping VIEW vwRandNumber'
		DROP VIEW dbo.vwRandNumber
	END
GO

PRINT 'Creating VIEW vwRandNumber'
GO

/****** Object:  View [dbo].[vwRandNumber]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwRandNumber.sql
**		Name: vwRandNumber
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
**		Date: 01/21/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/21/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwRandNumber]
AS
	-- Enter Query here
	SELECT RAND() rndResult;
GO
/* TEST */
-- SELECT * FROM vwRandNumber
