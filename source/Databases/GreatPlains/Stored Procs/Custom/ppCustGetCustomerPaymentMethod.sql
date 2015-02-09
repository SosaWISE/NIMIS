USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetCustomerPaymentMethod')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetCustomerPaymentMethod'
		DROP  Procedure  dbo.ppCustGetCustomerPaymentMethod
	END
GO

PRINT 'Creating Procedure ppCustGetCustomerPaymentMethod'
GO
/******************************************************************************
**		File: ppCustGetCustomerPaymentMethod.sql
**		Name: ppCustGetCustomerPaymentMethod
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
**		Date: 10/15/2009
**			
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	10/15/2009	Andres Sosa		Created by
**			
** EXEC dbo.ppCustGetCustomerPaymentMethod '406887'
** EXEC dbo.ppCustGetCustomerPaymentMethod '490156'
*******************************************************************************/
CREATE Procedure dbo.ppCustGetCustomerPaymentMethod
(
	@CUSTNMBR CHAR(15)
)
AS
BEGIN
	-- Local
	DECLARE @SOP10100 TABLE (CUSTNMBR CHAR(15))
	DECLARE @ACCTNMBR VARCHAR(50)
	DECLARE @BankName VARCHAR(50)
	DECLARE @Result TABLE (Result VARCHAR(50), AccountNumber VARCHAR(50), BankName VARCHAR(50))
	
	-- Insert value
	INSERT INTO @SOP10100 (CUSTNMBR)
	SELECT CUSTNMBR FROM SOP10100 WHERE CUSTNMBR = @CUSTNMBR
	INSERT INTO @Result (Result, AccountNumber) VALUES ('NO RESULT', NULL)
	
	/* CHECK FOR CREDIT CARD */
	IF 
	(SELECT 
		A.CRCARDID 
	FROM 
		RM00101 AS A 
		INNER JOIN @SOP10100 AS B
		ON 
			(A.CUSTNMBR = B.CUSTNMBR)) NOT LIKE '' 
	BEGIN
		PRINT 'CC'
		SELECT @ACCTNMBR = CRCARDID FROM RM00101 WHERE CUSTNMBR = @CUSTNMBR
		UPDATE @Result SET Result = 'CREDIT CARD', AccountNumber = @ACCTNMBR
	END
	ELSE
	BEGIN
		/* CHECK FOR BANK ACCOUNT */
		PRINT 'BANK ACCOUNT'
		IF 
		(SELECT 
			COUNT(*)
		FROM 
			SY06000 AS A 
			INNER JOIN @SOP10100 AS B
			ON
				(A.CUSTNMBR = B.CUSTNMBR)
		WHERE 
			LEN(RTRIM(A.EFTBankAcct)) > 0) > 0
		BEGIN
			PRINT 'BANK ACCOUNT'
			SELECT TOP 1 @ACCTNMBR = EFTBankAcct, @BankName = BANKNAME FROM SY06000 WHERE (CUSTNMBR = @CUSTNMBR)
			UPDATE @Result SET Result = 'BANK ACCOUT', AccountNumber = @ACCTNMBR, BankName = @BankName
		END
		ELSE
		BEGIN
			PRINT 'INVOICE'
			UPDATE @Result SET Result = 'INVOICE'
		END
	END
	
	-- Show Result
	SELECT * FROM @Result
END
GO

GRANT EXEC ON dbo.ppCustGetCustomerPaymentMethod TO PUBLIC
GO

