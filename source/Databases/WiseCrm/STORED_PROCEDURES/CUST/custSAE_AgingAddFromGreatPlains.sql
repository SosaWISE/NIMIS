USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custSAE_AgingAddFromGreatPlains')
	BEGIN
		PRINT 'Dropping Procedure custSAE_AgingAddFromGreatPlains'
		DROP  Procedure  dbo.custSAE_AgingAddFromGreatPlains
	END
GO

PRINT 'Creating Procedure custSAE_AgingAddFromGreatPlains'
GO
/******************************************************************************
**		File: custSAE_AgingAddFromGreatPlains.sql
**		Name: custSAE_AgingAddFromGreatPlains
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by: 
**		Job Agent to update Aging from Great Plains  
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Bob McFadden
**		Date: 08/04/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	08/04/2014	Bob McFadden	Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custSAE_AgingAddFromGreatPlains
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	DECLARE @cntr INT
	DECLARE @steps INT
	DECLARE @sql VARCHAR(MAX)
	DECLARE @AgingStepID VARCHAR(10)
	DECLARE @AGPERAMT VARCHAR(100)
	
	BEGIN TRY
		BEGIN TRANSACTION;

		TRUNCATE TABLE dbo.SAE_Aging

		/*
		StepOrder	AgingStepID	AgingStep
		0			CURR		Current
		1			TO30		1 to 30
		2			TO60		31 to 60
		3			TO90		61 to 90
		4			TO120		91 to 120
		5			GT120		> 120
		*/

		-- @steps is the number of steps in the AE_AgingStep table
		SET @steps = (SELECT MAX(StepOrder) FROM AE_AgingSteps) + 1

		/***  LOOP  ***/
		--@cntr goes from 1 to the number of steps (@steps)
		SET @cntr = 1
		WHILE @cntr <= @steps
		BEGIN
			SET @AgingStepID = (SELECT AgingStepID FROM AE_AgingSteps WHERE StepOrder = (@cntr-1))
			SET @AGPERAMT = 'AGPERAMT_' + CONVERT(VARCHAR,@cntr)

		--	SET @sql = 'SELECT @cntr, @steps, @AgingStepID, @AGPERAMT'

		SET @sql = 
		'INSERT dbo.SAE_Aging 
			(
			CustomerMasterFileID,
			AgingStepId,
			ValueDue
			) 
		SELECT 
			CONVERT(BIGINT,RM00103.CUSTNMBR),
			''' 
			+ @AgingStepID 
			+ ''',
			CONVERT(MONEY,' 
			+ @AGPERAMT
			+ 
		') 
		FROM 
			DYSNEYDAD.NEX.dbo.RM00103 WITH (NOLOCK) 
			JOIN AE_CustomerMasterFiles
				ON CONVERT(BIGINT,RM00103.CUSTNMBR) = AE_CustomerMasterFiles.CustomerMasterFileID
		WHERE ISNUMERIC(RM00103.CUSTNMBR) = 1'

			--PRINT @SQL
			EXEC (@sql)
			SET @cntr = @cntr + 1
		END	
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custSAE_AgingAddFromGreatPlains TO PUBLIC
GO

/** EXEC dbo.custSAE_AgingAddFromGreatPlains */