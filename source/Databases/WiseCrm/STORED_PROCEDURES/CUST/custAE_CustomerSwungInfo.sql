USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSwungInfo')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSwungInfo'
		DROP  Procedure  dbo.custAE_CustomerSwungInfo
	END
GO

PRINT 'Creating Procedure custAE_CustomerSwungInfo'
GO
/******************************************************************************
**		File: custAE_CustomerSwungInfo.sql
**		Name: custAE_CustomerSwungInfo
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
**		Date: 03/28/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	05/21/2014	Junryl/Reagan		Created By
**	05/26/2014	Junryl			Change to pull from MS_AccountSwungInfo and some fields to pull
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerSwungInfo
(
    @InterimAccountID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
		/** Return records from MS_AccountSwungInfo **/
		SELECT
			*
		FROM
		[WISE_CRM].dbo.[MS_AccountSwungInfo] AS MA WITH (NOLOCK)
		WHERE
		  MA.InterimAccountID = @InterimAccountID 
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerSwungInfo TO PUBLIC
GO

/** EXEC dbo.custAE_CustomerSwungInfo 100048 **/
