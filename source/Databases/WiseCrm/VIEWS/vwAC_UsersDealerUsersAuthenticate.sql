USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAC_UsersDealerUsersAuthenticate')
	BEGIN
		PRINT 'Dropping VIEW vwAC_UsersDealerUsersAuthenticate'
		DROP VIEW dbo.vwAC_UsersDealerUsersAuthenticate
	END
GO

PRINT 'Creating VIEW vwAC_UsersDealerUsersAuthenticate'
GO

/****** Object:  View [dbo].[vwAC_UsersDealerUsersAuthenticate]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAC_UsersDealerUsersAuthenticate.sql
**		Name: vwAC_UsersDealerUsersAuthenticate
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
**		Date: 01/18/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/18/2012	Andres Sosa		Created  by
*******************************************************************************/
CREATE VIEW [dbo].[vwAC_UsersDealerUsersAuthenticate]
AS
	/** Query */
	SELECT 
		AU.UserID
		, HRUR.UserID AS HRUserId
		, HRUR.GPEmployeeID
		, AU.Username
		, AU.Password
		, AU.IsActive AS AuIsActive
		, ASS.SessionID
		, ASS.IPAddress
		, ASS.CreatedOn
		, ASS.LastAccessedOn
		, MDU.DealerUserID
		, MDU.DealerUserTypeId
		, MDUT.DealerUserType
		, MDU.DealerId
		, DLR.DealerName
		, MDU.UserID AS McUserId
		, MDU.FullName
		, MDU.Firstname
		, MDU.Middlename
		, MDU.Lastname
		, MDU.Email
		, MDU.PhoneWork
		, MDU.PhoneCell
		, MDU.ADUsername
		, MDU.Username AS McUsername
		, MDU.[Password] AS McPassword
		, MDU.LastLoginOn
		, MDU.IsActive AS MduIsActive
	FROM
		dbo.AC_Users AS AU WITH (NOLOCK)
		INNER JOIN dbo.MC_DealerUsers AS MDU WITH (NOLOCK)
		ON
			(AU.UserID = MDU.AuthUserId)
--			AND (AU.IsActive = 1)
			AND (AU.IsDeleted = 0)
--			AND (MDU.IsActive = 1)
			AND (MDU.IsDeleted = 0)
		INNER JOIN dbo.MC_DealerUserTypes AS MDUT WITH (NOLOCK)
		ON
			(MDU.DealerUserTypeId = MDUT.DealerUserTypeID)
		INNER JOIN dbo.AE_Dealers AS DLR WITH (NOLOCK)
		ON
			(MDU.DealerId = DLR.DealerID)
		INNER JOIN (SELECT
				ASS1.SessionID
				, ASS1.UserId
				, ASS1.IPAddress
				, ASS1.CreatedOn
				, ASS1.LastAccessedOn
				, ROW_NUMBER() OVER(PARTITION BY ASS1.UserId ORDER BY LastAccessedOn DESC) AS [RowNumber]
			FROM
				dbo.AC_Sessions AS ASS1 WITH (NOLOCK)) AS ASS
		ON
			(AU.UserID = ASS.UserId)
			AND (ASS.RowNumber = 1) -- ONLY THE TOP ROW
		LEFT OUTER JOIN [dbo].RU_Users AS HRUR
		ON
			(AU.HRUserId = HRUR.UserID)
GO

/* TEST 
DECLARE @DealerUserID INT
DECLARE @SessionId BIGINT
SET @DealerUserID = 1
SET @SessionId = 
SELECT * FROM vwAC_UsersDealerUsersAuthenticate AS VW WHERE VW.UserID = 1
WHERE
	(VW.DealerUserID = @DealerUserID)
	AND (VW.SessionID = @SessionId)
*/