USE [WISE_HumanResource]
GO
/****** Object:  Trigger [dbo].[dbo.trgUniqueGPEmployeeID]    Script Date: 07/19/2008 14:04:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT * FROM dbo.sysobjects WHERE name = 'dbo.trgUniqueGPEmployeeID'
	AND OBJECTPROPERTY(id, 'IsTrigger') = 1)
BEGIN
	PRINT 'DROPPING TRIGGER dbo.trgUniqueGPEmployeeID'
	DROP TRIGGER [dbo].[dbo.trgUniqueGPEmployeeID]
END
GO

PRINT 'CREATING TRIGGER dbo.trgUniqueGPEmployeeID'
GO
/******************************************************************************
**		File: dbo.trgUniqueGPEmployeeID.sql
**		Name: dbo.trgUniqueGPEmployeeID
**		Desc: 
**
**		This trigger will save everytime there is a change in the alarmnet table
**			this is used for looking back at all the serial numbers used.
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés Sosa
**		Date: 04/22/2014
**			
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	04/22/2014	Andrés Sosa		Created by
*******************************************************************************/
CREATE TRIGGER [dbo].[dbo.trgUniqueGPEmployeeID]
   ON  [dbo].[RU_Users]
   FOR INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Locals
	DECLARE @UserID INT
	DECLARE @GPEmployeeID NVARCHAR(25)

	-- Get the new values
	SELECT @UserID = UserID, @GPEmployeeID = GPEmployeeID FROM INSERTED

	-- Get Existing User if there is one
	DECLARE @ExistingUserID INT
	SELECT TOP 1
		@ExistingUserID = UserID
	FROM RU_Users
	WHERE
		(GPEmployeeID = @GPEmployeeID)
		AND (@UserID <> UserID); -- Don't compare against itself

	IF @ExistingUserID IS NOT NULL --Check if the is an existing user with that GPEmployeeID
	BEGIN
		-- Throw Error
		RAISERROR ('TRIGGER ERROR: Column Uniqueness Constraint. GPEmployeeID ''%s'' is already assigned to UserID ''%d''.', 16, 1, @GPEmployeeID, @ExistingUserID);
		ROLLBACK TRANSACTION
	END

END
GO