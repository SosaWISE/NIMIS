USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custIE_LocationGetByLocationTypeID')
	BEGIN
		PRINT 'Dropping Procedure custIE_LocationGetByLocationTypeID'
		DROP  Procedure  dbo.custIE_LocationGetByLocationTypeID
	END
GO

PRINT 'Creating Procedure custIE_LocationGetByLocationTypeID'
GO
/******************************************************************************
**		File: custIE_LocationGetByLocationTypeID.sql
**		Name: custIE_LocationGetByLocationTypeID
**		Desc: retrived account system detail to be displayed in the	SWING Screen
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
**	06/24/2014	Reagan	Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custIE_LocationGetByLocationTypeID
(
	@LocationTypeID VARCHAR(20)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @TableName NVARCHAR(60) 
			, @FieldID NVARCHAR(60)
			, @FieldName NVARCHAR(60)
			, @Query NVARCHAR(1000)
	
	

	BEGIN TRY
		BEGIN TRANSACTION;
		
			-- extract table name for the given locationtype
			SET @TableName = (SELECT IELT.TableName FROM IE_LocationTypes IELT WHERE IELT.LocationTypeID = @LocationTypeID )
			SET @FieldName = (SELECT IELT.FieldName FROM IE_LocationTypes IELT WHERE IELT.LocationTypeID = @LocationTypeID )
			SET @FieldID = (SELECT IELT.FieldID FROM IE_LocationTypes IELT WHERE IELT.LocationTypeID = @LocationTypeID )
			
			SET @Query = 'SELECT '+ @FieldID +' AS LocationID, '+ @FieldName +'  AS LocationName FROM ' + @TableName + ' WHERE (IsActive = 1) AND (IsDeleted = 0)'
			EXEC(@Query)
			
			/*
			SELECT 
					123 AS 'LocationID'
					,'TEST' AS 'LocationName'
			--FROM
					--[dbo].MS_Account AS MAS WITH (NOLOCK)
			*/


		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custIE_LocationGetByLocationTypeID TO PUBLIC
GO

/** EXEC dbo.custIE_LocationGetByLocationTypeID  'Technician'*/