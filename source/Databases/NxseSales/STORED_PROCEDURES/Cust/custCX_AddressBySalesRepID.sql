USE [NXSE_Connext]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custCX_AddressBySalesRepID')
	BEGIN
		PRINT 'Dropping Procedure custCX_AddressBySalesRepID'
		DROP  Procedure  dbo.custCX_AddressBySalesRepID
	END
GO

PRINT 'Creating Procedure custCX_AddressBySalesRepID'
GO
/******************************************************************************
**		File: custCX_AddressBySalesRepID.sql
**		Name: custCX_AddressBySalesRepID
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
**		Date: 03/19/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/19/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custCX_AddressBySalesRepID
(
	@SalesRepId VARCHAR(20)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT
			ADR.*
		FROM
			[dbo].[CX_Contacts] AS CXCT WITH (NOLOCK)
			INNER JOIN [dbo].[CX_Address] AS ADR WITH (NOLOCK)
			ON
				(ADR.AddressID = CXCT.AddressId)
		WHERE
			(CXCT.SalesRepId = @SalesRepId)
			AND (CXCT.IsActive = 1) AND (CXCT.IsDeleted = 0)
			AND (ADR.IsActive = 1) AND (ADR.IsDeleted = 0);

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custCX_AddressBySalesRepID TO PUBLIC
GO

/** EXEC dbo.custCX_AddressBySalesRepID 'SOSA001'*/