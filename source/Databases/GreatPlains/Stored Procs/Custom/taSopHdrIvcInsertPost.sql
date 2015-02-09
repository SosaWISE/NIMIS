USE [PPROT]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'taSopHdrIvcInsertPost')
	BEGIN
		PRINT 'Dropping Procedure taSopHdrIvcInsertPost'
		DROP  Procedure  dbo.taSopHdrIvcInsertPost
	END
GO

PRINT 'Creating Procedure taSopHdrIvcInsertPost'
GO
/******************************************************************************
**		File: taSopHdrIvcInsertPost.sql
**		Name: taSopHdrIvcInsertPost
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
**		Auth: 
**		Date: 
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**			
*******************************************************************************/
CREATE Procedure dbo.taSopHdrIvcInsertPost
(
	@I_vSOPTYPE smallint
	, @I_vDOCID char(15)
	, @I_vSOPNUMBE char(21)
	, @I_vORIGNUMB char(21)
	, @I_vORIGTYPE smallint
	, @I_vTAXSCHID char(15)
	, @I_vFRTSCHID char(15)
	, @I_vMSCSCHID char(15)
	, @I_vSHIPMTHD char(15)
	, @I_vTAXAMNT numeric(19,5)
	, @I_vLOCNCODE char(10)
	, @I_vDOCDATE datetime
	, @I_vFREIGHT numeric(19,5)
	, @I_vMISCAMNT numeric(19,5)
	, @I_vTRDISAMT numeric(19,5)
	, @I_vTRADEPCT numeric(19,2)
	, @I_vDISTKNAM numeric(19,5)
	, @I_vMRKDNAMT numeric(19,5)
	, @I_vCUSTNMBR char(15)
	, @I_vCUSTNAME char(64)
	, @I_vCSTPONBR char(20)
	, @I_vShipToName char(64)
	, @I_vADDRESS1 char(60)
	, @I_vADDRESS2 char(60)
	, @I_vADDRESS3 char(60)
	, @I_vCNTCPRSN char(60)
	, @I_vFAXNUMBR char(21)
	, @I_vCITY char(35)
	, @I_vSTATE char(29)
	, @I_vZIPCODE char(10)
	, @I_vCOUNTRY char(60)
	, @I_vPHNUMBR1 char(21)
	, @I_vPHNUMBR2 char(21)
	, @I_vPHNUMBR3 char(21)
	, @I_vSUBTOTAL numeric(19,5)
	, @I_vDOCAMNT numeric(19,5)
	, @I_vPYMTRCVD numeric(19,5)
	, @I_vSALSTERR char(15)
	, @I_vSLPRSNID char(15)
	, @I_vUPSZONE char(3)
	, @I_vUSER2ENT char(15)
	, @I_vBACHNUMB char(15)
	, @I_vPRBTADCD char(15)
	, @I_vPRSTADCD char(15)
	, @I_vFRTTXAMT numeric(19,5)
	, @I_vMSCTXAMT numeric(19,5)
	, @I_vORDRDATE datetime
	, @I_vMSTRNUMB int
	, @I_vPYMTRMID char(20)
	, @I_vDUEDATE datetime
	, @I_vDISCDATE datetime
	, @I_vREFRENCE char(30)
	, @I_vUSINGHEADERLEVELTAXES int
	, @I_vBatchCHEKBKID char(15)
	, @I_vCREATECOMM smallint
	, @I_vCOMMAMNT numeric(19,2)
	, @I_vCOMPRCNT numeric(19,2)
	, @I_vCREATEDIST smallint
	, @I_vCREATETAXES smallint
	, @I_vDEFTAXSCHDS smallint
	, @I_vCURNCYID char(15)
	, @I_vXCHGRATE numeric(19,7)
	, @I_vRATETPID char(15)
	, @I_vEXPNDATE datetime
	, @I_vEXCHDATE datetime
	, @I_vEXGTBDSC char(30)
	, @I_vEXTBLSRC char(50)
	, @I_vRATEEXPR smallint
	, @I_vDYSTINCR smallint
	, @I_vRATEVARC numeric(19,7)
	, @I_vTRXDTDEF smallint
	, @I_vRTCLCMTD smallint
	, @I_vPRVDSLMT smallint
	, @I_vDATELMTS smallint
	, @I_vTIME1 datetime
	, @I_vDISAVAMT numeric(19,5)
	, @I_vDSCDLRAM numeric(19,5)
	, @I_vDSCPCTAM numeric(19,2)
	, @I_vFREIGTBLE int
	, @I_vMISCTBLE int
	, @I_vCOMMNTID char(15)
	, @I_vCOMMENT_1 char(50)
	, @I_vCOMMENT_2 char(50)
	, @I_vCOMMENT_3 char(50)
	, @I_vCOMMENT_4 char(50)
	, @I_vGPSFOINTEGRATIONID char(30)
	, @I_vINTEGRATIONSOURCE smallint
	, @I_vINTEGRATIONID char(30)
	, @I_vReqShipDate datetime
	, @I_vRequesterTrx smallint
	, @I_vCKCreditLimit tinyint
	, @I_vCKHOLD tinyint
	, @I_vUpdateExisting tinyint
	, @I_vQUOEXPDA datetime
	, @I_vQUOTEDAT datetime
	, @I_vINVODATE datetime
	, @I_vBACKDATE datetime
	, @I_vRETUDATE datetime
	, @I_vCMMTTEXT varchar(500)
	, @I_vPRCLEVEL char(10)
	, @I_vDEFPRICING tinyint
	, @I_vTAXEXMT1 char(25)
	, @I_vTAXEXMT2 char(25)
	, @I_vTXRGNNUM char(25)
	, @I_vREPTING tinyint
	, @I_vTRXFREQU smallint
	, @I_vTIMETREP smallint
	, @I_vQUOTEDYSTINCR smallint
	, @I_vNOTETEXT varchar(8000)
	, @I_vUSRDEFND1 char(50)
	, @I_vUSRDEFND2 char(50)
	, @I_vUSRDEFND3 char(50)
	, @I_vUSRDEFND4 varchar(8000)
	, @I_vUSRDEFND5 varchar(8000)
	, @O_iErrorState int output
	, @oErrString varchar(255) output
)
AS
BEGIN

	SET NOCOUNT ON
	
	DECLARE @BACHNUMB VARCHAR (15)
	DECLARE @STATUS INT
	DECLARE @SOPTYPE INT
	DECLARE @SOPNUMBE VARCHAR (21)
	DECLARE @DOCNUMBR VARCHAR (21)
	DECLARE @NEWDOCNMBR VARCHAR (21)
	DECLARE @COUNTER INT
	DECLARE @CHEKBKID VARCHAR (15)
	DECLARE @AMNTPAID NUMERIC (19,5)
	DECLARE @DOCDATE DATETIME


	SET @STATUS = 0

	/* Check to see if Customer is a Credit Card Customer*/
	if 	((SELECT CRCARDID FROM RM00101 WHERE CUSTNMBR = @I_vCUSTNMBR) NOT LIKE  '')
		AND (@STATUS = 0)
		AND (@I_vDOCID in ('MONITORING', 'INITIAL', 'UPGRADE','SERVICE','MISC'))
		AND (@I_vBACHNUMB = 'MONITORING')
		BEGIN
			-- Set Batch Number value
			SET @BACHNUMB = 'MONITORING-CC'  -- Set Batch Number value
			SELECT @AMNTPAID = @I_vDOCAMNT
					
			--Update Batch Number on SOP document by appending "-CC"
			UPDATE
				SOP10100
			SET
				bachnumb = @BACHNUMB
			WHERE
				(SOPNUMBE = @I_vSOPNUMBE)
				AND (SOPTYPE = @I_vSOPTYPE)
			
			IF (SELECT COUNT(*) FROM SY00500 WHERE BACHNUMB = @BACHNUMB AND BCHSOURC = 'Sales Entry') = 0 
				BEGIN
					INSERT INTO SY00500 (	GLPOSTDT, BCHSOURC, BACHNUMB, SERIES, MKDTOPST, NUMOFTRX, RECPSTGS, DELBACH, MSCBDINC, BACHFREQ,
						RCLPSTDT, NOFPSTGS, BCHCOMNT, BRKDNALL, CHKSPRTD, RVRSBACH, USERID, CHEKBKID, BCHTOTAL,
						BCHEMSG1, BCHEMSG2, BACHDATE, BCHSTRG1, BCHSTRG2, POSTTOGL, MODIFDT, CREATDDT, NOTEINDX,
						CURNCYID, BCHSTTUS, CNTRLTRX, CNTRLTOT, PETRXCNT, APPROVL, APPRVLDT, APRVLUSERID, ORIGIN,
						ERRSTATE, GLBCHVAL, Computer_Check_Doc_Date, Sort_Checks_By, SEPRMTNC, REPRNTED, CHKFRMTS,
						TRXSORCE, PmtMethod, EFTFileFormat)

					VALUES ('1900-01-01 00:00:00.000', 'Sales Entry', @BACHNUMB, 3, 0, 1, 0, 0, 0, 1,
						'1900-01-01 00:00:00.000', 0, '', 0, 0, 0, '', 'PP - OPER', @AMNTPAID, 
						0x00000000, 0x00000000, '1900-01-01 00:00:00.000', '', '', 0, '2004-05-03 00:00:00.000', '2004-05-03 00:00:00.000', 0,
						'', 0, 0, .00000, 0, 0, '1900-01-01 00:00:00.000', '', 1,
						0, 0x00000000, '1900-01-01 00:00:00.000', 0, 0, 0, 0,
						'', 0, 1025)
				END

				ELSE
					UPDATE SY00500 SET 	
						NUMOFTRX = isnull((SELECT COUNT(*) FROM SOP10100 WHERE BACHNUMB = @BACHNUMB),0),
						BCHTOTAL = isnull((SELECT SUM(DOCAMNT) FROM SOP10100 WHERE BACHNUMB = @BACHNUMB),0)
					WHERE BACHNUMB = @BACHNUMB

			SET @STATUS = 1	
		END
		
	--CHECK TO SEE IF CUSTOMER IS AN EFT CUSTOMER AND IF SO SET BATCH NUMBER = MONITORING-EFT AND BUILD CASH RECEIPT RECORD
	IF ((SELECT COUNT(*) FROM SY06000 WHERE CUSTNMBR = @I_vCUSTNMBR AND LEN(RTRIM(EFTBankAcct)) > 0)  > 0 )
		AND (@STATUS = 0)
		AND (@I_vDOCID in ('MONITORING', 'INITIAL', 'UPGRADE','SERVICE','MISC'))
		AND (@I_vBACHNUMB = 'MONITORING')
		BEGIN
			
			-- Set VARIABLE VALUES
			SET @BACHNUMB = 'MONITORING-EFT'  -- Set Batch Number value
			SELECT @DOCNUMBR = DOCNUMBR FROM RM40401 WHERE RMDTYPAL = 9
			IF EXISTS (SELECT DOCNUMBR FROM RM00401 WHERE DOCNUMBR = @DOCNUMBR)
				BEGIN
					SELECT @COUNTER = 1
					WHILE @COUNTER < 1000001
						BEGIN
							EXECUTE udp_SELECT_NEXT_DOCNUMBR @DOCNUMBR, @NEWDOCNMBR = @DOCNUMBR OUTPUT
							IF NOT EXISTS (SELECT DOCNUMBR FROM RM00401 WHERE DOCNUMBR = @DOCNUMBR)
								BEGIN
									GOTO CONTINUE_PROCESSING
								END-- ENDS NOT EXISTS STATEMENT
							SELECT @COUNTER = @COUNTER + 1
						END -- ENDS WHILE LOOP
				END -- ENDS IF EXISTS STATEMENT
	CONTINUE_PROCESSING:

			SELECT @SOPTYPE = @I_vSOPTYPE, 
				@SOPNUMBE = @I_vSOPNUMBE,
				@AMNTPAID = @I_vDOCAMNT,
				@DOCDATE = @I_vDOCDATE
			SELECT @CHEKBKID = CHEKBKID FROM SOP40100


			--Update Batch Number on SOP document by appending "-EFT"
			UPDATE
				SOP10100
			SET
				BACHNUMB = @BACHNUMB
			WHERE
				SOPNUMBE = @I_vSOPNUMBE
				AND SOPTYPE = @I_vSOPTYPE
			
			if (select count(*) from sy00500 where BACHNUMB = @BACHNUMB and BCHSOURC = 'Sales Entry') = 0 
				BEGIN
					insert into sy00500 (	GLPOSTDT, BCHSOURC, BACHNUMB, SERIES, MKDTOPST, NUMOFTRX, RECPSTGS, DELBACH, MSCBDINC, BACHFREQ,
						RCLPSTDT, NOFPSTGS, BCHCOMNT, BRKDNALL, CHKSPRTD, RVRSBACH, USERID, CHEKBKID, BCHTOTAL,
						BCHEMSG1, BCHEMSG2, BACHDATE, BCHSTRG1, BCHSTRG2, POSTTOGL, MODIFDT, CREATDDT, NOTEINDX,
						CURNCYID, BCHSTTUS, CNTRLTRX, CNTRLTOT, PETRXCNT, APPROVL, APPRVLDT, APRVLUSERID, ORIGIN,
						ERRSTATE, GLBCHVAL, Computer_Check_Doc_Date, Sort_Checks_By, SEPRMTNC, REPRNTED, CHKFRMTS,
						TRXSORCE, PmtMethod, EFTFileFormat)

					values ('1900-01-01 00:00:00.000', 'Sales Entry', @BACHNUMB, 3, 0, 1, 0, 0, 0, 1,
						'1900-01-01 00:00:00.000', 0, '', 0, 0, 0, '', @CHEKBKID, @AMNTPAID, 
						0x00000000, 0x00000000, '1900-01-01 00:00:00.000', '', '', 0, '2004-05-03 00:00:00.000', '2004-05-03 00:00:00.000', 0,
						'', 0, 0, .00000, 0, 0, '1900-01-01 00:00:00.000', '', 1,
						0, 0x00000000, '1900-01-01 00:00:00.000', 0, 0, 0, 0,
						'', 0, 1025)
				END
				ELSE
					UPDATE SY00500 SET 	
						NUMOFTRX = isnull((SELECT COUNT(*) FROM SOP10100 WHERE BACHNUMB = @BACHNUMB),0),
						BCHTOTAL = isnull((SELECT SUM(DOCAMNT) FROM SOP10100 WHERE BACHNUMB = @BACHNUMB),0)
					WHERE BACHNUMB = @BACHNUMB

			--BUILD CASH RECEIPT RECORD HERE.
				IF (SELECT COUNT(*) FROM SOP10103 WHERE SOPTYPE = @I_vSOPTYPE AND SOPNUMBE = @I_vSOPNUMBE) = 0 
					BEGIN 			
						--UPDATE NEXT PAYMENT NUMBER IN RM SETUP FILE
						
						EXECUTE udp_SELECT_NEXT_DOCNUMBR @DOCNUMBR, @NEWDOCNMBR = @NEWDOCNMBR OUTPUT
						UPDATE RM40401 SET DOCNUMBR = @NEWDOCNMBR WHERE  RMDTYPAL = 9
						
						--INSERT CASH RECEIPT RECORD IN SOP10103
						
						INSERT INTO SOP10103 (SOPTYPE, SOPNUMBE, SEQNUMBR, PYMTTYPE, DOCNUMBR, 
						RMDTYPAL, CHEKBKID, CHEKNMBR, CARDNAME, RCTNCCRD, AUTHCODE, AMNTPAID, OAMTPAID,
						AMNTREMA, OAMNTREM, DOCDATE, EXPNDATE, CURNCYID, CURRNIDX, TRXSORCE, DEPSTATS,
						DELETE1, GLPOSTDT, CASHINDEX, DEPINDEX)
						VALUES (@SOPTYPE, @SOPNUMBE, 16384, 5, @DOCNUMBR, 9, @CHEKBKID, 'EFT',
						'', '', '', @AMNTPAID, @AMNTPAID, @AMNTPAID,@AMNTPAID, @DOCDATE,
						'1900-01-01', '', 1007, '', 0, 0, '1900-01-01', 0, 0)
						
						-- BUILD RM KEYS RECORD
						INSERT INTO RM00401 (DOCNUMBR, RMDTYPAL, DCSTATUS, BCHSOURC, TRXSORCE,
									  CUSTNMBR, CHEKNMBR, DOCDATE)
						VALUES (@DOCNUMBR, 9, 0, '', '', '', '', '1900-01-01')
						
						--UPDATE SOP10100 DOCUMENT WITH PAYMENT INFORMATION
						UPDATE
							SOP10100
						SET
							PYMTRCVD = @AMNTPAID
							, ORPMTRVD = @AMNTPAID
							, ACCTAMNT = 0
							, ORACTAMT = 0
						WHERE
							SOPTYPE = @I_vSOPTYPE
							AND SOPNUMBE = @I_vSOPNUMBE

						--UPDATE DISTRIBUTION RECORD FOR CASH SIDE OF THE TRANSACTOIN
						UPDATE
							SOP10102
						SET 
							DISTTYPE = 3
							, ACTINDX = D.ACTINDX
						FROM
							SOP10102 A 
							INNER JOIN SOP10100 B 
							ON 
								A.SOPTYPE = B.SOPTYPE AND A.SOPNUMBE = B.SOPNUMBE AND A.DISTTYPE = 2
							INNER JOIN SOP10103 C 
							ON 
								B.SOPTYPE = C.SOPTYPE AND B.SOPNUMBE = C.SOPNUMBE
							INNER JOIN CM00100 D 
							ON 
								C.CHEKBKID = D.CHEKBKID 
						WHERE
							B.SOPTYPE = @I_vSOPTYPE
							AND B.SOPNUMBE = @I_vSOPNUMBE
					END
		SET @STATUS = 1
	END
	
	SELECT @O_iErrorState = 0  
	RETURN (@O_iErrorState)
	
END
GO

GRANT EXEC ON dbo.taSopHdrIvcInsertPost TO PUBLIC
GO 