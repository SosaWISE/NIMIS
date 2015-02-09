USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_CustomerMasterFileGeneral')
	BEGIN
		PRINT 'Dropping VIEW vwAE_CustomerMasterFileGeneral'
		DROP VIEW dbo.vwAE_CustomerMasterFileGeneral
	END
GO

PRINT 'Creating VIEW vwAE_CustomerMasterFileGeneral'
GO

/****** Object:  View [dbo].[vwAE_CustomerMasterFileGeneral]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_CustomerMasterFileGeneral.sql
**		Name: vwAE_CustomerMasterFileGeneral
**		Desc: 
**			This view is used as a data structure in Subsonic.
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
**		Date: 03/17/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/17/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_CustomerMasterFileGeneral]
AS
	-- Enter Query here
	SELECT
		CustomerMasterFileID
		, CAST(NULL AS BIGINT) AS FkId
		, CAST('CUSTOMER' AS VARCHAR(20)) AS ResultType
		, CAST('' AS VARCHAR(100)) AS AccountTypes
		, CAST('' AS NVARCHAR(250)) AS [Fullname]
		, CAST('' AS NVARCHAR(150)) AS [City]
		, CAST('' AS VARCHAR(50)) AS [Phone]
		, CAST('' AS VARCHAR(255)) AS [Email]
	FROM
		[dbo].[AE_CustomerMasterFiles]
GO
/* TEST */
-- SELECT * FROM vwAE_CustomerMasterFileGeneral
