USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwFE_RejectedAccounts')
	BEGIN
		PRINT 'Dropping VIEW vwFE_RejectedAccounts'
		DROP VIEW dbo.vwFE_RejectedAccounts
	END
GO

PRINT 'Creating VIEW vwFE_RejectedAccounts'
GO

/****** Object:  View [dbo].[vwFE_RejectedAccounts]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwFE_RejectedAccounts.sql
**		Name: vwFE_RejectedAccounts
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
**		Date: 03/31/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/31/2015	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwFE_RejectedAccounts]
AS
	-- Enter Query here
	SELECT
		*
	FROM
		[dbo].[FE_RejectedAccounts] AS FEP WITH (NOLOCK)

GO
/* TEST */
-- SELECT * FROM vwFE_RejectedAccounts
