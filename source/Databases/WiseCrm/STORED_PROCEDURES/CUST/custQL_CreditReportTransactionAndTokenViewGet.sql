USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custQL_CreditReportTransactionAndTokenViewGet')
	BEGIN
		PRINT 'Dropping Procedure custQL_CreditReportTransactionAndTokenViewGet'
		DROP  Procedure  dbo.custQL_CreditReportTransactionAndTokenViewGet
	END
GO

PRINT 'Creating Procedure custQL_CreditReportTransactionAndTokenViewGet'
GO
/******************************************************************************
**		File: custQL_CreditReportTransactionAndTokenViewGet.sql
**		Name: custQL_CreditReportTransactionAndTokenViewGet
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
**		Date: 01/10/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/10/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custQL_CreditReportTransactionAndTokenViewGet
(
	@CreditReportID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	BEGIN TRY
		
		SELECT 
			*
		FROM
			vwQL_CreditReportTransactionAndToken AS QCR
		WHERE
			(QCR.CreditReportID = @CreditReportID);

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custQL_CreditReportTransactionAndTokenViewGet TO PUBLIC
GO

/** EXEC dbo.custQL_CreditReportTransactionAndTokenViewGet 85641 */