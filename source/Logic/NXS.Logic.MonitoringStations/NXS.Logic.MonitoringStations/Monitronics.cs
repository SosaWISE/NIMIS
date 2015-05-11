using System;
using System.Data;
using NXS.Logic.MonitoringStations.Helpers;
using NXS.Logic.MonitoringStations.Models;
using NXS.Logic.MonitoringStations.Models.Slammed;
using NXS.Logic.MonitoringStations.MoniBounceAPI;
using NXS.Logic.MonitoringStations.MoniWSI45;
using NXS.Logic.MonitoringStations.Schemas;
using SOS.Data.SosCrm;

namespace NXS.Logic.MonitoringStations
{
	public class Monitronics
	{
		#region .ctor

		public Monitronics(string userId, string password)
		{
			_userId = userId;
			_password = password;
		}

		#endregion .ctor

		#region Properties

		private readonly string _userId;
		private readonly string _password;

		#endregion Properties

		#region Methods

		public bool GetDataTry(string entity, out DataSet dsResult, string csNumber = null, string xmlData = null)
		{
			Errors dsErrors;
			string firstErrorMsg;
			return GetDataTry(entity, out dsResult, out dsErrors, out firstErrorMsg, csNumber, xmlData);
		}

		public bool GetDataTry(string entity, out DataSet dsResult, out Errors dsErrors, out string firstErrorMsg, string csNumber = null, string xmlData = null)
		{
			// ** Initialize
			var services = new WSI();
			dsResult = null;
			dsErrors = null;
			firstErrorMsg = string.Empty;

			try
			{
				// ** get something
				dsResult = services.GetData(entity, _userId, _password, csNumber, xmlData);
				if (dsResult != null)
				{
					// ** Check for an error message

					if (!Utils.ErrorsTry(dsResult, out dsErrors, out firstErrorMsg))
					{
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				Console.Write("The following error occurred: {0}", ex.Message);
			}

			// ** Return result
			return false;
		}

		public bool Update(RequestTypes reqType, string csNo, string xmlData, out DataSet dsResult)
		{
			// ** Initialize
			bool result = false;
			var services = new WSI();
			dsResult = null;

			try
			{
				// ** get something
				dsResult = services.Update(_userId, _password, reqType.Value, csNo, xmlData);
				result = dsResult != null;
			}
			catch (Exception ex)
			{
				Console.Write("The following error occurred: {0}", ex.Message);
			}

			// ** Return result
			return result;
		}

		public bool Partial(RequestTypes reqType, string csNo, int wsiBatchNo)
		{
			// ** Initialize
			bool result = false;
			var services = new WSI();

			try
			{
				// ** get something
				DataSet dsResult = services.Partial(_userId, _password, reqType.Value, csNo, wsiBatchNo);
				result = dsResult != null;
			}
			catch (Exception ex)
			{
				Console.Write("The following error occurred: {0}", ex.Message);
			}

			// ** Return result
			return result;
		}

		public bool AccountOnlineTry(string csNo, string xmlData, out DataSet dsResult, out string confirmatioNumber, out string firstErrorMsg, string creditRequestXml = null, string purchaseInfoXml = null)
		{
			// ** Initialize
			var result = false;
			var services = new WSI();
			var appIdHeader = new ApplicationIDHeader { appID = "WSI" };
			services.ApplicationIDHeaderValue = appIdHeader;
			dsResult = null;
			confirmatioNumber = null;
			firstErrorMsg = null;

			// ** get something
			dsResult = services.AccountOnline(_userId, _password, csNo, xmlData, creditRequestXml, purchaseInfoXml);
			ErrorsOnBoardAccount dsErrors;
			result = !Utils.ErrorsOnBoardAccountTry(dsResult, out dsErrors, out firstErrorMsg, out confirmatioNumber);
			try
			{
				// ** For debugging
				if (!result)
				{
					foreach (ErrorsOnBoardAccount.TableRow row in dsErrors.Table.Rows)
					{
						Console.WriteLine("TableName: {0} | EntryId: {1} | SiteNo: {2} | CsNo: {3} | ErrNo: {4} | MsgType: {5} | ErrText: {6} | ErrDate: {7}"
							, row.Istable_nameNull() ? null : row.table_name
							, row.Isentry_idNull() ? null : row.entry_id
							, row.Issite_noNull() ? (int?)null : row.site_no
							, row.Iscs_noNull() ? null : row.cs_no
							, row.Iserr_noNull() ? (int?)null : row.err_no
							, row.Ismsg_typeNull() ? (byte?)null : row.msg_type
							, row.Iserr_textNull() ? null : row.err_text
							, row.Iserr_dateNull() ? (DateTime?)null : row.err_date);
					}
				}
			}
			catch (Exception ex)
			{
				Console.Write("The following error occurred: {0}", ex.Message);
			}

			// ** Return result.
			return result;
		}

		public bool Immediate(string entity, string csNo, string xmlData, out DataSet dsResult)
		{
			// ** Initialize
			var hasErrors = false;
			var services = new WSI();
			var appIdHeader = new ApplicationIDHeader { appID = "WSI" };
			dsResult = null;
			services.ApplicationIDHeaderValue = appIdHeader;

			try
			{
				// ** Execute Immediate
				dsResult = services.Immediate(entity, _userId, _password, csNo, xmlData);
				string firstErrorMsg, confirmationNumber;
				ErrorsOnBoardAccount dsErrors;
				hasErrors = Utils.ErrorsOnBoardAccountTry(dsResult, out dsErrors, out firstErrorMsg, out confirmationNumber);

				// ** For debugging
				if (hasErrors)
				{
					foreach (ErrorsOnBoardAccount.TableRow row in dsErrors.Table.Rows)
					{
						Console.WriteLine("TableName: {0} | EntryId: {1} | SiteNo: {2} | CsNo: {3} | ErrNo: {4} | MsgType: {5} | ErrText: {6} | ErrDate: {7}"
							, row.Istable_nameNull() ? null : row.table_name
							, row.Isentry_idNull() ? null : row.entry_id
							, row.Issite_noNull() ? (int?)null : row.site_no
							, row.Iscs_noNull() ? null : row.cs_no
							, row.Iserr_noNull() ? (int?)null : row.err_no
							, row.Ismsg_typeNull() ? (byte?)null : row.msg_type
							, row.Iserr_textNull() ? null : row.err_text
							, row.Iserr_dateNull() ? (DateTime?)null : row.err_date);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error occurred: {0}", ex.Message);
			}

			// ** Return result.
			return !hasErrors;
		}

		public bool InitiateTwoWayTest(MS_AccountSubmit accountSubmit, string csNo, string deviceId, out DataSet dsResult)
		{
			// ** Initialize
			var hasErrors = false;
			var services = new WSI();
			var appIdHeader = new ApplicationIDHeader { appID = "WSI" };
			dsResult = null;
			services.ApplicationIDHeaderValue = appIdHeader;
			var accountSubmitXml = new MS_AccountSubmitMsXml();
			accountSubmitXml.AccountSubmitID = accountSubmit.AccountSubmitID;

			try
			{
				// ** Build XML
				var twoWay = new Twoways
				{
					Twoway = new TwowayInfo
					{
						TwoWayDeviceId = deviceId
					}
				};
				var xmlData = twoWay.Serialize();
				accountSubmitXml.Account = xmlData;
				accountSubmitXml.CreatedBy = accountSubmit.CreatedBy;
				accountSubmitXml.CreatedOn = accountSubmit.CreatedOn;
				accountSubmitXml.Save(accountSubmit.CreatedBy);

				// ** Execute Immediate
				dsResult = services.Immediate("twoway", _userId, _password, csNo, xmlData);
				string firstErrorMsg, confirmationNumber;
				ErrorsOnBoardAccount dsErrors;
				hasErrors = Utils.ErrorsOnBoardAccountTry(dsResult, out dsErrors, out firstErrorMsg, out confirmationNumber);

				// ** For debugging
				if (hasErrors)
				{
					foreach (ErrorsOnBoardAccount.TableRow row in dsErrors.Table.Rows)
					{
						Console.WriteLine("TableName: {0} | EntryId: {1} | SiteNo: {2} | CsNo: {3} | ErrNo: {4} | MsgType: {5} | ErrText: {6} | ErrDate: {7}"
							, row.Istable_nameNull() ? null : row.table_name
							, row.Isentry_idNull() ? null : row.entry_id
							, row.Issite_noNull() ? (int?)null : row.site_no
							, row.Iscs_noNull() ? null : row.cs_no
							, row.Iserr_noNull() ? (int?)null : row.err_no
							, row.Ismsg_typeNull() ? (byte?)null : row.msg_type
							, row.Iserr_textNull() ? null : row.err_text
							, row.Iserr_dateNull() ? (DateTime?)null : row.err_date);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error occurred initiating a two-way test: {0}", ex.Message);
			}

			// ** Return result.
			return !hasErrors;
		}

		private MS_AccountSubmitM BindErrorRow(ErrorsOnBoardAccount.TableRow row, MS_AccountSubmit msAccountSubmit)
		{
			return new MS_AccountSubmitM
			{
				AccountSubmitId = msAccountSubmit.AccountSubmitID,
				TableName = row.table_name,
				EntryId = row.entry_id,
				SiteNo = row.site_no,
				CsNo = row.cs_no,
				ErrNo = row.err_no,
				MsgType = row.msg_type,
				ErrText = row.err_text,
				ErrDate = row.err_date,
				CreatedBy = msAccountSubmit.CreatedBy,
				CreatedOn = DateTime.UtcNow
			};
		}

		public bool PullPanelResetShell(MS_AccountSubmit parentAccountSubmit, string csNo, out DataSet dsResult)
		{
			#region INITIALIZE
			// ** Initialize
			bool hasErrors;
			var accountSubmit = MS_AccountSubmit.CreateFromParent(parentAccountSubmit, (short)MS_AccountSubmitType.AccountSubmitTypeEnum.Pull_Panel);
			accountSubmit.Save(parentAccountSubmit.CreatedBy);
			var services = new WSI();
			var appIdHeader = new ApplicationIDHeader { appID = "WSI" };
			dsResult = null;
			services.ApplicationIDHeaderValue = appIdHeader;
			#endregion INITIALIZE

			try
			{
				dsResult = services.Immediate("pullpanel", _userId, _password, csNo, null);
				ErrorsOnBoardAccount dsErrors;
				hasErrors = Utils.ErrorsOnBoardAccountTry(dsResult, out dsErrors);
				accountSubmit.WasSuccessfull = !hasErrors;

				if (hasErrors)
				{
					foreach (ErrorsOnBoardAccount.TableRow row in dsErrors.Table.Rows)
					{
						var msAccountSubmitMs = BindErrorRow(row, accountSubmit);
						msAccountSubmitMs.Save(accountSubmit.CreatedBy);
					}
				}

				accountSubmit.Save(parentAccountSubmit.CreatedBy);
			}
			catch (Exception ex)
			{

// ReSharper disable once PossibleIntendedRethrow
				throw ex;
			}

			// ** Return result
			return !hasErrors;
		}

		public bool GetCreditRequestTry(string csNo, out DataSet dsResult)
		{
			// ** Initialize
			var hasErrors = false;
			var services = new WSI();
			var appIdHeader = new ApplicationIDHeader { appID = "WSI" };
			dsResult = null;
			services.ApplicationIDHeaderValue = appIdHeader;

			try
			{
				var result = services.GetCreditRequest(_userId, _password, csNo);

				// ** Check for errors
				if (string.IsNullOrEmpty(result))
				{
					// ** Initialize
					const string ERR_TEXT = "Credit Request for CSNo: '{0}' returned nothing.";
					hasErrors = true;
					dsResult = new ErrorsOnBoardAccount();
					var dtResult = new ErrorsOnBoardAccount.TableDataTable();
					var errorRow = dtResult.NewTableRow();

					// ** Bind data
					errorRow.table_name = "NXS:credit_requests Get";
					errorRow.msg_type = 1;
					errorRow.err_text = string.Format(ERR_TEXT, csNo);
					errorRow.err_date = DateTime.UtcNow;

					dsResult.Tables.Add(dtResult);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error occurred: {0}", ex.Message);
			}

			// ** Return result
			return !hasErrors;
		}

		public bool UpdateCreditRequestTry(string csNo, string xmlUpdate, out ErrorsOnBoardAccount dsResult)
		{
			// ** Initialize
			const string ERR_TEXT_SUCCESS = "Successfuly update Credit Request with the following XML: {0}";
			var hasErrors = false;
			var services = new WSI();
			var appIdHeader = new ApplicationIDHeader { appID = "WSI" };
			services.ApplicationIDHeaderValue = appIdHeader;
			dsResult = new ErrorsOnBoardAccount();
			//var dtResult = new ErrorsOnBoardAccount.TableDataTable();
			//var dtResult = dsResult.Table;
			var errorRow = dsResult.Table.NewTableRow();
			errorRow.cs_no = csNo;
			errorRow.err_no = 0;
			errorRow.table_name = "NXS:credit_requests Update";
			errorRow.err_date = DateTime.UtcNow;

			try
			{
				services.UpdateCreditRequest(_userId, _password, xmlUpdate);

				// ** Bind data
				errorRow.msg_type = 0;
				errorRow.err_text = string.Format(ERR_TEXT_SUCCESS, xmlUpdate);

			}
			catch (Exception ex)
			{
				// ** Initialize
				const string ERR_TEXT = "Credit Request Update returned the following error: {0}.";
				hasErrors = true;

				// ** Bind data
				errorRow.msg_type = 1;
				errorRow.err_text = string.Format(ERR_TEXT, ex.Message);

				Console.WriteLine("The following error occurred: {0}", ex.Message);
			}

			// ** Return result
			//dtResult.AddTableRow(errorRow);
			dsResult.Table.AddTableRow(errorRow);
			return !hasErrors;
		}

		public MatchResultFields IsSlammedAccount(SlammedInputFields fields)
		{
			/** Locals. */
			MatchResultFields result;
			var service = new wwwBouncer();

			var searchInfo = new SearchInfo
			{
				Address1 = fields.Address1,
				City = fields.City,
				State = fields.State,
				Zip = fields.Zip,
				ApplicationName = fields.ApplicationName,
				ProcessName = fields.ProcessNameStr,
				DealerName = fields.DealerName,
				DealerNumber = fields.DealerNumber,
				FirstName = fields.FirstName,
				LastName = fields.LastName
			};
			
			/** Get all the other fields that are not required. */
			if (!string.IsNullOrEmpty(fields.Address2))
				searchInfo.Address2 = fields.Address2;
			if (!string.IsNullOrEmpty(fields.Phone1))
				searchInfo.Phone1 = fields.Phone1;
			if (!string.IsNullOrEmpty(fields.Phone2))
				searchInfo.Phone1 = fields.Phone2;
			if (fields.CurrentSiteID != 0)
				searchInfo.CurrentSiteID = fields.CurrentSiteID;

			MatchResult matchResult = service.Match(searchInfo);
			result = new MatchResultFields(matchResult);

			// ** Return result
			return result;
		}

		public string SlammedReason(int matchId)
		{
			/** Locals*/
			var result = string.Empty;
			var service = new wwwBouncer();

			/** Execute call. */
			MatchComment[] matchResult = service.GetMatchComments(matchId, true);

			/** Get the comments. */
			foreach (var matchComment in matchResult)
			{
				result += string.Format("Comment: {0}\r\n", matchComment);
			}

			/** Return result. */
			return result;
		}

		#endregion Methods
	}
}
