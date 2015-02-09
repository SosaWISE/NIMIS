USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_AccountGetPurchasedAccounts')
	BEGIN
		PRINT 'Dropping Procedure custMS_AccountGetPurchasedAccounts'
		DROP  Procedure  dbo.custMS_AccountGetPurchasedAccounts
	END
GO

PRINT 'Creating Procedure custMS_AccountGetPurchasedAccounts'
GO
/******************************************************************************
**		File: custMS_AccountGetPurchasedAccounts.sql
**		Name: custMS_AccountGetPurchasedAccounts
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
**		Auth: Andres Sosa
**		Date: 10/06/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	10/06/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_AccountGetPurchasedAccounts
(
	@City NVARCHAR(50) = NULL,
	@StateAB NCHAR(2) = NULL,
	@County NVARCHAR(50) = NULL,
	@PurchaseDateStart DATETIME = NULL,
	@PurchaseDateEnd DATETIME = NULL
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY

		SELECT MSA.AccountID
			, (MCL.FirstName + ' ' + MCL.LastName) AS [CustomerName]
			, MCA.StreetAddress
			, MCA.City
			, MCPS.StateAB
			, MCA.PostalCode
			, MCA.County
			, MSA.PremisePhone
			, MCL.PhoneHome
			, MCL.PhoneWork
			, MCL.PhoneWorkExt
			, MCL.PhoneCell
			, FEP.PurchaserName AS [PurchasedBy]
			, FPC.PurchasedDate
		FROM MC_Address AS MCA WITH (NOLOCK)
			INNER JOIN MC_PoliticalState AS MCPS WITH (NOLOCK)
			ON
				(MCA.StateID = MCPS.StateID)
			INNER JOIN MS_Account AS MSA WITH (NOLOCK)
			ON
				(MCA.AddressID = MSA.PremiseAddressID)
			INNER JOIN MC_Lead AS MCL WITH (NOLOCK)
			ON
				(MSA.Customer1ID = MCL.LeadID)
			INNER JOIN MS_AccountStatus AS MSAS WITH (NOLOCK)
			ON
				(MSA.AccountID = MSAS.AccountID)
			LEFT JOIN dbo.SAE_AccountFundingStatus AS AFS WITH (NOLOCK)
			ON
				(MSA.AccountID = AFS.AccountID)
			LEFT JOIN FE_PurchasedAccount AS FPA WITH (NOLOCK)
			ON
				(AFS.AccountFundingStatusId = FPA.AccountFundingStatusId)
			LEFT JOIN dbo.FE_PurchaseContract AS FPC WITH (NOLOCK)
			ON
				((FPA.PurchaseContractID = FPC.PurchaseContractID) 
					AND (FPC.IsCollateralized = 0))
			LEFT JOIN FE_Purchasers AS FEP WITH (NOLOCK)
			ON
				(FPC.PurchaserId = FEP.PurchaserId)
		WHERE (@City IS NULL OR MCA.City = @City)
				AND (@StateAB IS NULL OR MCPS.StateAB = @StateAB)
				AND (@County IS NULL OR MCA.County = @County)
				AND ((@PurchaseDateStart IS NULL AND @PurchaseDateEnd IS NULL) OR
					(FPC.PurchasedDate BETWEEN @PurchaseDateStart AND @PurchaseDateEnd))
				AND MSAS.InactiveReasonID IS NULL
				AND MSAS.InstallDate IS NOT NULL
				AND FEP.PurchaserName IS NOT NULL
		ORDER BY MSA.AccountID

	END TRY
	BEGIN CATCH
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custMS_AccountGetPurchasedAccounts TO PUBLIC
GO

/** EXEC dbo.custMS_AccountGetPurchasedAccounts */