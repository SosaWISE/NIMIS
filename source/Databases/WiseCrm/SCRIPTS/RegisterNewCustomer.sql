USE [WISE_CRM]
GO
/**********************************************************************************************************************
* Description:  Create a new registration for a new portal customer. 
*
* Date: 05/30/2013
**********************************************************************************************************************/

/* PARAM Declarations */
DECLARE @DealerID INT = 5000;
DECLARE @UserID NVARCHAR(20) = 'SYSTEM';
DECLARE @LocalizationID VARCHAR(20) = 'en-US';
DECLARE @SalesRepId VARCHAR(10) = 'MSTR001';
DECLARE @Email NVARCHAR(256);
DECLARE @LeadSourceId INT = 1;

/** Local Scope Declarations. */
DECLARE @CMFID BIGINT;
DECLARE @AddressID BIGINT;
DECLARE @LeadID BIGINT;


/* Create a lead. */

/** Before we ever create a Lead we need to create a CMFID. */
INSERT INTO dbo.AE_CustomerMasterFiles (DealerId, CreatedBy) VALUES (@DealerID, @UserID);
SET @CMFID = SCOPE_IDENTITY();

/** Need to create a leads address first. */
INSERT INTO dbo.QL_Address (
	DealerId ,
	ValidationVendorId ,
	AddressValidationStateId ,
	StateId ,
	CountryId ,
	TimeZoneId ,
	AddressTypeId ,
	StreetAddress ,
	City ,
	PostalCode ,
	Phone ,
	DeliveryPoint ,
	Latitude ,
	Longitude ,
	CongressionalDistric ,
	DPV ,
	IsActive ,
	IsDeleted ,
	CreatedBy
) VALUES (
	@DealerId , -- DealerId - int
	'NOVENDOR' , -- ValidationVendorId - varchar(20)
	'UNV' , -- AddressValidationStateId - varchar(5)
	'NN' , -- StateId - varchar(4)
	N'NON' , -- CountryId - nvarchar(10)
	0 , -- TimeZoneId - int
	'N' , -- AddressTypeId - varchar(10)
	N'' , -- StreetAddress - nvarchar(50)
	N'' , -- City - nvarchar(50)
	N'00000' , -- PostalCode - nvarchar(5)
	'' , -- Phone - varchar(20)
	N'' , -- DeliveryPoint - nvarchar(2)
	0.0 , -- Latitude - float
	0.0 , -- Longitude - float
	0 , -- CongressionalDistric - int
	NULL , -- DPV - bit
	1 , -- IsActive - bit
	0 ,   -- IsDeleted - bit
	@UserID
);
SET @AddressID = SCOPE_IDENTITY();

INSERT INTO dbo.QL_Leads (
	AddressId ,
	CustomerTypeId ,
	CustomerMasterFileId ,
	DealerId ,
	LocalizationId ,
	TeamLocationId ,
	SeasonId ,
	SalesRepId ,
	LeadSourceId ,
	LeadDispositionId ,
	LeadDispositionDateChange ,
	Salutation ,
	FirstName ,
	MiddleName ,
	LastName ,
	Suffix ,
	Gender ,
	Email ,
	CreatedBy
) VALUES (
@AddressID , -- AddressId - bigint
'LEAD' , -- CustomerTypeId - varchar(20)
@CMFID , -- CustomerMasterFileId - bigint
@DealerID , -- DealerId - int
@LocalizationID , -- LocalizationId - varchar(20)
0 , -- TeamLocationId - int
0 , -- SeasonId - int
@SalesRepId , -- SalesRepId - varchar(10)
0 , -- LeadSourceId - int
0 , -- LeadDispositionId - int
'2013-05-31 19:57:58' , -- LeadDispositionDateChange - datetime
N'' , -- Salutation - nvarchar(50)
N'' , -- FirstName - nvarchar(50)
N'' , -- MiddleName - nvarchar(50)
N'' , -- LastName - nvarchar(50)
N'' , -- Suffix - nvarchar(50)
N'' , -- Gender - nvarchar(10)
@Email , -- Email - nvarchar(256)
@UserID  -- CreatedBy - nvarchar(50)
)