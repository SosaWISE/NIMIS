USE [WISE_GPSTRACKING]
GO

DECLARE @AccountID BIGINT;
DECLARE @UnitID BIGINT;
DECLARE @Password VARCHAR(8);
DECLARE @Sentence VARCHAR(250);

BEGIN TRANSACTION

--SET @AccountID = 80003902; SET @UnitID = 80003902; SET @Sentence = '$LPCFG,00000000,AT00=1,AT01=8*7A';
--INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('LPCFG', @AccountID, @UnitID, @Sentence, 0);

/** Turn off G-Sensor */
--SET @Password = '00000000'; SET @AccountID = 80003902; SET @UnitID = 80003902; SET @Sentence = '$AVCFG,00000000,6*63'; 
--INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('AVCFG', @AccountID, @UnitID, @Sentence, 0);

/** Get 1 GeoFence Item */
--$EAVGOF,00000000,4,0*34
--$EAVGOF,00000000,4,1*35
--$EAVGOF,00000000,4,2*36
--$EAVGOF,00000000,4,3*37
--$EAVGOF,00000000,4,4*30
SET @Password = '00000000'; SET @AccountID = 80003902; SET @UnitID = 80003902; SET @Sentence = '$EAVGOF,00000000,4,0*34';
INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('EAVGOF', @AccountID, @UnitID, @Sentence, 0);
SET @Sentence = '$EAVGOF,00000000,4,1*35';
INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('EAVGOF', @AccountID, @UnitID, @Sentence, 0);
SET @Sentence = '$EAVGOF,00000000,4,2*36';
INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('EAVGOF', @AccountID, @UnitID, @Sentence, 0);
SET @Sentence = '$EAVGOF,00000000,4,3*37';
INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('EAVGOF', @AccountID, @UnitID, @Sentence, 0);
SET @Sentence = '$EAVGOF,00000000,4,4*30';
INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('EAVGOF', @AccountID, @UnitID, @Sentence, 0);
/** Deletes all the fences. */
--SET @Sentence = '$EAVGOF,00000000,2,0*32';
--SET @Sentence = '$EAVGOF,00000000,2,1*33';
--SET @Sentence = '$EAVGOF,00000000,2,2*30';
--SET @Sentence = '$EAVGOF,00000000,2,3*31';
--SET @Sentence = '$EAVGOF,00000000,2,4*36';
--INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('EAVGOF', @AccountID, @UnitID, @Sentence, 0);

/** Get Device Location */
--SET @Password = '00000000'; SET @AccountID = 80003902; SET @UnitID = 80003902; SET @Sentence = '$AVREQ,00000000,1*60';
--INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('AVREQ', @AccountID, @UnitID, @Sentence, 0);

/** Request Other System Information */
--SET @Password = '00000000'; SET @AccountID = 80003902; SET @UnitID = 80003902; SET @Sentence = '$EAVGOF,00000000,2,4*36';
--INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('EAVGOF', @AccountID, @UnitID, @Sentence, 0);

--SET @Password = '95468715'; SET @AccountID = 00100195; SET @UnitID = 00100195; SET @Sentence = '$LPCFG,95468715,AT00=0,AT01=8*7E'; 
--INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('LPCFG', @AccountID, @UnitID, @Sentence, 0);

--SET @Password = '95468715'; SET @AccountID = 00100195; SET @UnitID = 00100195; SET @Sentence = '$AVCFG,95468715,6*66'; 
--INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('AVCFG', @AccountID, @UnitID, @Sentence, 0);

/** Get 1 GeoFence Item */
--SET @Password = '95468715'; SET @AccountID = 00100195; SET @UnitID = 00100195; SET @Sentence = '$EAVGOF,95468715,4,0*31';
--INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('EAVGOF', @AccountID, @UnitID, @Sentence, 0);

/** Get Device Location */
--SET @Password = '95468715'; SET @AccountID = 00100195; SET @UnitID = 00100195; SET @Sentence = '$AVREQ,95468715,1*65';
--INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('AVREQ', @AccountID, @UnitID, @Sentence, 0);

/** Request a Geo Fence  $EAVGOF, PSW, 4, Geo-fencei* CHKSUM 
SET @Password = '95468715'; SET @AccountID = 00100195; SET @UnitID = 00100195; SET @Sentence = '$EAVGOF,95468715,4,5*34';
INSERT INTO dbo.LP_Requests (RequestNameId, AccountId, UnitID, Sentence, Attempts) VALUES  ('EAVGOF', @AccountID, @UnitID, @Sentence, 0);
*/

COMMIT TRANSACTION