USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetActivationWaives')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetActivationWaives'
		DROP  Procedure  dbo.custRU_UsersGetActivationWaives
	END
GO

PRINT 'Creating Procedure custRU_UsersGetActivationWaives'
GO
/******************************************************************************
**		File: custRU_UsersGetActivationWaives.sql
**		Name: custRU_UsersGetActivationWaives
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: 
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:				Description:
**	-----------	-------------		-------------------------------------------
**	12/05/2013	Carly Christiansen	Created by
*******************************************************************************/
CREATE Procedure dbo.custRU_UsersGetActivationWaives
(@UserID INT
	, @SeasonID INT
)
AS
BEGIN
	
--	DECLARE @SeasonID INT
--	SET @SeasonID = 6
--
--	DECLARE @UserID NVarChar(25)
--	SET @UserID = 4146

	SELECT
		COUNT(*) AS ActWaives
	FROM dbo.SAE_ValidSales AS VS WITH(NOLOCK)
	INNER JOIN Platinum_Protection_InterimCRM.dbo.MS_Account MSA WITH (NOLOCK)
	ON
		VS.AccountID = MSA.AccountID
	WHERE
		(MSA.ActivationFee = 0)
		AND (VS.InstallDate IS NOT NULL)
		AND (VS.SalesRepUserID = @UserID)
		AND (VS.SeasonID = @SeasonID)

END
GO

GRANT EXEC ON dbo.custRU_UsersGetActivationWaives TO PUBLIC
GO