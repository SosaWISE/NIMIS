USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custQL_CreditReportMaxScoreByCmfID')
	BEGIN
		PRINT 'Dropping Procedure custQL_CreditReportMaxScoreByCmfID'
		DROP  Procedure  dbo.custQL_CreditReportMaxScoreByCmfID
	END
GO

PRINT 'Creating Procedure custQL_CreditReportMaxScoreByCmfID'
GO
/******************************************************************************
**		File: custQL_CreditReportMaxScoreByCmfID.sql
**		Name: custQL_CreditReportMaxScoreByCmfID
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
**		Auth: Aaron Shumway
**		Date: 08/13/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
*******************************************************************************/
CREATE Procedure dbo.custQL_CreditReportMaxScoreByCmfID
(
	@CustomerMasterFileId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	--SELECT * FROM dbo.fxGetQl_CreditReportMax(@CustomerMasterFileId);

/* Moved this into a Function that can be used in a view. */
	SELECT 
		CR.*
	FROM QL_CreditReports AS CR
	INNER JOIN QL_Leads AS L
	ON
		CR.LeadId = L.LeadID
	INNER JOIN
	(
		-- Find max credit score for a CustomerMasterFileId
		SELECT
			L.CustomerMasterFileId,
			MAX(CR.Score) MaxScore
		FROM QL_CreditReports AS CR
		INNER JOIN QL_Leads AS L
		ON
			CR.LeadId = L.LeadID
		WHERE
			CR.IsHit = 1
			AND CR.IsActive = 1 -- ?? not sure what active means for a credit report
			AND CR.IsDeleted = 0
			AND L.CustomerMasterFileId = @CustomerMasterFileId
		GROUP
			BY L.CustomerMasterFileId
	) M ON
		L.CustomerMasterFileId = M.CustomerMasterFileId
		AND CR.Score = M.MaxScore
	WHERE
		CR.IsHit = 1
		AND CR.IsActive = 1 -- ?? not sure what active means for a credit report
		AND CR.IsDeleted = 0
		AND L.CustomerMasterFileId = @CustomerMasterFileId

END
GO

GRANT EXEC ON dbo.custQL_CreditReportMaxScoreByCmfID TO PUBLIC
GO

/**

EXEC dbo.custQL_CreditReportMaxScoreByCmfID 3020200

*/