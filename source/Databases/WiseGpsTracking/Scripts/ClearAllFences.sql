USE [WISE_GPSTRACKING]
GO

BEGIN TRANSACTION

DELETE dbo.GS_AccountGeoFencePolygons WHERE (GeoFenceId IN (SELECT GeoFenceID FROM dbo.GS_AccountGeoFences WHERE AccountId = 100169));
DELETE dbo.GS_AccountGeoFenceCircles WHERE (GeoFenceId IN (SELECT GeoFenceID FROM dbo.GS_AccountGeoFences WHERE AccountId = 100169));
DELETE dbo.GS_AccountGeoFencePoints WHERE (GeoFenceId IN (SELECT GeoFenceID FROM dbo.GS_AccountGeoFences WHERE AccountId = 100169));
DELETE dbo.GS_AccountGeoFenceRectangles WHERE (GeoFenceId IN (SELECT GeoFenceID FROM dbo.GS_AccountGeoFences WHERE AccountId = 100169));

DELETE dbo.LP_GsGeoFences WHERE (GsGeoFenceId IN (SELECT GeoFenceID FROM dbo.GS_AccountGeoFences WHERE AccountId = 100169));

DELETE dbo.GS_AccountGeoFences WHERE AccountId = 100169;

ROLLBACK TRANSACTION
