USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_UsersGetDocumentsByGPID')
	BEGIN
		PRINT 'Dropping Procedure custRU_UsersGetDocumentsByGPID'
		DROP  Procedure  dbo.custRU_UsersGetDocumentsByGPID
	END
GO

PRINT 'Creating Procedure custRU_UsersGetDocumentsByGPID'
GO
/******************************************************************************
**		File: custRU_UsersGetDocumentsByGPID.sql
**		Name: custRU_UsersGetDocumentsByGPID
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
**		Date: 12/05/2013
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:				Description:
**	-----------	-------------		-------------------------------------------
**	12/05/2013	Carly Christiansen	Created by
*******************************************************************************/
CREATE Procedure dbo.custRU_UsersGetDocumentsByGPID
(
	@GPEmployeeID NVARCHAR(10)
)
AS
BEGIN

	-- Enter CODE HERE>
	SELECT DISTINCT
		RUU.GPEmployeeID AS CompanyID
		, Doc.DocumentId
		, Doc.Created
		, Doc.Modified
		, Doc.ModifiedBy
		, DocTypes.Name AS DocumentType
		, DS.Path + '\' + LEFT(DF.DocumentStoreFileID, 3) + '\' + SUBSTRING(DF.DocumentStoreFileID, 4, 3) + '\' + DF.DocumentStoreFileID AS DocPath
		, DF.DocumentStoreFileID AS filename
		, Doc.KeyPropertyValue AS ScannedBarcode
		, 'User Documents' as Season
	FROM RU_Users RUU
		INNER JOIN RU_Recruits RUR
		ON
			RUU.UserID = RUR.UserID
		INNER JOIN SLS_Cms.dbo.BE_Barcodes UserBarcode
		ON
			RUU.UserID = UserBarcode.ForeignKey
		INNER JOIN SLS_DocLink.dbo.Documents AS Doc
		ON
			Doc.KeyPropertyValue = UserBarcode.BarcodeNumber
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
	WHERE RUU.GPEmployeeID = @GPEmployeeID
		AND (DOC.DocumentTypeId = 20)-- 20-HR Documents
		AND DF.DocumentStoreFileID <> ''


UNION

	SELECT DISTINCT
		RUU.GPEmployeeID AS CompanyID
		, Doc.DocumentId
		, Doc.Created
		, Doc.Modified
		, Doc.ModifiedBy
		, DocTypes.Name AS DocumentType
		, DS.Path + '\' + LEFT(DF.DocumentStoreFileID, 3) + '\' + SUBSTRING(DF.DocumentStoreFileID, 4, 3) + '\' + DF.DocumentStoreFileID AS DocPath
		, DF.DocumentStoreFileID AS filename
		, Doc.KeyPropertyValue AS ScannedBarcode
		, RUS.SeasonName as Season

	FROM RU_Users RUU
		INNER JOIN RU_Recruits AS RUR WITH (NOLOCK)
		ON
			RUU.UserID = RUR.UserID
		INNER JOIN RU_Season AS RUS WITH (NOLOCK)
		ON
			RUS.SeasonID = RUR.SeasonID
		INNER JOIN SLS_Cms.dbo.BE_Barcodes AS RecruitBarcode WITH (NOLOCK)
		ON
			RUR.RecruitID = RecruitBarcode.ForeignKey
		INNER JOIN SLS_DocLink.dbo.Documents AS Doc WITH (NOLOCK)
		ON
		Doc.KeyPropertyValue = RecruitBarcode.BarcodeNumber
		INNER JOIN SLS_DocLink.dbo.DocumentTypes AS DocTypes WITH (NOLOCK)
		ON
			(Doc.DocumentTypeId = DocTypes.DocumentTypeId)
		INNER JOIN SLS_DocLink.dbo.PropertyCharValues AS PCV WITH (NOLOCK)
		ON
			(Doc.DocumentId = PCV.ParentId)
		INNER JOIN SLS_DocLink.dbo.DocumentTypePropertys AS DTP WITH (NOLOCK)
		ON
			(PCV.DocumentTypePropertyId = DTP.DocumentTypePropertyId)
			AND (PCV.PropertyID = DTP.PropertyId)
		INNER JOIN SLS_DocLink.dbo.Propertys AS Prop WITH (NOLOCK)
		ON
			(DTP.PropertyId = Prop.PropertyId)
		INNER JOIN SLS_DocLink.dbo.DocumentFiles AS DF WITH (NOLOCK)
		ON
			(Doc.DocumentId = DF.ParentId)
		INNER JOIN SLS_DocLink.dbo.DocumentStores AS DS WITH (NOLOCK)
		ON
			(DF.DocumentStoreID = DS.DocumentStoreID)
	WHERE RUU.GPEmployeeID = @GPEmployeeID
		AND (DOC.DocumentTypeId = 20)-- 20-HR Documents
		AND DF.DocumentStoreFileID <> ''
		AND RecruitBarcode.DocTypeID <> 12
		AND RecruitBarcode.DocTypeID <> 15
		
END
GO

GRANT EXEC ON dbo.custRU_UsersGetDocumentsByGPID TO PUBLIC
GO