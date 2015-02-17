using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using NSE.FOS.Contracts.Models;
using NXS.Logic.MonitoringStations.Helpers;
using NXS.Logic.MonitoringStations.Models;
using NXS.Logic.MonitoringStations.Models.Get;
using NXS.Logic.MonitoringStations.Schemas;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FOS.MonitoringStationServices.Contracts.Models;
using SOS.FOS.MonitoringStationServices.Monitronics.Models;
using SOS.FOS.MonitoringStationServices.Utilities.Exceptions;
using SOS.Lib.Core.ErrorHandling;
using Contact = NXS.Logic.MonitoringStations.Models.Contact;
using GetPrefixes = NXS.Logic.MonitoringStations.Schemas.GetPrefixes;
using GetAgencies = NXS.Logic.MonitoringStations.Schemas.GetAgencies;
using SystemStatusInfo = SOS.FOS.MonitoringStationServices.Monitronics.Models.SystemStatusInfo;

namespace SOS.FOS.MonitoringStationServices.Monitronics
{
	public class CentralStation : IMonitoringStation
	{

		#region .ctor

		public CentralStation()
		{
			_username = Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("MN_USERNAME");
			_username = Lib.Util.Cryptography.TripleDES.DecryptString(_username, null);
			_password = Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("MN_PASSWORD");
			_password = Lib.Util.Cryptography.TripleDES.DecryptString(_password, null);
            _servCoNummber = "811110003";

			// ** Save default DEFAULT SYSTEM ACCOUNTID
			var defaultSysAccountId = Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("MN_PASSWORD");
			defaultSysAccountId = Lib.Util.Cryptography.TripleDES.DecryptString(defaultSysAccountId, null);
			if (!long.TryParse(defaultSysAccountId, out _defSysActId))
				_defSysActId = _DEFAULT_SYS_ACCOUNTID;
		}
		#endregion .ctor

		#region Properties

		private readonly string _username;
		private readonly string _password;
// ReSharper disable once NotAccessedField.Local
	    private readonly string _servCoNummber;
// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
		private readonly long _defSysActId;
		private const long _DEFAULT_SYS_ACCOUNTID = 1000;

		#endregion Properties

		public IFosResult<MS_AccountSubmit> AccountShell(MS_AccountSubmit msAccountSubmit, string gpEmployeeId)
		{
			throw new NotImplementedException();
		}

