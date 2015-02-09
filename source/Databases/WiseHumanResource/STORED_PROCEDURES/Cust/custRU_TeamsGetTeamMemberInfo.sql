USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_TeamsGetTeamMemberInfo')
	BEGIN
		PRINT 'Dropping Procedure custRU_TeamsGetTeamMemberInfo'
		DROP  Procedure  dbo.custRU_TeamsGetTeamMemberInfo
	END
GO

PRINT 'Creating Procedure custRU_TeamsGetTeamMemberInfo'
GO
/******************************************************************************
**		File: custRU_TeamsGetTeamMemberInfo.sql
**		Name: custRU_TeamsGetTeamMemberInfo
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
CREATE Procedure dbo.custRU_TeamsGetTeamMemberInfo
(
	@TeamID INT
	, @IsDeleted BIT = NULL
)
AS
BEGIN


--	DECLARE @TeamID INT
--	SET @TeamID = 900

--	DECLARE @IsDeleted BIT
--	SET @IsDeleted = NULL

SELECT
	RecUser.FullName
	, RecUser.PublicFullName
	, RecUser.UserID
	, RecUser.RecruitID
	, RecUser.GPEmployeeID AS CompanyID
	, Platinum_Protection_InterimCRM.dbo.FormatPhoneString(RecUser.PhoneCell) AS PhoneCell
	, NULL AS CorporateEmail
	, RecUser.Email
	, RecUser.UserType AS [Description]
	, RecUser.Email AS UserName
	, RecUser.PayScaleID
	, RecUser.PayScaleName AS PayScale
	, RecUser.IsDeleted
	, Struct.TeamID
	
	, Man.FullName AS ManagerFullName
	, Man.PublicFullName AS ManagerPublicFullName
	, Man.UserID AS ManagerUserID
	
FROM VW_RecruitingStructure AS Struct WITH (NOLOCK)--use VW_RecruitingStructure instead of SAE_RecruitingStructure because this is used to display editable information
INNER JOIN vw_RecruitUser AS RecUser WITH (NOLOCK)
ON
	(Struct.RecruitID = RecUser.RecruitID)
LEFT OUTER JOIN vw_RecruitUser AS Man WITH (NOLOCK)
ON
	(RecUser.ReportsToID = Man.RecruitID)
WHERE
	(Struct.TeamID = @TeamID)
	AND (@IsDeleted IS NULL OR RecUser.IsDeleted = @IsDeleted)

----Order By
	ORDER BY
		RecUser.IsDeleted ASC
		, RecUser.FullName
----Order By


END
GO

GRANT EXEC ON dbo.custRU_TeamsGetTeamMemberInfo TO PUBLIC
GO