USE Platinum_Protection_InterimCRM

DECLARE @reporttype VARCHAR(20)
DECLARE @SendToInsideSales BIT
DECLARE @stateAB char(2)
SET @reporttype =
'Inside Sales'
--= 'Outside Sales'
--='ByZip'
SET @stateAB = 'VA'
SET @SendToInsideSales = 0

/******************************
***  GET PLATINUM ACCOUNTS  ***
*******************************
-- select AccountID into a temp table if the following criteria are met:
-- specific post codes
-- not friends and family - if the monthly fee is >= $30 we assume they are not F&F
-- not test accounts - make sure that first name doesn't start with 'TEST'
-- accounts not cancelled for the following reasons: Sent to Collections, Bankruptcy, Collection Settlement, Death
*/
IF object_id('tempdb..#PlatinumAccounts') IS NOT NULL DROP TABLE #PlatinumAccounts

SELECT
	DISTINCT MS_ACCOUNT.AccountID AS AccountID
INTO #PlatinumAccounts
FROM
	-- ACCOUNT
	MS_ACCOUNT as MS_Account with (nolock)

	-- LEAD FOR CUSTOMER 1
	left join MC_Lead as customer1 with (nolock)
		on MS_ACCOUNT.Customer1ID = customer1.LeadID

	-- LEAD FOR CUSTOMER 2
	left join MC_Lead as customer2 with (nolock)
		on MS_ACCOUNT.Customer2ID = customer2.LeadID

	-- ACCOUNT STATUS
	left join MS_AccountStatus as MS_AccountStatus with (nolock)
		on MS_ACCOUNT.AccountID = MS_AccountStatus.AccountID

	-- PURCHASERS
	left join FE_Purchasers as FE_Purchasers with (nolock)
		on MS_AccountStatus.PurchasedById = FE_Purchasers.PurchaserId

	-- ACCOUNT STATUS INACTIVE REASONS
	left join MS_AccountStatusInactiveReasons as MS_AccountStatusInactiveReasons with (nolock)
		on MS_AccountStatus.InactiveReasonID = MS_AccountStatusInactiveReasons.InactiveReasonID

	-- COLLECTION AGENCIES
	left join MC_CollectionAgencies as MC_CollectionAgencies with (nolock)
		on MS_AccountStatus.SentToCollectionsAgencyID = MC_CollectionAgencies.CollectionAgencyId

	-- PREMISE ADDRESS
	left join MC_Address as MC_Address with (nolock)
		on MS_ACCOUNT.PremiseAddressID = MC_Address.AddressID

	-- STATE
	left join MC_PoliticalState as States with (nolock)
		on MC_Address.StateID = States.StateID

	-- ACCOUNT BILLING INFO
	left join MC_AccountBillingInfo as MC_AccountBillingInfo with (nolock)
		on MS_ACCOUNT.AccountID = MC_AccountBillingInfo.AccountID

	-- PAYMENT METHODS
	left join MC_PaymentMethods as MC_PaymentMethods with (nolock)
		on MC_AccountBillingInfo.New_AutoBillingMethod = MC_PaymentMethods.PaymentMethodID

	-- MONITORING STATION OS
	left join MS_MonitoringStationOS as MS_MonitoringStationOS with (nolock)
		on MS_ACCOUNT.MStationOSID = MS_MonitoringStationOS.MStationOSID

	-- MONITORING STATION
	left join MS_MonitoringStation as MS_MonitoringStation with (nolock)
		on MS_MonitoringStationOS.MStationID = MS_MonitoringStation.MStationID

	---- CREDIT SCORES
	--LEFT JOIN
	--	(
	--	SELECT AccountID, IndustryNumber, CreditScore, QualificationDate
	--		FROM 
	--			(
	--			SELECT AccountID, IndustryNumber, CreditScore, QualificationDate
	--			FROM Platinum_Protection_Recruiting.dbo.SAE_MaxCredit AS MC WITH (NOLOCK)
	--			UNION
	--			SELECT AccountID, CONVERT(varchar, AccountID) AS IndustryNumber, CreditScore, QualificationDate
	--			FROM Platinum_Protection_Recruiting.dbo.SAE_MaxCredit AS MC1 WITH (NOLOCK)) AS MC1_1
	--	) AS MC
	--	ON MS_ACCOUNT.AccountId = MC.accountid

	-- EMERGENCY CONTACT
	left join MS_EmergencyContact as MS_EmergencyContact with (nolock)
		on MS_ACCOUNT.AccountID = MS_EmergencyContact.AccountID and MS_EmergencyContact.EmergencyContactRelationshipId in (31, 87)

	---- SALES REPS
	--left join Platinum_Protection_recruiting.dbo.RU_Users as RU_Users1 with (nolock)
	--	on MS_ACCOUNT.GPSalesRepID = RU_Users1.GPEmployeeID

	---- TECHNICIANS
	--left join Platinum_Protection_Recruiting.dbo.RU_Users as RU_Users2 with (nolock)
	--	on MS_ACCOUNT.GPTechnicianID = RU_Users2.GPEmployeeID