		public IFosResult<MS_AccountSubmit> AccountOnboard(MS_AccountSubmit msAccountSubmit, string gpEmployeeId)
		{
			// ** Inititalize.
			const string METHOD_NAME = "AccountOnboard";
			#region WORK IN PROGRESS

			#region Setup Account

			var result = new FosResult<MS_AccountSubmit>
			{
				Code = BaseErrorCodes.ErrorCodes.Initializing.Code(),
				Message = string.Format(BaseErrorCodes.ErrorCodes.Initializing.Message(), METHOD_NAME)
			};
			var msAccount = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(msAccountSubmit.AccountId);
			var mcAccount = msAccount.Account;
			//var msAccountSalesInfo =
			//	SosCrmDataContext.Instance.MS_AccountSalesInformations.LoadByPrimaryKey(msAccountSubmit.AccountId);
			var msDealer = SosCrmDataContext.Instance.MS_Dealers.LoadByPrimaryKey(mcAccount.DealerId);
			var aeMoniCustomer =
				SosCrmDataContext.Instance.AE_CustomerInformationViews.LoadSingle(
					SosCrmDataStoredProcedureManager.AE_CustomerInformationViewMonitoredPartyByAccountId(msAccount.AccountID));
			var mcAddress =
				SosCrmDataContext.Instance.MC_AddressesViews.LoadSingle(
					SosCrmDataStoredProcedureManager.MC_AddressGetPremiseByAccountId(msAccount.AccountID));
			var qlReport =
				SosCrmDataContext.Instance.QL_CreditReports.LoadSingle(
					SosCrmDataStoredProcedureManager.QL_CreditReportMaxScoreByCmfID(aeMoniCustomer.CustomerMasterFileId));
			if (msAccount.Contract == null) 
				throw new CsExceptionNoContract(msAccountSubmit.AccountId, msAccount.IndustryAccount.Csid);
			var contractLength = msAccount.Contract.ContractLength;

			var qlQualifyCustomerInfo = SosCrmDataContext.Instance.QL_QualifyCustomerInfoViews.LoadByAccountId(msAccount.AccountID);
			var optionIdCMPUR = qlQualifyCustomerInfo.Score > 600 ? "PUR" : "CM" ;
			var dslVoip = msAccount.DslSeizure.DslSeizureID == (short)MS_AccountDslSeizureType.DslSeizureEnum.Dsl
				? "DSL"
				: "NONE";


			var sysTypeId = msAccount.GetMoniSysTypeId().SystemTypeID; // SysTypeId = "A1S001"
			var secSysTypeId = string.Empty;
			if (msAccount.CellularTypeId.Equals(MS_AccountCellularType.MetaData.Cell_PrimaryID))
			{
				var cellSysTypeId = msAccount.GetMoniCellTypeId();
				if (cellSysTypeId == null) throw new CsExceptionMissingMetadata(msAccount.AccountID, "Monitronics Cell System Type");
				sysTypeId = cellSysTypeId.SystemTypeID;
				secSysTypeId = msAccount.GetMoniSysTypeId().SystemTypeID;
			}
			var twoWayDeviceId = msAccount.GetMoniSysTypeId().SystemTypeID;
			var panelLocation = SosCrmDataContext.Instance.MS_EquipmentLocationsViews.GetPanelLocationByAccountId(msAccount.AccountID);
			if(panelLocation == null)
				throw new Exception("Unable to find a panel for this account.");
			var installDate = DateTime.Now;

			var acct = new Account
			{
				#region SiteSystem
				SiteSystems = new List<SiteSystem>
				{
					new SiteSystem
					{
						SiteName = string.Format("{0} {1}", aeMoniCustomer.FirstName, aeMoniCustomer.LastName),
						SiteAddr1 = mcAddress.StreetAddress,
						CityName = mcAddress.City,
						StateId = mcAddress.StateId,
						CountyName = mcAddress.County,
						ZipCode = mcAddress.PostalCode + mcAddress.PlusFour,
						//Phone1 = mcAddress.Phone,
						SiteTypeId = msAccount.SiteType.GetMoniSiteTypeId().SiteTypeID,
						SiteStateId = "A",
						CodeWord1 = msAccount.AccountPassword,
//						CsPartNo = "300",
						CrossStreet = mcAddress.CrossStreet,
						PanelLocation = panelLocation.EquipmentLocationDesc,
//						InstallDate = msAccountSalesInfo.InstallDate.ToString(),
						InstallDate = string.Format("{0:MM/dd/yyyy}", installDate),
						PanelCode = msAccount.PanelCode ?? "1234",
						PanelPhone = msAccount.PanelPhone ?? mcAddress.Phone,
						ReceiverPhone = msAccount.IndustryAccount.ReceiverLine.ReceiverNumber,
						SysTypeId = sysTypeId, // msAccount.GetMoniSysTypeId().SystemTypeID, 
						TwoWayDeviceId = twoWayDeviceId,
						ServiceNo = msDealer.MoniServiceNo,
						InstallServiceNo = msDealer.MoniInstallServiceNo
					}
				},

				#endregion SiteSystem

				#region SiteOptions
				SiteOptions = new List<SiteOption>
				{
					new SiteOption
					{
						OptionId = "CMPUR",
						OptionValue = optionIdCMPUR
					},
					new SiteOption
					{
						OptionId = "CONTRLEN",
						OptionValue = contractLength.ToString(CultureInfo.InvariantCulture)
					}
				},

				#endregion SiteOptions

				#region SiteSytemOptions
				SiteSystemOptions = new List<SiteSystemOption>
				{
					new SiteSystemOption
					{
						OptionId = "DSL-VOIP",
						OptionValue = dslVoip
					},
		           
					new SiteSystemOption
					{
						OptionId = "INST CODE",
						OptionValue = msAccount.PanelCode ?? "0607"
					},
                   
					new SiteSystemOption
					{
						OptionId = "PRIVACY",
						OptionValue = (msAccount.Privacy != null && msAccount.Privacy.Value) ? "Y" : "N"
					},
                   
					new SiteSystemOption
					{
						OptionId = "WIRELESS",
						OptionValue = "W"   
					},
                    
					new SiteSystemOption
					{
						OptionId = "SIGFMT",
						OptionValue = msAccount.GetMsAccountSignalFormat()
					},
                    
					new SiteSystemOption
					{
						OptionId = "TRANSFORMER",
						OptionValue = panelLocation.EquipmentLocationDesc
					},
					new SiteSystemOption
					{
						OptionId = "ALMCOMINTSVC",
						OptionValue = "RBI"
					}
				}
				#endregion SiteSytemOptions
			};
			// Check for secondary system
			if (!string.IsNullOrEmpty(secSysTypeId))
			{
				acct.SiteSystems[0].SecSysTypeId = secSysTypeId;
			}

			#region Setup Contacts

			#region Cellular Settings

			if (msAccount.CellularTypeId.Equals(MS_AccountCellularType.MetaData.Cell_BackupID)
			    || msAccount.CellularTypeId.Equals(MS_AccountCellularType.MetaData.Cell_PrimaryID))
			{
				var cellProvider = msAccount.GetMoniCellProvider();
				if (cellProvider == null) throw new CsExceptionMissingMetadata(msAccount.AccountID, "Monitronics Cell Provider");

				// ** Set Phone1
				acct.SiteSystems[0].Phone1 = "8662055200";

				// ** Get Cell Mac based on Provider
				var serialNumber = msAccount.GetCellDeviceSerialNumber();
				if (serialNumber == null)
					throw new Exception(string.Format("This system does not have a Serial number for its cellular device."));

				acct.SiteSystemOptions.Add(new SiteSystemOption
				{
					OptionId = "CELLPROV",
					OptionValue = msAccount.GetMoniCellProvider().ValidValue
				});

//				var serialNumber = msAccount.IndustryAccount.ReceiverLineBlock
				acct.SiteSystemOptions.Add(new SiteSystemOption
				{
					OptionId = "CELLMAC",
					OptionValue = serialNumber.MACAddress
				});
			}
			else
			{
				// ** Set Phone1
				acct.SiteSystems[0].PanelPhone = mcAddress.Phone;
			}

			#endregion Cellular Settings

// ReSharper disable once UseObjectOrCollectionInitializer
			var emergencyContactsList = new List<Contact>();
			// ** Add the Primary Contract Signer
			emergencyContactsList.Add(new Contact
			{
				FirstName = aeMoniCustomer.FirstName,
				LastName = aeMoniCustomer.LastName,
				ContactTypeId = MS_MonitronicsEntityContactType.MetaData.Monitoring_ContactID,
				ContlTypeNo = "5000",  // EVC
				RelationId = MS_MonitronicsEntityRelation.MetaData.OwnerID,
				AuthId = MS_MonitronicsEntityAuthority.MetaData.Full_AuthorityID,
				ContractSignerFlag = "Y",
				HasKeyFlag = "Y",
				Phone1 = aeMoniCustomer.PhoneHome ?? mcAddress.Phone,
				PhoneTypeId1 = MS_MonitronicsEntityPhoneType.MetaData.HomeID,
				Phone2 = aeMoniCustomer.PhoneWork,
				PhoneTypeId2 = MS_MonitronicsEntityPhoneType.MetaData.WorkID,
				Phone3 = aeMoniCustomer.PhoneMobile,
				PhoneTypeId3 = MS_MonitronicsEntityPhoneType.MetaData.Cellular_PhoneID,
				EmailAddress = aeMoniCustomer.Email
			});
// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (MS_EmergencyContact contact in msAccount.GetEmergencyContact())
			{
				var emc = new Contact
				{
					FirstName = contact.FirstName,
					LastName = contact.LastName,
					ContactTypeId = contact.EmergencyContactType.MsContactTypeId,
					RelationId = contact.Relationship.MsRelationshipId,
					AuthId = contact.Authority.MsAuthorityId,
					Pin = contact.Password,
					ContractSignerFlag = "N",
					Phone1 = contact.Phone1,
					PhoneTypeId1 = contact.Phone1Type.MsPhoneTypeId,
					Phone2 = contact.Phone2,
					PhoneTypeId2 = contact.Phone2Type == null ? null : contact.Phone2Type.MsPhoneTypeId,
					Phone3 = contact.Phone3,
					PhoneTypeId3 = contact.Phone3Type == null ? null : contact.Phone3Type.MsPhoneTypeId,
					EmailAddress = contact.Email
				};
				emergencyContactsList.Add(emc);
			}
			acct.Contacts = emergencyContactsList;

			#endregion Setup Contacts

			#region SiteAgencyPermits

			var siteAgencyPermits = new List<SiteAgencyPermit>();
			var accountDaAssignments =
				SosCrmDataContext.Instance.MS_AccountDispatchAgencyAssignments.LoadCollectionByAccountId(msAccountSubmit.AccountId);
			foreach (var daAssignment in accountDaAssignments)
			{
				var daItem = new SiteAgencyPermit
				{
					AgencyNo = daAssignment.DispatchAgency.MsAgencyNumber,
					AgencyTypeId = daAssignment.DispatchAgency.DispatchAgencyType.MsAgencyTypeNo,
					Phone1 = daAssignment.DispatchAgency.Phone1
				};
				if (daAssignment.PermitNumber != null)
					daItem.PermitNo = daAssignment.PermitNumber;
				if (daAssignment.PermitEffectiveDate != null)
					daItem.EffectiveDate = daAssignment.PermitEffectiveDate.ToString();
				if (daAssignment.PermitExpireDate != null)
					daItem.EffectiveDate = daAssignment.PermitExpireDate.ToString();

				siteAgencyPermits.Add(daItem);
			}
			acct.SiteAgencyPermits = siteAgencyPermits;
			
			#endregion SiteAgencyPermits

			#region Zones
			
			var zoneAssignments = SosCrmDataContext.Instance.MS_AccountZoneAssignments.GetZoneAssignmentsByAccountId(msAccount.AccountID);
			var zones = new List<Zone>();
			foreach (var zone in zoneAssignments)
			{
				if (zone.AccountZoneTypeId == null || zone.AccountZoneTypeId.Equals(MS_AccountZoneType.MetaData.No_ZoneID)) continue;
				if (zone.Zone.Equals("000")) continue;
				int zoneInt;
				if (!int.TryParse(zone.Zone, out zoneInt)) continue;

				var newZone = new Zone
				{
					ZoneId = zoneInt.ToString(CultureInfo.InvariantCulture),
					EquipmentLocationId = zone.AccountEquipment.EquipmentLocation.MonitronicsCode,
					EquipmentTypeId = zone.AccountEquipment.Equipment.EquipmentType.MonitronicsCode,
					ZoneComment = zone.Comments,
					ZoneStateId = "A"
				};

				if (zone.AccountEvent == null)
					throw new CsExceptionInvalidZoneConfiguration(msAccount.AccountID, msAccount.IndustryAccount.ReceiverLine.MonitoringStationOSId, zone.AccountZoneAssignmentID, zone.AccountZoneTypeId, zone.Zone);
				if (zone.AccountEvent.MoniEvent != null)
					newZone.EventId = zone.AccountEvent.MoniEvent.event_id.ToString(CultureInfo.InvariantCulture);

				zones.Add(newZone);
			}
			acct.Zones = zones;

			#endregion Zones

			#region SiteGeneralDispatches

			var acctGDs = SosCrmDataContext.Instance.MS_AccountSiteGeneralDispatches.GetByAccountId(msAccount.AccountID);
			var siteGeneralDispatches = acctGDs.Select(gds => new SiteGeneralDispatches
			{
				Instructions = gds.Instructions
			}).ToList();
			if (siteGeneralDispatches.Count > 0) acct.SiteGeneralDispatches = siteGeneralDispatches;
			
			#endregion SiteGeneralDispatches

			#endregion Setup Account

			#region Setup CreditRequestXml

			CreditRequest credReq = null;
			var credRepTID = SosCrmDataContext.Instance.QL_CreditReportTransactionAndTokenViews.Get(qlQualifyCustomerInfo.CreditReportID);
			if (credRepTID != null)
			{
				credReq = new CreditRequest
				{
					CS = msAccount.IndustryAccount.Csid,
					SSN = qlReport.SSN,
					FirstName = aeMoniCustomer.FirstName,
					LastName = aeMoniCustomer.LastName,
					StreetNumber = qlReport.Address.StreetNumber,
					City = qlReport.Address.City,
					State = qlReport.Address.StateId,
					Zip = qlReport.Address.PostalCode,
					FICO = qlReport.Score.ToString(CultureInfo.InvariantCulture),
					TransactionID = credRepTID.TransactionID,
					Token = credRepTID.Token,
					DealerId = aeMoniCustomer.DealerId.ToString(CultureInfo.InvariantCulture),
					UserId = gpEmployeeId,
					RequestDate = DateTime.Now.ToString(CultureInfo.InvariantCulture)
				};
				
			}

			#endregion Setup CreditRequestXml

			// Convert to XML.
			var xmlizedString = acct.Serialize();
			var moniService = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);
			var creditRequestXml = credReq != null ? credReq.Serialize() : null;
			var purchaseInfoXml = string.Empty;
			var accountSubmitXml = new MS_AccountSubmitMsXml
			{
				AccountSubmitID =  msAccountSubmit.AccountSubmitID,
				Account = xmlizedString
			};
			if (!string.IsNullOrEmpty(creditRequestXml)) accountSubmitXml.CreditRequest = creditRequestXml;
			if (!string.IsNullOrEmpty(purchaseInfoXml)) accountSubmitXml.PurchaseInfo = purchaseInfoXml;
			accountSubmitXml.Save(gpEmployeeId);
			DataSet dsRaw;
			string confirmationNumber;
			string firstErrorMsg;
			if (!moniService.AccountOnlineTry(msAccount.IndustryAccount.Csid, xmlizedString, out dsRaw, out confirmationNumber, out firstErrorMsg, creditRequestXml, purchaseInfoXml))
			{
				msAccountSubmit.WasSuccessfull = false;
				msAccountSubmit.IndustryAccountId = msAccount.IndustryAccountId;
				msAccountSubmit.DateSubmitted = DateTime.UtcNow;
				msAccountSubmit.Message = firstErrorMsg;
				msAccountSubmit.Save(gpEmployeeId);

				var dsErrorsOnBoardAccount = Utils.ConvertDataSet<ErrorsOnBoardAccount>(dsRaw);
				var sb = new StringBuilder();
				foreach (ErrorsOnBoardAccount.TableRow row in dsErrorsOnBoardAccount.Tables[0].Rows)
				{
// ReSharper disable once UseObjectOrCollectionInitializer
					var submitItem = new MS_AccountSubmitM();
					submitItem.AccountSubmitId = msAccountSubmit.AccountSubmitID;
					submitItem.TableName = row.Istable_nameNull() ? null : row.table_name;
					submitItem.EntryId = row.Isentry_idNull() ? null : row.entry_id;
					submitItem.SiteNo = row.Issite_noNull() ? (int?) null : row.site_no;
					submitItem.CsNo = row.Iscs_noNull() ? null : row.cs_no;
					submitItem.ErrNo = row.Iserr_noNull() ? (int?) null : row.err_no;
					submitItem.MsgType = row.Ismsg_typeNull() ? (byte?) null : row.msg_type;
					submitItem.ErrText = row.Iserr_textNull() ? null : row.err_text;
					submitItem.ErrDate = row.Iserr_dateNull() ? (DateTime?) null : row.err_date;
					submitItem.CreatedBy = gpEmployeeId;
					submitItem.CreatedOn = DateTime.UtcNow;
					submitItem.Save(gpEmployeeId);

					if (row.msg_type == 1)
					{
						sb.AppendFormat("* Err#:{0} | {1}\r\n", row.err_no, row.err_text);
					}
				}

				return	new FosResult<MS_AccountSubmit>
				{
					Code = BaseErrorCodes.ErrorCodes.MSAccountOnboardError.Code()
					, Message = string.Format(BaseErrorCodes.ErrorCodes.MSAccountOnboardError.Message(), msAccount.AccountID, msAccount.IndustryAccount.Csid, sb)
					, Value = msAccountSubmit
				};
			}
			if (string.IsNullOrEmpty(confirmationNumber))
			{
				msAccountSubmit.WasSuccessfull = false;
				msAccountSubmit.DateSubmitted = DateTime.UtcNow;
				msAccountSubmit.Message = string.Format("Odd situation. The account appeared to go through but there is not confirmation number");
				msAccountSubmit.Save(gpEmployeeId);

				return new FosResult<MS_AccountSubmit>
				{
					Code = BaseErrorCodes.ErrorCodes.MSAccountOnboardMissingConfNumber.Code()
					, Message = string.Format(BaseErrorCodes.ErrorCodes.MSAccountOnboardMissingConfNumber.Message(), msAccount.AccountID, msAccount.IndustryAccount.Csid, firstErrorMsg)
					, Value = msAccountSubmit
				};
			}

