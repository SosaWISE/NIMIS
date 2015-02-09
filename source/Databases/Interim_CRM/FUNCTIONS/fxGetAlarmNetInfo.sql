USE [Platinum_Protection_InterimCRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxGetAlarmNetInfo')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetAlarmNetInfo'
		DROP FUNCTION  dbo.fxGetAlarmNetInfo
	END
GO

PRINT 'Creating FUNCTION fxGetAlarmNetInfo'
GO
/******************************************************************************
**		File: fxGetAlarmNetInfo.sql
**		Name: fxGetAlarmNetInfo
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
**		Date: 03/10/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	03/10/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetAlarmNetInfo
(
	@IndustryAccountID INT
)
RETURNS 
@ParsedList table
(
	IndustryAccountID INT
	, BlockAccountId INT
	, CSID VARCHAR(12)
	, MACAddress NVARCHAR(50)
	, CRCNumber VARCHAR(10)
	, RegisteredDate DATETIME
	, UnRegisteredDate DATETIME
	, CityID CHAR(2)
	, AN_CSID CHAR(2)
	, SubscriberId VARCHAR(8)
	, TransferredDate DATETIME
	, TransferDirection VARCHAR(20)
	, DeviceID VARCHAR(20)
	, DeviceMode INT
	, DeviceType VARCHAR(30)
	, Supervision SMALLINT
	, RegisterStatus VARCHAR(20)
)
AS
BEGIN
	/** Declarations */
	DECLARE @ID varchar(20);

	/** Initialize. */
	INSERT INTO @ParsedList (
		IndustryAccountID
		, BlockAccountId
		, CSID
		, MACAddress
		, CRCNumber
		, RegisteredDate
		, UnRegisteredDate
		, CityID
		, AN_CSID
		, SubscriberId
		, TransferredDate
		, TransferDirection
		, DeviceID
		, DeviceMode
		, DeviceType
		, Supervision
		, RegisterStatus
	)
	SELECT
		IND.IndustryAccountID
		, IND.BlockAccountId
		, RL.MSDesignator  + LTRIM(RTRIM(RLBA.SubscriberId)) AS CSID
		, RLBAN.MACAddress
		, RLBAN.CRCNumber
		, RLBAN.RegisteredDate
		, RLBAN.UnRegisteredDate
		, ACM.CityID
		, ACM.CSID
		, ACM.SubscriberId
		, ACM.TransferredDate
		, ACM.TransferDirection
		, CASE 
			WHEN ACM.DeviceID IS NULL THEN '00D02D' + RLBAN.MACAddress
			ELSE ACM.DeviceID
		END AS DeviceID
		, ACM.DeviceMode
		, ACM.DeviceType
		, ACM.Supervision
		, ACM.RegisterStatus
	FROM
		[dbo].MS_IndustryAccount AS IND
		INNER JOIN [dbo].MS_ReceiverLine AS RL
		ON
			(RL.ReceiverLineID = IND.ReceiverLineID)
		INNER JOIN [dbo].MS_ReceiverLineBlockAccount AS RLBA
		ON
			(RLBA.BlockAccountId = IND.BlockAccountId)
		INNER JOIN [dbo].MS_ReceiverLineBlockAccountAlarmNet AS RLBAN
		ON
			(RLBAN.BlockAccountId = RLBA.BlockAccountId)
		LEFT OUTER JOIN [dbo].[MS_AlarmnetMonitoringStatus] AS ACM
		ON
			(ACM.BlockAccountID = RLBA.BlockAccountId)
	WHERE
		(IND.IndustryAccountID = @IndustryAccountID);

	/** Check to see if there is a result there. */
	IF (NOT EXISTS(SELECT * FROM @ParsedList))
	BEGIN
		INSERT INTO @ParsedList(IndustryAccountID) VALUES (@IndustryAccountID);
	END

	RETURN;
END
GO

SELECT * FROM [dbo].fxGetAlarmNetInfo(122604);
SELECT * FROM [dbo].fxGetAlarmNetInfo(212471);
SELECT * FROM [dbo].fxGetAlarmNetInfo(206156);
SELECT * FROM [dbo].fxGetAlarmNetInfo(22553);