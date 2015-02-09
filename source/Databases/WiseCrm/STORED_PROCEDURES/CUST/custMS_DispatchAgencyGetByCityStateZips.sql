USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_DispatchAgencyGetByCityStateZips')
	BEGIN
		PRINT 'Dropping Procedure custMS_DispatchAgencyGetByCityStateZips'
		DROP  Procedure  dbo.custMS_DispatchAgencyGetByCityStateZips
	END
GO

PRINT 'Creating Procedure custMS_DispatchAgencyGetByCityStateZips'
GO
/******************************************************************************
**		File: custMS_DispatchAgencyGetByCityStateZips.sql
**		Name: custMS_DispatchAgencyGetByCityStateZips
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
**		Date: 12/02/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/02/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_DispatchAgencyGetByCityStateZips
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

		SELECT
			MDA.*
		FROM
			[dbo].[vwMS_DispatchAgencies] AS MDA WITH (NOLOCK)
		WHERE
			(@City IS NULL OR (MDA.City LIKE '%' + @City + '%'))
			AND (@State IS NULL OR (MDA.[State] = @State))
			AND (@Zip IS NULL OR (MDA.ZipCode = @Zip));

		-- Query returns associated agencies.
		--SELECT
		--	MDA.EntityAgenciesID AS DispatchAgencyID
		--	, DAT.DispatchAgencyTypeId
		--	, 'MI_MASTER' AS MonitoringStationOSId
		--	, MDA.AgencyName AS DispatchAgencyName
		--	, MDA.AgencyNumberID AS [MsAgencyNumber]
		--	, NULL AS Address1
		--	, NULL AS Address2
		--	, @City AS City
		--	, @State AS STATE
  --          , MDA.ZipCode
		--	, MDA.Phone1
		--	, NULL AS Phone2
		--	, DAT.DispatchAgencyType
		--FROM
		--	[dbo].MS_MonitronicsEntityAgencies AS MDA WITH (NOLOCK)
		--	INNER JOIN [dbo].[fxGetXferTableDispatchAgencyTypes]() AS XREF
		--	ON
		--		(XREF.AgencyTypeID = MDA.AgencyTypeId)
		--	INNER JOIN [dbo].[MS_DispatchAgencyTypes] AS DAT WITH (NOLOCK)
		--	ON
		--		(DAT.DispatchAgencyTypeID = XREF.DispatchAgencyTypeId)
		--WHERE
		--	(@City IS NULL OR (MDA.CityName LIKE '%' + @City + '%'))
		--	AND (@State IS NULL OR (MDA.StateId = @State))
		--	AND (@Zip IS NULL OR (ZipCode = @Zip));

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_DispatchAgencyGetByCityStateZips TO PUBLIC
GO

/** EXEC dbo.custMS_DispatchAgencyGetByCityStateZips 'Orem', 'UT', '84097' */