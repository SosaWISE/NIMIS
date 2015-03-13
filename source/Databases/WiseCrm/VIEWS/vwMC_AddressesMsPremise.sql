USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwMC_AddressesMsPremise')
	BEGIN
		PRINT 'Dropping VIEW vwMC_AddressesMsPremise'
		DROP VIEW dbo.vwMC_AddressesMsPremise
	END
GO

PRINT 'Creating VIEW vwMC_AddressesMsPremise'
GO

/****** Object:  View [dbo].[vwMC_AddressesMsPremise]    Script Date: 01/10/2011 15:18:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************
**		File: vwMC_AddressesMsPremise.sql
**		Name: vwMC_AddressesMsPremise
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
**		Auth: Andres Sosa
**		Date: 10/14/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	10/14/2014	Andres Sosa		Created by
*******************************************************************************/
CREATE VIEW [dbo].[vwMC_AddressesMsPremise]
AS
	-- Enter Query here
	SELECT
		MAC.AccountId
		, MAC.CustomerTypeId
		, ACA.CustomerAddressTypeId
		, MCA.*
	FROM
		(SELECT
			MACI.*
			, ROW_NUMBER() OVER (PARTITION BY MACI.AccountId ORDER BY MACI.CustomerTypeId) AS RNBR
		FROM
			[dbo].AE_CustomerAccounts AS MACI WITH (NOLOCK)
		WHERE
			(MACI.CustomerTypeId = 'MONI')
			OR (MACI.CustomerTypeId = 'PRI')) AS MAC 
		INNER JOIN [dbo].AE_CustomerAddress AS ACA WITH (NOLOCK)
		ON
			(ACA.CustomerId = MAC.CustomerId)
			AND (MAC.RNBR = 1)
			AND (ACA.CustomerAddressTypeId = 'PRI')
		INNER JOIN [dbo].MC_Addresses AS MCA WITH (NOLOCK)
		ON
			(MCA.AddressID = ACA.AddressId)
GO
/* TEST 
SELECT * FROM vwMC_AddressesMsPremise WHERE AccountID = 150927
BEGIN TRANSACTION

UPDATE dbo.MS_Accounts SET
	PremiseAddressId = AMP.AddressID
--SELECT 
--	AMP.* 
FROM 
	dbo.MS_Accounts AS MSA WITH (NOLOCK)
	INNER JOIN vwMC_AddressesMsPremise AS AMP WITH (NOLOCK)
	ON
		(MSA.AccountID = AMP.AccountID)
WHERE
	(PremiseAddressId IS NULL)

ROLLBACK TRANSACTION

*/