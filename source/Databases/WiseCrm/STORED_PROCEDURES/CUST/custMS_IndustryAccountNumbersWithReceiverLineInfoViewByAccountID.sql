USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID')
	BEGIN
		PRINT 'Dropping Procedure custMS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID'
		DROP  Procedure  dbo.custMS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID
	END
GO

PRINT 'Creating Procedure custMS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID'
GO
/******************************************************************************
**		File: custMS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID.sql
**		Name: custMS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID
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
**		Date: 01/22/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/22/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID
(
	@AccountId BIGINT = NULL
	, @GpEmployeeID VARCHAR(50)
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
			[dbo].[vwMS_IndustryAccountNumbersWithReceiverLineInfo] 
		WHERE
			(AccountID = @AccountId)
		ORDER BY 
			[PrimaryCSID] DESC
			, [SecondaryCSID] DESC
			, IndustryAccountID DESC;
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID TO PUBLIC
GO

/** EXEC dbo.custMS_IndustryAccountNumbersWithReceiverLineInfoViewByAccountID 170964*/