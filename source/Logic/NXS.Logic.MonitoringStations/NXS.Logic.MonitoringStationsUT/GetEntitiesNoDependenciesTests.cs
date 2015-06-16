using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.Logic.MonitoringStations;
using NXS.Logic.MonitoringStations.Helpers;
using NXS.Logic.MonitoringStations.Models.Get;
using NXS.Logic.MonitoringStations.Schemas;
using SOS.Data.SosCrm;
using GetAgencies = NXS.Logic.MonitoringStations.Models.Get.GetAgencies;
using GetPrefixes = NXS.Logic.MonitoringStations.Models.Get.GetPrefixes;


namespace NXS.Logic.MonitoringStationsUT
{
    [TestClass]
    public class GetEntitiesNoDependenciesTests
    {
        #region Properteis
		/** TEST ENVIRONMENT
        private const string _USERID = "wsi_828070003";
        private const string _PASWRD = "password";
		*/
		private const string _USERID = "nexsense_wsi";
		private const string _PASWRD = "nexsense_wsi";

        #endregion Properteis

        #region Test Methods

    /* Agencies */
		#region AgenciesMethod
		[TestMethod]
        public void TestGetAgency()
        {
            
            //SosCrmDataContext.Instance.MS_MonitronicsEntityAgencies.LoadCollection(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityAgenciesNuke());

            var moniService = new Monitronics(_USERID, _PASWRD);
            var getAgencies = new GetAgencies
            {
                GetAgency = new AgencyInfo
                {
                    ZipCode = "84097"
                }
            };
            var serialized = getAgencies.Serialize();
            DataSet dsAgenciesRaw;
            Errors dsErrorsGet;
            string firstErrorMsgGet;
            Assert.IsTrue(moniService.GetDataTry(MS_MonitronicsEntity.MetaData.AgenciesID, out dsAgenciesRaw,
                out dsErrorsGet, out firstErrorMsgGet, null, serialized));
            if (dsAgenciesRaw != null)
            {
                var dsAgencyTypes = Utils.ConvertDataSet<MonitoringStations.Schemas.GetAgencies>(dsAgenciesRaw);
                Assert.IsNotNull(dsAgencyTypes, "DataSet conversion did not work for GetAgencyTypes");

                foreach (MonitoringStations.Schemas.GetAgencies.TableRow row in dsAgencyTypes.Table.Rows)
                {
                    Console.WriteLine(
                        "AgencyNumber: {0} | AgencyTypeId: {1} | AgencyName:{2} | CityName:{3} | StateId:{4} | ZipCode:{5} | Phone1:{6}",
                        row.Isagency_noNull() ? null : row.agency_no,
                        row.Isagencytype_idNull() ? null : row.agencytype_id,
                        row.Isagency_nameNull()? null: row.agency_name,
                        row.Iscity_nameNull()?null : row.city_name,
                        row.Isstate_idNull()?null: row.state_id,
                        row.Iszip_codeNull()?null : row.zip_code,
                        row.Isphone1Null()?null : row.phone1);
                    //SosCrmDataContext.Instance.MS_MonitronicsEntityAgencies.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityAgenciesSave(row.agency_no, row.agencytype_id, row.agency_name, row.city_name, row.state_id, row.zip_code, row.phone1, "SYSTEM-UT"));
                }
            }

            
        }
        #endregion AgenciesMethod

    /* AgencyType */
        #region AgencyTypeMethod
        [TestMethod]
        public void TestGetAgencytype()
        {
             

            var moniService = new Monitronics(_USERID, _PASWRD);
            var getAgencies = new GetAgencies
            {
                GetAgency = new AgencyInfo
                {
                    ZipCode = "84097"
                }
            };
            var serialized = getAgencies.Serialize();
            DataSet dsAgenciesRaw;
            Errors dsErrorsGet;
            string firstErrorMsgGet;
            Assert.IsTrue(moniService.GetDataTry(MS_MonitronicsEntity.MetaData.AgenciesID, out dsAgenciesRaw,
                out dsErrorsGet, out firstErrorMsgGet, null, serialized));
            if (dsAgenciesRaw != null)
            {
                var dsAgencyTypes = Utils.ConvertDataSet<GetAgencyTypes>(dsAgenciesRaw);
                Assert.IsNotNull(dsAgencyTypes, "DataSet conversion did not work for GetAgencyTypes");

                foreach (GetAgencyTypes.TableRow row in dsAgencyTypes.Table.Rows)
                {
                    Console.WriteLine("AgencyTypeId: {0} | Description: {1}", row.agencytype_id,
                        row.IsdescrNull() ? null : row.descr);
                }
            }
        }

