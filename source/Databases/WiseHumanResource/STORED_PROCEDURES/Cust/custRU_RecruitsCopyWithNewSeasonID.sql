USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custRU_RecruitsCopyWithNewSeasonID')
	BEGIN
		PRINT 'Dropping Procedure custRU_RecruitsCopyWithNewSeasonID'
		DROP  Procedure  dbo.custRU_RecruitsCopyWithNewSeasonID
	END
GO

PRINT 'Creating Procedure custRU_RecruitsCopyWithNewSeasonID'
GO
/******************************************************************************
**		File: custRU_RecruitsCopyWithNewSeasonID.sql
**		Name: custRU_RecruitsCopyWithNewSeasonID
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
CREATE Procedure dbo.custRU_RecruitsCopyWithNewSeasonID
(
	@RecruitId INT
	, @SeasonId INT
)
AS
BEGIN
	-- Run Query
	INSERT INTO RU_Recruits (
		UserID
		, UserTypeId
		, ReportsToID
		, CurrentAddressID
		, SeasonID
		, OwnerApprovalId
		, TeamID
		, PayScaleID
		, SchoolId
		, OwnerApprovalDate
		, ManagerApprovalDate
		, EmergencyName
		, EmergencyPhone
		, EmergencyRelationship
		, IsRecruiter
		, PreviousSummer
		, SignatureDate
		, GPW4Allowances
		, GPW9Name
		, GPW9BusinessName
		, GPW9TIN
		, CBxSocialSecCard
		, CBxDriversLicense
		, CBxW4
		, CBxI9
		, CBxW9
		, IsActive
		, IsDeleted
		, CreatedBy
		, CreatedOn
		, ModifiedBy
		, ModifiedOn
		)
	SELECT
		UserID
		, UserTypeId
		, NULL AS ReportsToID
		, CurrentAddressID
		, @SeasonID AS SeasonID
		, OwnerApprovalId
		, NULL AS TeamID
		, PayScaleID
		, SchoolId
		, OwnerApprovalDate
		, ManagerApprovalDate
		, EmergencyName
		, EmergencyPhone
		, EmergencyRelationship
		, IsRecruiter
		, PreviousSummer
		, SignatureDate
		, GPW4Allowances
		, GPW9Name
		, GPW9BusinessName
		, GPW9TIN
		, CBxSocialSecCard
		, CBxDriversLicense
		, CBxW4
		, CBxI9
		, CBxW9
		, 1 AS IsActive
		, 0 AS IsDeleted
		, CreatedBy
		, CreatedOn
		, ModifiedBy
		, ModifiedOn
	FROM
		RU_Recruits AS RU_Recruits_1
	WHERE
		(RecruitID = @RecruitID)

	-- Get the New Identity
	DECLARE @NewRecruitID INT
	SET @NewRecruitID = @@IDENTITY
	
	-- Show the result
	SELECT * FROM RU_Recruits WHERE RecruitID = @NewRecruitID
END

GO

GRANT EXEC ON dbo.custRU_RecruitsCopyWithNewSeasonID TO PUBLIC
GO