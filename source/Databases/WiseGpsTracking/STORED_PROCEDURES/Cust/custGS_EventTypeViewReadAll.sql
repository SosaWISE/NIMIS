USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custGS_EventTypeViewReadAll')
	BEGIN
		PRINT 'Dropping Procedure custGS_EventTypeViewReadAll'
		DROP  Procedure  dbo.custGS_EventTypeViewReadAll
	END
GO

PRINT 'Creating Procedure custGS_EventTypeViewReadAll'
GO
/******************************************************************************
**		File: custGS_EventTypeViewReadAll.sql
**		Name: custGS_EventTypeViewReadAll
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
**		Date: 09/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	----------	--------		-------------------------------------------
**	09/05/2013	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custGS_EventTypeViewReadAll
(
	@EventTypeID VARCHAR(20)
	, @EventType NVARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	/** Execute Query */
	SELECT
		*
	FROM
		[dbo].vwGS_EventTypes
	WHERE
		((@EventTypeID IS NULL) OR (EventTypeID = @EventTypeID))
		AND ((@EventType IS NULL) OR (EventType LIKE '%' + @EventType + '%'));
	
END
GO

GRANT EXEC ON dbo.custGS_EventTypeViewReadAll TO PUBLIC
GO

--EXEC dbo.custGS_EventTypeViewReadAll NULL, 'tamper'