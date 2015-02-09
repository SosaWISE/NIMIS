USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID')
	BEGIN
		PRINT 'Dropping Procedure custAE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID'
		DROP  Procedure  dbo.custAE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID
	END
GO

PRINT 'Creating Procedure custAE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID'
GO
/******************************************************************************
**		File: custAE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID.sql
**		Name: custAE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID
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
**		Date: 00/00/2012
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	00/00/2012	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID
(
	@CustomerMasterFileId BIGINT
	, @CustomerID BIGINT
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** Execute Query. */
	SELECT 
		CUST.[CustomerID]
		, CASE
			WHEN (CUST.[CustomerID] = @CustomerID) THEN CAST ('true' AS BIT)
			ELSE CAST('false' AS BIT)
		  END AS [IsCurrent]
		, CUST.[CustomerTypeId]
		, CUST.[CustomerTypeUi]
		, CUST.[CustomerMasterFileId]
		, CUST.[DealerId]
		, CUST.[AddressId]
		, CUST.[LeadId]
		, CUST.[LocalizationId]
		, CUST.[Prefix]
		, CUST.[FirstName]
		, CUST.[MiddleName]
		, CUST.[LastName]
		, CUST.[Postfix]
		, CUST.[Gender]
		, CUST.[PhoneHome]
		, CUST.[PhoneWork]
		, CUST.[PhoneMobile]
		, CUST.[Email]
		, CUST.[DOB]
		, CUST.[SSN]
		, CUST.[Username]
		, CUST.[Password]
		, CUST.[LastLoginOn]
		, CUST.[IsActive]
		, CUST.[IsDeleted]
		, CUST.[ModifiedOn]
		, CUST.[ModifiedBy]
		, CUST.[CreatedOn]
		, CUST.[CreatedBy]
		, CUST.[DEX_ROW_TS]
	FROM
		[dbo].[vwAE_GpsClientToCustomerMaster] AS CUST
	WHERE
		((@CustomerMasterFileId IS NULL) OR (CUST.[CustomerMasterFileId] = @CustomerMasterFileId))
		AND ((@CustomerID IS NULL) OR (CUST.[CustomerID] = @CustomerID));
	
END
GO

GRANT EXEC ON dbo.custAE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID TO PUBLIC
GO

--EXEC dbo.custAE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID 3000035, 100195;