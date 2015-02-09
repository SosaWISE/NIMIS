USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetTicketCoordinateByTicketId')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetTicketCoordinateByTicketId'
		DROP FUNCTION  dbo.fxGetTicketCoordinateByTicketId
	END
GO

PRINT 'Creating FUNCTION fxGetTicketCoordinateByTicketId'
GO
/******************************************************************************
**		File: fxGetTicketCoordinateByTicketId.sql
**		Name: fxGetTicketCoordinateByTicketId
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
**		Auth: Andrés E. Sosa
**		Date: 03/14/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	09/09/2014	Reagan Descartin	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetTicketCoordinateByTicketId
(
	@TicketId BIGINT
	,@CoordinateName VARCHAR(10)
)
RETURNS FLOAT
AS
BEGIN
	/** Declarations */
	DECLARE @Coordinate FLOAT;


	IF (@TicketId IS NOT NULL)
	BEGIN
		IF @CoordinateName ='Latitude' 
		BEGIN
			SET @Coordinate = ISNULL( (SELECT DISTINCT(T.Latitude) FROM [dbo].[vwSE_Tickets] T WHERE T.TicketID = @TicketId), NULL);
		END
		ELSE
		BEGIN
			SET @Coordinate = ISNULL( (SELECT DISTINCT(T.Longitude) FROM [dbo].[vwSE_Tickets] T WHERE T.TicketID = @TicketId), NULL);
		END
	END
	ELSE
	BEGIN
		SET @Coordinate = NULL
	END
	RETURN @Coordinate;
END
GO


/*
SELECT T.Latitude FROM [dbo].[vwSE_Tickets] T WHERE T.TicketID = 10149

*/

