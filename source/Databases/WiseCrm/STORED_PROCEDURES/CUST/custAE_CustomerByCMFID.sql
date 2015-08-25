USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerByCMFID')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerByCMFID'
		DROP  Procedure  dbo.custAE_CustomerByCMFID
	END
GO

PRINT 'Creating Procedure custAE_CustomerByCMFID'
GO
/******************************************************************************
**		File: custAE_CustomerByCMFID.sql
**		Name: custAE_CustomerByCMFID
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
**		Date: 08/25/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	08/25/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerByCMFID
(
	@CMFID BIGINT = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT
			AEC.*
		FROM
			[dbo].[AE_Customers] AS AEC WITH (NOLOCK)
			INNER JOIN [dbo].[AE_CustomerAccounts] AS AECA WITH(NOLOCK)
			ON
				(AECA.CustomerId = AEC.CustomerID)
		WHERE
			(AEC.CustomerMasterFileId = @CMFID)
			AND (AEC.IsActive = 1) AND (AEC.IsDeleted = 0);

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerByCMFID TO PUBLIC
GO

/** EXEC dbo.custAE_CustomerByCMFID 3051157 */