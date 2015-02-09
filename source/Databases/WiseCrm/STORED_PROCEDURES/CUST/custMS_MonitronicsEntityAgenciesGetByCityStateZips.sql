USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityAgenciesGetByCityStateZips')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityAgenciesGetByCityStateZips'
		DROP  Procedure  dbo.custMS_MonitronicsEntityAgenciesGetByCityStateZips
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityAgenciesGetByCityStateZips'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityAgenciesGetByCityStateZips.sql
**		Name: custMS_MonitronicsEntityAgenciesGetByCityStateZips
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
**		Date: 12/09/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/09/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityAgenciesGetByCityStateZips
(
	@City NVARCHAR(100)
	, @State NVARCHAR(2)
	, @Zip NVARCHAR(15)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		-- Query returns associated agencies.

		SELECT
			--MDA.MoniDispatchAgencyID AS DispatchAgencyID
			MEA.EntityAgenciesID AS DispatchAgencyID
			, MDAT.DispatchAgencyTypeID
			, 'MI_MASTER' AS MonitoringStationOSId
			, MEA.AgencyName AS DispatchAgencyName
			, MEA.AgencyNumberID AS [MsAgencyNumber]
			, NULL AS Address1
			, NULL AS Address2
			, MEA.CityName AS City
			--, @State AS STATE
			, MEA.StateId AS [State]
            , MEA.ZipCode
			, MEA.Phone1
			, NULL AS Phone2
			, MDAT.DispatchAgencyType
		FROM
			[dbo].[MS_MonitronicsEntityAgencies] AS MEA WITH (NOLOCK)
			INNER JOIN dbo.fxGetXferTableDispatchAgencyTypes() AS XREF
			ON
				(XREF.AgencyTypeID = MEA.AgencyTypeId)
			INNER JOIN [dbo].[MS_DispatchAgencyTypes] AS MDAT WITH (NOLOCK)
			ON
				(MDAT.DispatchAgencyTypeID = XREF.DispatchAgencyTypeId)
		WHERE
			((@City IS NULL) OR (MEA.CityName LIKE '%' + @City + '%'))
			AND ((@State IS NULL) OR (MEA.StateId = @State))
			AND ((@Zip IS NULL) OR (MEA.ZipCode = @Zip));

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityAgenciesGetByCityStateZips TO PUBLIC
GO

/** 
	EXEC dbo.custMS_MonitronicsEntityAgenciesGetByCityStateZips 'Orem', 'UT', '84097'
	EXEC dbo.custMS_MonitronicsEntityAgenciesGetByCityStateZips 'Orem', NULL, NULL
 */