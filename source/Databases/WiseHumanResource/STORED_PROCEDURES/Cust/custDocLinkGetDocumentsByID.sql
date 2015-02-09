USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custDocLinkGetDocumentsByID')
	BEGIN
		PRINT 'Dropping Procedure custDocLinkGetDocumentsByID'
		DROP  Procedure  dbo.custDocLinkGetDocumentsByID
	END
GO

PRINT 'Creating Procedure custDocLinkGetDocumentsByID'
GO
/******************************************************************************
**		File: custDocLinkGetDocumentsByID.sql
**		Name: custDocLinkGetDocumentsByID
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
*******************************************************************************/
CREATE Procedure dbo.custDocLinkGetDocumentsByID
(
	@DocumentID INT
)
AS
BEGIN
	SELECT DISTINCT
		Doc.DocumentId
		, Doc.Created
		, Doc.Modified
		, Doc.ModifiedBy
		, DocTypes.Name AS DocumentType
		, DS.Path + '\' + LEFT(DF.DocumentStoreFileID, 3) + '\' + SUBSTRING(DF.DocumentStoreFileID, 4, 3) + '\' + DF.DocumentStoreFileID AS DocPath
		, DF.DocumentStoreFileID AS filename
		, Doc.KeyPropertyValue AS ScannedBarcode

	FROM SLS_DocLink.dbo.Documents AS Doc
	INNER JOIN SLS_DocLink.dbo.DocumentTypes AS DocTypes
	ON
		(Doc.DocumentTypeId = DocTypes.DocumentTypeId)
	INNER JOIN SLS_DocLink.dbo.PropertyCharValues AS PCV
	ON
		(Doc.DocumentId = PCV.ParentId)
	INNER JOIN SLS_DocLink.dbo.DocumentTypePropertys AS DTP
	ON
		(PCV.DocumentTypePropertyId = DTP.DocumentTypePropertyId)
		AND (PCV.PropertyID = DTP.PropertyId)
	INNER JOIN SLS_DocLink.dbo.Propertys AS Prop
	ON
		(DTP.PropertyId = Prop.PropertyId)
	INNER JOIN SLS_DocLink.dbo.DocumentFiles AS DF
	ON
		(Doc.DocumentId = DF.ParentId)
	INNER JOIN SLS_DocLink.dbo.DocumentStores AS DS
	ON
		(DF.DocumentStoreID = DS.DocumentStoreID)
	WHERE
		Doc.DocumentID = @DocumentID
END
GO

GRANT EXEC ON dbo.custDocLinkGetDocumentsByID TO PUBLIC
GO