        #endregion AgencyTypeMethod

	/* AgencyType,BusRule,Authorities,CellProvider, CellServices*/	    
        #region MultipleMethods 
		[TestMethod]
        public void TestGetBusinessRules()
        {
            #region Method Get AgencyTypes

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsAgencyTypesRaw;
            Errors dsErrorsGet;
            string firstErrorMsgGet;
            Assert.IsTrue(moniService.GetDataTry(MS_MonitronicsEntity.MetaData.AgencyTypesID, out dsAgencyTypesRaw,
                out dsErrorsGet, out firstErrorMsgGet));
            if (dsAgencyTypesRaw != null)
            {
                var dsAgencyTypes = Utils.ConvertDataSet<GetAgencyTypes>(dsAgencyTypesRaw);
                Assert.IsNotNull(dsAgencyTypes, "DataSet conversion did not work for GetAgencyTypes");

                foreach (GetAgencyTypes.TableRow row in dsAgencyTypes.Table.Rows)
                {
                    Console.WriteLine("AgencyTypeId: {0} | Description: {1}", row.agencytype_id, row.descr);
                }
            }

            #endregion Method Get AgencyTypes

            #region Method Get Busrules

            DataSet dsBusRulesRaw;
            moniService.GetDataTry("busrules", out dsBusRulesRaw, out dsErrorsGet, out firstErrorMsgGet);
            if (dsBusRulesRaw != null)
            {
                var dsBusRules = Utils.ConvertDataSet<GetBusRules>(dsBusRulesRaw);
                Assert.IsNotNull(dsBusRules, "DataSet conversion did not work for GetBusRules");

                foreach (GetBusRules.TableRow row in dsBusRules.Table.Rows)
                {
                    Console.WriteLine("ErrorID: {0} | Message: {1}", row.err_no, row.busrule);
                }
            }

            #endregion Method Get Busrules

            #region Method Authorities

            DataSet dsAuthoritiesRaw;
            moniService.GetDataTry("authorities", out dsAuthoritiesRaw, out dsErrorsGet, out firstErrorMsgGet);
            if (dsAuthoritiesRaw != null)
            {
                var dsAuthorities = Utils.ConvertDataSet<GetAuthorities>(dsAuthoritiesRaw);
                Assert.IsNotNull(dsAuthorities, "DataSet conversion did not work for GetAuthorities");

                foreach (GetAuthorities.TableRow row in dsAuthorities.Table.Rows)
                {
                    Console.WriteLine("AuthID: {0} | Description: {1}", row.auth_id, row.descr);
                }
            }

            #endregion Method Authorities

            #region Method CellProviders

            DataSet dsCellProvidersRaw;
            moniService.GetDataTry("CellProviders", out dsCellProvidersRaw, out dsErrorsGet, out firstErrorMsgGet);
            if (dsCellProvidersRaw != null)
            {
                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsCellProvidersRaw, out dsErrors, out firstErrorMsg),
                    string.Format("There was an error retreiving Cell Providers: {0}", firstErrorMsg));

                var dsCellProviders = Utils.ConvertDataSet<GetCellProviders>(dsCellProvidersRaw);
                Assert.IsNotNull(dsCellProviders, "DataSet conversion did not work for GetCellProviders");

                foreach (GetCellProviders.TableRow row in dsCellProviders.Table.Rows)
                {
                    Console.WriteLine("CellProvider: {0} | Description: {1}", row.cell_provider, row.descr);
                }
            }

            #endregion Method CellProviders
        
            #region CellProviderServices

            /*
			 * CellProvider: ALMCOM | Description: Alarm.com
			 * CellProvider: ALMNET | Description: AlarmNet
			 * CellProvider: TELULR | Description: Tellular
			 * CellProvider: UPLINK | Description: Uplink
			 * */

