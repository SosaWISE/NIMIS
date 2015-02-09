/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 02/03/12
 * Time: 09:46
 * 
 * Description:  Implementes the MonitoringStation Services Service Engine
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Swing;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.Swing;


namespace SOS.FunctionalServices
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
    public class SwingService : ISwingService
    {
        #region CustomerSwingInfo

        public IFnsResult<IFnsCustomerSwingInfo> CustomerSwingInfoRead(long interimAccountId, string customerType)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "CustomerSwingInfoRead";
            var result = new FnsResult<IFnsCustomerSwingInfo>
            {
                Code = (int)ErrorCodes.GeneralMessage
                ,
                Message = string.Format("Initializing {0}", METHOD_NAME)
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the CustomSwingInfo service.
                AE_CustomerSWINGView csiList = SosCrmDataContext.Instance.AE_CustomerSWINGViews.GetCustomerInfo(interimAccountId, customerType);
                /*AE_InvoiceItemsViewCollectio aeList = SosCrmDataContext.Instance.AE_InvoiceItemsViews.ByInvoiceId(invoiceId);*/
                //var resultList = csiList.Select(item => new FnsAeInvoiceItemView(item)).Cast<IFnsAeInvoiceItemView>().ToList();
                //AE_Invoice invoiceHeader = SosCrmDataContext.Instance.AE_Invoices.LoadByPrimaryKey(invoiceId);

                // ** Build result
                var resultValue = new FnsCustomerSwingInfo(csiList);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
          
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsCustomerSwingInfo>
                {
                    Code = (int)ErrorCodes.UnexpectedException
                    ,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }
        #endregion CustomerSwingInfo

        #region CustomerSwingPremiseAddress

        public IFnsResult<IFnsCustomerSwingPremiseAddress> CustomerSwingPremiseAddressRead(long interimAccountId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "IFnsCustomerSwingPremiseAddressRead";
            var result = new FnsResult<IFnsCustomerSwingPremiseAddress>
            {
                Code = (int)ErrorCodes.GeneralMessage
                ,
                Message = string.Format("Initializing {0}", METHOD_NAME)
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the CustomSwingInfo service.
                AE_CustomerSWINGPremiseAddressView aeCSPA = SosCrmDataContext.Instance.AE_CustomerSWINGPremiseAddressViews.GetCustomerSWINGPremiseAddress(interimAccountId);
       
                // ** Build result
                var resultValue = new FnsCustomerSwingPremiseAddress(aeCSPA);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;

            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsCustomerSwingPremiseAddress>
                {
                    Code = (int)ErrorCodes.UnexpectedException
                    ,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }
        #endregion CustomerSwingPremiseAddress


        #region CustomerSwingEmergencyContact

        public IFnsResult<List<IFnsCustomerSwingEmergencyContact>> CustomerSwingEmergencyContactRead(long interimAccountId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "IFnsCustomerSwingEmergencyContactRead";
            var result = new FnsResult<List<IFnsCustomerSwingEmergencyContact>>
            {
                Code = (int)ErrorCodes.GeneralMessage
                ,
                Message = string.Format("Initializing {0}", METHOD_NAME)
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the CustomSwingInfo service.
                AE_CustomerSWINGEmergencyContactViewCollection aeCSECList = SosCrmDataContext.Instance.AE_CustomerSWINGEmergencyContactViews.GetCustomerSWINGEmergencyContact(interimAccountId);

                var resultList = aeCSECList.Select(item => new FnsCustomerSwingEmergencyContact(item)).Cast<IFnsCustomerSwingEmergencyContact>().ToList();
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;

            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsCustomerSwingEmergencyContact>>
                {
                    Code = (int)ErrorCodes.UnexpectedException
                    ,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }
        #endregion CustomerSwingEmergencyContact


        #region CustomerSwingEquipmentInfo

        public IFnsResult<List<IFnsCustomerSwingEquipmentInfo>> CustomerSwingEquipmentInfoRead(long interimAccountId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "IFnsCustomerSwingEquipmentInfoRead";
            var result = new FnsResult<List<IFnsCustomerSwingEquipmentInfo>>
            {
                Code = (int)ErrorCodes.GeneralMessage
                , Message = string.Format("Initializing {0}", METHOD_NAME)
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the CustomSwingInfo service.
                AE_CustomerSWINGEquipmentViewCollection aeCSEList = SosCrmDataContext.Instance.AE_CustomerSWINGEquipmentViews.GetCustomerSWINGEquipment(interimAccountId);

                var resultList = aeCSEList.Select(item => new FnsCustomerSwingEquipmentInfo(item)).Cast<IFnsCustomerSwingEquipmentInfo>().ToList();
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;

            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsCustomerSwingEquipmentInfo>>
                {
                    Code = (int)ErrorCodes.UnexpectedException
                    ,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }
        #endregion CustomerSwingEquipmentInfo


        #region CustomerSwingInterim

        public IFnsResult<IFnsCustomerSwingInterim> CustomerSwingInterimPost(long interimAccountId, bool swingEquipment)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "CustomerSwingInterimPost";
            var result = new FnsResult<IFnsCustomerSwingInterim>
            {
                Code = (int)ErrorCodes.GeneralMessage
                ,
                Message = string.Format("Initializing {0}", METHOD_NAME)
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the CustomSwingInfo service.
                AE_CustomerSWINGInterimView list = SosCrmDataContext.Instance.AE_CustomerSWINGInterimViews.CustomerSWINGInterim(interimAccountId, swingEquipment);


                var resultValue = new FnsCustomerSwingInterim(list);
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                //result.Value = resultList;
                result.Value = resultValue;

            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsCustomerSwingInterim>
                {
                    Code = (int)ErrorCodes.UnexpectedException
                    ,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }
        #endregion CustomerSwingEquipmentInfo


        #region CustomerSwingSystemDetails

        public IFnsResult<IFnsCustomerSwingSystemDetails> CustomerSwingSystemDetailsRead(long interimAccountId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "IFnsCustomerSwingSystemDetailsRead";
            var result = new FnsResult<IFnsCustomerSwingSystemDetails>
            {
                Code = (int)ErrorCodes.GeneralMessage
                ,
                Message = string.Format("Initializing {0}", METHOD_NAME)
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the CustomSwingSystemDetails service.
                AE_CustomerSWINGSystemDetailView aeCSSD = SosCrmDataContext.Instance.AE_CustomerSWINGSystemDetailViews.GetCustomerSWINGSystemDetails(interimAccountId);
                

                // ** Build result
                var resultValue = new FnsCustomerSwingSystemDetails(aeCSSD);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;

            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsCustomerSwingSystemDetails>
                {
                    Code = (int)ErrorCodes.UnexpectedException
                    ,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }
        #endregion CustomerSwingSystemDetails

        #region CustomerSwungInfo

        public IFnsResult<IFnsCustomerSwungInfo> CustomerSwungInfoRead(long interimAccountId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "IFnsCustomerSwungInfoRead";
            var result = new FnsResult<IFnsCustomerSwungInfo>
            {
                Code = (int)ErrorCodes.GeneralMessage
                ,
                Message = string.Format("Initializing {0}", METHOD_NAME)
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the CustomSwingSystemDetails service.
                AE_CustomerSWUNGInfoView aeCSSD = SosCrmDataContext.Instance.AE_CustomerSWUNGInfoViews.CustomerSwungInfo(interimAccountId);


                // ** Build result
                var resultValue = new FnsCustomerSwungInfo(aeCSSD);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;

            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsCustomerSwungInfo>
                {
                    Code = (int)ErrorCodes.UnexpectedException
                    ,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }
        #endregion CustomerSwungInfo


        #region SwingAddToDNC

        public IFnsResult<IFnsCustomerSwingAddDnc> CustomerSwingAddDncRead(string phoneNumber)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "IFnsCustomerSwingAddDncRead";
            var result = new FnsResult<IFnsCustomerSwingAddDnc>
            {
                Code = (int)ErrorCodes.GeneralMessage
                ,
                Message = string.Format("Initializing {0}", METHOD_NAME)
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the CustomSwingSwingAddDnc service.
                AE_CustomerSWINGAdd_DncView aeAddDnc = SosCrmDataContext.Instance.AE_CustomerSWINGAdd_DncViews.CustomerSwingAddDnc(phoneNumber);


                // ** Build result
                var resultValue = new FnsCustomerSwingAddDnc(aeAddDnc);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;

            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsCustomerSwingAddDnc>
                {
                    Code = (int)ErrorCodes.UnexpectedException
                    ,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }
        #endregion SwingAddToDNC

    }
}
