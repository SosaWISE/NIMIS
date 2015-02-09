USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustSetPaymentInfoToMS273512')
	BEGIN
		PRINT 'Dropping Procedure ppCustSetPaymentInfoToMS273512'
		DROP  Procedure  dbo.ppCustSetPaymentInfoToMS273512
	END
GO

PRINT 'Creating Procedure ppCustSetPaymentInfoToMS273512'
GO
/******************************************************************************
**		File: ppCustSetPaymentInfoToMS273512.sql
**		Name: ppCustSetPaymentInfoToMS273512
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
EXEC dbo.ppCustSetPaymentInfoToMS273512 '5ED28C2D-9407-46A6-92F4-C2A55B27D7CE', 'DEL ME', 'DELETE ME'
*******************************************************************************/
CREATE Procedure dbo.ppCustSetPaymentInfoToMS273512
(
	@I_vMSO_InstanceGUID CHAR(101)
	, @I_vBACHNUMB CHAR(15)
	, @I_vMSO_Doc_Number CHAR(21) OUTPUT
	
	, @O_iErrorState INT OUTPUT				-- Return value: 0 = No Errors, Any Errors > 0
	, @oErrString VARCHAR(255) OUTPUT		-- Return Error Message
)
AS
BEGIN
	-- Enter CODE HERE>
	BEGIN TRAN
	
	UPDATE MS273505 SET MSO_Doc_Number = @I_vMSO_Doc_Number, BACHNUMB = @I_vBACHNUMB WHERE MSO_InstanceGUID = @I_vMSO_InstanceGUID
	UPDATE MS273512 SET MSO_Doc_Number = @I_vMSO_Doc_Number, BACHNUMB = @I_vBACHNUMB WHERE MSO_InstanceGUID = @I_vMSO_InstanceGUID
	
	--SELECT * FROM MS273512 WHERE MSO_InstanceGUID = @MSO_InstanceGUID
	COMMIT TRAN
	
END
GO

GRANT EXEC ON dbo.ppCustSetPaymentInfoToMS273512 TO PUBLIC
GO