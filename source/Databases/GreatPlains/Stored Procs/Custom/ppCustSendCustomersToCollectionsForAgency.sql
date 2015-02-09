USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustSendCustomersToCollectionsForAgency')
	BEGIN
		PRINT 'Dropping Procedure ppCustSendCustomersToCollectionsForAgency'
		DROP  Procedure  dbo.ppCustSendCustomersToCollectionsForAgency
	END
GO

PRINT 'Creating Procedure ppCustSendCustomersToCollectionsForAgency'
GO
/******************************************************************************
**		File: ppCustSendCustomersToCollectionsForAgency.sql
**		Name: ppCustSendCustomersToCollectionsForAgency
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
**		Auth: Brett Kotter
**		Date: 10/01/2009
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	10/01/2009	Brett Kotter	Created	
**	10-19-2009	Brett Kotter	Updated sproc for way we are spliting int list
*******************************************************************************/
CREATE Procedure dbo.ppCustSendCustomersToCollectionsForAgency
(
	@UPSZone NVARCHAR(50)
	, @ShipmentMethod NVARCHAR(50)
	, @UserDef2 NVARCHAR(50)
	, @AccountIDs NVARCHAR(MAX) 
)
AS
BEGIN

	UPDATE RM00101
		SET
		UPSZONE = @UPSZone
		, SHIPMTHD = @ShipmentMethod
		, USERDEF2 = @UserDef2
	WHERE
		CUSTNMBR IN (
						SELECT
							Convert(NVARCHAR(10),ID)
						FROM dbo.SplitIntList(@AccountIDs))
	
END
GO

GRANT EXEC ON dbo.ppCustSendCustomersToCollectionsForAgency TO PUBLIC
GO