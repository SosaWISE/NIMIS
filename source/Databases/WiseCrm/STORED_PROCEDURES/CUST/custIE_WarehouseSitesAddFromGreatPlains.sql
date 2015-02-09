USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custIE_WarehouseSitesAddFromGreatPlains')
	BEGIN
		PRINT 'Dropping Procedure custIE_WarehouseSitesAddFromGreatPlains'
		DROP  Procedure  dbo.custIE_WarehouseSitesAddFromGreatPlains
	END
GO

PRINT 'Creating Procedure custIE_WarehouseSitesAddFromGreatPlains'
GO
/******************************************************************************
**		File: custIE_WarehouseSitesAddFromGreatPlains.sql
**		Name: custIE_WarehouseSitesAddFromGreatPlains
**		Desc: 
**		Warehouses are entered into Great Plains.  CRM will be refreshed when
**		new Warehouses are created in Great Plains.
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
CREATE Procedure dbo.custIE_WarehouseSitesAddFromGreatPlains
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	BEGIN TRY
		/*** WAREHOUSES  ***/
		INSERT INTO [dbo].[IE_WarehouseSites] (
                    [WarehouseSiteID]
                    , [WarehouseSiteName]
		)
		SELECT 
			LTRIM(RTRIM(IV40700.LOCNCODE))
			, LTRIM(RTRIM(IV40700.LOCNDSCR))
		FROM 
			[DYSNEYDAD].NEX.dbo.IV40700
			LEFT JOIN [dbo].[IE_WarehouseSites]
				ON (IV40700.LOCNCODE = IE_WarehouseSites.WarehouseSiteID)
		WHERE
			(IE_WarehouseSites.WarehouseSiteID IS NULL);

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custIE_WarehouseSitesAddFromGreatPlains TO PUBLIC
GO

/** EXEC dbo.custIE_WarehouseSitesAddFromGreatPlains; */