WHERE
	MS_ACCOUNT.MonthlyFee >= 30
	AND customer1.FirstName NOT LIKE 'TEST%'
	AND (
		(MS_AccountStatusInactiveReasons.ReasonDescription NOT IN ('Sent to Collections','Bankruptcy','Collection Settlement','Death')) 
		OR (MS_AccountStatusInactiveReasons.ReasonDescription IS NULL)
		)
	--AND 
		--states.StateAB = @stateAB
/* Ran this WHERE states.StateAB = 'UT' when I ran the report for the remainder of UT (accounts I hadn't run previously)
	(
		states.StateAB = @stateAB
		AND
		MC_Address.PostalCode NOT IN
		(
		'84045','84005', -- Saratoga Springs, Eagle Mountain
		'84003','84042','84062', -- American Fork, Lindon, Pleasant Grove
		'84057', '84058', '84097', '84601', '84604', '84606', '84660', -- Provo,Orem
		'84047','84092','84093','84094', --Sandy
		'84081','84084','84088', -- West Jordan
		'84118', '84119', '84120', '84128', -- West Valley
		'84044','84101','84102','84103','84104','84105','84106','84107','84108','84109','84111','84112','84113','84115','84116','84117','84121','84123','84124','84129' -- Salt Lake City
		)
	)
*/
	------	IN ('84045','84005') -- Saratoga Springs, Eagle Mountain
	------	IN ('84003','84042','84062') - American Fork, Lindon, Pleasant Grove
	------	IN ('84057', '84058', '84097', '84601', '84604', '84606', '84660') -- Provo,Orem
	------	IN ('84047','84092','84093','84094') --Sandy
	------IN ('84081','84084','84088') -- West Jordan
	----IN ('84118', '84119', '84120', '84128') -- West Valley
	--IN ('84044','84101','84102','84103','84104','84105','84106','84107','84108','84109','84111','84112','84113','84115','84116','84117','84121','84123','84124','84129') -- Salt Lake City

/***************************************
***  GENERATE LIST FOR INSIDE SALES  ***
****************************************/
-- These accounts had at least 1 valid phone number we can call
IF @reporttype = 'Inside Sales'
BEGIN
	SELECT 
		MS_Account.AccountID AS 'Account ID',
		Lead1.LeadID as 'Lead ID',
		Lead1.FirstName as 'Customer1 First Name',
		Lead1.LastName as 'Customer1 Last Name',
		CONVERT(DATE,Lead1.DOB) as DOB,
		ISNULL(Lead1.Email,'') as Email,
		ISNULL(Lead2.FirstName,'') AS 'Customer2 First Name',
		ISNULL(Lead2.LastName,'') AS 'Customer2 Last Name',
		ISNULL(MC_Address.StreetAddress,'') as 'Address 1',
		ISNULL(MC_Address.StreetAddress2,'') as 'Address 2',
		MC_Address.City as City,
		MC_PoliticalState.StateAB as State,
		MC_Address.PostalCode as Zip,
		--'' AS DOB,
		--'' AS CREDITSCORE,
		--'' AS LEADDATE,
		--'' AS CREATEDATE,
		--'' AS MODIFYDATE,
		MS_Account.PanelTypeID as 'Panel Type',
		--'' AS POINTSGIVEN,
		--'' AS INSTALLDATE,
		--'' AS NEW_TERMS,
		--'' AS ABORTCODE,
		--'' AS ACTIVITIONFEE,
		--'' AS UPGRADEFEE,
		--'' AS TECHUPGRADEFEE,
		--'' AS MONTHLYOVERRIDE,
		MS_Account.MonthlyFee,
		--'' AS PAYMENTMETHOD,
		--'' AS NEW_BILLINGDATE,
		--'' AS PREMISEAREA,
		--ISNULL(platinum.PremisePhone,'') AS 'Premise Phone',
		--ISNULL(platinum.AlternativePhone,'') AS 'Alternative Phone',
		--ISNULL(platinum.HomePhoneCust1,'') as 'Cust1 Home Phone',
		--ISNULL(platinum.CellPhoneCust1,'') as 'Cust1 Cell Phone',
		--ISNULL(platinum.HomePhoneCust2,'') as Cust2HomePhone,
		--ISNULL(platinum.CellPhoneCust2,'') as Cust2CellPhone,
		ISNULL(FE_Purchasers.PurchaserName,'') as 'Purchaser Name',
		'Platinum Accounts' as 'Lead Source'
	FROM
		#PlatinumAccounts as platinum
	 
		JOIN Platinum_Protection_InterimCRM.dbo.MS_Account
			on platinum.AccountID = MS_Account.AccountID

		--	MC_LEAD VIA CUSTOMER1ID
		JOIN Platinum_Protection_InterimCRM.dbo.MC_Lead AS Lead1
			on MS_Account.Customer1ID = Lead1.LeadID

		LEFT JOIN Platinum_Protection_InterimCRM.dbo.MC_Lead as Lead2
			on MS_Account.Customer2ID = Lead2.LeadID

		JOIN Platinum_Protection_InterimCRM.dbo.MC_Address
			on MS_Account.PremiseAddressID = MC_Address.AddressID

		JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalState
			on mc_address.StateID = MC_PoliticalState.StateID

		---- PURCHASED ACCOUNTS
		LEFT JOIN Platinum_Protection_InterimCRM.dbo.FE_Purchasers WITH (NOLOCK)
		   ON MS_Account.AccountOwnerID = FE_Purchasers.PurchaserId

		--LEFT JOIN Platinum_Protection_InterimCRM.dbo.FE_PurchasedAccount
		--	ON MS_Account.AccountID = FE_PurchasedAccount.AccountId
		--LEFT JOIN Platinum_Protection_InterimCRM.dbo.FE_PurchaseContract
		--	ON FE_PurchasedAccount.PurchaseContractId = FE_PurchaseContract.PurchaseContractId
		--LEFT JOIN Platinum_Protection_InterimCRM.DBO.FE_Purchasers
		--	ON FE_PurchaseContract.PurchaserId = FE_Purchasers.PurchaserId
	WHERE Lead1.Email IS NOT NULL
	ORDER BY 
		Lead1.LastName,
		Lead1.FirstName
