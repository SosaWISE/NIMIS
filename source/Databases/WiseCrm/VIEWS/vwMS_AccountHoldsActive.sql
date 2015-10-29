USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountHoldsActive')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountHoldsActive'
		DROP VIEW dbo.vwMS_AccountHoldsActive
	END
GO

PRINT 'Creating VIEW vwMS_AccountHoldsActive'
GO

/****** Object:  View [dbo].[vwMS_AccountHoldsActive]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountHoldsActive.sql
**		Name: vwMS_AccountHoldsActive
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
**		Date: 10/29/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	10/29/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountHoldsActive]
AS
	-- Enter Query here
	SELECT
		MAH.AccountHoldID
        , MAH.AccountId
        , MAHC2.Catg2ID
        , MAHC2.CatgName
        , MAHC2.Catg1Id
        , MAHC2.RecruitFriendlyName
        , MAHC2.CatgDescription
        , MAHC2.IsRepFrontEndHold
        , MAHC2.IsRepBackEndHold
        , MAHC2.IsTechFrontEndHold
        , MAHC2.IsTechBackEndHold
        , MAHC2.PreventsContractSale
        , MAHC2.IsAccountFlag
	FROM
		[dbo].[MS_AccountHolds] AS MAH WITH (NOLOCK)
		INNER JOIN [dbo].[MS_AccountHoldCatg2] AS MAHC2 WITH (NOLOCK)
		ON
			(MAHC2.Catg2ID = MAH.Catg2Id)
	WHERE
		(MAH.IsActive = 'TRUE');
GO
/* TEST */
-- SELECT * FROM vwMS_AccountHoldsActive
