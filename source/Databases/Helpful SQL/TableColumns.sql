USE [WISE_CRM]
GO
--SELECT SCHEMA_NAME(schema_id) AS schema_name
--    ,o.name AS object_name
--	,o.object_id
--    ,o.type_desc
--    --,p.parameter_id
--    --,p.name AS parameter_name
--    --,TYPE_NAME(p.user_type_id) AS parameter_type
--    --,p.max_length
--    --,p.precision
--    --,p.scale
--    --,p.is_output
--FROM sys.objects AS o
----INNER JOIN sys.parameters AS p ON o.object_id = p.object_id
--WHERE o.name = 'AE_CustomerMasterFiles'
----WHERE o.object_id = OBJECT_ID('dbo.AE_CustomerMasterFiles')
--ORDER BY schema_name, object_name--, p.parameter_id;

SELECT --*
	ORDINAL_POSITION AS POS
	, '' AS [R]
	, COLUMN_NAME AS [Name]
	, UPPER(DATA_TYPE) AS [Type]
	, ISNULL(CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR), '') AS [Len]
	, ISNULL(COLUMN_DEFAULT, '') AS [Default Value]
	, IS_NULLABLE AS [Nullable]
	, 'NO' AS [Auto Increment]
	, '' AS [Description]
FROM WISE_CRM.INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = N'IE_WarehouseSites';