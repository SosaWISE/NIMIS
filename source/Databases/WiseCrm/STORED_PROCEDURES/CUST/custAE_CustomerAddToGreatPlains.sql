USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerAddToGreatPlains')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerAddToGreatPlains'
		DROP  Procedure  dbo.custAE_CustomerAddToGreatPlains
	END
GO

PRINT 'Creating Procedure custAE_CustomerAddToGreatPlains'
GO
/******************************************************************************
**		File: custAE_CustomerAddToGreatPlains.sql
**		Name: custAE_CustomerAddToGreatPlains
**		Desc: Create the customer in Great Plains after the customer has been
**		created in CRM and the CustomerMasterFileID (CMFID) has been generated.
**		The CMFID is passed as a parameter and the Customer, Address, and Email
**		are inserted in the Great Plains database.
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
**		CMFID							None
**
**		Auth: Bob McFadden
**		Date: 06/30/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	08/19/2014	Bob McFadden	Created
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerAddToGreatPlains (@CMFID bigint )
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
--
		/******************
		***  ADDRESSES  ***
		*******************/
		INSERT DYSNEYDAD.NEX.dbo.RM00102 (
			CUSTNMBR, -- Customer Number
			ADRSCODE, -- Address Code
			SLPRSNID, -- Salesperson ID
			--UPSZONE, -- UPS Zone
			--SHIPMTHD, -- Shipping Method
			--TAXSCHID, -- Tax Schedule ID
			--CNTCPRSN, -- Contact Person
			ADDRESS1, -- Address 1
			ADDRESS2, -- Address 2
			--ADDRESS3, -- Address 3
			COUNTRY, -- Country
			CITY, -- City
			STATE, -- State
			ZIP, -- Zip
			PHONE1, -- Phone 1
			--PHONE2, -- Phone 2
			--PHONE3, -- Phone 3
			--FAX, -- Fax
			MODIFDT, -- Modified Date
			CREATDDT, -- Created Date
			--GPSFOINTEGRATIONID, -- GPSFO Integration ID
			--INTEGRATIONSOURCE, -- Integration Source
			--INTEGRATIONID, -- Integration ID
			CCode, -- Country Code
			--DECLID, -- Declarant ID
			--LOCNCODE, -- Location Code
			SALSTERR, -- Sales Territory
			--USERDEF1, -- User Defined 1
			--USERDEF2, -- User Defined 2
			ShipToName --,
			--Print_Phone_NumberGB
		)
		SELECT
			convert(char,AE_CustomerMasterFiles.CustomerMasterFileId),
			AE_CustomerAddress.CustomerAddressTypeId,
			ISNULL(QL_Leads.SalesRepId,''),
			--'', -- UPS Zone
			--'', -- Shipping Method
			--'', -- Tax Schedule ID
			--'', -- Contact Person
			ISNULL(MC_Addresses.StreetAddress,''),
			ISNULL(MC_Addresses.StreetAddress2,''),
			--'',  -- Address 3 - no address3 in CRM
			ISNULL(MC_PoliticalCountrys.CountryName,''), -- the Great Plains table for COUNTRY is VAT10001
			ISNULL(MC_Addresses.City,''),
			ISNULL(MC_PoliticalStates.StateAB,''),
			ISNULL(MC_Addresses.PostalCode,''),
			ISNULL(MC_Addresses.Phone,''),
			--'', -- Phone 2 - only one phone number exists
			--'', -- Phone 3 - only one phone number exists
			--'', -- Fax - no FAX number in CRM
			CONVERT(DATE, MC_Addresses.ModifiedOn),
			CONVERT(DATE, MC_Addresses.CreatedOn),
			--'', -- GPSFO Integration ID
			--'', -- Integration Source
			--'', -- Integration ID
			ISNULL(MC_Addresses.CountryID,''), --VAT10001.CCode
			--'', -- Declarant ID
			--'', -- Location Code
			ISNULL(RU_TeamLocations.GpSalesTerritoryId,''),
			--'', -- User Defined 1
			--'', -- User Defined 2
			AE_Customers.FirstName + ' ' + AE_Customers.LastName --,
			--'' --Print Phone Number
		FROM 
			-- CUSTOMER MASTER FILE
			dbo.AE_CustomerMasterFiles

			-- CUSTOMERS
			INNER JOIN dbo.AE_Customers
				ON AE_CustomerMasterFiles.CustomerMasterFileID = AE_Customers.CustomerMasterFileId

			-- CUSTOMER ADDRESS
			INNER JOIN dbo.AE_CustomerAddress
				ON AE_Customers.CustomerID = AE_CustomerAddress.CustomerId

			-- ADDRESS
			INNER JOIN dbo.MC_Addresses
				ON AE_CustomerAddress.AddressId = MC_Addresses.AddressID

			-- STATE
			INNER JOIN dbo.MC_PoliticalStates
				ON MC_Addresses.StateId = MC_PoliticalStates.StateID

			-- COUNTRY
			INNER JOIN dbo.MC_PoliticalCountrys
				ON MC_Addresses.CountryId = MC_PoliticalCountrys.CountryID

			-- LEADS - to get the SALESPERSON ID
			INNER JOIN dbo.QL_Leads
				ON AE_CustomerMasterFiles.CustomerMasterFileID = QL_Leads.CustomerMasterFileId

			-- TEAM LOCATIONS
			INNER JOIN dbo.RU_TeamLocations 
				ON QL_Leads.TeamLocationID = RU_TeamLocations.TeamLocationId
		WHERE AE_CustomerMasterFiles.CustomerMasterFileID = @CMFID

		/*****************
		***  CUSTOMER  ***
		******************/
		INSERT DYSNEYDAD.NEX.dbo.RM00101 (
			CUSTNMBR, 
			CUSTNAME, 
			CUSTCLAS, 
			--CPRCSTNM, 
			--CNTCPRSN, 
			STMTNAME, 
			SHRTNAME, 
			ADRSCODE, 
			--UPSZONE, 
			--SHIPMTHD, 
			--TAXSCHID, 
			ADDRESS1, 
			ADDRESS2, 
			--ADDRESS3, 
			COUNTRY, 
			CITY, 
			STATE, 
			ZIP, 
			PHONE1, 
			--PHONE2, 
			--PHONE3, 
			--FAX, 
			PRBTADCD, 
			PRSTADCD, 
			STADDRCD, 
			SLPRSNID, 
			--CHEKBKID, 
			--PYMTRMID, 
			--CRLMTTYP, 
			--CRLMTAMT, 
			--CRLMTPER, 
			--CRLMTPAM, 
			--CURNCYID, 
			--RATETPID, 
			--CUSTDISC, 
			--PRCLEVEL, 
			--MINPYTYP, 
			--MINPYDLR, 
			--MINPYPCT, 
			--FNCHATYP, 
			--FNCHPCNT, 
			--FINCHDLR, 
			--MXWOFTYP, 
			--MXWROFAM, 
			--COMMENT1, 
			--COMMENT2, 
			--USERDEF1, 
			--USERDEF2, 
			--TAXEXMT1, 
			--TAXEXMT2, 
			--TXRGNNUM, 
			--BALNCTYP, 
			--STMTCYCL, 
			--BANKNAME, 
			--BNKBRNCH, 
			SALSTERR, 
			--DEFCACTY, 
			--RMCSHACC, 
			--RMARACC, 
			--RMSLSACC, 
			--RMIVACC, 
			--RMCOSACC, 
			--RMTAKACC, 
			--RMAVACC, 
			--RMFCGACC, 
			--RMWRACC, 
			--RMSORACC, 
			--FRSTINDT, 
			INACTIVE, 
			HOLD --, 
			--CRCARDID, 
			--CRCRDNUM, 
			--CCRDXPDT, 
			--KPDSTHST, 
			--KPCALHST, 
			--KPERHIST, 
			--KPTRXHST, 
			--NOTEINDX, 
			--CREATDDT, 
			--MODIFDT
			--, 
			--Revalue_Customer, 
			--Post_Results_To, 
			--FINCHID, 
			--GOVCRPID, 
			--GOVINDID, 
			--DISGRPER, 
			--DUEGRPER, 
			--DOCFMTID, 
			--Send_Email_Statements, 
			--USERLANG, 
			--GPSFOINTEGRATIONID, 
			--INTEGRATIONSOURCE, 
			--INTEGRATIONID, 
			--ORDERFULFILLDEFAULT, 
			--CUSTPRIORITY, 
			--CCode, 
			--DECLID, 
			--RMOvrpymtWrtoffAcctIdx, 
			--SHIPCOMPLETE, 
			--CBVAT, 
			--INCLUDEINDP
			)
		SELECT
			convert(char,AE_CustomerMasterFiles.CustomerMasterFileId) as 'Customer Number',
			AE_Customers.FirstName + ' ' + AE_Customers.LastName as 'Customer Name',
			'DEFAULT' as 'Customer Class',
			--'' as 'Corporate Customer Number',
			--'' as 'Contact Person',
			AE_Customers.FirstName + ' ' + AE_Customers.LastName as 'Statement Name',
			AE_Customers.FirstName + ' ' + AE_Customers.LastName as 'Short Name',
			ISNULL(AE_CustomerAddress.CustomerAddressTypeId,'') AS 'Address Code',
			--'' as 'UPS Zone',
			--'' as 'Ship Method',
			--'' as 'Tax Schedule ID',
			ISNULL(MC_Addresses.StreetAddress,'') as 'Address 1',
			ISNULL(MC_Addresses.StreetAddress2,'') as 'Address 2',
			--'' as 'Address 3',  -- Address 3 - no address3 in CRM
			ISNULL(MC_PoliticalCountrys.CountryName,'') as 'Country', -- the Great Plains table for COUNTRY is VAT10001
			ISNULL(MC_Addresses.City,'') as 'City',
			ISNULL(MC_PoliticalStates.StateAB,'') as 'State',
			ISNULL(MC_Addresses.PostalCode,'') as 'Zip',
			ISNULL(MC_Addresses.Phone,'') as 'Phone',
			--'' as 'Phone 2', -- Phone 2 - only one phone number exists
			--'' as 'Phone 3', -- Phone 3 - only one phone number exists
			--'' as 'Fax', -- Fax - no FAX number in CRM
			ISNULL(AE_CustomerAddress.CustomerAddressTypeId,'') as 'Primary Billto Address Code',
			ISNULL(AE_CustomerAddress.CustomerAddressTypeId,'') as 'Primary Shipto Address Code',
			ISNULL(AE_CustomerAddress.CustomerAddressTypeId,'') as 'Statement Address Code',
			ISNULL(QL_Leads.SalesRepId,'') as 'Salesperson ID',
			--'' as 'Checkbook ID',
			--'' as 'Payment Terms ID',
			--0 as 'Credit Limit Type',
			--0 as 'Credit Limit Amount',
			--0 as 'Credit Limit Period',
			--0 as 'Credit Limit Period Amount',
			--'' as 'Currency ID',
			--'' as 'Rate Type ID',
			--0 as 'Customer Discount',
			--'' as 'PriceLevel',
			--0 as 'Minimum Payment Type',
			--0 as 'Minimum Payment Dollar',
			--0 as 'Minimum Payment Percent',
			--0 as 'Finance Charge Amt Type',
			--0 as 'Finance Charge Percent',
			--0 as 'Finance Charge Dollar',
			--0 as 'Maximum Writeoff Type',
			--0 as 'Max Writeoff Amount',
			--'' as 'Comment1',
			--'' as 'Comment2',
			--'' as 'User Defined 1',
			--'' as 'User Defined 2',
			--'' as 'Tax Exempt 1',
			--'' as 'Tax Exempt 2',
			--'' as 'Tax Registration Number',
			--0 as 'Balance Type',
			--0 as 'Statement Cycle',
			--'' as 'Bank Name',
			--'' as 'Bank Branch',
			ISNULL(RU_TeamLocations.GpSalesTerritoryId,'') as 'Sales Territory',
			--0 as 'Default Cash Account Type',
			--0 as 'RM Cash Account Index',
			--0 as 'RM AR Account Index',
			--0 as 'RM Sales Account Index',
			--0 as 'RM IV Account Index',
			--0 as 'RM Cost Of Sales Account Index',
			--0 as 'RM Discounts Taken Account Index',
			--0 as 'RM Discounts Avail Account Index',
			--0 as 'RM Finance Charge Account Index',
			--0 as 'RM Writeoff Account Index',
			--0 as 'RM Sales Order Returns Account Index',
			--GETDATE() as 'First Invoice Date',
			0 as 'Inactive',
			0 as 'Hold' --,
			--'' as 'Credit Card ID',
			--'' as 'Credit Card Number',
			--GETDATE() as 'Credit Card Exp Date',
			--0 as 'Keep Distribution History',
			--0 as 'Keep Calendar History',
			--0 as 'Keep Period History',
			--0 as 'Keep Trx History',
			--0 as 'Note Index',
			--AE_Customers.CreatedOn as 'Created Date',
			--AE_Customers.ModifiedOn as 'Modified Date',
			--0 as 'Revalue Customer',
			--0 as 'Post Results To',
			--'' as 'Finance Charge ID',
			--'' as 'Governmental Corporate ID',
			--'' as 'Governmental Individual ID',
			--0 as 'Discount Grace Period',
			--0 as 'Due Date Grace Period',
			--'' as 'Document Format ID',
			--0 as 'Send Email Statements',
			--0 as 'User Language ID',
			--'' as 'GPSFO Integration ID',
			--0 as 'Integration Source',
			--'' as 'Integration ID',
			--0 as 'Order Fulfillment Shortage Default',
			--0 as 'Customer Priority',
			--'' as 'Country Code',
			--'' as 'Declarant ID',
			--0 as 'RM Overpayment Writeoff Account Index',
			--0 as 'Ship Complete Document',
			--0 as 'Cash Based VAT',
			--0 as 'Include in Demand Planning'
		FROM 
			-- CUSTOMER MASTER FILE
			dbo.AE_CustomerMasterFiles

			-- CUSTOMERS
			INNER JOIN dbo.AE_Customers
				ON AE_CustomerMasterFiles.CustomerMasterFileID = AE_Customers.CustomerMasterFileId

			-- CUSTOMER ADDRESS
			INNER JOIN dbo.AE_CustomerAddress
				ON AE_Customers.CustomerID = AE_CustomerAddress.CustomerId
				and AE_Customers.AddressId = AE_CustomerAddress.AddressId

			-- ADDRESS
			INNER JOIN dbo.MC_Addresses
				ON AE_Customers.AddressId = MC_Addresses.AddressID

			-- STATE
			INNER JOIN dbo.MC_PoliticalStates
				ON MC_Addresses.StateId = MC_PoliticalStates.StateID

			-- COUNTRY
			INNER JOIN dbo.MC_PoliticalCountrys
				ON MC_Addresses.CountryId = MC_PoliticalCountrys.CountryID

			-- LEADS - to get the SALESPERSON ID
			INNER JOIN dbo.QL_Leads
				ON AE_CustomerMasterFiles.CustomerMasterFileID = QL_Leads.CustomerMasterFileId

			-- TEAM LOCATIONS (SALES TERRITORY)
			INNER JOIN dbo.RU_TeamLocations 
				ON QL_Leads.TeamLocationID = RU_TeamLocations.TeamLocationId
		WHERE AE_CustomerMasterFiles.CustomerMasterFileID = @CMFID

		/**************
		***  EMAIL  ***
		***************/
		INSERT DYSNEYDAD.NEX.dbo.RM00106 (
			CUSTNMBR,
			EMAIL_TYPE,
			EMAIL_RECIPIENT
			)
		SELECT
			convert(char,AE_CustomerMasterFiles.CustomerMasterFileId) as 'Customer Number',
			1,
			AE_Customers.Email
		FROM 
			-- CUSTOMER MASTER FILE
			dbo.AE_CustomerMasterFiles

			-- CUSTOMERS
			INNER JOIN dbo.AE_Customers
				ON AE_CustomerMasterFiles.CustomerMasterFileID = AE_Customers.CustomerMasterFileId
		WHERE 
			AE_CustomerMasterFiles.CustomerMasterFileID = @CMFID
			AND AE_Customers.Email IS NOT NULL
--
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerAddToGreatPlains TO PUBLIC
GO

/** EXEC dbo.custAE_CustomerAddToGreatPlains */