USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custQL_QualifyCustomerInfoViewByLeadID')
	BEGIN
		PRINT 'Dropping Procedure custQL_QualifyCustomerInfoViewByLeadID'
		DROP  Procedure  dbo.custQL_QualifyCustomerInfoViewByLeadID
	END
GO

PRINT 'Creating Procedure custQL_QualifyCustomerInfoViewByLeadID'
GO
/******************************************************************************
**		File: custQL_QualifyCustomerInfoViewByLeadID.sql
**		Name: custQL_QualifyCustomerInfoViewByLeadID
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
**		Date: 05/09/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	05/09/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custQL_QualifyCustomerInfoViewByLeadID
(
	@LeadID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		SELECT
			VW.*
		FROM
			[dbo].[vwQL_QualifyCustomerInfo] AS VW
			INNER JOIN dbo.fxGetMaxOrSelectedCreditReportByLeadId(@LeadID) AS FX
			ON
				(VW.CreditReportID = FX.CreditReportID)
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custQL_QualifyCustomerInfoViewByLeadID TO PUBLIC
GO

/** EXEC dbo.custQL_QualifyCustomerInfoViewByLeadID 1010120 */