			// ** Save successfull submission
			msAccountSubmit.WasSuccessfull = true;
			msAccountSubmit.IndustryAccountId = msAccount.IndustryAccountId;
			msAccountSubmit.DateSubmitted = DateTime.UtcNow;
			msAccountSubmit.Message = string.Format("Nexsense Confirmation #: {0}", msAccountSubmit.AccountSubmitID);
			msAccountSubmit.Save(gpEmployeeId);
			var dsOnBoardAccount = Utils.ConvertDataSet<ErrorsOnBoardAccount>(dsRaw);
			foreach (ErrorsOnBoardAccount.TableRow row in dsOnBoardAccount.Tables[0].Rows)
			{
// ReSharper disable once UseObjectOrCollectionInitializer
				var submitItem = new MS_AccountSubmitM();
				submitItem.AccountSubmitId = msAccountSubmit.AccountSubmitID;
				submitItem.TableName = row.table_name;
				submitItem.EntryId = row.entry_id;
				submitItem.SiteNo = row.site_no;
				submitItem.CsNo = row.cs_no;
				submitItem.ErrNo = row.err_no;
				submitItem.MsgType = row.msg_type;
				submitItem.ErrText = row.err_text;
				submitItem.ErrDate = row.err_date;
				submitItem.CreatedBy = gpEmployeeId;
				submitItem.CreatedOn = DateTime.UtcNow;
				submitItem.Save(gpEmployeeId);
			}

			// ** Save Successfull install information
			var salesInformation = SosCrmDataContext.Instance.MS_AccountSalesInformations.LoadByPrimaryKey(msAccount.AccountID);
			salesInformation.AccountSubmitId = msAccountSubmit.AccountSubmitID;
			salesInformation.SubmittedToCSDate = DateTime.UtcNow;
			salesInformation.InstallDate = DateTime.UtcNow;
			salesInformation.CsConfirmationNumber = confirmationNumber;
			salesInformation.Save(gpEmployeeId);

			// ** Setup result
			result.Code = BaseErrorCodes.ErrorCodes.MSAccountOnboardSuccessful.Code();
			result.Message = string.Format(BaseErrorCodes.ErrorCodes.MSAccountOnboardSuccessful.Message(), confirmationNumber);
			result.Value = msAccountSubmit;

			// ** Return result
			return result;

			#endregion WORK IN PROGRESS
		}

		public IFosResult<MS_AccountSubmit> AccountUpdate(long accountID, string gpEmployeeId)
		{
			throw new NotImplementedException();
		}

		public FosResult<List<IFosSignalHistoryItem>> GetSignalHistory(DateTime startDate, DateTime endDate, string csNo)
		{
			#region Get EventHistories

			// ** Initialize
			var result = new FosResult<List<IFosSignalHistoryItem>>();
			var moniService = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);
			var industryAccount = SosCrmDataContext.Instance.MS_IndustryAccounts.LoadSingle(SosCrmDataStoredProcedureManager.MS_IndustryAccountFindByCsID(csNo));

			// ** Check that their was a CSno that is existing in our system.
			if (industryAccount == null || !industryAccount.IsLoaded)
			{
				return new FosResult<List<IFosSignalHistoryItem>>
				{
					Code = 1,
					Message = string.Format("The csNo passed '{0}' was not found in our system.", csNo)
				};
			}

			DataSet dsRaw;
			Errors dsErrorsGet;
			string firstErrorMsgGet;


			if (moniService.GetDataTry(MS_MonitronicsEntity.MetaData.Event_HistoriesID, out dsRaw, out dsErrorsGet,
				out firstErrorMsgGet, csNo))
			{
				var ds = Utils.ConvertDataSet<GetEventHistories>(dsRaw);

				Errors dsErrors;
				string firstErrorMsg;

				// ** Save Signal history.
				var submitEvent = new MS_MonitronicsSubmitsGetData
				{
					EntityId = MS_MonitronicsEntity.MetaData.Event_HistoriesID,
					AccountId = industryAccount.AccountId,
					IsSuccess = true,
					CreatedOn = DateTime.UtcNow
				};
				submitEvent.Save("MS_SYSTEM");

				// ** Check if the error is in the dataset
				if (Utils.ErrorsTry(dsRaw, out dsErrors, out firstErrorMsg))
				{
					// ** Initialize
					submitEvent.IsSuccess = false;
					submitEvent.Save("MS_SYSTEM");

					// ** Save error messages
					foreach (Errors.TableRow row in dsErrors.Tables[0].Rows)
					{
						var msError = new MS_MonitronicsSubmitsGetDataError
						{
							SubmitsGetDataId = submitEvent.SubmitsGetDataID,
							ErrMsg = row.Iserr_msgNull() ? null : row.err_msg,
							CreatedOn = DateTime.UtcNow
						};
						msError.Save("MS_SYSTEM");
					}

					return new FosResult<List<IFosSignalHistoryItem>>
					{
						Code = 1,
						Message = string.Format("The first message generated from the GetSignalHistory is: {0}", firstErrorMsg)
					};
				}

				var count = 0;
				var listResult = new List<IFosSignalHistoryItem>();

				foreach (GetEventHistories.TableRow row in ds.Table.Rows)
				{
					try
					{
						// ** Save information
						var rowEvent = new MS_MonitronicsEntityEventHistory
						{
							SubmitsGetDataId = submitEvent.SubmitsGetDataID,
							IndustryAccountId = industryAccount.IndustryAccountID,
							CsNo = csNo,
							EventDate = row.Isevent_dateNull() ? (DateTime?)null : row.event_date,
							ZoneId = row.Iszone_idNull() ? null : row.zone_id,
							Area = row.IsareaNull() ? null : row.area,
							EventId = row.Isevent_idNull() ? null : row.event_id,
							UserName = row.Isuser_nameNull() ? null : row.user_name,
							ZoneStateId = row.Iszonestate_idNull() ? null : row.zonestate_id,
							match = row.IsmatchNull() ? null : row.match,
							computed = row.IscomputedNull() ? null : row.computed,
							CreatedOn = DateTime.UtcNow
						};
						rowEvent.Save("MS_SYSTEM");

						// ** Add to result list.
						listResult.Add(new FosMsSignalHistoryItem(row));

						/** Have to check for DBNull's */
						Debug.WriteLine(
							"Row#: {9} || CsNo: {0} | EventDate: {1} | ZoneId: {2} | Area: {3} | EventId: {4} | UserName: {5} | ZoneStateId: {6} | Match: {7} | Computed: {8}",
							!row.Iscs_noNull() ? row.cs_no : null,
							!row.Isevent_dateNull() ? (DateTime?)row.event_date : null,
							!row.Iszone_idNull() ? row.zone_id : null,
							!row.IsareaNull() ? row.area : null,
							!row.Isevent_idNull() ? row.event_id : null,
							!row.Isuser_nameNull() ? row.user_name : null,
							!row.Iszonestate_idNull() ? row.zonestate_id : null,
							!row.IsmatchNull() ? row.match : null,
							!row.IscomputedNull() ? row.computed : null,
							count);
						count++;
					}
					catch (Exception ex)
					{
						Debug.WriteLine("EventDate: {0} | Message: {1}", !row.Isevent_dateNull() ? (DateTime?)row.event_date : null, ex.Message);
						throw;
					}
				}

				result.Value = listResult;
				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				if (count == 0)
				{
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.MSAccountNoSignalHistoryFound.Message(), "Monitronics",
						csNo);
				}

