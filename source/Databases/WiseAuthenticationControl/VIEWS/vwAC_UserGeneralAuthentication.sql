USE [WISE_AuthenticationControl]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAC_UserGeneralAuthentication')
	BEGIN
		PRINT 'Dropping VIEW vwAC_UserGeneralAuthentication'
		DROP VIEW dbo.vwAC_UserGeneralAuthentication
	END
GO

PRINT 'Creating VIEW vwAC_UserGeneralAuthentication'
GO

/****** Object:  View [dbo].[vwAC_UserGeneralAuthentication]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAC_UserGeneralAuthentication.sql
**		Name: vwAC_UserGeneralAuthentication
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
**		Date: 09/15/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	09/15/2012	Andres Sosa		Created  by
*******************************************************************************/
CREATE VIEW [dbo].[vwAC_UserGeneralAuthentication]
AS
	/** Enter Query here */
	
	/** DEALER ACCOUNTS */
	SELECT
		AUS.UserID
		, AUS.Username
		, AUS.[Password]
		, MDU.DealerId
		, MDU.DealerUserID AS [ID]
		, APP.ApplicationID
		, MDU.Username AS [MdlUsername]
		, MDU.Password AS [MdlPassword]
		, APP.ApplicationName
		, APP.WebUrl
		, MDU.LastLoginOn
		, MDR.Username AS [DlrUsername]
		, MDR.[Password] AS [DlrPassword]
		, NULL AS [ClnUsername]
		, NULL AS [ClnPassword]
	FROM 
		dbo.AC_Users AS AUS WITH (NOLOCK)
		INNER JOIN WISE_CRM.dbo.MC_DealerUsers AS MDU WITH (NOLOCK)
		ON
			(AUS.UserID = MDU.AuthUserId)
		INNER JOIN WISE_CRM.dbo.AE_Dealers AS MDR WITH (NOLOCK)
		ON
			(MDU.DealerId = MDR.DealerID)
		INNER JOIN dbo.AC_Applications AS APP WITH (NOLOCK)
		ON
			('SOS_CRM' = APP.ApplicationID)
	UNION
	/** GPS CLIENT ACCOUNTS */
	SELECT
		AUS.UserID
		, AUS.Username
		, AUS.[Password]
		, CUST.DealerId
		, GPSC.CustomerId AS [ID]
		, APP.ApplicationID
		, GPSC.Username AS [MdlUsername]
		, GPSC.Password AS [MdlPassword]
		, APP.ApplicationName
		, APP.WebUrl
		, GPSC.LastLoginOn
		, NULL AS [DlrUsername]
		, NULL AS [DlrPassword]
		, CUST.Username AS [ClnUsername]
		, CUST.Password AS [ClnPassword]
	FROM 
		dbo.AC_Users AS AUS WITH (NOLOCK)
		INNER JOIN WISE_CRM.dbo.AE_CustomerGpsClients AS GPSC WITH (NOLOCK)
		ON
			(AUS.UserID = GPSC.AuthUserId)
		INNER JOIN WISE_CRM.dbo.AE_Customers AS CUST WITH (NOLOCK)
		ON
			(GPSC.CustomerId = CUST.CustomerID)
		INNER JOIN dbo.AC_Applications AS APP WITH (NOLOCK)
		ON
			('SOS_GPS_CLNT' = APP.ApplicationID)
GO
/* TEST */
SELECT * FROM vwAC_UserGeneralAuthentication WHERE Username = 'andres' AND Password = 'wise'
