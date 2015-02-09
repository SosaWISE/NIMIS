USE [WISE_SurveyEngine]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SPROC_NAME')
	BEGIN
		PRINT 'Dropping Procedure SPROC_NAME'
		DROP  Procedure  dbo.SPROC_NAME
	END
GO

PRINT 'Creating Procedure SPROC_NAME'
GO
/******************************************************************************
**		File: SPROC_NAME.sql
**		Name: SPROC_NAME
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
**		Date: 00/00/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	00/00/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.SPROC_NAME
(
	@PARAM INT
	, @PARAM2 NVARCHAR(25) OUTPUT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON
	
	BEGIN TRANSACTION
	/** Transfer data */
	
	INSERT INTO XN_CustomersMasterFileCompanyStats
	SELECT
		*
	FROM
		[WISE_CRM].[dbo].XN_Temp
	
	COMMIT TRANSACTION
END
GO

GRANT EXEC ON dbo.SPROC_NAME TO PUBLIC
GO