USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custBE_BarcodesExistingBarcode')
	BEGIN
		PRINT 'Dropping Procedure custBE_BarcodesExistingBarcode'
		DROP  Procedure  dbo.custBE_BarcodesExistingBarcode
	END
GO

PRINT 'Creating Procedure custBE_BarcodesExistingBarcode'
GO
/******************************************************************************
**		File: custBE_BarcodesExistingBarcode.sql
**		Name: custBE_BarcodesExistingBarcode
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
**		Auth: Andres E. Sosa
**		Date: 01/10/2011
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/10/2011	Andres E. Sosa	Created By
**	
*******************************************************************************/
CREATE PROCEDURE custBE_BarcodesExistingBarcode
(
	@RecruitID INT
	, @DocTypeID INT
	, @BarcodeNumber NVARCHAR(30)
)
AS
BEGIN

	SELECT
		BB.ForeignKey AS RecruitID
		, BB.BarcodeNumber AS RecruitsCurrentBarcode
		, BarcodeExists.ForeignKey AS ExistingBarcodeRecruitID
		, BarcodeExists.BarcodeNumber AS ExistingBarcode
	FROM
	(
		SELECT
			*
		FROM
		(
			SELECT
				BB.*
				, ROW_NUMBER() OVER (PARTITION BY BB.DocTypeID, BB.ForeignKey ORDER BY BB.[Date] DESC) AS RowNumber
			FROM BE_Barcodes AS BB
			INNER JOIN BE_DocTypes AS DT
			ON
				BB.DocTypeID = DT.DocTypeID
			WHERE
				BB.DocTypeID = @DocTypeID
				AND BB.ForeignKey = @RecruitID
		) AS BB
		WHERE
			RowNumber = 1
	) AS BB
	LEFT OUTER JOIN
	(
		SELECT
			BB.*
			, 1 AS NumberToJoinOn
		FROM BE_Barcodes AS BB
		WHERE
			BB.BarcodeNumber = @BarcodeNumber
			AND BB.DocTypeID = @DocTypeID
	) AS BarcodeExists
	ON
		BB.RowNumber = BarcodeExists.NumberToJoinOn

END
GRANT EXEC ON dbo.custBE_BarcodesExistingBarcode TO PUBLIC
GO