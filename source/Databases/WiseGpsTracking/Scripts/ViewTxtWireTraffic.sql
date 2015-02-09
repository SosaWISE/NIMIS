USE WISE_Receiver
GO

SELECT 
	*
FROM
	dbo.RE_TxtWireRequests AS TWR WITH (NOLOCK)
WHERE
	(TWR.Phone = '13134676579') -- Jessy
	OR (TWR.Phone = '15135107527') -- Andres Device
ORDER BY 
	TWR.TxtWireRequestID DESC