USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustSetCustomerClass')
	BEGIN
		PRINT 'Dropping Procedure ppCustSetCustomerClass'
		DROP  Procedure  dbo.ppCustSetCustomerClass
	END
GO

PRINT 'Creating Procedure ppCustSetCustomerClass'
GO
/******************************************************************************
**		File: ppCustSetCustomerClass.sql
**		Name: ppCustSetCustomerClass
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
**		Auth: 
**		Date: 
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**			
*******************************************************************************/
CREATE Procedure dbo.ppCustSetCustomerClass
(
	@I_vCustomerNumber CHAR(15)
	, @O_iErrorState INT OUTPUT				-- Return value: 0 = No Errors, Any Errors > 0
	, @oErrString VARCHAR(255) OUTPUT		-- Return Error Message
)
AS
BEGIN

	DECLARE @CustClass NVARCHAR(10)
	SELECT
		@CustClass = CUSTCLAS
	FROM
		RM00101 WITH (NOLOCK)
	WHERE
		CUSTNMBR = @I_vCustomerNumber
		
	IF (@CustClass IS NOT NULL)
	BEGIN
	
		UPDATE
			RM00101
		SET
			CHEKBKID = CLS.CHEKBKID
			, DEFCACTY = CLS.DEFCACTY
			, RMCSHACC = CLS.RMCSHACC
			, RMARACC = CLS.RMARACC
			, RMCOSACC = CLS.RMCOSACC
			, RMIVACC = CLS.RMIVACC
			, RMSLSACC = CLS.RMSLSACC
			, RMAVACC = CLS.RMAVACC
			, RMTAKACC = CLS.RMTAKACC
			, RMFCGACC = CLS.RMFCGACC
			, RMWRACC = CLS.RMWRACC
			, RMSORACC = CLS.RMSORACC
		FROM
			(
				SELECT
					CHEKBKID
					, DEFCACTY
					, RMCSHACC
					, RMARACC
					, RMCOSACC
					, RMIVACC
					, RMSLSACC
					, RMAVACC
					, RMTAKACC
					, RMFCGACC
					, RMWRACC
					, RMSORACC
				FROM
					RM00201 WITH (NOLOCK)
				WHERE
					CLASSID = @CustClass
			) AS CLS
		WHERE
			CUSTNMBR = @I_vCustomerNumber
	END
	
END
GO

GRANT EXEC ON dbo.ppCustSetCustomerClass TO PUBLIC
GO