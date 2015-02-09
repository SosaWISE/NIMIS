USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwRU_UsersCallerID')
	BEGIN
		PRINT 'Dropping VIEW vwRU_UsersCallerID'
		DROP VIEW dbo.vwRU_UsersCallerID
	END
GO

PRINT 'Creating VIEW vwRU_UsersCallerID'
GO

/****** Object:  View [dbo].[vwRU_UsersCallerID]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwRU_UsersCallerID.sql
**		Name: vwRU_UsersCallerID
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
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	------------	-----------------------------------------------
**	12/05/2013	Andres Sosa		Created
*******************************************************************************/
CREATE VIEW [dbo].[vwRU_UsersCallerID]
AS
	/** Enter Query here */
	SELECT
		RU.UserID
		, RU.GPEmployeeID AS EmployeeID
		, RU.UserEmployeeTypeId
		, [dbo].fxGetPhoneNumberScrubbed(RU.PhoneHome) AS CallerID
	FROM
		RU_Users AS RU WITH (NOLOCK)
	WHERE
		(RU.PhoneHome IS NOT NULL)
	UNION
	SELECT
		RU.UserID
		, RU.GPEmployeeID AS EmployeeID
		, RU.UserEmployeeTypeId
		, [dbo].fxGetPhoneNumberScrubbed(RU.PhoneCell) AS CallerID
	FROM
		RU_Users AS RU WITH (NOLOCK)
	WHERE
		(RU.PhoneCell IS NOT NULL)
GO
/* TEST */
-- SELECT * FROM vwRU_UsersCallerID
