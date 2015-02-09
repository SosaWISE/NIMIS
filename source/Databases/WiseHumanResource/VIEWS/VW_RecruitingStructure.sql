USE [WISE_HumanResource]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'VW_RecruitingStructure')
	BEGIN
		PRINT 'Dropping VIEW VW_RecruitingStructure'
		DROP VIEW dbo.VW_RecruitingStructure
	END
GO

PRINT 'Creating VIEW VW_RecruitingStructure'
GO
/******************************************************************************
**		File: VW_RecruitingStructure.sql
**		Name: VW_RecruitingStructure
**		Desc: 
**
**		Return values: 
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: Andrés E. Sosa
**		Date: 01/31/2011
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	--------	--------		-------------------------------------------
**	01/31/2011	Andrés E. Sosa	Created
*******************************************************************************/
CREATE VIEW dbo.VW_RecruitingStructure
AS
	SELECT
		*
	FROM
	(
	
		SELECT
		
			_1_2_3_4.RecruitID
			, _1_2_3_4.SeasonID
			
			, COALESCE(
					_1_2_3_4.ManagerID
					, _1_3_4.ManagerID
					, _1_2_4.ManagerID
					, _1_4.ManagerID
					) AS ManagerID
			
			, COALESCE(
					_1_2_3_4.TeamID
					, _1_3_4.TeamID
					, _1_2_4.TeamID
					, _1_4.TeamID
					) AS TeamID
			, COALESCE(
					_1_2_3_4.RegionID
					, _1_3_4.RegionID
					, _1_2_4.RegionID
					, _1_4.RegionID
					) AS RegionID
			, COALESCE(
					_1_2_3_4.NationalRegionID
					, _1_3_4.NationalRegionID
					, _1_2_4.NationalRegionID
					, _1_4.NationalRegionID
					) AS NationalRegionID
			
		FROM
		(
			--Reporting Levels: 1, 2, 3 and 4
			SELECT

				Rep.RecruitID
				, Rep.SeasonID
				
				, Man.RecruitID AS ManagerID
				
				, RT.TeamID
				, Reg.RecruitID AS RegionID
				, Ntl.RecruitID AS NationalRegionID
				
			FROM vw_RecruitUser AS Rep WITH(NOLOCK)
			LEFT OUTER JOIN vw_RecruitUser AS Man WITH(NOLOCK)
			ON
				Rep.ReportsToID = Man.RecruitID
				AND (Man.ReportingLevel = 2)
			LEFT OUTER JOIN vw_RecruitUser AS Reg WITH(NOLOCK)
			ON
				Man.ReportsToID = Reg.RecruitID
				AND (Reg.ReportingLevel = 3)
			LEFT OUTER JOIN vw_RecruitUser AS Ntl WITH(NOLOCK)
			ON
				Reg.ReportsToID = Ntl.RecruitID
				AND (Ntl.ReportingLevel = 4)

			--Manager's team
			LEFT OUTER JOIN RU_Teams AS RT WITH(NOLOCK)
			ON
				Man.TeamID = RT.TeamID
			
			WHERE
				Rep.ReportingLevel = 1
				
		) AS _1_2_3_4

		LEFT OUTER JOIN
		(
			--Reporting Levels: 1, 3 and 4
			SELECT

				Rep.RecruitID
				, Rep.SeasonID
				
				, Reg.RecruitID AS ManagerID
				
				, RT.TeamID
				, Reg.RecruitID AS RegionID
				, Ntl.RecruitID AS NationalRegionID
				
			FROM vw_RecruitUser AS Rep WITH(NOLOCK)
			--Skips manager, goes straight to Regional
			LEFT OUTER JOIN vw_RecruitUser AS Reg WITH(NOLOCK)
			ON
				Rep.ReportsToID = Reg.RecruitID
				AND (Reg.ReportingLevel = 3)
			LEFT OUTER JOIN vw_RecruitUser AS Ntl WITH(NOLOCK)
			ON
				Reg.ReportsToID = Ntl.RecruitID
				AND (Ntl.ReportingLevel = 4)

			--Regional's team
			LEFT OUTER JOIN RU_Teams AS RT WITH(NOLOCK)
			ON
				Reg.TeamID = RT.TeamID
				
			WHERE
				Rep.ReportingLevel = 1
				
		) AS _1_3_4
		ON
			_1_2_3_4.RecruitID = _1_3_4.RecruitID
		
		LEFT OUTER JOIN
		(
			--Reporting Levels: 1, 2 and 4
			SELECT

				Rep.RecruitID
				, Rep.SeasonID
				
				, Man.RecruitID AS ManagerID
				
				, RT.TeamID
				, CAST(NULL AS INT) AS RegionID
				, Ntl.RecruitID AS NationalRegionID
				
			FROM vw_RecruitUser AS Rep WITH(NOLOCK)
			LEFT OUTER JOIN vw_RecruitUser AS Man WITH(NOLOCK)
			ON
				Rep.ReportsToID = Man.RecruitID
				AND (Man.ReportingLevel = 2)
			--Skips Regional, goes straight to National Regional
			LEFT OUTER JOIN vw_RecruitUser AS Ntl WITH(NOLOCK)
			ON
				Man.ReportsToID = Ntl.RecruitID
				AND (Ntl.ReportingLevel = 4)

			--Manager's team
			LEFT OUTER JOIN RU_Teams AS RT WITH(NOLOCK)
			ON
				Man.TeamID = RT.TeamID
			
			WHERE
				Rep.ReportingLevel = 1
				
		) AS _1_2_4
		ON
			_1_2_3_4.RecruitID = _1_2_4.RecruitID
		
		LEFT OUTER JOIN
		(
			--Reporting Levels: 1 and 4
			SELECT

				Rep.RecruitID
				, Rep.SeasonID
				
				, Ntl.RecruitID AS ManagerID
				
				, RT.TeamID
				, CAST(NULL AS INT) AS RegionID
				, Ntl.RecruitID AS NationalRegionID
				
			FROM vw_RecruitUser AS Rep WITH(NOLOCK)
			--Skips Manager and Regional, goes straight to National Regional
			LEFT OUTER JOIN vw_RecruitUser AS Ntl WITH(NOLOCK)
			ON
				Rep.ReportsToID = Ntl.RecruitID
				AND (Ntl.ReportingLevel = 4)

			--National Regional's team
			LEFT OUTER JOIN RU_Teams AS RT WITH(NOLOCK)
			ON
				Ntl.TeamID = RT.TeamID
			
			WHERE
				Rep.ReportingLevel = 1
				
		) AS _1_4
		ON
			_1_2_3_4.RecruitID = _1_4.RecruitID
			
	) AS Reps


	-------------------------------------
	UNION ALL
	-------------------------------------
	
	
	SELECT 
	*
	FROM
	(
		SELECT
			
			_2_3_4.RecruitID
			, _2_3_4.SeasonID
			
			, COALESCE(
					_2_3_4.ManagerID
					, _2_4.ManagerID
					) AS ManagerID
			
			, COALESCE(
					_2_3_4.TeamID
					, _2_4.TeamID
					) AS TeamID
			, COALESCE(
					_2_3_4.RegionID
					, _2_4.RegionID
					) AS RegionID
			, COALESCE(
					_2_3_4.NationalRegionID
					, _2_4.NationalRegionID
					) AS NationalRegionID
			
		FROM
		(
			--Reporting Levels: 2, 3 and 4
			SELECT

				Man.RecruitID
				, Man.SeasonID
				
				, Reg.RecruitID AS ManagerID
				
				, RT.TeamID
				, Reg.RecruitID AS RegionID
				, Ntl.RecruitID AS NationalRegionID
				
			FROM vw_RecruitUser AS Man WITH(NOLOCK)
			LEFT OUTER JOIN vw_RecruitUser AS Reg WITH(NOLOCK)
			ON
				Man.ReportsToID = Reg.RecruitID
				AND (Reg.ReportingLevel = 3)
			LEFT OUTER JOIN vw_RecruitUser AS Ntl WITH(NOLOCK)
			ON
				Reg.ReportsToID = Ntl.RecruitID
				AND (Ntl.ReportingLevel = 4)

			--Manager's team
			LEFT OUTER JOIN RU_Teams AS RT WITH(NOLOCK)
			ON
				Man.TeamID = RT.TeamID
			
			WHERE
				Man.ReportingLevel = 2
				
		) AS _2_3_4
		
		LEFT OUTER JOIN
		(
			--Reporting Levels: 2 and 4
			SELECT

				Man.RecruitID
				, Man.SeasonID
				
				, NTL.RecruitID AS ManagerID
				
				, RT.TeamID
				, CAST(NULL AS INT) AS RegionID
				, Ntl.RecruitID AS NationalRegionID
				
			FROM vw_RecruitUser AS Man WITH(NOLOCK)
			--Skips Regional, goes straight to National Regional
			LEFT OUTER JOIN vw_RecruitUser AS Ntl WITH(NOLOCK)
			ON
				Man.ReportsToID = Ntl.RecruitID
				AND (Ntl.ReportingLevel = 4)

			--Manager's team
			LEFT OUTER JOIN RU_Teams AS RT WITH(NOLOCK)
			ON
				Man.TeamID = RT.TeamID
			
			WHERE
				Man.ReportingLevel = 2
				
		) AS _2_4
		ON
			_2_3_4.RecruitID = _2_4.RecruitID
				
	) AS Managers
	
	
	-------------------------------------
	UNION ALL
	-------------------------------------
	
			
	SELECT 
	*
	FROM
	(
			--Reporting Levels: 3 and 4
			SELECT

				Reg.RecruitID
				, Reg.SeasonID
				
				, Ntl.RecruitID AS ManagerID
				
				, RT.TeamID
				, Reg.RecruitID AS RegionID
				, Ntl.RecruitID AS NationalRegionID
				
			FROM vw_RecruitUser AS Reg WITH(NOLOCK)
			LEFT OUTER JOIN vw_RecruitUser AS Ntl WITH(NOLOCK)
			ON
				Reg.ReportsToID = Ntl.RecruitID
				AND (Ntl.ReportingLevel = 4)

			--Regional's team
			LEFT OUTER JOIN RU_Teams AS RT WITH(NOLOCK)
			ON
				Reg.TeamID = RT.TeamID
			
			WHERE
				Reg.ReportingLevel = 3
				
	) AS Regional
	
	
	-------------------------------------
	UNION ALL
	-------------------------------------
	
	
	SELECT 
	*
	FROM
	(
			--Reporting Levels: 4
			SELECT

				Ntl.RecruitID
				, Ntl.SeasonID
				
				, CAST(NULL AS INT) AS ManagerID
				
				, RT.TeamID
				, CAST(NULL AS INT) AS RegionID
				, Ntl.RecruitID AS NationalRegionID
				
			FROM vw_RecruitUser AS Ntl WITH(NOLOCK)

			--National Regional's team
			LEFT OUTER JOIN RU_Teams AS RT WITH(NOLOCK)
			ON
				Ntl.TeamID = RT.TeamID
			
			WHERE
				Ntl.ReportingLevel = 4
				
	) AS NationalRegional




