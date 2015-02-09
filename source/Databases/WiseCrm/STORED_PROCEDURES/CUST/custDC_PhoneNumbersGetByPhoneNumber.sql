USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custDC_PhoneNumbersGetByPhoneNumber')
	BEGIN
		PRINT 'Dropping Procedure custDC_PhoneNumbersGetByPhoneNumber'
		DROP  Procedure  dbo.custDC_PhoneNumbersGetByPhoneNumber
	END
GO

PRINT 'Creating Procedure custDC_PhoneNumbersGetByPhoneNumber'
GO
/******************************************************************************
**		File: custDC_PhoneNumbersGetByPhoneNumber.sql
**		Name: custDC_PhoneNumbersGetByPhoneNumber
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
CREATE Procedure dbo.custDC_PhoneNumbersGetByPhoneNumber
(
	@PhoneNumber CHAR(10)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
	
		/** Transfer data */
		--INSERT INTO XN_CustomersMasterFileCompanyStats
		SELECT
			PHN.*
		FROM
			dbo.DC_PhoneNumbers AS PHN WITH (NOLOCK)
		WHERE
			(PHN.PhoneNumberID = @PhoneNumber)
	
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custDC_PhoneNumbersGetByPhoneNumber TO PUBLIC
GO

/** EXEC dbo.custDC_PhoneNumbersGetByPhoneNumber '3850007359' */