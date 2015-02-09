USE [WISE_CRM]
GO
/****** Object:  Trigger [dbo].[MS_AccountInstallationStatus_trgHistory]    Script Date: 07/19/2008 14:04:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT * FROM dbo.sysobjects WHERE name = 'trgAE_Customers_CheckUniqueUsername'
	AND OBJECTPROPERTY(id, 'IsTrigger') = 1)
BEGIN
	PRINT 'DROPPING TRIGGER trgAE_Customers_CheckUniqueUsername'
	DROP TRIGGER [dbo].[trgAE_Customers_CheckUniqueUsername]
END
GO

PRINT 'CREATING TRIGGER trgAE_Customers_CheckUniqueUsername'
GO
/******************************************************************************
**		File: MS_AccountInstallationStatus_trgHistory.sql
**		Name: MS_AccountInstallationStatus_trgHistory
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
**		Date: 07/05/2012
**			
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	07/05/2012	Andrés Sosa		Created by
*******************************************************************************/
CREATE TRIGGER [dbo].[trgAE_Customers_CheckUniqueUsername]
   ON  [dbo].[AE_Customers]
   FOR INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	/** Check for unique username. */
	IF (EXISTS(SELECT
			CUST.CustomerID
			, CUST.Username
		FROM
			INSERTED AS CUST WITH(NOLOCK)
			INNER JOIN dbo.AE_Customers AS INST WITH (NOLOCK)
			ON
				(CUST.CustomerID <> INST.CustomerID)
				AND (CUST.Username = INST.Username)
				AND (INST.Username IS NOT NULL)
		))
	BEGIN
		/** Initialized. */
		DECLARE @Username VARCHAR(50);
		SELECT @Username = Username FROM INSERTED;

		RAISERROR (50100, 16, 1, @Username);
		ROLLBACK TRANSACTION;
		RETURN
	END

END