USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountHolds')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountHolds'
		DROP VIEW dbo.vwMS_AccountHolds
	END
GO

PRINT 'Creating VIEW vwMS_AccountHolds'
GO

/****** Object:  View [dbo].[vwMS_AccountHolds]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountHolds.sql
**		Name: vwMS_AccountHolds
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
**		Date: 07/02/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	07/02/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountHolds]
AS
	-- Enter Query here
	SELECT
		MAH.AccountHoldID
		, MAH.AccountId
		, CAT1.Catg1ID
		, MAH.Catg2Id
		, CAT1.CatgDescription AS [Catg1]
		, CAT2.CatgDescription AS [Catg2]
		, MAH.HoldDescription
		, MAH.FixedNote
		, MAH.FixedBy
		, MAH.FixedOn
		, MAH.IsActive
		, MAH.CreatedBy
		, MAH.CreatedOn
		, MAH.ModifiedBy
		, MAH.ModifiedOn
	FROM
		dbo.MS_AccountHolds AS MAH WITH (NOLOCK)
		INNER JOIN dbo.MS_AccountHoldCatg2 AS CAT2 WITH (NOLOCK)
		ON
			(CAT2.Catg2ID = MAH.Catg2Id)
		INNER JOIN dbo.MS_AccountHoldCatg1 AS CAT1 WITH (NOLOCK)
		ON
			(CAT1.Catg1ID = CAT2.Catg1Id)
	WHERE
		(MAH.IsActive = 'TRUE');
GO

/* TEST */
-- SELECT * FROM vwMS_AccountHolds
