/****** Script for SelectTopNRows command from SSMS  ******/
--SELECT  [AgencyID]
--      ,LOC.[LocationID]
--	  , LOC.LocationName
--	  , LCT.LocationTypeName
--	  , LCS.LocationName AS [State]
--      , AGN.[AgencyName]
--      , AGN.[Description]
--      , AGN.[Contact]
--      , AGN.[Website]
--      , AGN.[Phone1]
--      , ISNULL(AGN.[Phone2],'') AS Phone2
--      , ISNULL(AGN.[Fax],'') AS Fax
--      , ISNULL(AGN.[StreetAddress], '') AS StreetAddress
--      , ISNULL(AGN.[City],'') AS City
--      , ISNULL(AGN.[StateProvince], '') AS StateProvince
--      , ISNULL(AGN.[ZipCode], '') AS ZipCode
--      , ISNULL(AGN.[StreetAddress2], '') AS StreetAddress2
--      , ISNULL(AGN.[City2], '') AS City2
--      , ISNULL(AGN.[StateProvince2], '') AS StateProvince2
--      , ISNULL(AGN.[ZipCode2], '') AS ZipCode2
--      , ISNULL(AGN.[Country], '') AS Country
--  FROM 
--	[dbo].[LM_Agencies] AS AGN WITH (NOLOCK)
--	INNER JOIN [dbo].[LM_Locations] AS LOC WITH (NOLOCK)
--	ON
--		(LOC.LocationID = AGN.LocationID)
--	INNER JOIN [dbo].[LM_LocationTypes] AS LCT WITH (NOLOCK)
--	ON
--		(LOC.LocationTypeID = LCT.LocationTypeID)
--	LEFT OUTER JOIN [dbo].[LM_Locations] AS LCS WITH (NOLOCK)
--	ON
--		(LCS.LocationID = LOC.ParentStateID)
--ORDER BY
--	LCT.LocationTypeName

SELECT
	REQ.RequirementID
	, REQ.LocationID
	, REQ.RequirementTypeID
	, RQT.RequirementTypeName
	, REQ.AgencyID
	, AGN.AgencyName
	, REQ.RequirementName
	, REQ.ApplicationDescription
	, REQ.CallCenterMessage
	, ISNULL(REQ.RequiredForFunding, 0) AS RequiredForFunding
	, ISNULL(REQ.Fee, 0) AS Fee
FROM
	[dbo].[LM_Requirements] AS REQ WITH (NOLOCK)
	INNER JOIN [dbo].[LM_RequirementTypes] AS RQT WITH (NOLOCK)
	ON
		(RQT.RequirementTypeID = REQ.RequirementTypeID)
	INNER JOIN [dbo].[LM_Agencies] AS AGN WITH (NOLOCK)
	ON
		(AGN.AgencyID = REQ.AgencyID)