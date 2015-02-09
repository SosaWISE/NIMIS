USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAE_Aging')
	BEGIN
		PRINT 'Dropping VIEW vwAE_Aging'
		DROP VIEW dbo.vwAE_Aging
	END
GO

PRINT 'Creating VIEW vwAE_Aging'
GO

/****** Object:  View [dbo].[vwAE_Aging]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwAE_Aging.sql
**		Name: vwAE_Aging
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
**		Auth: Andres Sosa
**		Date: 01/02/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	01/02/2014	Andres Sosa	Created
*******************************************************************************/
CREATE VIEW [dbo].[vwAE_Aging]
AS
	/** Enter Query here */
	SELECT
		AGE.CustomerMasterFileID
		, AGE.AgingStepID
		, AGE.AgingStep
		, AGE.ValueDue
		, AGE.StepOrder
	FROM
		(SELECT
			SAG.CustomerMasterFileID
			, ASTP.AgingStepID
			, ASTP.AgingStep
			, SAG.ValueDue
			, ASTP.StepOrder
			, ROW_NUMBER() OVER (PARTITION BY SAG.CustomerMasterFileID ORDER BY ASTP.StepOrder) AS RowNumber
		FROM
			[dbo].SAE_Aging AS SAG WITH (NOLOCK)
			INNER JOIN [dbo].AE_AgingSteps AS ASTP WITH (NOLOCK)
			ON
				(ASTP.AgingStepID = SAG.AgingStepId)
		) AS AGE;

GO
/* TEST */
-- SELECT * FROM vwAE_Aging
