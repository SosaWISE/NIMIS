USE [WISE_CRM]
GO

--SELECT * FROM [dbo].[MS_Equipments] AS EQM WITH (NOLOCK) WHERE
--	(EQM.EquipmentID NOT IN (SELECT ItemID FROM [dbo].[AE_ItemInterims]));
--SELECT * FROM [dbo].[MS_EquipmentMonitoredTypes];
--SELECT * FROM [dbo].[MS_EquipmentTypes];
--SELECT * FROM [dbo].[MS_AccountZoneTypes];
--SELECT * FROM [dbo].[MS_EquipmentPanelTypes];

BEGIN TRANSACTION

--/** \1 */
--INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
--VALUES ('\1', \2, \3, '\4', \5, '\6', '\7', '\8', '\9');

/** EQPM_EXST_MS376 */
PRINT 'Here you go EQPM_EXST_MS376';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS376', 1, 1, 'POLICE', 5, 'EEQ-GE-DOORWINDOW', 'Generic GE Door/Window Sensor', 'GEN GE D/W Sensor', 'Generic GE Door/Window Sensor (EEQ-GE-DOORWINDOW)');

/** EQPM_EXST_MS377 */
PRINT 'Here you go EQPM_EXST_MS377';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS377', 11, 3, 'POLICE', 5, 'EEQ-GE-MOTION', 'Generic GE Motion Sensor', 'GEN GE MTN Sensor', 'Generic GE Motion Sensor (EEQ-GE-MOTION)');

/** EQPM_EXST_MS378 */
PRINT 'Here you go EQPM_EXST_MS378';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS378', 10, 5, 'POLICE', 5, 'EEQ-GE-GLASSBREAK', 'Generic GE Glass Break ', 'GEN GE GLASS', 'Generic GE Glass Break  (EEQ-GE-GLASSBREAK)');

/** EQPM_EXST_MS379 */
PRINT 'Here you go EQPM_EXST_MS379';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS379', 7, 4, 'FIRE', 5, 'EEQ-GE-SMOKE', 'Generic GE Smoke Sensor', 'GEN GE SMOKE', 'Generic GE Smoke Sensor (EEQ-GE-SMOKE)');

/** EQPM_EXST_MS380 */
PRINT 'Here you go EQPM_EXST_MS380';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS380', 6, 2, 'POLICE', 5, 'EEQ-GE-KEYFOB', 'Generic GE Key Fob', 'GEN GE KEYFOB', 'Generic GE Key Fob (EEQ-GE-KEYFOB)');

/** EQPM_EXST_MS381 */
PRINT 'Here you go EQPM_EXST_MS381';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS381', 13, 9, 'FIRE', 5, 'EEQ-GE-FREEZE', 'Generic GE Freeze Sensor', 'GEN GE FREEZE', 'Generic GE Freeze Sensor (EEQ-GE-FREEZE)');

/** EQPM_EXST_MS382 */
PRINT 'Here you go EQPM_EXST_MS382';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS382', 9, 7, 'FIRE', 5, 'EEQ-GE-CARBON', 'Generic GE Carbon Sensor', 'GEN GE CARBON', 'Generic GE Carbon Sensor (EEQ-GE-CARBON)');

/** EQPM_EXST_MS383 */
PRINT 'Here you go EQPM_EXST_MS383';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS383', 13, 8, 'FIRE', 5, 'EEQ-GE-FLOOD', 'Generic GE Flood Sensor', 'GEN GE FLOOD', 'Generic GE Flood Sensor (EEQ-GE-FLOOD)');

/** EQPM_EXST_MS384 */
PRINT 'Here you go EQPM_EXST_MS384';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS384', 1, 1, 'POLICE', 5, 'EEQ-GE-RECDOORWINDOW', 'Generic GE Recessed Door/Window Sensor', 'GEN GE R D/W Sensor', 'Generic GE Recessed Door/Window Sensor (EEQ-GE-RECDOORWINDOW)');

/** EQPM_EXST_MS385 */
PRINT 'Here you go EQPM_EXST_MS385';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS385', 4, 6, 'MEDICAL', 5, 'EEQ-GE-MEDICALPEN', 'Generic GE Medical Pendant', 'GEN GE Med Pen', 'Generic GE Medical Pendant (EEQ-GE-MEDICALPEN)');

