USE [NXSE_Funding]
GO

DECLARE @AccountID BIGINT
	, @CustomerNumber BIGINT
	, @PacketID INT
	, @Csid VARCHAR(20)
	, @GPEmployeeID VARCHAR(50);
SET @PacketID = 100100;
SET @GPEmployeeID = 'SOSAA001'
--BEGIN TRANSACTION

	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3081373) AND (Csid = '768260016');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091378) AND (Csid = '768260023');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091379) AND (Csid = '768260024');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091380) AND (Csid = '768260026');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091381) AND (Csid = '768260027');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091382) AND (Csid = '768260028');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091383) AND (Csid = '768260029');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091385) AND (Csid = '768260031');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091402) AND (Csid = '768260040');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091405) AND (Csid = '768260043');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091408) AND (Csid = '768260045');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091410) AND (Csid = '768260046');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091412) AND (Csid = '768260047');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091414) AND (Csid = '768260048');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091419) AND (Csid = '768260051');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3081375) AND (Csid = '768260017');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091384) AND (Csid = '768260030');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091394) AND (Csid = '768260033');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091398) AND (Csid = '768260036');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091416) AND (Csid = '768260049');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091418) AND (Csid = '768260050');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091420) AND (Csid = '768260052');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


	--SELECT @CustomerNumber = CustomerMasterFileId, @AccountID = AccountID, @CSID = Csid FROM [dbo].[vwMS_Accounts] WHERE (CustomerMasterFileId = 3091427) AND (Csid = '768260055');
	--PRINT '|-- AccountID: ' + CAST(@AccountID AS VARCHAR(20)) + ' | CMFID: ' + CAST(@CustomerNumber AS VARCHAR(20)) + ' | CSID: ' + @Csid;
	--EXEC dbo.custFE_PacketItemsAdd @PacketID, @AccountID, @GPEmployeeID;


--ROLLBACK TRANSACTION

--DELETE dbo.FE_PacketItems
--DBCC CHECKIDENT ('[dbo].[FE_PacketItems]', RESEED, 0);

--DELETE dbo.FE_AccountFundingStatus
--DBCC CHECKIDENT ('[dbo].[FE_AccountFundingStatus]', RESEED, 0);
