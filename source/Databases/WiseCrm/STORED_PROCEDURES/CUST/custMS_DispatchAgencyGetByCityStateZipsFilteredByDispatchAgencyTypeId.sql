USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId')
	BEGIN
		PRINT 'Dropping Procedure custMS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId'
		DROP  Procedure  dbo.custMS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId
	END
GO

PRINT 'Creating Procedure custMS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId'
GO
/******************************************************************************
**		File: custMS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId.sql
**		Name: custMS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId
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
CREATE Procedure dbo.custMS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId
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
	
		-- Query returns associated agencies.
		SELECT
			DAGC.*
			, DTYP.DispatchAgencyType
		FROM 
			[dbo].[MS_DispatchAgencies] AS DAGC WITH (NOLOCK) 
			INNER JOIN [dbo].[MS_DispatchAgencyTypes] AS DTYP WITH (NOLOCK)
			ON
				(DAGC.DispatchAgencyTypeId = DTYP.DispatchAgencyTypeId)
				AND (DTYP.DispatchAgencyTypeId = @DispatchAgencyTypeId)
		WHERE
			(DAGC.DispatchAgencyID
				IN (SELECT 
						DACZL.DispatchAgencyId 
					FROM 
						[dbo].[MS_DispatchAgencyCityZipLookups] AS DACZL WITH (NOLOCK)
					WHERE
						(DACZL.CityZipId 
							IN (SELECT
								DACZ.CityZipID
							FROM
								[dbo].[MS_DispatchAgencyCityZips] AS DACZ WITH (NOLOCK)
							WHERE
								CityName = @City
								AND State = @State
								AND Zip = @Zip))));
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId TO PUBLIC
GO

/** EXEC dbo.custMS_DispatchAgencyGetByCityStateZipsFilteredByDispatchAgencyTypeId */