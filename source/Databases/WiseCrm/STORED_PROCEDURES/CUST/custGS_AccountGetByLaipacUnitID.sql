USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_AccountGetByLaipacUnitID')
	BEGIN
		PRINT 'Dropping Procedure custGS_AccountGetByLaipacUnitID'
		DROP  Procedure  dbo.custGS_AccountGetByLaipacUnitID
	END
GO

PRINT 'Creating Procedure custGS_AccountGetByLaipacUnitID'
GO
/******************************************************************************
**		File: custGS_AccountGetByLaipacUnitID.sql
**		Name: custGS_AccountGetByLaipacUnitID
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
**		Auth: Andres Sosa
**		Date: 11/18/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	11/18/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custGS_AccountGetByLaipacUnitID
(
	@UnitID VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Transfer data */
	SELECT
		MSA.*
	FROM
		[WISE_GPSTRACKING].[dbo].LP_Devices AS DEV WITH (NOLOCK)
		INNER JOIN [WISE_CRM].[dbo].GS_Accounts AS MSA WITH (NOLOCK)
		ON
			(DEV.AccountID = MSA.AccountID)
	WHERE
		(DEV.UnitID = @UnitID);

END
GO

GRANT EXEC ON dbo.custGS_AccountGetByLaipacUnitID TO PUBLIC
GO
/*
EXEC dbo.custGS_AccountGetByLaipacUnitID '90007200';
SELECT * FROM [WISE_GPSTRACKING].[dbo].LP_Devices AS DEV WITH (NOLOCK) WHERE UnitID = 80003902;
*/