            #region ALMCOM

            DataSet dsCellProviderSrvcsRaw;
            var getCellsvcs = new GetCellSvcs
            {
                GetCellSvc = new CellSvc
                {
                    CellProvider = "ALMCOM"
                }
            };
            var xmlData = getCellsvcs.Serialize();

            moniService.GetDataTry("Cellsvcs", out dsCellProviderSrvcsRaw, out dsErrorsGet, out firstErrorMsgGet, null,
                xmlData);
            if (dsCellProviderSrvcsRaw != null)
            {
                var dsCellProviderOptions = Utils.ConvertDataSet<GetCellProviderOptions>(dsCellProviderSrvcsRaw);
                Assert.IsNotNull(dsCellProviderOptions, "DataSet conversion did not work for GetCellsvcs");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsCellProviderSrvcsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Cellsvcs: {0}", firstErrorMsg));

                foreach (GetCellProviderOptions.TableRow row in dsCellProviderOptions.Table.Rows)
                {
                    Console.WriteLine("ALMCOM:: OptionId: {0} | Description: {1}", row.option_id, row.descr);
                }
            }

            #endregion ALMCOM

            #region ALMNET

            getCellsvcs = new GetCellSvcs
            {
                GetCellSvc = new CellSvc
                {
                    CellProvider = "ALMNET"
                }
            };
            xmlData = getCellsvcs.Serialize();

            moniService.GetDataTry("Cellsvcs", out dsCellProviderSrvcsRaw, out dsErrorsGet, out firstErrorMsgGet, null,
                xmlData);
            if (dsCellProviderSrvcsRaw != null)
            {
                var dsCellProviderOptions = Utils.ConvertDataSet<GetCellProviderOptions>(dsCellProviderSrvcsRaw);
                Assert.IsNotNull(dsCellProviderOptions, "DataSet conversion did not work for GetCellsvcs");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsCellProviderSrvcsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Cellsvcs: {0}", firstErrorMsg));

                foreach (GetCellProviderOptions.TableRow row in dsCellProviderOptions.Table.Rows)
                {
                    Console.WriteLine("ALMNET:: OptionId: {0} | Description: {1}", row.option_id, row.descr);
                }
            }

            #endregion ALMNET

            #region TELULR

            getCellsvcs = new GetCellSvcs
            {
                GetCellSvc = new CellSvc
                {
                    CellProvider = "TELULR"
                }
            };
            xmlData = getCellsvcs.Serialize();

            moniService.GetDataTry("Cellsvcs", out dsCellProviderSrvcsRaw, out dsErrorsGet, out firstErrorMsgGet, null,
                xmlData);
            if (dsCellProviderSrvcsRaw != null)
            {
                var dsCellProviderOptions = Utils.ConvertDataSet<GetCellProviderOptions>(dsCellProviderSrvcsRaw);
                Assert.IsNotNull(dsCellProviderOptions, "DataSet conversion did not work for GetCellsvcs");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsCellProviderSrvcsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Cellsvcs: {0}", firstErrorMsg));

                foreach (GetCellProviderOptions.TableRow row in dsCellProviderOptions.Table.Rows)
                {
                    Console.WriteLine("TELULR:: OptionId: {0} | Description: {1}", row.option_id, row.descr);
                }
            }

            #endregion TELULR

            #region UPLINK

            getCellsvcs = new GetCellSvcs
            {
                GetCellSvc = new CellSvc
                {
                    CellProvider = "UPLINK"
                }
            };
            xmlData = getCellsvcs.Serialize();

            moniService.GetDataTry("Cellsvcs", out dsCellProviderSrvcsRaw, out dsErrorsGet, out firstErrorMsgGet, null,
                xmlData);
            if (dsCellProviderSrvcsRaw != null)
            {
                var dsCellProviderOptions = Utils.ConvertDataSet<GetCellProviderOptions>(dsCellProviderSrvcsRaw);
                Assert.IsNotNull(dsCellProviderOptions, "DataSet conversion did not work for GetCellsvcs");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsCellProviderSrvcsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Cellsvcs: {0}", firstErrorMsg));

                foreach (GetCellProviderOptions.TableRow row in dsCellProviderOptions.Table.Rows)
                {
                    Console.WriteLine("UPLINK:: OptionId: {0} | Description: {1}", row.option_id, row.descr);
                }
            }

