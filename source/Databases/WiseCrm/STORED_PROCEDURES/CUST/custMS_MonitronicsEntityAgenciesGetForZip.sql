USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntityAgenciesGetForZip')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntityAgenciesGetForZip'
		DROP  Procedure  dbo.custMS_MonitronicsEntityAgenciesGetForZip
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntityAgenciesGetForZip'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntityAgenciesGetForZip.sql
**		Name: custMS_MonitronicsEntityAgenciesGetForZip
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
**		Auth: Jake Jenne
**		Date: 12/4/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	12/4/2014	Jake Jenne		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntityAgenciesGetForZip
(
	@ZipCode VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	-- Return rows
	SELECT * FROM [dbo].[MS_MonitronicsEntityAgencies] WHERE (ZipCode = @ZipCode);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntityAgenciesGetForZip TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntityAgenciesGetForZip */