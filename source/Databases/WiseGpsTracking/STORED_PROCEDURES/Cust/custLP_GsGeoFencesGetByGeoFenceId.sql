USE [WISE_GPSTRACKING]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custLP_GsGeoFencesGetByGeoFenceId')
	BEGIN
		PRINT 'Dropping Procedure custLP_GsGeoFencesGetByGeoFenceId'
		DROP  Procedure  dbo.custLP_GsGeoFencesGetByGeoFenceId
	END
GO

PRINT 'Creating Procedure custLP_GsGeoFencesGetByGeoFenceId'
GO
/******************************************************************************
**		File: custLP_GsGeoFencesGetByGeoFenceId.sql
**		Name: custLP_GsGeoFencesGetByGeoFenceId
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
**		Auth: Andres Sosa
**		Date: 11/08/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	11/08/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custLP_GsGeoFencesGetByGeoFenceId
(
	@GsGeoFenceId BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Check that we have a GeoFenceID. */
	SELECT * FROM [dbo].LP_GsGeoFences WHERE GsGeoFenceID = @GsGeoFenceId;
END
GO

GRANT EXEC ON dbo.custLP_GsGeoFencesGetByGeoFenceId TO PUBLIC
GO