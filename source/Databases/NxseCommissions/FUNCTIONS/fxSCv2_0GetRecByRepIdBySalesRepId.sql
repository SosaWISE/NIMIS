USE [NXSE_Commissions]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxSCv2_0GetRecByRepIdBySalesRepId')
	BEGIN
		PRINT 'Dropping FUNCTION fxSCv2_0GetRecByRepIdBySalesRepId'
		DROP FUNCTION  dbo.fxSCv2_0GetRecByRepIdBySalesRepId
	END
GO

PRINT 'Creating FUNCTION fxSCv2_0GetRecByRepIdBySalesRepId'
GO
/******************************************************************************
**		File: fxSCv2_0GetRecByRepIdBySalesRepId.sql
**		Name: fxSCv2_0GetRecByRepIdBySalesRepId
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
**		Date: 05/01/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	05/01/2015	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxSCv2_0GetRecByRepIdBySalesRepId
(
	@SalesRepId VARCHAR(25)
)
RETURNS VARCHAR(25)
AS
BEGIN
	/** Declarations */
	DECLARE @RecByRepId VARCHAR(25);

	/** Execute actions. */
	SELECT 
		@RecByRepId = RU.GPEmployeeId
	FROM
		[WISE_HumanResource].[dbo].[RU_Users] AS RU WITH (NOLOCK)
		INNER JOIN [WISE_HumanResource].[dbo].[RU_Users] AS RU1 WITH (NOLOCK)
		ON
			(RU1.RecruitedById = RU.UserID)
	WHERE
		(RU1.GPEmployeeId = @SalesRepId);

	RETURN @RecByRepId;
END
GO
/** TEST
SELECT dbo.fxSCv2_0GetRecByRepIdBySalesRepId('SHERJ001')
 */