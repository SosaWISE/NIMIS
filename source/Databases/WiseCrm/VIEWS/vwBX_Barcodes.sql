USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwBX_Barcodes')
	BEGIN
		PRINT 'Dropping VIEW vwBX_Barcodes'
		DROP VIEW dbo.vwBX_Barcodes
	END
GO

PRINT 'Creating VIEW vwBX_Barcodes'
GO

/****** Object:  View [dbo].[vwBX_Barcodes]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwBX_Barcodes.sql
**		Name: vwBX_Barcodes
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
CREATE VIEW [dbo].[vwBX_Barcodes]
AS
	-- Enter Query here
	SELECT 
		BXB.*
		,       BBT.Version,
		       	BBP.PrinterName,
				BDT.DocName
	FROM
		[dbo].[BX_Barcodes] AS BXB WITH (NOLOCK)
		INNER JOIN [dbo].[BX_BarcodeTypes] AS BBT WITH (NOLOCK)
		ON
			(BBT.BarcodeTypeID = BXB.BarcodeTypeId)
		INNER JOIN [dbo].[BX_Printers] AS BBP WITH (NOLOCK)
		ON
			(BBT.PrinterId = BBP.PrinterID)
		INNER JOIN [dbo].[BX_DocTypes] AS BDT WITH (NOLOCK)
		ON
			(BBT.DocTypeId = BDT.DocTypeID)
		
		
GO
/* TEST
 SELECT * FROM vwBX_Barcodes WHERE BarcodeID = 100001;  */