/** EQPM_EXST_MS386 */
PRINT 'Here you go EQPM_EXST_MS386';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS386', 12, 10, 'NOZONE', 5, 'EEQ-GE-TOUCHSCRN', 'Generic GE Touch Screen', 'GEN GE Toch SCR', 'Generic GE Touch Screen (EEQ-GE-TOUCHSCRN)');

/** EQPM_EXST_MS387 */
PRINT 'Here you go EQPM_EXST_MS387';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS387', 12, 10, 'NOZONE', 5, 'EEQ-GE-KEYPAD', 'Generic GE Keypad', 'GEN GE KEYPAD', 'Generic GE Keypad (EEQ-GE-KEYPAD)');

/** EQPM_EXST_MS388 */
PRINT 'Here you go EQPM_EXST_MS388';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS388', 1, 1, 'POLICE', 5, 'EEQ-HY-DOORWINDOW', 'Generic HY Door/Window Sensor', 'GEN HY D/W Sensor', 'Generic HY Door/Window Sensor (EEQ-HY-DOORWINDOW)');

--/** EQPM_EXST_MS389 */
--PRINT 'Here you go EQPM_EXST_MS389';
--INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
--VALUES ('EQPM_EXST_MS389', 11, 3, 'POLICE', 5, 'EEQ-HY-MOTION', 'Generic HY Motion Sensor', 'GEN HY MTN Sensor', 'Generic HY Motion Sensor (EEQ-HY-MOTION)');

/** EQPM_EXST_MS390 */
PRINT 'Here you go EQPM_EXST_MS390';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS390', 10, 5, 'POLICE', 5, 'EEQ-HY-GLASSBREAK', 'Generic HY Glass Break ', 'GEN HY GLASS', 'Generic HY Glass Break  (EEQ-HY-GLASSBREAK)');

/** EQPM_EXST_MS391 */
PRINT 'Here you go EQPM_EXST_MS391';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS391', 7, 4, 'FIRE', 5, 'EEQ-HY-SMOKE', 'Generic HY Smoke Sensor', 'GEN HY SMOKE', 'Generic HY Smoke Sensor (EEQ-HY-SMOKE)');

/** EQPM_EXST_MS392 */
PRINT 'Here you go EQPM_EXST_MS392';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS392', 6, 2, 'POLICE', 5, 'EEQ-HY-KEYFOB', 'Generic HY Key Fob', 'GEN HY KEYFOB', 'Generic HY Key Fob (EEQ-HY-KEYFOB)');

/** EQPM_EXST_MS393 */
PRINT 'Here you go EQPM_EXST_MS393';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS393', 13, 9, 'FIRE', 5, 'EEQ-HY-FREEZE', 'Generic HY Freeze Sensor', 'GEN HY FREEZE', 'Generic HY Freeze Sensor (EEQ-HY-FREEZE)');

/** EQPM_EXST_MS394 */
PRINT 'Here you go EQPM_EXST_MS394';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS394', 9, 7, 'FIRE', 5, 'EEQ-HY-CARBON', 'Generic HY Carbon Sensor', 'GEN HY CARBON', 'Generic HY Carbon Sensor (EEQ-HY-CARBON)');

/** EQPM_EXST_MS395 */
PRINT 'Here you go EQPM_EXST_MS395';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS395', 13, 8, 'FIRE', 5, 'EEQ-HY-FLOOD', 'Generic HY Flood Sensor', 'GEN HY FLOOD', 'Generic HY Flood Sensor (EEQ-HY-FLOOD)');

/** EQPM_EXST_MS396 */
PRINT 'Here you go EQPM_EXST_MS396';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS396', 1, 1, 'POLICE', 5, 'EEQ-HY-RECDOORWINDOW', 'Generic HY Recessed Door/Window Sensor', 'GEN HY R D/W Sensor', 'Generic HY Recessed Door/Window Sensor (EEQ-HY-RECDOORWINDOW)');

/** EQPM_EXST_MS397 */
PRINT 'Here you go EQPM_EXST_MS397';
INSERT INTO [dbo].[MS_Equipments] (EquipmentID, EquipmentMonitoredTypeId, EquipmentTypeId, AccountZoneTypeId, EquipmentPanelTypeId, GPItemNmbr, ItemDescription, ShortName, GenDescription)
VALUES ('EQPM_EXST_MS397', 4, 6, 'MEDICAL', 5, 'EEQ-HY-MEDICALPEN', 'Generic HY Medical Pendant', 'GEN HY Med Pen', 'Generic HY Medical Pendant (EEQ-HY-MEDICALPEN)');

ROLLBACK TRANSACTION