USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersSalesInfoConnextGetByUserID')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersSalesInfoConnextGetByUserID'
		DROP  Procedure  dbo.custRU_UsersSalesInfoConnextGetByUserID
	END
GO

PRINT 'Creating Procedure custRU_UsersSalesInfoConnextGetByUserID'
GO
/******************************************************************************
**		File: custRU_UsersSalesInfoConnextGetByUserID.sql
**		Name: custRU_UsersSalesInfoConnextGetByUserID
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
**	03/28/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custRU_UsersSalesInfoConnextGetByUserID
(
	@UserID INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		
		SELECT * FROM dbo.vwRU_UsersSalesInfoConnext WHERE (UserID = @UserID);
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custRU_UsersSalesInfoConnextGetByUserID TO PUBLIC
GO

/** EXEC dbo.custRU_UsersSalesInfoConnextGetByUserID 1 */
