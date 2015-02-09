USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ppCustCreateContract')
	BEGIN
		PRINT 'Dropping Procedure ppCustCreateContract'
		DROP  Procedure  dbo.ppCustCreateContract
	END
GO

PRINT 'Creating Procedure ppCustCreateContract'
GO
/******************************************************************************
**		File: ppCustCreateContract.sql
**		Name: ppCustCreateContract
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
**		Auth: Todd Carlson
**		Date: 12/16/2008
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	12/16/2008	Todd Carlson	Created
*******************************************************************************/
CREATE Procedure dbo.ppCustCreateContract
(
	@I_vCustomerNumber CHAR(15)
	, @I_vContractLength SMALLINT
	, @I_vContractPeriod SMALLINT
	, @I_vTotalContractValue MONEY
	, @I_vPriceSchedule CHAR(11)
	, @I_vSalesRepID CHAR(15)
	, @I_vAddressCode CHAR(15)
	, @I_vCountry CHAR(61) = ''
	, @I_vTimeZone CHAR(3)
	, @I_vTaxCode CHAR(15)
	, @I_vLocationCode CHAR(67)
	, @I_vBillingDay SMALLINT
	, @I_vBillPeriod SMALLINT
	, @I_vBillStartDate DATETIME
	, @I_vBillEndDate DATETIME
	, @I_vBillingCycle SMALLINT
	, @I_vContractType CHAR(11)
	, @I_vDescription CHAR(31)
	, @I_vServiceType CHAR(11)
	, @I_vInstallDate DATETIME
	, @I_vInstallTime DATETIME
	, @I_vCreatedByID CHAR(15)
	, @I_vCreatedDate DATETIME
	, @I_vCreatedTime DATETIME
	, @I_vIsOnHold BIT = 0
	
	, @I_vItemSku CHAR(31)
	, @I_vQuantity NUMERIC(19,5)
	, @I_vItemDescription CHAR(31)
	
	, @O_iErrorState INT OUTPUT				-- Return value: 0 = No Errors, Any Errors > 0
	, @oErrString VARCHAR(255) OUTPUT		-- Return Error Message
)
AS
BEGIN

	DECLARE @ContractNumber CHAR(11)
	DECLARE @NoteIndex NUMERIC(19,5)

	-- Make sure pricing is set up for the contract
	EXECUTE ppCustEnsureContractPricingSetup @I_vPriceSchedule, @I_vItemSku

	-- Get the next contract number
	EXECUTE SVC_New_Contract_Number @ContractNumber OUTPUT
	
	-- Get the next Note Index
	EXECUTE SVC_Get_Next_Note_Index @NoteIndex OUTPUT
	
	-- Create the contract record
	INSERT INTO
		SVC00600 -- CONTRACT HEADER TABLE
		(
			CONTNBR
			, NOTEINDX
			, CUSTNMBR
			, Bill_To_Customer
			, ADRSCODE
			, SVC_Bill_To_Address_Code
			, Amount_To_Invoice
			, TOTAL
			, ORIGTOTAL
			, Orig_Amount_To_Invoice
			, BILLNGTH
			, NUMOFINV
			, Contract_Length
			, BILPRD
			, Contract_Period
			, BILONDY
			, TAXSCHID
			, TIMEZONE
			, SLPRSNID
			, BILSTRT
			, BILEND
			, BILCYC
			, CNTTYPE
			, DSCRIPTN
			, SRVTYPE
			, COUNTRY
			, ENTDTE
			, ENTTME
			, EXCHDATE
			, Location_Segment
			, PRICSHED
			, STRTDATE
			, ENDDATE
			, Credit_Hold
			, Created_User_ID
			, CONSTS
			, PREPAID			
			, RENCNTTYP
			, TIME1
			, SVC_Liability_Type
			, SVC_Invoice_Detail
			, SmoothInvoiceCalc
		)
	VALUES
		(
			@ContractNumber
			, @NoteIndex
			, @I_vCustomerNumber
			, @I_vCustomerNumber
			, @I_vAddressCode
			, @I_vAddressCode
			, @I_vTotalContractValue
			, @I_vTotalContractValue
			, @I_vTotalContractValue
			, @I_vTotalContractValue
			, @I_vContractLength
			, @I_vContractLength
			, @I_vContractLength
			, @I_vBillPeriod
			, @I_vContractPeriod
			, @I_vBillingDay
			, @I_vTaxCode
			, @I_vTimeZone
			, @I_vSalesRepID
			, @I_vBillStartDate
			, @I_vBillEndDate
			, @I_vBillingCycle
			, @I_vContractType
			, @I_vDescription
			, @I_vServiceType
			, @I_vCountry
			, @I_vInstallDate
			, @I_vInstallTime
			, @I_vCreatedDate
			, @I_vLocationCode
			, @I_vPriceSchedule
			, @I_vInstallDate
			, @I_vBillEndDate
			, @I_vIsOnHold
			, @I_vCreatedByID
			, 2 -- CONTSTS
			, 1 -- PREPAID
			, 2 -- RENCNTTYP
			, @I_vCreatedTime -- TIME1
			, 1 -- SVC_Liability_Type
			, 1 -- SVC_Invoice_Detail
			, 1 -- SmoothInvoiceCalc
		)
	
	-- Create the contract line item
	INSERT INTO
		SVC00601 -- CONTRACT LINES
		(
			CONTNBR
			, NOTEINDX
			, CUSTNMBR
			, Bill_To_Customer
			, ADRSCODE
			, SVC_Bill_To_Address_Code
			, Amount_To_Invoice
			, TOTAL
			, ORIGTOTAL
			, Orig_Amount_To_Invoice
			, Total_Unit
			, NUMOFINV
			, ITEMNMBR
			, EQUIPID
			, QUANTITY
			, TAXSCHID
			, SLPRSNID
			, CNTTYPE
			, DSCRIPTN
			, SRVTYPE
			, COUNTRY
			, ENTDTE
			, ENTTME
			, EXCHDATE
			, Location_Segment
			, PRICSHED
			, SVC_Monthly_Price 
			, FRSTBLDTE
			, BILSTRT
			, BILEND
			, BILLNGTH
			, BILPRD
			, BILONDY
			, STRTDATE
			, ENDDATE
			, CONSTS
			, PREPAID
			, LNSEQNBR
			, BILCYC
			, CNTPRCOVR
			, UOFM
			, Contract_Line_Status
			, SVC_Liability_Type
		)
	VALUES
		(
			@ContractNumber
			, 0 -- NOTEINDX
			, @I_vCustomerNumber
			, @I_vCustomerNumber
			, @I_vAddressCode
			, @I_vAddressCode
			, @I_vTotalContractValue
			, @I_vTotalContractValue
			, @I_vTotalContractValue
			, @I_vTotalContractValue
			, @I_vTotalContractValue
			, @I_vContractLength
			, @I_vItemSku
			, @I_vCustomerNumber
			, @I_vQuantity
			, @I_vTaxCode
			, @I_vSalesRepID
			, @I_vContractType
			, @I_vItemDescription
			, @I_vServiceType
			, @I_vCountry
			, @I_vInstallDate
			, @I_vInstallTime
			, @I_vCreatedDate
			, @I_vLocationCode
			, @I_vPriceSchedule
			, @I_vPriceSchedule
			, @I_vInstallDate
			, @I_vInstallDate
			, @I_vBillEndDate
			, @I_vContractLength
			, @I_vBillPeriod
			, @I_vBillingDay
			, @I_vInstallDate
			, @I_vBillEndDate
			, 2 -- CONTSTS
			, 1 -- PREPAID
			, 100.0 -- LNSEQNBR
			, 1 -- BILCYC
			, 0 -- CNTPRCOVR
			, 'EA' -- UOFM
			, 'N' -- Contract_Line_Status
			, 1 -- SVC_Liability_Type
		)

	-- Create the Invoices for the contract line
	--EXEC SVC_Set_Contract_Line_Invoice 2, @ContractNumber, 100.0
		
END
GO

GRANT EXEC ON dbo.ppCustCreateContract TO PUBLIC
GO