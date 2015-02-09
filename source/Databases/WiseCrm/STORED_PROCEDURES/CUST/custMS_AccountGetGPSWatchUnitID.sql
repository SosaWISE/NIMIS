USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountGetGPSWatchUnitID')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountGetGPSWatchUnitID'
		DROP  Procedure  dbo.custMS_AccountGetGPSWatchUnitID
	END
GO

PRINT 'Creating Procedure custMS_AccountGetGPSWatchUnitID'
GO
/******************************************************************************
**		File: custMS_AccountGetGPSWatchUnitID.sql
**		Name: custMS_AccountGetGPSWatchUnitID
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
CREATE Procedure dbo.custMS_AccountGetGPSWatchUnitID
(
	@UnitID VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Transfer data */
	SELECT
		*
	FROM
		[WISE_CRM].[dbo].MS_Accounts AS MSA WITH (NOLOCK)
	WHERE
		(MSA.GpsWatchUnitID = @UnitID);

END
GO

GRANT EXEC ON dbo.custMS_AccountGetGPSWatchUnitID TO PUBLIC
GO
/*
EXEC dbo.custMS_AccountGetGPSWatchUnitID '90007200';

SELECT * FROM MS_Accounts WHERE GpsWatchPhoneNumber = '15135107527';
*/
