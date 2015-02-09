USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustEnsureCustomerPriceLevelExists')
	BEGIN
		PRINT 'Dropping Procedure ppCustEnsureCustomerPriceLevelExists'
		DROP  Procedure  dbo.ppCustEnsureCustomerPriceLevelExists
	END
GO

PRINT 'Creating Procedure ppCustEnsureCustomerPriceLevelExists'
GO
/******************************************************************************
**		File: ppCustEnsureCustomerPriceLevelExists.sql
**		Name: ppCustEnsureCustomerPriceLevelExists
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
**		Auth: Todd Carlson
**		Date: 12/16/2008
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	12/16/2008	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustEnsureCustomerPriceLevelExists
(
	@PriceLevel CHAR(11)
)
AS
BEGIN
	
	IF NOT EXISTS (SELECT * FROM IV40800 WHERE PRCLEVEL = @PriceLevel)
	BEGIN
		
		-- Get the next Note Index
		DECLARE @NoteIndex NUMERIC(19,5)
		EXECUTE SVC_Get_Next_Note_Index @NoteIndex OUTPUT
		
		DECLARE @CreatedDate DATETIME
		SET @CreatedDate = CAST(DATEPART(YEAR, GETDATE()) AS NVARCHAR(10)) + '-' + CAST(DATEPART(MONTH, GETDATE()) AS NVARCHAR(10)) + '-' + CAST(DATEPART(DAY, GETDATE()) AS NVARCHAR(10))
	
		INSERT INTO
			IV40800
			(
				PRCLEVEL
				, DSCRIPTN
				, NOTEINDX
				, CREATDDT
				, MODIFDT
			)
		VALUES
			(
				@PriceLevel
				, @PriceLevel
				, @NoteIndex
				, @CreatedDate
				, @CreatedDate
			)
	END
		
END
GO

GRANT EXEC ON dbo.ppCustEnsureCustomerPriceLevelExists TO PUBLIC
GO