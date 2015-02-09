USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMS_AccountDispatchAgencyAssignment')
	BEGIN
		PRINT 'Dropping VIEW vwMS_AccountDispatchAgencyAssignment'
		DROP VIEW dbo.vwMS_AccountDispatchAgencyAssignment
	END
GO

PRINT 'Creating VIEW vwMS_AccountDispatchAgencyAssignment'
GO

/****** Object:  View [dbo].[vwMS_AccountDispatchAgencyAssignment]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMS_AccountDispatchAgencyAssignment.sql
**		Name: vwMS_AccountDispatchAgencyAssignment
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
**		Date: 12/05/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/05/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMS_AccountDispatchAgencyAssignment]
AS
	-- Enter Query here
	SELECT
		ADAA.DispatchAgencyAssignmentID
		, ADAA.DispatchAgencyId
		, ADAA.DispatchAgencyTypeId
		, DAT.DispatchAgencyType AS DispatchAgencyTypeName
		, DA.MonitoringStationOSId
		, DA.MsAgencyNumber
		, ADAA.AccountId
		, IA.IndustryAccountID
		, IA.Csid AS CsNo
		, ADAA.DispatchAgencyName
		, ADAA.Phone1
		, ADAA.PermitNumber
		, ADAA.PermitEffectiveDate
		, ADAA.PermitExpireDate
		, ADAA.IsVerified
		, ADAA.IsActive
	FROM
		[dbo].[MS_AccountDispatchAgencyAssignments] AS ADAA WITH (NOLOCK)
		INNER JOIN [dbo].[MS_DispatchAgencies] AS DA WITH (NOLOCK)
		ON
			(DA.DispatchAgencyID = ADAA.DispatchAgencyId)
		INNER JOIN [dbo].[MS_DispatchAgencyTypes] AS DAT WITH (NOLOCK)
		ON
			(DAT.DispatchAgencyTypeID = ADAA.DispatchAgencyTypeId)
		INNER JOIN [dbo].[MS_IndustryAccounts] AS IA WITH (NOLOCK)
		ON
			(IA.IndustryAccountID = ADAA.IndustryAccountId)
GO
/* TEST */
-- SELECT * FROM vwMS_AccountDispatchAgencyAssignment
