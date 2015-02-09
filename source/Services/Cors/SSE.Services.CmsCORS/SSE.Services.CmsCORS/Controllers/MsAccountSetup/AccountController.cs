using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MsAccountSetup
{
	[RoutePrefix("MsAccountSetupSrv")]
	public class AccountController : ApiController
	{
		// POST api/msaccountleadinfo
		[Route("Accounts")]
		public CmsCORSResult<MsAccountLeadInfo> Post([FromBody]MsAccountLeadInfo info)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Accounts Create";
			int countNumbers = 23;
			var result = new CmsCORSResult<MsAccountLeadInfo>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				if (info == null)
				{
					argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
				}
				else
				{
					argArray.Add(new CORSArg(info.CustomerMasterFileId, (info.CustomerMasterFileId == 0), "<li>'CustomerMasterFileId' was not passed.</li>"));
				}

				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
					IFnsResult<IFnsMsAccountLeadInfo> oFnsModel = oService.CreateMasterFileAccounts(info.CustomerMasterFileId, user.GPEmployeeID);
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oFnsMsAccount = (IFnsMsAccountLeadInfo)oFnsModel.GetValue();
					var oMsAccount = new MsAccountLeadInfo
					{
						AccountID = oFnsMsAccount.AccountID,
						LeadId = oFnsMsAccount.LeadId,
						CustomerId = oFnsMsAccount.CustomerId,
						CustomerMasterFileId = oFnsMsAccount.CustomerMasterFileId,
						IndustryAccountId = oFnsMsAccount.IndustryAccountId,
						SystemTypeId = oFnsMsAccount.SystemTypeId,
						CellularTypeId = oFnsMsAccount.CellularTypeId,
						PanelTypeId = oFnsMsAccount.PanelTypeId,
						PanelItemId = oFnsMsAccount.PanelItemId,
						CellPackageItemId = oFnsMsAccount.CellPackageItemId,
						ContractTemplateId = oFnsMsAccount.ContractId
					};

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oMsAccount;
				}

				#endregion TRY

				#region CATCH

				catch (Exception ex)
				{
					result.Code = (int)CmsResultCodes.ExceptionThrown;
					result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
						METHOD_NAME,
						ex.Message);
				}

				#endregion CATCH

				#region Result

				return result;

				#endregion Result

			});
		}

		[Route("Accounts/{id}/EmergencyContacts")]
		[HttpGet]
		public CmsCORSResult<List<MsEmergencyContact>> EmergencyContactsGet(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get EmergencyContacts All by AccountID";
			var result = new CmsCORSResult<List<MsEmergencyContact>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region TRY

				try
				{
					// ** Create Service
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
					IFnsResult<List<IFnsMsEmergencyContact>> oFnsModel = oService.EmergencyContactGetByAccountId(id, user.GPEmployeeID);
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oMsEmergencyContactList = ConvertTo.CastFnsToMsEmergencyContactList((List<IFnsMsEmergencyContact>)oFnsModel.GetValue());


					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oMsEmergencyContactList;
				}
				#endregion TRY

				#region CATCH

				catch (Exception ex)
				{
					result.Code = (int)CmsResultCodes.ExceptionThrown;
					result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
						ex.Message);
				}

				#endregion CATCH

				#region Result

				return result;

				#endregion Result

			});
		}

        //commented by Reagan 08/29/2014
        //this method produces the following error
        //The 'ObjectContent`1' type failed to serialize the response body for content type 'application/xml; charset=utf-8'.
        //Type 'System.Collections.Generic.List`1[[SOS.FunctionalServices.Contracts.Models.CentralStation.IFnsMsAccountEquipmentsView, SOS.FunctionalServices.Contracts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]' with data contract name 'ArrayOfanyType:http://schemas.microsoft.com/2003/10/Serialization/Arrays' is not expected. Consider using a DataContractResolver or add any types not known statically to the list of known types - for example, by using the KnownTypeAttribute attribute or by adding them to the list of known types passed to DataContractSerializer.
        //[Route("Accounts/{id}/Equipment")]
        //[HttpGet]
        //public CmsCORSResult<object> Equipment(long id)
        //{
        //    return CORSSecurity.AuthenticationWrapper("Equipment", user =>
        //    {
        //        var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
        //        var fnsResult = service.EquipmentByAccountId(id);
        //        return new CmsCORSResult<object>(fnsResult.Code, fnsResult.Message)
        //        {
        //            Value = fnsResult.GetValue(),
        //        };
        //    });
        //}

        //replacement from the existing Equipment method
        [Route("Accounts/{id}/Equipment")]
        [HttpGet]
        public CmsCORSResult<List<MsAccountEquipment>> Equipment(long id) {

            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get MsAccountEquipments All by AccountID";
            var result = new CmsCORSResult<List<MsAccountEquipment>>((int)CmsResultCodes.Initializing
                , string.Format("Initializing {0}.", METHOD_NAME));

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
            , user =>
            {
                #region TRY

                try
                {
                    // ** Create Service
                    var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
                    IFnsResult<List<IFnsMsAccountEquipmentsView>> oFnsModel = oService.EquipmentByAccountId(id);
                    /** Check corsResult. */
                    if (oFnsModel.Code != 0)
                    {
                        result.Code = oFnsModel.Code;
                        result.Message = oFnsModel.Message;
                        return result;
                    }

                    /** Setup return corsResult. */
                    var oMsAccountEquipmentList = ConvertTo.CastFnsToMsAccountEquipmentList((List<IFnsMsAccountEquipmentsView>)oFnsModel.GetValue());


                    /** Save success results. */
                    result.Code = (int)CmsResultCodes.Success;
                    result.SessionId = user.SessionID;
                    result.Message = "Success";
                    result.Value = oMsAccountEquipmentList;
                }
                #endregion TRY

                #region CATCH

                catch (Exception ex)
                {
                    result.Code = (int)CmsResultCodes.ExceptionThrown;
                    result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
                        ex.Message);
                }

                #endregion CATCH

                #region Result

                return result;

                #endregion Result

            });
        
        
        }
        
	}
}
