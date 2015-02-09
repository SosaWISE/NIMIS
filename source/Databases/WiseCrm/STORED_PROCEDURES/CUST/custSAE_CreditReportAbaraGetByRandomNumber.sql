USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSAE_CreditReportAbaraGetByRandomNumber')
	BEGIN
		PRINT 'Dropping Procedure custSAE_CreditReportAbaraGetByRandomNumber'
		DROP  Procedure  dbo.custSAE_CreditReportAbaraGetByRandomNumber
	END
GO

PRINT 'Creating Procedure custSAE_CreditReportAbaraGetByRandomNumber'
GO
/******************************************************************************
**		File: custSAE_CreditReportAbaraGetByRandomNumber.sql
**		Name: custSAE_CreditReportAbaraGetByRandomNumber
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
**		Date: 01/13/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/13/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custSAE_CreditReportAbaraGetByRandomNumber
(
	@RndNumber INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	/** Transfer data */
	IF (@RndNumber = 0)
	BEGIN
		SELECT TOP 1
			*
		FROM
			[dbo].SAE_CreditReportAbara AS CRA WITH (NOLOCK)
		WHERE
			(CRA.Score > 624);
	END
	ELSE IF (@RndNumber = 1)
	BEGIN
		SELECT TOP 1
			*
		FROM
			[dbo].SAE_CreditReportAbara AS CRA WITH (NOLOCK)
		WHERE
			(CRA.Score BETWEEN 600 AND 624);
	END	
	ELSE IF (@RndNumber = 2)
	BEGIN
		SELECT TOP 1
			*
		FROM
			[dbo].SAE_CreditReportAbara AS CRA WITH (NOLOCK)
		WHERE
			(CRA.Score BETWEEN 575 AND 599);
	END
	ELSE
	BEGIN
		SELECT TOP 1
			*
		FROM
			[dbo].SAE_CreditReportAbara AS CRA WITH (NOLOCK)
		WHERE
			(CRA.Score < 575);
	END	
END
GO

GRANT EXEC ON dbo.custSAE_CreditReportAbaraGetByRandomNumber TO PUBLIC
GO

/** EXEC dbo.custSAE_CreditReportAbaraGetByRandomNumber 2 */