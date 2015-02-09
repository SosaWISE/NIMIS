using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Swing;
using SOS.Services.Interfaces.Models.Swing;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using SSE.Services.CmsCORS.Models.Swing;

namespace SSE.Services.CmsCORS.Controllers.Swing
{
    [RoutePrefix("SwingAccountSrv")]
	public class SwingController : ApiController
    {
        //[Route("CustomerSwingInfo/{id}")]
        //[HttpGet]
        //public CmsCORSResult<CustomerSwingInfo> CustomerSwingInfoRead(long id)  //(long interimAccountId, string customerType)
        
        [Route("CustomerSwingInfo")]
        [HttpPost]
        [HttpOptions]
        public CmsCORSResult<CustomerSwingInfo> CustomerSwingInfoRead([FromBody]SwingParam jsonParam)  //(long interimAccountId, string customerType)
        
    {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "CustomerSwingInfoRead";
            
            #endregion Initialize

            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {

                    #region Parameter Validation

                    var argArray = new List<CORSArg>();
                    if (jsonParam == null)
                    {
                        argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
                    }
                    else
                    {
                        argArray.Add(new CORSArg(jsonParam.InterimAccountId, (jsonParam.InterimAccountId == 0), "<li>'InterimAccountId' was not passed.</li>"));
                        argArray.Add(new CORSArg(jsonParam.CustomerType, (string.IsNullOrEmpty(jsonParam.CustomerType)), "<li>'Customer Type (PRI/SEC)' was not passed.</li>"));
                    }
                    CmsCORSResult<CustomerSwingInfo> result;
                    if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                    #endregion Parameter Validation

                    #region TRY

                    /** Authenticate session first. */
                    //removed authentication for testing purpose only
                    //we need need to add authentication later
                    try
                    {
                        // ** Create Service
                        var swingService =
                            SosServiceEngine.Instance.FunctionalServices.Instance<ISwingService>();

                        // ** Prepare arguents
                        IFnsResult<IFnsCustomerSwingInfo> fnsResult =
                            swingService.CustomerSwingInfoRead(jsonParam.InterimAccountId, jsonParam.CustomerType);

                        // ** Save result
                        result.Code = fnsResult.Code;
                        //  result.SessionId = user.SessionID;
                        result.Message = fnsResult.Message;

                        // ** Get Values
                        var fnsResultValue = (IFnsCustomerSwingInfo)fnsResult.GetValue();
                        if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                        {
                            var resultValue = new CustomerSwingInfo
                            {
                                Salutation = fnsResultValue.Salutation,
                                FirstName = fnsResultValue.FirstName,
                                MiddleName = fnsResultValue.MiddleName,
                                LastName = fnsResultValue.LastName,
                                DOB = fnsResultValue.DOB,
                                Email = fnsResultValue.Email,
                                Suffix = fnsResultValue.Suffix,
                                SSN = fnsResultValue.SSN


                            };
                            result.Value = resultValue;
                        }
                 }
                    
#endregion try

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


        [Route("CustomerSwingPremiseAddress/{id}")]
        [HttpGet]
        public CmsCORSResult<CustomerSwingPremiseAddress> CustomerSwingPremiseAddressRead(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "CustomerSwingPremiseAddressRead";
            var result = new CmsCORSResult<CustomerSwingPremiseAddress>((int)ErrorCodes.Initializing, string.Format("Initializing '{0}'.", METHOD_NAME));

            #endregion Initialize




            /** Authenticate session first. */
            //just escape Authentication for the mean time while testing 
            //removed authentication for testing purpose only
            //we need need to add authentication later
              return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
              {

            #region TRY
            try
            {
                // ** Create Service
                var swingService =
                    SosServiceEngine.Instance.FunctionalServices.Instance<ISwingService>();

                // ** Prepare arguents
                IFnsResult<IFnsCustomerSwingPremiseAddress> fnsResult =
                    swingService.CustomerSwingPremiseAddressRead(id);

                // ** Save result
                result.Code = fnsResult.Code;
                //  result.SessionId = user.SessionID;
                result.Message = fnsResult.Message;

                // ** Get Values
                var fnsResultValue = (IFnsCustomerSwingPremiseAddress)fnsResult.GetValue();
                if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                {
                    var resultValue = new CustomerSwingPremiseAddress
                    {
                        StreetAddress1 = fnsResultValue.StreetAddress1,
                        StreetAddress2 = fnsResultValue.StreetAddress2,
                        City = fnsResultValue.City,
                        County = fnsResultValue.County,
                        PostalCode = fnsResultValue.PostalCode,
                        State = fnsResultValue.State
                    };
                    result.Value = resultValue;
                }
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

            #region RESULT
            return result;
            #endregion RESULT

              });

        }





        [Route("CustomerSwingEmergencyContact/{id}")]
        [HttpGet]
        public CmsCORSResult<List<CustomerSwingEmergencyContact>> CustomerSwingEmergencyContactRead(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "CustomerSwingEmergencyContactRead";
            var result = new CmsCORSResult<List<CustomerSwingEmergencyContact>>((int)ErrorCodes.Initializing, string.Format("Initializing '{0}'.", METHOD_NAME));

            #endregion Initialize


            /** Authenticate session first. */
            //removed authentication for testing purpose only
            //we need need to add authentication later
            try
            {
                // ** Create Service
                var swingService =
                    SosServiceEngine.Instance.FunctionalServices.Instance<ISwingService>();

                // ** Prepare arguents
                IFnsResult<List<IFnsCustomerSwingEmergencyContact>> oFnsModel =
                    swingService.CustomerSwingEmergencyContactRead(id);
               
                /** Check corsResult. */
                if (oFnsModel.Code != 0)
                {
                    result.Code = oFnsModel.Code;
                    result.Message = oFnsModel.Message;
                    return result;
                }


                /** Setup return corsResult. */
                var oCustomerSwingEmergencyContactList = ConvertTo.CastFnsToCustomerSwingEmergencyContactList((List<IFnsCustomerSwingEmergencyContact>)oFnsModel.GetValue());


                /** Save success results. */
                result.Code = (int)CmsResultCodes.Success;
               // result.SessionId = user.SessionID;
                result.Message = "Success";
                result.Value = oCustomerSwingEmergencyContactList;
            }


            catch (Exception ex)
            {
                result.Code = (int)CmsResultCodes.ExceptionThrown;
                result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
                    METHOD_NAME,
                    ex.Message);
            }

            return result;

        }




        [Route("CustomerSwingEquipmentInfo/{id}")]
        [HttpGet]
        public CmsCORSResult<List<CustomerSwingEquipmentInfo>> CustomerSwingEquipmentInfoRead(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "CustomerSwingEquipmentInfoRead";
            var result = new CmsCORSResult<List<CustomerSwingEquipmentInfo>>((int)ErrorCodes.Initializing, string.Format("Initializing '{0}'.", METHOD_NAME));

            #endregion Initialize


            /** Authenticate session first. */
            //removed authentication for testing purpose only
            //we need need to add authentication later
            try
            {
                // ** Create Service
                var swingService =
                    SosServiceEngine.Instance.FunctionalServices.Instance<ISwingService>();

                // ** Prepare arguents
                IFnsResult<List<IFnsCustomerSwingEquipmentInfo>> oFnsModel =
                    swingService.CustomerSwingEquipmentInfoRead(id);

                /** Check corsResult. */
                if (oFnsModel.Code != 0)
                {
                    result.Code = oFnsModel.Code;
                    result.Message = oFnsModel.Message;
                    return result;
                }


                /** Setup return corsResult. */
                var oCustomerSwingEquipmentInfoList = ConvertTo.CastFnsToCustomerSwingEquipmentInfoList((List<IFnsCustomerSwingEquipmentInfo>)oFnsModel.GetValue());


                /** Save success results. */
                result.Code = (int)CmsResultCodes.Success;
                // result.SessionId = user.SessionID;
                result.Message = "Success";
                result.Value = oCustomerSwingEquipmentInfoList;

            }


            catch (Exception ex)
            {
                result.Code = (int)CmsResultCodes.ExceptionThrown;
                result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
                    METHOD_NAME,
                    ex.Message);
            }

            return result;

        }


