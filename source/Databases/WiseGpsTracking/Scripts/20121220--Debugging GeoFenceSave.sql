/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 * FROM [WISE_GPSTRACKING].[dbo].[LP_CommandMessageEAVRSP3s] ORDER BY CommandMessageID DESC;
SELECT TOP 10 * FROM [WISE_GPSTRACKING].[dbo].[LP_Requests] ORDER BY RequestID DESC;
SELECT TOP 20 * FROM [WISE_GPSTRACKING].[dbo].[LP_CommandMessages] ORDER BY CommandMessageID DESC;

BEGIN TRANSACTION
/** Resete fences. 
DELETE [WISE_GPSTRACKING].[dbo].LP_GsGeoFences;
DBCC CHECKIDENT ('[WISE_GPSTRACKING].[dbo].LP_GsGeoFences', RESEED, 1);

DELETE [WISE_GPSTRACKING].[dbo].GS_AccountGeoFenceRectangles;

DELETE [WISE_GPSTRACKING].[dbo].GS_AccountGeoFences;
DBCC CHECKIDENT ('[WISE_GPSTRACKING].[dbo].GS_AccountGeoFences', RESEED, 1);
*/
ROLLBACK TRANSACTION