USE [NXSE_Funding]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custFE_PacketItemsViewByPacketID')
	BEGIN
		PRINT 'Dropping Procedure custFE_PacketItemsViewByPacketID'
		DROP  Procedure  dbo.custFE_PacketItemsViewByPacketID
	END
GO

PRINT 'Creating Procedure custFE_PacketItemsViewByPacketID'
GO
/******************************************************************************
**		File: custFE_PacketItemsViewByPacketID.sql
**		Name: custFE_PacketItemsViewByPacketID
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
**		Date: 03/25/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/25/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custFE_PacketItemsViewByPacketID
(
	@PacketID INT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT
			*
		FROM
			[dbo].[vwFE_PacketItems] AS FEPI WITH (NOLOCK)
		WHERE
			(FEPI.PacketId = @PacketID);

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custFE_PacketItemsViewByPacketID TO PUBLIC
GO

/** EXEC dbo.custFE_PacketItemsViewByPacketID */