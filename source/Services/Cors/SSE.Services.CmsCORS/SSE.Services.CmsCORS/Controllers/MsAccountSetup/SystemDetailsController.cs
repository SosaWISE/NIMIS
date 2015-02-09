using System.Collections.Generic;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Models.CentralStation;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using System;
using System.Web.Http;

namespace SSE.Services.CmsCORS.Controllers.MsAccountSetup
{
	[RoutePrefix("MsAccountSetupSrv")]
    public class SystemDetailsController : ApiController
    {
        // GET MsAccountSetupSrv/SystemDetails
		[Route("SystemDetails/{id}")]
        [HttpGet]
        public CmsCORSResult<MsAccounts> SystemDetails(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get System Details";
            var result = new CmsCORSResult<MsAccounts>((int)CmsResultCodes.Initializing
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
                        IFnsResult<IFnsMsAccount> oFnsModel = oService.SystemDetailsGet(id, user.GPEmployeeID);
                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oFnsMsAccount = (IFnsMsAccount) oFnsModel.GetValue();
						var oMsAccount = new MsAccounts
						{
							AccountID = oFnsMsAccount.AccountID,
							SystemTypeId = oFnsMsAccount.SystemTypeId,
							CellularTypeId = oFnsMsAccount.CellularTypeId,
							PanelTypeId = oFnsMsAccount.PanelTypeId,
                            AccountPassword = oFnsMsAccount.AccountPassword,
							DslSeizureId = oFnsMsAccount.DslSeizureId
						};


                        /** Save success results. */
                        result.Code = (int) CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oMsAccount;
                    }

					#endregion TRY

                    #region CATCH

                    catch (Exception ex)
                    {
                        result.Code = (int) CmsResultCodes.ExceptionThrown;
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

		[Route("SystemDetails/{id}")]
		[HttpPost]
		public CmsCORSResult<MsAccounts> SystemDetailsSave(long id, [FromBody] MsAccounts account)
		{
			/** Check the id. */
			if (id != 0) account.AccountID = id;

			return SystemDetailsSaveGeneric(account);
		}

		public CmsCORSResult<MsAccounts> SystemDetailsSave(MsAccounts account)
		{
			return SystemDetailsSaveGeneric(account);
		}

		private CmsCORSResult<MsAccounts> SystemDetailsSaveGeneric(MsAccounts account)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Save System Details";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validations

					var argArray = new List<CORSArg>
					{
						new CORSArg(account.AccountID, (account.AccountID == 0), "<li>'AccountID' Has to be passed.</li>")
					};
					CmsCORSResult<MsAccounts> result;
					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validations

					#region TRY

					try
					{
						// ** Cast account to fnsMsAccount
						var fnsMsAccount = new FnsMsAccount(account.AccountID)
						{
							SystemTypeId = account.SystemTypeId,
							CellularTypeId = account.CellularTypeId,
							PanelTypeId = account.PanelTypeId,
							AccountPassword = account.AccountPassword,
							DslSeizureId = account.DslSeizureId
						};


						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
						IFnsResult<IFnsMsAccount> oFnsModel = oService.SystemDetailsSave(fnsMsAccount, user.GPEmployeeID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oFnsMsAccount = (IFnsMsAccount)oFnsModel.GetValue();
						var oMsAccount = new MsAccounts
						{
							AccountID = oFnsMsAccount.AccountID,
							SystemTypeId = oFnsMsAccount.SystemTypeId,
							CellularTypeId = oFnsMsAccount.CellularTypeId,
							PanelTypeId = oFnsMsAccount.PanelTypeId,
                            AccountPassword = oFnsMsAccount.AccountPassword,
							DslSeizureId = oFnsMsAccount.DslSeizureId
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

		//[Route("CellularTypes")]
		//[HttpGet]
		//[Obsolete("This has been moved to SalesSummaryController.cs", true)]
		//public CmsCORSResult<List<MsAccountCellularType>> CellularTypes()
		//{
		//	#region Initialize

		//	/** Initialize. */
		//	const string METHOD_NAME = "Get CellularTypes";
		//	var result = new CmsCORSResult<List<MsAccountCellularType>>((int)CmsResultCodes.Initializing
		//		, string.Format("Initializing {0}.", METHOD_NAME));

		//	#endregion Initialize

		//	/** Authenticate session first. */
		//	return CORSSecurity.AuthenticationWrapper(METHOD_NAME
		//		, user =>
		//		{
		//			#region TRY

		//			try
		//			{
		//				// ** Create Service
		//				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
		//				IFnsResult<List<IFnsMsAccountCellularType>> oFnsModel = oService.CellularTypesGet(user.UserID);
		//				/** Check corsResult. */
		//				if (oFnsModel.Code != 0)
		//				{
		//					result.Code = oFnsModel.Code;
		//					result.Message = oFnsModel.Message;
		//					return result;
		//				}

		//				/** Setup return corsResult. */
		//				var oMsCellularTypeList = ConvertTo.CastFnsToMsCellularTypeList((List<IFnsMsAccountCellularType>)oFnsModel.GetValue());


		//				/** Save success results. */
		//				result.Code = (int)CmsResultCodes.Success;
		//				result.SessionId = user.SessionID;
		//				result.Message = "Success";
		//				result.Value = oMsCellularTypeList;
		//			}
		//			#endregion TRY

		//			#region CATCH

		//			catch (Exception ex)
		//			{
		//				result.Code = (int)CmsResultCodes.ExceptionThrown;
		//				result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
		//					ex.Message);
		//			}

		//			#endregion CATCH

		//			#region Result

		//			return result;

		//			#endregion Result
		//		});
		//}

		[Route("ServiceTypes")]
		[HttpGet]
		public CmsCORSResult<List<MsAccountServiceType>> ServiceTypes()
	    {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get ServiceTypes";
            var result = new CmsCORSResult<List<MsAccountServiceType>>((int)CmsResultCodes.Initializing
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
	                    var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
	                    IFnsResult<List<IFnsMsAccountServiceType>> oFnsModel = oService.ServiceTypesGet(user.UserID);
	                    /** Check corsResult. */
	                    if (oFnsModel.Code != 0)
	                    {
	                        result.Code = oFnsModel.Code;
	                        result.Message = oFnsModel.Message;
	                        return result;
	                    }

	                    /** Setup return corsResult. */
	                    var oMsServiceTypeList =
	                        ConvertTo.CastFnsToMsServiceTypeList((List<IFnsMsAccountServiceType>) oFnsModel.GetValue());


	                    /** Save success results. */
	                    result.Code = (int) CmsResultCodes.Success;
	                    result.SessionId = user.SessionID;
	                    result.Message = "Success";
	                    result.Value = oMsServiceTypeList;
	                }
	                    #endregion TRY

	                #region CATCH

	                catch (Exception ex)
	                {
	                    result.Code = (int) CmsResultCodes.ExceptionThrown;
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

		[Route("PanelTypes")]
		[HttpGet]
		public CmsCORSResult<List<MsAccountPanelType>> PanelTypes()
	    {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get PanelTypes";
            var result = new CmsCORSResult<List<MsAccountPanelType>>((int)CmsResultCodes.Initializing
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
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
						IFnsResult<List<IFnsMsAccountPanelType>> oFnsModel = oService.PanelTypesGet(user.UserID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oMsPanelTypeList = ConvertTo.CastFnsToMsPanelTypeList((List<IFnsMsAccountPanelType>)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oMsPanelTypeList;
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

		[Route("DslSeizureTypes")]
		[HttpGet]
		public CmsCORSResult<List<MsAccountDslSeisureTypes>> DslSeizureTypes()
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get DslSeisureTypes";
			var result = new CmsCORSResult<List<MsAccountDslSeisureTypes>>((int)CmsResultCodes.Initializing
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
						IFnsResult<List<IFnsMsAccountDslSeizureType>> oFnsModel = oService.DslSeizureTypesGet(user.UserID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oMsPanelTypeList = ConvertTo.CastFnsToMsDslSeizureTypeList((List<IFnsMsAccountDslSeizureType>)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oMsPanelTypeList;
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
