USE WISE_CRM
GO

/** Check seed value. */
--DBCC CHECKIDENT ('dbo.AE_CustomerMasterToCustomer', NORESEED);

/** Reset it. */
DBCC CHECKIDENT ('dbo.AE_CustomerMasterToCustomer', RESEED, 9);
