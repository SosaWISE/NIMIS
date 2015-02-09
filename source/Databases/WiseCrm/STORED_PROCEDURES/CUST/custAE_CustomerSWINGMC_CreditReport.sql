USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custAE_CustomerSWINGMC_CreditReport')
	BEGIN
		PRINT 'Dropping Procedure custAE_CustomerSWINGMC_CreditReport'
		DROP  Procedure  dbo.custAE_CustomerSWINGMC_CreditReport
	END
GO

PRINT 'Creating Procedure custAE_CustomerSWINGMC_CreditReport'
GO
/******************************************************************************
**		File: custAE_CustomerSWINGMC_CreditReport.sql
**		Name: custAE_CustomerSWINGMC_CreditReport
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
**		Auth: Andres Sosa
**		Date: 03/28/2014
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	03/28/2014	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custAE_CustomerSWINGMC_CreditReport
(
	@InterimAccountID BIGINT,
	@LeadID BIGINT,
	@AddressID BIGINT
	
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
	
			DECLARE @CreditReportID BIGINT

			/*step 6. Move Credit Reports
			*/
	
			-- 6.1 	WCM.QL_CreditReports
			  INSERT INTO [Wise_CRM].[dbo].[QL_CreditReports]
					(
						   [LeadId]
						  ,[AddressId]
						  ,[BureauId]
						  ,[SeasonId]
						  ,[CreditReportVendorId]
						  ,[CreditReportVendorAbaraId]
						  ,[CreditReportVendorMicrobiltId]
						  ,[CreditReportVendorEasyAccessId]
						  ,[CreditReportVendorManualId]
						  ,[Prefix]
						  ,[FirstName]
						  ,[MiddleName]
						  ,[LastName]
						  ,[Suffix]
						  ,[SSN]
						  ,[DOB]
						  ,[Score]
						  ,[IsScored]
						  ,[IsHit]
					)

					SELECT 
						  @LeadID, -- not sure on this part 
						  @AddressID,  -- not sure on this part 
						  --QL_CreditReportBureaus.BureauID, not all data matched apply case
						  CASE
							WHEN MC_CreditReport.[BureauID] = 1 THEN 'MN'
							WHEN MC_CreditReport.[BureauID] = 2 THEN 'TU'
							WHEN MC_CreditReport.[BureauID] = 3 THEN 'EQ'
							WHEN MC_CreditReport.[BureauID] = 4 THEN 'EX'
						  END [BureauId], 
						  0, --use 0 for SeasonId
						  QL_CreditReportVendors.CreditReportVendorID,
						  NULL, --	  QL_CreditReportAbara.CreditReportVendorAbaraId, --not sure with this -- we plan to update this credit after CreditReportVendorAbara was generated
						  NULL, --	  QL_CreditReportMicrobilt.CreditReportVendorMicrobiltId, -not sure with this -- we plan to update this credit after CreditReportVendorMicrobilt was generated
						  NULL, --never used
						  NULL, --never used
						  MC_CreditReport.Prefix,
						  MC_CreditReport.FirstName,
						  MC_CreditReport.MiddleName,
						  MC_CreditReport.LastName,
						  MC_CreditReport.Suffix,
						  MC_CreditReport.SSN,
						  MC_CreditReport.DOB,
						  MC_CreditReport.Score,
						  CASE										-- IsScored -- not sure with this
							WHEN   MC_CreditReport.Score = 0 THEN 0
							ELSE 1
						  END  IsScored,
						  0  IsHit -- IsHit -- not sure with this - not clearly defined in the documentation
					FROM
					[Platinum_Protection_InterimCRM].[dbo].[MC_CreditReport]
					INNER JOIN
					[Platinum_Protection_InterimCRM].[dbo].[MS_Account]
					ON
					[MC_CreditReport].[LeadID]= [MS_Account].[Customer1ID]

					--INNER JOIN
					--[WISE_CRM].[dbo].[QL_CreditReportBureaus]
					--ON
					--[MC_CreditReport].BureauName = [QL_CreditReportBureaus].BureauName   -- commented since not all BureauName are equal

					INNER JOIN
					[WISE_CRM].[dbo].[QL_CreditReportVendors]
					ON
					[QL_CreditReportVendors].[CreditReportVendorID] = [MC_CreditReport].[CreditReportVendorID]
		
					WHERE 
					[MS_Account].[AccountID] =  @InterimAccountID

					SET @CreditReportID = SCOPE_IDENTITY()
					
					--PRINT CAST (@CreditReportID AS VARCHAR(10)) + '- CreditReportID'
					

			-- 6.2 	WCM.QL_CreditReportVendorMicrobilt   -- not sure if this the correct query here
					
				INSERT INTO [WISE_CRM].[dbo].[QL_CreditReportVendorMicrobilt]
				(
					[BureauId],
					[Score],
					[IsScored],
					[IsHit],
					[CreditReport],
					[MicroBiltGUID]
				)
				SELECT 
					--  QL_CreditReportBureaus.BureauID,
					CASE
							WHEN MC_CreditReport.[BureauID] = 1 THEN 'MN'
							WHEN MC_CreditReport.[BureauID] = 2 THEN 'TU'
							WHEN MC_CreditReport.[BureauID] = 3 THEN 'EQ'
							WHEN MC_CreditReport.[BureauID] = 4 THEN 'EX'
						  END [BureauId], 

						MC_CreditReport.Score,
						CASE										-- IsScored -- not sure with this
							WHEN   MC_CreditReport.Score = 0 THEN 0
							ELSE 1
						END  IsScored,
						0  IsHit, -- IsHit -- not sure with this - not clearly defined in the documentation
						'This is the actual credit report.',
						MC_CreditReport.MicroBiltGUID

				FROM
				[Platinum_Protection_InterimCRM].[dbo].[MC_CreditReport]
				INNER JOIN
				[Platinum_Protection_InterimCRM].[dbo].[MS_Account]
				ON
				[MC_CreditReport].[LeadID]= [MS_Account].[Customer1ID]

				--INNER JOIN
				--[WISE_CRM].[dbo].[QL_CreditReportBureaus]
				--ON
				--[MC_CreditReport].BureauName = [QL_CreditReportBureaus].BureauName   -- commented since not all BureauName are equal
				WHERE 
				[MS_Account].[AccountID] =  @InterimAccountID -- not sure if this is only the filter

				--SET @CreditReportVendorMicrobiltID = SCOPE_IDENTITY()  --- not sure if it is needed
		

			-- 6.3 	[QL_CreditReportVendorAbara]
				INSERT INTO [WISE_CRM].[dbo].[QL_CreditReportVendorAbara]
				(
					 [CreditReportId]
					,[BureauId]
					,[ReportID]
					,[ReportGuid]
					,[Result]
					,[Score]
					,[IsScored]
					,[IsHit]
					,[ReportHtml]
					,[ReportXML]
					,[ErrorMessage]
					,[HitStatus]
					,[DecisionCode]
					,[DecisionText]
	
				)
				SELECT 
					@CreditReportID,
				--  QL_CreditReportBureaus.BureauID,
					CASE
							WHEN [MC_CreditReportAbara].[BureauID] = 1 THEN 'MN'
							WHEN [MC_CreditReportAbara].[BureauID] = 2 THEN 'TU'
							WHEN [MC_CreditReportAbara].[BureauID] = 3 THEN 'EQ'
							WHEN [MC_CreditReportAbara].[BureauID] = 4 THEN 'EX'
						  END [BureauId], 
					[MC_CreditReportAbara].[ReportID],
					[MC_CreditReportAbara].[ReportGuid],
					[MC_CreditReportAbara].[Result],
					[MC_CreditReportAbara].[Score],
					[MC_CreditReportAbara].IsScored,
					[MC_CreditReportAbara].IsHit, 
					[MC_CreditReportAbara].[ReportHtml],
					[MC_CreditReportAbara].[ReportXML],
					[MC_CreditReportAbara].[ErrorMessage],
					[MC_CreditReportAbara].[HitStatus],
					[MC_CreditReportAbara].[DecisionCode],
					[MC_CreditReportAbara].[DecisionText]
					--[MS_Account].*, --Temp
					--[MC_CreditReport].*, --Temp
					--[MC_CreditReportAbara].* -- Temp
				FROM
				[Platinum_Protection_InterimCRM].[dbo].[MC_CreditReport]
				INNER JOIN
				[Platinum_Protection_InterimCRM].[dbo].[MS_Account]
				ON
				[MC_CreditReport].[LeadID]= [MS_Account].[Customer1ID]
				INNER JOIN
				[Platinum_Protection_InterimCRM].[dbo].[MC_CreditReportAbara]
				ON
				[MC_CreditReportAbara].[CreditReportId] = [MC_CreditReport].CreditReportID

				--INNER JOIN
				--[WISE_CRM].[dbo].[QL_CreditReportBureaus]
				--ON
				--[MC_CreditReport].BureauName = [QL_CreditReportBureaus].BureauName   -- commented since not all BureauName are equal
				WHERE 
				[MS_Account].[AccountID] = @InterimAccountID  --- not sure if it is needed




	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH
END
GO

GRANT EXEC ON dbo.custAE_CustomerSWINGMC_CreditReport TO PUBLIC
GO

/** EXEC dbo.SPROC_NAME */