USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAC_UsersAppAuthentication')
	BEGIN
		PRINT 'Dropping VIEW vwAC_UsersAppAuthentication'
		DROP VIEW dbo.vwAC_UsersAppAuthentication
	END
GO

PRINT 'Creating VIEW vwAC_UsersAppAuthentication'
GO

/****** Object:  View [dbo].[vwAC_UsersAppAuthentication]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAC_UsersAppAuthentication.sql
**		Name: vwAC_UsersAppAuthentication
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
**		Date: 11/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	----------	--------		-------------------------------------------
**	11/05/2013	Andres Sosa		Created  by
**  09/15/2014  Reagan          Added UserEmployeeType info (for checking of user type on client side)
*******************************************************************************/
CREATE VIEW [dbo].[vwAC_UsersAppAuthentication]
AS
	--Enter Query here
	SELECT
		URS.UserID
		, URS.DealerId
		, HRU.UserId AS [HRUserId]
		, HRU.GPEmployeeID
		, URS.SSID
		, URS.Username
		, URS.[Password]
		, HRU.FullName
		, HRU.FirstName
		, HRU.LastName
		, CAST(NULL AS INT) AS [SessionId]
		, HRUET.[UserEmployeeTypeID]
		, HRUET.[UserEmployeeTypeName]
		, dbo.fxGetUserSecurityLevelByUserId(URS.[HRUserId]) AS 'SecurityLevel'
		, URS.IsActive
		, URS.IsDeleted
	FROM
		[dbo].AC_Users AS URS WITH (NOLOCK)
		INNER JOIN [dbo].RU_Users AS HRU WITH (NOLOCK)
		ON
			(URS.HRUserID = HRU.UserID)
		INNER JOIN  [dbo].[RU_UserEmployeeTypes] HRUET
		ON
		(HRUET.[UserEmployeeTypeID] = HRU.[UserEmployeeTypeId])


		




GO
/* TEST 
DECLARE @username VARCHAR(50) = 'dethington';
DECLARE @password VARCHAR(20) = 'NexSense';
DECLARE @SessionId BIGINT = 282327;
DECLARE @ApplicationId VARCHAR(50) = 'SSE_CMS_CORS';

SELECT 
	*
FROM
	vwAC_UsersAppAuthentication AS USR
	INNER JOIN [dbo].AC_UserACLs AS ACL WITH (NOLOCK)
	ON
		(USR.UserID = ACL.UserId)
WHERE
	(USR.Username = @username)
--	AND (USR.[Password] = @password)
--	AND (ACL.ApplicationId = @ApplicationId)
	AND (USR.IsActive = 1) AND (USR.IsDeleted = 0);


	SELECT * FROM [dbo].[vwAC_UsersAppAuthentication]

*/