END

/****************************************
***  GENERATE LIST FOR OUTSIDE SALES  ***
****************************************/
-- These accounts have no valid phone numbers we can call

IF @reporttype = 'Outside Sales'
BEGIN
	SELECT 
		MS_Account.AccountID AS AccountID,
		Lead1.LeadID as LeadID,
		Lead1.FirstName as Customer1FirstName,
		Lead1.LastName as Customer1LastName,
		Lead1.DOB as DOB,
		Lead1.Email as Email,
		ISNULL(Lead2.FirstName,'') AS Customer2FirstName,
		ISNULL(Lead2.LastName,'') AS Customer2LastName,
		ISNULL(MC_Address.StreetAddress,'') as Address1,
		ISNULL(MC_Address.StreetAddress2,'') as Address2,
		MC_Address.City as City,
		MC_PoliticalState.StateAB as State,
		MC_Address.PostalCode as Zip,
		--'' AS DOB,
		--'' AS CREDITSCORE,
		--'' AS LEADDATE,
		--'' AS CREATEDATE,
		--'' AS MODIFYDATE,
		MS_Account.PanelTypeID as PanelType,
		--'' AS POINTSGIVEN,
		--'' AS INSTALLDATE,
		--'' AS NEW_TERMS,
		--'' AS ABORTCODE,
		--'' AS ACTIVITIONFEE,
		--'' AS UPGRADEFEE,
		--'' AS TECHUPGRADEFEE,
		--'' AS MONTHLYOVERRIDE,
		MS_Account.MonthlyFee,
		--'' AS PAYMENTMETHOD,
		--'' AS NEW_BILLINGDATE,
		--'' AS PREMISEAREA,
		--ISNULL(platinum.PremisePhone,'') AS PremisePhone,
		--ISNULL(platinum.AlternativePhone,'') AS AlternativePhone,
		--ISNULL(platinum.WorkPhone,'') AS WorkPhone,
		--ISNULL(platinum.Cust1HomePhone,'') as Cust1HomePhone,
		--ISNULL(platinum.Cust1CellPhone,'') as Cust1CellPhone,
		--ISNULL(platinum.Cust1WorkPhone,'') as Cust1WorkPhone,
		--ISNULL(platinum.Cust2HomePhone,'') as Cust2HomePhone,
		--ISNULL(platinum.Cust2CellPhone,'') as Cust2CellPhone,
		--ISNULL(platinum.Cust2WorkPhone,'') as Cust2WorkPhone,
		ISNULL(FE_Purchasers.PurchaserName,'') as PurchaserName,
		'Platinum Accounts' as LeadSource
	FROM
		#PlatinumPhones as platinum
	 
		JOIN Platinum_Protection_InterimCRM.dbo.MS_Account
			on platinum.AccountID = MS_Account.AccountID
			and platinum.DNC = 1

		--	MC_LEAD VIA CUSTOMER1ID
		JOIN Platinum_Protection_InterimCRM.dbo.MC_Lead AS Lead1
			on MS_Account.Customer1ID = Lead1.LeadID

		LEFT JOIN Platinum_Protection_InterimCRM.dbo.MC_Lead as Lead2
			on MS_Account.Customer2ID = Lead2.LeadID

		JOIN Platinum_Protection_InterimCRM.dbo.MC_Address
			on MS_Account.PremiseAddressID = MC_Address.AddressID

		JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalState
			on mc_address.StateID = MC_PoliticalState.StateID

		---- PURCHASED ACCOUNTS
		LEFT JOIN Platinum_Protection_InterimCRM.dbo.FE_Purchasers WITH (NOLOCK)
		   ON MS_Account.AccountOwnerID = FE_Purchasers.PurchaserId

		--LEFT JOIN Platinum_Protection_InterimCRM.dbo.FE_PurchasedAccount
		--	ON MS_Account.AccountID = FE_PurchasedAccount.AccountId
		--LEFT JOIN Platinum_Protection_InterimCRM.dbo.FE_PurchaseContract
		--	ON FE_PurchasedAccount.PurchaseContractId = FE_PurchaseContract.PurchaseContractId
		--LEFT JOIN Platinum_Protection_InterimCRM.DBO.FE_Purchasers
		--	ON FE_PurchaseContract.PurchaserId = FE_Purchasers.PurchaserId
	ORDER BY 
		Lead1.LastName,
		Lead1.FirstName