				Console.WriteLine("RowCount: {0}", count);

			}
			else
			{
				result.Code = 1;
				result.Message = string.Format("The following error was generated: {0}.  \r\nThere are a total of {1} error(s) generated.", firstErrorMsgGet, dsErrorsGet.Tables[0].Rows.Count);
			}

			// ** Return result
			return result;

			#endregion Get EventHistories
		}

		public FosResult<bool> UpdateContacts(long accountId)
		{
//TODO: ANDRES			throw new NotImplementedException();
			return new FosResult<bool>{Code = BaseErrorCodes.ErrorCodes.Success.Code(), Message = BaseErrorCodes.ErrorCodes.Success.Message()};
		}

		public FosResult<object> TwoWayTestData(long accountId)
		{
			throw new NotImplementedException();
		}
		public FosResult<object> InitTwoWayTest(long accountId, string gpEmployeeId)
		{
			// ** Initialize
			var result = new FosResult<object>();
			var moniService = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);
			var msAccount = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(accountId);
			var msAccountSubmit = new MS_AccountSubmit();
			msAccountSubmit.AccountId = accountId;
			msAccountSubmit.AccountSubmitTypeId = (short) MS_AccountSubmitType.AccountSubmitTypeEnum.Initiate_Two_Way_Test;
			msAccountSubmit.IndustryAccountId = msAccount.IndustryAccountId;
			msAccountSubmit.MonitoringStationOSId = msAccount.IndustryAccount.ReceiverLine.MonitoringStationOSId;
			msAccountSubmit.GPTechId = msAccount.TechId;
			msAccountSubmit.DateSubmitted = DateTime.UtcNow;
			msAccountSubmit.WasSuccessfull = false;
			msAccountSubmit.CreatedBy = gpEmployeeId;
			msAccountSubmit.CreatedOn = DateTime.UtcNow;
			msAccountSubmit.Save(gpEmployeeId);

		    var deviceId = msAccount.GetMoniSysTypeId();
		    if (deviceId == null)
		    {
		        throw new Exception("This account does not have a panel associated with it that will return a clear Monitronics Device ID.");
		    }

			DataSet dsResult;
			if (!moniService.InitiateTwoWayTest(msAccountSubmit, msAccount.IndustryAccount.Csid, msAccount.GetMoniSysTypeId().SystemTypeID,
				out dsResult))
			{
				ErrorsOnBoardAccount dsErrors;
				string firstError, confNumber;
				if (Utils.ErrorsOnBoardAccountTry(dsResult, out dsErrors, out firstError, out confNumber))
				{
					foreach (ErrorsOnBoardAccount.TableRow row in dsErrors.Tables[0].Rows)
					{
// ReSharper disable once UseObjectOrCollectionInitializer
						var submitItem = new MS_AccountSubmitM();
						submitItem.AccountSubmitId = msAccountSubmit.AccountSubmitID;
						submitItem.TableName = row.table_name;
						submitItem.EntryId = row.entry_id;
						submitItem.SiteNo = row.Issite_noNull() ? (int?) null : row.site_no;
						submitItem.CsNo = row.cs_no;
						submitItem.ErrNo = row.err_no;
						submitItem.MsgType = row.msg_type;
						submitItem.ErrText = row.err_text;
						submitItem.ErrDate = row.err_date;
						submitItem.CreatedBy = gpEmployeeId;
						submitItem.CreatedOn = DateTime.UtcNow;
						submitItem.Save(gpEmployeeId);
					}

					result.Code = BaseErrorCodes.ErrorCodes.MSAccountInitTwoWayFailed.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.MSAccountInitTwoWayFailed.Message(),
						msAccount.IndustryAccount.Csid, firstError);
				}
			}
			else
			{
				msAccountSubmit.WasSuccessfull = true;
				msAccountSubmit.Save(gpEmployeeId);

				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
			}

			// ** Return result
			return result;
		}
		public FosResult<object> CompleteTwoWayTest(long accountId, string confirmedBy, string gpEmployeeId)
		{
			throw new NotImplementedException();
		}
		public FosResult<List<IFosDeviceTest>> ActiveTests(long accountId)
		{
			throw new NotImplementedException();
		}
		public FosResult<bool> ClearActiveTests(long accountId)
		{
			throw new NotImplementedException();
		}
		public FosResult<bool> ClearTest(long accountId, int testNum)
		{
			throw new NotImplementedException();
		}
		public FosResult<ISystemStatusInfo> ServiceStatus(long accountId, string gpEmployeeId)
		{
			#region Initialize

			var result = new FosResult<ISystemStatusInfo>();
			var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);
			var msAccount = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(accountId);
			
			#endregion Initialize

			try
			{
				DataSet dsRaw;
				Errors dsErrorsGet;
				string firstErrorMsgGet;
				if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Site_SystemsID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet, msAccount.IndustryAccount.Csid))
				{
					result.Code = BaseErrorCodes.ErrorCodes.MSAccountSystemInfoGetFailed.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.MSAccountSystemInfoGetFailed.Message(), accountId,
						msAccount.IndustryAccount.Csid, firstErrorMsgGet);
					return result;
				}

				// Save data
				var dsSiteSystemInfo = Utils.ConvertDataSet<GetSiteSystemInfo>(dsRaw);
				var systemStatusInfo = new SystemStatusInfo(false, true);
				foreach (GetSiteSystemInfo.TableRow row in dsSiteSystemInfo.Table.Rows)
				{
					var siteSystemInfo =
					SosCrmDataContext.Instance.MS_MonitronicsEntitySiteSystems.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySiteSystemInfoSave(
						msAccount.IndustryAccount.IndustryAccountID
						, row.Issite_nameNull() ? null : row.site_name
						, row.Issitetype_idNull() ? null : row.sitetype_id
						, row.Issitestat_idNull() ? null : row.sitestat_id
						, row.Issite_addr1Null() ? null : row.site_addr1
						, row.Issite_addr2Null() ? null : row.site_addr2
						, row.Iscity_nameNull() ? null : row.city_name
						, row.Iscounty_nameNull() ? null : row.county_name
						, row.Isstate_idNull() ? null : row.state_id
						, row.Iszip_codeNull() ? null : row.zip_code
						, row.Isphone1Null() ? null : row.phone1
						, row.Isext1Null() ? null : row.ext1
						, row.Isstreet_noNull() ? null : row.street_no
						, row.Isstreet_nameNull() ? null : row.street_name
						, row.Iscountry_nameNull() ? null : row.country_name
						, row.Istimezone_noNull() ? (int?) null : row.timezone_no
						, row.Istimezone_descrNull() ? null : row.timezone_descr
						, row.Isservco_noNull() ? (int?) null : row.servco_no
						, row.Isinstall_servco_noNull() ? null : row.install_servco_no
						, row.Iscspart_noNull() ? null : row.cspart_no
						, row.IssubdivisionNull() ? null : row.subdivision
						, row.Iscross_streetNull() ? null : row.cross_street
						, row.Iscodeword1Null() ? null : row.codeword1
						, row.Iscodeword2Null() ? null : row.codeword2
						, row.Isorig_install_dateNull() ? (DateTime?) null : row.orig_install_date
						, row.Islang_idNull() ? null : row.lang_id
						, row.Iscs_noNull() ? null : row.cs_no
						, row.Issystype_idNull() ? null : row.systype_id
						, row.Issec_systype_idNull() ? null : row.sec_systype_id
						, row.Ispanel_phoneNull() ? null : row.panel_phone
						, row.Ispanel_locationNull() ? null : row.panel_location
						, row.Isreceiver_phoneNull() ? null : row.receiver_phone
						, row.Isati_hoursNull() ? (short?) null : row.ati_hours
						, row.Isati_minutesNull() ? (byte?) null : row.ati_minutes
						, row.Ispanel_codeNull() ? null : row.panel_code
						, row.Istwoway_device_idNull() ? null : row.twoway_device_id
						, row.Isalkup_cs_noNull() ? null : row.alkup_cs_no
						, row.Isblkup_cs_noNull() ? null : row.blkup_cs_no
						, row.Isontest_flagNull() ? null : row.ontest_flag
						, row.Isontest_expire_dateNull() ? (DateTime?) null : row.ontest_expire_date
						, row.Isoos_flagNull() ? null : row.oos_flag
						, row.Isinstall_dateNull() ? (DateTime?) null : row.install_date
						, row.Ismonitor_typeNull() ? null : row.monitor_type
						, gpEmployeeId
					));
					systemStatusInfo = new SystemStatusInfo(siteSystemInfo.oos_flag.Equals("no"), siteSystemInfo.ontest_flag.Equals("yes"));
				}
				
				// ** Init successfull result
				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = systemStatusInfo;
			}
			catch (Exception ex)
			{
				result.Code = BaseErrorCodes.ErrorCodes.GeneralException.Code();
				result.Message = string.Format(BaseErrorCodes.ErrorCodes.GeneralException.Message(), "Moni CentralStation", ex.Message);
				throw;
			}

			// ** Return result
			return result;
		}
		public FosResult<string> SetServiceStatus(long accountId, string oosCat, DateTime startDate, string comment, string gpEmployeeId)
		{
			throw new NotImplementedException();
		}

		public FosResult<bool> GenerateMetaData(long? accountId = null, string username = "SYSTEM")
		{
			string firstErrorMsg;

			#region AgencyTypes
			if (!GetAgencyTypes(out firstErrorMsg, username))
				return new FosResult<bool>
				{
					Code = 1,
					Message = firstErrorMsg,
					Value = false
				};
			#endregion AgencyTypes

			#region Authorities
			if (!GetAuthorities(out firstErrorMsg, username))
				return new FosResult<bool>
				{
					Code = 1,
					Message = firstErrorMsg,
					Value = false
				};
			#endregion Authorities

			#region Events
			if (!GetEvents(out firstErrorMsg, username))
				return new FosResult<bool>
				{
					Code = 1,
					Message = firstErrorMsg,
					Value = false
				};
			#endregion Events

			#region SystemTypes
			if (!GetSystemTypes(out firstErrorMsg, username))
				return new FosResult<bool>
				{
					Code = 1,
					Message = firstErrorMsg,
					Value = false
				};
			#endregion SystemTypes

			#region SiteTypes
			if (!GetSiteTypes(out firstErrorMsg, username))
				return new FosResult<bool>
				{
					Code = 1,
					Message = firstErrorMsg,
					Value = false
				};
			#endregion SiteTypes

			#region ContactTypes
			if (!GetContactTypes(out firstErrorMsg, username))
				return new FosResult<bool>
				{
					Code = 1,
					Message = firstErrorMsg,
					Value = false
				};
			#endregion ContactTypes

			#region PhoneTypes
			if (!GetPhoneTypes(out firstErrorMsg, username))
				return new FosResult<bool>
				{
					Code = 1,
					Message = firstErrorMsg,
					Value = false
				};
			#endregion PhoneTypes

            #region Relations
            if (!GetRelations(out firstErrorMsg, username))
				return new FosResult<bool>
				{
					Code = 1,
					Message = firstErrorMsg,
					Value = false
				};
			#endregion Relations

            #region BusRules
            if (!GetBusRules(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion BusRules

            #region PartialBatches
            if (!GetPartialBatches(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion PartialBatches

            #region Options
            if (!GetOptions(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion Options

            #region OosCats
            if (!GetOosCats(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion OosCats

            #region Languages
            if (!GetLanguages(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion Languages

            #region NamePrefixes
            if (!GetNamePrefixes(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion NamePrefixes

            #region NameSuffixes
            if (!GetNameSuffixes(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion NameSuffixes

            #region Zips
            if (!GetZips(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion Zips

            #region TwoWays
			if (!GetTwoWays(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion TwoWays

            #region ZoneStates
            if (!GetZoneStates(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion ZoneStates

            #region Prefixes
            if (!GetPrefixes(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion Prefixes

            #region States
            if (!GetStates(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion States

            #region PermitTypes
            if (!GetPermitTypes(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion PermitTypes

            #region TestCats
            if (!GetTestCats(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion TestCats

			//#region ServiceCompany
			//if (!GetServiceCompany(out firstErrorMsg, username))
			//	return new FosResult<bool>
			//	{
			//		Code = 1,
			//		Message = firstErrorMsg,
			//		Value = false
			//	};
			//#endregion ServiceCompany


            #region CellProviders
            if (!GetCellProviders(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion CellProviders

            #region Agencies
            if (!GetAgencies(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion Agencies

            #region EquipEventXRef
            if (!GetEquipEventXRef(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion EquipEventXRef

            #region EquipmentLocations
            if (!GetEquipmentLocations(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion EquipmentLocations

            #region EquipmentTypes
            if (!GetEquipmentTypes(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion EquipmentTypes

            #region SecGroups
            if (!GetSecGroups(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion SecGroups

            #region SiteOptions
            if (!GetSiteOptions(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion SiteOptions

			#region SiteSystemOptions
			if (!GetSiteSystemOptions(out firstErrorMsg, username))
				return new FosResult<bool>
				{
					Code = 1,
					Message = firstErrorMsg,
					Value = false
				};

			#endregion SiteSystemOptions

			#region SystemTypeXRef
			if (!GetSystemTypeXRef(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion SystemTypeXRef

            #region CellServices
            if (!GetCellServices(out firstErrorMsg, username))
                return new FosResult<bool>
                {
                    Code = 1,
                    Message = firstErrorMsg,
                    Value = false
                };
            #endregion CellServices

            // ** Return result
			return new FosResult<bool>
			{
				Code = 0,
				Message = "Success",
				Value = true
			};
		}

		public FosResult<List<MS_DispatchAgency>> FindDispatchAgency(string agencyTypeId, string phone, string city, string state, string zip, string gpEmployeeId)
		{

			/** Initialize. */
			const string METHOD_NAME = "FindDispatchAgency";
			var result = new FosResult<List<MS_DispatchAgency>>
			{
				Code = BaseErrorCodes.ErrorCodes.Initializing.Code(),
				Message = string.Format(BaseErrorCodes.ErrorCodes.Initializing.Message(), METHOD_NAME)
			};
			var getAgencies = new NXS.Logic.MonitoringStations.Models.Get.GetAgencies { GetAgency = new AgencyInfo() };

			// ** Check arguments
			var sb = new StringBuilder();
//			if (!string.IsNullOrEmpty(phone)) sb.Append(" phone,");
			if (!string.IsNullOrEmpty(zip)) sb.Append(" zip,");
//			if (!string.IsNullOrEmpty(agencyTypeId)) sb.Append(" agencyTypeId");
			if (string.IsNullOrEmpty(sb.ToString()))
			{
				result.Code = BaseErrorCodes.ErrorCodes.ArgumentValidation.Code();
				result.Message = string.Format(BaseErrorCodes.ErrorCodes.ArgumentValidation.Message(), METHOD_NAME, sb);
				return result;
			}

			try
			{
				// ** Createa an MS_AccountsSubmit
				var acctSubmits = new MS_MonitronicsSubmitsGetData
				{
					EntityId = MS_MonitronicsEntity.MetaData.AgenciesID,
					IsSuccess = true,
					CreatedOn = DateTime.UtcNow
				};
				acctSubmits.Save(gpEmployeeId);

				// ** Create XML argument
				//getAgencies.GetAgency.AgencyTypeId = agencyTypeId;
				//if (string.IsNullOrEmpty(phone))
					getAgencies.GetAgency.ZipCode = zip;
				//else
				//	getAgencies.GetAgency.Phone1 = phone;
				var xmlData = getAgencies.Serialize();
				var moniService = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);
				DataSet dsAgenciesRaw;
				Errors dsErrorsGet;
				string firstErrorMsgGet;
				if (!moniService.GetDataTry(MS_MonitronicsEntity.MetaData.AgenciesID, out dsAgenciesRaw,
					out dsErrorsGet, out firstErrorMsgGet, null, xmlData))
				{
					// Save errors
					acctSubmits.IsSuccess = false;
					acctSubmits.Save(gpEmployeeId);
					foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
					{
						var msGetError = new MS_MonitronicsSubmitsGetDataError
						{
							SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
							ErrMsg = row.err_msg,
							CreatedOn = DateTime.UtcNow
						};

						msGetError.Save(gpEmployeeId);
					}
					// ** Check that there was a result found.
					result.Code = BaseErrorCodes.ErrorCodes.CSLookupFailed.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.CSLookupFailed.Message(), "Get Agencies Entities", firstErrorMsgGet);
					return result;
				}

				// ** Check that we got a result back.
				if (dsAgenciesRaw.Tables[0].Rows.Count == 0)
				{
					// ** Check that there was a result found.
					result.Code = BaseErrorCodes.ErrorCodes.CSLookupNotFound.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.CSLookupNotFound.Message(), "Get Agencies Entities");
					return result;
				}

				// ** Check to see if the agency is in our system
				var dsAgencies = Utils.ConvertDataSet<GetAgencies>(dsAgenciesRaw);
				var msMoniEntityDAgency = new MS_MonitronicsEntityAgencyCollection();
				foreach (GetAgencies.TableRow agency in dsAgencies.Table.Rows)
				{
					msMoniEntityDAgency.Add(SosCrmDataContext.Instance.MS_MonitronicsEntityAgencies.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityAgenciesSave(agency.agency_no
						, agency.agencytype_id
						, agency.agency_name
						, agency.city_name
						, agency.state_id
						, zip ?? agency.zip_code
						, agency.phone1
						, gpEmployeeId)));
				}

				// ** Save all entities to the Dispatch Agency table.
				var msDa = new List<MS_DispatchAgency>();
				foreach (var entityAgency in msMoniEntityDAgency)
				{
					foreach (var rawItem in SosCrmDataContext.Instance.MS_DispatchAgencies.LoadCollection(
						SosCrmDataStoredProcedureManager.MS_DispatchAgenciesSaveFromMoniEntity(entityAgency.EntityAgenciesID, gpEmployeeId)))
					{
						msDa.Add(rawItem);
					}
				}

				// ** Add to account
				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = string.Format(BaseErrorCodes.ErrorCodes.Success.Message(), METHOD_NAME);
				result.Value = msDa;

			}
			catch (Exception ex)
			{
				result.Code = BaseErrorCodes.ErrorCodes.ExceptionThrown.Code();
				result.Message = string.Format(BaseErrorCodes.ErrorCodes.ExceptionThrown.Message(), METHOD_NAME, ex.Message);
			}
			// ** Return result
			return result;
		}

		#region Private 

		private bool GetAgencyTypes(out string firstErrorMsgGet, string username = "SYSTEM")
		{
			#region AgencyTypes
			// ** Initialize
			var result = false;
			firstErrorMsgGet = null;
			var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

			try
			{
				// ** Createa an MS_AccountsSubmit
				var acctSubmits = new MS_MonitronicsSubmitsGetData
				{
					EntityId = MS_MonitronicsEntity.MetaData.AgencyTypesID,
					IsSuccess = true,
					CreatedOn = DateTime.UtcNow
				};
				acctSubmits.Save(username);

				// ** Call the getData for AgencyTypes.
				DataSet dsRaw;
				Errors dsErrorsGet;
				if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.AgencyTypesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
				{
					// Save errors
					acctSubmits.IsSuccess = false;
					acctSubmits.Save(username);
					foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
					{
						var msGetError = new MS_MonitronicsSubmitsGetDataError
						{
							SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
							ErrMsg = row.err_msg,
							CreatedOn = DateTime.UtcNow
						};

						msGetError.Save(username);
					}
				}
				else
				{
					// Save data
					var dsAgencyTypes = Utils.ConvertDataSet<GetAgencyTypes>(dsRaw);
					// // ** This Nukes the tables or sets all entities to deleted.
					SosCrmDataContext.Instance.MS_MonitronicsEntityAgencyTypes.LoadCollection(
						SosCrmDataStoredProcedureManager.MS_MonitronicsEntityAgencyTypesNuke());
					foreach (GetAgencyTypes.TableRow row in dsAgencyTypes.Table.Rows)
					{
						SosCrmDataContext.Instance.MS_MonitronicsEntityAgencyTypes.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityAgencyTypesSave(row.agencytype_id, row.descr, username));
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.AgencyTypesID, ex.Message);
			}

			// ** Return result
			return result;
			#endregion AgencyTypes
		}

		private bool GetAuthorities(out string firstErrorMsgGet, string username = "SYSTEM")
		{
			#region Authorities
			// ** Initialize
			var result = false;
			firstErrorMsgGet = null;
			var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

			try
			{
				// ** Createa an MS_AccountsSubmit
				var acctSubmits = new MS_MonitronicsSubmitsGetData
				{
					EntityId = MS_MonitronicsEntity.MetaData.AuthoritiesID,
					IsSuccess = true,
					CreatedOn = DateTime.UtcNow
				};
				acctSubmits.Save(username);

				// ** Call the getData for Authorities.
				DataSet dsRaw;
				Errors dsErrorsGet;
				if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.AuthoritiesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
				{
					// Save errors
					acctSubmits.IsSuccess = false;
					acctSubmits.Save(username);
					foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
					{
						var msGetError = new MS_MonitronicsSubmitsGetDataError
						{
							SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
							ErrMsg = row.err_msg,
							CreatedOn = DateTime.UtcNow
						};

						msGetError.Save(username);
					}
				}
				else
				{
					// Save data
					var dsAUthorities = Utils.ConvertDataSet<GetAuthorities>(dsRaw);
					SosCrmDataContext.Instance.MS_MonitronicsEntityAuthorities.LoadCollection(
						SosCrmDataStoredProcedureManager.MS_MonitronicsEntityAuthoritiesNuke());
					foreach (GetAuthorities.TableRow row in dsAUthorities.Table.Rows)
					{
						SosCrmDataContext.Instance.MS_MonitronicsEntityAuthorities.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityAuthoritiesSave(row.auth_id, row.descr, username));
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.AuthoritiesID, ex.Message);
			}

			// ** Return result
			return result;
			#endregion Authorities
		}

		private bool GetEvents(out string firstErrorMsgGet, string username = "SYSTEM")
		{
			#region Events
			// ** Initialize
			var result = false;
			firstErrorMsgGet = null;
			var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

			try
			{
				// ** Createa an MS_AccountsSubmit
				var acctSubmits = new MS_MonitronicsSubmitsGetData
				{
					EntityId = MS_MonitronicsEntity.MetaData.EventsID,
					IsSuccess = true,
					CreatedOn = DateTime.UtcNow
				};
				acctSubmits.Save(username);

				// ** Call the getData for Events.
				DataSet dsRaw;
				Errors dsErrorsGet;
				if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.EventsID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
				{
					// Save errors
					acctSubmits.IsSuccess = false;
					acctSubmits.Save(username);
					foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
					{
						var msGetError = new MS_MonitronicsSubmitsGetDataError
						{
							SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
							ErrMsg = row.err_msg,
							CreatedOn = DateTime.UtcNow
						};

						msGetError.Save(username);
					}
				}
				else
				{
					// Save data
					var dsEvents = Utils.ConvertDataSet<GetEvents>(dsRaw);
					SosCrmDataContext.Instance.MS_MonitronicsEntityEvents.LoadCollection(
						SosCrmDataStoredProcedureManager.MS_MonitronicsEntityEventsNuke());
					foreach (GetEvents.TableRow row in dsEvents.Table.Rows)
					{
						SosCrmDataContext.Instance.MS_MonitronicsEntityEvents.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityEventsSave(row.event_id, row.descr, username));
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.EventsID, ex.Message);
			}

			// ** Return result
			return result;
			#endregion Events
		}

		private bool GetSystemTypes(out string firstErrorMsgGet, string username = "SYSTEM")
		{
			#region System Types
			// ** Initialize
			var result = false;
			firstErrorMsgGet = null;
			var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

			try
			{
				// ** Createa an MS_AccountsSubmit
				var acctSubmits = new MS_MonitronicsSubmitsGetData
				{
					EntityId = MS_MonitronicsEntity.MetaData.System_TypesID,
					IsSuccess = true,
					CreatedOn = DateTime.UtcNow
				};
				acctSubmits.Save(username);

				// ** Call the getData for Events.
				DataSet dsRaw;
				Errors dsErrorsGet;
				if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.System_TypesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
				{
					// Save errors
					acctSubmits.IsSuccess = false;
					acctSubmits.Save(username);
					foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
					{
						var msGetError = new MS_MonitronicsSubmitsGetDataError
						{
							SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
							ErrMsg = row.err_msg,
							CreatedOn = DateTime.UtcNow
						};

						msGetError.Save(username);
					}
				}
				else
				{
					// Save data
					var dsSysTypes = Utils.ConvertDataSet<GetSysTypes>(dsRaw);
					SosCrmDataContext.Instance.MS_MonitronicsEntitySystemTypes.LoadCollection(
						SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySystemTypesNuke());
					foreach (GetSysTypes.TableRow row in dsSysTypes.Table.Rows)
					{
						SosCrmDataContext.Instance.MS_MonitronicsEntitySystemTypes.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySystemTypesSave(row.systype_id, row.descr, username));
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.System_TypesID, ex.Message);
			}

			// ** Return result
			return result;
			#endregion System Types
		}

		private bool GetSiteTypes(out string firstErrorMsgGet, string username = "SYSTEM")
		{
			#region SiteTypes
			// ** Initialize
			var result = false;
			firstErrorMsgGet = null;
			var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

			try
			{
				// ** Createa an MS_AccountsSubmit
				var acctSubmits = new MS_MonitronicsSubmitsGetData
				{
					EntityId = MS_MonitronicsEntity.MetaData.Site_TypesID,
					IsSuccess = true,
					CreatedOn = DateTime.UtcNow
				};
				acctSubmits.Save(username);

				// ** Call the getData for Events.
				DataSet dsRaw;
				Errors dsErrorsGet;
				if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Site_TypesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
				{
					// Save errors
					acctSubmits.IsSuccess = false;
					acctSubmits.Save(username);
					foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
					{
						var msGetError = new MS_MonitronicsSubmitsGetDataError
						{
							SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
							ErrMsg = row.err_msg,
							CreatedOn = DateTime.UtcNow
						};

						msGetError.Save(username);
					}
				}
				else
				{
					// Save data
					var dsSysTypes = Utils.ConvertDataSet<GetSiteTypes>(dsRaw);
					SosCrmDataContext.Instance.MS_MonitronicsEntitySiteTypes.LoadCollection(
						SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySiteTypesNuke());
					foreach (GetSiteTypes.TableRow row in dsSysTypes.Table.Rows)
					{
						SosCrmDataContext.Instance.MS_MonitronicsEntitySiteTypes.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySiteTypesSave(row.sitetype_id, row.descr, row.site_kind, username));
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Site_TypesID, ex.Message);
			}

			// ** Return result
			return result;
			#endregion SiteTypes
		}

		private bool GetContactTypes(out string firstErrorMsgGet, string username = "SYSTEM")
		{
			#region Contact Types
			// ** Initialize
			var result = false;
			firstErrorMsgGet = null;
			var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

			try
			{
				// ** Createa an MS_AccountsSubmit
				var acctSubmits = new MS_MonitronicsSubmitsGetData
				{
					EntityId = MS_MonitronicsEntity.MetaData.Contact_TypesID,
					IsSuccess = true,
					CreatedOn = DateTime.UtcNow
				};
				acctSubmits.Save(username);

				// ** Call the getData for Events.
				DataSet dsRaw;
				Errors dsErrorsGet;
				if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Contact_TypesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
				{
					// Save errors
					acctSubmits.IsSuccess = false;
					acctSubmits.Save(username);
					foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
					{
						var msGetError = new MS_MonitronicsSubmitsGetDataError
						{
							SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
							ErrMsg = row.err_msg,
							CreatedOn = DateTime.UtcNow
						};

						msGetError.Save(username);
					}
				}
				else
				{
					// Save data
					var dsSysTypes = Utils.ConvertDataSet<GetConTypes>(dsRaw);
					SosCrmDataContext.Instance.MS_MonitronicsEntityContactTypes.LoadCollection(
						SosCrmDataStoredProcedureManager.MS_MonitronicsEntityContactTypesNuke());
					foreach (GetConTypes.TableRow row in dsSysTypes.Table.Rows)
					{
						SosCrmDataContext.Instance.MS_MonitronicsEntityContactTypes.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityContactTypesSave(row.ctactype_id, row.descr, username));
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Contact_TypesID, ex.Message);
			}

			// ** Return result
			return result;

			#endregion Contact Types
		}

		private bool GetPhoneTypes(out string firstErrorMsgGet, string username = "SYSTEM")
		{
			#region Phone Types
			// ** Initialize
			var result = false;
			firstErrorMsgGet = null;
			var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

			try
			{
				// ** Createa an MS_AccountsSubmit
				var acctSubmits = new MS_MonitronicsSubmitsGetData
				{
					EntityId = MS_MonitronicsEntity.MetaData.Phone_TypesID,
					IsSuccess = true,
					CreatedOn = DateTime.UtcNow
				};
				acctSubmits.Save(username);

				// ** Call the getData for Events.
				DataSet dsRaw;
				Errors dsErrorsGet;
				if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Phone_TypesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
				{
					// Save errors
					acctSubmits.IsSuccess = false;
					acctSubmits.Save(username);
					foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
					{
						var msGetError = new MS_MonitronicsSubmitsGetDataError
						{
							SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
							ErrMsg = row.err_msg,
							CreatedOn = DateTime.UtcNow
						};

						msGetError.Save(username);
					}
				}
				else
				{
					// Save data
					var dsSysTypes = Utils.ConvertDataSet<GetPhoneTypes>(dsRaw);
					SosCrmDataContext.Instance.MS_MonitronicsEntityPhoneTypes.LoadCollection(
						SosCrmDataStoredProcedureManager.MS_MonitronicsEntityPhoneTypesNuke());
					foreach (GetPhoneTypes.TableRow row in dsSysTypes.Table.Rows)
					{
						SosCrmDataContext.Instance.MS_MonitronicsEntityPhoneTypes.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityPhoneTypesSave(row.phonetype_id, row.descr, row.method, username));
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Phone_TypesID, ex.Message);
			}

			// ** Return result
			return result;

			#endregion Phone Types
		}

		private bool GetRelations(out string firstErrorMsgGet, string username = "SYSTEM")
		{
			#region Relations
			// ** Initialize
			var result = false;
			firstErrorMsgGet = null;
			var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

			try
			{
				// ** Createa an MS_AccountsSubmit
				var acctSubmits = new MS_MonitronicsSubmitsGetData
				{
					EntityId = MS_MonitronicsEntity.MetaData.RelationsID,
					IsSuccess = true,
					CreatedOn = DateTime.UtcNow
				};
				acctSubmits.Save(username);

				// ** Call the getData for Events.
				DataSet dsRaw;
				Errors dsErrorsGet;
				if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.RelationsID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
				{
					// Save errors
					acctSubmits.IsSuccess = false;
					acctSubmits.Save(username);
					foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
					{
						var msGetError = new MS_MonitronicsSubmitsGetDataError
						{
							SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
							ErrMsg = row.err_msg,
							CreatedOn = DateTime.UtcNow
						};

						msGetError.Save(username);
					}
				}
				else
				{
					// Save data
					var dsSysTypes = Utils.ConvertDataSet<GetRelations>(dsRaw);
					SosCrmDataContext.Instance.MS_MonitronicsEntityRelations.LoadCollection(
						SosCrmDataStoredProcedureManager.MS_MonitronicsEntityRelationsNuke());
					foreach (GetRelations.TableRow row in dsSysTypes.Table.Rows)
					{
						SosCrmDataContext.Instance.MS_MonitronicsEntityRelations.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityRelationsSave(row.relation_id, row.descr, username));
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.RelationsID, ex.Message);
			}

			// ** Return result
			return result;

			#endregion Relations
		}

        private bool GetBusRules(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region BusRules
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Bus_RulesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Bus_RulesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetBusRules>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityBusRules.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityBusRulesNuke());
                    foreach (GetBusRules.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityBusRules.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityBusRulesSave(row.err_no, row.table_name, row.busrule, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Bus_RulesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion BusRules
        }

        private bool GetPartialBatches(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region PartialBatches
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Partial_BatchesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Partial_BatchesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetPartialBatches>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityPartialBatches.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityPartialBatchesNuke());
                    foreach (GetPartialBatches.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityPartialBatches.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityPartialBatchesSave(row.wsi_batch_no, row.cs_no, row.site_name, row.servco_no, row.mm_change_date, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Partial_BatchesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion PartialBatches
        }

        private bool GetOptions(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region Options
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.OptionsID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.OptionsID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetOptions>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityOptions.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityOptionsNuke());
                    foreach (GetOptions.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityOptions.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityOptionsSave(row.option_id, row.Usage, row.descr, row.valid_value, row.value_descr, row.value_required, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.OptionsID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion Options
        }

        private bool GetOosCats(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region OosCats
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.OosCatsID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.OosCatsID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetOosCats>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityOosCats.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityOosCatsNuke());
                    foreach (GetOosCats.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityOosCats.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityOosCatsSave(row.ooscat_id, row.descr, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.OosCatsID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion OosCats
        }

        /***
         * Populates the MS_MonitronicsEntityLanguages table with hooey from their server
         ***/
        private bool GetLanguages(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region Languages
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.LanguagesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.LanguagesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetLanguages>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityLanguages.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityLanguagesNuke());
                    foreach (GetLanguages.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityLanguages.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityLanguagesSave(row.lang_id, row.descr, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.LanguagesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion Languages
        }

        /***
         * Populates the MS_MonitronicsEntityNamePrefixes table with hooey from their server
         ***/
        private bool GetNamePrefixes(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region NamePrefixes
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Name_PrefixesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Name_PrefixesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetNamePrefixes>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityNamePrefixes.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityNamePrefixesNuke());
                    foreach (GetNamePrefixes.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityNamePrefixes.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityNamePrefixesSave(row.prefix, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Name_PrefixesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion NamePrefixes
        }

        /***
         * Populates the MS_MonitronicsEntityNameSuffixes table with hooey from their server
         ***/
        private bool GetNameSuffixes(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region NameSuffixes
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Name_SuffixesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Name_SuffixesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetNameSuffixes>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityNameSuffixes.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityNameSuffixesNuke());
                    foreach (GetNameSuffixes.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityNameSuffixes.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityNameSuffixesSave(row.suffix, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Name_SuffixesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion NameSuffixes
        }

        /***
         * Populates the MS_MonitronicsEntityZips table with hooey from their server
         ***/
        private bool GetZips(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region Zips
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Create an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.ZipsID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // Delete all existing data
                SosCrmDataContext.Instance.MS_MonitronicsEntityZips.LoadCollection(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityZipsNuke());

                // Open file with valid US zip codes (this should be moved to a database table later
                StreamReader sr = new StreamReader(new FileStream(@"C:\Users\jjenne.NSUTLTITM004\Documents\states-and-counties.csv", FileMode.Open));
                while (!sr.EndOfStream)
                {
                    // get one zip code from file stream
                    String unique = sr.ReadLine();
                    if (String.IsNullOrEmpty(unique))
                        continue;
                    String[] parts = unique.Split(',');
                    String state = parts[0];
                    String county = parts[1];

                    String xmlzip = "<?xml version=\"1.0\"?><GetZipCodes xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><GetZipCode state_id=\"" + state.ToUpper() + "\" county_name=\"" + county.ToUpper() + "\" /></GetZipCodes>";

                    // ** Call the getData for Events.
                    DataSet dsRaw;
                    Errors dsErrorsGet;
                    if (
                        !services.GetDataTry(MS_MonitronicsEntity.MetaData.ZipsID, out dsRaw, out dsErrorsGet,
                            out firstErrorMsgGet, null, xmlzip))
                    {
                        // Save errors
                        acctSubmits.IsSuccess = false;
                        acctSubmits.Save(username);
                        foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                        {
                            var msGetError = new MS_MonitronicsSubmitsGetDataError
                            {
                                SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                                ErrMsg = row.err_msg,
                                CreatedOn = DateTime.UtcNow
                            };

                            msGetError.Save(username);
                        }
                    }
                    else
                    {
                        // Save data
                        var dsSysTypes = Utils.ConvertDataSet<GetZips>(dsRaw);
                        foreach (GetZips.TableRow row in dsSysTypes.Table.Rows)
                        {
                            SosCrmDataContext.Instance.MS_MonitronicsEntityZips.LoadSingle(
                                SosCrmDataStoredProcedureManager.MS_MonitronicsEntityZipsSave(row.city_name,
                                    row.county_name, row.state_id, row.zip_code, username));
                        }
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.ZipsID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion Zips
        }

        /***
         * Populates the MS_MonitronicsEntityTwoWays table with hooey from their server
         ***/
        private bool GetTwoWays(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region TwoWays
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Two_WaysID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Two_WaysID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetTwoWays>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityTwoWays.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityTwoWaysNuke());
                    foreach (GetTwoWays.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityTwoWays.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityTwoWaysSave(row.twoway_device_id, row.descr, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Two_WaysID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion TwoWays
        }

        /***
         * Populates the MS_MonitronicsEntityZoneStates table with hooey from their server
         ***/
        private bool GetZoneStates(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region ZoneStates
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Zone_StatesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Zone_StatesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetZoneStates>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityZoneStates.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityZoneStatesNuke());
                    foreach (GetZoneStates.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityZoneStates.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityZoneStatesSave(row.zonestate_id, row.descr, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Zone_StatesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion ZoneStates
        }

        /***
         * Populates the MS_MonitronicsEntityPrefixes table with hooey from their server
         ***/
        private bool GetPrefixes(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region Prefixes
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.PrefixesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.PrefixesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetPrefixes>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityPrefixes.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityPrefixesNuke());
                    foreach (GetPrefixes.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityPrefixes.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityPrefixesSave(row.cell_flag, row.csno_len, row.cm_purchase, row.servco_no, row.cell_provider, row.systype_id, row.co_no, row.branded_flag, row.receiver_phone, row.alarmnet_citycs, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.PrefixesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion Prefixes
        }


        /***
         * Populates the MS_MonitronicsEntityStates table with hooey from their server
         ***/
        private bool GetStates(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region States
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.StatesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.StatesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetStates>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityStates.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityStatesNuke());
                    foreach (GetStates.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityStates.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityStatesSave(row.state_id, row.state_name, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.StatesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion States
        }

        /***
         * Populates the MS_MonitronicsEntityPermitTypes table with hooey from their server
         ***/
        private bool GetPermitTypes(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region PermitTypes
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Permit_TypesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Permit_TypesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetPermitTypes>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityPermitTypes.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityPermitTypesNuke());
                    foreach (GetPermitTypes.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityPermitTypes.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityPermitTypesSave(row.permtype_id, row.descr, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Permit_TypesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion PermitTypes
        }

        /***
         * Populates the MS_MonitronicsEntityTestCats table with hooey from their server
         ***/
        private bool GetTestCats(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region TestCats
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Test_CategoriesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Test_CategoriesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetTestCats>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityTestCats.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityTestCatsNuke());
                    foreach (GetTestCats.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityTestCats.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityTestCatsSave(row.testcat_id, row.descr, row.default_hours, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Test_CategoriesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion TestCats
        }

        /***
         * Populates the MS_MonitronicsEntityCellProviders table with hooey from their server
         ***/
        private bool GetCellProviders(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region CellProviders
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Cell_ProvidersID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Cell_ProvidersID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetCellProviders>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityCellProviders.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityCellProvidersNuke());
                    foreach (GetCellProviders.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityCellProviders.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityCellProvidersSave(row.cell_provider, row.descr, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Cell_ProvidersID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion CellProviders
        }

        /***
         * Populates the MS_MonitronicsEntityAgencies table with hooey from their server
         ***/
        private bool GetAgencies(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region Agencies
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.AgenciesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // Delete all existing data
                SosCrmDataContext.Instance.MS_MonitronicsEntityAgencies.LoadCollection(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityAgenciesNuke());

                // Open file with valid US zip codes (this should be moved to a database table later
                var sr = new StreamReader(new FileStream(@"C:\Users\jjenne.NSUTLTITM004\Documents\free-zipcode-database.txt", FileMode.Open));
                while (!sr.EndOfStream)
                {
                    // get one zip code from file stream
                    String zip = sr.ReadLine();

                    // pick up where we left off before the db server crapped.   TEMPORARY!
                    if (String.Compare(zip, "30237", StringComparison.Ordinal) < 1)
                        continue;

                    // check if this zip code has already been populated in database
                    //MS_MonitronicsEntityAgency tmp = SosCrmDataContext.Instance.MS_MonitronicsEntityAgencies.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityAgenciesGetForZip(zip));

                    // if this zip code hasn't been populated yet, get it from Monitronics (this section should be removed after the first time this has finished running so we can overwrite data with new data periodically)
                    //if (tmp == null)
                    //{
                        String xmlzip =
                            "<?xml version=\"1.0\"?><GetAgencies xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><GetAgency zip_code=\"" +
                            zip + "\" /></GetAgencies>";

                        // ** Call the getData for Events.
                        DataSet dsRaw;
                        Errors dsErrorsGet;
                        if (
                            !services.GetDataTry(MS_MonitronicsEntity.MetaData.AgenciesID, out dsRaw, out dsErrorsGet,
                                out firstErrorMsgGet, null, xmlzip))
                        {
                            // Save errors
                            acctSubmits.IsSuccess = false;
                            acctSubmits.Save(username);
                            foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                            {
                                var msGetError = new MS_MonitronicsSubmitsGetDataError
                                {
                                    SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                                    ErrMsg = row.err_msg,
                                    CreatedOn = DateTime.UtcNow
                                };

                                msGetError.Save(username);
                            }
                        }
                        else
                        {
                            // Save data
                            var dsSysTypes = Utils.ConvertDataSet<GetAgencies>(dsRaw);
                            foreach (GetAgencies.TableRow row in dsSysTypes.Table.Rows)
                            {
                                SosCrmDataContext.Instance.MS_MonitronicsEntityAgencies.LoadSingle(
                                    SosCrmDataStoredProcedureManager.MS_MonitronicsEntityAgenciesSave(row.agency_no,
                                        row.agencytype_id, row.agency_name, row.city_name, row.state_id, zip,
                                        row.phone1, username));
                            }
                            result = true;
                        }
                    //}
                }

                sr.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.AgenciesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion Agencies
        }

        /***
         * Populates the MS_MonitronicsEntityEquipEventXRef table with hooey from their server
         ***/
        private bool GetEquipEventXRef(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region EquipEventXRef
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Zone_Equipment_EventsID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Zone_Equipment_EventsID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetEquipEventXref>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityEquipEventXRefs.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityEquipEventXRefNuke());
                    foreach (GetEquipEventXref.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityEquipEventXRefs.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityEquipEventXRefSave(row.equiptype_id, row.event_id, row.site_kind, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Zone_Equipment_EventsID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion EquipEventXRef
        }

        /***
         * Populates the MS_MonitronicsEntityEquipmentLocations table with hooey from their server
         ***/
        private bool GetEquipmentLocations(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region EquipmentLocations
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Equipment_LocationsID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Equipment_LocationsID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetEquipLocations>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityEquipmentLocations.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityEquipmentLocationsNuke());
                    foreach (GetEquipLocations.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityEquipmentLocations.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityEquipmentLocationsSave(row.equiploc_id, row.descr, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Equipment_LocationsID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion EquipmentLocations
        }

        /***
         * Populates the MS_MonitronicsEntityEquipmentTypes table with hooey from their server
         ***/
        private bool GetEquipmentTypes(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region EquipmentTypes
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Equipment_TypesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Equipment_TypesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetEquipTypes>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntityEquipmentTypes.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntityEquipmentTypesNuke());
                    foreach (GetEquipTypes.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntityEquipmentTypes.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityEquipmentTypesSave(row.equiptype_id, row.descr, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Equipment_TypesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion EquipmentTypes
        }

        /***
         * Populates the MS_MonitronicsEntitySecGroups table with hooey from their server
         ***/
        private bool GetSecGroups(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region SecGroups
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.SecGroupsID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.SecGroupsID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetSecGroups>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntitySecGroups.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySecGroupsNuke());
                    foreach (GetSecGroups.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntitySecGroups.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySecGroupsSave(row.sec_group, row.sec_level, row.all_users, row.all_accounts, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.SecGroupsID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion SecGroups
        }

        /***
         * Populates the MS_MonitronicsEntitySiteOptions table with hooey from their server
         ***/
        private bool GetSiteOptions(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region SiteOptions
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Site_OptionsID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Site_OptionsID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetSiteOptions>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntitySiteOptions.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySiteOptionsNuke());
                    foreach (GetSiteOptions.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntitySiteOptions.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySiteOptionsSave(row.cs_no, row.option_id, row.option_value, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Site_OptionsID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion SiteOptions
        }

		/***
		 * Populates GetSiteSystemOptions
		 ***/
		private bool GetSiteSystemOptions(out string firstErrorMsgGet, string username = "SYSTEM")
		{
			#region SiteSystemOptions
			// ** Initialize
			var result = false;
			firstErrorMsgGet = null;
			var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

			try
			{
				// ** Createa an MS_AccountsSubmit
				var acctSubmits = new MS_MonitronicsSubmitsGetData
				{
					EntityId = MS_MonitronicsEntity.MetaData.Site_OptionsID,
					IsSuccess = true,
					CreatedOn = DateTime.UtcNow
				};
				acctSubmits.Save(username);

				// ** Call the getData for Events.
				DataSet dsRaw;
				Errors dsErrorsGet;
				if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Site_System_OptionsID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
				{
					// Save errors
					acctSubmits.IsSuccess = false;
					acctSubmits.Save(username);
					foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
					{
						var msGetError = new MS_MonitronicsSubmitsGetDataError
						{
							SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
							ErrMsg = row.err_msg,
							CreatedOn = DateTime.UtcNow
						};

						msGetError.Save(username);
					}
				}
				else
				{
					// Save data
					var dsSysTypes = Utils.ConvertDataSet<NXS.Logic.MonitoringStations.Schemas.GetSiteSystemOptions>(dsRaw);
					SosCrmDataContext.Instance.MS_MonitronicsEntitySiteSystemOptions.LoadCollection(
						SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySiteSystemOptionsNuke());
					foreach (GetSiteOptions.TableRow row in dsSysTypes.Table.Rows)
					{
						SosCrmDataContext.Instance.MS_MonitronicsEntitySiteOptions.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySiteOptionsSave(row.cs_no, row.option_id, row.option_value, username));
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Site_OptionsID, ex.Message);
			}

			// ** Return result
			return result;

			#endregion SiteSystemOptions
		}

		/***
         * Populates the MS_MonitronicsEntitySystemTypeXRef table with hooey from their server
         ***/
		private bool GetSystemTypeXRef(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region SystemTypeXRef
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.System_Type_XrefID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);

                // ** Call the getData for Events.
                DataSet dsRaw;
                Errors dsErrorsGet;
                if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.System_Type_XrefID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet))
                {
                    // Save errors
                    acctSubmits.IsSuccess = false;
                    acctSubmits.Save(username);
                    foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                    {
                        var msGetError = new MS_MonitronicsSubmitsGetDataError
                        {
                            SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                            ErrMsg = row.err_msg,
                            CreatedOn = DateTime.UtcNow
                        };

                        msGetError.Save(username);
                    }
                }
                else
                {
                    // Save data
                    var dsSysTypes = Utils.ConvertDataSet<GetSysTypeXref>(dsRaw);
                    SosCrmDataContext.Instance.MS_MonitronicsEntitySystemTypeXRefs.LoadCollection(
                        SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySystemTypeXRefNuke());
                    foreach (GetSysTypeXref.TableRow row in dsSysTypes.Table.Rows)
                    {
                        SosCrmDataContext.Instance.MS_MonitronicsEntitySystemTypeXRefs.LoadSingle(SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySystemTypeXRefSave(row.dig_systype_id, row.twoway_device_id, row.cell_systype_id, username));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.System_Type_XrefID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion SystemTypeXRef
        }

        /***
         * Populates the MS_MonitronicsEntityCellServices table with hooey from their server
         ***/
        private bool GetCellServices(out string firstErrorMsgGet, string username = "SYSTEM")
        {
            #region CellServices
            // ** Initialize
            var result = false;
            firstErrorMsgGet = null;
            var services = new NXS.Logic.MonitoringStations.Monitronics(_username, _password);

            try
            {
                // ** Createa an MS_AccountsSubmit
                var acctSubmits = new MS_MonitronicsSubmitsGetData
                {
                    EntityId = MS_MonitronicsEntity.MetaData.Cell_ServicesID,
                    IsSuccess = true,
                    CreatedOn = DateTime.UtcNow
                };
                acctSubmits.Save(username);


                // delete existing data from table
                SosCrmDataContext.Instance.MS_MonitronicsEntityCellServices.LoadCollection(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityCellServicesNuke());

                // get all cell providers from database
                MS_MonitronicsEntityCellProviderCollection cpc = SosCrmDataContext.Instance.MS_MonitronicsEntityCellProviders.LoadCollection(SosCrmDataStoredProcedureManager.MS_MonitronicsEntityCellProvidersGetAll());

                // iterate through all providers to get cellservices / cellprovideroptions
                foreach (MS_MonitronicsEntityCellProvider cp in cpc)
                {
                    String xmlstr = "<?xml version=\"1.0\"?><GetCellSvcs xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><GetCellSvc cell_provider=\"" + cp.CellProviderID + "\" /></GetCellSvcs>";

                    // ** Call the getData for Events.
                    DataSet dsRaw;
                    Errors dsErrorsGet;
                    if (!services.GetDataTry(MS_MonitronicsEntity.MetaData.Cell_ServicesID, out dsRaw, out dsErrorsGet, out firstErrorMsgGet, null, xmlstr))
                    {
                        // Save errors
                        acctSubmits.IsSuccess = false;
                        acctSubmits.Save(username);
                        foreach (Errors.TableRow row in dsErrorsGet.Table.Rows)
                        {
                            var msGetError = new MS_MonitronicsSubmitsGetDataError
                            {
                                SubmitsGetDataId = acctSubmits.SubmitsGetDataID,
                                ErrMsg = row.err_msg,
                                CreatedOn = DateTime.UtcNow
                            };

                            msGetError.Save(username);
                        }
                    }
                    else
                    {
                        // Save data
                        var dsSysTypes = Utils.ConvertDataSet<GetCellProviderOptions>(dsRaw);
                        foreach (GetCellProviderOptions.TableRow row in dsSysTypes.Table.Rows)
                        {
                            SosCrmDataContext.Instance.MS_MonitronicsEntityCellServices.LoadSingle(
                                SosCrmDataStoredProcedureManager.MS_MonitronicsEntityCellServicesSave(row.option_id,
                                    row.descr, username));
                        }
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error was thrown when GetData for {0} entity: {1}", MS_MonitronicsEntity.MetaData.Cell_ServicesID, ex.Message);
            }

            // ** Return result
            return result;

            #endregion CellServices
        }

        #endregion Private 
	}
}