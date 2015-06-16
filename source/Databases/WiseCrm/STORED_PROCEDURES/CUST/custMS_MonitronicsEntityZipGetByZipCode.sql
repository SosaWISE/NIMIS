USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityZipGetByZipCode')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityZipGetByZipCode'
		DROP  Procedure  dbo.custMS_MonitronicsEntityZipGetByZipCode
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityZipGetByZipCode'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityZipGetByZipCode.sql
**		Name: custMS_MonitronicsEntityZipGetByZipCode
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
**		Date: 06/15/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	06/15/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityZipGetByZipCode
(
	@ZipCode VARCHAR(10)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

	SELECT TOP 1
		*
	FROM
		[dbo].[MS_MonitronicsEntityZips] AS MMEZ WITH (NOLOCK)
	WHERE
		(MMEZ.ZipCode = @ZipCode)
		AND (MMEZ.IsActive = 1) AND (MMEZ.IsDeleted = 0);

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityZipGetByZipCode TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityZipGetByZipCode 63112 */