        //Swing process starts here
        [Route("CustomerSwingInterim")]
        [HttpPost]
        [HttpOptions]
        public CmsCORSResult<CustomerSwingInterim> CustomerSwingInterimPost([FromBody]SwingParam jsonParam)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "CustomerSwingInterimPost";
            
            #endregion Initialize

            //just escape Authentication for the mean time while testing 
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
              {


            #region Parameter Validation

            var argArray = new List<CORSArg>();
            if (jsonParam == null)
            {
                argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
            }
            else
            {
                argArray.Add(new CORSArg(jsonParam.InterimAccountId, (jsonParam.InterimAccountId == 0), "<li>'InterimAccountId' was not passed.</li>"));
                
            }
            CmsCORSResult<CustomerSwingInterim> result;
            if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

            #endregion Parameter Validation

            #region TRY

            /** Authenticate session first. */
            //removed authentication for testing purpose only
            //we need need to add authentication later
            try
            {
                // ** Create Service
                var swingService =
                    SosServiceEngine.Instance.FunctionalServices.Instance<ISwingService>();

                // ** Prepare arguents
                IFnsResult<IFnsCustomerSwingInterim> fnsResult =
                    swingService.CustomerSwingInterimPost(jsonParam.InterimAccountId, jsonParam.SwingEquipment);

                // ** Save result
                result.Code = fnsResult.Code;
                //  result.SessionId = user.SessionID;
                result.Message = fnsResult.Message;

                // ** Get Values
                var fnsResultValue = (IFnsCustomerSwingInterim)fnsResult.GetValue();
                if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                {

                    var resultValue = new CustomerSwingInterim
                    {
						InterimAccountID = fnsResultValue.InterimAccountID,
						MsAccountID = fnsResultValue.MsAccountID,
						CustomerMasterFileID = fnsResultValue.CustomerMasterFileID,
						CustomerMasterFile = fnsResultValue.CustomerMasterFile,
						PremiseAddress = fnsResultValue.PremiseAddress,
						McAccount = fnsResultValue.McAccount,
						MsAccount = fnsResultValue.MsAccount,
						QlLead = fnsResultValue.QlLead,
						QlCreditReport = fnsResultValue.QlCreditReport,
						AeCustomer = fnsResultValue.AeCustomer,
						AeCustomerAccount = fnsResultValue.AeCustomerAccount,
						MsEmergencyContact = fnsResultValue.MsEmergencyContact,
						EquipmentSwung = fnsResultValue.EquipmentSwung,
						CreatedOn = fnsResultValue.CreatedOn,
						CreatedBy = fnsResultValue.CreatedBy
                    };
                    result.Value = resultValue;
                }
            }

            #endregion try

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

        //Get Swing System Details
        [Route("CustomerSwingSystemDetails/{id}")]
        [HttpGet]
        public CmsCORSResult<CustomerSwingSystemDetails> CustomerSwingSystemDetailsRead(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "CustomerSwingSystemDetailsRead";
            var result = new CmsCORSResult<CustomerSwingSystemDetails>((int)ErrorCodes.Initializing, string.Format("Initializing '{0}'.", METHOD_NAME));

            #endregion Initialize


            /** Authenticate session first. */
            //just escape Authentication for the mean time while testing 
            //removed authentication for testing purpose only
            //we need need to add authentication later
              return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
              {

            #region TRY
            try
            {
                // ** Create Service
                var swingService =
                    SosServiceEngine.Instance.FunctionalServices.Instance<ISwingService>();

                // ** Prepare arguments
                IFnsResult<IFnsCustomerSwingSystemDetails> fnsResult =
                    swingService.CustomerSwingSystemDetailsRead(id);

                // ** Save result
                result.Code = fnsResult.Code;
                //  result.SessionId = user.SessionID;
                result.Message = fnsResult.Message;

                // ** Get Values
                var fnsResultValue = (IFnsCustomerSwingSystemDetails)fnsResult.GetValue();
                if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                {
                    var resultValue = new CustomerSwingSystemDetails
                    {
                        ServiceType = fnsResultValue.ServiceType,
                        CellularType = fnsResultValue.CellularType,
                        PassPhrase = fnsResultValue.PassPhrase,
                        PanelType = fnsResultValue.PanelType,
                        DslSeizure = fnsResultValue.DslSeizure                        
                    };
                    result.Value = resultValue;
                }
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

            #region RESULT
            return result;
            #endregion RESULT

              });

        }


        //Get Swung info
        [Route("CustomerSwungInfo/{id}")]
        [HttpGet]
        public CmsCORSResult<CustomerSwungInfo> CustomerSwungInfoRead(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "CustomerSwungInfoRead";
            var result = new CmsCORSResult<CustomerSwungInfo>((int)ErrorCodes.Initializing, string.Format("Initializing '{0}'.", METHOD_NAME));

            #endregion Initialize


            /** Authenticate session first. */
            //just escape Authentication for the mean time while testing 
            //removed authentication for testing purpose only
            //we need need to add authentication later
              return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
              {

            #region TRY
            try
            {
                // ** Create Service
                var swingService =
                    SosServiceEngine.Instance.FunctionalServices.Instance<ISwingService>();

                // ** Prepare arguments
                IFnsResult<IFnsCustomerSwungInfo> fnsResult =
                    swingService.CustomerSwungInfoRead(id);

                // ** Save result
                result.Code = fnsResult.Code;
                //  result.SessionId = user.SessionID;
                result.Message = fnsResult.Message;

                // ** Get Values
                var fnsResultValue = (IFnsCustomerSwungInfo)fnsResult.GetValue();
                if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                {
                    var resultValue = new CustomerSwungInfo
                    {
						InterimAccountID = fnsResultValue.InterimAccountID,
						MsAccountID = fnsResultValue.MsAccountID,
						CustomerMasterFileID = fnsResultValue.CustomerMasterFileID,
						CustomerMasterFile = fnsResultValue.CustomerMasterFile,
						PremiseAddress = fnsResultValue.PremiseAddress,
						McAccount = fnsResultValue.McAccount,
						MsAccount = fnsResultValue.MsAccount,
						QlLead = fnsResultValue.QlLead,
						QlCreditReport = fnsResultValue.QlCreditReport,
						AeCustomer = fnsResultValue.AeCustomer,
						AeCustomerAccount = fnsResultValue.AeCustomerAccount,
						MsEmergencyContact = fnsResultValue.MsEmergencyContact,
						EquipmentSwung = fnsResultValue.EquipmentSwung,
						CreatedOn = fnsResultValue.CreatedOn,
						CreatedBy = fnsResultValue.CreatedBy
                    };
                    result.Value = resultValue;
                }
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

            #region RESULT
            return result;
            #endregion RESULT

              });

        }


        //Swing Add DNC list
        [Route("CustomerSwingAddDnc/{phoneNumber}")]
        [HttpGet]
        public CmsCORSResult<CustomerSwingAddDnc> CustomerSwingAddDncRead(string phoneNumber)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "CustomerSwingAddDncRead";
            var result = new CmsCORSResult<CustomerSwingAddDnc>((int)ErrorCodes.Initializing, string.Format("Initializing '{0}'.", METHOD_NAME));

            #endregion Initialize


            /** Authenticate session first. */
            //just escape Authentication for the mean time while testing 
            //removed authentication for testing purpose only
            //we need need to add authentication later
              return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
              {

            #region TRY
            try
            {
                // ** Create Service
                var swingService =
                    SosServiceEngine.Instance.FunctionalServices.Instance<ISwingService>();

                // ** Prepare arguments
                IFnsResult<IFnsCustomerSwingAddDnc> fnsResult =
                    swingService.CustomerSwingAddDncRead(phoneNumber);

                // ** Save result
                result.Code = fnsResult.Code;
                //result.SessionId = user.SessionID;
                result.Message = fnsResult.Message;
                

                // ** Get Values
                var fnsResultValue = (IFnsCustomerSwingAddDnc)fnsResult.GetValue();
                if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                {
                    var resultValue = new CustomerSwingAddDnc
                    {
                        Dnc_Status = fnsResultValue.Dnc_Status
                    };
                    result.Value = resultValue;
                }
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

            #region RESULT
            return result;
            #endregion RESULT

              });

        }


	
	}



    
}
