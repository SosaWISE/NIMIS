USE WISE_CRM;
GO


SELECT
	MAC.AccountID
	, MAC.SimProductBarcodeId
	, MAC.GpsWatchProductBarcodeId
	, MAC.GpsWatchPhoneNumber
	, MAC.GpsWatchPassword
	, MAC.GpsWatchUnitID
	, IND.*
	
FROM
	MS_Accounts AS MAC WITH (NOLOCK)
	INNER JOIN dbo.MS_IndustryAccounts AS IND WITH (NOLOCK)
	ON
		(MAC.IndustryAccountId = IND.IndustryAccountID)
WHERE
	(MAC.AccountID = 100166);
	

--USE WISE_Receiver;
--GO

--/** Alert from S911 BL, ID 80002602 ,11:10am,22/03/13,Panic Alert, LAT North 35.697815 & LON West 86.686690 */
--SELECT * FROM dbo.RE_TxtWireRequests WHERE PHONE = '13133580068' ORDER BY TxtWireRequestID DESC;