END

IF @reporttype = 'ByZip'
BEGIN

-- Total Platinum Accounts
	SELECT 
		MC_PoliticalState.StateAB as State,
		MC_Address.City as City,
		MC_Address.PostalCode as Zip,
		count(DISTINCT MS_Account.AccountID)
	FROM
		#PlatinumAccounts as platinum
	 
		JOIN Platinum_Protection_InterimCRM.dbo.MS_Account
			on platinum.AccountID = MS_Account.AccountID

		----	MC_LEAD VIA CUSTOMER1ID
		--JOIN Platinum_Protection_InterimCRM.dbo.MC_Lead AS Lead1
		--	on MS_Account.Customer1ID = Lead1.LeadID

		--LEFT JOIN Platinum_Protection_InterimCRM.dbo.MC_Lead as Lead2
		--	on MS_Account.Customer2ID = Lead2.LeadID

		JOIN Platinum_Protection_InterimCRM.dbo.MC_Address
			on MS_Account.PremiseAddressID = MC_Address.AddressID

		JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalState
			on mc_address.StateID = MC_PoliticalState.StateID

		------ PURCHASED ACCOUNTS
		--LEFT JOIN Platinum_Protection_InterimCRM.dbo.FE_Purchasers WITH (NOLOCK)
		--   ON MS_Account.AccountOwnerID = FE_Purchasers.PurchaserId

	--WHERE MC_PoliticalState.StateAB IN ('TN')
	--WHERE MC_PoliticalState.StateAB = @stateAB
	GROUP BY		
		MC_PoliticalState.StateAB,
		MC_Address.City,
		MC_Address.PostalCode
	ORDER BY 
		MC_PoliticalState.StateAB,
		MC_Address.City,
		MC_Address.PostalCode
/*
-- Accounts with at least one phone number not on the DNC
	SELECT 
		MC_PoliticalState.StateAB as State,
		MC_Address.City as City,
		MC_Address.PostalCode as Zip,
		count(DISTINCT MS_Account.AccountID)
	FROM
		#PlatinumPhones as platinum
	 
		JOIN Platinum_Protection_InterimCRM.dbo.MS_Account
			on platinum.AccountID = MS_Account.AccountID
			and platinum.DNC = 0

		--	MC_LEAD VIA CUSTOMER1ID
		JOIN Platinum_Protection_InterimCRM.dbo.MC_Lead AS Lead1
			on MS_Account.Customer1ID = Lead1.LeadID

		LEFT JOIN Platinum_Protection_InterimCRM.dbo.MC_Lead as Lead2
			on MS_Account.Customer2ID = Lead2.LeadID

		JOIN Platinum_Protection_InterimCRM.dbo.MC_Address
			on MS_Account.PremiseAddressID = MC_Address.AddressID

		JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalState
			on mc_address.StateID = MC_PoliticalState.StateID

		---- PURCHASED ACCOUNTS
		LEFT JOIN Platinum_Protection_InterimCRM.dbo.FE_Purchasers WITH (NOLOCK)
		   ON MS_Account.AccountOwnerID = FE_Purchasers.PurchaserId

--WHERE MC_PoliticalState.StateAB IN ('TN')
WHERE MC_PoliticalState.StateAB = @stateAB
GROUP BY		
	MC_PoliticalState.StateAB,
	MC_Address.City,
	MC_Address.PostalCode
ORDER BY 
	MC_PoliticalState.StateAB,
	MC_Address.City,
	MC_Address.PostalCode
*/
END
