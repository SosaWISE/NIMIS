USE [WISE_CRM]
GO

-- TF = Table function
-- IF = Inline Table function
IF EXISTS (SELECT * FROM sysobjects WHERE (type = 'TF' OR type = 'IF') AND name = 'fxGetMcAddressByInvoiceId')
	BEGIN
		PRINT 'Dropping FUNCTION fxGetMcAddressByInvoiceId'
		DROP FUNCTION  dbo.fxGetMcAddressByInvoiceId
	END
GO

PRINT 'Creating FUNCTION fxGetMcAddressByInvoiceId'
GO
/******************************************************************************
**		File: fxGetMcAddressByInvoiceId.sql
**		Name: fxGetMcAddressByInvoiceId
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
**		Date: 01/21/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-------------------------------------------
**	01/21/2014	Andrés E. Sosa	Created By
**	
*******************************************************************************/
CREATE FUNCTION dbo.fxGetMcAddressByInvoiceId
(
	@InvoiceID BIGINT
)
RETURNS 
@ParsedList table
(
	AddressID BIGINT
	, StateId VARCHAR(4)
	, StreetAddress NVARCHAR(50)
	, StreetAddress2 NVARCHAR(50)
	, City NVARCHAR(50)
	, PostalCode VARCHAR(5)
)
AS
BEGIN
	IF (EXISTS(	SELECT * FROM
		[dbo].AE_Invoices AS INV WITH (NOLOCK)
		INNER JOIN [dbo].MC_Accounts AS MAC WITH (NOLOCK)
		ON
			(MAC.AccountID = INV.AccountId)
			AND (INV.InvoiceID = @InvoiceID)
		INNER JOIN [dbo].MC_Addresses AS ADR WITH (NOLOCK)
		ON
			(ADR.AddressID = MAC.ShipAddressId)))
	BEGIN
		INSERT INTO @ParsedList (
			AddressID,
			StateId,
			StreetAddress,
			StreetAddress2,
			PostalCode,
			City
		) 
		SELECT
			ADR.AddressID
			, ADR.StateId
			, ADR.StreetAddress
			, ADR.StreetAddress2
			, ADR.PostalCode
			, ADR.City
		FROM
			[dbo].AE_Invoices AS INV WITH (NOLOCK)
			INNER JOIN [dbo].MC_Accounts AS MAC WITH (NOLOCK)
			ON
				(MAC.AccountID = INV.AccountId)
				AND (INV.InvoiceID = @InvoiceID)
			INNER JOIN [dbo].MC_Addresses AS ADR WITH (NOLOCK)
			ON
				(ADR.AddressID = MAC.ShipAddressId);
	END
	ELSE
	BEGIN
		INSERT INTO @ParsedList (
			AddressID ,
			StateId ,
			StreetAddress ,
			PostalCode ,
			City
		) VALUES (
			0
			, 'UT'
			, '1573 N Technology WAY'
		);
	END

	RETURN;
END
GO

/** Test 

SELECT * FROM [dbo].fxGetMcAddressByInvoiceId(10000000);

*/