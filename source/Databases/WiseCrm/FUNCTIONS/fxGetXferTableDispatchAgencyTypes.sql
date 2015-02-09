USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxGetXferTableDispatchAgencyTypes')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetXferTableDispatchAgencyTypes'
		DROP FUNCTION  dbo.fxGetXferTableDispatchAgencyTypes
	END
GO

PRINT 'Creating FUNCTION fxGetXferTableDispatchAgencyTypes'
GO
/******************************************************************************
**		File: fxGetXferTableDispatchAgencyTypes.sql
**		Name: fxGetXferTableDispatchAgencyTypes
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
**		Date: 01/21/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	01/21/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetXferTableDispatchAgencyTypes
()
RETURNS 
@XRefTable TABLE (AgencyTypeID VARCHAR(50), DispatchAgencyTypeId INT)
AS
BEGIN
	/** Initialize*/
	--INSERT INTO @XRefTable (AgencyTypeID, DispatchAgencyTypeId) VALUES  ( 'PD', 1);
	--INSERT INTO @XRefTable (AgencyTypeID, DispatchAgencyTypeId) VALUES  ( 'FD', 2);
	--INSERT INTO @XRefTable (AgencyTypeID, DispatchAgencyTypeId) VALUES  ( 'MD', 3);
	--INSERT INTO @XRefTable (AgencyTypeID, DispatchAgencyTypeId) VALUES  ( 'GD', 4);

	INSERT INTO @XRefTable (AgencyTypeID, DispatchAgencyTypeId)
	SELECT DAT.MsAgencyTypeNo AS AgencyTypeID, DAT.DispatchAgencyTypeID FROM [dbo].[MS_DispatchAgencyTypes] AS DAT WITH (NOLOCK) WHERE (DAT.MonitoringStationOSId = 'MI_MASTER');

	RETURN
END
GO

/** TEST 
SELECT * FROM dbo.fxGetXferTableDispatchAgencyTypes()
*/