            #endregion UPLINK

        }
        #endregion CellProviderServices 
        #endregion MultipleMethods

    /* Contypes */
        #region ConTypesMethod
        [TestMethod]
        public void TestGetConTypes()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsConTypesRaw;
            Errors dsErrors;
            string firstErrorMsg;
            Assert.IsTrue(moniService.GetDataTry("Contypes", out dsConTypesRaw, out dsErrors, out firstErrorMsg));
            if (dsConTypesRaw != null)
            {
                var dsConTypes = Utils.ConvertDataSet<GetConTypes>(dsConTypesRaw);
                Assert.IsNotNull(dsConTypes, "DataSet conversion did not work for GetConTypes");

                foreach (GetConTypes.TableRow row in dsConTypes.Table.Rows)
                {
                    Console.WriteLine("CTacTypeId: {0} | Description: {1}", row.ctactype_id, row.descr);
                }
            }

            
        }
        #endregion ConTypesMethod

    /* Equip_event_xref */
        #region EquipEventXRefMethod
        [TestMethod]
        public void TestGetEquipEventXref()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsEquipEventXrefRaw;
            Errors dsErrorsGet;
            string firstErrorMsgGet;
            Assert.IsTrue(moniService.GetDataTry("Equip_event_xref", out dsEquipEventXrefRaw, out dsErrorsGet,
                out firstErrorMsgGet));
            if (dsEquipEventXrefRaw != null)
            {
                var dsEquipEventXref = Utils.ConvertDataSet<GetEquipEventXref>(dsEquipEventXrefRaw);
                Assert.IsNotNull(dsEquipEventXref, "DataSet conversion did not work for GetConTypes");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsEquipEventXrefRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Equip_event_xref: {0}", firstErrorMsg));
                foreach (GetEquipEventXref.TableRow row in dsEquipEventXref.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("EquipTypeId: {0} | EventID: {1} | SiteKind: {2}",
                        row.Isequiptype_idNull() ? null : row.equiptype_id,
                        row.Isevent_idNull() ? null : row.event_id,
                        row.Issite_kindNull() ? null : row.site_kind);
                }
            }

            
        }
        #endregion EquipEventXRefMethod

    /* Equiplocs */
        #region EquipLocsMethod
        [TestMethod]
        public void TestGetEquipLocs()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsEquipLocsRaw;
            Assert.IsTrue(moniService.GetDataTry("Equiplocs", out dsEquipLocsRaw));
            if (dsEquipLocsRaw != null)
            {
                var dsEquipLocs = Utils.ConvertDataSet<GetEquipLocations>(dsEquipLocsRaw);
                Assert.IsNotNull(dsEquipLocs, "DataSet conversion did not work for GetEquipLocs");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsEquipLocsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting EquipLocs: {0}", firstErrorMsg));
                foreach (GetEquipLocations.TableRow row in dsEquipLocs.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("EquipLocId: {0} | Description: {1}",
                        row.Isequiploc_idNull() ? null : row.equiploc_id,
                        row.IsdescrNull() ? null : row.descr);
                }
            }

            
        }
        #endregion EquipLocsMethod

    /* Equiptypes */
        #region EquiptypesMethod
        [TestMethod]
        public void TestGetEquipTypes()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Equiptypes", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetEquipTypes>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for EquipTypes");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting EquipTypes: {0}", firstErrorMsg));
                foreach (GetEquipTypes.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("EquipTypeId: {0} | Description: {1}",
                        row.Isequiptype_idNull() ? null : row.equiptype_id,
                        row.IsdescrNull() ? null : row.descr);
                }
            }

            
        }
        #endregion EquiptypesMethod

    /* Events -- These are the different events you can have for zones. */
        #region EventsMethod
        [TestMethod]
        public void TestGetEvents()
        {
           

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Events", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetEvents>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Events");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Events: {0}", firstErrorMsg));
                foreach (GetEvents.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("EventId: {0} | Description: {1}",
                        row.Isevent_idNull() ? null : row.event_id,
                        row.IsdescrNull() ? null : row.descr);
                }
            }

            
        }
        #endregion EventsMethod

    /** Languages */
        #region LanguagesMethod
        [TestMethod]
        public void TestGetLanguages()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Languages", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetLanguages>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Events");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Events: {0}", firstErrorMsg));
                foreach (GetLanguages.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("LanguageId: {0} | Description: {1}",
                        row.Islang_idNull() ? null : row.lang_id,
                        row.IsdescrNull() ? null : row.descr);
                }
            }

           
        }
        #endregion LanguagesMethod

    /** Nameprefixes */
        #region NamePrefixesMethod
        [TestMethod]
        public void TestGetNamePrefixes()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Nameprefixes", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetNamePrefixes>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Name Prefixes");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Name Prefixes: {0}", firstErrorMsg));
                foreach (GetNamePrefixes.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("Name Prefix: {0}",
                        row.IsprefixNull() ? null : row.prefix);
                }
            }

            
        }
        #endregion NamePrefixesMethod

    /** NameSuffixes */
        #region NameSuffixesMethod
        [TestMethod]
        public void TestGetNameSuffixes()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Namesuffixes", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetNameSuffixes>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Name Suffixes");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Name Suffixes: {0}", firstErrorMsg));
                foreach (GetNameSuffixes.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("Name Suffix: {0}",
                        row.IssuffixNull() ? null : row.suffix);
                }
            }

            
        }
        #endregion NameSuffixesMethod

    /* Ooscats */
        #region OutOfServiceCatsMethod
        [TestMethod]
        public void TestGetOosCats()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Ooscats", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetOosCats>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Name OosCategories");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Name OosCategories: {0}", firstErrorMsg));
                foreach (GetOosCats.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("OosCatId: {0} | Description: {1}",
                        row.Isooscat_idNull() ? null : row.ooscat_id,
                        row.IsdescrNull() ? null : row.descr);
                }
            }

            
        }
        #endregion OutOfServiceCatsMethod Method

        /* Options */
        #region OptionsMethod
        [TestMethod]
        public void TestGetOptions()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Options", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetOptions>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Options");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Options: {0}", firstErrorMsg));
                foreach (GetOptions.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine(
                        "Usage: {0} | OptionId: {1} | Description: {2} | ValidValue: {3} | ValueDesc: {4} | ValueReq: {5}",
                        row.IsUsageNull() ? null : row.Usage,
                        row.Isoption_idNull() ? null : row.option_id,
                        row.IsdescrNull() ? null : row.descr,
                        row.Isvalid_valueNull() ? null : row.valid_value,
                        row.Isvalue_descrNull() ? null : row.value_descr,
                        row.Isvalue_requiredNull() ? null : row.value_required);
                }
            }

            
        }
        #endregion OptionsMethod

    /* Partialbatches */
        #region PartialBatchesMethod
        [TestMethod]
        public void TestGetPartialBatches()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Partialbatches", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetPartialBatches>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for PartialBatches");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting PartialBatches: {0}", firstErrorMsg));
                foreach (GetPartialBatches.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("WsiBatchNo: {0} | CsNo: {1} | SiteName: {2} | ServcoNo: {3} | MmChangeDate: {4}",
                        !row.Iswsi_batch_noNull() ? (int?) row.wsi_batch_no : null,
                        row.Iscs_noNull() ? null : row.cs_no,
                        row.Issite_nameNull() ? null : row.site_name,
                        !row.Isservco_noNull() ? (int?) row.servco_no : null,
                        !row.Ismm_change_dateNull() ? (DateTime?) row.mm_change_date : null);
                }
            }

            
        }
        #endregion PartialBatchesMethod

    /* Permtypes */
        #region PermTypesMethod
        [TestMethod]
        public void TestGetPermtypes()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Permtypes", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetPermitTypes>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for PermitTypes");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting PermitTypes: {0}", firstErrorMsg));
                foreach (GetPermitTypes.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("PermitTypeId: {0} | Description: {1}",
                        !row.Ispermtype_idNull() ? row.permtype_id : null,
                        row.IsdescrNull() ? null : row.descr);
                }
            }

            
        }
        #endregion PermTypesMethod

    /* Phonetypes */
        #region PhoneTypesMethod
        [TestMethod]
        public void TestGetPhoneTypes()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Phonetypes", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetPhoneTypes>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for PhoneTypes");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting PhoneTypes: {0}", firstErrorMsg));
                foreach (GetPhoneTypes.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("PhoneTypeId: {0} | Description: {1} | Method: {2}",
                        !row.Isphonetype_idNull() ? row.phonetype_id : null,
                        row.IsdescrNull() ? null : row.descr,
                        row.IsmethodNull() ? null : row.method);
                }
            }

            
        }
        #endregion PhoneTypesMethod

    /** Prefixes */
        #region PrefixesMethod
        [TestMethod]
        public void TestGetPrefixes()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            var getPrefixes = new GetPrefixes
            {
                GetPrefix = new Prefix
                {
                    CsNo = "6606"
                }
            };
            var xmlData = getPrefixes.Serialize();
            Assert.IsTrue(moniService.GetDataTry("Prefixes", out dsRaw, null, xmlData));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<MonitoringStations.Schemas.GetPrefixes>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Prefixes");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Prefixes: {0}", firstErrorMsg));
                foreach (MonitoringStations.Schemas.GetPrefixes.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine(
                        "CellFlag: {0} | CsNoLength: {1} | CmPurchase: {2} | ServcoNo: {3} | CellProvider: {4} | SysTypeId: {5} | CsNo: {6} | BrandedFlag: {7} | ReceiverPhone: {8} | AlarmNetCityCs: {9}",
                        !row.Iscell_flagNull() ? row.cell_flag : null,
                        !row.Iscsno_lenNull() ? (object) row.csno_len : null,
                        row.Iscm_purchaseNull() ? null : row.cm_purchase,
                        !row.Isservco_noNull() ? (int?) row.servco_no : null,
                        row.Iscell_providerNull() ? null : row.cell_provider,
                        row.Issystype_idNull() ? null : row.systype_id,
                        !row.Isco_noNull() ? (short?) row.co_no : null,
                        row.Isbranded_flagNull() ? null : row.branded_flag,
                        row.Isreceiver_phoneNull() ? null : row.receiver_phone,
                        row.Isalarmnet_citycsNull() ? null : row.alarmnet_citycs);
                }
            }

            
        }
        #endregion PrefixesMethod

    /* Relations */
        #region RelationsMethod
        [TestMethod]
        public void TestGetRelations()
        {
           

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Relations", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetRelations>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Relations");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Relations: {0}", firstErrorMsg));
                foreach (GetRelations.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("RelationId: {0} | Description: {1}",
                        !row.Isrelation_idNull() ? row.relation_id : null,
                        !row.IsdescrNull() ? row.descr : null);
                }
            }

            
        }
        #endregion RelationsMethod

    /* Sitetypes */
        #region SiteTypesMethod
        [TestMethod]
        public void TestGetSiteTypes()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            const string CS_NO = "24234234";
            Assert.IsTrue(moniService.GetDataTry("Sitetypes", out dsRaw, CS_NO));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetSiteTypes>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Sitetypes");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Sitetypes: {0}", firstErrorMsg));
                foreach (GetSiteTypes.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("SiteTypeId: {0} | Description: {1} | SiteKind: {2}",
                        !row.Issitetype_idNull() ? row.sitetype_id : null,
                        !row.IsdescrNull() ? row.descr : null,
                        !row.Issite_kindNull() ? row.site_kind : null);
                }
            }

            
        }
        #endregion SiteTypesMethod

    /* States */
        #region StatesMethod
        [TestMethod]
        public void TestGetStates()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("States", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetStates>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for States");

                Errors dsErrors;
                string firstErrorMsg;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting States: {0}", firstErrorMsg));
                foreach (GetStates.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("StateId: {0} | StateName: {1}",
                        !row.Isstate_idNull() ? row.state_id : null,
                        !row.Isstate_nameNull() ? row.state_name : null);
                }
            }

            
        }
        #endregion StatesMethod


    /* Systype_xref */
        #region SysTypeXRefMethod
        [TestMethod]
        public void TestGetSysTypeXref()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Systype_xref", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetSysTypeXref>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for States");

                Errors dsErrors;
                string firstErrorMsg;
                var count = 0;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting States: {0}", firstErrorMsg));
                foreach (GetSysTypeXref.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("DigSysTypeId: {0} | TwoWayDeviceId: {1} | CellSysTypeId: {2}",
                        !row.Isdig_systype_idNull() ? row.dig_systype_id : null,
                        !row.Istwoway_device_idNull() ? row.twoway_device_id : null,
                        !row.Iscell_systype_idNull() ? row.cell_systype_id : null);
                    count++;
                }
                Console.WriteLine("Total Number: {0}", count);
            }

            
        }
        #endregion SysTypeXRefMethod


    /* Systypes */
        #region SystemTypesMethod
        [TestMethod]
        public void TestGetSysTypes()
        {
           

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Systypes", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetSysTypes>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Systypes");

                Errors dsErrors;
                string firstErrorMsg;
                var count = 0;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Systypes: {0}", firstErrorMsg));
                foreach (GetSysTypes.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("SysTypeId: {0} | Description: {1}",
                        !row.Issystype_idNull() ? row.systype_id : null,
                        !row.IsdescrNull() ? row.descr : null);
                    count++;
                }
                Console.WriteLine("Total Number: {0}", count);
            }

            
        }
        #endregion SystemTypesMethod


    /* Testcats */
        #region TestCategoriesMethod
        [TestMethod]
        public void TestGetTestCats()
        {


            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Testcats", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetTestCats>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Testcats");

                Errors dsErrors;
                string firstErrorMsg;
                var count = 0;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Testcats: {0}", firstErrorMsg));
                foreach (GetTestCats.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("TestCatId: {0} | Description: {1} | Default Hours: {2} | Default Minutes: {3}",
                        !row.Istestcat_idNull() ? row.testcat_id : null,
                        !row.IsdescrNull() ? row.descr : null,
                        !row.Isdefault_hoursNull() ? (int?) row.default_hours : null,
                        !row.Isdefault_minutesNull() ? (int?) row.default_minutes : null);
                    count++;
                }
                Console.WriteLine("Total Number: {0}", count);
            }


        }
        #endregion TestCategoriesMethod


    /* Twoways */
        #region TwoWaysMethod
        [TestMethod]
        public void TestGetTwoWays()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Twoways", out dsRaw));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetTwoWays>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Twoways");

                Errors dsErrors;
                string firstErrorMsg;
                var count = 0;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Twoways: {0}", firstErrorMsg));
                foreach (GetTwoWays.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("TwoWayDeviceId: {0} | Description: {1}",
                        !row.Istwoway_device_idNull() ? row.twoway_device_id : null,
                        !row.IsdescrNull() ? row.descr : null);
                    count++;
                }
                Console.WriteLine("Total Number: {0}", count);
            }

            
        }
        #endregion TwowaysMethod 


    /* Zips */
        #region ZipsMethod
        [TestMethod]
        public void TestGetZips()
        {
           

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            var getZipCodes = new GetZipCodes
            {
                GetZipCode = new ZipCode
                {
					//StateId = "MO",
					//CountyName = "LOS ANGELES"
					PostalCode = "63112"
                }
            };
            Assert.IsTrue(moniService.GetDataTry("Zips", out dsRaw, null, getZipCodes.Serialize<GetZipCodes>()));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetZips>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Zips");

                Errors dsErrors;
                string firstErrorMsg;
                var count = 0;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Zips: {0}", firstErrorMsg));
                foreach (GetZips.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("City Name: {0} | County Name: {1} | State Id: {2} | Zip Code: {3}",
                        !row.Iscity_nameNull() ? row.city_name : null,
                        !row.Iscounty_nameNull() ? row.county_name : null,
                        !row.Isstate_idNull() ? row.state_id : null,
                        !row.Iszip_codeNull() ? row.zip_code : null);
                    count++;
                }
                Console.WriteLine("Total Number: {0}", count);
            }

            
        }
        #endregion ZipsMethod 


    /* Zonestates */
        #region ZoneStatesMethod
        [TestMethod]
        public void TestGetZoneStates()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("Zonestates", out dsRaw), "Failed to GetData for Zonestates");
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetZoneStates>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Zone States");

                Errors dsErrors;
                string firstErrorMsg;
                var count = 0;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Zone States: {0}", firstErrorMsg));
                foreach (GetZoneStates.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("Zone State Id: {0} | Description: {1}",
                        !row.Iszonestate_idNull() ? row.zonestate_id : null,
                        !row.IsdescrNull() ? row.descr : null);
                    count++;
                }
                Console.WriteLine("Total Number: {0}", count);
            }

            
        }
        #endregion Method
        

    /* SiteStat */
        #region SiteStatMethod 
        [TestMethod]
        public void TestGetSiteStat()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("SiteStat", out dsRaw), "Failed to GetData for SiteStat");
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetZoneStates>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for Zone States");

                Errors dsErrors;
                string firstErrorMsg;
                var count = 0;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting Site Stat: {0}", firstErrorMsg));
                foreach (GetZoneStates.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("Site State Id: {0} | Description: {1}",
                        !row.Iszonestate_idNull() ? row.zonestate_id : null,
                        !row.IsdescrNull() ? row.descr : null);
                    count++;
                }
                Console.WriteLine("Total Number: {0}", count);
            }

            
        }
        #endregion Method 


    /* SecGroups */
        #region SecGoupsMethod 
        [TestMethod]
        public void TestGetSecGroups()
        {
            

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            Assert.IsTrue(moniService.GetDataTry("secgroups", out dsRaw), "Failed to GetData for Security Groups");
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetSecGroups>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for SecGroups");

                Errors dsErrors;
                string firstErrorMsg;
                var count = 0;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting SecGroups: {0}", firstErrorMsg));
                foreach (GetSecGroups.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("Security Group: {0} | Security Level: {1} | All Users:{2} | All Accounts:{3}",
                        !row.Issec_groupNull() ? row.sec_group : null,
                        !row.Issec_levelNull() ? row.sec_level : null,
                        !row.Isall_usersNull() ? row.all_users : null,
                        !row.Isall_accountsNull() ? row.all_accounts : null);
                    count++;
                }
                Console.WriteLine("Total Number: {0}", count);
            }

        }
        #endregion SecGroupsMethod 
        

    /* ServiceCompany */
        #region ServiceCompanyMethod
        [TestMethod]
        public void TestGetServiceCompany()
        {
          

            var moniService = new Monitronics(_USERID, _PASWRD);
            DataSet dsRaw;
            var getServiceCompany = new GetServiceCompanies
            {

                GetServiceCompany = new ServiceCompany
                {
                    ServiceCompanyNumber = "811110001"
                }
            };
            Assert.IsTrue(moniService.GetDataTry("servicecompany", out dsRaw, null, getServiceCompany.Serialize<GetServiceCompanies>()));
            if (dsRaw != null)
            {
                var ds = Utils.ConvertDataSet<GetServiceCompany>(dsRaw);
                Assert.IsNotNull(ds, "DataSet conversion did not work for ServiceCompany");

                Errors dsErrors;
                string firstErrorMsg;
                var count = 0;
                Assert.IsFalse(Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg),
                    string.Format("Error on getting ServiceCompany: {0}", firstErrorMsg));
                foreach (GetServiceCompany.TableRow row in ds.Table.Rows)
                {
                    /** Have to check for DBNull's */
                    Console.WriteLine("ServiceCoNumber: {0} | ServiceCoName: {1} | ServiceCoAddr1:{2} | ServiceCoAddr2:{3}| CityName:{4} | StateId:{5}| ZipCode:{6} | Phone1:{7}",
                        !row.Isservco_noNull() ? row.servco_no: null,
                        !row.Isservco_nameNull()? row.servco_name: null,
                        !row.Isservco_addr1Null()? row.servco_addr1: null,
                        !row.Isservco_addr2Null()? row.servco_addr2: null,
                        !row.Iscity_nameNull()? row.city_name:null,
                        !row.Isstate_idNull()? row.state_id: null,
                        !row.Iszip_codeNull()? row.zip_code: null,
                        !row.Isphone1Null()? row.phone1: null);
                    count++;
                }
                Console.WriteLine("Total Number: {0}", count);
            }

           
        } 
        #endregion ServiceCompanyMethod



        #endregion Test Methods

    }
}