USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersSalesInfoExtendedConnextGetByUserID')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersSalesInfoExtendedConnextGetByUserID'
		DROP  Procedure  dbo.custRU_UsersSalesInfoExtendedConnextGetByUserID
	END
GO

PRINT 'Creating Procedure custRU_UsersSalesInfoExtendedConnextGetByUserID'
GO
/******************************************************************************
**		File: custRU_UsersSalesInfoExtendedConnextGetByUserID.sql
**		Name: custRU_UsersSalesInfoExtendedConnextGetByUserID
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
CREATE Procedure dbo.custRU_UsersSalesInfoExtendedConnextGetByUserID
(
	@UserID INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		
		SELECT * FROM vwRU_UsersSalesInfoExtendedConnext WHERE (UserID = @UserID);
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custRU_UsersSalesInfoExtendedConnextGetByUserID TO PUBLIC
GO

/** EXEC dbo.custRU_UsersSalesInfoExtendedConnextGetByUserID 1 */
