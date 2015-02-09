USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF' OR type = 'FN') AND name = 'fxGetCustomerSWINGAccountStatus')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetCustomerSWINGAccountStatus'
		DROP FUNCTION  dbo.fxGetInterimCustomerAccountStatus
	END
GO

PRINT 'Creating FUNCTION fxGetCustomerSWINGAccountStatus'
GO
/******************************************************************************
**		File: fxGetInterimCustomerAccountStatus.sql
**		Name: fxGetInterimCustomerAccountStatus
**		Desc: this function will be used to Identify if the AccountID is for a full customer or a Lead.
**
**		This template can be customized:
**              
**		Return values: Table of IDs/Ints
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**     InterimAccountId                 Onboard Full / Onboard Lead
**
**		Auth: Andrés E. Sosa
**		Date: 01/27/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	04/16/2014	Reagan Descartin	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetCustomerSWINGAccountStatus
(
	@InterimAccountId BIGINT
)
RETURNS VARCHAR(30)
AS
BEGIN
	
	DECLARE @PassedCredit VARCHAR(3) , @FullCustomer VARCHAR(3), @Status VARCHAR(30)
	
	SELECT 
		 @PassedCredit = ( 
			CASE
				WHEN MASS.IsQualifyCustomerComplete = 1 THEN 'Yes'
				ELSE 'No'
			END 
		)
		, 
		@FullCustomer =  (
			CASE
				WHEN MASS.IsAccountSetupComplete = 1 THEN 'Yes'
				ELSE 'No'
			END
		)
		FROM
		[Platinum_Protection_InterimCRM].[dbo].[MS_Account] AS MAS WITH (NOLOCK)
		LEFT OUTER JOIN 	[Platinum_Protection_InterimCRM].[dbo].[MS_AccountSetupStatus] AS MASS WITH (NOLOCK)
		ON
		(MAS.AccountID = MASS.AccountID)
	WHERE
	MAS.AccountID = @InterimAccountId
		
	-- check if Onboard Full
	IF(@PassedCredit = 'Yes' AND @FullCustomer='Yes')
	BEGIN
		SET @Status='Onboard Full'
	END
	ELSE
	BEGIN
		SET @Status='Onboard Lead'
	END
		
	RETURN @Status
END
GO



/*
	SUPPLEMENTAL QUERIES
	
	SELECT distinct(IsQualifyCustomerComplete) FROM 	Platinum_Protection_InterimCRM.[dbo].[MS_AccountSetupStatus]

	print dbo.fxGetCustomerSWINGAccountStatus(100030)

*/


