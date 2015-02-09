USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustEnsureUniqueVendorDocumentNumber')
	BEGIN
		PRINT 'Dropping Procedure ppCustEnsureUniqueVendorDocumentNumber'
		DROP  Procedure  dbo.ppCustEnsureUniqueVendorDocumentNumber
	END
GO

PRINT 'Creating Procedure ppCustEnsureUniqueVendorDocumentNumber'
GO
/******************************************************************************
**		File: ppCustEnsureUniqueVendorDocumentNumber.sql
**		Name: ppCustEnsureUniqueVendorDocumentNumber
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
**		Auth: 02/23/2009
**		Date: Todd Carlson
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	02/23/2009	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustEnsureUniqueVendorDocumentNumber
(
	@VendorID NVARCHAR(20)
	, @DocumentNumber NVARCHAR(30)
)
AS
BEGIN

	DECLARE @Result NVARCHAR(30)
	
	IF NOT EXISTS (SELECT * FROM PM30200 WHERE VENDORID = @VendorID AND DOCNUMBR = @DocumentNumber)
	BEGIN
		SET @Result = @DocumentNumber
	END
	ELSE IF NOT EXISTS (SELECT * FROM PM30200 WHERE VENDORID = @VendorID AND DOCNUMBR = @DocumentNumber + '.')
	BEGIN
		SET @Result = @DocumentNumber + '.'
	END
	ELSE IF NOT EXISTS (SELECT * FROM PM30200 WHERE VENDORID = @VendorID AND DOCNUMBR = @DocumentNumber + '-')
	BEGIN
		SET @Result = @DocumentNumber + '-'
	END
	ELSE IF NOT EXISTS (SELECT * FROM PM30200 WHERE VENDORID = @VendorID AND DOCNUMBR = @DocumentNumber + '*')
	BEGIN
		SET @Result = @DocumentNumber + '*'
	END
	ELSE IF NOT EXISTS (SELECT * FROM PM30200 WHERE VENDORID = @VendorID AND DOCNUMBR = @DocumentNumber + '^')
	BEGIN
		SET @Result = @DocumentNumber + '^'
	END
	ELSE IF NOT EXISTS (SELECT * FROM PM30200 WHERE VENDORID = @VendorID AND DOCNUMBR = @DocumentNumber + '#')
	BEGIN
		SET @Result = @DocumentNumber + '#'
	END
	ELSE IF NOT EXISTS (SELECT * FROM PM30200 WHERE VENDORID = @VendorID AND DOCNUMBR = @DocumentNumber + '>')
	BEGIN
		SET @Result = @DocumentNumber + '>'
	END
	ELSE
	BEGIN
		SET @Result = @DocumentNumber
	END
	
	SELECT @Result AS DocumentNumber
END
GO

GRANT EXEC ON dbo.ppCustEnsureUniqueVendorDocumentNumber TO PUBLIC
GO