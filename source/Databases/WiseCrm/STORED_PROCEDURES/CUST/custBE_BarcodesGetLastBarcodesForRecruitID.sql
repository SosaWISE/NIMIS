USE [WISE_CRM]
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custBE_BarcodesGetLastBarcodesForRecruitID')
	BEGIN
		PRINT 'Dropping Procedure custBE_BarcodesGetLastBarcodesForRecruitID'
		DROP  Procedure  dbo.custBE_BarcodesGetLastBarcodesForRecruitID
	END
GO

PRINT 'Creating Procedure custBE_BarcodesGetLastnBarcodesForRecruitID'
GO
/******************************************************************************
**		File: custBE_BarcodesGetLastBarcodesForRecruitID.sql
**		Name: custBE_BarcodesGetLastBarcodesForRecruitID
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:a
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés E. Sosa
**		Date: 01/10/2011
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/10/2011	Andrés E. Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custBE_BarcodesGetLastBarcodesForRecruitID
(
	@RecruitID INT
)
AS
BEGIN

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
			--BB.DocTypeID IN (1, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15)
			DT.DocTypeColumnID IN (1, 4)
			AND BB.ForeignKey = @RecruitID
	) AS BB
	WHERE
		BB.RowNumber = 1
	--GROUP BY
	--	BEB.ForeignKey
	ORDER BY
		BB.[Date]

END
GO

GRANT EXEC ON dbo.custBE_BarcodesGetLastBarcodesForRecruitID TO PUBLIC
GO

