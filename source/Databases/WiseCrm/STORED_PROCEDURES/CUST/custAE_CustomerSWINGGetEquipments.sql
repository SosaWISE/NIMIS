USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGGetEquipments')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGGetEquipments'
		DROP  Procedure  dbo.custAE_CustomerSWINGGetEquipments
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGGetEquipments'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGGetEquipments.sql
**		Name: custAE_CustomerSWINGGetEquipments
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
**		Date: 03/28/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/22/2014	Junryl/Reagan		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerSWINGGetEquipments
(
    @InterimAccountID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
	
        /** Return Equipment Name, Zone and Location **/
		SELECT
			EQ.FullName
			, AZA.Zone AS [ZoneNumber]
			, MZT.ZoneTypeName
			, ISNULL(MEL.EquipmentLocationDesc, '[No Loc Needed]') AS EquipmentLocationDesc
			, ROW_NUMBER() OVER (ORDER BY AZA.Zone) AS [RowNumber]
		FROM
			[Platinum_Protection_InterimCRM].dbo.[MS_AccountInventory]  AS MAI WITH (NOLOCK)
			INNER JOIN [Platinum_Protection_InterimCRM].dbo.[MS_Equipment] AS EQ WITH (NOLOCK)
			ON
			   (MAI.EquipmentID = EQ.EquipmentID)
			LEFT OUTER JOIN [Platinum_Protection_InterimCRM].[dbo].[MS_AccountZoneAssignment] AS AZA WITH (NOLOCK)
			ON
				(AZA.AccountInventoryID = MAI.AccountInventoryID)
			INNER JOIN [Platinum_Protection_InterimCRM].dbo.[MS_ZoneType] AS MZT WITH (NOLOCK)
			ON
			   (MZT.ZoneTypeID = EQ.ZoneTypeID)

			LEFT OUTER JOIN [Platinum_Protection_InterimCRM].dbo.[MS_EquipmentLocation]  AS MEL WITH (NOLOCK)
			ON
			   (MEL.EquipmentLocationID = MAI.EquipmentLocationId)

			WHERE
			   (MAI.AccountID = @InterimAccountID)
			ORDER BY
				AZA.Zone;
	
	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerSWINGGetEquipments TO PUBLIC
GO

/** EXEC dbo.custAE_CustomerSWINGGetEquipments 583601 */

/**QUERIES NOTES 

SELECT TOP 10 * FROM [Platinum_Protection_InterimCRM].dbo.[MS_AccountInventory]  
SELECT TOP 10 * FROM [Platinum_Protection_InterimCRM].dbo.[MS_Equipment]
SELECT TOP 10 * FROM [Platinum_Protection_InterimCRM].dbo.[MS_ZoneType] 
SELECT TOP 10 * FROM [Platinum_Protection_InterimCRM].dbo.[MS_EquipmentLocation]

**/