USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetEmergencyContactTypeIDByPlatinumEMCTypeID')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetEmergencyContactTypeIDByPlatinumEMCTypeID'
		DROP FUNCTION  dbo.fxGetEmergencyContactTypeIDByPlatinumEMCTypeID
	END
GO

PRINT 'Creating FUNCTION fxGetEmergencyContactTypeIDByPlatinumEMCTypeID'
GO
/******************************************************************************
**		File: fxGetEmergencyContactTypeIDByPlatinumEMCTypeID.sql
**		Name: fxGetEmergencyContactTypeIDByPlatinumEMCTypeID
**
**		Description: 
**		The table for Emergency Contact Types has changed.  When accounts
**		SWING from the old Platinum database to the new database the new code
**		will be assigned by this function.  The old code is passed and the new
**		code is returned.
**
**      Pass value: The ID for the old EmergencyContactType
**		Return value: The ID for the new EmergencyContactType
** 
**		Called by: WISE_CRM.dbo.custAE_CustomerSWINGFromInterim
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**     23	Dealer						137	Dealer
**     24	Employee					89	Employee
**     25	Friend						94	Friend
**     26	Janitorial / Housekeeping	102	Housekeeper
**     27	Maintenance					139	Maintenance
**     28	Manager						106	Manager
**     29	Neighbor					109	Neighbor
**     30	On Site Security			125	Security
**     31	Owner						116	Owner
**     32	Relative					122	Relative
**     33	Resident					138	Resident
**		<Any others>					116 Owner
**
**		Auth: Bob McFadden
**		Date: 06/02/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	06/01/2014	Bob McFadden	Created
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetEmergencyContactTypeIDByPlatinumEMCTypeID
(
	@EmergencyContactRelationshipID INT
)
RETURNS INT
AS
BEGIN
	DECLARE @RelationshipID INT;

	SELECT
		@RelationshipID = 
		CASE
			WHEN @EmergencyContactRelationshipID = 23 THEN	137
			WHEN @EmergencyContactRelationshipID = 24 THEN	89
			WHEN @EmergencyContactRelationshipID = 25 THEN	94
			WHEN @EmergencyContactRelationshipID = 26 THEN	102
			WHEN @EmergencyContactRelationshipID = 27 THEN	139
			WHEN @EmergencyContactRelationshipID = 28 THEN	106
			WHEN @EmergencyContactRelationshipID = 29 THEN	109
			WHEN @EmergencyContactRelationshipID = 30 THEN	125
			WHEN @EmergencyContactRelationshipID = 31 THEN	116
			WHEN @EmergencyContactRelationshipID = 32 THEN	122
			WHEN @EmergencyContactRelationshipID = 33 THEN	138
			ELSE 116
		END

	RETURN @RelationshipID
END
GO


