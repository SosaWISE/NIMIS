USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetMsAccountLastAvailableZone')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetMsAccountLastAvailableZone'
		DROP FUNCTION  dbo.fxGetMsAccountLastAvailableZone
	END
GO

PRINT 'Creating FUNCTION fxGetMsAccountLastAvailableZone'
GO
/******************************************************************************
**		File: fxGetMsAccountLastAvailableZone.sql
**		Name: fxGetMsAccountLastAvailableZone
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
**
**		Auth: Andrés E. Sosa
**		Date: 03/03/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	03/03/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetMsAccountLastAvailableZone
(
	@AccountID BIGINT
)
RETURNS VARCHAR(3)
AS
BEGIN
	/** DECLARATIONS */
	DECLARE @ZoneResult VARCHAR(3)
		, @Zone INT;

	SELECT
		@ZoneResult = CASE 
			WHEN MAX(ZONE) IS NULL THEN '001'
			ELSE MAX(ZONE)
		END
	FROM
		[dbo].[MS_AccountZoneAssignments] AS MAZA WITH (NOLOCK)
		INNER JOIN [dbo].[MS_AccountEquipment] AS MAE WITH (NOLOCK)
		ON
			(MAE.AccountEquipmentID = MAZA.AccountEquipmentId)
			AND (MAE.AccountId = @AccountID);
	
	/** Add 1 for next zone. */
	SET @Zone = CAST(@ZoneResult AS INT) + 1;
	SELECT @ZoneResult = dbo.fxSubscriberNumberWithPadding(@Zone, 3, '0');

	/** Return result. */
	RETURN @ZoneResult;
END
GO

SELECT [dbo].fxGetMsAccountLastAvailableZone (2434343) AS Zone