USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxGetCustomerMasterIcons')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetCustomerMasterIcons'
		DROP FUNCTION  dbo.fxGetCustomerMasterIcons
	END
GO

PRINT 'Creating FUNCTION fxGetCustomerMasterIcons'
GO
/******************************************************************************
**		File: fxGetCustomerMasterIcons.sql
**		Name: fxGetCustomerMasterIcons
**		Desc: 
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
**
**		Auth: Andrés E. Sosa
**		Date: 03/17/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	03/17/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetCustomerMasterIcons
(
)
RETURNS 
@IconsList table
(
	CustomerMasterFileID BIGINT
	, ICONS VARCHAR(100)
)
AS
BEGIN
	INSERT INTO @IconsList ( CustomerMasterFileID, ICONS )
		SELECT
			-- CustomerMasterFileID, 'ALRM' AS [ALRM], 'INSEC' AS [INSEC]
			PVT.CustomerMasterFileID
			, CASE 
				WHEN PVT.ALRM > 0 THEN ';ALRM' -- Alarm System
				ELSE ''
			  END +
			  CASE
				WHEN PVT.INSEC > 0 THEN ';INSEC'  -- Internet Security
				ELSE ''
			  END +
			  CASE
				WHEN PVT.LFLCK > 0 THEN ';LFLCK'  -- Life Lock
				ELSE ''
			  END +
			  CASE
				WHEN PVT.NUMAN > 0 THEN ';NUMAN'  -- Nu Mana
				ELSE ''
			  END +
			  CASE
				WHEN PVT.PERS > 0 THEN ';PERS'  -- Nu Mana
				ELSE ''
			  END +
			  CASE
				WHEN PVT.SKPLT > 0 THEN ';SKPLT'  -- Strike Plate
				ELSE ''
			  END +
			  CASE
				WHEN PVT.WNFIL > 0 THEN ';WNFIL'  -- Window Film
				ELSE ''
			  END
			 AS [ICONS] 
			-- , PVT.*
		FROM
		(SELECT
			MCA.AccountID
			, MCA.AccountTypeId
			, CMF.CustomerMasterFileID
		FROM
			[dbo].[AE_CustomerMasterFiles] AS CMF WITH (NOLOCK)
			INNER JOIN [dbo].[AE_Customers] AS CST WITH (NOLOCK)
			ON
				(CST.CustomerMasterFileId = CMF.CustomerMasterFileID)
			INNER JOIN [dbo].[AE_CustomerAccounts] AS ACA WITH (NOLOCK)
			ON
				(ACA.CustomerId = CST.CustomerID)
			INNER JOIN [dbo].[MC_Accounts] AS MCA WITH (NOLOCK)
			ON
				(MCA.AccountID = ACA.AccountId)) AS MST
		PIVOT
		(
			COUNT(AccountID)
			FOR AccountTypeId IN ([ALRM], [INSEC], [LFLCK], [NUMAN], [PERS], [SKPLT], [WNFIL]) 
		) AS PVT
		ORDER BY
			PVT.CustomerMasterFileID;
		RETURN
END
GO