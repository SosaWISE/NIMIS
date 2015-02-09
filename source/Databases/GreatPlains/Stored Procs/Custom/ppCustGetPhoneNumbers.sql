USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustGetPhoneNumbers')
	BEGIN
		PRINT 'Dropping Procedure ppCustGetPhoneNumbers'
		DROP  Procedure  dbo.ppCustGetPhoneNumbers
	END
GO

PRINT 'Creating Procedure ppCustGetPhoneNumbers'
GO
/******************************************************************************
**		File: ppCustGetPhoneNumbers.sql
**		Name: ppCustGetPhoneNumbers
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
**		Auth: Brett Kotter
**		Date: 10/13/2009
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	10/13/2009	Brett Kotter	Created		
*******************************************************************************/
CREATE Procedure dbo.ppCustGetPhoneNumbers
(
	@AccountID NVARCHAR(50)
)
AS
BEGIN

	SELECT	CUSTNMBR
			, PHONE1
			, PHONE2
			, PHONE3
	FROM 
		RM00101
	WHERE 
		(CUSTNMBR = @AccountID)
	
END
GO

GRANT EXEC ON dbo.ppCustGetPhoneNumbers TO PUBLIC
GO