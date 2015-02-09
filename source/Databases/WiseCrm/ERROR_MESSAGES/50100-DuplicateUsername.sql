USE WISE_CRM
GO
/** Local Declarations. */
DECLARE @message_id INT = 50100;
DECLARE @us_english VARCHAR(50) = 'us_english';
DECLARE @gen_spanish VARCHAR(50) = 'spanish';

BEGIN TRANSACTION

IF (EXISTS(SELECT * FROM sys.messages WHERE (message_id = @message_id)))
BEGIN
	PRINT 'DROPING messages. ';
	EXEC sp_dropmessage @message_id, @gen_spanish;
	EXEC sp_dropmessage @message_id, @us_english;
END

/** US English. */
PRINT 'CREATING MSGID: ' + CAST(@message_id AS VARCHAR) + ' | Local: '  + CAST(@us_english AS VARCHAR); 
EXEC sp_addmessage @msgnum = @message_id, @severity = 16, 
	@msgtext = N'The username ''%s'' already exists.  Please use a different one.', 
	@lang = @us_english,
	@with_log = 'FALSE';

/** Generic Spanish. */
PRINT 'CREATING MSGID: ' + CAST(@message_id AS VARCHAR) + ' | Local: '  + CAST(@gen_spanish AS VARCHAR); 
EXEC sp_addmessage @msgnum = @message_id, @severity = 16, 
	@msgtext = N'El nombre de usuario ''%s'' ya existe.  Por favor use uno diferente.', 
	@lang = @gen_spanish,
	@with_log = 'FALSE';

COMMIT TRANSACTION

--SELECT Count(*) FROM sys.messages GROUP BY	Message_ID;
SELECT * FROM sys.messages WHERE (Message_id = @message_id);
