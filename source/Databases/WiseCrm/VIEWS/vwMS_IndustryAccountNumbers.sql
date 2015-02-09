USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_IndustryAccountNumbers')
	BEGIN
		PRINT 'Dropping VIEW vwMS_IndustryAccountNumbers'
		DROP VIEW dbo.vwMS_IndustryAccountNumbers
	END
GO

PRINT 'Creating VIEW vwMS_IndustryAccountNumbers'
GO

/****** Object:  View [dbo].[vwMS_IndustryAccountNumbers]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_IndustryAccountNumbers.sql
**		Name: vwMS_IndustryAccountNumbers
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
**		Date: 02/26/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/26/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_IndustryAccountNumbers]
AS
	-- Enter Query here
	SELECT
		mia.IndustryAccountID
		, mia.AccountId
		, mrl.ReceiverLineId
		, mia.ReceiverLineBlockId
		, CASE 
			WHEN mia.IsMove = 1 THEN 'M_'
			WHEN mia.IsTakeOver = 1 THEN 'T_'
			ELSE ''
		  END + LTRIM(mrl.Designator) + RTRIM(LTRIM(mia.SubscriberId)) AS [IndustryAccount]
		, LTRIM(mrl.Designator) AS Designator
		, RTRIM(LTRIM(mia.SubscriberId)) AS SubscriberNumber
		, mrl.ReceiverNumber
		, mia.IsActive
		, mia.IsDeleted
	FROM
		MS_IndustryAccounts AS mia WITH (NOLOCK)
		INNER JOIN MS_ReceiverLines AS mrl WITH (NOLOCK)
		ON
			mia.ReceiverLineID = mrl.ReceiverLineID

GO
/* TEST */
SELECT * FROM vwMS_IndustryAccountNumbers
