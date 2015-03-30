USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxQlCreditReportGetGatewayByMsAccountID')
	BEGIN
		PRINT 'Dropping FUNCTION fxQlCreditReportGetGatewayByMsAccountID'
		DROP FUNCTION  dbo.fxQlCreditReportGetGatewayByMsAccountID
	END
GO

PRINT 'Creating FUNCTION fxQlCreditReportGetGatewayByMsAccountID'
GO
/******************************************************************************
**		File: fxQlCreditReportGetGatewayByMsAccountID.sql
**		Name: fxQlCreditReportGetGatewayByMsAccountID
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés E. Sosa
**		Date: 03/30/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	03/30/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxQlCreditReportGetGatewayByMsAccountID
(
	@AccountId BIGINT
)
RETURNS NVARCHAR(50)
AS
BEGIN
	/** Declarations */
	DECLARE @Gateway NVARCHAR(50);

	/** Execute actions. */
	SELECT @Gateway = CAST(Gateway AS NVARCHAR(50)) FROM dbo.fxQlCreditReportGetByMsAccountID(@AccountId);

	RETURN @Gateway;
END
GO

--SELECT * FROM dbo.fxQlCreditReportGetByMsAccountID(12312);
--SELECT dbo.fxQlCreditReportGetGatewayByMsAccountID(12321) AS ReportID;