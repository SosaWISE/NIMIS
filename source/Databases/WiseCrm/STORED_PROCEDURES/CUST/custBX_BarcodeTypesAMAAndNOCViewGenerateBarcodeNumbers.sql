USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custBX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers')
	BEGIN
		PRINT 'Dropping Procedure custBX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers'
		DROP  Procedure  dbo.custBX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers
	END
GO

PRINT 'Creating Procedure custBX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers'
GO
/******************************************************************************
**		File: custBX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers.sql
**		Name: custBX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers
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
**		Date: 09/19/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	09/19/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custBX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers
(
	@BarcodeTypeID CHAR(8)
	, @AccountId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @RangeCurrent INT
		, @AMABarcodeTypeID CHAR(8)
		, @NOCBarcodeTypeID CHAR(8)
		, @AMABarcodeNumber VARCHAR(30)
		, @AMABarcodeID BIGINT
		, @NOCBarcodeNumber VARCHAR(30)
		, @NOCBarcodeID BIGINT;
	
	BEGIN TRY
		/** Initialize */
		SET @AMABarcodeTypeID = @BarcodeTypeID;
		SELECT @NOCBarcodeTypeID = 'NC' + SUBSTRING(@BarcodeTypeID, 3, 6);
		PRINT '@NOCBarcodeTypeID: ' + CAST(@NOCBarcodeTypeID AS VARCHAR);
		PRINT '@@BarcodeTypeID: ' + CAST(@BarcodeTypeID AS VARCHAR);

		BEGIN TRANSACTION;
	
		-- Select the Current range value
		SELECT @RangeCurrent = RangeCurrent FROM [dbo].[BX_BarcodeTypes] WHERE (BarcodeTypeID = @BarcodeTypeID);

		-- Increment the Range
		SET @RangeCurrent = @RangeCurrent + 1;
--		SET @AMABarcodeNumber = @AMABarcodeTypeID + CAST(@RangeCurrent AS VARCHAR);
		SELECT @AMABarcodeNumber = [DocTypeId] + '-' + [PrinterId] + '-' + [GroupDoc] + '-' + [Version] FROM [dbo].[BX_BarcodeTypes] WHERE (BarcodeTypeID = @BarcodeTypeID);
		SET @AMABarcodeNumber = @AMABarcodeNumber + '-' + CAST(@RangeCurrent AS VARCHAR);
		PRINT '@AMABarcodeNumber: ' + @AMABarcodeNumber;
		
--		SET @NOCBarcodeNumber = @NOCBarcodeTypeID + CAST(@RangeCurrent AS VARCHAR);
		SELECT @NOCBarcodeNumber = [DocTypeId] + '-' + [PrinterId] + '-' + [GroupDoc] + '-' + [Version] FROM [dbo].[BX_BarcodeTypes] WHERE (BarcodeTypeID = @NOCBarcodeTypeID);
		SET @NOCBarcodeNumber = @NOCBarcodeNumber + '-' + CAST(@RangeCurrent AS VARCHAR);
		PRINT '@NOCBarcodeNumber: ' + @NOCBarcodeNumber;
		-- Update the BX_BarcodeTypes table
		UPDATE [dbo].[BX_BarcodeTypes] SET RangeCurrent = @RangeCurrent WHERE (BarcodeTypeID = @AMABarcodeTypeID);
		UPDATE [dbo].[BX_BarcodeTypes] SET RangeCurrent = @RangeCurrent WHERE (BarcodeTypeID = @NOCBarcodeTypeID);

		-- Save barcode information into BX_Barcodes table.
		INSERT INTO [dbo].[BX_Barcodes] (
			[BarcodeTypeId]
			, [ForeignKey]
			, [BarcodeNumber]
		) VALUES (
			@AMABarcodeTypeID -- BarcodeTypeId - char(8)
			, CAST(@AccountId AS VARCHAR) -- ForeignKey - varchar(50)
			, @AMABarcodeNumber -- BarcodeNumber - nvarchar(30)
		);
		SET @AMABarcodeID = SCOPE_IDENTITY();
		INSERT INTO [dbo].[BX_Barcodes] (
			[BarcodeTypeId]
			, [ForeignKey]
			, [BarcodeNumber]
		) VALUES (
			@NOCBarcodeTypeID -- BarcodeTypeId - char(8)
			, CAST(@AccountId AS VARCHAR) -- ForeignKey - varchar(50)
			, @NOCBarcodeNumber -- BarcodeNumber - nvarchar(30)
		);
		SET @NOCBarcodeID = SCOPE_IDENTITY();

		-- ** RETURN result 
		SELECT @AccountId AS AccountID, @AMABarcodeNumber AS AMABarcodeNumber, @AMABarcodeID AS AMABarcodeID, @NOCBarcodeNumber AS NOCBarcodeNumber, @NOCBarcodeID AS NOCBarcodeID
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custBX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers TO PUBLIC
GO

/** EXEC dbo.custBX_BarcodeTypesAMAAndNOCViewGenerateBarcodeNumbers 'AMNXS001', 130000 */