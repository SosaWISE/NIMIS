USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_TimeZoneLookupGetByStateAB')
	BEGIN
		PRINT 'Dropping Procedure custMS_TimeZoneLookupGetByStateAB'
		DROP  Procedure  dbo.custMS_TimeZoneLookupGetByStateAB
	END
GO

PRINT 'Creating Procedure custMS_TimeZoneLookupGetByStateAB'
GO
/******************************************************************************
**		File: custMS_TimeZoneLookupGetByStateAB.sql
**		Name: custMS_TimeZoneLookupGetByStateAB
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
**		Date: 00/00/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	00/00/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_TimeZoneLookupGetByStateAB
(
	@StateAB CHAR(2)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Execute Statement. */
	SELECT * FROM MS_TimeZoneLookup WHERE (StateAB = @StateAB);
END
GO

GRANT EXEC ON dbo.custMS_TimeZoneLookupGetByStateAB TO PUBLIC
GO