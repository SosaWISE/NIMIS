USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custQL_QualifyCustomerInfoViewByCustomerID')
	BEGIN
		PRINT 'Dropping Procedure custQL_QualifyCustomerInfoViewByCustomerID'
		DROP  Procedure  dbo.custQL_QualifyCustomerInfoViewByCustomerID
	END
GO

PRINT 'Creating Procedure custQL_QualifyCustomerInfoViewByCustomerID'
GO
/******************************************************************************
**		File: custQL_QualifyCustomerInfoViewByCustomerID.sql
**		Name: custQL_QualifyCustomerInfoViewByCustomerID
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
**		Date: 05/20/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	05/20/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custQL_QualifyCustomerInfoViewByCustomerID
(
	@CustomerID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @LeadID BIGINT;

	/** Find Lead ID. */
	SELECT @LeadID = LeadId FROM [dbo].[AE_Customers] WHERE (CustomerID = @CustomerID);

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

GRANT EXEC ON dbo.custQL_QualifyCustomerInfoViewByCustomerID TO PUBLIC
GO

/** EXEC dbo.custQL_QualifyCustomerInfoViewByCustomerID 1010120 */