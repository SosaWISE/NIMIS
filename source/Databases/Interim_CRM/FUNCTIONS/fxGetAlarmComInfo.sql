USE [Platinum_Protection_InterimCRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxGetAlarmComInfo')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetAlarmComInfo'
		DROP FUNCTION  dbo.fxGetAlarmComInfo
	END
GO

PRINT 'Creating FUNCTION fxGetAlarmComInfo'
GO
/******************************************************************************
**		File: fxGetAlarmComInfo.sql
**		Name: fxGetAlarmComInfo
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
CREATE FUNCTION dbo.fxGetAlarmComInfo
(
	@IndustryAccountID INT
)
RETURNS 
@ParsedList table
(
	IndustryAccountID INT
	, BlockAccountId INT
	, CSID VARCHAR(12)
	, CustomerID INT
	, SerialNumber NVARCHAR(50)
	, FirmwareVersion INT
	, IsTwoWay BIT
	, ServicePlanPackageId INT
	, ServicePlanType VARCHAR(50)
	, ServicePlanTotalPrice MONEY
	, RegisteredDate DATETIME
	, UnRegisteredDate DATETIME
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
		, CustomerID
		, SerialNumber
		, FirmwareVersion
		, IsTwoWay
		, ServicePlanPackageId
		, ServicePlanType
		, ServicePlanTotalPrice
		, RegisteredDate
		, UnRegisteredDate
	)
	SELECT
		IND.IndustryAccountID
		, IND.BlockAccountId
		, RL.MSDesignator  + LTRIM(RTRIM(RLBA.SubscriberId)) AS CSID
		, RLBAA.CustomerId
		, RLBAA.SerialNumber
		, ModemFirmwareVersion
		, RLBAA.IsTwoWay
		, ACM.ServicePlanPackageId
		, ACM.ServicePlanType
		, ACM.ServicePlanTotalPrice
		, RLBAA.RegisteredDate
		, RLBAA.UnRegisteredDate
	FROM
		[dbo].MS_IndustryAccount AS IND
		INNER JOIN [dbo].MS_ReceiverLine AS RL
		ON
			(RL.ReceiverLineID = IND.ReceiverLineID)
		INNER JOIN [dbo].MS_ReceiverLineBlockAccount AS RLBA
		ON
			(RLBA.BlockAccountId = IND.BlockAccountId)
		INNER JOIN [dbo].MS_ReceiverLineBlockAccountAlarmCom AS RLBAA
		ON
			(RLBAA.BlockAccountId = RLBA.BlockAccountId)
		LEFT OUTER JOIN [dbo].[MS_AlarmDotComAccountStatus] AS ACM
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

SELECT * FROM [dbo].fxGetAlarmComInfo(242424);
SELECT * FROM [dbo].fxGetAlarmComInfo(242425);