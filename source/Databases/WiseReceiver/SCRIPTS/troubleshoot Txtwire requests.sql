USE WISE_Receiver;
GO

--SELECT * FROM WISE_CRM.[dbo].MS_Accounts 


SELECT
	TWR.TxtWireRequestID
	, TWR.Title
	, TWR.ShortCode
	, TWR.Message
	, TWR.Phone
	, TWR.CreatedBy
	, TWR.CreatedOn
FROM
	dbo.RE_TxtWireRequests AS TWR WITH (NOLOCK)
WHERE
	(TWR.Phone = '13132896563') OR (TWR.Phone = '13133580068')
ORDER BY
	TWR.TxtWireRequestID DESC