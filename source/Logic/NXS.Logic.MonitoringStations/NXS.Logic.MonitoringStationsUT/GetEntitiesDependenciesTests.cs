using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.Logic.MonitoringStations;
using NXS.Logic.MonitoringStations.Helpers;
using NXS.Logic.MonitoringStations.Schemas;

namespace NXS.Logic.MonitoringStationsUT
{
	[TestClass]
	public class GetEntitiesDependenciesTests
	{
		#region Properteis

		private const string _USERID = "wsi_828070003";
		private const string _PASWRD = "password";
		private const string _CSNO_DG_PRIMARY = "768247002";

		#endregion Properteis

		#region Test Methods

		/** Contacts */
		[TestMethod]
		public void TestGetContacts()
		{
			#region Get Contacts

			var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;
			const string CS_NO = _CSNO_DG_PRIMARY;
			Errors dsErrorsGet;
			string firstErrorMsgGet;
			Assert.IsTrue(moniService.GetDataTry("Contacts", out dsRaw, out dsErrorsGet, out firstErrorMsgGet, CS_NO));
			if (dsRaw != null)
			{
				var ds = Utils.ConvertDataSet<GetContacts>(dsRaw);
				Assert.IsNotNull(ds, "DataSet conversion did not work for Contacts");

				Errors dsErrors;
				string firstErrorMsg;
				Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg), string.Format("Error on getting Contacts: {0}", firstErrorMsg));
				foreach (GetContacts.TableRow row in ds.Table.Rows)
				{
					/** Have to check for DBNull's */
					Console.WriteLine("CsNo: {0} | Prefix: {1} | FirstName: {2} | MiddleName: {3} | LastName: {4} | Suffix: {5} | cTacTypeId: {6} | CsSeqno: {7} | AuthId: {8} | RelationId: {9} | Pin: {10} | HasKeyFlag: {11} | ContractSignerFlag: {12} | EndDate: {13} | Phone1: {14} | PhoneTypeId1: {15} | PhoneExt1: {16} | PhoneSeqNo1: {17} | Phone2: {18} | PhoneTypeId2: {19} | PhoneExt2: {20} | PhoneSeqNo2: {21} | Phone3: {22} | PhoneTypeId3: {23} | PhoneExt3: {24} | PhoneSeqNo3: {25} | Phone4: {26} | PhoneTypeId4: {27} | PhoneExt4: {28} | PhoneSeqNo4: {29} | EmailAddress: {30} | AutoNotifyFlag: {31} | ContlTypeNo: {32} | UserId: {33} | ContactNo: {34} | CTacLinkNo: {35}",
						!row.Iscs_noNull() ? row.cs_no : null,
						!row.IsprefixNull() ? row.prefix : null,
						!row.Isfirst_nameNull() ? row.first_name : null,
						!row.Ismiddle_initialNull() ? row.middle_initial : null,
						!row.Islast_nameNull() ? row.last_name : null,
						!row.IssuffixNull() ? row.suffix : null,
						!row.Isctactype_idNull() ? row.ctactype_id : null,
						!row.Iscs_seqnoNull() ? (int?) row.cs_seqno : null,
						!row.Isauth_idNull() ? row.auth_id : null,
						!row.Isrelation_idNull() ? row.relation_id : null,
						!row.IspinNull() ? row.pin : null,
						!row.Ishas_key_flagNull() ? row.has_key_flag : null,
						!row.Iscontract_signer_flagNull() ? row.contract_signer_flag : null,
						!row.Isend_dateNull() ? (DateTime?)row.end_date : null,
						!row.Isphone1Null() ? row.phone1 : null,
						!row.Isphonetype_id1Null() ? row.phonetype_id1 : null,
						!row.Isphone_ext1Null() ? row.phone_ext1 : null,
						!row.Isphone_seqno1Null() ? (Byte?)row.phone_seqno1 : null,
						!row.Isphone2Null() ? row.phone2 : null,
						!row.Isphonetype_id2Null() ? row.phonetype_id2 : null,
						!row.Isphone_ext2Null() ? row.phone_ext2 : null,
						!row.Isphone_seqno2Null() ? (Byte?)row.phone_seqno2 : null,
						!row.Isphone3Null() ? row.phone3 : null,
						!row.Isphonetype_id3Null() ? row.phonetype_id3 : null,
						!row.Isphone_ext3Null() ? row.phone_ext3 : null,
						!row.Isphone_seqno3Null() ? (Byte?)row.phone_seqno3 : null,
						!row.Isphone4Null() ? row.phone4 : null,
						!row.Isphonetype_id4Null() ? row.phonetype_id4 : null,
						!row.Isphone_ext4Null() ? row.phone_ext4 : null,
						!row.Isphone_seqno4Null() ? (Byte?)row.phone_seqno4 : null,
						!row.Isemail_addressNull() ? row.email_address : null,
						!row.Isauto_notify_flagNull() ? row.auto_notify_flag : null,
						!row.Iscontltype_noNull() ? (int?) row.contltype_no : null,
						!row.Isuser_idNull() ? row.user_id : null,
						!row.Iscontact_noNull() ? (int?)row.contact_no : null,
						!row.Isctaclink_noNull() ? (int?)row.ctaclink_no : null);
				}
			}

