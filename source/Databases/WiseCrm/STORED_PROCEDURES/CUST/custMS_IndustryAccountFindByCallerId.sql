USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_IndustryAccountFindByCallerId')
	BEGIN
		PRINT 'Dropping Procedure custMS_IndustryAccountFindByCallerId'
		DROP  Procedure  dbo.custMS_IndustryAccountFindByCallerId
	END
GO

PRINT 'Creating Procedure custMS_IndustryAccountFindByCallerId'
GO
/******************************************************************************
**		File: custMS_IndustryAccountFindByCallerId.sql
**		Name: custMS_IndustryAccountFindByCallerId
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
**		Date: 04/16/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	04/16/2012	Andres Sosa		Created By
**			
*******************************************************************************/
CREATE Procedure dbo.custMS_IndustryAccountFindByCallerId
(
	@CallerId VARCHAR(30)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Query */
	SELECT
		INbC.*
	FROM
		dbo.vwMS_IndustryNumberByCallerId AS INbC
	WHERE
		(INbC.GpsWatchPhoneNumber = @CallerId)
		
	
END
GO

GRANT EXEC ON dbo.custMS_IndustryAccountFindByCallerId TO PUBLIC
GO

EXEC dbo.custMS_IndustryAccountFindByCallerId '15135107528'