USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetSeTicketCountByBlockId')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetSeTicketCountByBlockId'
		DROP FUNCTION  dbo.fxGetSeTicketCountByBlockId
	END
GO

PRINT 'Creating FUNCTION fxGetSeTicketCountByBlockId'
GO
/******************************************************************************
**		File: fxGetSeTicketCountByBlockId.sql
**		Name: fxGetSeTicketCountByBlockId
**		Desc: this function will be used to count the no. of tickets by blocked id
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
**     blockid                no. of tickets
**
**		Auth: Andrés E. Sosa
**		Date: 01/27/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	07/29/2014	Reagan Descartin	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetSeTicketCountByBlockId
(
	@BlockId BIGINT
)
RETURNS INT
AS
BEGIN
	DECLARE @NoOfTickets INT

	SET @NoOfTickets = ISNULL(
		(SELECT COUNT(SEST.TicketId) FROM  [dbo].[SE_ScheduleTickets] SEST WHERE SEST.[BlockId] = @BlockId AND SEST.IsDeleted = 0)
	
	,0)
		
	RETURN @NoOfTickets
END
GO




