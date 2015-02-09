USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetCustomerAddressByADRSCODE')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetCustomerAddressByADRSCODE'
		DROP  Procedure  dbo.ppCustGetCustomerAddressByADRSCODE
	END
GO

PRINT 'Creating Procedure ppCustGetCustomerAddressByADRSCODE'
GO
/******************************************************************************
**		File: ppCustGetCustomerAddressByADRSCODE.sql
**		Name: ppCustGetCustomerAddressByADRSCODE
**		Desc: This proc returns the requested address type.  If ADRSCODE is 
**			passed as NULL it will return the BILLING type.
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
**		Date: 10/16/2009
**			
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	10/16/2009	Andres Sosa		Created by
**			
**			Test DATA
** EXEC dbo.ppCustGetCustomerAddressByADRSCODE '100002', 'BILLING'
** EXEC dbo.ppCustGetCustomerAddressByADRSCODE '374699', 'BILLING'
** EXEC dbo.ppCustGetCustomerAddressByADRSCODE '374699', 'MONITORING'
** EXEC dbo.ppCustGetCustomerAddressByADRSCODE '490156', NULL
** EXEC dbo.ppCustGetCustomerAddressByADRSCODE '490156', 'MONITORING'
*******************************************************************************/
CREATE Procedure dbo.ppCustGetCustomerAddressByADRSCODE
(
	@CUSTNMBR CHAR(15)
	, @ADRSCODE CHAR(15)
)
AS
BEGIN
	-- SELECT * FROM RM00102 WHERE CUSTNMBR = '100002'
	-- Check that there is ADRSCODE
	IF (@ADRSCODE IS NOT NULL)
	BEGIN
		PRINT 'ADRSCODE IS NOT NULL.'
		SELECT TOP 1 * FROM RM00102 WHERE (CUSTNMBR = @CUSTNMBR) AND (ADRSCODE = @ADRSCODE) ORDER BY DEX_ROW_ID DESC
		
		-- Return
		RETURN
	END

	IF (EXISTS(SELECT * FROM RM00102 WHERE (CUSTNMBR = @CUSTNMBR) AND (ADRSCODE = 'BILLING')))
	BEGIN
		PRINT 'Found Billing Address'
		SELECT TOP 1 * FROM RM00102 WHERE (CUSTNMBR = @CUSTNMBR) AND (ADRSCODE = 'BILLING') ORDER BY DEX_ROW_ID DESC
	END
	ELSE
	BEGIN
		PRINT 'Billing Address Not Found'
		IF (EXISTS(SELECT * FROM RM00102 WHERE (CUSTNMBR = @CUSTNMBR) AND (ADRSCODE = 'MONITORING')))
		BEGIN
			PRINT 'Found Monitoring Address'
			SELECT TOP 1 * FROM RM00102 WHERE (CUSTNMBR = @CUSTNMBR) AND (ADRSCODE = 'MONITORING') ORDER BY DEX_ROW_ID DESC
		END
		ELSE
		BEGIN
			PRINT 'NOT Found Monitoring Address'
		END
	END
END
GO

GRANT EXEC ON dbo.ppCustGetCustomerAddressByADRSCODE TO PUBLIC
GO