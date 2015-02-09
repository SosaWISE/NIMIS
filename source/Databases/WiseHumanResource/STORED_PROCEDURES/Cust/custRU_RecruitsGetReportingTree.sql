USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsGetReportingTree')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsGetReportingTree'
		DROP  Procedure  dbo.custRU_RecruitsGetReportingTree
	END
GO

PRINT 'Creating Procedure custRU_RecruitsGetReportingTree'
GO
/******************************************************************************
**		File: custRU_RecruitsGetReportingTree.sql
**		Name: custRU_RecruitsGetReportingTree
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
CREATE Procedure dbo.custRU_RecruitsGetReportingTree
(
	--First 2 Params used for determining the Starting Level
	@Type NVARCHAR(50) = NULL -- Not optional - Valid Values('ReportingLevel' or NULL, and 'UserID')
	, @TypeID INT = NULL --(Optional, pass in null)

	, @HasOwnTeam BIT = NULL --(Optional, pass in null) - Valid Values(NULL, 0, 1) - 0:IsManager, 1:IsMember, NULL:Both
	, @TeamID INT = NULL --(Optional, pass in null)
	, @SeasonID INT = NULL --(Optional, pass in null)	
)
WITH RECOMPILE
AS
BEGIN
	
	SELECT * FROM GetReportingTree_Testing(@Type, @TypeID, @HasOwnTeam, @TeamID, @SeasonID)

END
GO

GRANT EXEC ON dbo.custRU_RecruitsGetReportingTree TO PUBLIC
GO