USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerUpdateInGreatPlains')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerUpdateInGreatPlains'
		DROP  Procedure  dbo.custAE_CustomerUpdateInGreatPlains
	END
GO

PRINT 'Creating Procedure custAE_CustomerUpdateInGreatPlains'
GO
/******************************************************************************
**		File: custAE_CustomerUpdateInGreatPlains.sql
**		Name: custAE_CustomerUpdateInGreatPlains
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
**		CMFID							None - Updates to Great Plains tables
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
CREATE Procedure dbo.custAE_CustomerUpdateInGreatPlains (@CMFID bigint)
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
		-- Update the customer address table on Great Plains if the modified date on any of the customer address tables in CRM 
		-- is newer than the modified date on the customer address table.
		UPDATE RM00102
		SET 
--			CUSTNMBR = convert(char,AE_CustomerMasterFiles.CustomerMasterFileId), -- Customer Number
			ADRSCODE = AE_CustomerAddress.CustomerAddressTypeId, -- Address Code
			SLPRSNID = ISNULL(QL_Leads.SalesRepId,''), -- Salesperson ID
			--UPSZONE, -- UPS Zone
			--SHIPMTHD, -- Shipping Method
			--TAXSCHID, -- Tax Schedule ID
			--CNTCPRSN, -- Contact Person
			ADDRESS1 = ISNULL(MC_Addresses.StreetAddress,''), -- Address 1
			ADDRESS2 = ISNULL(MC_Addresses.StreetAddress2,''), -- Address 2
			--ADDRESS3, -- Address 3
			COUNTRY = ISNULL(MC_PoliticalCountrys.CountryName,''), -- Country
			CITY = ISNULL(MC_Addresses.City,''), -- City
			STATE = ISNULL(MC_PoliticalStates.StateAB,''), -- State
			ZIP = ISNULL(MC_Addresses.PostalCode,''), -- Zip
			PHONE1 = ISNULL(MC_Addresses.Phone,''), -- Phone 1
			--PHONE2, -- Phone 2
			--PHONE3, -- Phone 3
			--FAX, -- Fax
			MODIFDT = CONVERT(DATE, GETDATE()), -- Modified Date
			--CREATDDT, -- Created Date
			--GPSFOINTEGRATIONID, -- GPSFO Integration ID
			--INTEGRATIONSOURCE, -- Integration Source
			--INTEGRATIONID, -- Integration ID
			CCode = ISNULL(MC_Addresses.CountryID,''), -- Country Code
			--DECLID, -- Declarant ID
			--LOCNCODE, -- Location Code
			SALSTERR = ISNULL(RU_TeamLocations.GpSalesTerritoryId,''), -- Sales Territory
			--USERDEF1, -- User Defined 1
			--USERDEF2, -- User Defined 2
			ShipToName = AE_Customers.FirstName + ' ' + AE_Customers.LastName -- Ship to name
			--Print_Phone_NumberGB
		FROM 
			-- RM00102
			DYSNEYDAD.NEX.dbo.RM00102 AS RM00102

			-- CUSTOMER MASTER FILE
			JOIN dbo.AE_CustomerMasterFiles
				ON RM00102.CUSTNMBR = convert(char,AE_CustomerMasterFiles.CustomerMasterFileId)

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
		WHERE 
			AE_CustomerMasterFiles.CustomerMasterFileID = @CMFID
			AND 
			(
			(CONVERT(DATE,AE_Customers.ModifiedOn) > RM00102.MODIFDT)
			OR
			(CONVERT(DATE,AE_CustomerAddress.ModifiedOn) > RM00102.MODIFDT)
			OR
			(CONVERT(DATE,MC_Addresses.ModifiedOn) > RM00102.MODIFDT)
			OR
			(CONVERT(DATE,RU_TeamLocations.ModifiedOn) > RM00102.MODIFDT)
			)

		/*****************
		***  CUSTOMER  ***
		******************/
		-- Update the customer table on Great Plains if the modified date on any of the customer or address tables in CRM 
		-- is newer than the modified date on the customer table.
		UPDATE RM00101
		SET 
			--CUSTNMBR = convert(char,AE_CustomerMasterFiles.CustomerMasterFileId), 
			CUSTNAME = AE_Customers.FirstName + ' ' + AE_Customers.LastName, 
			--CUSTCLAS -- Corporate Customer Number not changed in CRM 
			--CPRCSTNM -- Corporate Customer Number 
			--CNTCPRSN -- Contact Person
			STMTNAME = AE_Customers.FirstName + ' ' + AE_Customers.LastName, --Statement Name
			SHRTNAME = AE_Customers.FirstName + ' ' + AE_Customers.LastName, --Short Name
			ADRSCODE = ISNULL(AE_CustomerAddress.CustomerAddressTypeId,''),  --Address Code
			--UPSZONE, --UPS Zone 
			--SHIPMTHD, --Ship Method
			--TAXSCHID, --Tax ScheduleID
			ADDRESS1 = ISNULL(MC_Addresses.StreetAddress,''), 
			ADDRESS2 = ISNULL(MC_Addresses.StreetAddress2,''), 
			--ADDRESS3, 
			COUNTRY = ISNULL(MC_PoliticalCountrys.CountryName,''), 
			CITY = ISNULL(MC_Addresses.City,''), 
			STATE = ISNULL(MC_PoliticalStates.StateAB,''), 
			ZIP = ISNULL(MC_Addresses.PostalCode,''), 
			PHONE1 = ISNULL(MC_Addresses.Phone,''), 
			--PHONE2, 
			--PHONE3, 
			--FAX, 
			PRBTADCD = ISNULL(AE_CustomerAddress.CustomerAddressTypeId,''), -- Primary Billto Address Code 
			PRSTADCD = ISNULL(AE_CustomerAddress.CustomerAddressTypeId,''), -- Primary Shipto Address Code 
			STADDRCD = ISNULL(AE_CustomerAddress.CustomerAddressTypeId,''), -- Statement Address Code
			SLPRSNID = ISNULL(QL_Leads.SalesRepId,''), -- Salesperson ID
			--CHEKBKID,  -- Checkbook ID
			--PYMTRMID,  -- Payment Terms ID
			--CRLMTTYP,  -- Credit Limit Type
			--CRLMTAMT,  -- Credit Limit Amount
			--CRLMTPER,  -- Credit Limit Period
			--CRLMTPAM,  -- Credit Limit Period Amount 
			--CURNCYID,	 -- Currency ID
			--RATETPID,  -- Rate Type ID
			--CUSTDISC,  -- Customer Discount
			--PRCLEVEL,  -- Price Level
			--MINPYTYP,  -- Minimum Payment Type
			--MINPYDLR,  -- Minimum Payment Dollar
			--MINPYPCT,  -- Minimum Payment Percent
			--FNCHATYP,  -- Finance Charge Amount Type
			--FNCHPCNT,  -- Finance Charge Percent
			--FINCHDLR,  -- Finance Charge Dollar
			--MXWOFTYP,  -- Max Writeoff Type
			--MXWROFAM,  -- Max Writeoff Amount
			--COMMENT1,  
			--COMMENT2, 
			--USERDEF1, -- User Defined Field 1
			--USERDEF2, -- User Defined Field 2
			--TAXEXMT1, -- Tax Exempt 1
			--TAXEXMT2, -- Tax Exempt 2
			--TXRGNNUM, -- Tax Registration Number
			--BALNCTYP, -- Balance Type
			--STMTCYCL, -- Statement Cycle
			--BANKNAME, -- Bank Name
			--BNKBRNCH, -- Bank Branch
			SALSTERR = ISNULL(RU_TeamLocations.GpSalesTerritoryId,''), -- Sales Territory 
			--DEFCACTY, -- Default Cash Account Type
			--RMCSHACC, -- RM Cash Account Index
			--RMARACC,  -- RM AR Account Index
			--RMSLSACC, -- RM Sales Account Index
			--RMIVACC,  -- RM IV Account Index
			--RMCOSACC, -- RM Cost Of Sales Account Index
			--RMTAKACC, -- RM Discounts Taken Account Index
			--RMAVACC,  -- RM Discounts Avail Account Index
			--RMFCGACC, -- RM Finance Charge Account Index
			--RMWRACC,  -- RM Writeoff Account Index
			--RMSORACC, -- RM Sales Order Returns Account Index
			--FRSTINDT, -- First Invoice Date
			INACTIVE =
				CASE 
					WHEN AE_CustomerMasterFiles.IsActive = '1'  -- IsActive flag is set
						THEN '0' -- set Inactive flag to 0
					ELSE '1' -- Set to Inactive if IsActive is not set
				END, 
			-- HOLD -- Account on hold is not changed in CRM 
			--CRCARDID, -- Credit Card ID
			--CRCRDNUM, -- Credit Card Number
			--CCRDXPDT,  -- Credit Card Exp Date
			--KPDSTHST,  -- Keep Distribution History
			--KPCALHST,  -- Keep Calendar History
			--KPERHIST,  -- Keep Period History
			--KPTRXHST,  -- Keep Trx History
			--NOTEINDX,  -- Note Index
			--CREATDDT,  -- Created Date
			MODIFDT = CONVERT(DATE,GETDATE()) -- Modified Date
			--Revalue_Customer, -- Revalue Customer
			--Post_Results_To, -- Post Results To
			--FINCHID, -- Finance Charge ID
			--GOVCRPID, -- Governmental Corporate ID
			--GOVINDID, -- Governmental Individual ID
			--DISGRPER, -- Discount Grace Period
			--DUEGRPER, -- Discount Grace Period
			--DOCFMTID, -- Document Format ID
			--Send_Email_Statements, -- Send Email Statements
			--USERLANG, -- User Language ID
			--GPSFOINTEGRATIONID, -- GPSFO Integration ID
			--INTEGRATIONSOURCE, -- Integration Source
			--INTEGRATIONID, -- Integration ID
			--ORDERFULFILLDEFAULT, -- Order Fulfillment Shortage Default
			--CUSTPRIORITY, -- Customer Priority'
			--CCode, -- Country Code
			--DECLID, -- Declarant ID
			--RMOvrpymtWrtoffAcctIdx, -- RM Overpayment Writeoff Account Index
			--SHIPCOMPLETE, -- Ship Complete Document 
			--CBVAT, -- Cash Based VAT
			--INCLUDEINDP -- Include in Demand Planning
		FROM 
			-- RM00101
			DYSNEYDAD.NEX.dbo.RM00101 AS RM00101

			-- CUSTOMER MASTER FILE
			JOIN dbo.AE_CustomerMasterFiles
				ON RM00101.CUSTNMBR = convert(char,AE_CustomerMasterFiles.CustomerMasterFileId)

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
		WHERE 
			AE_CustomerMasterFiles.CustomerMasterFileID = @CMFID
			AND 
			(
			(CONVERT(DATE,AE_Customers.ModifiedOn) > RM00101.MODIFDT)
			OR
			(CONVERT(DATE,AE_CustomerAddress.ModifiedOn) > RM00101.MODIFDT)
			OR
			(CONVERT(DATE,MC_Addresses.ModifiedOn) > RM00101.MODIFDT)
			OR
			(CONVERT(DATE,RU_TeamLocations.ModifiedOn) > RM00101.MODIFDT)
			)

		/**************
		***  EMAIL  ***
		***************/
		UPDATE RM00106
		SET 
--			CUSTNMBR = convert(char,AE_CustomerMasterFiles.CustomerMasterFileId),  -- Customer Number
			EMAIL_TYPE = 1,
			EMAIL_RECIPIENT = AE_Customers.Email
		FROM 
			-- RM00106
			DYSNEYDAD.NEX.dbo.RM00106 AS RM00106

			-- CUSTOMER MASTER FILE
			JOIN dbo.AE_CustomerMasterFiles
				ON RM00106.CUSTNMBR = convert(char,AE_CustomerMasterFiles.CustomerMasterFileId)

			-- CUSTOMERS
			INNER JOIN dbo.AE_Customers
				ON AE_CustomerMasterFiles.CustomerMasterFileID = AE_Customers.CustomerMasterFileId
		WHERE 
			AE_CustomerMasterFiles.CustomerMasterFileID = @CMFID
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

GRANT EXEC ON dbo.custAE_CustomerUpdateInGreatPlains TO PUBLIC
GO

/** EXEC dbo.custAE_CustomerUpdateInGreatPlains 3051528 */