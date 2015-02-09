USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE  (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetEmergencyContactPhoneTypeIDByPlatinumEmergencyContactPhoneTypeID')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetEmergencyContactPhoneTypeIDByPlatinumEmergencyContactPhoneTypeID'
		DROP FUNCTION  dbo.fxGetEmergencyContactPhoneTypeIDByPlatinumEmergencyContactPhoneTypeID
	END
GO
PRINT 'Creating FUNCTION fxGetEmergencyContactPhoneTypeIDByPlatinumEmergencyContactPhoneTypeID'
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************************
**		File: fxGetEmergencyContactPhoneTypeIDByPlatinumEmergencyContactPhoneTypeID.sql
**		Name: fxGetEmergencyContactPhoneTypeIDByPlatinumEmergencyContactPhoneTypeID
**		Desc: 
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**		1	Cellular					1 Cell or Mobile
**		2	Email						2 Email
**		3	Fax							3 Fax
**		4	Home						4 Home
**		5	Pager						5 Pager
**		6	Work						6 Work or office
**		7	Cell or Mobile				1 Cell or mobile
**
**		Auth: Bob McFadden
**		Date: 06/02/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	06/02/2014	Bob McFadden	Created
**	
*******************************************************************************/

CREATE FUNCTION [dbo].[fxGetEmergencyContactPhoneTypeIDByPlatinumEmergencyContactPhoneTypeID]
(
	@EmergencyContactPhoneTypeID INT
)
RETURNS INT
AS
BEGIN
	DECLARE @PhoneTypeID INT;

	SELECT
		@PhoneTypeID = 
		CASE
			--WHEN @EmergencyContactPhoneTypeID = 1 THEN 1		-- Cellular --> Cell or Mobile
			--WHEN @EmergencyContactPhoneTypeID = 2 THEN 2		-- Email	--> Email
			--WHEN @EmergencyContactPhoneTypeID = 3 THEN 3		-- Fax		--> Fax
			--WHEN @EmergencyContactPhoneTypeID = 4 THEN 4		-- Home		--> Home
			--WHEN @EmergencyContactPhoneTypeID = 5 THEN 5		-- Pager	--> Pager
			--WHEN @EmergencyContactPhoneTypeID = 6 THEN 6		-- Word		--> Work
			WHEN @EmergencyContactPhoneTypeID = 7 THEN 1		-- Cell or Mobile	--> Cell or Mobile
			ELSE @EmergencyContactPhoneTypeID	--Home
		END

	RETURN @PhoneTypeID
END



GO