			#endregion Get Contacts
		}
       
        /** EventHistories */
        [TestMethod]
        public void TestGetEventHistories()
        {
            #region Get EventHistories

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            const string CS_NO = _CSNO_DG_PRIMARY;
			Errors dsErrorsGet;
			string firstErrorMsgGet;
			Assert.IsTrue(moniService.GetDataTry("EventHistories", out dsRaw, out dsErrorsGet, out firstErrorMsgGet, CS_NO));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetEventHistories>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Contacts");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg), string.Format("Error on getting Contacts: {0}", firstErrorMsg));
	            var count = 0;
				foreach (GetEventHistories.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("CsNo: {0} | EventDate: {1} | ZoneId: {2} | Area: {3} | EventId: {4} | UserName: {5} | ZoneStateId: {6} | Match: {7} | Computed: {8}",
                        !row.Iscs_noNull() ? row.cs_no : null,
						!row.Isevent_dateNull() ? (DateTime?)row.event_date : null,
						!row.Iszone_idNull() ? row.zone_id : null,
						!row.IsareaNull() ? row.area : null,
						!row.Isevent_idNull() ? row.event_id : null,
						!row.Isuser_nameNull() ? row.user_name : null,
						!row.Iszonestate_idNull() ? row.zonestate_id : null,
						!row.IsmatchNull() ? row.match : null,
						!row.IscomputedNull() ? row.computed : null);
	                count++;
                }

				Console.WriteLine("RowCount: {0}", count);
            }

            #endregion Get EventHistories
        }

        /** SiteAgencyPermits */
        [TestMethod]
        public void TestGetSiteAgencyPermits()
        {
            #region Get SiteAgencyPermits

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            const string CS_NO = _CSNO_DG_PRIMARY;
			Errors dsErrorsGet;
			string firstErrorMsgGet;
			Assert.IsTrue(moniService.GetDataTry("SiteAgencyPermits", out dsRaw, out dsErrorsGet, out firstErrorMsgGet, CS_NO));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetSiteAgencyPermits>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for SiteSystems");

                Errors dsErrors;
                string firstErrorMsg;
	            var count = 0;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg), string.Format("Error on getting SiteSystems: {0}", firstErrorMsg));
				foreach (GetSiteAgencyPermits.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
					Console.WriteLine("CsNo: {0} | AgencyNo: {1} | AgencyTypeId: {2} | AgencyName: {3} | Phone1: {4} | PermitNo: {5} | PermitTypeId: {6} | EffectiveDate: {7} | ExpireDate: {8}",
                        !row.Iscs_noNull() ? row.cs_no : null,
                        !row.Isagency_noNull() ? (int?) row.agency_no: null,
						!row.Isagencytype_idNull() ? row.agencytype_id : null,
						!row.Isagency_nameNull() ? row.agency_name : null,
						!row.Isphone1Null() ? row.phone1 : null,
						!row.Ispermit_noNull() ? row.permit_no : null,
						!row.Ispermtype_idNull() ? row.permtype_id : null,
						!row.Iseffective_dateNull() ? (DateTime?) row.effective_date : null,
						!row.Isexpire_dateNull() ? (DateTime?) row.expire_date : null);
	                count++;
                }
				Console.WriteLine("SiteAgencyPermits Count: {0}", count);
            }

            #endregion Get SiteAgencyPermits
        }

        /** SiteDispatches */
        [TestMethod]
        public void TestGetSiteDispatches()
        {
            #region Get SiteDispatches

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            const string CS_NO = _CSNO_DG_PRIMARY;
	        var count = 0;
			Errors dsErrorsGet;
			string firstErrorMsgGet;
			Assert.IsTrue(moniService.GetDataTry("SiteDispatches", out dsRaw, out dsErrorsGet, out firstErrorMsgGet, CS_NO));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetSiteDispatches>(dsRaw);
				Assert.IsNotNull(ds, "DataSet conversion did not work for SiteDispatches");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg), string.Format("Error on getting SiteDispatches: {0}", firstErrorMsg));
				foreach (GetSiteDispatches.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
					Console.WriteLine("CsNo: {0} | DisPageNo: {1} | EffectiveDate: {2} | ExpireDate: {3} | Instructions: {4} | WeekDays: {5} | StartTime: {6} | EndTime: {7} | OnlyExcept: {8}",
                        !row.Iscs_noNull() ? row.cs_no : null,
                        !row.Isdispage_noNull() ? (short?) row.dispage_no : null,
						!row.Iseffective_dateNull() ? (DateTime?) row.effective_date : null,
						!row.Isexpire_dateNull() ? (DateTime?) row.expire_date : null,
						!row.IsinstructionsNull() ? row.instructions : null,
						!row.IsweekdaysNull() ? row.weekdays : null,
						!row.Isstart_timeNull() ? (short?) row.start_time : null,
						!row.Isend_timeNull() ? (short?) row.end_time : null,
						!row.Isonly_exceptNull() ? row.only_except : null);
	                count++;
                }
				Console.WriteLine("SiteAgencyPermits Count: {0}", count);
            }

            #endregion Get SiteDispatches
        }

        /** SiteErrors */
        [TestMethod]//needs XML
        public void TestGetSiteErrors()
        {
            #region Get SiteErrors

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            const string CS_NO = _CSNO_DG_PRIMARY;
			Errors dsErrorsGet;
			string firstErrorMsgGet;
			Assert.IsTrue(moniService.GetDataTry("SiteErrors", out dsRaw, out dsErrorsGet, out firstErrorMsgGet, CS_NO));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetSiteErrors>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for SiteSystems");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg), string.Format("Error on getting SiteSystems: {0}", firstErrorMsg));
                foreach (GetSiteErrors.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("TableName: {0} | EntryId: {1} | SiteNo: {2} | CsNo: {3} | ErrNo: {4} | MsgType: {5} | ErrText: {6} | ErrDate: {7}",
                        !row.Istable_nameNull() ? row.table_name : null,
                        !row.Isentry_idNull() ? row.entry_id : null,
                        !row.Issite_noNull() ? (int?) row.site_no : null,
                        !row.Iscs_noNull() ? row.cs_no : null,
                        !row.Iserr_noNull() ? (int?) row.err_no : null,
                        !row.Ismsg_typeNull() ? (Byte?) row.msg_type : null,
                        !row.Iserr_textNull() ? row.err_text : null,
                        !row.Iserr_dateNull() ? (DateTime?) row.err_date: null);
                }
            }

            #endregion Get SiteErrors
        } 
 
        /** SiteGeneralDispatches */
        [TestMethod]
        public void TestGetSiteGeneralDispatches()
        {
            #region Get SiteGeneralDispatches

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            const string CS_NO = _CSNO_DG_PRIMARY;
			Errors dsErrorsGet;
			string firstErrorMsgGet;
			Assert.IsTrue(moniService.GetDataTry("SiteGeneralDispatches", out dsRaw, out dsErrorsGet, out firstErrorMsgGet, CS_NO));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetSiteGeneralDispatches>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for SiteGeneralDispatches");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg), string.Format("Error on getting SiteSystems: {0}", firstErrorMsg));
				foreach (GetSiteGeneralDispatches.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
					Console.WriteLine("CsNo: {0} | EffectiveDate: {1} | ExpireDate: {2} | Instructions: {3} | WeekDays: {4} | StartTime: {5} | EndTime: {6} | OnlyExcept: {7}",
                        !row.Iscs_noNull() ? row.cs_no: null,
                        !row.Iseffective_dateNull() ? (DateTime?) row.effective_date : null,
						!row.IsinstructionsNull() ? row.instructions : null,
						!row.Iseffective_dateNull() ? (DateTime?) row.effective_date : null,
						!row.IsweekdaysNull() ? row.weekdays: null,
						!row.Isstart_timeNull() ? (short?) row.start_time: null,
						!row.Isend_timeNull() ? (short?) row.end_time: null,
						!row.Isonly_exceptNull() ? row.only_except : null);
                }
            }

            #endregion Get SiteGeneralDispatches
        }

         /** SiteNotes */
        [TestMethod]
        public void TestGetSiteNotes()
        {
            #region Get SiteNotes

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            const string CS_NO = _CSNO_DG_PRIMARY;
			Errors dsErrorsGet;
			string firstErrorMsgGet;
			Assert.IsTrue(moniService.GetDataTry("SiteNotes", out dsRaw, out dsErrorsGet, out firstErrorMsgGet, CS_NO));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetSiteNotes>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for SiteSystems");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg), string.Format("Error on getting SiteSystems: {0}", firstErrorMsg));
                foreach (GetSiteNotes.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine(
                        "CsNo: {0} | SeqNo: {1} | StartDate: {2} | EndDate: {3} | Note: {4} | TextType: {5} | Color: {6}",
                        !row.Iscs_noNull() ? row.cs_no : null,
                        !row.IsseqnoNull() ? (short?) row.seqno : null,
                        !row.Isstart_dateNull() ? (DateTime?) row.start_date : null, 
                        !row.Isend_dateNull() ? (DateTime?)row.end_date: null,
                        !row.IsnoteNull() ? row.note: null,
                        !row.Istext_typeNull() ? (byte?)row.text_type: null,
                        !row.IscolorNull() ? row.color: null);
                }
            }

            #endregion Get SiteNotes
        }
     
        /** SiteSystems */
		[TestMethod]
		public void TestGetSiteSystems()
		{
			#region Get SiteSystems

			var moniService = new Monitronics(_USERID, _PASWRD);
			DataSet dsRaw;
			const string CS_NO = _CSNO_DG_PRIMARY;
			Errors dsErrorsGet;
			string firstErrorMsgGet;
			Assert.IsTrue(moniService.GetDataTry("SiteSystems", out dsRaw, out dsErrorsGet, out firstErrorMsgGet, CS_NO));
			if (dsRaw != null)
			{
				var ds = Utils.ConvertDataSet<GetSiteSystemInfo>(dsRaw);
				Assert.IsNotNull(ds, "DataSet conversion did not work for SiteSystems");

				Errors dsErrors;
				string firstErrorMsg;
				Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg), string.Format("Error on getting SiteSystems: {0}", firstErrorMsg));
				foreach (GetSiteSystemInfo.TableRow row in ds.Table.Rows)
				{
					/** Have to check for DBNull's */
					Console.WriteLine("SiteName: {0} | SiteTypeId: {1} | SiteStatId: {2} | SiteAddr1: {3} | SiteAddr2: {4} | CityName: {5} | CountyName: {6} | StateId: {7} | ZipCode: {8} | Phone1: {9} | Ext1: {10} | StreetNo: {11} | StreetName: {12} | CountryName: {13} | TimeZoneNo: {14} | TimeZoneDesc: {15} | ServcoNo: {16} | InstallServcoNo: {17} | CsPartNo: {18} | SubDivision: {19} | CrossStreet: {20} | CodeWord1: {21} | CodeWord2: {22} | OrigInstallDate: {23} | LangId: {24} | CsNo: {25} | SysTypeId: {26} | SecSysTypeId: {27} | PanelPhone: {28} | PanelLocation: {29} | ReceiverPhone: {30} | AtiHours: {31} | AtiMinutes: {32} | PanelCode: {33} | TwoWayDeviceId: {34} | AlkupCsNo: {35} | BlkupCsNo: {36} | OnTestFlag: {37} | OnTestExpireDate: {38} | OosFlag: {39} | InstallDate: {40} | MonitorType: {41}",
						!row.Issite_nameNull() ? row.site_name : null,
						!row.Issitetype_idNull() ? row.sitetype_id : null,
						!row.Issitestat_idNull() ? row.sitestat_id : null,
						!row.Issite_addr1Null() ? row.site_addr1 : null,
						!row.Issite_addr2Null() ? row.site_addr2 : null,
						!row.Iscity_nameNull() ? row.city_name : null,
						!row.Iscounty_nameNull() ? row.county_name : null,
						!row.Isstate_idNull() ? row.state_id : null,
						!row.Iszip_codeNull() ? row.zip_code : null,
						!row.Isphone1Null() ? row.phone1 : null,
						!row.Isext1Null() ? row.ext1 : null,
						!row.Isstreet_noNull() ? row.street_no : null,
						!row.Isstreet_nameNull() ? row.street_name : null,
						!row.Iscountry_nameNull() ? row.country_name : null,
						!row.Istimezone_noNull() ? (int?) row.timezone_no : null,
						!row.Istimezone_descrNull() ? row.timezone_descr : null,
						!row.Isservco_noNull() ? (int?) row.servco_no : null,
						!row.Isinstall_servco_noNull() ? row.install_servco_no : null,
						!row.Iscspart_noNull() ? row.cspart_no : null,
						!row.IssubdivisionNull() ? row.subdivision : null,
						!row.Iscross_streetNull() ? row.cross_street : null,
						!row.Iscodeword1Null() ? row.codeword1 : null,
						!row.Iscodeword2Null() ? row.codeword2 : null,
						!row.Isorig_install_dateNull() ? (DateTime?) row.orig_install_date : null,
						!row.Islang_idNull() ? row.lang_id : null,
						!row.Iscs_noNull() ? row.cs_no : null,
						!row.Issystype_idNull() ? row.systype_id : null,
						!row.Issec_systype_idNull() ? row.sec_systype_id : null,
						!row.Ispanel_phoneNull() ? row.panel_phone : null,
						!row.Ispanel_locationNull() ? row.panel_location : null,
						!row.Isreceiver_phoneNull() ? row.receiver_phone : null,
						!row.Isati_hoursNull() ? (short?) row.ati_hours : null,
						!row.Isati_minutesNull() ? (byte?) row.ati_minutes : null,
						!row.Ispanel_codeNull() ? row.panel_code : null,
						!row.Istwoway_device_idNull() ? row.twoway_device_id : null,
						!row.Isalkup_cs_noNull() ? row.alkup_cs_no : null,
						!row.Isblkup_cs_noNull() ? row.blkup_cs_no : null,
						!row.Isontest_flagNull() ? row.ontest_flag : null,
						!row.Isontest_expire_dateNull() ? (DateTime?) row.ontest_expire_date : null,
						!row.Isoos_flagNull() ? row.oos_flag : null,
						!row.Isinstall_dateNull() ? (DateTime?) row.install_date : null,
						!row.Ismonitor_typeNull() ? row.monitor_type : null);
				}
			}

			#endregion Get SiteSystems
		}

        /** Zones */
        [TestMethod]
        public void TestGetZones()
        {
            #region Get Zones

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            const string CS_NO = _CSNO_DG_PRIMARY;
			Errors dsErrorsGet;
			string firstErrorMsgGet;
			Assert.IsTrue(moniService.GetDataTry("Zones", out dsRaw, out dsErrorsGet, out firstErrorMsgGet, CS_NO));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetZones>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for SiteSystems");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg), string.Format("Error on getting SiteSystems: {0}", firstErrorMsg));
                foreach (GetZones.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine(
                        "CsNo: {0} | ZoneId: {1} | ZoneStateId: {2} | EventId: {3} | AlarmGrpNo: {4} | EquipTypeId: {5} | EquipLocId: {6} | ZoneComment: {7}",
                        !row.Iscs_noNull() ? row.cs_no : null,
                        !row.Iszone_idNull() ? row.zone_id : null,
                        !row.Iszonestate_idNull() ? row.zonestate_id : null,
                        !row.Isevent_idNull() ? row.event_id : null,
                        !row.Isalarmgrp_noNull() ? (short?)row.alarmgrp_no : null,
                        !row.Isequiptype_idNull() ? row.equiptype_id : null,
                        !row.Isequiploc_idNull() ? row.equiploc_id : null,
                        !row.Iszone_commentNull() ? row.zone_comment : null);
                }
            }

            #endregion Zones
        }

        /** SiteSystemOptions*/
        [TestMethod]
        public void TestGetSiteSystemOptions()
        {
            #region Get SiteSystemOptions

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            const string CS_NO = _CSNO_DG_PRIMARY;
            Errors dsErrorsGet;
            string firstErrorMsgGet;
            Assert.IsTrue(moniService.GetDataTry("SiteSystemOptions", out dsRaw, out dsErrorsGet, out firstErrorMsgGet, CS_NO));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetSiteSystemOptions>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for SecGroups");

                Errors dsErrors;
                string firstErrorMsg;
                var count = 0;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting SecGroups: {0}", firstErrorMsg));
                foreach (GetSiteSystemOptions.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("CsNumber: {0} | OptionId: {1} | OptionValue: {2}",
                        !row.Iscs_noNull() ? row.cs_no : null,
                        !row.Isoption_idNull()? row.option_id: null,
                        !row.Isoption_valueNull()?row.option_value : null);
                    count++;
                }
                Console.WriteLine("Total Number: {0}", count);
            }

            #endregion Get SiteSystemOptions
        }

        /** SiteOptions */

        [TestMethod]
        public void TestGetSiteOptions()
        {
            #region Get SiteOptions

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            const string CS_NO = _CSNO_DG_PRIMARY;
            Errors dsErrorsGet;
            string firstErrorMsgGet;
            Assert.IsTrue(moniService.GetDataTry("SiteOptions", out dsRaw, out dsErrorsGet, out firstErrorMsgGet, CS_NO));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetSiteOptions>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Zone States");

                Errors dsErrors;
                string firstErrorMsg;
                var count = 0;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Zone States: {0}", firstErrorMsg));
                foreach (GetSiteOptions.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("CsNumber: {0} | OptionId: {1} | OptionValue: {2}",
                        !row.Iscs_noNull() ? row.cs_no : null,
                        !row.Isoption_idNull() ? row.option_id : null,
                        !row.Isoption_valueNull() ? row.option_value : null);
                    count++;
                }
                Console.WriteLine("Total Number: {0}", count);
            }

            #endregion Get SiteOptions
        }

		#endregion Test Methods
	}
}