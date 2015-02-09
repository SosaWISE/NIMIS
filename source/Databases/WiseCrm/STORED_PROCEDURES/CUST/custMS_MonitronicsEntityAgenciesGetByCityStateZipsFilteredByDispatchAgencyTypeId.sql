USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId'
		DROP  Procedure  dbo.custMS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId.sql
**		Name: custMS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId
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
**		Date: 12/08/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/08/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId
(
	@City NVARCHAR(100)
	, @State NVARCHAR(2)
	, @Zip NVARCHAR(15)
	, @DispatchAgencyTypeId INT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		-- Initialize
		DECLARE @AgencyTypeId VARCHAR(50);
		SELECT @AgencyTypeId = CASE
				WHEN @DispatchAgencyTypeId = 1 THEN 'PD'
				WHEN @DispatchAgencyTypeId = 2 THEN 'FD'
				WHEN @DispatchAgencyTypeId = 3 THEN 'MD'
				WHEN @DispatchAgencyTypeId = 4 THEN 'GD'
				ELSE NULL
			END;
	
		-- Query returns associated agencies.
		SELECT
			MEA.EntityAgenciesID AS [DispatchAgencyID]
			, CASE
				WHEN MEA.AgencyTypeId = 'PD' THEN CAST(1 AS TINYINT)
				WHEN MEA.AgencyTypeId = 'FD' THEN CAST(2 AS TINYINT)
				WHEN MEA.AgencyTypeId = 'MD' THEN CAST(3 AS TINYINT)
				WHEN MEA.AgencyTypeId = 'GD' THEN CAST(4 AS TINYINT)
				ELSE CAST(NULL AS TINYINT)
			  END AS DispatchAgencyTypeId
			, 'MI_MASTER' AS MonitoringStationOSId
			, MEA.AgencyName AS [DispatchAgencyName]
			, MEA.AgencyNumberID AS [MsAgencyNumber]
			, CAST(NULL AS NVARCHAR(100)) AS Address1
			, CAST(NULL AS NVARCHAR(100)) AS Address2
			, MEA.CityName AS [City]
			, MEA.StateId AS [State]
			, MEA.ZipCode
			, MEA.Phone1
			, CAST(NULL AS NVARCHAR(15)) AS Phone2
			, DAT.DispatchAgencyType
		FROM
			[dbo].[MS_MonitronicsEntityAgencies] AS MEA WITH (NOLOCK)
			INNER JOIN dbo.fxGetXferTableDispatchAgencyTypes() AS XREF
			ON
				(XREF.AgencyTypeID = MEA.AgencyTypeId)
			INNER JOIN [dbo].[MS_DispatchAgencyTypes] AS DAT WITH (NOLOCK)
			ON
				(DAT.DispatchAgencyTypeID = XREF.DispatchAgencyTypeId)
		WHERE
			(MEA.IsDeleted = 0 AND MEA.IsActive = 1)
			AND (@DispatchAgencyTypeId IS NULL OR MEA.AgencyTypeId = @AgencyTypeId)
			AND (@City IS NULL OR MEA.CityName LIKE '%' + @City + '%')
			AND (@State IS NULL OR MEA.StateId = @State)
			AND (@Zip IS NULL OR MEA.ZipCode = @Zip);
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityAgenciesGetByCityStateZipsFilteredByDispatchAgencyTypeId 'OREM', 'UT', NULL, NULL*/