USE [WISE_CRM]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'custMS_MonitronicsEntitySiteSystemInfoSave')
	BEGIN
		PRINT 'Dropping Procedure custMS_MonitronicsEntitySiteSystemInfoSave'
		DROP  Procedure  dbo.custMS_MonitronicsEntitySiteSystemInfoSave
	END
GO

PRINT 'Creating Procedure custMS_MonitronicsEntitySiteSystemInfoSave'
GO
/******************************************************************************
**		File: custMS_MonitronicsEntitySiteSystemInfoSave.sql
**		Name: custMS_MonitronicsEntitySiteSystemInfoSave
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
**		Date: 02/13/2015
*******************************************************************************
**	Change History
*******************************************************************************
**	Date:		Author:			Description:
**	-----------	---------------	-----------------------------------------------
**	02/13/2015	Andres Sosa		Created By
**	
*******************************************************************************/
CREATE Procedure dbo.custMS_MonitronicsEntitySiteSystemInfoSave
(
	@IndustryAccountID BIGINT = NULL
	, @site_name VARCHAR(50)
	, @sitetype_id VARCHAR(50)
	, @sitestat_id VARCHAR(50)
	, @site_addr1 VARCHAR(50)
	, @site_addr2 VARCHAR(50)
	, @city_name VARCHAR(50)
	, @county_name VARCHAR(50)
	, @state_id VARCHAR(50)
	, @zip_code VARCHAR(50)
	, @phone1 VARCHAR(50)
	, @ext1 VARCHAR(50)
	, @street_no VARCHAR(50)
	, @street_name VARCHAR(50)
	, @country_name VARCHAR(50)
	, @timezone_no INT
	, @timezone_descr VARCHAR(50)
	, @servco_no INT
	, @install_servco_no VARCHAR(50)
	, @cspart_no VARCHAR(50)
	, @subdivision VARCHAR(50)
	, @cross_street VARCHAR(50)
	, @codeword1 VARCHAR(50)
	, @codeword2 VARCHAR(50)
	, @orig_install_date DATETIME
	, @lang_id VARCHAR(50)
	, @cs_no VARCHAR(50)
	, @systype_id VARCHAR(50)
	, @sec_systype_id VARCHAR(50)
	, @panel_phone VARCHAR(50)
	, @panel_location VARCHAR(50)
	, @receiver_phone VARCHAR(50)
	, @ati_hours SMALLINT
	, @ati_minutes TINYINT
	, @panel_code VARCHAR(50)
	, @twoway_device_id VARCHAR(50)
	, @alkup_cs_no VARCHAR(50)
	, @blkup_cs_no VARCHAR(50)
	, @ontest_flag VARCHAR(50)
	, @ontest_expire_date DATETIME
	, @oos_flag VARCHAR(50)
	, @install_date DATETIME
	, @monitor_type VARCHAR(50)
	, @GpEmployeeID VARCHAR(50)
)
AS
BEGIN
	/** SET NO COUNTING */
	SET NOCOUNT ON

	/** DECLARATIONS */
	
	BEGIN TRY
		BEGIN TRANSACTION;
		
		IF(EXISTS(SELECT * FROM [dbo].[MS_MonitronicsEntitySiteSystems] WHERE (IndustryAccountID = @IndustryAccountID)))
		BEGIN
			UPDATE [dbo].[MS_MonitronicsEntitySiteSystems] SET 
				site_name = @site_name
				, sitetype_id = @sitetype_id
				, sitestat_id = @sitestat_id
				, site_addr1 = @site_addr1
				, site_addr2 = @site_addr2
				, city_name = @city_name
				, county_name = @county_name
				, state_id = @state_id
				, zip_code = @zip_code
				, phone1 = @phone1
				, ext1 = @ext1
				, street_no = @street_no
				, street_name = @street_name
				, country_name = @country_name
				, timezone_no = @timezone_no
				, timezone_descr = @timezone_descr
				, servco_no = @servco_no
				, install_servco_no = @install_servco_no
				, cspart_no = @cspart_no
				, subdivision = @subdivision
				, cross_street = @cross_street
				, codeword1 = @codeword1
				, codeword2 = @codeword2
				, orig_install_date = @orig_install_date
				, lang_id = @lang_id
				, cs_no = @cs_no
				, systype_id = @systype_id
				, sec_systype_id = @sec_systype_id
				, panel_phone = @panel_phone
				, panel_location = @panel_location
				, receiver_phone = @receiver_phone
				, ati_hours = @ati_hours
				, ati_minutes = @ati_minutes
				, panel_code = @panel_code
				, twoway_device_id = @twoway_device_id
				, alkup_cs_no = @alkup_cs_no
				, blkup_cs_no = @blkup_cs_no
				, ontest_flag = @ontest_flag
				, ontest_expire_date = @ontest_expire_date
				, oos_flag = @oos_flag
				, install_date = @install_date
				, monitor_type = @monitor_type
				, ModifiedBy = @GpEmployeeID
				, ModifiedOn = GETUTCDATE()
			WHERE
				(IndustryAccountID = @IndustryAccountID);
		END
		ELSE
		BEGIN
			INSERT INTO [dbo].[MS_MonitronicsEntitySiteSystems] (
				[IndustryAccountID]
				,[site_name]
				,[sitetype_id]
				,[sitestat_id]
				,[site_addr1]
				,[site_addr2]
				,[city_name]
				,[county_name]
				,[state_id]
				,[zip_code]
				,[phone1]
				,[ext1]
				,[street_no]
				,[street_name]
				,[country_name]
				,[timezone_no]
				,[timezone_descr]
				,[servco_no]
				,[install_servco_no]
				,[cspart_no]
				,[subdivision]
				,[cross_street]
				,[codeword1]
				,[codeword2]
				,[orig_install_date]
				,[lang_id]
				,[cs_no]
				,[systype_id]
				,[sec_systype_id]
				,[panel_phone]
				,[panel_location]
				,[receiver_phone]
				,[ati_hours]
				,[ati_minutes]
				,[panel_code]
				,[twoway_device_id]
				,[alkup_cs_no]
				,[blkup_cs_no]
				,[ontest_flag]
				,[ontest_expire_date]
				,[oos_flag]
				,[install_date]
				,[monitor_type]
				,[ModifiedBy]
				,[CreatedBy]
			) VALUES (
				@IndustryAccountID
				, @site_name
				, @sitetype_id
				, @sitestat_id
				, @site_addr1
				, @site_addr2
				, @city_name
				, @county_name
				, @state_id
				, @zip_code
				, @phone1
				, @ext1
				, @street_no
				, @street_name
				, @country_name
				, @timezone_no
				, @timezone_descr
				, @servco_no
				, @install_servco_no
				, @cspart_no
				, @subdivision
				, @cross_street
				, @codeword1
				, @codeword2
				, @orig_install_date
				, @lang_id
				, @cs_no
				, @systype_id
				, @sec_systype_id
				, @panel_phone
				, @panel_location
				, @receiver_phone
				, @ati_hours
				, @ati_minutes
				, @panel_code
				, @twoway_device_id
				, @alkup_cs_no
				, @blkup_cs_no
				, @ontest_flag
				, @ontest_expire_date
				, @oos_flag
				, @install_date
				, @monitor_type
				, @GpEmployeeID
				, @GpEmployeeID
			);

		END
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		EXEC dbo.wiseSP_ExceptionsThrown;
		RETURN;
	END CATCH

	SELECT * FROM [dbo].[MS_MonitronicsEntitySiteSystems] WHERE (IndustryAccountID = @IndustryAccountID);
END
GO

GRANT EXEC ON dbo.custMS_MonitronicsEntitySiteSystemInfoSave TO PUBLIC
GO

/** EXEC dbo.custMS_MonitronicsEntitySiteSystemInfoSave */