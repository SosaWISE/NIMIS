USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custIE_VendorsAddFromGreatPlains')
	BEGIN
		PRINT 'Dropping Procedure custIE_VendorsAddFromGreatPlains'
		DROP  Procedure  dbo.custIE_VendorsAddFromGreatPlains
	END
GO

PRINT 'Creating Procedure custIE_VendorsAddFromGreatPlains'
GO
/******************************************************************************
**		File: custIE_VendorsAddFromGreatPlains.sql
**		Name: custIE_VendorsAddFromGreatPlains
**		Desc: 
**		Vendors are entered into Great Plains.  CRM will be refreshed when
**		new Vendors are created in Great Plains.
**
**		This template can be customized:
**              
**		Return values:
**		None
** 
**		Called by:
**		Job Agent to refresh CRM with Great Plains data.
**      
**		Parameters:
**		Input							Output
**     ----------						-----------
**		None							None
**
**		Auth: Bob McFadden
**		Date: 07/16/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/30/2014	Bob McFadden	Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custIE_VendorsAddFromGreatPlains
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	BEGIN TRY
			/*** VENDOR  ***/
		INSERT INTO [dbo].[IE_Vendors](
			[VendorID]
			,[VendorName]
			)
		SELECT DISTINCT 
			LTRIM(RTRIM(PM00200.VENDORID)),
			LTRIM(RTRIM(PM00200.VENDNAME))
		FROM
			-- PO LINES 
			DYSNEYDAD.NEX.dbo.POP10110

			-- ITEM SKUs
			JOIN DYSNEYDAD.NEX.dbo.IV00101
				ON POP10110.ITEMNMBR = IV00101.ITEMNMBR

			--VENDOR MASTER
			JOIN DYSNEYDAD.NEX.dbo.PM00200
				ON POP10110.VENDORID = PM00200.VENDORID
		WHERE PM00200.VENDORID not in (SELECT VendorID from WISE_CRM.dbo.IE_Vendors) 
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custIE_VendorsAddFromGreatPlains TO PUBLIC
GO

/** EXEC dbo.custIE_VendorsAddFromGreatPlains; */